using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using NHibernate.Expression;
using com.Sconit.Entity.Production;
using Castle.Services.Transaction;
using com.Sconit.Entity.View;
using com.Sconit.Entity.Dss;

namespace com.Sconit.Service.Dss.Impl
{
    [Transactional]
    public class WoReceiptInboundMgr : AbstractInboundMgr
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssInbound");

        private ICriteriaMgr criteriaMgr;
        private IUomConversionMgr uomConversionMgr;
        private IUserMgr userMgr;
        private IHuMgr huMgr;
        private INumberControlMgr numberControlMgr;
        private IOrderMgr orderManager;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        private IDssImportHistoryMgr dssImportHistoryMgr;

        public WoReceiptInboundMgr(
            ICriteriaMgr criteriaMgr,
            IUomConversionMgr uomConversionMgr,
            IUserMgr userMgr,
            IHuMgr huMgr,
            INumberControlMgr numberControlMgr,
            IOrderMgr orderManager,
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            IDssImportHistoryMgr dssImportHistoryMgr)
            : base(dssImportHistoryMgr)
        {
            this.criteriaMgr = criteriaMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.userMgr = userMgr;
            this.huMgr = huMgr;
            this.numberControlMgr = numberControlMgr;
            this.orderManager = orderManager;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.dssImportHistoryMgr = dssImportHistoryMgr;
        }

        public override FlatFileReader DataReader(string fileName, Encoding enc, string delimiter)
        {
            return base.DataReader(fileName, Encoding.ASCII, "\t");
        }

        protected override object DeserializeForDelete(DssImportHistory dssImportHistory)
        {
            return dssImportHistory;
        }

        protected override object DeserializeForCreate(DssImportHistory dssImportHistory)
        {
            dssImportHistory.ItemCode = dssImportHistory[1];
            dssImportHistory.HuId = dssImportHistory[2];
            dssImportHistory.Qty = decimal.Parse(dssImportHistory[3]);
            dssImportHistory.ShiftCode = BarcodeHelper.GetShiftCode(dssImportHistory.HuId);

            return dssImportHistory;
        }
        protected override void CreateOrUpdateObject(object obj)
        {
        }
        protected override void DeleteObject(object obj)
        {
        }

        protected override void CreateOrUpdateObject(IList<object> objList)
        {
            List<DssImportHistory> sourceList = objList.Select(o => (DssImportHistory)o).ToList<DssImportHistory>();

            if (sourceList != null && sourceList.Count > 0)
            {
                foreach (DssImportHistory dssImportHistory in sourceList)
                {
                    ReceiveWo(dssImportHistory);
                }
            }
        }

