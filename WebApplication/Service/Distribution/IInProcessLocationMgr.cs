using System;
using System.Collections.Generic;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Distribution
{
    public interface IInProcessLocationMgr : IInProcessLocationBaseMgr
    {
        #region Customized Methods

        InProcessLocation GenerateInProcessLocation(OrderHead orderHead);

        void CreateInProcessLocation(InProcessLocation inProcessLocation, User user);

        void CreateInProcessLocation(InProcessLocation inProcessLocation, User user, string type);

        void CloseInProcessLocation(InProcessLocation inProcessLocation, User user);

        void CloseInProcessLocation(InProcessLocation inProcessLocation, User user, bool handleGap);

        void TryCloseInProcessLocation(InProcessLocation inProcessLocation, User user);

        InProcessLocation LoadInProcessLocation(string ipNo, string userCode);

        InProcessLocation LoadInProcessLocation(string ipNo, User user);

        InProcessLocation LoadInProcessLocation(string ipNo, string userCode, bool includeDetail);

        InProcessLocation LoadInProcessLocation(string ipNo, User user, bool includeDetail);

        InProcessLocation LoadInProcessLocation(String ipNo, bool includeDetail);

        void UpdateInProcessLocation(InProcessLocation ip, int op, User currentUser);

        void UpdateInProcessLocation(InProcessLocation ip, int op, string userCode);

        void UpdateInProcessLocation(string ipNo, int op, User currentUser);

        void UpdateInProcessLocation(string ipNo, int op, string userCode);

        void ResolveInPorcessLocationGap(InProcessLocation inProcessLocation, string grGapTo, User user);

        InProcessLocation CheckAndLoadInProcessLocation(string ipNo);

        IList<InProcessLocation> GetInProcessLocation(string userCode, int firstRow, int maxRows, params string[] orderTypes);

        void ResolveInPorcessLocationNormal(InProcessLocation inProcessLocation, string grGapTo, User user);

        IList<InProcessLocation> GetAvaliableInProcessLocation(string route);

        #endregion Customized Methods
    }
}
