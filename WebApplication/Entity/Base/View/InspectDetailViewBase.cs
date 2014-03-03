using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public abstract class InspectDetailViewBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private com.Sconit.Entity.MasterData.InspectOrder _inspectOrder;
        public com.Sconit.Entity.MasterData.InspectOrder InspectOrder
		{
			get
			{
                return _inspectOrder;
			}
			set
			{
                _inspectOrder = value;
			}
		}
		private com.Sconit.Entity.MasterData.Item _item;
		public com.Sconit.Entity.MasterData.Item Item
		{
			get
			{
				return _item;
			}
			set
			{
				_item = value;
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
		private Decimal _remainQty;
		public Decimal RemainQty
		{
			get
			{
				return _remainQty;
			}
			set
			{
				_remainQty = value;
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
            InspectDetailViewBase another = obj as InspectDetailViewBase;

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
