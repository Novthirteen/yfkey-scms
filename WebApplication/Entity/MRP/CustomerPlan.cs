using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    public partial class CustomerPlan
    {

        public string ItemFullDescription
        {
            get
            {
                return ((ItemDescription != null ? ItemDescription : string.Empty) + 
                    ((ItemReference != null && ItemReference != string.Empty) ? "[" + ItemReference + "]" : string.Empty));
            }

        }

    }
}