using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeanEngine.Entity
{
    public class SupplyChain : EntityBase
    {
        public int ID { get; set; }

        /// <summary>
        /// The same Supply Chain has the same GroupID
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// Next operation ID
        /// </summary>
        public int NextOpID { get; set; }


        public string Loc { get; set; }
        public string Item { get; set; }

        public ItemFlow ItemFlow { get; set; }


        public decimal MaxInv { get; set; }
        public decimal SafeInv { get; set; }

        public double LeadTime { get; set; }

        public int BomLevel { get; set; }
        public decimal QtyPer { get; set; }

        public bool IsTrace { get; set; }


        public override int GetHashCode()
        {
            if (ID != 0)
            {
                return ID.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            SupplyChain another = obj as SupplyChain;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.ID == another.ID);
            }
        }
    }
}
