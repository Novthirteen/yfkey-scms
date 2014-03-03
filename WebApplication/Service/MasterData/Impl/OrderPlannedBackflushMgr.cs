using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Service.Hql;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class OrderPlannedBackflushMgr : OrderPlannedBackflushBaseMgr, IOrderPlannedBackflushMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IHqlMgr hqlMgr;

        public OrderPlannedBackflushMgr(IOrderPlannedBackflushDao entityDao,
            ICriteriaMgr criteriaMgr, IHqlMgr hqlMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.hqlMgr = hqlMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<object[]> GetActiveOrderPlannedBackflush(string prodLineCode)
        {
            string hql = @"select op.id,olt.Operation,op.PlannedQty,i.Code from OrderPlannedBackflush as op 
                                        join op.OrderLocationTransaction as olt 
                                        join olt.Item as i
                                        where op.Flow = ? and op.IsActive = ?";
            return hqlMgr.FindAll<object[]>(hql,
               new Object[] {prodLineCode,true
                });
        }


        #endregion Customized Methods
    }
}