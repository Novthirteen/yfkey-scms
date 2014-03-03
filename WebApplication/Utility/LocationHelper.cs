using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Utility
{
    public static class LocationHelper
    {
        public static bool IsLocationEqual(Location location1, Location location2)
        {
            if (location1 == null && location2 == null)
            {
                return true;
            }

            if (location1 == null && location2 != null)
            {
                return false;
            }

            if (location1 != null && location2 == null)
            {
                return false;
            }

            if (location1.Code == location2.Code)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
