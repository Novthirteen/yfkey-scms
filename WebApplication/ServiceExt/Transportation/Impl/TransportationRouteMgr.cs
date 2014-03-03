using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Transportation;
using com.Sconit.Entity.Transportation;

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class TransportationRouteMgr : TransportationRouteBaseMgr, ITransportationRouteMgr
    {
        private ITransportationRouteDetailMgr transportationRouteDetailMgr;

        public TransportationRouteMgr(ITransportationRouteDao entityDao,
            ITransportationRouteDetailMgr transportationRouteDetailMgr)
            : base(entityDao)
        {
            this.transportationRouteDetailMgr = transportationRouteDetailMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public override void DeleteTransportationRoute(String code)
        {
            IList<TransportationRouteDetail> transportationRouteDetails = this.transportationRouteDetailMgr.GetAllTransportationRouteDetail(code);

            if (transportationRouteDetails != null)
                this.transportationRouteDetailMgr.DeleteTransportationRouteDetail(transportationRouteDetails);

            base.DeleteTransportationRoute(code);
        }

        [Transaction(TransactionMode.Requires)]
        public override void DeleteTransportationRoute(TransportationRoute entity)
        {
            DeleteTransportationRoute(entity.Code);
        }

        #endregion Customized Methods
    }
}