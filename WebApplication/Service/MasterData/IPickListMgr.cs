using System;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IPickListMgr : IPickListBaseMgr
    {
        #region Customized Methods

        PickList LoadPickList(string pickListNo, bool includePickListDetail);

        PickList LoadPickList(string pickListNo, bool includePickListDetail, bool includePickListResult);

        PickList CheckAndLoadPickList(string pickListNo);

        PickList CreatePickList(List<string> orderNoList, User user);

        PickList CreatePickList(List<Transformer> transformerList, User user);

        PickList CreatePickList(IList<OrderLocationTransaction> orderLocationTransactionList, User user);

        void DoPick(PickList pickList, User user);

        void DoPick(PickList pickList, string userCode);

        void ManualClosePickList(PickList pickList, User user);

        void ManualClosePickList(string pickListNo, User user);

        void CancelPickList(PickList pickList, User user);

        void CancelPickList(string pickListNo, User user);

        void DeletePickList(PickList pickList, User user);

        void DeletePickList(string pickListNo, User user);

        void StartPickList(PickList pickList, User user);

        void StartPickList(string pickListNo, User user);       

        #endregion Customized Methods
    }
}
