using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IBillMgr : IBillBaseMgr
    {
        #region Customized Methods

        Bill CheckAndLoadBill(string billNo);

        Bill CheckAndLoadBill(string billNo, bool includeBillDetail);

        Bill LoadBill(string billNo, bool includeBillDetail);

        IList<Bill> CreateBill(IList<ActingBill> actingBillList, User user);

        IList<Bill> CreateBill(IList<ActingBill> actingBillList, string userCode);

        IList<Bill> CreateBill(IList<ActingBill> actingBillList, User user, string status);

        IList<Bill> CreateBill(IList<ActingBill> actingBillList, string userCode, string status);

        IList<Bill> CreateBill(IList<ActingBill> actingBillList, User user, string status, decimal discount);

        IList<Bill> CreateBill(IList<ActingBill> actingBillList, string userCode, string status, decimal discount);

        void AddBillDetail(string billNo, IList<ActingBill> actingBillList, string userCode);

        void AddBillDetail(string billNo, IList<ActingBill> actingBillList, User user);

        void AddBillDetail(Bill bill, IList<ActingBill> actingBillList, string userCode);

        void AddBillDetail(Bill bill, IList<ActingBill> actingBillList, User user);

        void DeleteBillDetail(IList<BillDetail> billDetailList, string userCode);

        void DeleteBillDetail(IList<BillDetail> billDetailList, User user);

        void DeleteBill(string billNo, string userCode);

        void DeleteBill(string billNo, User user);

        void DeleteBill(Bill bill, string userCode);

        void DeleteBill(Bill bill, User user);

        void UpdateBill(Bill bill, string userCode);

        void UpdateBill(Bill bill, User user);

        void ReleaseBill(string billNo, string userCode);

        void ReleaseBill(string billNo, User user);

        void ReleaseBill(Bill bill, string userCode);

        void ReleaseBill(Bill bill, User user);

        void CancelBill(string billNo, string userCode);

        void CancelBill(string billNo, User user);

        void CancelBill(Bill bill, string userCode);

        void CancelBill(Bill bill, User user);

        void CloseBill(string billNo, string userCode);

        void CloseBill(string billNo, User user);

        void CloseBill(Bill bill, string userCode);

        void CloseBill(Bill bill, User user);

        Bill VoidBill(string billNo, string userCode);

        Bill VoidBill(string billNo, User user);

        Bill VoidBill(Bill bill, string userCode);

        Bill VoidBill(Bill bill, User user);

        IList<ActingBill> ManualCreateActingBill(IList<PlannedBill> plannedBillList, User user);

        ActingBill ManualCreateActingBill(PlannedBill plannedBill, User user);

        ActingBill ManualCreateActingBill(PlannedBill plannedBill, LocationLotDetail locationLotDetail, User user);

        ActingBill CreateActingBill(PlannedBill plannedBill, User user);

        ActingBill CreateActingBill(PlannedBill plannedBill, LocationLotDetail locationLotDetail, User user);

        void TryCreateBill(string customers);

        void RecalculatePrice(string billNo, User user);
        #endregion Customized Methods
    }
}
