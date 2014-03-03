using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class RoutingDetailBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Routing _routing;
		public com.Sconit.Entity.MasterData.Routing Routing
		{
			get
			{
				return _routing;
			}
			set
			{
				_routing = value;
			}
		}
		private Int32 _operation;
		public Int32 Operation
		{
			get
			{
				return _operation;
			}
			set
			{
				_operation = value;
			}
		}
		private string _reference;
		public string Reference
		{
			get
			{
				return _reference;
			}
			set
			{
				_reference = value;
			}
		}
		private string _activity;
        public string Activity
		{
			get
			{
                return _activity;
			}
			set
			{
                _activity = value;
			}
		}
		private DateTime _startDate;
		public DateTime StartDate
		{
			get
			{
				return _startDate;
			}
			set
			{
				_startDate = value;
			}
		}
		private DateTime? _endDate;
		public DateTime? EndDate
		{
			get
			{
				return _endDate;
			}
			set
			{
				_endDate = value;
			}
		}
		private com.Sconit.Entity.MasterData.WorkCenter _workCenter;
		public com.Sconit.Entity.MasterData.WorkCenter WorkCenter
		{
			get
			{
				return _workCenter;
			}
			set
			{
				_workCenter = value;
			}
		}
		private Decimal _setupTime;
		public Decimal SetupTime
		{
			get
			{
				return _setupTime;
			}
			set
			{
				_setupTime = value;
			}
		}
		private Decimal _runTime;
		public Decimal RunTime
		{
			get
			{
				return _runTime;
			}
			set
			{
				_runTime = value;
			}
		}
		private Decimal _moveTime;
		public Decimal MoveTime
		{
			get
			{
				return _moveTime;
			}
			set
			{
				_moveTime = value;
			}
		}
        private Decimal _tactTime;
        public Decimal TactTime
		{
			get
			{
				return _tactTime;
			}
			set
			{
				_tactTime = value;
			}
		}
		private com.Sconit.Entity.MasterData.Location _location;
		public com.Sconit.Entity.MasterData.Location Location
		{
			get
			{
				return _location;
			}
			set
			{
				_location = value;
			}
		}
        private string _tagNo;
        public string TagNo
        {
            get
            {
                return _tagNo;
            }
            set
            {
                _tagNo = value;
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
            RoutingDetailBase another = obj as RoutingDetailBase;

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
