using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using System.IO;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.Dss;
using Castle.Services.Transaction;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;

using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Xml;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Service.Dss
{
    [Transactional]
    public abstract class AbstractOutboundMgr : IOutboundMgr
    {
        #region 变量
        protected static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssOutbound");
        protected IList<DssObjectMapping> _dssObjectMapping;

        private INumberControlMgr numberControlMgr;
        private IDssExportHistoryMgr dssExportHistoryMgr;
        private ICriteriaMgr criteriaMgr;
        private IDssOutboundControlMgr dssOutboundControlMgr;
        private IDssObjectMappingMgr dssObjectMappingMgr;
        private ILocationMgr locationMgr;
        #endregion

        public AbstractOutboundMgr(INumberControlMgr numberControlMgr,
            IDssExportHistoryMgr dssExportHistoryMgr,
            ICriteriaMgr criteriaMgr,
            IDssOutboundControlMgr dssOutboundControlMgr,
            IDssObjectMappingMgr dssObjectMappingMgr,
            ILocationMgr locationMgr)
        {
            this.numberControlMgr = numberControlMgr;
            this.dssExportHistoryMgr = dssExportHistoryMgr;
            this.criteriaMgr = criteriaMgr;
            this.dssOutboundControlMgr = dssOutboundControlMgr;
            this.dssObjectMappingMgr = dssObjectMappingMgr;
            this.locationMgr = locationMgr;
        }



        public void ProcessOutbound(DssOutboundControl dssOutboundControl)
        {
            log.Info("Start process outbound.");
            this.Initital_DBCache();

            #region Initailize
            this._dssObjectMapping = this.dssObjectMappingMgr.GetAllDssObjectMapping();
            #endregion

            string outFolder = dssOutboundControl.OutFolder;
            //string serviceName = dssOutboundControl.ServiceName;
            string archiveFolder = dssOutboundControl.ArchiveFolder;
            string tempFolder = dssOutboundControl.TempFolder;
            //string encoding = dssOutboundControl.FileEncoding;
            string encoding = Encoding.Default.WebName;
            string filePrefix = this.GetFilePrefix(dssOutboundControl);

            #region 初始化本地目录
            outFolder = outFolder.Replace("\\", "/");
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
                Directory.Delete(tempFolder, true);
            }
            Directory.CreateDirectory(tempFolder);
            #endregion

            #region 抽取数据
            log.Info("Begin to extract data:extsyscode:" + dssOutboundControl.ExternalSystem.Code + ",extobjcode:" + dssOutboundControl.ExternalObjectCode + ",service:" + dssOutboundControl.ServiceName);
            IList<DssExportHistory> dataList = this.GetHisList(dssOutboundControl.Id);

            #region 缓存数据
            IList<DssExportHistory> dssExportHistoryList = ExtractOutboundData(dssOutboundControl);
            this.ObjectMapping(dssExportHistoryList);
            dssExportHistoryList = this.FilterList(dssExportHistoryList);
            if (dssExportHistoryList != null && dssExportHistoryList.Count > 0)
            {
                this.CreateDssExportHistory(dssExportHistoryList, dssOutboundControl);
                dataList = dataList.Concat(dssExportHistoryList).ToList();

                log.Info("DssExportHistory count:" + dataList.Count + ",update new mark:" + dssOutboundControl.Mark);
            }
            #endregion

            #endregion

            #region 循环处理抽取数据
            if (dataList != null && dataList.Count > 0)
            {
                //if (dssOutboundControl.Id != 7)
                //{
                    #region 非iss-so
                    foreach (DssExportHistory dssExportHistory in dataList)
                    {
                        try
                        {
                            object obj = null;
                            try
                            {
                                obj = GetOutboundData(dssExportHistory);
                            }
                            catch (BusinessErrorException ex)
                            {
                                log.Warn("Get no outbound data:", ex);
                                continue;
                            }

                            if (obj == null)
                                continue;

                            #region 对象转换为数组
                            object o = Serialize(obj);
                            DateTime effDate = (DateTime)((object[])o)[0];
                            string[][] data = (string[][])((object[])o)[1];
                            #endregion

                            #region 抽取数据导入文件
                            string transNo = this.GetTransNumber(dssOutboundControl, filePrefix, effDate);
                            //modified by williamlu@esteering.cn
                            //2012/5   
                            string location = dssExportHistory.Location;
                            bool partyfrom = dssExportHistory.PartyFrom == "1001" ? true : false;
                            bool partyto = dssExportHistory.PartyTo == "1001" ? true : false;
                            //string region = locationMgr.LoadLocation(location).Region.Code;
                            string bj_region = partyfrom || partyto == true ? "BJ" : "";
                            string fileName = this.GetFileName(dssOutboundControl, filePrefix, transNo, bj_region);
                            //modified end
                            StreamWriter streamWriter = new StreamWriter(tempFolder + fileName, false, Encoding.GetEncoding(encoding));
                            FlatFileWriter flatFileWriter = new FlatFileWriter(streamWriter, Environment.NewLine, "|");
                            flatFileWriter.Write(data);
                            flatFileWriter.Dispose();
                            #endregion

                            #region 文件移至目录
                            try
                            {
                                File.Copy(tempFolder + fileName, archiveFolder + fileName);  //备份目录
                                File.Move(tempFolder + fileName, outFolder + fileName);     //导出目录

                                #region 更新导出标记
                                dssExportHistory.IsActive = false;
                                dssExportHistory.TransNo = transNo;
                                dssExportHistoryMgr.UpdateDssExportHistory(dssExportHistory);
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                log.Error("Create export file error.", ex);
                                if (File.Exists(archiveFolder + fileName))
                                {
                                    File.Delete(archiveFolder + fileName);
                                }

                                if (File.Exists(outFolder + fileName))
                                {
                                    File.Delete(outFolder + fileName);
                                }
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            log.Error("Export data error.", ex);
                            break;//2012-10-24 发生错误执行下一任务 djin
                        }
                    }
                    #endregion
                //}
                //else
                //{
                //    var dataListGroup = (from i in dataList select i.DefinedString1).Distinct().ToList();
                //    foreach (var i in dataListGroup)
                //    {
                //        IList<string[][]> outString = new List<string[][]>();
                //        var result = (from x in dataList where x.DefinedString1 == i select x).ToList();
                //        #region  iss-so
                       
                //        if (result != null && result.Count > 0)
                //        {
                //            foreach (DssExportHistory dssExportHistory in result)
                //            {
                //                try
                //                {
                //                    object obj = null;
                //                    try
                //                    {
                //                        obj = GetOutboundData(dssExportHistory);
                //                    }
                //                    catch (BusinessErrorException ex)
                //                    {
                //                        log.Warn("Get no outbound data:", ex);
                //                        continue;
                //                    }

                //                    if (obj == null)
                //                        continue;

                //                    #region 对象转换为数组
                //                    object o = Serialize(obj);

                //                    string[][] data = (string[][])((object[])o)[1];
                                      
                //                    outString.Add(data);
                                  
                //                    #endregion
                //                }
                //                catch (Exception ex)
                //                {
                //                    log.Error("Export data error.", ex);
                //                    break;//2012-10-24 发生错误执行下一任务 djin
                //                }
                //            }

                //            #region 抽取数据导入文件
                //            try
                //            {
                //                DateTime effDate = (DateTime)((object[])Serialize(GetOutboundData(result[0])))[0];

                //                string transNo = this.GetTransNumber(dssOutboundControl, filePrefix, effDate);
                //                //modified by williamlu@esteering.cn
                //                //2012/5   
                //                string location = result[0].Location;
                //                bool partyfrom = result[0].PartyFrom == "1001" ? true : false;
                //                bool partyto = result[0].PartyTo == "1001" ? true : false;
                //                //string region = locationMgr.LoadLocation(location).Region.Code;
                //                string bj_region = partyfrom || partyto == true ? "BJ" : "";
                //                string fileName = this.GetFileName(dssOutboundControl, filePrefix, transNo, bj_region);
                //                fileName = fileName.Split(new char[] { '.' })[0].TrimEnd(new char[] { '1' }) +"."+ fileName.Split(new char[] { '.' })[1];
                //                //modified end
                //                StreamWriter streamWriter = new StreamWriter(tempFolder + fileName, false, Encoding.GetEncoding(encoding));
                //                FlatFileWriter flatFileWriter = new FlatFileWriter(streamWriter, Environment.NewLine, "|");
                //                flatFileWriter.WriteWithNewLine(((string[][])(outString[0]))[0]);
                //                foreach (string[][] str in outString)
                //                {
                //                    flatFileWriter.WriteWithNewLine(str[1]);
                //                }
                //                flatFileWriter.Dispose();
                //            #endregion

                //                #region 文件移至目录
                //                try
                //                {
                //                    File.Copy(tempFolder + fileName, archiveFolder + fileName);  //备份目录
                //                    File.Move(tempFolder + fileName, outFolder + fileName);     //导出目录

                //                    #region 更新导出标记
                //                    foreach (DssExportHistory dssExportHistory in dataList)
                //                    {
                //                        dssExportHistory.IsActive = false;
                //                        dssExportHistory.TransNo = transNo;
                //                        dssExportHistoryMgr.UpdateDssExportHistory(dssExportHistory);
                //                    }
                //                    #endregion
                //                }
                //                catch (Exception ex)
                //                {
                //                    log.Error("Create export file error.", ex);
                //                    if (File.Exists(archiveFolder + fileName))
                //                    {
                //                        File.Delete(archiveFolder + fileName);
                //                    }

                //                    if (File.Exists(outFolder + fileName))
                //                    {
                //                        File.Delete(outFolder + fileName);
                //                    }
                //                }
                //                #endregion
                //            }
                //            catch (Exception ex)
                //            {
                //                continue;
                //            }
                //        #endregion
                //        }
                //    }
                //}
            }
            else
            {
                log.Info("No data export.");
            }
            #endregion

            this.Initital_DBCache();
            log.Info("Start process outbound successful.");
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateDssExportHistory(IList<DssExportHistory> dssExportHistoryList, DssOutboundControl dssOutboundControl)
        {

            if (dssExportHistoryList != null && dssExportHistoryList.Count > 0)
            {
                int newMark1 = 0;
                if (dssOutboundControl.Id != 3)
                    newMark1 = dssExportHistoryList.Max(d => d.OriginalId);
                else if (dssOutboundControl.Id == 3)//RCT-TR中取费零星销售的最大ID
                {
                    var dssList = (from dss in dssExportHistoryList where dss.Comments != "ISS-UNP2RCT-TR" select dss).ToList();
                    if (dssList != null && dssList.Count > 0)
                        newMark1 = dssList.Max(d => d.OriginalId);
                }
                int newMark2 = 0;
                // bool errFlag = false;
                //  int markID = 0;
                if (dssOutboundControl.Id == 9) Fileter(dssExportHistoryList);//ISS-UNP中删除零星销售
                if (dssExportHistoryList != null && dssExportHistoryList.Count > 0)
                {
                    dssExportHistoryMgr.CreateDssExportHistory(dssExportHistoryList);
                    #region
                    //dssExportHistoryList = (from l in dssExportHistoryList orderby l.OriginalId select l).ToList();
                    //try
                    //{
                    //    foreach (DssExportHistory dss in dssExportHistoryList)
                    //    {

                    //        dssExportHistoryMgr.CreateDssExportHistory(dss);
                    //        if (dss.DssExportHistoryDetails != null && dss.DssExportHistoryDetails.Count > 0)
                    //        {
                    //            markID = dss.DssExportHistoryDetails.Max(d => d.OriginalId);
                    //        }
                    //        markID = Math.Max(markID, dss.OriginalId);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    errFlag = true;
                    //}
                    #endregion

                    #region New Mark
                    var query = from d in dssExportHistoryList
                                where d.DssExportHistoryDetails != null && d.DssExportHistoryDetails.Count > 0
                                from dd in d.DssExportHistoryDetails
                                select dd;
                    if (query != null && query.Count() > 0)
                    {
                        newMark2 = query.Max(d => d.OriginalId);
                    }
                }
                DssOutboundControl newDssOutboundControl = this.dssOutboundControlMgr.LoadDssOutboundControl(dssOutboundControl.Id);
                newMark1 = Math.Max(newMark1, newDssOutboundControl.Mark);
                //if (!errFlag)
                newDssOutboundControl.Mark = Math.Max(newMark1, newMark2);
                dssOutboundControl.Mark = newDssOutboundControl.Mark;//修正logBug
                // else
                // newDssOutboundControl.Mark = markID;
                    #endregion
                try
                {
                    dssOutboundControlMgr.UpdateDssOutboundControl(newDssOutboundControl);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message + " Markid" + newDssOutboundControl.Mark);
                    bool isupd = false;
                    int i = 0;
                    while (!isupd && i < 100)
                    {
                        dssOutboundControlMgr.UpdateDssOutboundControl(newDssOutboundControl);
                        DssOutboundControl errDSC = this.dssOutboundControlMgr.LoadDssOutboundControl(newDssOutboundControl.Id);
                        i++;
                        if (errDSC.Mark == newDssOutboundControl.Mark)
                            isupd = true;
                    }
                }
            }
        }
        protected void Fileter(IList<DssExportHistory> dssExportHistoryList)//过滤ISS-UNP转RCT-TR的
        {
            var list = (from a in dssExportHistoryList where a.Comments == "DEL" select a).ToList();
            foreach (var i in list)
            {
                dssExportHistoryList.Remove((DssExportHistory)i);
            }
        }

        #region Abstract Method
        protected abstract IList<DssExportHistory> ExtractOutboundData(DssOutboundControl dssOutboundControl);
        protected abstract object GetOutboundData(DssExportHistory dssExportHistory);
        protected abstract object Serialize(object obj);
        #endregion

        #region Virtual Method
        #region Object Mapping
        protected virtual void ObjectMapping(IList<DssExportHistory> dssExportHistoryList)
        {
            if (dssExportHistoryList != null && dssExportHistoryList.Count > 0)
            {
                foreach (var item in dssExportHistoryList)
                {
                    this.ObjectMapping(item);
                }
            }
        }
        protected virtual void ObjectMapping(DssExportHistory dssExportHistory)
        {

            string entityParty = BusinessConstants.DSS_ENTITY_PARTY;
            string entityLocation = BusinessConstants.DSS_ENTITY_LOCATION;
            string entitySite = BusinessConstants.DSS_ENTITY_SITE;
            string defaultSite = dssExportHistory.DssOutboundControl.ExternalSystem.UndefinedString1;
            string extSysCode = dssExportHistory.DssOutboundControl.ExternalSystem.Code;
            string location = dssExportHistory.Location;
            string refLocation = dssExportHistory.ReferenceLocation;
            string partyFrom = dssExportHistory.PartyFrom;
            string partyTo = dssExportHistory.PartyTo;

            if (partyFrom == BusinessConstants.SYSTEM_REGION)
            {
                if (refLocation != null && refLocation != BusinessConstants.SYSTEM_LOCATION_INSPECT && refLocation != BusinessConstants.SYSTEM_LOCATION_REJECT)
                {
                    partyFrom = locationMgr.LoadLocation(refLocation).Region.Code;
                }
                else if (location != BusinessConstants.SYSTEM_LOCATION_INSPECT && location != BusinessConstants.SYSTEM_LOCATION_REJECT)
                {
                    partyFrom = locationMgr.LoadLocation(location).Region.Code;
                }
            }
            if (partyTo == BusinessConstants.SYSTEM_REGION)
            {
                if (location != BusinessConstants.SYSTEM_LOCATION_INSPECT && location != BusinessConstants.SYSTEM_LOCATION_REJECT)
                {
                    partyTo = locationMgr.LoadLocation(location).Region.Code;
                }
                else if (refLocation != null && refLocation != BusinessConstants.SYSTEM_LOCATION_INSPECT && refLocation != BusinessConstants.SYSTEM_LOCATION_REJECT)
                {
                    partyTo = locationMgr.LoadLocation(refLocation).Region.Code;
                }
            }

            //供应商,来源区域 => Site
            dssExportHistory.PartyFrom = this.GetMappingExternalCode(entityParty, extSysCode, partyFrom, entitySite, partyFrom);
            //客户,目的区域 => Site
            dssExportHistory.PartyTo = this.GetMappingExternalCode(entityParty, extSysCode, partyTo, entitySite, partyTo);
            //库位
            dssExportHistory.Location = this.GetMappingExternalCode(entityLocation, extSysCode, location, location);

            //移库来源库位
            if (dssExportHistory.ReferenceLocation != null && dssExportHistory.ReferenceLocation.Trim() != string.Empty)
            {
                dssExportHistory.ReferenceLocation = this.GetMappingExternalCode(entityLocation, extSysCode, refLocation, refLocation);
            }
        }

        protected virtual string GetMappingExternalCode(string entity, string externalSystem, string code, string defaultResult)
        {
            return this.GetMappingExternalCode(entity, externalSystem, code, entity, defaultResult);
        }
        protected virtual string GetMappingExternalCode(string entity, string externalSystem, string code, string externalEntity, string defaultResult)
        {
            var query = this._dssObjectMapping
                .Where(d => StringHelper.Eq(d.Entity, entity)
                    && StringHelper.Eq(d.ExternalSystem.Code, externalSystem)
                    && StringHelper.Eq(d.ExternalEntity, externalEntity)
                    && StringHelper.Eq(d.Code, code)).ToList();

            if (query.Count > 0)
            {
                return query[0].ExternalCode;
            }
            else
            {
                return defaultResult;
            }
        }
        #endregion

        #endregion

        #region Private Method
        private void Initital_DBCache()
        {
            this._dssObjectMapping = null;
        }

        private string GetFilePrefix(DssOutboundControl dssOutboundControl)
        {
            if (dssOutboundControl.FilePrefix != null && dssOutboundControl.FilePrefix.Trim() != string.Empty)
                return dssOutboundControl.FilePrefix;

            string sysCode = BusinessConstants.DSS_SYSTEM_CODE_SCONIT;
            string extSysCode = dssOutboundControl.ExternalSystem.Code;

            if (dssOutboundControl.SysAlias != null && dssOutboundControl.SysAlias.Trim() != string.Empty)
                sysCode = dssOutboundControl.SysAlias;
            if (dssOutboundControl.ExternalSystem.SysAlias != null && dssOutboundControl.ExternalSystem.SysAlias.Trim() != string.Empty)
                extSysCode = dssOutboundControl.ExternalSystem.SysAlias;

            string filePrefix = sysCode + "_" + extSysCode;
            return filePrefix;
        }

        private string GetTransNumber(DssOutboundControl dssOutboundControl, string filePrefix, DateTime effDate)
        {
            string codePrefix = filePrefix + effDate.ToString("yyyyMMdd");
            if (dssOutboundControl.ExternalSystem.Flag != null && dssOutboundControl.ExternalSystem.Flag.Trim() != string.Empty)
                codePrefix += dssOutboundControl.ExternalSystem.Flag.Substring(0, 1);
            else
                codePrefix += "0";

            string transNo = this.numberControlMgr.GenerateNumber(codePrefix, 8);
            return transNo.Substring(transNo.Length - 17, 17);
        }

        //modified by williamlu@esteering.cn
        //2012/5
        //Filename default: Region + FilePrefix + TransNo + FileSuffix .REQ
        private string GetFileName(DssOutboundControl dssOutboundControl, string filePrefix, string transNo, string region)
        {
            string fileSuffix = string.Empty;
            if (dssOutboundControl.FileSuffix != null && dssOutboundControl.FileSuffix.Trim() != string.Empty)
                fileSuffix = dssOutboundControl.FileSuffix;
            else
                fileSuffix = dssOutboundControl.ExternalObjectCode + ".REQ";
            string fileName = filePrefix + "_" + transNo + "_" + fileSuffix;
            if (region != "")
            {
                fileName = region + filePrefix + "_" + transNo + "_" + fileSuffix;
            }
            return fileName;
        }
        //modified end

        private IList<DssExportHistory> GetHisList(int dssOutboundControlId)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(DssExportHistory))
                .Add(Expression.Eq("IsActive", true))
                .Add(Expression.Gt("CreateDate", DateTime.Now.AddMonths(-1)))//仅处理一个月内的历史
                .Add(Expression.Eq("DssOutboundControl.Id", dssOutboundControlId));

            IList<DssExportHistory> dssExportHistoryList = criteriaMgr.FindAll<DssExportHistory>(criteria);
            if (dssExportHistoryList != null && dssExportHistoryList.Count > 0 && dssOutboundControlId == 6)
            {
                List<int> IdList = dssExportHistoryList.Select(d => d.Id).ToList();
                IList<DssExportHistoryDetail> details = this.GetHisDetList(IdList);
                foreach (var dssExportHistory in dssExportHistoryList)
                {
                    dssExportHistory.DssExportHistoryDetails = details.Where(d => d.MstrId == dssExportHistory.Id).ToList();
                }
            }

            return dssExportHistoryList;
        }

        private IList<DssExportHistoryDetail> GetHisDetList(List<int> IdList)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(DssExportHistoryDetail));
            CriteriaHelper.SetInCriteria<int>(criteria, "MstrId", IdList);

            return criteriaMgr.FindAll<DssExportHistoryDetail>(criteria);
        }

        /// <summary>
        /// 过滤Mapping后相同库位的移库
        /// </summary>
        /// <param name="dssExportHistoryList"></param>
        /// <returns></returns>
        private IList<DssExportHistory> FilterList(IList<DssExportHistory> dssExportHistoryList)
        {
            if (dssExportHistoryList == null || dssExportHistoryList.Count == 0)
                return null;

            var query = dssExportHistoryList
                .Where(d => d.ReferenceLocation == null || (!StringHelper.Eq(d.ReferenceLocation, d.Location)))
                .ToList();

            return query;
        }

        #endregion
    }
}
