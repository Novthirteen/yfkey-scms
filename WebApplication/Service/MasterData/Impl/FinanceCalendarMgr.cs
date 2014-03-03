using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity.Exception;
using System.Linq;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class FinanceCalendarMgr : FinanceCalendarBaseMgr, IFinanceCalendarMgr
    {
        private ICriteriaMgr criteriaMgr;
        public FinanceCalendarMgr(IFinanceCalendarDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }
        #region Customized Methods

        public FinanceCalendar GetLastestOpenFinanceCalendar()
        {
            DetachedCriteria criteria = DetachedCriteria.For<FinanceCalendar>();
            criteria.Add(Expression.Eq("IsClosed", false));
            criteria.AddOrder(Order.Asc("StartDate"));
            IList<FinanceCalendar> financeCalendarList = this.criteriaMgr.FindAll<FinanceCalendar>(criteria, 0, 1);
            if (financeCalendarList != null && financeCalendarList.Count > 0)
            {
                return financeCalendarList[0];
            }
            else
            {
                throw new BusinessErrorException("FinanceCalendar.Error.OpenFinanceCalendarNotFind");
            }
        }

        public FinanceCalendar GetFinanceCalendar(Int32 year, Int32 month)
        {
            DetachedCriteria criteria = DetachedCriteria.For<FinanceCalendar>();
            criteria.Add(Expression.Eq("FinanceYear", year));
            criteria.Add(Expression.Eq("FinanceMonth", month));
            IList<FinanceCalendar> financeCalendarList = this.criteriaMgr.FindAll<FinanceCalendar>(criteria);
            if (financeCalendarList != null && financeCalendarList.Count > 0)
            {
                return financeCalendarList[0];
            }
            else
            {
                return null;
            }
        }

        public FinanceCalendar GetFinanceCalendar(Int32 year, Int32 month, int interval)//通过当前会计月进行加减得到过去或未来的会计月
        {
            DetachedCriteria criteria = DetachedCriteria.For<FinanceCalendar>();

            criteria.AddOrder(Order.Asc("FinanceYear"));
            criteria.AddOrder(Order.Asc("FinanceMonth"));
            IList<FinanceCalendar> financeCalendarList = this.criteriaMgr.FindAll<FinanceCalendar>(criteria);

            FinanceCalendar fc = this.GetFinanceCalendar(year, month);
            int seq = 0;
            if (fc != null)
                seq = financeCalendarList.IndexOf(this.GetFinanceCalendar(year, month));
            if (seq + interval >= 0 && seq + interval < financeCalendarList.Count)
            {
                return financeCalendarList[seq + interval];
            }
            else
            {
                return null;
            }
        }

        #endregion Customized Methods
    }
}