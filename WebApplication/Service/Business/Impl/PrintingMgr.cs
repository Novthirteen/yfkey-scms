using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.Report;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Entity.Distribution;

namespace com.Sconit.Service.Business.Impl
{
    public class PrintingMgr : AbstractBusinessMgr
    {
        public ICriteriaMgr criteriaMgr;
        public IReportMgr reportMgr;
        public IPickListMgr pickListMgr;
        public IOrderHeadMgr orderHeadMgr;
        public IInspectOrderMgr inspectOrderMgr;
        public IInProcessLocationMgr inProcessLocationMgr;
        public IReceiptMgr receiptMgr;
        public IReceiptDetailMgr receiptDetailMgr;

        public PrintingMgr(ICriteriaMgr criteriaMgr,
            IReportMgr reportMgr,
            IPickListMgr pickListMgr,
             IOrderHeadMgr orderHeadMgr,
             IInspectOrderMgr inspectOrderMgr,
            IInProcessLocationMgr inProcessLocationMgr,
           IReceiptMgr receiptMgr,
            IReceiptDetailMgr receiptDetailMgr)
        {
            this.criteriaMgr = criteriaMgr;
            this.reportMgr = reportMgr;
            this.pickListMgr = pickListMgr;
            this.orderHeadMgr = orderHeadMgr;
            this.inspectOrderMgr = inspectOrderMgr;
            this.inProcessLocationMgr = inProcessLocationMgr;
            this.receiptMgr = receiptMgr;
            this.receiptDetailMgr = receiptDetailMgr;

        }

