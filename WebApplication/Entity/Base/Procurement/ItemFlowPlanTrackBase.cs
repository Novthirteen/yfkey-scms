using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Procurement
{
    [Serializable]
    public abstract class ItemFlowPlanTrackBase : EntityBase
    {
        #region O/R Mapping Properties

        private Int32 _id;
        public Int32 Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private com.Sconit.Entity.Procurement.ItemFlowPlanDetail _itemFlowPlanDetail;
        public com.Sconit.Entity.Procurement.ItemFlowPlanDetail ItemFlowPlanDetail
        {
            get
            {
                return _itemFlowPlanDetail;
            }
            set
            {
                _itemFlowPlanDetail = value;
            }
        }
        private com.Sconit.Entity.MasterData.OrderLocationTransaction _orderLocationTransaction;
        public com.Sconit.Entity.MasterData.OrderLocationTransaction OrderLocationTransaction
        {
            get
            {
                return _orderLocationTransaction;
            }
            set
            {
                _orderLocationTransaction = value;
            }
        }
        private ItemFlowPlanDetail _referencePlanDetail;
        public ItemFlowPlanDetail ReferencePlanDetail
        {
            get
            {
                return _referencePlanDetail;
            }
            set
            {
                _referencePlanDetail = value;
            }
        }
        private Decimal _rate;
        public Decimal Rate
        {
            get
            {
                return _rate;
            }
            set
            {
                _rate = value;
            }
        }

        #endregion

        public override int GetHashCode()
        {
            if (Id != 0)
            {
                return Id.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            ItemFlowPlanTrackBase another = obj as ItemFlowPlanTrackBase;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.Id == another.Id);
            }
        }
    }

}
