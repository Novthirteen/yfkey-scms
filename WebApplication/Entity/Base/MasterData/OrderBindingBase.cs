using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class OrderBindingBase : EntityBase
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
		private com.Sconit.Entity.MasterData.OrderHead _orderHead;
		public com.Sconit.Entity.MasterData.OrderHead OrderHead
		{
			get
			{
				return _orderHead;
			}
			set
			{
				_orderHead = value;
			}
		}
		private com.Sconit.Entity.MasterData.Flow _bindedFlow;
		public com.Sconit.Entity.MasterData.Flow BindedFlow
		{
			get
			{
				return _bindedFlow;
			}
			set
			{
				_bindedFlow = value;
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
        private string _bindedOrderNo;
        public string BindedOrderNo
		{
			get
			{
                return _bindedOrderNo;
			}
			set
			{
                _bindedOrderNo = value;
			}
		}
		private string _remark;
		public string Remark
		{
			get
			{
				return _remark;
			}
			set
			{
				_remark = value;
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
            OrderBindingBase another = obj as OrderBindingBase;

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
