using com.Sconit.Service.Ext.MasterData;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class UomConversionMgr : UomConversionBaseMgr, IUomConversionMgr
    {
        private ICriteriaMgr criteriaMgr;
        public UomConversionMgr(IUomConversionDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public decimal ConvertUomQty(string itemCode, Uom sourceUom, decimal sourceQty, Uom targetUom)
        {
            return ConvertUomQty(itemCode, sourceUom.Code, sourceQty, targetUom.Code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public decimal ConvertUomQty(string itemCode, string sourceUomCode, decimal sourceQty, string targetUomCode)
        {
            if (sourceUomCode == targetUomCode)
            {
                return sourceQty;
            }

            UomConversion uomConversion = this.LoadUomConversion(itemCode, sourceUomCode, targetUomCode);
            if (uomConversion != null)
            {
                return (sourceQty * uomConversion.BaseQty / uomConversion.AlterQty);
            }
            else
            {
                uomConversion = this.LoadUomConversion(itemCode, targetUomCode, sourceUomCode);
                if (uomConversion != null)
                {
                    return (sourceQty * uomConversion.AlterQty / uomConversion.BaseQty);
                }
                else
                {
                    uomConversion = this.LoadUomConversion(null, sourceUomCode, targetUomCode);
                    if (uomConversion != null)
                    {
                        return (sourceQty * uomConversion.BaseQty / uomConversion.AlterQty);
                    }
                    else
                    {
                        uomConversion = this.LoadUomConversion(null, targetUomCode, sourceUomCode);
                        if (uomConversion != null)
                        {
                            return (sourceQty * uomConversion.AlterQty / uomConversion.BaseQty);
                        }
                        else
                        {
                            throw new BusinessErrorException("UomConversion.Error.NotFound", itemCode, sourceUomCode, targetUomCode);
                        }
                    }

                }
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public decimal ConvertUomQty(Item item, Uom sourceUom, decimal sourceQty, Uom targetUom)
        {
            return ConvertUomQty(item.Code, sourceUom, sourceQty, targetUom);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList GetUomConversion(string itemCode, string altUom, string baseUom)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(UomConversion));
            if (itemCode != string.Empty && itemCode != null)
                criteria.Add(Expression.Like("Item.Code", itemCode, MatchMode.Start));
            if (altUom != string.Empty && altUom != null)
                criteria.Add(Expression.Like("AlterUom.Code", altUom, MatchMode.Start));
            if (baseUom != string.Empty && baseUom != null)
                criteria.Add(Expression.Like("BaseUom.Code", baseUom, MatchMode.Start));
            return criteriaMgr.FindAll(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<UomConversion> GetUomConversion(string itemCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(UomConversion));
            criteria.Add(Expression.Eq("Item.Code", itemCode));
            return criteriaMgr.FindAll<UomConversion>(criteria);

        }

        [Transaction(TransactionMode.Unspecified)]
        public override UomConversion LoadUomConversion(string itemCode, string alterUomCode, string baseUomCode)
        {
            if (itemCode != null && itemCode != string.Empty)
            {
                return base.LoadUomConversion(itemCode, alterUomCode, baseUomCode);
            }
            else
            {
                DetachedCriteria criteria = DetachedCriteria.For(typeof(UomConversion));
                criteria.Add(Expression.IsNull("Item"));
                if (alterUomCode != string.Empty && alterUomCode != null)
                    criteria.Add(Expression.Eq("AlterUom.Code", alterUomCode));
                if (baseUomCode != string.Empty && baseUomCode != null)
                    criteria.Add(Expression.Eq("BaseUom.Code", baseUomCode));
                IList<UomConversion> uomConversionList = criteriaMgr.FindAll<UomConversion>(criteria);
                if (uomConversionList != null && uomConversionList.Count > 0)
                {
                    return uomConversionList[0];
                }
                else
                    return null;
            }

        }

        public decimal ConvertUomQtyInvToOrder(FlowDetail flowDetail, decimal invQty)
        {
            return this.ConvertUomQty(flowDetail.Item, flowDetail.Item.Uom, invQty, flowDetail.Uom);
        }

        #endregion Customized Methods
    }
}
