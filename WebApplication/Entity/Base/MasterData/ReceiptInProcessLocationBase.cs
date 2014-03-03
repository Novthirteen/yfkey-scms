using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class ReceiptInProcessLocationBase : EntityBase
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
		private com.Sconit.Entity.Distribution.InProcessLocation _inProcessLocation;
		public com.Sconit.Entity.Distribution.InProcessLocation InProcessLocation
		{
			get
			{
				return _inProcessLocation;
			}
			set
			{
				_inProcessLocation = value;
			}
		}
		private com.Sconit.Entity.MasterData.Receipt _receipt;
		public com.Sconit.Entity.MasterData.Receipt Receipt
		{
			get
			{
				return _receipt;
			}
			set
			{
				_receipt = value;
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
            ReceiptInProcessLocationBase another = obj as ReceiptInProcessLocationBase;

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
