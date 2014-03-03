using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Utility
{
    public static class AddressHelper
    {
        public static bool IsAddressEqual(Address addr1, Address addr2)
        {
            if (addr1 == null && addr2 == null)
            {
                return true;
            }

            if (addr1 == null && addr2 != null)
            {
                return false;
            }

            if (addr1 != null && addr2 == null)
            {
                return false;
            }

            if (addr1.Code == addr2.Code)
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
