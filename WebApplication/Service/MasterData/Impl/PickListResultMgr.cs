using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Transform;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class PickListResultMgr : PickListResultBaseMgr, IPickListResultMgr
    {
        private ICriteriaMgr criteriaMgr;
        public PickListResultMgr(IPickListResultDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<PickListResult> GetPickListResult(int pickListDetailId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PickListResult>();
            criteria.Add(Expression.Eq("PickListDetail.Id", pickListDetailId));
            return this.criteriaMgr.FindAll<PickListResult>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<PickListResult> GetPickListResult(PickListDetail pickListDetail)
        {
            return GetPickListResult(pickListDetail.Id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<PickListResult> GetPickListResult(string pickListNo)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PickListResult>();
            criteria.CreateAlias("PickListDetail", "pld");
            criteria.CreateAlias("pld.PickList", "pl");
            criteria.Add(Expression.Eq("pl.PickListNo", pickListNo));
            return this.criteriaMgr.FindAll<PickListResult>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<PickListResult> GetPickListResult(string locationCode, string itemCode, decimal? unitCount, string uomCode, string[] status)
        {
            return GetPickListResult(new string[] { locationCode }, new string[] { itemCode }, unitCount, uomCode, status);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<PickListResult> GetPickListResult(string[] locationCodes, string[] itemCodes, decimal? unitCount, string uomCode, string[] status)
        {
            return GetPickListResult(locationCodes, itemCodes, unitCount, uomCode, status, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<PickListResult> GetPickListResult(string[] locationCodes, string[] itemCodes, decimal? unitCount, string uomCode, string[] status, bool isGroup)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PickListResult>();
            criteria.CreateAlias("LocationLotDetail", "lld");
            criteria.CreateAlias("PickListDetail", "pld");

            if (locationCodes != null && locationCodes.Length > 0)
            {
                criteria.CreateAlias("lld.Location", "l");
                if (locationCodes.Length == 1)
                {
                    criteria.Add(Expression.Eq("l.Code", locationCodes[0]));
                }
                else
                {
                    criteria.Add(Expression.In("l.Code", locationCodes));
                }
            }

            if (itemCodes != null && itemCodes.Length > 0)
            {
                criteria.CreateAlias("lld.Item", "i");
                if (itemCodes.Length == 1)
                {
                    criteria.Add(Expression.Eq("i.Code", itemCodes[0]));
                }
                else
                {
                    criteria.Add(Expression.In("i.Code", itemCodes));
                }
            }

            if (uomCode != null && uomCode.Trim() != string.Empty)
            {
                criteria.CreateAlias("pld.Uom", "u");
                criteria.Add(Expression.Eq("u.Code", uomCode));
            }

            if (unitCount.HasValue)
            {
                criteria.Add(Expression.Eq("pld.UnitCount", unitCount));
            }

            if (status != null && status.Length > 0)
            {
                criteria.CreateAlias("pld.PickList", "pl");
                if (status.Length == 1)
                {
                    criteria.Add(Expression.Eq("pl.Status", status[0]));
                }
                else
                {
                    criteria.Add(Expression.In("pl.Status", status));
                }
            }
            if (isGroup)
            {
                criteria.SetProjection(Projections.ProjectionList()
                    .Add(Projections.GroupProperty("lld.Location.Code").As("LocationCode"))
                    .Add(Projections.GroupProperty("lld.Item.Code").As("ItemCode"))
                    .Add(Projections.GroupProperty("pld.Uom.Code").As("UomCode"))
                    .Add(Projections.GroupProperty("pld.UnitCount").As("UnitCount"))
                    .Add(Projections.GroupProperty("pl.Status").As("Status"))
                    .Add(Projections.Sum("Qty").As("Qty")));
                criteria.SetResultTransformer(Transformers.AliasToBean(typeof(PickListResult)));
            }
            return this.criteriaMgr.FindAll<PickListResult>(criteria);
        }
        #endregion Customized Methods
    }
}