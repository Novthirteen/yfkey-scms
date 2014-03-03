using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Persistence.Procurement;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement.Impl
{
    [Transactional]
    public class ItemFlowPlanTrackMgr : ItemFlowPlanTrackBaseMgr, IItemFlowPlanTrackMgr
    {
        private ICriteriaMgr CriteriaMgr;
        public ItemFlowPlanTrackMgr(
            IItemFlowPlanTrackDao entityDao,
            ICriteriaMgr CriteriaMgr)
            : base(entityDao)
        {
            this.CriteriaMgr = CriteriaMgr;
        }

        #region Public Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<ItemFlowPlanTrack> GenerateItemFlowPlanTrack(ItemFlowPlanDetail parentItemFlowPlanDetail, ItemFlowPlanDetail itemFlowPlanDetail, decimal qtyPer)
        {
            IList<ItemFlowPlanTrack> itemFlowPlanTrackList = new List<ItemFlowPlanTrack>();
            if (parentItemFlowPlanDetail.ItemFlowPlanTracks != null && parentItemFlowPlanDetail.ItemFlowPlanTracks.Count > 0)
            {
                foreach (ItemFlowPlanTrack parentItemFlowPlanTrack in parentItemFlowPlanDetail.ItemFlowPlanTracks)
                {
                    ItemFlowPlanTrack itemFlowPlanTrack = new ItemFlowPlanTrack();
                    itemFlowPlanTrack.ItemFlowPlanDetail = itemFlowPlanDetail;
                    itemFlowPlanTrack.ReferencePlanDetail = parentItemFlowPlanTrack.ReferencePlanDetail;
                    itemFlowPlanTrack.OrderLocationTransaction = parentItemFlowPlanTrack.OrderLocationTransaction;
                    //itemFlowPlanTrack.DemandQty = parentItemFlowPlanTrack.DemandQty * qtyPer;
                    itemFlowPlanTrackList.Add(itemFlowPlanTrack);
                }
            }

            return itemFlowPlanTrackList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ItemFlowPlanTrack> GetItemFlowPlanTrackList(ItemFlowPlanDetail mstrIfpd, ItemFlowPlanDetail refIfpd, OrderLocationTransaction orderLocTrans)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ItemFlowPlanTrack));
            if (mstrIfpd != null)
                criteria.Add(Expression.Eq("ItemFlowPlanDetail.Id", mstrIfpd.Id));
            if (refIfpd != null)
                criteria.Add(Expression.Eq("ReferencePlanDetail.Id", refIfpd.Id));
            if (orderLocTrans != null)
                criteria.Add(Expression.Eq("OrderLocationTransaction.Id", orderLocTrans.Id));

            return CriteriaMgr.FindAll<ItemFlowPlanTrack>(criteria);
        }

        [Transaction(TransactionMode.Requires)]
        public void ClearOldRelation(ItemFlowPlanDetail mstrIfpd, ItemFlowPlanDetail refIfpd, OrderLocationTransaction orderLocTrans)
        {
            IList<ItemFlowPlanTrack> ifptList = this.GetItemFlowPlanTrackList(mstrIfpd, refIfpd, orderLocTrans);
            if (ifptList != null && ifptList.Count > 0)
            {
                foreach (ItemFlowPlanTrack ifpt in ifptList)
                {
                    this.DeleteItemFlowPlanTrack(ifpt);
                }
            }
        }

        #endregion Public Methods

    }
}