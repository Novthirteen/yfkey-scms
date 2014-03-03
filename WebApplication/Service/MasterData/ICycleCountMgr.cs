using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface ICycleCountMgr : ICycleCountBaseMgr
    {
        #region Customized Methods

        CycleCount LoadCycleCount(string code, bool includeDetails);

        CycleCount CheckAndLoadCycleCount(string code);        

        void CreateCycleCount(CycleCount cycleCount, User user);

        void CreateCycleCount(StorageBin bin, IList<CycleCountDetail> cycleCountDetailList, User user);

        void CreateCycleCount(Location location, IList<CycleCountDetail> cycleCountDetailList, User user);

        void SubmitCycleCount(string orderNo, User user);

        void SubmitCycleCount(CycleCount cycleCount, User user);

        IList<CycleCountResult> CalcCycleCount(string orderNo, User user);

        IList<CycleCountResult> CalcCycleCount(CycleCount cycCnt, User user);

        IList<CycleCountResult> ReCalcCycleCount(string orderNo, User user);

        void SaveCycleCount(string orderNo, IList<CycleCountDetail> cycCntDetList, User user);

        void SaveCycleCount(CycleCount cycCnt, IList<CycleCountDetail> cycCntDetList, User user);       

        void CheckHuExistThisCount(string huId);

        #endregion Customized Methods

        #region 新的盘点功能
        CycleCount CreateCycleCount(string type, string locationCode, string bins, string items, User user);
        void ReleaseCycleCount(string orderNo, User user);
        void ReleaseCycleCount(CycleCount cycleCount, User user);
        void StartCycleCount(string orderNo, User user);
        void CompleteCycleCount(string orderNo, User user);
        void DeleteCycleCount(string orderNo, User user);
        void ManualCloseCycleCount(string orderNo, User user);
        void CancelCycleCount(string orderNo, User user);
        void ProcessCycleCountResult(IList<int> cycleCountResultIdList, User user);
        void RecordCycleCountDetail(string orderNo, IList<CycleCountDetail> cycleCountDetailList, User user);
        IList<CycleCountResult> CalcCycleCount(string orderNo);
        IList<CycleCountResult> CalcCycleCount(string orderNo, bool listShortage, bool listProfit, bool listEqual, IList<string> binList, IList<string> itemList);
        IList<CycleCountResult> ListCycleCountResult(string orderNo, bool listShortage, bool listProfit, bool listEqual, IList<string> binList, IList<string> itemList);
        IList<CycleCountResult> ListCycleCountResultDetail(string orderNo, bool listShortage, bool listProfit, bool listEqual, IList<string> binList, IList<string> itemList);
        IList<CycleCountResult> ListCycleCountResultDetail(string orderNo, bool listShortage, bool listProfit, bool listEqual, IList<string> binList, IList<string> itemList, bool isOnFloor);
        void ProcessCycleCountResult(string orderNo, User user);
        void ProcessCycleCountResult(CycleCount cycCnt, User user);
        #endregion
    }
}
