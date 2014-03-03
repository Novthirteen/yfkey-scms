using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Service.Criteria;
using NPOI.HSSF.UserModel;
using System.Collections;
using com.Sconit.Utility;
using System.IO;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using NPOI.SS.UserModel;
using com.Sconit.Entity.MRP;


namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ImportMgr : IImportMgr
    {
        #region 变量
        private ICriteriaMgr criteriaMgr;
        private IShiftMgr shiftMgr;
        private IFlowDetailMgr flowDetailMgr;
        private IItemMgr itemMgr;
        private IUomMgr uomMgr;
        private IUomConversionMgr uomConversionMgr;
        private IHuMgr huMgr;
        private IStorageBinMgr storageBinMgr;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        private ISupplierMgr supplierMgr;
        private IFlowMgr flowMgr;
        private IOrderMgr orderMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;
        private IItemReferenceMgr itemReferenceMgr;
        #endregion

        #region 构造函数
        public ImportMgr(ICriteriaMgr criteriaMgr,
            IShiftMgr shiftMgr,
            IFlowDetailMgr flowDetailMgr,
            IItemMgr itemMgr,
            IUomMgr uomMgr,
            IUomConversionMgr uomConversionMgr,
            IHuMgr huMgr,
            IStorageBinMgr storageBinMgr,
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            ISupplierMgr supplierMgr,
            IFlowMgr flowMgr,
             IOrderMgr orderMgr,
            IEntityPreferenceMgr entityPreferenceMgr,
            IItemReferenceMgr itemReferenceMgr)
        {
            this.criteriaMgr = criteriaMgr;
            this.shiftMgr = shiftMgr;
            this.flowDetailMgr = flowDetailMgr;
            this.itemMgr = itemMgr;
            this.uomMgr = uomMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.huMgr = huMgr;
            this.storageBinMgr = storageBinMgr;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.supplierMgr = supplierMgr;
            this.flowMgr = flowMgr;
            this.orderMgr = orderMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
            this.itemReferenceMgr = itemReferenceMgr;
        }
        #endregion

        #region IImportMgr接口实现
        [Transaction(TransactionMode.Unspecified)]
        public IList<ShiftPlanSchedule> ReadPSModelFromXls(Stream inputStream, User user, string regionCode, string flowCode, DateTime date, string shiftCode)
        {
            IList<ShiftPlanSchedule> spsList = new List<ShiftPlanSchedule>();
            Shift shift = shiftMgr.LoadShift(shiftCode);

            if (inputStream.Length == 0)
                throw new BusinessErrorException("Import.Stream.Empty");

            if (shift == null)
                throw new BusinessErrorException("Import.PSModel.ShiftNotExist");

            HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

            Sheet sheet = workbook.GetSheetAt(0);
            IEnumerator rows = sheet.GetRowEnumerator();

            ImportHelper.JumpRows(rows, 4);
            int colIndex = this.GetPlanColumnIndexToRead((HSSFRow)rows.Current, shift.ShiftName, date);

            if (colIndex < 0)
                throw new BusinessErrorException("Import.PSModel.Shift.Not.Exist", shift.ShiftName);

            ImportHelper.JumpRows(rows, 2);

            while (rows.MoveNext())
            {
                Row row = (HSSFRow)rows.Current;
                if (!this.CheckValidDataRow(row, 1, 4))
                {
                    break;//边界
                }

                //string regCode=row.GetCell(
                string fCode = string.Empty;
                string itemCode = string.Empty;
                int seq = 0;
                decimal planQty = 0;
                Cell cell = null;

                #region 读取生产线
                fCode = row.GetCell(1).StringCellValue;
                if (fCode.Trim() == string.Empty)
                    throw new BusinessErrorException("Import.PSModel.Empty.Error.Flow", (row.RowNum + 1).ToString());

                if (flowCode != null && flowCode.Trim() != string.Empty)
                {
                    if (fCode.Trim().ToUpper() != flowCode.Trim().ToUpper())
                        continue;//生产线过滤
                }
                #endregion

                #region 读取序号
                try
                {
                    string seqStr = row.GetCell(2).StringCellValue;
                    seq = row.GetCell(2).StringCellValue.Trim() != string.Empty ? int.Parse(row.GetCell(2).StringCellValue) : 0;
                }
                catch
                {
                    throw new BusinessErrorException("Import.PSModel.Read.Error.Seq", (row.RowNum + 1).ToString());
                }
                #endregion

                #region 读取成品代码
                try
                {
                    itemCode = row.GetCell(3).StringCellValue;
                    if (itemCode == string.Empty)
                        throw new BusinessErrorException("Import.PSModel.Empty.Error.ItemCode", (row.RowNum + 1).ToString());
                }
                catch
                {
                    throw new BusinessErrorException("Import.PSModel.Read.Error.ItemCode", (row.RowNum + 1).ToString());
                }
                #endregion

                #region 读取计划量
                try
                {
                    cell = row.GetCell(colIndex);
                    if (cell == null || cell.CellType == NPOI.SS.UserModel.CellType.BLANK)
                        continue;

                    planQty = Convert.ToDecimal(row.GetCell(colIndex).NumericCellValue);
                }
                catch
                {
                    throw new BusinessErrorException("Import.PSModel.Read.Error.PlanQty", (row.RowNum + 1).ToString());
                }
                #endregion

                FlowDetail flowDetail = flowDetailMgr.LoadFlowDetail(fCode, itemCode, seq);
                if (flowDetail == null)
                    throw new BusinessErrorException("Import.PSModel.FlowDetail.Not.Exist", (row.RowNum + 1).ToString());

                //区域权限过滤
                if (regionCode != null && regionCode.Trim() != string.Empty)
                {
                    if (regionCode.Trim().ToUpper() != flowDetail.Flow.PartyTo.Code.ToUpper())
                        continue;
                }
                if (!user.HasPermission(flowDetail.Flow.PartyTo.Code))
                    continue;

                ShiftPlanSchedule sps = new ShiftPlanSchedule();
                sps.FlowDetail = flowDetail;
                sps.ReqDate = date;
                sps.Shift = shift;
                sps.PlanQty = planQty;
                sps.LastModifyUser = user;
                sps.LastModifyDate = DateTime.Now;
                spsList.Add(sps);
            }

            if (spsList.Count == 0)
                throw new BusinessErrorException("Import.Result.Error.ImportNothing");

            return spsList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<FlowPlan> ReadShipScheduleYFKFromXls(Stream inputStream, User user, string planType, string partyCode, string timePeriodType, DateTime date)
        {
            IList<FlowPlan> flowPlanList = new List<FlowPlan>();
            if (inputStream.Length == 0)
                throw new BusinessErrorException("Import.Stream.Empty");

            HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

            Sheet sheet = workbook.GetSheetAt(0);
            IEnumerator rows = sheet.GetRowEnumerator();

            ImportHelper.JumpRows(rows, 8);
            int colIndex = this.GetColumnIndexToRead_ShipScheduleYFK((HSSFRow)rows.Current, date);

            if (colIndex < 0)
                throw new BusinessErrorException("Import.MRP.DateNotExist", date.ToShortDateString());

            #region 列定义
            int colFlow = 1;//Flow
            int colUC = 6;//单包装
            #endregion

            while (rows.MoveNext())
            {
                Row row = (HSSFRow)rows.Current;
                if (!this.CheckValidDataRow(row, 1, 6))
                {
                    break;//边界
                }

                //string regCode=row.GetCell(
                string flowCode = string.Empty;
                string itemCode = string.Empty;
                decimal UC = 1;
                decimal planQty = 0;
                string refOrderNo = string.Empty;
                #region 读取客户代码
                //try
                //{
                //    pCode = row.GetCell(1).StringCellValue;
                //    if (pCode.Trim() == string.Empty)
                //        throw new BusinessErrorException("Import.MRP.Empty.Error.Customer", (row.RowNum + 1).ToString());

                //    if (partyCode != null && partyCode.Trim() != string.Empty)
                //    {
                //        if (pCode.Trim().ToUpper() != partyCode.Trim().ToUpper())
                //            continue;//客户过滤
                //    }
                //}
                //catch
                //{
                //    throw new BusinessErrorException("Import.MRP.Read.Error.Customer", (row.RowNum + 1).ToString());
                //}
                #endregion

                #region 读取参考订单号
                try
                {
                    refOrderNo = row.GetCell(3).StringCellValue;
                }
                catch { throw new BusinessErrorException("Import.MRP.Read.Error.RefOrderNo", (row.RowNum + 1).ToString()); }

                #endregion

                #region 读取Flow
                try
                {
                    flowCode = row.GetCell(colFlow).StringCellValue;
                    if (flowCode.Trim() == string.Empty)
                        continue;
                }
                catch
                {
                    this.ThrowCommonError(row, colIndex);
                }
                #endregion

                #region 读取成品代码
                try
                {
                    itemCode = row.GetCell(4).StringCellValue;
                    if (itemCode == string.Empty)
                        throw new BusinessErrorException("Import.PSModel.Empty.Error.ItemCode", (row.RowNum + 1).ToString());
                }
                catch
                {
                    throw new BusinessErrorException("Import.PSModel.Read.Error.ItemCode", (row.RowNum + 1).ToString());
                }
                #endregion

                #region 读取单包装
                try
                {
                    UC = Convert.ToDecimal(row.GetCell(colUC).NumericCellValue);
                }
                catch
                {
                    this.ThrowCommonError(row.RowNum, colUC, row.GetCell(colUC));
                }
                #endregion

                #region 读取计划量
                try
                {
                    planQty = Convert.ToDecimal(row.GetCell(colIndex).NumericCellValue);
                }
                catch
                {
                    throw new BusinessErrorException("Import.PSModel.Read.Error.PlanQty", (row.RowNum + 1).ToString());
                }
                #endregion

                FlowDetail flowDetail = this.LoadFlowDetailByFlow(flowCode, itemCode, UC);
                if (flowDetail == null)
                    throw new BusinessErrorException("Import.MRP.Distribution.FlowDetail.Not.Exist", (row.RowNum + 1).ToString());

                if (partyCode != null && partyCode.Trim() != string.Empty)
                {
                    if (!StringHelper.Eq(flowCode, partyCode))
                        continue;//客户过滤
                }

                //区域过滤
                if (partyCode != null && partyCode.Trim() != string.Empty)
                {
                    if (!StringHelper.Eq(partyCode, flowDetail.Flow.PartyTo.Code))
                        continue;//客户过滤
                }
                //区域权限过滤
                if (!user.HasPermission(flowDetail.Flow.PartyFrom.Code) && !user.HasPermission(flowDetail.Flow.PartyTo.Code))
                    continue;

                FlowPlan flowPlan = new FlowPlan();
                flowPlan.FlowDetail = flowDetail;
                flowPlan.TimePeriodType = timePeriodType;
                flowPlan.ReqDate = date;
                flowPlan.PlanQty = planQty;
                flowPlan.RefOrderNo = refOrderNo;
                flowPlanList.Add(flowPlan);
            }

            if (flowPlanList.Count == 0)
                throw new BusinessErrorException("Import.Result.Error.ImportNothing");

            return flowPlanList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<CycleCountDetail> ReadCycleCountFromXls(Stream inputStream, User user, CycleCount cycleCount)
        {
            if (inputStream.Length == 0)
                throw new BusinessErrorException("Import.Stream.Empty");

            //区域权限过滤
            if (!user.HasPermission(cycleCount.Location.Region.Code))
            {
                throw new BusinessErrorException("Common.Business.Error.NoPartyPermission", cycleCount.Location.Region.Code);
            }

            HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

            Sheet sheet = workbook.GetSheetAt(0);
            IEnumerator rows = sheet.GetRowEnumerator();

            ImportHelper.JumpRows(rows, 11);

            #region 列定义
            int colItem = 1;//物料代码
            int colUom = 3;//单位
            int colQty = 4;//数量
            int colHu = 5;//条码
            int colBin = 6;//库格
            #endregion

            IList<CycleCountDetail> cycleCountDetailList = new List<CycleCountDetail>();
            while (rows.MoveNext())
            {
                Row row = (HSSFRow)rows.Current;
                if (!this.CheckValidDataRow(row, 1, 7))
                {
                    break;//边界
                }

                if (row.GetCell(colHu) == null)
                {
                    string itemCode = string.Empty;
                    decimal qty = 0;
                    string uomCode = string.Empty;

                    #region 读取数据
                    #region 读取物料代码
                    itemCode = row.GetCell(colItem) != null ? row.GetCell(colItem).StringCellValue : string.Empty;
                    if (itemCode == null || itemCode.Trim() == string.Empty)
                        this.ThrowCommonError(row.RowNum, colItem, row.GetCell(colItem));

                    var i = (
                        from c in cycleCountDetailList
                        where c.HuId == null && c.Item.Code.Trim().ToUpper() == itemCode.Trim().ToUpper()
                        select c).Count();

                    if (i > 0)
                        throw new BusinessErrorException("Import.Business.Error.Duplicate", itemCode, (row.RowNum + 1).ToString(), (colItem + 1).ToString());
                    #endregion

                    #region 读取数量
                    try
                    {
                        qty = Convert.ToDecimal(row.GetCell(colQty).NumericCellValue);
                    }
                    catch
                    {
                        this.ThrowCommonError(row.RowNum, colQty, row.GetCell(colQty));
                    }
                    #endregion

                    #region 读取单位
                    uomCode = row.GetCell(colUom) != null ? row.GetCell(colUom).StringCellValue : string.Empty;
                    if (uomCode == null || uomCode.Trim() == string.Empty)
                        throw new BusinessErrorException("Import.Read.Error.Empty", (row.RowNum + 1).ToString(), colUom.ToString());
                    #endregion
                    #endregion

                    #region 填充数据
                    Item item = itemMgr.CheckAndLoadItem(itemCode);
                    Uom uom = uomMgr.CheckAndLoadUom(uomCode);
                    //单位换算
                    if (item.Uom.Code.Trim().ToUpper() != uom.Code.Trim().ToUpper())
                    {
                        qty = uomConversionMgr.ConvertUomQty(item, uom, qty, item.Uom);
                    }

                    CycleCountDetail cycleCountDetail = new CycleCountDetail();
                    cycleCountDetail.CycleCount = cycleCount;
                    cycleCountDetail.Item = item;
                    cycleCountDetail.Qty = qty; cycleCountDetailList.Add(cycleCountDetail);
                    #endregion
                }
                else
                {
                    string huId = string.Empty;
                    string binCode = string.Empty;

                    #region 读取数据
                    #region 读取条码
                    huId = row.GetCell(colHu) != null ? row.GetCell(colHu).StringCellValue : string.Empty;
                    if (huId == null || huId.Trim() == string.Empty)
                        throw new BusinessErrorException("Import.Read.Error.Empty", (row.RowNum + 1).ToString(), colHu.ToString());

                    var i = (
                        from c in cycleCountDetailList
                        where c.HuId != null && c.HuId.Trim().ToUpper() == huId.Trim().ToUpper()
                        select c).Count();

                    if (i > 0)
                        throw new BusinessErrorException("Import.Business.Error.Duplicate", huId, (row.RowNum + 1).ToString(), colHu.ToString());
                    #endregion

                    #region 读取库格
                    binCode = row.GetCell(colBin) != null ? row.GetCell(colBin).StringCellValue : null;
                    if (cycleCount.PhyCntGroupBy == BusinessConstants.CODE_MASTER_PHYCNT_GROUPBY_BIN
                        && (binCode == null || binCode == string.Empty))
                    {
                        throw new BusinessErrorException("Import.Read.Error.Empty", (row.RowNum + 1).ToString(), colBin.ToString());
                    }
                    #endregion
                    #endregion

                    #region 填充数据
                    Hu hu = huMgr.CheckAndLoadHu(huId);
                    StorageBin bin = null;
                    if (binCode != null && binCode.Trim() != string.Empty)
                    {
                        bin = storageBinMgr.CheckAndLoadStorageBin(binCode);
                    }

                    CycleCountDetail cycleCountDetail = new CycleCountDetail();
                    cycleCountDetail.CycleCount = cycleCount;
                    cycleCountDetail.Item = hu.Item;
                    cycleCountDetail.Qty = hu.Qty * hu.UnitQty;
                    cycleCountDetail.HuId = hu.HuId;
                    cycleCountDetail.LotNo = hu.LotNo;
                    cycleCountDetail.StorageBin = bin != null ? bin.Code : null;
                    cycleCountDetailList.Add(cycleCountDetail);
                    #endregion
                }
            }

            if (cycleCountDetailList.Count == 0)
                throw new BusinessErrorException("Import.Result.Error.ImportNothing");

            return cycleCountDetailList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> ReadOrderLocationTransactionFromXls(Stream inputStream, string orderNo)
        {
            if (inputStream.Length == 0)
            {
                throw new BusinessErrorException("Import.Stream.Empty");

            }

            HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

            Sheet sheet = workbook.GetSheetAt(0);
            IEnumerator rows = sheet.GetRowEnumerator();

            ImportHelper.JumpRows(rows, 11);

            #region 列定义
            int colItem = 1;//物料代码
            int colUom = 3;//单位
            int colQty = 4;//数量
            #endregion

            IList<OrderLocationTransaction> existOrderLocTransList = orderLocationTransactionMgr.GetOrderLocationTransaction(orderNo, BusinessConstants.IO_TYPE_OUT);
            if (existOrderLocTransList == null || existOrderLocTransList.Count == 0)
            {
                throw new TechnicalException();
            }
            IList<OrderLocationTransaction> orderLocTransList = new List<OrderLocationTransaction>();
            while (rows.MoveNext())
            {
                Row row = (HSSFRow)rows.Current;
                if (!this.CheckValidDataRow(row, 1, 5))
                {
                    break;//边界
                }

                string itemCode = string.Empty;
                decimal qty = 0;
                string uomCode = string.Empty;

                #region 读取数据

                #region 读取物料代码
                itemCode = row.GetCell(colItem) != null ? row.GetCell(colItem).StringCellValue : string.Empty;
                if (itemCode == null || itemCode.Trim() == string.Empty)
                    this.ThrowCommonError(row.RowNum, colItem, row.GetCell(colItem));

                var i = (
                    from c in orderLocTransList
                    where c.Item.Code.Trim().ToUpper() == itemCode.Trim().ToUpper()
                    select c).Count();

                if (i == 0)
                {
                    i = (
                    from c in existOrderLocTransList
                    where c.Item.Code.Trim().ToUpper() == itemCode.Trim().ToUpper()
                    select c).Count();
                }

                if (i > 0)
                {
                    throw new BusinessErrorException("Import.Business.Error.Duplicate", itemCode, (row.RowNum + 1).ToString(), (colItem + 1).ToString());
                }
                #endregion

                #region 读取数量
                try
                {
                    qty = Convert.ToDecimal(row.GetCell(colQty).NumericCellValue);
                }
                catch
                {
                    this.ThrowCommonError(row.RowNum, colQty, row.GetCell(colQty));
                }
                #endregion

                #region 读取单位
                uomCode = row.GetCell(colUom) != null ? row.GetCell(colUom).StringCellValue : string.Empty;
                if (uomCode == null || uomCode.Trim() == string.Empty)
                {
                    throw new BusinessErrorException("Import.Read.Error.Empty", (row.RowNum + 1).ToString(), colUom.ToString());
                }
                #endregion

                #endregion

                #region 填充数据
                Item item = itemMgr.CheckAndLoadItem(itemCode);
                Uom uom = uomMgr.CheckAndLoadUom(uomCode);
                //单位换算
                if (item.Uom.Code.Trim().ToUpper() != uom.Code.Trim().ToUpper())
                {
                    qty = uomConversionMgr.ConvertUomQty(item, uom, qty, item.Uom);
                }

                OrderLocationTransaction orderLocTrans = new OrderLocationTransaction();
                orderLocTrans.OrderDetail = existOrderLocTransList[0].OrderDetail;
                orderLocTrans.Location = existOrderLocTransList[0].Location;
                orderLocTrans.Item = item;
                orderLocTrans.Uom = uom;
                orderLocTrans.OrderedQty = qty;
                orderLocTrans.UnitQty = 1;
                orderLocTrans.Operation = 10;
                orderLocTrans.IsAssemble = true;
                orderLocTrans.IOType = BusinessConstants.IO_TYPE_OUT;
                orderLocTrans.IsBlank = false;
                orderLocTrans.TransactionType = BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO;

                orderLocTransList.Add(orderLocTrans);
                #endregion


            }
            return orderLocTransList;
        }


        [Transaction(TransactionMode.Unspecified)]
        public IList<RollingForecast> ReadRollingForecastFromXls(Stream inputStream, User user)
        {
            if (inputStream.Length == 0)
            {
                throw new BusinessErrorException("Import.Stream.Empty");
            }

            HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

            Sheet sheet = workbook.GetSheetAt(0);
            IEnumerator rows = sheet.GetRowEnumerator();

            ImportHelper.JumpRows(rows, 11);

            #region 列定义
            int colSupplier = 1;//供应商代码
            int colItem = 2;//物料代码
            int colDate = 3;//日期
            int colQty = 4;//数量

            #endregion

            IList<RollingForecast> rollingForecastList = new List<RollingForecast>();
            while (rows.MoveNext())
            {
                Row row = (HSSFRow)rows.Current;
                if (!this.CheckValidDataRow(row, 1, 4))
                {
                    break;//边界
                }


                string supplierCode = string.Empty;
                string itemCode = string.Empty;
                DateTime date = DateTime.Now;
                decimal qty = 0;

                #region 读取数据
                #region 读取供应商和物料代码
                supplierCode = row.GetCell(colSupplier) != null ? row.GetCell(colSupplier).StringCellValue : string.Empty;
                if (supplierCode == null || supplierCode.Trim() == string.Empty)
                    this.ThrowCommonError(row.RowNum, colSupplier, row.GetCell(colSupplier));

                itemCode = row.GetCell(colItem) != null ? row.GetCell(colItem).StringCellValue : string.Empty;
                if (itemCode == null || itemCode.Trim() == string.Empty)
                    this.ThrowCommonError(row.RowNum, colItem, row.GetCell(colItem));


                #endregion

                #region 读取日期和数量

                try
                {
                    date = Convert.ToDateTime(row.GetCell(colDate).StringCellValue);
                }
                catch
                {
                    this.ThrowCommonError(row.RowNum, colDate, row.GetCell(colDate));
                }

                try
                {
                    qty = Convert.ToDecimal(row.GetCell(colQty).NumericCellValue);
                }
                catch
                {
                    this.ThrowCommonError(row.RowNum, colQty, row.GetCell(colQty));
                }
                #endregion


                #endregion

                #region 填充数据
                Item item = itemMgr.CheckAndLoadItem(itemCode);
                Supplier supplier = supplierMgr.CheckAndLoadSupplier(supplierCode);
                if (date.DayOfWeek.ToString() != "Monday")
                {
                    throw new BusinessErrorException("Date is not Monday", row.GetCell(colDate).ToString());
                }

                RollingForecast rollingForecast = new RollingForecast();
                rollingForecast.Supplier = supplier;
                rollingForecast.Item = item;
                rollingForecast.Qty = qty;
                rollingForecast.CreateDate = DateTime.Now;
                rollingForecast.Date = date;
                rollingForecast.CreateUser = user;
                rollingForecastList.Add(rollingForecast);
                #endregion


            }

            if (rollingForecastList.Count == 0)
                throw new BusinessErrorException("Import.Result.Error.ImportNothing");

            return rollingForecastList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public OrderHead ReadBatchTransferFromXls(Stream inputStream, User user, string flowCode)
        {
            if (inputStream.Length == 0)
            {
                throw new BusinessErrorException("Import.Stream.Empty");
            }

            Flow flow = flowMgr.LoadFlow(flowCode);
            OrderHead orderHead = orderMgr.TransferFlow2Order(flow);
            orderHead.OrderDetails = new List<OrderDetail>();

            HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

            Sheet sheet = workbook.GetSheetAt(0);
            IEnumerator rows = sheet.GetRowEnumerator();

            ImportHelper.JumpRows(rows, 11);

            #region 列定义
            int colItem = 1;//物料代码
            int colUom = 3;//单位
            int colQty = 4;//数量
            int colHu = 5;//条码
            #endregion

            while (rows.MoveNext())
            {
                Row row = (HSSFRow)rows.Current;
                if (!this.CheckValidDataRow(row, 1, 6))
                {
                    break;//边界
                }

                if (!flow.IsShipScanHu)
                {
                    string itemCode = string.Empty;
                    decimal qty = 0;
                    string uomCode = string.Empty;

                    #region 读取数据
                    #region 读取物料代码
                    itemCode = row.GetCell(colItem) != null ? row.GetCell(colItem).StringCellValue : string.Empty;
                    if (itemCode == null || itemCode.Trim() == string.Empty)
                    {
                        this.ThrowCommonError(row.RowNum, colItem, row.GetCell(colItem));
                    }

                    var i = (
                      from f in flow.FlowDetails
                      where f.Item.Code.Trim().ToUpper() == itemCode.Trim().ToUpper()
                      select f).Count();

                    if (i == 0 && !flow.AllowCreateDetail)
                    {
                        throw new BusinessErrorException("MasterData.Flow.NotAllowCreateDetail", itemCode);
                    }

                    var j = (
                        from c in orderHead.OrderDetails
                        where c.HuId == null && c.Item.Code.Trim().ToUpper() == itemCode.Trim().ToUpper()
                        select c).Count();

                    if (j > 0)
                    {
                        throw new BusinessErrorException("Import.Business.Error.Duplicate", itemCode, (row.RowNum + 1).ToString(), (colItem + 1).ToString());
                    }
                    #endregion

                    #region 读取数量
                    try
                    {
                        qty = Convert.ToDecimal(row.GetCell(colQty).NumericCellValue);
                    }
                    catch
                    {
                        this.ThrowCommonError(row.RowNum, colQty, row.GetCell(colQty));
                    }
                    #endregion

                    #region 读取单位
                    uomCode = row.GetCell(colUom) != null ? row.GetCell(colUom).StringCellValue : string.Empty;
                    if (uomCode == null || uomCode.Trim() == string.Empty)
                    {
                        throw new BusinessErrorException("Import.Read.Error.Empty", (row.RowNum + 1).ToString(), colUom.ToString());
                    }
                    #endregion
                    #endregion

                    #region 填充数据
                    Item item = itemMgr.CheckAndLoadItem(itemCode);
                    Uom uom = uomMgr.CheckAndLoadUom(uomCode);
                    //单位换算
                    if (item.Uom.Code.Trim().ToUpper() != uom.Code.Trim().ToUpper())
                    {
                        qty = uomConversionMgr.ConvertUomQty(item, uom, qty, item.Uom);
                    }

                    OrderDetail newOrderDetail = new OrderDetail();

                    int seqInterval = int.Parse(entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);


                    if (orderHead.OrderDetails == null || orderHead.OrderDetails.Count == 0)
                    {
                        newOrderDetail.Sequence = seqInterval;
                    }
                    else
                    {
                        newOrderDetail.Sequence = orderHead.OrderDetails.Last<OrderDetail>().Sequence + seqInterval;
                    }
                    newOrderDetail.Item = item;
                    newOrderDetail.Uom = uom;
                    newOrderDetail.OrderedQty = qty;
                    newOrderDetail.RequiredQty = qty;
                    newOrderDetail.UnitCount = item.UnitCount;
                    newOrderDetail.LocationFrom = orderHead.LocationFrom;
                    newOrderDetail.LocationTo = orderHead.LocationTo;
                    orderHead.AddOrderDetail(newOrderDetail);

                    #endregion
                }
                else
                {
                    string huId = string.Empty;

                    #region 读取条码
                    huId = row.GetCell(colHu) != null ? row.GetCell(colHu).StringCellValue : string.Empty;
                    if (huId == null || huId.Trim() == string.Empty)
                    {
                        throw new BusinessErrorException("Import.Read.Error.Empty", (row.RowNum + 1).ToString(), colHu.ToString());
                    }

                    Hu hu = huMgr.CheckAndLoadHu(huId);

                    var i = (
                    from f in flow.FlowDetails
                    where f.Item.Code.Trim().ToUpper() == hu.Item.Code.Trim().ToUpper()
                    select f).Count();

                    if (i == 0 && !flow.AllowCreateDetail)
                    {
                        throw new BusinessErrorException("MasterData.Flow.NotAllowCreateDetail", hu.Item.Code);
                    }

                    var j = (
                        from c in orderHead.OrderDetails
                        where c.HuId != null && c.HuId.Trim().ToUpper() == huId.Trim().ToUpper()
                        select c).Count();

                    if (j > 0)
                        throw new BusinessErrorException("Import.Business.Error.Duplicate", huId, (row.RowNum + 1).ToString(), colHu.ToString());
                    #endregion


                    #region 填充数据


                    OrderDetail newOrderDetail = new OrderDetail();
                    newOrderDetail.IsScanHu = true;
                    int seqInterval = int.Parse(entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);

                    if (orderHead.OrderDetails == null || orderHead.OrderDetails.Count == 0)
                    {
                        newOrderDetail.Sequence = seqInterval;
                    }
                    else
                    {
                        newOrderDetail.Sequence = orderHead.OrderDetails.Last<OrderDetail>().Sequence + seqInterval;
                    }

                    newOrderDetail.Item = hu.Item;
                    newOrderDetail.Uom = hu.Uom;
                    newOrderDetail.HuId = hu.HuId;
                    newOrderDetail.HuQty = hu.Qty;
                    newOrderDetail.RequiredQty = hu.Qty;
                    newOrderDetail.OrderedQty = hu.Qty;
                    newOrderDetail.UnitCount = hu.UnitCount;
                    newOrderDetail.LocationFrom = orderHead.LocationFrom;
                    newOrderDetail.LocationTo = orderHead.LocationTo;
                    orderHead.AddOrderDetail(newOrderDetail);
                    #endregion
                }
            }

            if (orderHead.OrderDetails.Count == 0)
            {
                throw new BusinessErrorException("Import.Result.Error.ImportNothing");
            }
            return orderHead;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<CustomerSchedule> ReadCustomerScheduleFromXls(Stream inputStream, User user, DateTime? startDate, DateTime? endDate,
            string flowCode, string refScheduleNo, bool isItemRef)
        {
            if (inputStream.Length == 0)
            {
                throw new BusinessErrorException("Import.Stream.Empty");
            }

            HSSFWorkbook workbook = new HSSFWorkbook(inputStream);
            Sheet sheet = workbook.GetSheetAt(0);
            IEnumerator rows = sheet.GetRowEnumerator();

            #region 读取路线,参考日程号等
            Flow flow = null;
            decimal leadTime = 0M;
            IList<string> flowList = new List<string>();
            if (flowCode != null && flowCode.Trim() != string.Empty)
            {
                flow = flowMgr.CheckAndLoadFlow(flowCode, true, true);
                leadTime = flow.LeadTime.HasValue ? flow.LeadTime.Value : 0M;
                flowList.Add(flowCode);
            }

            Row typeRow = sheet.GetRow(5);
            Row dateRow = sheet.GetRow(6);

            //IList<CustomerSchedule> customerSchedules = customerScheduleMgr.GetCustomerSchedules(flowCode, refScheduleNo, null, null, null);
            //if (customerSchedules.Count > 0)
            //{
            //    throw new BusinessErrorException("MRP.Schedule.Import.CannotImportSameProductionSchedule");
            //}
            #endregion

            #region CustomerSchedule

            IList<CustomerScheduleDetail> customerScheduleDetaiList = new List<CustomerScheduleDetail>();
            #endregion

            ImportHelper.JumpRows(rows, 7);

            #region 列定义
            int colItemCode = 0;//物料代码或参考物料号
            // int colItemDescription = 1;//物料描述
            int colUom = 2;//单位
            int colUc = 3;//单包装
            #endregion

            while (rows.MoveNext())
            {
                Item item = null;
                Uom uom = null;
                decimal? uc = null;
                string itemReference = null;
                string location = null;
                Flow currentFlow = flow;
                string bom = null;

                Row row = (HSSFRow)rows.Current;
                if (!this.CheckValidDataRow(row, 0, 3))
                {
                    break;//边界
                }
                string rowIndex = (row.RowNum + 1).ToString();

                #region 读取物料代码
                try
                {
                    string itemCode = GetCellStringValue(row.GetCell(colItemCode));
                    if (itemCode == null)
                    {
                        throw new BusinessErrorException("MRP.Schedule.Import.ItemCode.Empty", rowIndex);
                    }
                    item = itemMgr.LoadItem(itemCode);
                    if (item == null)
                    {
                        throw new BusinessErrorException("MRP.Schedule.Import.Item.NotExist", rowIndex);
                    }

                    #region 找路线,先找生产，在找销售，找不到就悲剧
                    DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowDetail));
                    criteria.CreateAlias("Flow", "f");
                    criteria.Add(Expression.Eq("f.IsActive", true));
                    criteria.Add(Expression.Or(
                        Expression.And(Expression.Eq("f.FlowStrategy", BusinessConstants.CODE_MASTER_FLOW_STRATEGY_VALUE_MRP),
                        Expression.Eq("f.Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)),
                        Expression.Eq("f.Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)));
                    // criteria.Add(Expression.Eq("f.FlowStrategy", BusinessConstants.CODE_MASTER_FLOW_STRATEGY_VALUE_MRP));
                    criteria.Add(Expression.Eq("Item.Code", item.Code));
                    criteria.AddOrder(Order.Desc("f.Type"));
                    if (flow != null)
                    {
                        criteria.Add(Expression.Eq("f.Code", flow.Code));
                    }
                    IList<FlowDetail> flowDetailList = criteriaMgr.FindAll<FlowDetail>(criteria);
                    if (flowDetailList == null || flowDetailList.Count == 0)
                    {
                        throw new BusinessErrorException("MRP.Schedule.Import.Flow.NotExist", rowIndex);
                    }

                    currentFlow = flowDetailList[0].Flow;
                    location = flowDetailList[0].DefaultLocationFrom.Code;
                    leadTime = currentFlow.LeadTime.HasValue ? currentFlow.LeadTime.Value : 0M;
                    if (currentFlow.Type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
                    {
                        bom = flowDetailList[0].Bom != null ? flowDetailList[0].Bom.Code : null;
                    }
                    if (!flowList.Contains(currentFlow.Code))
                    {
                        flowList.Add(currentFlow.Code);
                    }
                    #endregion

                }
                catch (BusinessErrorException ex)
                {
                    throw ex;
                }
                #endregion

                #region 读取单位
                try
                {
                    string uomCode = GetCellStringValue(row.GetCell(colUom));
                    if (uomCode != null)
                    {
                        uom = uomMgr.CheckAndLoadUom(uomCode);
                    }
                }
                catch
                {
                    this.ThrowCommonError(row, colUom);
                }
                #endregion

                #region 读取单包装
                try
                {
                    string uc_ = GetCellStringValue(row.GetCell(colUc));
                    if (uc_ != null)
                    {
                        uc = Convert.ToDecimal(uc_);
                    }
                }
                catch
                {
                    this.ThrowCommonError(row, colUc);
                }
                #endregion

                #region 读取数量
                try
                {
                    for (int i = 4; ; i++)
                    {
                        string periodType = GetCellStringValue(typeRow.GetCell(i));

                        if (periodType != BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY &&
                            periodType != BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_MONTH &&
                            periodType != BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK)
                        {
                            break;
                        }
                        Cell dateCell = dateRow.GetCell(i);
                        DateTime? dateCellValue = null;
                        if (dateCell != null && dateCell.CellType == CellType.NUMERIC)
                        {
                            dateCellValue = dateCell.DateCellValue;
                        }
                        else
                        {
                            break;
                        }
                        if (startDate.HasValue && dateCellValue.Value.Date < startDate.Value.Date)
                        {
                            continue;
                        }
                        if (endDate.HasValue && dateCellValue.Value.Date > endDate.Value.Date)
                        {
                            break;
                        }
                        string qtyValue = GetCellStringValue(row.GetCell(i));
                        decimal qty = 0M;
                        if (qtyValue != null)
                        {
                            qty = Convert.ToDecimal(qtyValue);
                        }
                        if (qty < 0M)
                        {
                            throw new BusinessErrorException("MRP.Schedule.Import.Qty.MustGreatThanZero", rowIndex);
                        }
                        else
                        {
                            CustomerScheduleDetail customerScheduleDetail = new CustomerScheduleDetail();
                            customerScheduleDetail.DateFrom = DateTimeHelper.GetStartTime(periodType, dateCellValue.Value);
                            customerScheduleDetail.DateTo = DateTimeHelper.GetEndTime(periodType, dateCellValue.Value);
                            customerScheduleDetail.Item = item.Code;
                            customerScheduleDetail.ItemDescription = item.Description;
                            customerScheduleDetail.ItemReference = itemReference;
                            customerScheduleDetail.Location = location;
                            customerScheduleDetail.Type = periodType;
                            customerScheduleDetail.UnitCount = uc.Value;
                            customerScheduleDetail.Uom = uom.Code;
                            customerScheduleDetail.StartTime = customerScheduleDetail.DateFrom.AddHours(-(double)leadTime);
                            customerScheduleDetail.Qty = qty;
                            customerScheduleDetail.Flow = currentFlow.Code;
                            if (currentFlow.Type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
                            {
                                customerScheduleDetail.Bom = bom == null ? item.Code : bom;
                            }
                            customerScheduleDetaiList.Add(customerScheduleDetail);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new BusinessErrorException("MRP.Schedule.Import.Qty.Error", rowIndex);
                }
                #endregion
            }

            #region 建多个客户需求计划
            IList<CustomerSchedule> customerScheduleList = new List<CustomerSchedule>();
            if (flowList.Count > 0)
            {
                foreach (string f in flowList)
                {
                    CustomerSchedule cs = new CustomerSchedule();
                    cs.Flow = f;
                    cs.ReferenceScheduleNo = refScheduleNo;

                    var q = customerScheduleDetaiList.Where<CustomerScheduleDetail>(p => p.Flow == f);



                    var gCustomerScheduleDetails = from c in q.ToList()
                                                   group c by new
                                                   {
                                                       c.DateFrom,
                                                       c.DateTo,
                                                       c.Item,
                                                       c.ItemDescription,
                                                       c.ItemReference,
                                                       c.Location,
                                                       c.Type,
                                                       c.UnitCount,
                                                       c.Uom,
                                                       c.StartTime,
                                                       c.Flow,
                                                       c.Bom
                                                   } into customerScheduleDetail
                                                   select new CustomerScheduleDetail
                                        {

                                            DateFrom = customerScheduleDetail.Key.DateFrom,
                                            DateTo = customerScheduleDetail.Key.DateTo,
                                            Item = customerScheduleDetail.Key.Item,
                                            ItemDescription = customerScheduleDetail.Key.ItemDescription,
                                            ItemReference = customerScheduleDetail.Key.ItemReference,
                                            Location = customerScheduleDetail.Key.Location,
                                            Type = customerScheduleDetail.Key.Type,
                                            UnitCount = customerScheduleDetail.Key.UnitCount,
                                            Uom = customerScheduleDetail.Key.Uom,
                                            StartTime = customerScheduleDetail.Key.StartTime,
                                            Qty = customerScheduleDetail.Sum(p => p.Qty),
                                            Flow = customerScheduleDetail.Key.Flow,
                                            Bom = customerScheduleDetail.Key.Bom
                                        };
                    cs.CustomerScheduleDetails = gCustomerScheduleDetails.OrderBy(c => c.StartTime).ToList<CustomerScheduleDetail>();
                    customerScheduleList.Add(cs);
                }
            }

            #endregion

            return customerScheduleList;
        }


        #endregion

        #region Private Method
        private int GetPlanColumnIndexToRead(Row row, string shiftName, DateTime date)
        {
            int colIndex = -1;
            int startColIndex = 5; //从第5列开始

            int dayOfWeek = (int)date.DayOfWeek;
            if (dayOfWeek == 0)
                dayOfWeek = 7;

            startColIndex = startColIndex + (dayOfWeek - 1) * 6;
            for (int i = startColIndex; i < row.LastCellNum; i = i + 2)
            {
                Cell cell = row.GetCell(i);
                string cellValue = cell.StringCellValue;
                if (cellValue == shiftName)
                {
                    colIndex = i;
                    break;
                }
            }

            return colIndex;
        }

        private int GetColumnIndexToRead_ShipScheduleYFK(Row row, DateTime date)
        {
            int colIndex = -1;
            int startColIndex = 7; //从第7列开始

            for (int i = startColIndex; i < row.LastCellNum; i++)
            {
                Cell cell = row.GetCell(i);
                DateTime cellValue = cell.DateCellValue;
                if (DateTime.Compare(cellValue, date) == 0)
                {
                    colIndex = i;
                    break;
                }
            }

            return colIndex;
        }

        private bool CheckValidDataRow(Row row, int startColIndex, int endColIndex)
        {
            for (int i = startColIndex; i < endColIndex; i++)
            {
                Cell cell = row.GetCell(i);
                if (cell != null && cell.CellType != NPOI.SS.UserModel.CellType.BLANK)
                {
                    return true;
                }
            }

            return false;
        }

        private void ThrowCommonError(Row row, int colIndex)
        {
            this.ThrowCommonError(row.RowNum, colIndex, row.GetCell(colIndex));
        }
        private void ThrowCommonError(int rowIndex, int colIndex, Cell cell)
        {
            string errorValue = string.Empty;
            if (cell != null)
            {
                if (cell.CellType == NPOI.SS.UserModel.CellType.STRING)
                {
                    errorValue = cell.StringCellValue;
                }
                else if (cell.CellType == NPOI.SS.UserModel.CellType.NUMERIC)
                {
                    errorValue = cell.NumericCellValue.ToString("0.########");
                }
                else if (cell.CellType == NPOI.SS.UserModel.CellType.BOOLEAN)
                {
                    errorValue = cell.NumericCellValue.ToString();
                }
                else if (cell.CellType == NPOI.SS.UserModel.CellType.BLANK)
                {
                    errorValue = "Null";
                }
                else
                {
                    errorValue = "Unknow value";
                }
            }
            throw new BusinessErrorException("Import.Read.CommonError", (rowIndex + 1).ToString(), (colIndex + 1).ToString(), errorValue);
        }

        [Transaction(TransactionMode.Unspecified)]
        private FlowDetail LoadFlowDetailByFlow(string flowCode, string itemCode, decimal UC)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowView));
            criteria.CreateAlias("FlowDetail", "fd");
            criteria.Add(Expression.Eq("Flow.Code", flowCode));
            criteria.Add(Expression.Eq("fd.Item.Code", itemCode));
            IList<FlowView> flowViewList = criteriaMgr.FindAll<FlowView>(criteria);

            FlowDetail flowDetail = null;
            if (flowViewList != null && flowViewList.Count > 0)
            {
                var q1 = flowViewList.Where(f => f.FlowDetail.UnitCount == UC).Select(f => f.FlowDetail);
                if (q1.Count() > 0)
                {
                    flowDetail = q1.First();
                }
                else
                {
                    flowDetail = flowViewList[0].FlowDetail;
                }
            }

            return flowDetail;
        }

        private string GetCellStringValue(Cell cell)
        {
            string strValue = null;
            if (cell != null)
            {
                if (cell.CellType == CellType.STRING)
                {
                    strValue = cell.StringCellValue;
                }
                else if (cell.CellType == CellType.NUMERIC)
                {
                    strValue = cell.NumericCellValue.ToString("0.########");
                }
                else if (cell.CellType == CellType.BOOLEAN)
                {
                    strValue = cell.NumericCellValue.ToString();
                }
                else if (cell.CellType == CellType.FORMULA)
                {
                    if (cell.CachedFormulaResultType == CellType.STRING)
                    {
                        strValue = cell.StringCellValue;
                    }
                    else if (cell.CachedFormulaResultType == CellType.NUMERIC)
                    {
                        strValue = cell.NumericCellValue.ToString("0.########");
                    }
                    else if (cell.CachedFormulaResultType == CellType.BOOLEAN)
                    {
                        strValue = cell.NumericCellValue.ToString();
                    }
                }
            }
            if (strValue != null)
            {
                strValue = strValue.Trim();
            }
            strValue = strValue == string.Empty ? null : strValue;
            return strValue;
        }

        #endregion
    }
}
