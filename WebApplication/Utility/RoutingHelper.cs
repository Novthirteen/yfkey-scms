using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Utility
{
    public static class RoutingHelper
    {
        public static bool IsRoutingEqual(Routing routing1, Routing routing2)
        {
            if (routing1 == null && routing2 == null)
            {
                return true;
            }

            if (routing1 == null && routing2 != null)
            {
                return false;
            }

            if (routing1 != null && routing2 == null)
            {
                return false;
            }

            if (routing1.Code == routing2.Code)
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
