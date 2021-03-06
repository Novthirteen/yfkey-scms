using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.EDI;
using com.Sconit.Persistence.Batch;
using com.Sconit.Service.EDI;
using System.IO;
using System.Linq;
using System.Diagnostics;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.MasterData.Impl;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MRP;

//TODO: Add other using statements here.

namespace com.Sconit.Service.EDI.Impl
{
    [Transactional]
    public class EDIMgr : SessionBase, IEDIMgr
    {
        private INumberControlMgr numberControlMgr;
        protected IGenericMgr genericMgr;
        protected IEntityPreferenceMgr entityPreferenceMgr;
        protected IFlowMgr flowMgr;
        protected IOrderMgr orderMgr;

        public EDIMgr(IGenericMgr genericMgr, INumberControlMgr numberControlMgr, IEntityPreferenceMgr entityPreferenceMgr, IFlowMgr flowMgr, IOrderMgr orderMgr)
        {
            this.genericMgr = genericMgr;
            this.numberControlMgr = numberControlMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
            this.flowMgr = flowMgr;
            this.orderMgr = orderMgr;
        }
        public void RunBat()
        {

            string batPath = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_BATPATH).Value;

            Process pro = new Process();

            FileInfo file = new FileInfo(batPath);

            pro.StartInfo.WorkingDirectory = file.Directory.FullName;

            pro.StartInfo.FileName = batPath;

            pro.StartInfo.CreateNoWindow = false;

            pro.Start();

            pro.WaitForExit();

        }

