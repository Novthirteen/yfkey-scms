using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MRP;
using com.Sconit.Entity.MRP;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity;
using System.Linq;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP.Impl
{
    [Transactional]
    public class MrpShipPlanMgr : MrpShipPlanBaseMgr, IMrpShipPlanMgr
    {
        private ICriteriaMgr criteriaMgr;

        public MrpShipPlanMgr(IMrpShipPlanDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        //[1]added by williamlu@esteering.cn
        //[1]2012/5
        [Transaction(TransactionMode.Requires)]
        public IList<MrpShipPlan> GetMrpShipPlansBB(string flowCode, string locCode, string itemCode, List<string> itemCodes, DateTime effectiveDate, DateTime? winDate, DateTime? startDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(MrpShipPlan));
            if (itemCode != null && itemCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Like("Item", itemCode, MatchMode.Start));
            }

            //[2]in参数超过2100报错的解决
            //[2]added by williamlu@esteering.cn
            //[2]2012/05
            int LimitCount = 2000;
            double dLoopCycle = (itemCodes.Count / LimitCount);
            int iLoopCycle = Convert.ToInt32(Math.Ceiling(dLoopCycle));
            iLoopCycle = iLoopCycle == 0 ? 1 : (iLoopCycle + 1);

            string strItem = string.Empty;
            string tempSql = string.Empty;
            for (int i = 0; i < iLoopCycle; i++)
            {
                int rangeIndex = i * LimitCount;
                List<string> tempItemCodes = itemCodes.GetRange(rangeIndex, Math.Min(LimitCount, itemCodes.Count - rangeIndex));

                strItem = "";
                for (int j = 0; j < Math.Min(LimitCount, itemCodes.Count - rangeIndex); j++)
                {
                    strItem += "'" + tempItemCodes[j] + "'" + ",";
                }
                strItem = strItem.Substring(0, strItem.Length - 1);

                if (i == 0)
                {
                    tempSql = string.Format("Item in ({0})", strItem);
                }
                else
                {
                    tempSql = "(" + tempSql + string.Format(" or Item in ({0})", strItem) + ")";
                }
            }
            criteria.Add(Expression.Sql(tempSql));
            //[2]added end            

            if (flowCode != null && flowCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("Flow", flowCode));
            }
            if (locCode != null && locCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("LocationTo", locCode));
            }
            criteria.Add(Expression.Eq("EffectiveDate", effectiveDate.Date));

            if (winDate.HasValue)
            {
                criteria.Add(Expression.Ge("WindowTime", winDate.Value.Date));
                criteria.Add(Expression.Lt("WindowTime", winDate.Value.Date.AddDays(1)));
            }
            if (startDate.HasValue)
            {
                criteria.Add(Expression.Ge("StartTime", startDate.Value.Date));
                criteria.Add(Expression.Lt("StartTime", startDate.Value.Date.AddDays(1)));

            }

            IList<MrpShipPlan> mrpShipPlan = criteriaMgr.FindAll<MrpShipPlan>(criteria);

            mrpShipPlan = mrpShipPlan.OrderBy(m => m.WindowTime).ThenBy(m => m.Item).ThenBy(m => m.Id).ToList();
            return mrpShipPlan;
        }
        //[1]added end

        [Transaction(TransactionMode.Requires)]
        public IList<MrpShipPlan> GetMrpShipPlans(string flowCode, string locCode, string itemCode, DateTime effectiveDate, DateTime? winDate, DateTime? startDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(MrpShipPlan));
            if (itemCode != null && itemCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Like("Item", itemCode, MatchMode.Start));
            }
            if (flowCode != null && flowCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("Flow", flowCode));
            }
            if (locCode != null && locCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("LocationTo", locCode));
            }
            criteria.Add(Expression.Eq("EffectiveDate", effectiveDate.Date));

            if (winDate.HasValue)
            {
                criteria.Add(Expression.Ge("WindowTime", winDate.Value.Date));
                criteria.Add(Expression.Lt("WindowTime", winDate.Value.Date.AddDays(1)));
            }
            if (startDate.HasValue)
            {
                criteria.Add(Expression.Ge("StartTime", startDate.Value.Date));
                criteria.Add(Expression.Lt("StartTime", startDate.Value.Date.AddDays(1)));
               
            }

            IList<MrpShipPlan> mrpShipPlan = criteriaMgr.FindAll<MrpShipPlan>(criteria);
            mrpShipPlan = mrpShipPlan.OrderBy(m => m.WindowTime).ThenBy(m => m.Item).ThenBy(m => m.Id).ToList();
            return mrpShipPlan;
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateMrpShipPlan(IList<MrpShipPlan> mrpShipPlans)
        {
            foreach (MrpShipPlan mrpShipPlan in mrpShipPlans)
            {
                UpdateMrpShipPlan(mrpShipPlan);
            }
        }

        #endregion Customized Methods
    }
}
