using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class LocationDetSnapShotBase : EntityBase
    {
        //Item, Location, Plant, Qty, InTransitQty, InspectQty, Effdate
        public string Item { get; set; }
        public string Location { get; set; }
        public string Plant { get; set; }
        public decimal Qty { get; set; }
        public decimal InTransitQty { get; set; }
        public decimal InspectQty { get; set; }
        public DateTime Effdate { get; set; }



        public override int GetHashCode()
        {
            if (!string.IsNullOrEmpty(Item) && !string.IsNullOrEmpty(Location))
            {
                return Item.GetHashCode()
                    ^ Location.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            LocationDetSnapShotBase another = obj as LocationDetSnapShotBase;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.Item == another.Item)
                    && (this.Location == another.Location);
            }
        }
    }
	
}
