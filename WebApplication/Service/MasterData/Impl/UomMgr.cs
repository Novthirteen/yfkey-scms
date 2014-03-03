using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity.Exception;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class UomMgr : UomBaseMgr, IUomMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IUomConversionMgr uomConversionMgr;
        private IItemMgr itemMgr;
        public UomMgr(IUomDao entityDao, ICriteriaMgr criteriaMgr, IUomConversionMgr uomConversionMgr, IItemMgr itemMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.itemMgr = itemMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public Uom CheckAndLoadUom(string uomCode)
        {
            Uom uom = this.LoadUom(uomCode);
            if (uom == null)
            {
                throw new BusinessErrorException("Uom.Error.UomCodeNotExist", uomCode);
            }

            return uom;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList GetUom(string code, string name, string desc)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Uom));
            if (code != string.Empty && code != null)
                criteria.Add(Expression.Like("Code", code, MatchMode.Start));
            if (name != string.Empty && name != null)
                criteria.Add(Expression.Like("Name", name, MatchMode.Start));
            if (desc != string.Empty && desc != null)
                criteria.Add(Expression.Like("Description", desc, MatchMode.Start));
            return criteriaMgr.FindAll(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Uom> GetItemUom(string itemCode)
        {
            List<Uom> uomList = new List<Uom>();
            if (itemCode != null && itemCode != string.Empty)
            {
                IList<UomConversion> uomConversionList = uomConversionMgr.GetUomConversion(itemCode);

                //添加基本单位
                uomList.Add(itemMgr.LoadItem(itemCode).Uom);
                //添加单位转换中的单位
                foreach (UomConversion uomConversion in uomConversionList)
                {
                    if (!uomList.Contains(uomConversion.BaseUom))
                        uomList.Add(uomConversion.BaseUom);
                    if (!uomList.Contains(uomConversion.AlterUom))
                        uomList.Add(uomConversion.AlterUom);
                }
            }
            return uomList;
        }

        #endregion Customized Methods
    }
}