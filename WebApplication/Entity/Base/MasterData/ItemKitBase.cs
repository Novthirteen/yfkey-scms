using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class ItemKitBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private com.Sconit.Entity.MasterData.Item _parentItem;
		public com.Sconit.Entity.MasterData.Item ParentItem
		{
			get
			{
				return _parentItem;
			}
			set
			{
				_parentItem = value;
			}
		}
		private com.Sconit.Entity.MasterData.Item _childItem;
		public com.Sconit.Entity.MasterData.Item ChildItem
		{
			get
			{
				return _childItem;
			}
			set
			{
				_childItem = value;
			}
		}
		private Decimal _qty;
		public Decimal Qty
		{
			get
			{
				return _qty;
			}
			set
			{
				_qty = value;
			}
		}
		private Boolean _isActive;
		public Boolean IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
			}
		}
        
        #endregion

		public override int GetHashCode()
        {
			if (ParentItem != null && ChildItem != null)
            {
                return ParentItem.GetHashCode() ^ ChildItem.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            ItemKitBase another = obj as ItemKitBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.ParentItem == another.ParentItem) && (this.ChildItem == another.ChildItem);
            }
        } 
    }
	
}
