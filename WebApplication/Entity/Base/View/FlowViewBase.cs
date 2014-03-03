using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public abstract class FlowViewBase : EntityBase
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
		private Boolean _isAutoCreate;
		public Boolean IsAutoCreate
		{
			get
			{
				return _isAutoCreate;
			}
			set
			{
				_isAutoCreate = value;
			}
		}
		private com.Sconit.Entity.MasterData.Location _locationFrom;
		public com.Sconit.Entity.MasterData.Location LocationFrom
		{
			get
			{
				return _locationFrom;
			}
			set
			{
				_locationFrom = value;
			}
		}
		private com.Sconit.Entity.MasterData.Location _locationTo;
		public com.Sconit.Entity.MasterData.Location LocationTo
		{
			get
			{
				return _locationTo;
			}
			set
			{
				_locationTo = value;
			}
		}
		private string _referenceFlow;
		public string ReferenceFlow
		{
			get
			{
				return _referenceFlow;
			}
			set
			{
				_referenceFlow = value;
			}
		}
        
        #endregion

		public override int GetHashCode()
        {
			if (Id != null)
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
            FlowViewBase another = obj as FlowViewBase;

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
