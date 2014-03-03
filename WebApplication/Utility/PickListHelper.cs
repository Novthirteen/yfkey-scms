using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;

namespace com.Sconit.Utility
{
    public static class PickListHelper
    {
        public static bool CheckAuthrize(PickList pickList, User user)
        {
            bool partyFromAuthrized = false;
            //bool partyToAuthrized = false;

            foreach (Permission permission in user.Permissions)
            {
                if (permission.Code == pickList.PartyFrom.Code)
                {
                    partyFromAuthrized = true;
                    break;
                }

                //if (permission.Code == pickList.PartyTo.Code)
                //{
                //    partyToAuthrized = true;
                //}

                //if (partyFromAuthrized && partyToAuthrized)
                //{
                //    break;
                //}
            }

            //if (!(partyFromAuthrized && partyToAuthrized))
            if (!partyFromAuthrized)
            {
                //没有该订单的操作权限
                throw new BusinessErrorException("Order.Error.PickUp.NoPermission", pickList.PickListNo);
            }

            return true;
        }
    }
}
