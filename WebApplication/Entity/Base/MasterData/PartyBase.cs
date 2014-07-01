using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class PartyBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private string _code;
        public virtual string Code
		{
			get
			{
				return _code;
			}
			set
			{
				_code = value;
			}
		}
		private string _name;
        public virtual string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
        private string _paymentTerm;
        public virtual string PaymentTerm
        {
            get
            {
                return _paymentTerm;
            }
            set
            {
                _paymentTerm = value;
            }
        }
        private string _tradeTerm;
        public virtual string TradeTerm
        {
            get
            {
                return _tradeTerm;
            }
            set
            {
                _tradeTerm = value;
            }
        }
        private string _country;
        public virtual string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
            }
        }
       
		private Party _parentParty;
        public virtual Party ParentParty
		{
			get
			{
				return _parentParty;
			}
			set
			{
				_parentParty = value;
			}
		}
		private Boolean _isActive;
        public virtual Boolean IsActive
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

        #region  20120828 wuri //内部客户
        private Boolean _isIntern;
        public virtual Boolean IsIntern
        {
            get
            {
                return _isIntern;
            }
            set
            {
                _isIntern = value;
            }
        }
        #endregion

        public string Plant { get; set; }


        #endregion

        public override int GetHashCode()
        {
			if (Code != null)
            {
                return Code.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            PartyBase another = obj as PartyBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Code == another.Code);
            }
        } 
    }
	
}
