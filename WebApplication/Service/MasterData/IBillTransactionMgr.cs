using com.Sconit.Service.Ext.MasterData;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IBillTransactionMgr : IBillTransactionBaseMgr
    {
        #region Customized Methods

        void RecordBillTransaction(PlannedBill plannedBill, ActingBill actingBill, LocationLotDetail locationLotDetail, User user);

        void RecordBillTransaction(BillDetail billDetail, User user, bool isCancel);

        #endregion Customized Methods
    }
}

#region Extend Interface

namespace com.Sconit.Service.Ext.MasterData
{
    public partial interface IBillTransactionMgrE : com.Sconit.Service.MasterData.IBillTransactionMgr
    {
        
    }
}

#endregion
