using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class InspectOrderDetailBase : EntityBase
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
		private com.Sconit.Entity.MasterData.LocationLotDetail _locationLotDetail;
		public com.Sconit.Entity.MasterData.LocationLotDetail LocationLotDetail
		{
			get
			{
				return _locationLotDetail;
			}
			set
			{
				_locationLotDetail = value;
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
		private Decimal? _qualifiedQty;
		public Decimal? QualifiedQty
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
		private Decimal? _rejectedQty;
		public Decimal? RejectedQty
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

        private Decimal _pendingQualifiedQty;
        public Decimal PendingQualifiedQty
        {
            get
            {
                return _pendingQualifiedQty;
            }
            set
            {
                _pendingQualifiedQty = value;
            }
        }
        private Decimal _pendingRejectedQty;
        public Decimal PendingRejectedQty
        {
            get
            {
                return _pendingRejectedQty;
            }
            set
            {
                _pendingRejectedQty = value;
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
            InspectOrderDetailBase another = obj as InspectOrderDetailBase;

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
