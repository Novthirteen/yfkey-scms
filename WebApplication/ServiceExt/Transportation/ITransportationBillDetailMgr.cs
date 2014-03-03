using System;
using com.Sconit.Entity.Transportation;

namespace com.Sconit.Service.Transportation
{
    public interface ITransportationBillDetailMgr : ITransportationBillDetailBaseMgr
    {
        #region Customized Methods

        TransportationBillDetail TransferTransportationActBill2TransportationBillDetail(TransportationActBill transportationActBill);

        #endregion Customized Methods
    }
}