        [Transaction(TransactionMode.RequiresNew)]
        public virtual void ReceiveWo(DssImportHistory dssImportHistory)
        {
            try
            {
                string prodLine = dssImportHistory.data0;
                string itemCode = dssImportHistory.data1;
                string huId = dssImportHistory.data2;
                decimal qty = decimal.Parse(dssImportHistory.data3);
                string itemHuId = dssImportHistory.data4;
                string onlineDate = dssImportHistory.data5;
                string onlineTime = dssImportHistory.data6;
                string offlineDate = dssImportHistory.data7;
                string offlineTime = dssImportHistory.data8;

                DateTime woOffTime = DateTime.Parse(offlineDate);
                string[] timeArr = offlineTime.Split(':');
                if (timeArr == null && timeArr.Length != 3)
                {
                    log.Error("Error date format" + offlineTime);
                    dssImportHistory.Memo = "Error date format" + offlineTime;
                    dssImportHistory.ErrorCount++;
                    this.dssImportHistoryMgr.UpdateDssImportHistory(dssImportHistory);
                    return;
                }
                DateTime woTime = woOffTime.AddHours(double.Parse(timeArr[0])).AddMinutes(double.Parse(timeArr[1])).AddSeconds(double.Parse(timeArr[2]));

                string customerCode = dssImportHistory.data9;
                string customerLoc = dssImportHistory.data10;

                if (this.huMgr.LoadHu(huId) != null)
                {
                    log.Error("Hu " + huId + " already exist in database.");
                    dssImportHistory.Memo = "Hu " + huId + " already exist in database.";
                    dssImportHistory.ErrorCount++;
                    this.dssImportHistoryMgr.UpdateDssImportHistory(dssImportHistory);

                    return;
                }

                #region 查找工单
                //shiftCode = BarcodeHelper.GetShiftCode(huId);
                DetachedCriteria criteria = DetachedCriteria.For<OrderDetail>();
                criteria.CreateAlias("OrderHead", "od");
               // criteria.CreateAlias("od.Flow", "f");
                criteria.CreateAlias("Item", "i");
                //criteria.CreateAlias("Shift", "s");

                criteria.Add(Expression.Like("od.Flow", prodLine, MatchMode.End));
                criteria.Add(Expression.Eq("i.Code", itemCode));
                //criteria.Add(Expression.Eq("s.Code", shiftCode));
                criteria.Add(Expression.Eq("od.Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
                criteria.Add(Expression.Eq("od.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
                criteria.Add(Expression.Le("od.StartTime", woTime));
                criteria.Add(Expression.Ge("od.WindowTime", woTime));

                criteria.AddOrder(Order.Asc("od.StartTime"));

                IList<OrderDetail> orderDetailList = this.criteriaMgr.FindAll<OrderDetail>(criteria);

                OrderDetail orderDetail = null;
                if (orderDetailList != null && orderDetailList.Count > 0)
                {
                    for (int i = 0; i < orderDetailList.Count; i++)
                    {
                        orderDetail = orderDetailList[i];
                        if (orderDetail.ReceivedQty == null || orderDetail.OrderedQty > (decimal)orderDetail.ReceivedQty)
                        {
                            break;
                        }
                    }

                    log.Info("Find match wo " + orderDetail.OrderHead.OrderNo);
                }
                else
                {
                    FlowDetail flowDetail = this.LoadFlowDetail(prodLine, itemCode);
                    if (flowDetail != null)
                    {
                        OrderHead orderHead = this.orderManager.TransferFlow2Order(flowDetail.Flow);
                        orderDetail = orderHead.OrderDetails.SingleOrDefault(o => flowDetail.Equals(o.FlowDetail));
                        orderHead.StartTime = woTime.AddHours(-12);
                        orderHead.WindowTime = woTime.AddHours(12);
                        orderHead.Priority = BusinessConstants.CODE_MASTER_ORDER_PRIORITY_VALUE_NORMAL;
                        orderDetail.RequiredQty = qty;
                        orderDetail.OrderedQty = qty;
                        OrderHelper.FilterZeroOrderQty(orderHead);
                        this.orderManager.CreateOrder(orderHead, this.userMgr.GetMonitorUser());
                        log.Info("Find match wo " + orderHead.OrderNo);
                    }
                }

                if (orderDetail != null)
                {
                    this.FlushSession();
                    this.CleanSession();
                    this.orderManager.DoReceiveWO(huId, orderDetail, qty);
                    dssImportHistory.IsActive = false;
                    this.dssImportHistoryMgr.UpdateDssImportHistory(dssImportHistory);
                }
                else
                {
                    log.Error("No item found for item code " + itemCode + " for prodline + " + prodLine);
                    dssImportHistory.Memo = "No item found for item code " + itemCode + " for prodline + " + prodLine;
                    dssImportHistory.ErrorCount++;
                    this.dssImportHistoryMgr.UpdateDssImportHistory(dssImportHistory);
                }
                #endregion
            }
            catch (Exception ex)
            {
                this.CleanSession();
                log.Error("Receive WO Error.", ex);
                dssImportHistory.Memo = ex.Message;
                dssImportHistory.ErrorCount++;
                this.dssImportHistoryMgr.UpdateDssImportHistory(dssImportHistory);
            }
        }

        protected void CreateOrUpdateObject2(IList<object> objList)
        {
            List<DssImportHistory> sourceList = objList.Select(o => (DssImportHistory)o).ToList<DssImportHistory>();
            IList<string> huIdList = sourceList.Select(d => d.data2).Distinct().ToList<string>();
            IList<Hu> huList = this.GetHu(huIdList);
            //过滤掉已存在的Hu
            //IList<DssImportHistory> dssImportHistoryList = sourceList
            //    .Where(d => huList.SingleOrDefault(h => StringHelper.Eq(d.HuId, h.HuId)) == null)
            //    .ToList<DssImportHistory>();
            IList<DssImportHistory> dssImportHistoryList = new List<DssImportHistory>();
            IList<DssImportHistory> huExistDssImportHistoryList = new List<DssImportHistory>();
            if (huList != null && huList.Count > 0 && sourceList != null && sourceList.Count > 0)
            {
                foreach (DssImportHistory dssImportHistory in sourceList)
                {
                    bool findMatch = false;
                    foreach (Hu hu in huList)
                    {
                        if (hu.HuId == dssImportHistory.data2)
                        {
                            dssImportHistory.Memo = "Hu " + dssImportHistory.data2 + " already exist in database.";
                            dssImportHistory.ErrorCount++;
                            huExistDssImportHistoryList.Add(dssImportHistory);
                            findMatch = true;
                            break;
                        }
                    }

                    if (!findMatch)
                    {
                        dssImportHistoryList.Add(dssImportHistory);
                    }
                }
            }
            else
            {
                dssImportHistoryList = sourceList;
            }

            if (huExistDssImportHistoryList != null && huExistDssImportHistoryList.Count > 0)
            {
                this.dssImportHistoryMgr.UpdateDssImportHistory(huExistDssImportHistoryList);
            }

            if (dssImportHistoryList != null && dssImportHistoryList.Count > 0)
            {
                this.BatchProcess(dssImportHistoryList);
            }

            log.Info("Wo receipt batch job is finished.");

        }

        private void BatchProcess(IList<DssImportHistory> dssImportHistoryList)
        {
            if (dssImportHistoryList == null || dssImportHistoryList.Count == 0)
                return;

            IList<string> itemList = dssImportHistoryList.Select(d => d.data1).Distinct().ToList<string>();
            IList<string> shiftList = dssImportHistoryList.Select(d => d.ShiftCode).Distinct().ToList<string>();

            var query = from d in dssImportHistoryList
                        group d by new { d.ProdLine, d.ShiftCode, Item = d.ItemCode } into g
                        select new { g.Key, list = g.ToList<DssImportHistory>(), Qty = g.Sum(d => d.Qty) };

            foreach (var q in query)
            {
                string prodLine = q.Key.ProdLine;
                string shiftCode = q.Key.ShiftCode;
                string itemCode = q.Key.Item;
                decimal qty = q.Qty;
                List<DssImportHistory> list = q.list;

                if (list == null || list.Count == 0)
                    continue;

                OrderDetail orderDetail = null;
                Receipt receipt = null;
                IList<OrderLocationTransaction> orderLocationTransactionList = this.GetOrderLocationTransaction(prodLine, shiftCode, itemCode);
                IList<ReceiptDetail> receiptDetailList = new List<ReceiptDetail>();
                if (orderLocationTransactionList != null && orderLocationTransactionList.Count > 0)
                {
                    ReceiptDetail receiptDetail = new ReceiptDetail();
                    receiptDetail.OrderLocationTransaction = orderLocationTransactionList[0];
                    orderDetail = orderLocationTransactionList[0].OrderDetail;

                    receipt = this.BuildReceipt(orderLocationTransactionList[0], list);

                    //write db
                    try
                    {
                        ReceiveWo(list, orderDetail, receipt);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                else
                {
                    foreach (var dssImportHistory in q.list)
                    {
                        dssImportHistory.Memo = "No FG find for prodLine " + prodLine + ",shift " + shiftCode + ",item " + itemCode;
                        dssImportHistory.ErrorCount++;
                    }
                    this.dssImportHistoryMgr.UpdateDssImportHistory(q.list);
                }
            }
        }

        [Transaction(TransactionMode.RequiresNew)]
        public virtual void ReceiveWo(IList<DssImportHistory> list, OrderDetail orderDetail, Receipt receipt)
        {
            try
            {
                IList<Hu> huList = this.ResolveAndCreateHu(list, orderDetail);
                this.orderManager.ReceiveOrder(receipt, this.userMgr.GetMonitorUser());
                this.dssImportHistoryMgr.UpdateDssImportHistory(list, false);
            }
            catch (Exception ex)
            {
                foreach (var dssImportHistory in list)
                {
                    dssImportHistory.Memo = ex.Message;
                    dssImportHistory.ErrorCount++;
                }
                this.dssImportHistoryMgr.UpdateDssImportHistory(list);
            }
        }

        private Receipt BuildReceipt(OrderLocationTransaction orderLocationTransaction, IList<DssImportHistory> dssImportHistoryList)
        {
            if (orderLocationTransaction == null || dssImportHistoryList == null || dssImportHistoryList.Count == 0)
                return null;

            Receipt receipt = new Receipt();
            IList<OrderLocationTransaction> orderLocTransList = this.orderLocationTransactionMgr.GetOrderLocationTransaction(orderLocationTransaction.OrderDetail.Id, BusinessConstants.IO_TYPE_OUT);
            foreach (var dssImportHistory in dssImportHistoryList)
            {
                ReceiptDetail receiptDetail = new ReceiptDetail();
                receiptDetail.Receipt = receipt;
                receiptDetail.OrderLocationTransaction = orderLocationTransaction;
                receiptDetail.HuId = dssImportHistory.HuId;
                receiptDetail.ReceivedQty = dssImportHistory.Qty;

                #region 找Out的OrderLocTrans，填充MaterialFlushBack
                foreach (OrderLocationTransaction orderLocTrans in orderLocTransList)
                {
                    MaterialFlushBack material = new MaterialFlushBack();
                    material.OrderLocationTransaction = orderLocTrans;
                    if (orderLocTrans.UnitQty != 0)
                    {
                        material.Qty = dssImportHistory.Qty;
                    }
                    receiptDetail.AddMaterialFlushBack(material);
                }
                #endregion

                receipt.AddReceiptDetail(receiptDetail);
            }

            return receipt;
        }

        private IList<Hu> ResolveAndCreateHu(IList<DssImportHistory> dssImportHistoryList, OrderDetail orderDetail)
        {
            IList<Hu> huList = new List<Hu>();
            if (dssImportHistoryList != null && dssImportHistoryList.Count > 0)
            {
                foreach (var dssImportHistory in dssImportHistoryList)
                {
                    Hu hu = this.ResolveAndCreateHu(dssImportHistory.HuId, orderDetail, dssImportHistory.Qty);
                    huList.Add(hu);
                }
            }
            return huList;
        }

        private Hu ResolveAndCreateHu(string barCode, OrderDetail orderDetail, decimal qty)
        {
            string[] splitedBarcode = BarcodeHelper.SplitFGBarcode(barCode);
            Item item = orderDetail.Item;
            string lotNo = splitedBarcode[2];
            DateTime manufactureDate = LotNoHelper.ResolveLotNo(lotNo);

            Hu hu = new Hu();
            hu.HuId = barCode;
            hu.Item = item;
            hu.Uom = orderDetail.Uom;   //用Flow单位
            #region 单位用量
            if (item.Uom.Code != orderDetail.Uom.Code)
            {
                hu.UnitQty = this.uomConversionMgr.ConvertUomQty(item, orderDetail.Uom, 1, item.Uom);   //单位用量
            }
            else
            {
                hu.UnitQty = 1;
            }
            #endregion
            hu.QualityLevel = BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1;
            hu.Qty = qty;
            hu.UnitCount = orderDetail.UnitCount;
            hu.LotNo = lotNo;
            hu.LotSize = qty;
            hu.ManufactureDate = manufactureDate;
            hu.ManufactureParty = orderDetail.OrderHead.PartyFrom;
            hu.CreateUser = this.userMgr.GetMonitorUser();
            hu.CreateDate = DateTime.Now;
            hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CREATE;

            this.huMgr.CreateHu(hu);
            this.numberControlMgr.ReverseUpdateHuId(barCode);

            return hu;
        }

        private Hu CreateItemHu(string barCode, OrderDetail orderDetail, string lotNo, DateTime manufactureDate)
        {
            Item item = orderDetail.Item;
            decimal qty = 1;

            Hu hu = new Hu();
            hu.HuId = barCode;
            hu.Item = item;
            hu.Uom = orderDetail.Uom;   //用Flow单位
            #region 单位用量
            if (item.Uom.Code != orderDetail.Uom.Code)
            {
                hu.UnitQty = this.uomConversionMgr.ConvertUomQty(item, orderDetail.Uom, 1, item.Uom);   //单位用量
            }
            else
            {
                hu.UnitQty = 1;
            }
            #endregion
            hu.QualityLevel = BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1;
            hu.Qty = qty;
            hu.UnitCount = 1;
            hu.LotNo = lotNo;
            hu.LotSize = 1;
            hu.ManufactureDate = manufactureDate;
            hu.ManufactureParty = orderDetail.OrderHead.PartyFrom;
            hu.CreateUser = this.userMgr.GetMonitorUser();
            hu.CreateDate = DateTime.Now;
            hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CREATE;

            this.huMgr.CreateHu(hu);

            return hu;
        }

        #region Load Data from Database

        #endregion
        private FlowDetail LoadFlowDetail(string prodLine, string itemCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowDetail));
            criteria.CreateAlias("Flow", "f");
            criteria.Add(Expression.Like("f.Code", prodLine, MatchMode.End));
            criteria.Add(Expression.Eq("f.Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION));
            criteria.Add(Expression.Eq("Item.Code", itemCode));

            IList<FlowDetail> flowDetails = this.criteriaMgr.FindAll<FlowDetail>(criteria);
            if (flowDetails != null && flowDetails.Count > 0)
            {
                return flowDetails[0];
            }
            else
            {
                return null;
            }
        }

        private IList<Hu> GetHu(IList<string> huIdList)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Hu));
            criteria.Add(Expression.InG<string>("HuId", huIdList));

            return this.criteriaMgr.FindAll<Hu>(criteria);
        }

        private IList<OrderLocationTransaction> GetOrderLocationTransaction(string productionLine, string shiftCode, string itemCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<OrderLocationTransaction>();
            criteria.CreateAlias("OrderDetail", "od");
            criteria.CreateAlias("od.OrderHead", "oh");

            criteria.Add(Expression.Eq("oh.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
            criteria.Add(Expression.Eq("oh.Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
            criteria.Add(Expression.Like("oh.Flow.Code", productionLine, MatchMode.End));
            //criteria.Add(Expression.Eq("oh.Shift.Code", shiftCode));
            criteria.Add(Expression.Eq("Item.Code", itemCode));
            //criteria.Add(Expression.Or(Expression.IsNull("od.ReceivedQty"), Expression.LtProperty("od.ReceivedQty", "od.OrderedQty")));
            criteria.AddOrder(Order.Asc("oh.StartTime"));

            return this.criteriaMgr.FindAll<OrderLocationTransaction>(criteria);
        }

    }
}