        protected override void GetReceiptNotes(Resolver resolver)
        {
            try
            {
                if (resolver.Input != null && resolver.Input.Trim() != string.Empty)
                {
                    //清空，不然老是重打
                    resolver.ReceiptNotes = null;

                    string[] printPara = resolver.Input.Split('&');
                    string[] printTargets = printPara[0].Split('|');
                    string[] printRegion = printPara[1].Split(',');

                    foreach (string printTarget in printTargets)
                    {
                        if (printTarget == "Receipt")
                        {
                            PrintReceipt(resolver, printRegion);
                        }
                        if (printTarget == "Inspect")
                        {
                            PrintInspect(resolver, printRegion);
                        }
                        else if (printTarget == "Picklist")
                        {
                            PrintPickList(resolver, printRegion);
                        }
                        else if (printTarget == "ASN")
                        {
                            PrintASN(resolver, printRegion);
                        }
                        else if (printTarget == "Order")
                        {
                            PrintOrder(resolver, printRegion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void PrintReceipt(Resolver resolver, string[] region)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Receipt));

            criteria.Add(Expression.Eq("IsPrinted", false));
            criteria.Add(Expression.Eq("NeedPrint", true));
            criteria.Add(Expression.In("PartyTo.Code", region));

            IList<Receipt> receiptList = criteriaMgr.FindAll<Receipt>(criteria);

            List<ReceiptNote> receiptNotes = new List<ReceiptNote>();
            if (receiptList != null && receiptList.Count > 0)
            {
                foreach (Receipt receipt in receiptList)
                {
                    IList<object> list = new List<object>();
                    list.Add(receipt);
                    list.Add(receiptDetailMgr.SummarizeReceiptDetails(receipt.ReceiptDetails));

                    string newUrl = reportMgr.WriteToFile(receipt.ReceiptTemplate, list);
                    receipt.IsPrinted = true;//to be refactored
                    receiptMgr.UpdateReceipt(receipt);
                    ReceiptNote receiptNote = Receipt2ReceiptNote(receipt);
                    receiptNote.PrintUrl = newUrl;
                    receiptNotes.Add(receiptNote);
                }
            }

            if (resolver.ReceiptNotes == null)
            {
                resolver.ReceiptNotes = receiptNotes;
            }
            else
            {
                IListHelper.AddRange<ReceiptNote>(resolver.ReceiptNotes, receiptNotes);
            }
        }

        private void PrintInspect(Resolver resolver, string[] region)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(InspectOrder));

            criteria.Add(Expression.Eq("IsPrinted", false));
            criteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
            criteria.Add(Expression.In("Region", region));

            IList<InspectOrder> inspectOrderList = criteriaMgr.FindAll<InspectOrder>(criteria);

            List<ReceiptNote> receiptNotes = new List<ReceiptNote>();
            if (inspectOrderList != null && inspectOrderList.Count > 0)
            {
                foreach (InspectOrder inspectOrder in inspectOrderList)
                {

                    string newUrl = reportMgr.WriteToFile("InspectOrder.xls", inspectOrder.InspectNo);
                    inspectOrder.IsPrinted = true;//to be refactored
                    inspectOrderMgr.UpdateInspectOrder(inspectOrder);
                    ReceiptNote receiptNote = InspectOrder2ReceiptNote(inspectOrder);
                    receiptNote.PrintUrl = newUrl;
                    receiptNotes.Add(receiptNote);
                }
            }

            if (resolver.ReceiptNotes == null)
            {
                resolver.ReceiptNotes = receiptNotes;
            }
            else
            {
                IListHelper.AddRange<ReceiptNote>(resolver.ReceiptNotes, receiptNotes);
            }
        }

        private void PrintPickList(Resolver resolver, string[] region)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(PickList));
            criteria.Add(Expression.Eq("IsPrinted", false));
            criteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT));
            criteria.Add(Expression.In("PartyFrom.Code", region));

            IList<PickList> pickList = criteriaMgr.FindAll<PickList>(criteria);

            List<ReceiptNote> receiptNotes = new List<ReceiptNote>();
            if (pickList != null && pickList.Count > 0)
            {
                foreach (PickList pl in pickList)
                {
                    string newUrl = reportMgr.WriteToFile("PickList.xls", pl.PickListNo);
                    pl.IsPrinted = true;//to be refactored
                    pickListMgr.UpdatePickList(pl);
                    ReceiptNote receiptNote = PickList2ReceiptNote(pl);
                    receiptNote.PrintUrl = newUrl;
                    receiptNotes.Add(receiptNote);
                }
            }

            if (resolver.ReceiptNotes == null)
            {
                resolver.ReceiptNotes = receiptNotes;
            }
            else
            {
                IListHelper.AddRange<ReceiptNote>(resolver.ReceiptNotes, receiptNotes);
            }
        }

        private void PrintASN(Resolver resolver, string[] region)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(InProcessLocation));

            criteria.Add(Expression.Eq("IsPrinted", false));
            criteria.Add(Expression.Eq("NeedPrintAsn", true));
            criteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
            criteria.Add(Expression.Or(Expression.Eq("OrderType", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION),
                                       Expression.Eq("OrderType", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)));
            criteria.Add(Expression.In("PartyFrom.Code", region));

            IList<InProcessLocation> inProcessLocationList = criteriaMgr.FindAll<InProcessLocation>(criteria);

            List<ReceiptNote> receiptNotes = new List<ReceiptNote>();
            if (inProcessLocationList != null && inProcessLocationList.Count > 0)
            {
                foreach (InProcessLocation inProcessLocation in inProcessLocationList)
                {
                    IList<object> list = new List<object>();
                    list.Add(inProcessLocation);
                    list.Add(inProcessLocation.InProcessLocationDetails);

                    string newUrl = reportMgr.WriteToFile(inProcessLocation.AsnTemplate, list);
                    inProcessLocation.IsPrinted = true;//to be refactored
                    inProcessLocationMgr.UpdateInProcessLocation(inProcessLocation);
                    ReceiptNote receiptNote = InProcessLocation2ReceiptNote(inProcessLocation);
                    receiptNote.PrintUrl = newUrl;
                    receiptNotes.Add(receiptNote);
                }
            }

            if (resolver.ReceiptNotes == null)
            {
                resolver.ReceiptNotes = receiptNotes;
            }
            else
            {
                IListHelper.AddRange<ReceiptNote>(resolver.ReceiptNotes, receiptNotes);
            }
        }


        private void PrintOrder(Resolver resolver, string[] region)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderHead));

            criteria.Add(Expression.Eq("IsPrinted", false));
            criteria.Add(Expression.Eq("NeedPrintOrder", true));
            criteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
            criteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER));
            criteria.Add(Expression.In("PartyFrom.Code", region));

            IList<OrderHead> orderList = criteriaMgr.FindAll<OrderHead>(criteria);

            List<ReceiptNote> receiptNotes = new List<ReceiptNote>();
            if (orderList != null && orderList.Count > 0)
            {
                foreach (OrderHead orderHead in orderList)
                {
                    IList<object> list = new List<object>();
                    list.Add(orderHead);
                    list.Add(orderHead.OrderDetails);

                    string newUrl = reportMgr.WriteToFile(orderHead.OrderTemplate, list);
                    orderHead.IsPrinted = true;//to be refactored
                    orderHeadMgr.UpdateOrderHead(orderHead);
                    ReceiptNote receiptNote = Order2ReceiptNote(orderHead);
                    receiptNote.PrintUrl = newUrl;
                    receiptNotes.Add(receiptNote);
                }
            }

            if (resolver.ReceiptNotes == null)
            {
                resolver.ReceiptNotes = receiptNotes;
            }
            else
            {
                IListHelper.AddRange<ReceiptNote>(resolver.ReceiptNotes, receiptNotes);
            }

        }

        private ReceiptNote InspectOrder2ReceiptNote(InspectOrder inspectOrder)
        {
            ReceiptNote receiptNote = new ReceiptNote();
            receiptNote.OrderNo = inspectOrder.InspectNo;
            receiptNote.CreateDate = inspectOrder.CreateDate;
            receiptNote.CreateUser = inspectOrder.CreateUser == null ? string.Empty : inspectOrder.CreateUser.Code;
            receiptNote.Status = inspectOrder.Status;

            return receiptNote;
        }

        private ReceiptNote PickList2ReceiptNote(PickList pickList)
        {
            ReceiptNote receiptNote = new ReceiptNote();
            receiptNote.OrderNo = pickList.PickListNo;
            receiptNote.CreateDate = pickList.CreateDate;
            receiptNote.CreateUser = pickList.CreateUser == null ? string.Empty : pickList.CreateUser.Code;
            receiptNote.Status = pickList.Status;

            return receiptNote;
        }

        private ReceiptNote InProcessLocation2ReceiptNote(InProcessLocation inProcessLocation)
        {
            ReceiptNote receiptNote = new ReceiptNote();
            receiptNote.OrderNo = inProcessLocation.IpNo;
            receiptNote.CreateDate = inProcessLocation.CreateDate;
            receiptNote.CreateUser = inProcessLocation.CreateUser == null ? string.Empty : inProcessLocation.CreateUser.Code;
            receiptNote.Status = inProcessLocation.Status;

            return receiptNote;
        }

        private ReceiptNote Order2ReceiptNote(OrderHead orderHead)
        {
            ReceiptNote receiptNote = new ReceiptNote();
            receiptNote.OrderNo = orderHead.OrderNo;
            receiptNote.CreateDate = orderHead.CreateDate;
            receiptNote.CreateUser = orderHead.CreateUser == null ? string.Empty : orderHead.CreateUser.Code;
            receiptNote.Status = orderHead.Status;

            return receiptNote;
        }

        private ReceiptNote Receipt2ReceiptNote(Receipt receipt)
        {
            ReceiptNote receiptNote = new ReceiptNote();
            receiptNote.OrderNo = receipt.ReceiptNo;
            receiptNote.CreateDate = receipt.CreateDate;
            receiptNote.CreateUser = receipt.CreateUser == null ? string.Empty : receipt.CreateUser.Code;
            receiptNote.Status = "Close";

            return receiptNote;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {

        }
        protected override void GetDetail(Resolver resolver)
        {

        }
        protected override void SetDetail(Resolver resolver)
        {

        }
        protected override void ExecuteSubmit(Resolver resolver)
        {

        }
        protected override void ExecuteCancel(Resolver resolver)
        {

        }
        protected override void ExecutePrint(Resolver resolver)
        {

        }
    }
}
