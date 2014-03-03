using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class OrderPlannedBackflushBase : EntityBase
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
		private string _inProcessLocation;
		public string InProcessLocation
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
        private Decimal _plannedQty;
		public Decimal PlannedQty
		{
			get
			{
                return _plannedQty;
			}
			set
			{
                _plannedQty = value;
			}
		}
        public Boolean IsActive { get; set; }
        public string Flow { get; set; }
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
            OrderPlannedBackflushBase another = obj as OrderPlannedBackflushBase;

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
