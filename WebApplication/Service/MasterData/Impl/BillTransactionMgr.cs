using com.Sconit.Service.Ext.MasterData;


using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using System;
using com.Sconit.Entity;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class BillTransactionMgr : BillTransactionBaseMgr, IBillTransactionMgr
    {
        public BillTransactionMgr(IBillTransactionDao entityDao)
            : base(entityDao)
        {
        }

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public void RecordBillTransaction(PlannedBill plannedBill, ActingBill actingBill, LocationLotDetail locationLotDetail, User user)
        {
            #region 记BillTransaction
            DateTime dateTimeNow = DateTime.Now;

            BillTransaction billTransaction = new BillTransaction();
            billTransaction.OrderNo = plannedBill.OrderNo;
            billTransaction.ExternalReceiptNo = plannedBill.ExternalReceiptNo;
            billTransaction.ReceiptNo = plannedBill.ReceiptNo;
            billTransaction.Item = plannedBill.Item.Code;
            billTransaction.ItemDescription = plannedBill.Item.Description;
            billTransaction.Uom = plannedBill.Uom.Code;
            billTransaction.BillAddress = plannedBill.BillAddress.Code;
            billTransaction.BillAddressDescription = plannedBill.BillAddress.Address;
            billTransaction.Party = plannedBill.BillAddress.Party.Code;
            billTransaction.PartyName = plannedBill.BillAddress.Party.Name;
            billTransaction.Qty = plannedBill.CurrentActingQty;
            billTransaction.EffectiveDate = DateTime.Parse(dateTimeNow.ToShortDateString());   //仅保留年月日;
            billTransaction.TransactionType = plannedBill.TransactionType;
            billTransaction.PlannedBill = plannedBill.Id;
            billTransaction.CreateUser = user.Code;
            billTransaction.CreateDate = dateTimeNow;
            billTransaction.ActingBill = actingBill.Id;
            billTransaction.LocationFrom = plannedBill.LocationFrom;
            billTransaction.PartyFrom = plannedBill.PartyFrom;
            billTransaction.IpNo = plannedBill.IpNo;
            billTransaction.ReferenceItemCode = plannedBill.ReferenceItemCode;
            if (locationLotDetail != null)
            {
                billTransaction.Location = locationLotDetail.Location.Code;
                billTransaction.LocationName = locationLotDetail.Location.Name;
                billTransaction.HuId = locationLotDetail.Hu != null ? locationLotDetail.Hu.HuId : string.Empty;
                billTransaction.LotNo = locationLotDetail.LotNo;
                billTransaction.BatchNo = locationLotDetail.Id;
            }

            this.CreateBillTransaction(billTransaction);
            #endregion
        }

        public void RecordBillTransaction(BillDetail billDetail, User user, bool isCancel)
        {
            DateTime dateTimeNow = DateTime.Now;

            BillTransaction billTransaction = new BillTransaction();
            billTransaction.OrderNo = billDetail.ActingBill.OrderNo;
            billTransaction.ExternalReceiptNo = billDetail.ActingBill.ExternalReceiptNo;
            billTransaction.ReceiptNo = billDetail.ActingBill.ReceiptNo;
            billTransaction.Item = billDetail.ActingBill.Item.Code;
            billTransaction.ItemDescription = billDetail.ActingBill.Item.Description;
            billTransaction.Uom = billDetail.ActingBill.Uom.Code;
            billTransaction.BillAddress = billDetail.ActingBill.BillAddress.Code;
            billTransaction.BillAddressDescription = billDetail.ActingBill.BillAddress.Address;
            billTransaction.Party = billDetail.ActingBill.BillAddress.Party.Code;
            billTransaction.PartyName = billDetail.ActingBill.BillAddress.Party.Name;
            if (!isCancel) {
                billTransaction.Qty = billDetail.BilledQty;
            }
            else
            {
                billTransaction.Qty = billDetail.BilledQty * -1;
            }
            billTransaction.EffectiveDate = DateTime.Parse(dateTimeNow.ToShortDateString());   //仅保留年月日;
            if (billDetail.ActingBill.TransactionType == BusinessConstants.BILL_TRANS_TYPE_PO)
            {
                billTransaction.TransactionType = BusinessConstants.BILL_TRANS_TYPE_PO_BILL;
            }
            else
            {
                billTransaction.TransactionType = BusinessConstants.BILL_TRANS_TYPE_SO_BILL;
            }
            //billTransaction.PlannedBill = billDetail.ActingBill.Id;
            billTransaction.BillDetail = billDetail.Id;
            billTransaction.CreateUser = user.Code;
            billTransaction.CreateDate = dateTimeNow;
            billTransaction.ActingBill = billDetail.ActingBill.Id;
            billTransaction.LocationFrom = billDetail.ActingBill.LocationFrom;
            billTransaction.IpNo = billDetail.ActingBill.IpNo;
            billTransaction.ReferenceItemCode = billDetail.ActingBill.ReferenceItemCode;
            //billTransaction.Location = billDetail.ActingBill.LocationFrom;
            //billTransaction.LocationName = locationLotDetail.Location.Name;
            //billTransaction.HuId = locationLotDetail.Hu != null ? locationLotDetail.Hu.HuId : string.Empty;
            //billTransaction.LotNo = locationLotDetail.LotNo;
            //billTransaction.BatchNo = locationLotDetail.Id;

            this.CreateBillTransaction(billTransaction);
        }
        #endregion Customized Methods
    }
}

