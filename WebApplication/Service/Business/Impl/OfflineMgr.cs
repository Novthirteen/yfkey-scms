using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Service.Report;

namespace com.Sconit.Service.Business.Impl
{
    public class OfflineMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        private IEmployeeMgr employeeMgr;
        private IOrderMgr orderMgr;
        private IOrderHeadMgr orderHeadMgr;
        private IReportMgr reportMgr;
        private IHuMgr huMgr;
        private ILanguageMgr languageMgr;

        public OfflineMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            IEmployeeMgr employeeMgr,
            IOrderMgr orderMgr,
            IOrderHeadMgr orderHeadMgr,
            IReportMgr reportMgr,
            IHuMgr huMgr,
            ILanguageMgr languageMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.employeeMgr = employeeMgr;
            this.orderMgr = orderMgr;
            this.orderHeadMgr = orderHeadMgr;
            this.reportMgr = reportMgr;
            this.huMgr = huMgr;
            this.languageMgr = languageMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER)
            {
                setBaseMgr.FillResolverByOrder(resolver);

                #region 校验
                if (resolver.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
                    throw new BusinessErrorException("Common.Business.Error.StatusError", resolver.Code, resolver.Status);

                if (resolver.OrderType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                    throw new BusinessErrorException("Order.Error.OrderOfflineIsNotProduction", resolver.Code, resolver.OrderType);
                #endregion
            }
            else
            {
                throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
            }
        }

        protected override void GetDetail(Resolver resolver)
        {
            FillOffline(resolver);
        }

        protected override void SetDetail(Resolver resolver)
        {
        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
            this.ReceiveWorkOrder(resolver);
        }

        protected override void ExecuteCancel(Resolver resolver)
        {

        }

        /// <summary>
        /// 生产单下线
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public void ReceiveWorkOrder(Resolver resolver)
        {
            IList<ReceiptDetail> receiptDetails = orderMgr.ConvertTransformerToReceiptDetail(resolver.Transformers);
            //Receipt receipt = new Receipt();
            //foreach (Transformer transformer in resolver.Transformers)
            //{                
            //    ReceiptDetail receiptDetail = new ReceiptDetail();
            //    receiptDetail.OrderLocationTransaction = orderLocationTransactionMgr.LoadOrderLocationTransaction(transformer.OrderLocTransId);
            //    receiptDetail.HuId = null;
            //    receiptDetail.ReceivedQty = transformer.CurrentQty;
            //    receiptDetail.RejectedQty = transformer.CurrentRejectQty;
            //    receiptDetail.ScrapQty = transformer.ScrapQty;
            //    receiptDetail.Receipt = receipt;
            //    receipt.AddReceiptDetail(receiptDetail);
            //}
            List<WorkingHours> workingHoursList = new List<WorkingHours>();
            foreach (string[] stringArray in resolver.WorkingHours)
            {
                WorkingHours workingHours = new WorkingHours();
                workingHours.Employee = employeeMgr.LoadEmployee(stringArray[0]);
                workingHours.Hours = Convert.ToDecimal(stringArray[1]);
                workingHoursList.Add(workingHours);
            }

            Receipt receiptResult = orderMgr.ReceiveOrder(receiptDetails, resolver.UserCode, null, null, workingHoursList, true, resolver.IsOddCreateHu);
            //(receiptDetailList, this.CurrentUser, null, null, null, true, isOddCreateHu);

            //OrderHead orderHead = orderHeadMgr.LoadOrderHead(resolver.Code);
            IList<Hu> huList = new List<Hu>();
            IList<ReceiptDetail> receiptDetailList = receiptResult.ReceiptDetails;
            if (resolver.AutoPrintHu)
            {
                if (receiptDetailList == null || receiptDetailList.Count == 0)
                {
                    throw new BusinessErrorException("Inventory.Error.PrintHu.ReceiptDetail.Required");
                }
                foreach (ReceiptDetail receiptDetail in receiptDetailList)
                {
                    if (receiptDetail.HuId != null)
                    {
                        Hu hu = huMgr.LoadHu(receiptDetail.HuId);
                        if (hu != null)
                        {
                            huList.Add(hu);
                        }
                    }
                }
                if (huList.Count > 0)
                {
                    IList<object> huDetailObj = new List<object>();
                    huDetailObj.Add(huList);
                    huDetailObj.Add(resolver.UserCode);
                    resolver.PrintUrl = reportMgr.WriteToFile("BarCode.xls", huDetailObj, "BarCode.xls");
                }
            }
            resolver.Transformers = null;
            string huString = string.Empty;
            foreach (Hu hu in huList)
            {
                huString += " " + hu.HuId;
            }
            resolver.Result = languageMgr.TranslateMessage("MasterData.WorkOrder.OrderHead.Receive.Successfully", resolver.UserCode, resolver.Code);
            if (huList.Count > 0)
            {
                resolver.Result += languageMgr.TranslateMessage("Inventory.CreateHu.Successful", resolver.UserCode, huString);
            }
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void FillOffline(Resolver resolver)
        {
            OrderHead orderHead = orderHeadMgr.LoadOrderHead(resolver.Code, true);
            if (orderHead == null || orderHead.OrderDetails == null || orderHead.OrderDetails.Count == 0)
            {
                throw new BusinessErrorException("Common.Business.Error.EntityNotExist", resolver.Code);
            }
            List<Transformer> newTransformerList = new List<Transformer>();
            foreach (OrderDetail orderDetail in orderHead.OrderDetails)
            {
                IList<OrderLocationTransaction> orderLocationTransactions
                    = orderLocationTransactionMgr.GetOrderLocationTransaction(orderDetail.Id, BusinessConstants.IO_TYPE_IN);
                if (orderLocationTransactions == null && orderLocationTransactions.Count != 1)
                {
                    throw new BusinessErrorException("Common.Business.Error.EntityNotExist", resolver.Code);
                }
                Transformer transformer = Utility.TransformerHelper.ConvertOrderLocationTransactionToTransformer(orderLocationTransactions[0]);
                //收货批量
                transformer.Qty = orderDetail.GoodsReceiptLotSize.HasValue ? orderDetail.GoodsReceiptLotSize.Value : 0;//收货批量
                newTransformerList.Add(transformer);
            }
            resolver.Transformers = newTransformerList;
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void ExecutePrint(Resolver resolver)
        {
            IList<Hu> huList = new List<Hu>();
            Hu hu = huMgr.CheckAndLoadHu(resolver.Code);
            huList.Add(hu);

            IList<object> huDetailObj = new List<object>();
            huDetailObj.Add(huList);
            resolver.PrintUrl = reportMgr.WriteToFile("BarCode.xls", huDetailObj, "BarCode.xls");
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void GetReceiptNotes(Resolver resolver)
        {
            string[] orderTypes = new string[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION };
            string[] row = resolver.Code.Split('|');
            int firstRow = int.Parse(row[0]);
            int maxRows = int.Parse(row[1]);
            IList<Hu> huList = huMgr.GetHuList(resolver.UserCode, firstRow, maxRows, orderTypes);
            resolver.ReceiptNotes = this.ConvertHusToReceiptNotes(huList);
        }

        #region Private Methods
        private List<ReceiptNote> ConvertHusToReceiptNotes(IList<Hu> HuList)
        {
            if (HuList == null)
            {
                return null;
            }
            List<ReceiptNote> receiptNotes = new List<ReceiptNote>();
            int seq = 1;
            foreach (Hu hu in HuList)
            {
                ReceiptNote receiptNote = new ReceiptNote();
                receiptNote.CreateDate = hu.CreateDate;
                receiptNote.CreateUser = hu.CreateUser == null ? string.Empty : hu.CreateUser.Name;
                receiptNote.OrderNo = hu.OrderNo;
                receiptNote.HuId = hu.HuId;
                receiptNote.ItemDescription = hu.Item.Description;
                receiptNote.UnitCount = hu.UnitCount;
                receiptNote.Qty = hu.Qty;
                receiptNote.Sequence = seq;
                receiptNote.ReceiptNo = hu.ReceiptNo;
                receiptNotes.Add(receiptNote);
                seq++;
            }
            return receiptNotes;
        }

        #endregion
    }
}
