using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Procurement
{
    [Serializable]
    public abstract class ItemFlowPlanBase : EntityBase
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
        private com.Sconit.Entity.MasterData.Flow _flow;
        public com.Sconit.Entity.MasterData.Flow Flow
        {
            get
            {
                return _flow;
            }
            set
            {
                _flow = value;
            }
        }
        private com.Sconit.Entity.MasterData.FlowDetail _flowDetail;
        public com.Sconit.Entity.MasterData.FlowDetail FlowDetail
        {
            get
            {
                return _flowDetail;
            }
            set
            {
                _flowDetail = value;
            }
        }
        private string _planType;
        public string PlanType
        {
            get
            {
                return _planType;
            }
            set
            {
                _planType = value;
            }
        }
        private IList<ItemFlowPlanDetail> _itemFlowPlanDetails;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<ItemFlowPlanDetail> ItemFlowPlanDetails
        {
            get
            {
                return _itemFlowPlanDetails;
            }
            set
            {
                _itemFlowPlanDetails = value;
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
            ItemFlowPlanBase another = obj as ItemFlowPlanBase;

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
