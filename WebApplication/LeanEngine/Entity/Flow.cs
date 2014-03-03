using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeanEngine.Utility;

namespace LeanEngine.Entity
{
    public class Flow : EntityBase
    {
        #region Input data
        public string Code { get; set; }

        public string PartyFrom { get; set; }
        public string PartyTo { get; set; }

        public FlowStrategy FlowStrategy { get; set; }

        public Enumerators.FlowType FlowType { get; set; }
        public Enumerators.TimeUnit TimeUnit { get; set; }

        public DateTime? OrderTime { get; set; }
        public DateTime? WindowTime { get; set; }
        #endregion

        #region Output data
        public DateTime? NextOrderTime { get; set; }
        public DateTime? NextWindowTime { get; set; }
        public bool IsUpdateWindowTime { get; set; }
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
            Flow another = obj as Flow;

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
