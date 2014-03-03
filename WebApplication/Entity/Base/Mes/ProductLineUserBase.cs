using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Mes
{
    [Serializable]
    public abstract class ProductLineUserBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Flow _productLine;
		public com.Sconit.Entity.MasterData.Flow ProductLine
		{
			get
			{
				return _productLine;
			}
			set
			{
				_productLine = value;
			}
		}
		private com.Sconit.Entity.MasterData.User _deliveryUser;
		public com.Sconit.Entity.MasterData.User DeliveryUser
		{
			get
			{
				return _deliveryUser;
			}
			set
			{
				_deliveryUser = value;
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
            ProductLineUserBase another = obj as ProductLineUserBase;

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
