using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ShiftDetailMgr : ShiftDetailBaseMgr, IShiftDetailMgr
    {
        private ICriteriaMgr criteriaMgr;
        public ShiftDetailMgr(
            IShiftDetailDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<ShiftDetail> GetShiftDetail(string shiftCode, DateTime date)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ShiftDetail));
            criteria.Add(Expression.Eq("Shift.Code", shiftCode));
            criteria.Add(Expression.Or(Expression.Le("StartDate", date), Expression.IsNull("StartDate")));
            criteria.Add(Expression.Or(Expression.Ge("EndDate", date), Expression.IsNull("EndDate")));
            criteria.AddOrder(Order.Desc("StartDate"));

            return criteriaMgr.FindAll<ShiftDetail>(criteria);
        }

        #endregion Customized Methods
    }
}