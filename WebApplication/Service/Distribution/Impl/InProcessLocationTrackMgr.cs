using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Distribution;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Distribution.Impl
{
    [Transactional]
    public class InProcessLocationTrackMgr : InProcessLocationTrackBaseMgr, IInProcessLocationTrackMgr
    {
        private IRoutingDetailMgr routingDetailMgr;
        private ICriteriaMgr criteriaMgr;
        public InProcessLocationTrackMgr(IInProcessLocationTrackDao entityDao,
            IRoutingDetailMgr routingDetailMgr,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.routingDetailMgr = routingDetailMgr;
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public IList<InProcessLocationTrack> CreateIInProcessLocationTrack(InProcessLocation inProcessLocation, Routing routing)
        {
            IList<RoutingDetail> routingDetailList = this.routingDetailMgr.GetRoutingDetail(routing, DateTime.Now);

            if (routingDetailList != null && routingDetailList.Count > 0)
            {
                IList<InProcessLocationTrack> inProcessLocationTrackList = new List<InProcessLocationTrack>();
                foreach (RoutingDetail routingDetail in routingDetailList)
                {
                    InProcessLocationTrack inProcessLocationTrack = new InProcessLocationTrack();
                    inProcessLocationTrack.IpProcessLocation = inProcessLocation;
                    inProcessLocationTrack.Operation = routingDetail.Operation;
                    inProcessLocationTrack.Activity = routingDetail.Activity;
                    inProcessLocationTrack.WorkCenter = routingDetail.WorkCenter;

                    this.entityDao.CreateInProcessLocationTrack(inProcessLocationTrack);
                    inProcessLocationTrackList.Add(inProcessLocationTrack);
                }

                return inProcessLocationTrackList;
            }

            return null;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InProcessLocationTrack> GetInProcessLocationTrack(string ipNo,int op)
        {
            DetachedCriteria criteria = DetachedCriteria.For<InProcessLocationTrack>();
            criteria.Add(Expression.Eq("IpProcessLocation.IpNo", ipNo));
            criteria.Add(Expression.Ge("Operation", op));
            return criteriaMgr.FindAll<InProcessLocationTrack>(criteria);
        }

        #endregion Customized Methods
    }
}