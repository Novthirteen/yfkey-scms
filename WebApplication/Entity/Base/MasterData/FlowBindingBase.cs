using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class FlowBindingBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Flow _masterFlow;
		public com.Sconit.Entity.MasterData.Flow MasterFlow
		{
			get
			{
				return _masterFlow;
			}
			set
			{
				_masterFlow = value;
			}
		}
		private com.Sconit.Entity.MasterData.Flow _slaveFlow;
		public com.Sconit.Entity.MasterData.Flow SlaveFlow
		{
			get
			{
				return _slaveFlow;
			}
			set
			{
				_slaveFlow = value;
			}
		}
        private string _bindingType;
        public string BindingType
		{
			get
			{
                return _bindingType;
			}
			set
			{
                _bindingType = value;
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
            FlowBindingBase another = obj as FlowBindingBase;

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
