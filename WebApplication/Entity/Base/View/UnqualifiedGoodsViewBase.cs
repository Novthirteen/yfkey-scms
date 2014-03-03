using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public abstract class UnqualifiedGoodsViewBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private InspectOrder _inspectOrder;
        public InspectOrder InspectOrder
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
		private Decimal _inspectQty;
		public Decimal InspectQty
		{
			get
			{
				return _inspectQty;
			}
			set
			{
				_inspectQty = value;
			}
		}
		private Decimal _qualifiedQty;
		public Decimal QualifiedQty
		{
			get
			{
				return _qualifiedQty;
			}
			set
			{
				_qualifiedQty = value;
			}
		}
		private Decimal _rejectedQty;
		public Decimal RejectedQty
		{
			get
			{
				return _rejectedQty;
			}
			set
			{
				_rejectedQty = value;
			}
		}
		private string _disposition;
		public string Disposition
		{
			get
			{
				return _disposition;
			}
			set
			{
				_disposition = value;
			}
		}
        private string _defectClassification;
        public string DefectClassification
		{
			get
			{
                return _defectClassification;
			}
			set
			{
                _defectClassification = value;
			}
		}
        private string _defectFactor;
        public string DefectFactor
        {
            get
            {
                return _defectFactor;
            }
            set
            {
                _defectFactor = value;
            }
        }
		private Item _item;
        public Item Item
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
        private Location _locationFrom;
        public Location LocationFrom
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
        private Item _finishGoods;
        public Item FinishGoods
        {
            get
            {
                return _finishGoods;
            }
            set
            {
                _finishGoods = value;
            }
        }
		private Int32? _id;
		public Int32? Id
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
            UnqualifiedGoodsViewBase another = obj as UnqualifiedGoodsViewBase;

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
