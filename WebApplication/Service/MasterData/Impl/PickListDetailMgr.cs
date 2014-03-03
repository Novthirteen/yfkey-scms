using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class PickListDetailMgr : PickListDetailBaseMgr, IPickListDetailMgr
    {
        private ICriteriaMgr criteriaMgr;
        public PickListDetailMgr(IPickListDetailDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public PickListDetail LoadPickListDetail(int pickListDetailId, bool includePickListResult)
        {
            PickListDetail pickListDetail = base.LoadPickListDetail(pickListDetailId);
            if (includePickListResult && pickListDetail != null && pickListDetail.PickListResults != null && pickListDetail.PickListResults.Count > 0)
            {
            }
            return pickListDetail;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<PickListDetail> GetPickListDetail(string locationCode, string itemCode, decimal? unitCount, string uomCode, string[] status)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PickListDetail>();

            if (locationCode != null && locationCode.Trim() != string.Empty)
            {
                criteria.CreateAlias("Location", "l");
                criteria.Add(Expression.Eq("l.Code", locationCode));
            }

            if (itemCode != null && itemCode.Trim() != string.Empty)
            {
                criteria.CreateAlias("Item", "i");
                criteria.Add(Expression.Eq("i.Code", itemCode));
            }

            if (uomCode != null && uomCode.Trim() != string.Empty)
            {
                criteria.CreateAlias("Uom", "u");
                criteria.Add(Expression.Eq("u.Code", uomCode));
            }

            if (unitCount.HasValue)
            {
                criteria.Add(Expression.Eq("UnitCount", unitCount));
            }

            if (status != null)
            {
                criteria.CreateAlias("PickList", "pl");
                if (status.Length == 1)
                {
                    criteria.Add(Expression.Eq("pl.Status", status[0]));
                }
                else if (status.Length > 1)
                {
                    criteria.Add(Expression.In("pl.Status", status));
                }
            }

            return this.criteriaMgr.FindAll<PickListDetail>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<PickListDetail> GetPickedPickListDetail(int orderLocationTransactionId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PickListDetail>();
            criteria.CreateAlias("OrderLocationTransaction", "olt");
            criteria.CreateAlias("PickList", "pk");

            IList<string> statusList = new List<string>();
            statusList.Add(BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT);
            statusList.Add(BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS);

            criteria.Add(Expression.Eq("olt.Id", orderLocationTransactionId));
            if (statusList != null && statusList.Count > 0)
            {
                if (statusList.Count == 1)
                {
                    criteria.Add(Expression.Eq("pk.Status", statusList[0]));
                }
                else
                {
                    criteria.Add(Expression.InG<string>("pk.Status", statusList));
                }
            }
            return this.criteriaMgr.FindAll<PickListDetail>(criteria);
        }
        #endregion Customized Methods
    }
}