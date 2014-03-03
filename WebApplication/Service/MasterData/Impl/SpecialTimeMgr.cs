using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Persistence.Criteria;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Service.Criteria;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class SpecialTimeMgr : SpecialTimeBaseMgr, ISpecialTimeMgr
    {
        private ICriteriaMgr criteriaMgr;
        public SpecialTimeMgr(ISpecialTimeDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public IList GetReferSpecialTimeWizard(DateTime starttime, DateTime endtime, string region, string workcenter)
        {
            IList specialTimes = new List<SpecialTime>();
            specialTimes = this.GetSpecialTime(starttime, endtime, region, workcenter);
            if (specialTimes.Count > 0)
            {
                return specialTimes;
            }

            workcenter = null;
            specialTimes = this.GetSpecialTime(starttime, endtime, region, workcenter);
            if (specialTimes.Count > 0)
            {
                return specialTimes;
            }

            region = null;
            specialTimes = this.GetSpecialTime(starttime, endtime, region, workcenter);

            return this.SpecialTimeDataClean(specialTimes);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList GetSpecialTime(DateTime starttime, DateTime endtime, string region, string workcenter)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(SpecialTime));
            criteria.Add(Expression.Or(
                Expression.And(Expression.Le("StartTime", starttime), Expression.Gt("EndTime", starttime)),
                Expression.And(Expression.Lt("StartTime", endtime), Expression.Ge("EndTime", endtime))));
            if (workcenter == null || workcenter.Trim() == "")
            {
                criteria.Add(Expression.IsNull("WorkCenter.Code"));
            }
            else
            {
                criteria.Add(Expression.Eq("WorkCenter.Code", workcenter));
            }
            if (region == null || region.Trim() == "")
            {
                criteria.Add(Expression.IsNull("Region.Code"));
            }
            else
            {
                criteria.Add(Expression.Eq("Region.Code", region));
            }
            criteria.AddOrder(Order.Asc("StartTime"));
            criteria.AddOrder(Order.Asc("EndTime"));

            return criteriaMgr.FindAll(criteria);
        }

        private IList SpecialTimeDataClean(IList specialTimes)
        {
            IList removeList = new List<SpecialTime>();

            if (specialTimes.Count > 1)
            {
                foreach (SpecialTime st in specialTimes)
                {
                    int index = specialTimes.IndexOf(st);
                    if (index == 0)
                    {
                        continue;
                    }

                    //work > rest
                    if (((SpecialTime)specialTimes[index - 1]).Type == st.Type)
                    {
                        if (DateTime.Compare(((SpecialTime)specialTimes[index - 1]).EndTime, st.StartTime) >= 0)
                        {
                            st.StartTime = ((SpecialTime)specialTimes[index - 1]).StartTime;
                            removeList.Add(specialTimes[specialTimes.IndexOf(st) - 1]);
                        }
                    }
                    else
                    {
                        if (DateTime.Compare(((SpecialTime)specialTimes[index - 1]).EndTime, st.StartTime) > 0)
                        {
                            if (st.Type == BusinessConstants.CODE_MASTER_WORKCALENDAR_TYPE_VALUE_WORK)
                            {
                                ((SpecialTime)specialTimes[index - 1]).EndTime = st.StartTime;
                            }
                            else
                            {
                                st.StartTime = ((SpecialTime)specialTimes[index - 1]).EndTime;
                            }
                        }
                    }
                }
            }

            IList returnList = new List<SpecialTime>();
            if (removeList.Count > 0)
            {
                foreach (SpecialTime returnSpecialTime in specialTimes)
                {
                    if (removeList.Contains(returnSpecialTime))
                    {
                        continue;
                    }
                    else
                    {
                        returnList.Add(returnSpecialTime);
                    }
                }
            }
            else
            {
                returnList = specialTimes;
            }

            return returnList;
        }

        #endregion Customized Methods
    }
}