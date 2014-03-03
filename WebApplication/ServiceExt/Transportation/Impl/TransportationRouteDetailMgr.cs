using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Transportation;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.Transportation;

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class TransportationRouteDetailMgr : TransportationRouteDetailBaseMgr, ITransportationRouteDetailMgr
    {
        private ICriteriaMgr criteriaMgr;

        public TransportationRouteDetailMgr(ITransportationRouteDetailDao entityDao, 
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<TransportationRouteDetail> GetAllTransportationRouteDetail(string transportationRouteCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TransportationRouteDetail>();
            criteria.Add(Expression.Eq("TransportationRoute.Code", transportationRouteCode));

            return criteriaMgr.FindAll<TransportationRouteDetail>(criteria);
        }

        #endregion Customized Methods
    }
}