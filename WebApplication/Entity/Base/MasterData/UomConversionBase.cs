using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class UomConversionBase : EntityBase
    {
        #region O/R Mapping Properties
		
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
		private com.Sconit.Entity.MasterData.Uom _alterUom;
		public com.Sconit.Entity.MasterData.Uom AlterUom
		{
			get
			{
				return _alterUom;
			}
			set
			{
				_alterUom = value;
			}
		}
		private com.Sconit.Entity.MasterData.Uom _baseUom;
		public com.Sconit.Entity.MasterData.Uom BaseUom
		{
			get
			{
				return _baseUom;
			}
			set
			{
				_baseUom = value;
			}
		}
		private Decimal _alterQty;
		public Decimal AlterQty
		{
			get
			{
				return _alterQty;
			}
			set
			{
				_alterQty = value;
			}
		}
		private Decimal _baseQty;
		public Decimal BaseQty
		{
			get
			{
				return _baseQty;
			}
			set
			{
				_baseQty = value;
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
            UomConversionBase another = obj as UomConversionBase;

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
