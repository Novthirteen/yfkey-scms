using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class WorkCenterBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private string _code;
		public string Code
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
		public string Name
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
		private Boolean _isActive;
		public Boolean IsActive
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
		private com.Sconit.Entity.MasterData.Party _party;
		public com.Sconit.Entity.MasterData.Party Party
		{
			get
			{
				return _party;
			}
			set
			{
				_party = value;
			}
		}
		private string _type;
		public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}
		private Decimal? _laborBurdenPercent;
		public Decimal? LaborBurdenPercent
		{
			get
			{
				return _laborBurdenPercent;
			}
			set
			{
				_laborBurdenPercent = value;
			}
		}
		private Decimal? _laborBurdenRate;
		public Decimal? LaborBurdenRate
		{
			get
			{
				return _laborBurdenRate;
			}
			set
			{
				_laborBurdenRate = value;
			}
		}
		private Decimal? _setupBurdenPercent;
		public Decimal? SetupBurdenPercent
		{
			get
			{
				return _setupBurdenPercent;
			}
			set
			{
				_setupBurdenPercent = value;
			}
		}
		private Decimal? _setupBurdenRate;
		public Decimal? SetupBurdenRate
		{
			get
			{
				return _setupBurdenRate;
			}
			set
			{
				_setupBurdenRate = value;
			}
		}
		private Decimal? _laborRate;
		public Decimal? LaborRate
		{
			get
			{
				return _laborRate;
			}
			set
			{
				_laborRate = value;
			}
		}
		private string _machine;
		public string Machine
		{
			get
			{
				return _machine;
			}
			set
			{
				_machine = value;
			}
		}
		private Decimal? _machineQty;
		public Decimal? MachineQty
		{
			get
			{
				return _machineQty;
			}
			set
			{
				_machineQty = value;
			}
		}
		private Decimal? _machineBurdenRate;
		public Decimal? MachineBurdenRate
		{
			get
			{
				return _machineBurdenRate;
			}
			set
			{
				_machineBurdenRate = value;
			}
		}
		private Decimal? _machineSetupBurdenRate;
		public Decimal? MachineSetupBurdenRate
		{
			get
			{
				return _machineSetupBurdenRate;
			}
			set
			{
				_machineSetupBurdenRate = value;
			}
		}
		private Decimal? _runCrew;
		public Decimal? RunCrew
		{
			get
			{
				return _runCrew;
			}
			set
			{
				_runCrew = value;
			}
		}
		private Decimal? _setupCrew;
		public Decimal? SetupCrew
		{
			get
			{
				return _setupCrew;
			}
			set
			{
				_setupCrew = value;
			}
		}
		private Decimal? _setupRate;
		public Decimal? SetupRate
		{
			get
			{
				return _setupRate;
			}
			set
			{
				_setupRate = value;
			}
		}
		private Decimal? _queueTime;
		public Decimal? QueueTime
		{
			get
			{
				return _queueTime;
			}
			set
			{
				_queueTime = value;
			}
		}
		private Decimal? _waitTime;
		public Decimal? WaitTime
		{
			get
			{
				return _waitTime;
			}
			set
			{
				_waitTime = value;
			}
		}
		private Decimal? _percentEfficiency;
		public Decimal? PercentEfficiency
		{
			get
			{
				return _percentEfficiency;
			}
			set
			{
				_percentEfficiency = value;
			}
		}
		private Decimal? _percentUtilization;
		public Decimal? PercentUtilization
		{
			get
			{
				return _percentUtilization;
			}
			set
			{
				_percentUtilization = value;
			}
		}
        
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
            WorkCenterBase another = obj as WorkCenterBase;

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