        [Transaction(TransactionMode.Requires)]
        public void LoadEDI(User user)
        {
            string sourceFilePath = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SOURCEFILEPATH).Value;
            string bakFilePath = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_BAKFILEPATH).Value;
            string errorFilePath = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_ERRORFILEPATH).Value;
            bool isTestSystem = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_ISTESTSYSTEM).Value == "1";
            //string sourceFilePath = "D:\\source";
            //string bakFilePath = "D:\\bak";
            //string errorFilePath = "D:\\error";
            this.ProcessPath(ref sourceFilePath);

            try
            {
                List<string> fileList = new List<string>();
                if (Directory.Exists(sourceFilePath))
                {

                    string [] paths = Directory.GetDirectories(sourceFilePath);
                    for (int i = 0; i < paths.Length; i++)
                    {
                        if (isTestSystem)
                        {
                            if ((paths[i]).Contains("F159E") && ((paths[i]).Contains("830") || (paths[i]).Contains("862")))
                            {
                                fileList.AddRange(Directory.GetFiles(paths[i]));
                            }
                        }
                        else
                        {
                            if ((paths[i]).Contains("F159B") && ((paths[i]).Contains("830") || (paths[i]).Contains("862")))
                            {
                                fileList.AddRange(Directory.GetFiles(paths[i]));
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("文件地址不存在");
                }
                if (fileList != null && fileList.Count > 0)
                {
                    #region 导入/备份
                    List<string> errorlogs = new List<string>();
                    var fileNames = fileList.OrderBy(f => f);
                    foreach (string filePath in fileNames)
                    {
                        string fileName = Path.GetFileName(filePath);
                        try
                        {
                            this.ProcessPath(ref bakFilePath);
                            if (!Directory.Exists(bakFilePath))
                            {
                                Directory.CreateDirectory(bakFilePath);
                            }
                            //string bakFile = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + fileName;

                            this.ImportEDIFile(filePath, fileName,user);
                            File.Move(filePath, Path.Combine(bakFilePath, fileName));
                        }
                        catch (Exception ex)
                        {
                            this.ProcessPath(ref errorFilePath);
                            //string errFile = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + fileName;
                            File.Move(filePath, Path.Combine(errorFilePath, fileName));
                            //SendErrorEmail("导入读取EDI文件时出错", ex);
                        }
                    }

                    #endregion
                }
                else
                {
                    //SendAlert(importMethod);
                    //log.Info("No files found to process.");
                }
            }
            catch (Exception ex)
            {
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void TransformationPlan(User user)
        {
            try
            {
                DateTime datetimeNow = System.DateTime.Now;
                #region  存入周计划
                string weeklySql = " select t from TEMP_FORD_EDI_830 as t where t.IsHandle=0 order by t.Id asc";
                IList<TEMP_FORD_EDI_830> weeklyPlans = this.genericMgr.FindAllWithCustomQuery<TEMP_FORD_EDI_830>(weeklySql);
                IList<EDIFordPlan> planList = new List<EDIFordPlan>();
                if (weeklyPlans != null && weeklyPlans.Count > 0)
                {
                    //IList<FlowDetail> flowdets = this.genericMgr.FindAllWithCustomQuery<FlowDetail>(string.Format(" select d from FlowDetail as d where  d.ReferenceItemCode in('{0}') ",string.Join("','", weeklyPlans.Select(w => w.Part_Num).Distinct().ToArray())));
                    //IList<ItemReference> itemReferences = this.genericMgr.FindAllWithCustomQuery<ItemReference>(string.Format(" select t from ItemReference as t where t.ReferenceCode in('{0}')", string.Join("','", weeklyPlans.Select(w => w.Part_Num).Distinct().ToArray())));
                    foreach (var weekly in weeklyPlans)
                    {
                        #region
                        //var flowDet = flowdets.Where(f => f.ReferenceItemCode == weekly.Part_Num);
                        //if (flowDet == null || flowDet.Count() == 0)
                        //{
                        //    throw new Exception("福特物料号找不到对应的明细。");
                        //}
                        //ItemReference itemReference = itemReferences.Where(i => i.ReferenceCode == weekly.Part_Num).First();
                        EDIFordPlan eDIFordPlan = new EDIFordPlan();
                        eDIFordPlan.TempId = weekly.Id;
                        eDIFordPlan.BatchNo = weekly.BatchNo;
                        eDIFordPlan.Control_Num = weekly.Interchange_Control_Num;
                        eDIFordPlan.SupplierCode = weekly.Ship_From_GSDB_Code;
                        eDIFordPlan.CustomerCode = weekly.Ship_To_GSDB_Code;
                        eDIFordPlan.ReleaseIssueDate =  DateTime.Parse(weekly.Message_Release_Date.Substring(0, 4) + "-" + weekly.Message_Release_Date.Substring(4, 2) + "-" + weekly.Message_Release_Date.Substring(6, 2));
                        eDIFordPlan.Item = string.Empty;
                        eDIFordPlan.ItemDesc = string.Empty;
                        eDIFordPlan.RefItem = weekly.Part_Num;
                        eDIFordPlan.Uom = weekly.UOM;
                        eDIFordPlan.LastShippedQuantity = string.IsNullOrEmpty(weekly.Last_Shipped_Qty) ? null : (decimal?)(Convert.ToDecimal(weekly.Last_Shipped_Qty));
                        eDIFordPlan.LastShippedCumulative = string.IsNullOrEmpty(weekly.Cum_Shipped_Qty) ? null : (decimal?)(Convert.ToDecimal(weekly.Cum_Shipped_Qty));
                        eDIFordPlan.LastShippedDate = string.IsNullOrEmpty(weekly.Last_Shipped_Date) ? null : (DateTime?)(DateTime.Parse(weekly.Last_Shipped_Date.Substring(0, 4) + "-" + weekly.Last_Shipped_Date.Substring(4, 2) + "-" + weekly.Last_Shipped_Date.Substring(6, 2)));
                        eDIFordPlan.DockCode = weekly.Dock_Code;
                        eDIFordPlan.LineFeed = weekly.Line_Feed;
                        eDIFordPlan.StorageLocation = weekly.Reserve_Line_Feed;
                        eDIFordPlan.IntermediateConsignee = weekly.Intermediate_Consignee;
                        eDIFordPlan.ForecastQty = string.IsNullOrEmpty(weekly.Forecast_Net_Qty) ? 0 : Convert.ToDecimal(weekly.Forecast_Net_Qty);
                        eDIFordPlan.ForecastCumQty = string.IsNullOrEmpty(weekly.Forecast_Cum_Qty) ? 0 : Convert.ToDecimal(weekly.Forecast_Cum_Qty);
                        eDIFordPlan.ForecastDate = string.IsNullOrEmpty(weekly.Forecast_Date) ? System.DateTime.Now.AddDays(-365) : DateTime.Parse(weekly.Forecast_Date.Substring(0, 4) + "-" + weekly.Forecast_Date.Substring(4, 2) + "-" + weekly.Forecast_Date.Substring(6, 2));
                        eDIFordPlan.CreateDate = datetimeNow;
                        eDIFordPlan.CreateUserName = user.Name;
                        eDIFordPlan.Type = weekly.Forecast_Date_Qual_r;
                        eDIFordPlan.PurchaseOrder = weekly.Purchase_Order_Num;
                        this.genericMgr.Create(eDIFordPlan);
                        planList.Add(eDIFordPlan);
                        //weekly.IsHandle = true    ;
                        //this.genericMgr.Update(weekly);
                        #endregion
                    }
                    string upIsHandleSql = string.Format("update TEMP_FORD_EDI_830 set IsHandle=1 where Interchange_Control_Num in ('{0}')",string.Join("','", weeklyPlans.Select(w=>w.Interchange_Control_Num).Distinct().ToArray()));
                    this.genericMgr.ExecuteSql(upIsHandleSql);

                    #region 周计划转客户需求
                    var groupWeeklys = weeklyPlans.GroupBy(w => w.Interchange_Control_Num).ToDictionary(d => d.Key, d => d.OrderBy(rd => rd.Forecast_Date).ToList());

                    foreach (var g in groupWeeklys)
                    {
                        string searchFlowCodeSql = string.Format("select Code from flowmstr where type='Distribution' and CustomerCodes like '%{0}%' and SupplierCodes like '%{1}%'", g.Value.First().Ship_To_GSDB_Code, g.Value.First().Ship_From_GSDB_Code);
                        var flowCodes = this.genericMgr.GetDatasetBySql(searchFlowCodeSql).Tables[0];
                        string flowCode = string.Empty;
                        foreach (System.Data.DataRow row in flowCodes.Rows)
                        {
                            flowCode = row[0].ToString();
                        }
                        foreach (var v in g.Value)
                        {
                            v.TempFlow = flowCode;
                        }
                    }
                    var groupWeeklyByFlow = weeklyPlans.GroupBy(w => w.TempFlow).ToDictionary(d => d.Key, d => d.OrderBy(rd => rd.Forecast_Date).ToList());
                    IList<CustomerScheduleDetail> tempDetList = new List<CustomerScheduleDetail>();
                    foreach (var g in groupWeeklyByFlow)
                    {
                        Flow currentFlow = this.flowMgr.LoadFlow(g.Key);
                        if (currentFlow == null) continue;
                        int customerPlanVersion = numberControlMgr.GenerateNumberNextSequence("CustomerPlan_" + currentFlow.Code + "_" + BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK);
                        var mrpLeadtime = currentFlow.MrpLeadTime.HasValue ? Convert.ToDouble(-currentFlow.MrpLeadTime.Value) : 0;
                        CustomerSchedule mstr = new CustomerSchedule
                        {
                            ReferenceScheduleNo = g.Key + "-Weekly-" + customerPlanVersion.ToString().PadLeft(3,'0'),
                            Flow = g.Key,
                            Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT,
                            Type = BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK,
                            CreateDate = datetimeNow,
                            CreateUser = user.Code,
                            LastModifyDate = datetimeNow,
                            LastModifyUser = user.Code,
                            ReleaseDate=  datetimeNow,
                            ReleaseUser=user.Code,
                            Version = customerPlanVersion,
                            ShipFlow = currentFlow.ShipFlow,
                        };
                        this.genericMgr.Create(mstr);
                        for (int i = 0; i < g.Value.Count; i++)
                        {
                            var r = g.Value[i];
                            var rn = g.Value[i + 1];
                            if (rn.Forecast_Date_Qual_r == "M")
                            {
                                for (int ii = i; ii < g.Value.Count; ii++)
                                {
                                    var r1 = g.Value[ii];
                                    var sDate = DateTime.Parse(r1.Forecast_Date.Substring(0, 4) + "-" + r1.Forecast_Date.Substring(4, 2) + "-" + r1.Forecast_Date.Substring(6, 2));
                                    var firstDate = DateTime.Parse(r1.Forecast_Date.Substring(0, 4) + "-" + r1.Forecast_Date.Substring(4, 2) + "-" + r1.Forecast_Date.Substring(6, 2));
                                    var year = sDate.Date.Year;
                                    var month = sDate.Date.Month;
                                    if (sDate.Date.Month == 12)
                                    {
                                        year = sDate.Date.Year + 1;
                                        month = 0;
                                    }
                                    var eDate = new DateTime(year, month + 1, 1).AddDays(-1);
                                    TimeSpan ts = eDate - sDate;
                                    int sub = ts.Days;
                                    int surplusWeekly = sub % 7 > 4 ? sub / 7 + 1 : sub / 7;
                                    if (surplusWeekly == 0)
                                    {
                                        if (sDate.Date.Month == 11)
                                        {
                                            year = sDate.Date.Year + 1;
                                            month = -1;
                                        }
                                        else if (sDate.Date.Month == 12)
                                        {
                                            year = sDate.Date.Year + 1;
                                            month = 0;
                                        }
                                        eDate = new DateTime(year, month + 2, 1).AddDays(-1);
                                        ts = eDate - sDate;
                                        sub = ts.Days;
                                        surplusWeekly = sub % 7 > 4 ? sub / 7 + 1 : sub / 7;
                                    }
                                    var averageQty = Convert.ToDecimal(r.Forecast_Net_Qty) / surplusWeekly;
                                    for (int j = 0; j < surplusWeekly; j++)
                                    {
                                        var cFlowDets = currentFlow.FlowDetails.Where(d => d.ReferenceItemCode == r1.Part_Num);
                                        FlowDetail fdet = cFlowDets != null && cFlowDets.Count() > 0 ? cFlowDets.First() : new FlowDetail();
                                        if (fdet.Item != null)
                                        {
                                            #region
                                            CustomerScheduleDetail cdet = new CustomerScheduleDetail();
                                            cdet.CustomerSchedule = mstr;
                                            cdet.Item = fdet.Item.Code;
                                            cdet.ItemDescription = fdet.Item.Description;
                                            cdet.ItemReference = r1.Part_Num;
                                            cdet.Type = BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK;
                                            cdet.Version = mstr.Version;
                                            cdet.DateFrom = sDate;
                                            cdet.DateTo = sDate.AddDays(7);
                                            cdet.Uom = fdet.Uom.Code;
                                            cdet.UnitCount = fdet.UnitCount;
                                            cdet.Qty = averageQty < 0 ? 0 : averageQty;
                                            cdet.Location = currentFlow.LocationFrom != null ? currentFlow.LocationFrom.Code : string.Empty;
                                            cdet.StartTime = sDate.AddDays(-mrpLeadtime).Date;
                                            cdet.Flow = mstr.Flow;
                                            cdet.ReferenceScheduleNo = mstr.ReferenceScheduleNo;
                                            cdet.ShipFlow = mstr.ShipFlow;
                                            cdet.ShipQty = 0;
                                            cdet.FordPlanId = planList.FirstOrDefault(d => d.RefItem == r1.Part_Num && d.Control_Num == r1.Interchange_Control_Num && d.ForecastDate == firstDate).Id;
                                            //this.genericMgr.Create(cdet);
                                            tempDetList.Add(cdet);
                                            sDate = sDate.AddDays(7);
                                            #endregion
                                        }
                                    }
                                }
                                break;
                            }
                            var currentFlowDets = currentFlow.FlowDetails.Where(d => d.ReferenceItemCode == r.Part_Num);
                            FlowDetail flowdet = currentFlowDets != null && currentFlowDets.Count() > 0 ? currentFlowDets.First() : new FlowDetail();
                            if (flowdet.Item != null)
                            {
                                #region
                                CustomerScheduleDetail det = new CustomerScheduleDetail();
                                det.CustomerSchedule = mstr;
                                det.Item = flowdet.Item != null ? flowdet.Item.Code : string.Empty;
                                det.ItemDescription = flowdet.Item != null ? flowdet.Item.Description : string.Empty;
                                det.ItemReference = r.Part_Num;
                                //det.Bom = null;
                                det.Type = BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK;
                                det.DateFrom = string.IsNullOrEmpty(r.Forecast_Date) ? System.DateTime.Now.AddDays(-365) : DateTime.Parse(r.Forecast_Date.Substring(0, 4) + "-" + r.Forecast_Date.Substring(4, 2) + "-" + r.Forecast_Date.Substring(6, 2));
                                det.DateTo = det.DateFrom.AddDays(7);
                                det.Uom = flowdet.Uom.Code;
                                det.UnitCount = flowdet.UnitCount;
                                det.Qty = string.IsNullOrEmpty(r.Forecast_Net_Qty) ? 0 : Convert.ToDecimal(r.Forecast_Net_Qty) < 0 ? 0 : Convert.ToDecimal(r.Forecast_Net_Qty);
                                det.Location = currentFlow.LocationFrom != null ? currentFlow.LocationFrom.Code : string.Empty;
                                //det.StartTime = det.DateFrom;
                                det.StartTime = det.DateFrom.AddDays(-mrpLeadtime).Date;
                                det.Version = mstr.Version;
                                det.Flow = mstr.Flow;
                                det.ReferenceScheduleNo = mstr.ReferenceScheduleNo;
                                det.ShipFlow = mstr.ShipFlow;
                                det.ShipQty = 0;
                                det.FordPlanId = planList.FirstOrDefault(d => d.RefItem == r.Part_Num && d.Control_Num == r.Interchange_Control_Num && d.ForecastDate==det.DateFrom).Id;
                                //this.genericMgr.Create(det);
                                tempDetList.Add(det);
                                #endregion
                            }
                        }
                        if(tempDetList.Count>0){
                            var groupbyItem = tempDetList.GroupBy(d => new { d.Item, d.DateFrom });
                            foreach (var gi in groupbyItem)
                            {
                                var det = gi.First();
                                det.Qty = gi.Sum(gd => gd.Qty);
                                this.genericMgr.Create(det);
                            }
                            tempDetList.Clear();
                        }

                    }
                    #endregion
                }
                #endregion

                #region  存入天计划
                planList.Clear();
                //planList = new List<EDIFordPlan>();
                string dailySql = " select t from TEMP_FORD_EDI_862 as t where t.IsHandle=0 order by t.Id asc";
                IList<TEMP_FORD_EDI_862> dailyPlans = this.genericMgr.FindAllWithCustomQuery<TEMP_FORD_EDI_862>(dailySql);
                if (dailyPlans != null && dailyPlans.Count > 0)
                {
                    //IList<FlowDetail> flowdets = this.genericMgr.FindAllWithCustomQuery<FlowDetail>(string.Format(" select d from FlowDetail as d where exists( select 1 from Flow as m where m.Code=d.Flow and m.Description like '%福特%' and m.IsActive=1) and d.ReferenceItemCode in('{0}') ", string.Join("','", dailyPlans.Select(w => w.Part_Num).Distinct().ToArray())));
                    //IList<FlowDetail> flowdets = this.genericMgr.FindAllWithCustomQuery<FlowDetail>(string.Format(" select d from FlowDetail as d where d.ReferenceItemCode in('{0}') ", string.Join("','", dailyPlans.Select(w => w.Part_Num).Distinct().ToArray())));
                    //IList<ItemReference> itemReferences = this.genericMgr.FindAllWithCustomQuery<ItemReference>(string.Format(" select t from ItemReference as t where t.ReferenceCode in('{0}')", string.Join("','", dailyPlans.Select(w => w.Part_Num).Distinct().ToArray())));
                    foreach (var daily in dailyPlans)
                    {
                        #region
                        //ItemReference itemReference = itemReferences.Where(i => i.ReferenceCode == daily.Part_Num).First();
                        //var flowDet = flowdets.Where(f => f.ReferenceItemCode == daily.Part_Num);
                        //if (flowDet == null || flowDet.Count() == 0)
                        //{
                        //    throw new Exception("福特物料号找不到对应的明细。");
                        //}
                        EDIFordPlan eDIFordPlan = new EDIFordPlan();
                        eDIFordPlan.TempId = daily.Id;
                        eDIFordPlan.BatchNo = daily.BatchNo;
                        eDIFordPlan.Control_Num = daily.Interchange_Control_Num;
                        eDIFordPlan.SupplierCode = daily.Ship_From_GSDB_Code;
                        eDIFordPlan.CustomerCode = daily.Ship_To_GSDB_Code;
                        eDIFordPlan.ReleaseIssueDate = DateTime.Parse(daily.Message_Release_Date.Substring(0, 4) + "-" + daily.Message_Release_Date.Substring(4, 2) + "-" + daily.Message_Release_Date.Substring(6, 2));
                        //eDIFordPlan.Item = flowDet.First().Item.Code;
                        //eDIFordPlan.ItemDesc = flowDet.First().Item.Desc1;
                        //eDIFordPlan.RefItem = flowDet.First().ReferenceItemCode;
                        eDIFordPlan.Item = string.Empty;
                        eDIFordPlan.ItemDesc = string.Empty;
                        eDIFordPlan.RefItem = daily.Part_Num;
                        eDIFordPlan.Uom = daily.UOM;
                        eDIFordPlan.LastShippedQuantity = string.IsNullOrEmpty(daily.Last_Shipped_Qty) ? null : (decimal?)(Convert.ToDecimal(daily.Last_Shipped_Qty));
                        eDIFordPlan.LastShippedCumulative = string.IsNullOrEmpty(daily.Cum_Shipped_Qty) ? null : (decimal?)(Convert.ToDecimal(daily.Cum_Shipped_Qty));
                        eDIFordPlan.LastShippedDate = string.IsNullOrEmpty(daily.Last_Shipped_Date) ? null : (DateTime?)(DateTime.Parse(daily.Last_Shipped_Date.Substring(0, 4) + "-" + daily.Last_Shipped_Date.Substring(4, 2) + "-" + daily.Last_Shipped_Date.Substring(6, 2)));
                        eDIFordPlan.DockCode = daily.Dock_Code;
                        eDIFordPlan.LineFeed = daily.Line_Feed;
                        eDIFordPlan.StorageLocation = daily.Reserve_Line_Feed;
                        eDIFordPlan.IntermediateConsignee = daily.Intermediate_Consignee;
                        eDIFordPlan.ForecastQty = string.IsNullOrEmpty(daily.Forecast_Net_Qty) ? 0 : Convert.ToDecimal(daily.Forecast_Net_Qty);
                        eDIFordPlan.ForecastCumQty = string.IsNullOrEmpty(daily.Forecast_Cum_Qty) ? 0 : Convert.ToDecimal(daily.Forecast_Cum_Qty);
                        eDIFordPlan.ForecastDate = string.IsNullOrEmpty(daily.Forecast_Date) ? System.DateTime.Now.AddDays(-365) : DateTime.Parse(daily.Forecast_Date.Substring(0, 4) + "-" + daily.Forecast_Date.Substring(4, 2) + "-" + daily.Forecast_Date.Substring(6, 2));
                        eDIFordPlan.CreateDate = datetimeNow;
                        eDIFordPlan.CreateUserName = user.Name;
                        eDIFordPlan.Type = "D";
                        eDIFordPlan.PurchaseOrder = daily.Purchase_Order_Num;
                        this.genericMgr.Create(eDIFordPlan);
                        planList.Add(eDIFordPlan);
                        //daily.IsHandle = true;
                        //this.genericMgr.Update(daily);
                        #endregion
                    }
                    string upIsHandleSql = string.Format("update TEMP_FORD_EDI_862 set IsHandle=1 where Interchange_Control_Num in ('{0}')", string.Join("','", dailyPlans.Select(w => w.Interchange_Control_Num).Distinct().ToArray()));
                    this.genericMgr.ExecuteSql(upIsHandleSql);

                    
                    #region   天计划转客户需求
                    var groupDailyPlan = dailyPlans.GroupBy(w => w.Interchange_Control_Num).ToDictionary(d => d.Key, d => d.ToList());

                    foreach (var g in groupDailyPlan)
                    {
                        string searchFlowCodeSql = string.Format("select Code from flowmstr where type='Distribution' and CustomerCodes like '%{0}%' and SupplierCodes like '%{1}%'", g.Value.First().Ship_To_GSDB_Code, g.Value.First().Ship_From_GSDB_Code);
                        var flowCodes = this.genericMgr.GetDatasetBySql(searchFlowCodeSql).Tables[0];
                        string flowCode = string.Empty;
                        foreach (System.Data.DataRow row in flowCodes.Rows)
                        {
                            flowCode = row[0].ToString();
                        }
                        foreach (var v in g.Value)
                        {
                            v.TempFlow = flowCode;
                        }
                    }
                    var groupdailyByFlow = dailyPlans.GroupBy(w => w.TempFlow).ToDictionary(d => d.Key, d => d.OrderBy(rd => rd.Forecast_Date).ToList());

                    IList<CustomerScheduleDetail> tempDetList = new List<CustomerScheduleDetail>();
                    foreach (var g in groupdailyByFlow)
                    {
                        Flow currentFlow = this.flowMgr.LoadFlow(g.Key);
                        if (currentFlow == null) continue;
                        int customerPlanVersion = numberControlMgr.GenerateNumberNextSequence("CustomerPlan_" + g.Key + "_" + BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK);
                        var firstWeek = g.Value.First();
                        CustomerSchedule mstr = new CustomerSchedule
                        {
                            ReferenceScheduleNo = g.Key + "-Daily-" + customerPlanVersion.ToString().PadLeft(3,'0'),
                            Flow = g.Key,
                            Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT,
                            Type = BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY,
                            CreateDate = datetimeNow,
                            CreateUser = user.Code,
                            LastModifyDate = datetimeNow,
                            LastModifyUser = user.Code,
                            ReleaseDate = datetimeNow,
                            ReleaseUser = user.Code,
                            Version = customerPlanVersion,
                            ShipFlow = currentFlow.ShipFlow,
                        };
                        this.genericMgr.Create(mstr);
                        foreach (var r in g.Value)
                        {
                            var currentFlowDets = currentFlow.FlowDetails.Where(d => d.ReferenceItemCode == r.Part_Num);
                            FlowDetail flowdet = currentFlowDets != null && currentFlowDets.Count() > 0 ? currentFlowDets.First() : new FlowDetail();
                            var mrpLeadtime=currentFlow.MrpLeadTime.HasValue?  Convert.ToDouble(-currentFlow.MrpLeadTime.Value):0;
                            if (flowdet.Item != null)
                            {
                                CustomerScheduleDetail det = new CustomerScheduleDetail();
                                det.CustomerSchedule = mstr;
                                det.Item = flowdet.Item != null ? flowdet.Item.Code : string.Empty;
                                det.ItemDescription = flowdet.Item != null ? flowdet.Item.Description : string.Empty;
                                det.ItemReference = r.Part_Num;
                                //det.Bom = null;
                                det.Type = BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY;
                                det.DateFrom = string.IsNullOrEmpty(r.Forecast_Date) ? System.DateTime.Now.AddDays(-365) : DateTime.Parse(r.Forecast_Date.Substring(0, 4) + "-" + r.Forecast_Date.Substring(4, 2) + "-" + r.Forecast_Date.Substring(6, 2));
                                det.DateTo = string.IsNullOrEmpty(r.Forecast_Date) ? System.DateTime.Now.AddDays(-365) : DateTime.Parse(r.Forecast_Date.Substring(0, 4) + "-" + r.Forecast_Date.Substring(4, 2) + "-" + r.Forecast_Date.Substring(6, 2));
                                det.Uom = flowdet.Uom.Code;
                                det.UnitCount = flowdet.UnitCount;
                                det.Qty = string.IsNullOrEmpty(r.Forecast_Net_Qty) ? 0 : Convert.ToDecimal(r.Forecast_Net_Qty) < 0 ? 0 : Convert.ToDecimal(r.Forecast_Net_Qty);
                                det.Location = currentFlow.LocationFrom != null ? currentFlow.LocationFrom.Code : string.Empty;
                                det.StartTime = det.DateFrom.AddDays(-mrpLeadtime).Date;
                                det.Version = mstr.Version;
                                det.Flow = mstr.Flow;
                                det.ReferenceScheduleNo = mstr.ReferenceScheduleNo;
                                det.ShipFlow = mstr.ShipFlow;
                                det.ShipQty = 0;
                                det.FordPlanId = planList.FirstOrDefault(d => d.RefItem == r.Part_Num && d.Control_Num == r.Interchange_Control_Num && d.ForecastDate==det.DateFrom).Id;
                                //this.genericMgr.Create(det);
                                tempDetList.Add(det);
                            }
                        }
                        if (tempDetList.Count > 0)
                        {
                            var groupbyItem = tempDetList.GroupBy(d => new { d.Item,d.DateFrom});
                            foreach (var gi in groupbyItem)
                            {
                                var det = gi.First();
                                det.Qty = gi.Sum(gd => gd.Qty);
                                this.genericMgr.Create(det);
                            }
                            tempDetList.Clear();
                        }
                    }
                    #endregion
                }
                #endregion
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void ShipEDIFordPlan(List<EDIFordPlan> shipEDIFordPlanList, string currentUserCode,Flow currentFlow)
        {
            try
            {
                if (shipEDIFordPlanList == null || shipEDIFordPlanList.Count == 0)
                {
                    throw new Exception("订单明细不能为空。");
                }
                bool isTestSystem = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_ISTESTSYSTEM).Value == "1";

                currentFlow.FlowDetails = (from det in currentFlow.FlowDetails
                                          where shipEDIFordPlanList.Select(s => s.Item).Contains(det.Item.Code) 
                                          select det).ToList();

                #region   路线明细
                IList<FlowDetail> flowDets = new List<FlowDetail>();
                var groupsByItem=currentFlow.FlowDetails.GroupBy(f => f.Item.Code).ToDictionary(d=>d.Key,d=>d.ToList());
                foreach (var g in groupsByItem)
                {
                    flowDets.Add(g.Value.First());
                }
                currentFlow.FlowDetails = flowDets;
                #endregion

                #region 新建订单
                OrderHead orderHead = orderMgr.TransferFlow2Order(currentFlow);
                orderHead.SubType = "Nml";
                orderHead.WindowTime = System.DateTime.Now;
                orderHead.Priority = "Normal";
                orderHead.Type = "Distribution";
                orderHead.StartTime = System.DateTime.Now;
                orderHead.IsAutoRelease = true;
                orderHead.IsAutoShip = true;
                orderHead.IsAutoStart = true;


                foreach (var orderDet in orderHead.OrderDetails)
                {
                    EDIFordPlan ediFordPlan = shipEDIFordPlanList.Where(s => s.Item == orderDet.Item.Code).First();
                    orderDet.RequiredQty = ediFordPlan.ShipQty.Value;
                    orderDet.OrderedQty = ediFordPlan.ShipQty.Value;
                }
                orderMgr.CreateOrder(orderHead, currentUserCode);

                #endregion
                this.genericMgr.FlushSession();
                #region    插入中间表
                InProcessLocation ipMaster = this.genericMgr.FindAllWithCustomQuery<InProcessLocationDetail>(" select i from InProcessLocationDetail as i where i.OrderLocationTransaction=? "
                    , orderHead.OrderDetails.First().OrderLocationTransactions.First().Id).First().InProcessLocation;
                int batch = numberControlMgr.GenerateNumberNextSequence(BusinessConstants.CODE_PREFIX_IMPORT_TEMPEDI856);
                foreach (var orderDet in orderHead.OrderDetails)
                {
                    EDIFordPlan ediFordPlan = shipEDIFordPlanList.Where(s => s.Item == orderDet.Item.Code).First();
                    TEMP_FORD_EDI_856 temp_FORD_EDI_856 = new TEMP_FORD_EDI_856();
                    temp_FORD_EDI_856.Message_Type_Code = "856";
                    temp_FORD_EDI_856.Message_Type = "FORDCSVFLAT";
                    temp_FORD_EDI_856.ReleaseVersion = "1";
                    temp_FORD_EDI_856.Receiver_ID = isTestSystem ? "ZZ:F159E" : "ZZ:F159B";
                    temp_FORD_EDI_856.Sender_ID = "ZZ:" + ediFordPlan.SupplierCode;
                    temp_FORD_EDI_856.BatchNo = batch;

                    temp_FORD_EDI_856.Interchange_Control_Num = ediFordPlan.Control_Num;
                    temp_FORD_EDI_856.ASN_Creation_DateTime = ipMaster.CreateDate.ToString("yyyyMMdd") + "-" + ipMaster.CreateDate.ToString("HHmm");
                    temp_FORD_EDI_856.Ship_To_GSDB_Code = ediFordPlan.CustomerCode;
                    temp_FORD_EDI_856.Ship_From_GSDB_Code = ediFordPlan.SupplierCode;
                    temp_FORD_EDI_856.Intermediate_Consignee_Code = ediFordPlan.IntermediateConsignee;
                    temp_FORD_EDI_856.Message_Purpose_Code = ediFordPlan.Purpose;
                    temp_FORD_EDI_856.Shipment_ID = ediFordPlan.ShipmentID;
                    temp_FORD_EDI_856.AsnNo = ipMaster.IpNo;
                    temp_FORD_EDI_856.Shipped_DateTime = ipMaster.CreateDate.ToString("yyyyMMdd") + "-" + ipMaster.CreateDate.ToString("HHmm");
                    temp_FORD_EDI_856.Gross_Weight = ediFordPlan.GrossWeight.ToString();
                    temp_FORD_EDI_856.Net_Weight = ediFordPlan.NetWeight.ToString();
                    temp_FORD_EDI_856.UOM = ediFordPlan.WeightUom;
                    temp_FORD_EDI_856.Packaging_Type_Code = ediFordPlan.OutPackType;
                    temp_FORD_EDI_856.Lading_Qty = ediFordPlan.OutPackQty.HasValue ? ediFordPlan.OutPackQty.Value.ToString() : string.Empty;
                    temp_FORD_EDI_856.Carrier_SCAC_Code = ediFordPlan.CarrierCode;
                    temp_FORD_EDI_856.Transportation_Method_Code = ediFordPlan.TransportationMethod;
                    temp_FORD_EDI_856.Equipment_Desc_Code = ediFordPlan.EquipmentDesc;
                    temp_FORD_EDI_856.Equipment_Num = ediFordPlan.EquipmentNum;
                    temp_FORD_EDI_856.LadingNum = ediFordPlan.LadingNum;
                    temp_FORD_EDI_856.Part_Num = ediFordPlan.RefItem;
                    temp_FORD_EDI_856.Purchase_Order_Num = ediFordPlan.PurchaseOrder;
                    temp_FORD_EDI_856.Shipped_Qty = ediFordPlan.ShipQty.ToString();
                    temp_FORD_EDI_856.Cum_Shipped_Qty = ediFordPlan.LastShippedCumulative.HasValue ? (ediFordPlan.LastShippedCumulative.Value + ediFordPlan.CurrenCumQty + ediFordPlan.ShipQty.Value).ToString() : (ediFordPlan.CurrenCumQty + ediFordPlan.ShipQty.Value).ToString();
                    temp_FORD_EDI_856.Cum_Shipped_UOM = ediFordPlan.Uom;
                    temp_FORD_EDI_856.Number_of_Loads = ediFordPlan.InPackQty.HasValue ? ediFordPlan.InPackQty.ToString() : string.Empty;  // 包装个数
                    temp_FORD_EDI_856.Qty_Per_Load = ediFordPlan.PerLoadQty.HasValue ? ediFordPlan.PerLoadQty.Value.ToString() : string.Empty;  // 单箱件数
                    temp_FORD_EDI_856.Packaging_Code = ediFordPlan.InPackType;
                    temp_FORD_EDI_856.Airport_Code = ediFordPlan.AirportCode;
                    temp_FORD_EDI_856.CreateDate = System.DateTime.Now;
                    temp_FORD_EDI_856.CreateUserName = "";
                    temp_FORD_EDI_856.IsHandle = false;
                    temp_FORD_EDI_856.ReadFileName = string.Empty;
                    this.genericMgr.Create(temp_FORD_EDI_856);
                    if (ediFordPlan.Id > 0)
                    {
                        ediFordPlan.CurrenCumQty += ediFordPlan.ShipQty.Value;
                        ediFordPlan.Item = string.Empty;
                        ediFordPlan.ItemDesc = string.Empty;
                        this.genericMgr.Update(ediFordPlan);
                    }
                }
                #endregion

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void ReadEDIFordPlanASN()
        {
            string archiveFolder = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_ARCHIVEFOLDER).Value;
            string tempFolder = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_TEMPFOLDER).Value;
            string outFolder = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_OUTFOLDER).Value;
            bool isTestSystem = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_ISTESTSYSTEM).Value == "1";
            
            #region 初始化本地目录
            outFolder = outFolder.Replace("\\", "/");
            if (isTestSystem)
            {
                outFolder += "/EP4TA-FORD-F159E";
            }
            else
            {
                outFolder += "/EP4TA-FORD-F159B";
            }
            if (!outFolder.EndsWith("/"))
            {
                outFolder += "/";
            }

            if (!Directory.Exists(outFolder))
            {
                Directory.CreateDirectory(outFolder);
            }

            archiveFolder = archiveFolder.Replace("\\", "/");
            if (!archiveFolder.EndsWith("/"))
            {
                archiveFolder += "/";
            }

            if (!Directory.Exists(archiveFolder))
            {
                Directory.CreateDirectory(archiveFolder);
            }

            tempFolder = tempFolder.Replace("\\", "/");
            if (!tempFolder.EndsWith("/"))
            {
                tempFolder += "/";
            }

            if (Directory.Exists(tempFolder))
            {
                var d = new DirectoryInfo(tempFolder);
                //则删除此目录、其子目录以及所有文件
                d.Delete(true);
                //Directory.Delete(tempFolder);
            }
            Directory.CreateDirectory(tempFolder);
            #endregion
            Random r = new Random();
            IList<TEMP_FORD_EDI_856> temp_FORD_EDI_856List = this.genericMgr.FindAllWithCustomQuery<TEMP_FORD_EDI_856>(" select t from TEMP_FORD_EDI_856 as t where t.IsHandle=0 order by BatchNo asc ");
            if (temp_FORD_EDI_856List != null && temp_FORD_EDI_856List.Count > 0)
            {
                var groups = (from tak in temp_FORD_EDI_856List
                              group tak by tak.BatchNo into result
                              select new
                              {
                                  BatchNo = result.Key,
                                  List = result.ToList()
                              });
                foreach (var g in groups)
                {
                    string fileName = "ASN" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + (g.BatchNo) + ".csv";
                    List<List<string>> writeList = new List<List<string>>();
                    int i=0;
                    foreach (var l in g.List)
                    {
                        if(i==0)
                        {
                            #region    头文件
                            List<string> titleLine = new List<string>();
                            titleLine.Add(l.Message_Type_Code);
                            titleLine.Add(l.Message_Type);
                            titleLine.Add(l.ReleaseVersion);
                            titleLine.Add(l.Receiver_ID);
                            titleLine.Add(l.Sender_ID);
                            writeList.Add(titleLine);

                            titleLine = new List<string>();
                            titleLine.Add("Interchange Control Num");
                            titleLine.Add("ASN Creation DateTime");
                            titleLine.Add("Ship To GSDB Code");
                            titleLine.Add("Ship From GSDB Code");
                            titleLine.Add("Intermediate Consignee Code");
                            titleLine.Add("Message Purpose Code");
                            titleLine.Add("Shipment ID");
                            titleLine.Add("Shipped DateTime");
                            titleLine.Add("Gross Weight");
                            titleLine.Add("Net Weight");
                            titleLine.Add("UOM");
                            titleLine.Add("Packaging Type Code");
                            titleLine.Add("Lading Qty");
                            titleLine.Add("Carrier SCAC Code");
                            titleLine.Add("Transportation Method Code");
                            titleLine.Add("Equipment Desc Code");
                            titleLine.Add("Equipment Num");
                            titleLine.Add("Bill of Lading Num");
                            titleLine.Add("Part Num");
                            titleLine.Add("Purchase Order Num");
                            titleLine.Add("Shipped Qty");
                            titleLine.Add("Cum Shipped Qty");
                            titleLine.Add("UOM");
                            titleLine.Add("Number of Loads");
                            titleLine.Add("Qty Per Load");
                            titleLine.Add("Packaging Type Code");
                            titleLine.Add("Airport Code");
                            writeList.Add(titleLine);
                            #endregion
                        }

                        List<string> writeLine = new List<string>();
                        writeLine.Add( l.BatchNo.ToString().PadLeft(5,'0'));
                        writeLine.Add(l.ASN_Creation_DateTime);
                        writeLine.Add(l.Ship_To_GSDB_Code);
                        writeLine.Add(l.Ship_From_GSDB_Code);
                        writeLine.Add(l.Intermediate_Consignee_Code);
                        writeLine.Add(l.Message_Purpose_Code);
                        writeLine.Add(l.Shipment_ID);
                        writeLine.Add(l.Shipped_DateTime);
                        writeLine.Add(l.Gross_Weight);
                        writeLine.Add(l.Net_Weight);
                        writeLine.Add(l.UOM);
                        writeLine.Add(l.Packaging_Type_Code);
                        writeLine.Add(l.Lading_Qty);
                        writeLine.Add(l.Carrier_SCAC_Code);
                        writeLine.Add(l.Transportation_Method_Code);
                        writeLine.Add(l.Equipment_Desc_Code);
                        writeLine.Add(l.Equipment_Num);
                        writeLine.Add(l.LadingNum);
                        writeLine.Add(l.Part_Num);
                        writeLine.Add(l.Purchase_Order_Num);
                        writeLine.Add(l.Shipped_Qty);
                        writeLine.Add(l.Cum_Shipped_Qty);
                        writeLine.Add(l.Cum_Shipped_UOM);
                        writeLine.Add(l.Number_of_Loads);
                        writeLine.Add(l.Qty_Per_Load);
                        writeLine.Add(l.Packaging_Code);
                        writeLine.Add(l.Airport_Code);
                        writeList.Add(writeLine);
                        l.IsHandle = true;
                        l.ReadFileName = fileName;
                        this.genericMgr.Update(l);
                        i++;
                    }
                    WriteCSV(tempFolder + fileName, false, writeList);
                    File.Copy(tempFolder + fileName, archiveFolder + fileName);  //备份目录
                    File.Move(tempFolder + fileName, outFolder + fileName);     //导出目录
                }
            }
        }


        private void WriteCSV(string filePathName, bool append, List<List<string>> writeList)
        {
            StreamWriter fileWriter=new StreamWriter(filePathName,append,Encoding.Default);
            foreach (List<string> strArr in writeList)
            {
                fileWriter.WriteLine(string.Join (",",strArr.ToArray()) );
            }
            fileWriter.Flush();
            fileWriter.Close();
        }


        public static List<String[]> ReadCSV(string filePathName)
        {
            List<String[]> ls = new List<String[]>();
            StreamReader fileReader=new   StreamReader(filePathName);  
            string strLine="";
            while (strLine != null)
            {
                strLine = fileReader.ReadLine();
                if (strLine != null && strLine.Length>0)
                {
                    ls.Add(strLine.Split(','));
                    //Debug.WriteLine(strLine);
                }
            } 
            fileReader.Close();
            return ls;
        }


        private void ImportEDIFile(string fileName, string bakFile,User user)
        {
            string[] datStrs = System.IO.File.ReadAllLines(fileName);
            if (datStrs.Length == 0)
            {
                throw new Exception("文件为空。");
            }
            fileName = fileName.Substring(fileName.LastIndexOf("/") + 1);
            fileName = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
            //datStrs.ToList().Remove(datStrs.First());
            TEMP_FORD_EDI_830 fistEntity= new TEMP_FORD_EDI_830();
            for (int i = 0; i < datStrs.Length; i++)
            {
                string[] lineData = datStrs[i].Split(new char[] { ',' }).ToArray();
                //string inserSql = string.Format(" insert into EDI_TEMP_FORD_EDI_830 ( BatchNo , Interchange_Control_Num, Message_Release_Num, Message_Release_Date, Message_Purpose, Schedule_Type, Horizon_Start_Date, Horizon_End_Date, Comment_Note, Ship_To_GSDB_Code, Ship_From_GSDB_Code, Intermediate_Consignee, Part_Num, Purchase_Order_Num, Part_Release_Status, Dock_Code, Line_Feed, Reserve_Line_Feed, Contact_Name, Contact_Telephone, Fab_Auth_Qty, Fab_Auth_Start_Date, Fab_Auth_End_Date, Mat_Auth_Qty, Mat_Auth_Start_Date, Mat_Auth_End_Date, Last_Received_ASN_Num, Last_Shipped_Qty, Last_Shipped_Date, Cum_Shipped_Qty, Cum_Start_Date, Cum_End_Date, Forecast_Cum_Qty, Forecast_Net_Qty, UOM, Forecast_Status, Forecast_Date, Flexible_Forcast_Start_Date, Flexible_Forcast_End_Date, Forecast_Date_Qual_r, CreateDate, CreateUserName, IsHandle, ReadFileName) values(1,{0})", string.Join("','", lineData));
                if (i == 0)
                {
                    fistEntity.Message_Type_Code = GetLineDataValue(lineData, 0);
                    fistEntity.Message_Type = GetLineDataValue(lineData, 1);
                    fistEntity.Sender_ID_Title = GetLineDataValue(lineData, 2);
                    fistEntity.Sender_ID = GetLineDataValue(lineData, 3);
                    fistEntity.Receiver_ID_Title = GetLineDataValue(lineData, 4);
                    fistEntity.Receiver_ID = GetLineDataValue(lineData, 5);
                    if (fileName.Contains("_830_"))
                    {
                        fistEntity.BatchNo = numberControlMgr.GenerateNumberNextSequence(BusinessConstants.CODE_PREFIX_IMPORT_TEMPEDI830);
                    }
                    //else if (fileName.Contains("856"))
                    //{
                    //    fistEntity.BatchNo = numberControlMgr.GenerateNumberNextSequence(BusinessConstants.CODE_PREFIX_IMPORT_TEMPEDI856);
                    //}
                    else if (fileName.Contains("_862_"))
                    {
                        fistEntity.BatchNo = numberControlMgr.GenerateNumberNextSequence(BusinessConstants.CODE_PREFIX_IMPORT_TEMPEDI862);
                    }
                }
                else if (i == 1)
                {
                    continue;
                }
                else
                {
                    CreateTemp_FORD_EDI( fistEntity, lineData,  fileName,user.Name);
                }
            }

        }

        private void ProcessPath(ref string path)
        {
            path = path.Replace("\\", "/");
            if (!path.EndsWith("/"))
            {
                path += "/";
            }
        }

        private string GetLineDataValue(string[] lineData, int colIndex)
        {
            if (lineData.Length < colIndex)
            {
                return null;
            }
            else
            {
                string colData = lineData[colIndex];

                return colData == null ? null : colData.Trim();
            }
        }

        private void CreateTemp_FORD_EDI(TEMP_FORD_EDI_830 fistEntity, string[] lineData, string fileName,string userName)
        {
            DateTime datetimeNow = System.DateTime.Now;
            if (fileName.Contains("_830_"))
            {
                #region 
                TEMP_FORD_EDI_830 temp_FORD_EDI_830 = new TEMP_FORD_EDI_830();
                temp_FORD_EDI_830.Message_Type_Code = fistEntity.Message_Type_Code;
                temp_FORD_EDI_830.Message_Type = fistEntity.Message_Type;
                temp_FORD_EDI_830.Sender_ID_Title = fistEntity.Sender_ID_Title;
                temp_FORD_EDI_830.Sender_ID = fistEntity.Sender_ID;
                temp_FORD_EDI_830.Receiver_ID_Title = fistEntity.Receiver_ID_Title;
                temp_FORD_EDI_830.Receiver_ID = fistEntity.Receiver_ID;
                temp_FORD_EDI_830.BatchNo = fistEntity.BatchNo;

                temp_FORD_EDI_830.Interchange_Control_Num = GetLineDataValue(lineData, 0);
                temp_FORD_EDI_830.Message_Release_Num = GetLineDataValue(lineData, 1);
                temp_FORD_EDI_830.Message_Release_Date = GetLineDataValue(lineData, 2);
                temp_FORD_EDI_830.Message_Purpose = GetLineDataValue(lineData, 3);
                temp_FORD_EDI_830.Schedule_Type = GetLineDataValue(lineData, 4);
                temp_FORD_EDI_830.Horizon_Start_Date = GetLineDataValue(lineData, 5);
                temp_FORD_EDI_830.Horizon_End_Date = GetLineDataValue(lineData, 6);
                temp_FORD_EDI_830.Comment_Note = GetLineDataValue(lineData, 7);
                temp_FORD_EDI_830.Ship_To_GSDB_Code = GetLineDataValue(lineData, 8);
                temp_FORD_EDI_830.Ship_From_GSDB_Code = GetLineDataValue(lineData, 9);
                temp_FORD_EDI_830.Intermediate_Consignee = GetLineDataValue(lineData, 10);
                temp_FORD_EDI_830.Part_Num = GetLineDataValue(lineData, 11);
                temp_FORD_EDI_830.Purchase_Order_Num = GetLineDataValue(lineData, 12);
                temp_FORD_EDI_830.Part_Release_Status = GetLineDataValue(lineData, 13);
                temp_FORD_EDI_830.Dock_Code = GetLineDataValue(lineData, 14);
                temp_FORD_EDI_830.Line_Feed = GetLineDataValue(lineData, 15);
                temp_FORD_EDI_830.Reserve_Line_Feed = GetLineDataValue(lineData, 16);
                temp_FORD_EDI_830.Contact_Name = GetLineDataValue(lineData, 17);
                temp_FORD_EDI_830.Contact_Telephone = GetLineDataValue(lineData, 18);
                temp_FORD_EDI_830.Fab_Auth_Qty = GetLineDataValue(lineData, 19);
                temp_FORD_EDI_830.Fab_Auth_Start_Date = GetLineDataValue(lineData, 20);
                temp_FORD_EDI_830.Fab_Auth_End_Date = GetLineDataValue(lineData, 21);
                temp_FORD_EDI_830.Mat_Auth_Qty = GetLineDataValue(lineData, 22);
                temp_FORD_EDI_830.Mat_Auth_Start_Date = GetLineDataValue(lineData, 23);
                temp_FORD_EDI_830.Mat_Auth_End_Date = GetLineDataValue(lineData, 24);
                temp_FORD_EDI_830.Last_Received_ASN_Num = GetLineDataValue(lineData, 25);
                temp_FORD_EDI_830.Last_Shipped_Qty = GetLineDataValue(lineData, 26);
                temp_FORD_EDI_830.Last_Shipped_Date = GetLineDataValue(lineData, 27);
                temp_FORD_EDI_830.Cum_Shipped_Qty = GetLineDataValue(lineData, 28);
                temp_FORD_EDI_830.Cum_Start_Date = GetLineDataValue(lineData, 29);
                temp_FORD_EDI_830.Cum_End_Date = GetLineDataValue(lineData, 30);
                temp_FORD_EDI_830.Forecast_Cum_Qty = GetLineDataValue(lineData, 31);
                temp_FORD_EDI_830.Forecast_Net_Qty = GetLineDataValue(lineData, 32);
                temp_FORD_EDI_830.UOM = GetLineDataValue(lineData, 33);
                temp_FORD_EDI_830.Forecast_Status = GetLineDataValue(lineData, 34);
                temp_FORD_EDI_830.Forecast_Date = GetLineDataValue(lineData, 35);
                temp_FORD_EDI_830.Flexible_Forcast_Start_Date = GetLineDataValue(lineData, 36);
                temp_FORD_EDI_830.Flexible_Forcast_End_Date = GetLineDataValue(lineData, 37);
                temp_FORD_EDI_830.Forecast_Date_Qual_r = GetLineDataValue(lineData, 38);
                temp_FORD_EDI_830.CreateDate = datetimeNow;
                temp_FORD_EDI_830.CreateUserName = userName;
                temp_FORD_EDI_830.IsHandle = false;
                temp_FORD_EDI_830.ReadFileName = fileName;
                this.genericMgr.Create(temp_FORD_EDI_830);
                #endregion
            }
            //else if (fileName.Contains("856"))
            //{
            //    #region
            //    TEMP_FORD_EDI_856 temp_FORD_EDI_856 = new TEMP_FORD_EDI_856();
            //    temp_FORD_EDI_856.Message_Type_Code = fistEntity.Message_Type_Code;
            //    temp_FORD_EDI_856.Message_Type = fistEntity.Message_Type;
            //    temp_FORD_EDI_856.Sender_ID_Title = fistEntity.Sender_ID_Title;
            //    temp_FORD_EDI_856.Sender_ID = fistEntity.Sender_ID;
            //    temp_FORD_EDI_856.Receiver_ID_Title = fistEntity.Receiver_ID_Title;
            //    temp_FORD_EDI_856.Receiver_ID = fistEntity.Receiver_ID;
            //    temp_FORD_EDI_856.BatchNo = fistEntity.BatchNo;

            //    temp_FORD_EDI_856.Interchange_Control_Num = GetLineDataValue(lineData, 0);
            //    temp_FORD_EDI_856.ASN_Creation_DateTime = GetLineDataValue(lineData, 1);
            //    temp_FORD_EDI_856.Ship_To_GSDB_Code = GetLineDataValue(lineData, 2);
            //    temp_FORD_EDI_856.Ship_From_GSDB_Code = GetLineDataValue(lineData, 3);
            //    temp_FORD_EDI_856.Intermediate_Consignee_Code = GetLineDataValue(lineData, 4);
            //    temp_FORD_EDI_856.Message_Purpose_Code = GetLineDataValue(lineData, 5);
            //    temp_FORD_EDI_856.Shipment_ID = GetLineDataValue(lineData, 6);
            //    temp_FORD_EDI_856.Shipped_DateTime = GetLineDataValue(lineData, 7);
            //    temp_FORD_EDI_856.Gross_Weight = GetLineDataValue(lineData, 8);
            //    temp_FORD_EDI_856.Net_Weight = GetLineDataValue(lineData, 9);
            //    temp_FORD_EDI_856.UOM = GetLineDataValue(lineData, 10);
            //    temp_FORD_EDI_856.Packaging_Type_Code = GetLineDataValue(lineData, 11);
            //    temp_FORD_EDI_856.Lading_Qty = GetLineDataValue(lineData, 12);
            //    temp_FORD_EDI_856.Carrier_SCAC_Code = GetLineDataValue(lineData, 13);
            //    temp_FORD_EDI_856.Transportation_Method_Code = GetLineDataValue(lineData, 14);
            //    temp_FORD_EDI_856.Equipment_Desc_Code = GetLineDataValue(lineData, 15);
            //    temp_FORD_EDI_856.Part_Num = GetLineDataValue(lineData, 16);
            //    temp_FORD_EDI_856.Purchase_Order_Num = GetLineDataValue(lineData, 17);
            //    temp_FORD_EDI_856.Shipped_Qty = GetLineDataValue(lineData, 18);
            //    temp_FORD_EDI_856.Cum_Shipped_Qty = GetLineDataValue(lineData, 19);
            //    temp_FORD_EDI_856.Cum_Shipped_UOM = GetLineDataValue(lineData, 20);
            //    temp_FORD_EDI_856.Number_of_Loads = GetLineDataValue(lineData, 21);
            //    temp_FORD_EDI_856.Qty_Per_Load = GetLineDataValue(lineData, 22);
            //    temp_FORD_EDI_856.Packaging_Code = GetLineDataValue(lineData, 23);
            //    temp_FORD_EDI_856.Airport_Code = GetLineDataValue(lineData, 24);
            //    temp_FORD_EDI_856.CreateDate = System.DateTime.Now;
            //    temp_FORD_EDI_856.CreateUserName = "";
            //    temp_FORD_EDI_856.IsHandle = false;
            //    temp_FORD_EDI_856.ReadFileName = fileName;
            //    this.genericMgr.Create(temp_FORD_EDI_856);
            //    #endregion
            //}
            else if (fileName.Contains("_862_"))
            {
                TEMP_FORD_EDI_862 temp_FORD_EDI_862 = new TEMP_FORD_EDI_862();
                temp_FORD_EDI_862.Message_Type_Code = fistEntity.Message_Type_Code;
                temp_FORD_EDI_862.Message_Type = fistEntity.Message_Type;
                temp_FORD_EDI_862.Sender_ID_Title = fistEntity.Sender_ID_Title;
                temp_FORD_EDI_862.Sender_ID = fistEntity.Sender_ID;
                temp_FORD_EDI_862.Receiver_ID_Title = fistEntity.Receiver_ID_Title;
                temp_FORD_EDI_862.Receiver_ID = fistEntity.Receiver_ID;
                temp_FORD_EDI_862.BatchNo = fistEntity.BatchNo;

                temp_FORD_EDI_862.Interchange_Control_Num = GetLineDataValue(lineData, 0);
                temp_FORD_EDI_862.Message_Release_Num = GetLineDataValue(lineData, 1);
                temp_FORD_EDI_862.Message_Release_Date = GetLineDataValue(lineData, 2);
                temp_FORD_EDI_862.Message_Purpose = GetLineDataValue(lineData, 3);
                temp_FORD_EDI_862.Schedule_Type = GetLineDataValue(lineData, 4);
                temp_FORD_EDI_862.Horizon_Start_Date = GetLineDataValue(lineData, 5);
                temp_FORD_EDI_862.Horizon_End_Date = GetLineDataValue(lineData, 6);
                temp_FORD_EDI_862.Message_Reference_Num = GetLineDataValue(lineData, 7);
                temp_FORD_EDI_862.Ship_To_GSDB_Code = GetLineDataValue(lineData, 8);
                temp_FORD_EDI_862.Ship_From_GSDB_Code = GetLineDataValue(lineData, 9);
                temp_FORD_EDI_862.Intermediate_Consignee = GetLineDataValue(lineData, 10);
                temp_FORD_EDI_862.Part_Num = GetLineDataValue(lineData, 11);
                temp_FORD_EDI_862.Purchase_Order_Num = GetLineDataValue(lineData, 12);
                temp_FORD_EDI_862.Dock_Code = GetLineDataValue(lineData, 13);
                temp_FORD_EDI_862.Line_Feed = GetLineDataValue(lineData, 14);
                temp_FORD_EDI_862.Reserve_Line_Feed = GetLineDataValue(lineData, 15);
                temp_FORD_EDI_862.Contact_Name = GetLineDataValue(lineData, 16);
                temp_FORD_EDI_862.Contact_Telephone = GetLineDataValue(lineData, 17);
                temp_FORD_EDI_862.Last_Received_ASN_Num = GetLineDataValue(lineData, 18);
                temp_FORD_EDI_862.Last_Shipped_Qty = GetLineDataValue(lineData, 19);
                temp_FORD_EDI_862.Last_Shipped_Date = GetLineDataValue(lineData, 20);
                temp_FORD_EDI_862.Cum_Shipped_Qty = GetLineDataValue(lineData, 21);
                temp_FORD_EDI_862.Cum_Start_Date = GetLineDataValue(lineData, 22);
                temp_FORD_EDI_862.Cum_End_Date = GetLineDataValue(lineData, 23);
                temp_FORD_EDI_862.Forecast_Cum_Qty = GetLineDataValue(lineData, 24);
                temp_FORD_EDI_862.Forecast_Net_Qty = GetLineDataValue(lineData, 25);
                temp_FORD_EDI_862.UOM = GetLineDataValue(lineData, 26);
                temp_FORD_EDI_862.Forecast_Status = GetLineDataValue(lineData, 27);
                temp_FORD_EDI_862.Forecast_Date = GetLineDataValue(lineData, 28);
                temp_FORD_EDI_862.Forecast_Time = GetLineDataValue(lineData, 29);
                temp_FORD_EDI_862.CreateDate = datetimeNow;
                temp_FORD_EDI_862.CreateUserName = userName;
                temp_FORD_EDI_862.IsHandle = false;
                temp_FORD_EDI_862.ReadFileName = fileName;
                this.genericMgr.Create(temp_FORD_EDI_862);
            }
        }

    }
}
