using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeanEngine.Utility;

namespace LeanEngine.Entity
{
    public class FlowStrategy : EntityBase
    {
        public string Code { get; set; }

        public Enumerators.Strategy Strategy { get; set; }

        public double LeadTime { get; set; }
        public double EmLeadTime { get; set; }
        public int WeekInterval { get; set; }

        public string[] MonWinTime { get; set; }
        public string[] TueWinTime { get; set; }
        public string[] WedWinTime { get; set; }
        public string[] ThuWinTime { get; set; }
        public string[] FriWinTime { get; set; }
        public string[] SatWinTime { get; set; }
        public string[] SunWinTime { get; set; }


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
            FlowStrategy another = obj as FlowStrategy;

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
