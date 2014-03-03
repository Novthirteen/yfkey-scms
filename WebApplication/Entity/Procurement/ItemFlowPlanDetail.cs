using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Procurement
{
    [Serializable]
    public class ItemFlowPlanDetail : ItemFlowPlanDetailBase
    {
        #region Non O/R Mapping Properties

        public void AddItemFlowPlanTrack(ItemFlowPlanTrack itemFlowPlanTrack)
        {
            if (this.ItemFlowPlanTracks == null)
            {
                this.ItemFlowPlanTracks = new List<ItemFlowPlanTrack>();
            }

            this.ItemFlowPlanTracks.Add(itemFlowPlanTrack);
        }

        public void AddRangeItemFlowPlanTrack(IList<ItemFlowPlanTrack> itemFlowPlanTrackList)
        {
            if (itemFlowPlanTrackList != null && itemFlowPlanTrackList.Count > 0)
            {
                foreach (ItemFlowPlanTrack itemFlowPlanTrack in itemFlowPlanTrackList)
                {
                    this.AddItemFlowPlanTrack(itemFlowPlanTrack);
                }
            }
        }

        private Decimal _existPlanQty;
        public Decimal ExistPlanQty
        {
            get
            {
                return _existPlanQty;
            }
            set
            {
                _existPlanQty = value;
            }
        }

        private Decimal _orderRemainQty;
        public Decimal OrderRemainQty
        {
            get
            {
                return _orderRemainQty;
            }
            set
            {
                _orderRemainQty = value;
            }
        }

        private Decimal _pAB;
        public Decimal PAB
        {
            get
            {
                return _pAB;
            }
            set
            {
                _pAB = value;
            }
        }

        #endregion
    }
}