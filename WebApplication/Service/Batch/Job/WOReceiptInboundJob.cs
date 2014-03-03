using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;
using System.IO;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Production;

namespace com.Sconit.Service.Batch.Job
{
    [Transactional]
    public class WOReceiptInboundJob : IJob
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.WoReceiptInbound");

        private string INBOUND_DIRECTORY = "inboundDirectory";
        private string ARCHIVE_DIRECTORY = "archiveDirectory";
        private string ERROR_DIRECTORY = "errorDirectory";
        private string FTP_SERVER = "ftpServer";
        private string FTP_PORT = "ftpPort";
        private string FTP_INBOUND_FOLDER = "ftpInboundFolder";
        private string FTP_USER = "ftpUser";
        private string FTP_PASSWORD = "ftpPass";
        private string LOCAL_TEMP_FOLDER = "localTempFolder";

        private ICriteriaMgr criteriaMgr;
        private IUomConversionMgr uomConversionMgr;
        private IUserMgr userMgr;
        private IHuMgr huMgr;
        private INumberControlMgr numberControlMgr;
        private IOrderMgr orderManager;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;

        #region 构造函数
        public WOReceiptInboundJob(
            ICriteriaMgr criteriaMgr,
            IUomConversionMgr uomConversionMgr,
            IUserMgr userMgr,
            IHuMgr huMgr,
            INumberControlMgr numberControlMgr,
            IOrderMgr orderManager,
            IOrderLocationTransactionMgr orderLocationTransactionMgr)
        {
            this.criteriaMgr = criteriaMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.userMgr = userMgr;
            this.huMgr = huMgr;
            this.numberControlMgr = numberControlMgr;
            this.orderManager = orderManager;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
        }
        #endregion

        [Transaction(TransactionMode.Unspecified)]
        public void Execute(JobRunContext context)
        {
            #region 获取参数
            JobDataMap jobDataMap = context.JobDataMap;
            string inboundDirectoryName = string.Empty;
            string archiveDirectoryName = string.Empty;
            string errorDirectoryName = string.Empty;
            string ftpServer = string.Empty;
            int ftpPort = 21;
            string ftpInboundFolder = string.Empty;
            string ftpUser = string.Empty;
            string ftpPass = string.Empty;
            string localTempFolder = string.Empty;

            if (jobDataMap.ContainKey(INBOUND_DIRECTORY))
            {
                inboundDirectoryName = jobDataMap.GetStringValue(INBOUND_DIRECTORY);
            }
            else
            {
                throw new BusinessErrorException("not specify inbound directory.");
            }

            if (jobDataMap.ContainKey(ARCHIVE_DIRECTORY))
            {
                archiveDirectoryName = jobDataMap.GetStringValue(ARCHIVE_DIRECTORY);
            }
            else
            {
                throw new BusinessErrorException("not specify archive directory.");
            }

            if (jobDataMap.ContainKey(ERROR_DIRECTORY))
            {
                errorDirectoryName = jobDataMap.GetStringValue(ERROR_DIRECTORY);
            }
            else
            {
                throw new BusinessErrorException("not specify error directory.");
            }

            if (jobDataMap.ContainKey(FTP_SERVER))
            {
                ftpServer = jobDataMap.GetStringValue(FTP_SERVER);
            }
            else
            {
                throw new BusinessErrorException("not ftp server address.");
            }

            if (jobDataMap.ContainKey(FTP_PORT))
            {
                ftpPort = jobDataMap.GetIntValue(FTP_PORT);
            }
            else
            {
                log.Info("using default ftp port 21.");
            }

            if (jobDataMap.ContainKey(FTP_INBOUND_FOLDER))
            {
                ftpInboundFolder = jobDataMap.GetStringValue(FTP_INBOUND_FOLDER);
            }
            else
            {
                throw new BusinessErrorException("not specify ftp inbound folder.");
            }

            if (jobDataMap.ContainKey(FTP_USER))
            {
                ftpUser = jobDataMap.GetStringValue(FTP_USER);
            }
            else
            {
                throw new BusinessErrorException("not specify ftp user.");
            }

            if (jobDataMap.ContainKey(FTP_PASSWORD))
            {
                ftpPass = jobDataMap.GetStringValue(FTP_PASSWORD);
            }
            else
            {
                throw new BusinessErrorException("not specify ftp password.");
            }

            if (jobDataMap.ContainKey(LOCAL_TEMP_FOLDER))
            {
                localTempFolder = jobDataMap.GetStringValue(LOCAL_TEMP_FOLDER);
            }
            else
            {
                throw new BusinessErrorException("not specify local temp folder.");
            }
            #endregion

            #region 下载WO文件
            try
            {
                #region 初始化本地目录
                localTempFolder = localTempFolder.Replace("\\", "/");
                if (!localTempFolder.EndsWith("/"))
                {
                    localTempFolder += "/";
                }
                if (!Directory.Exists(localTempFolder))
                {
                    Directory.CreateDirectory(localTempFolder);
                }
         
                inboundDirectoryName = inboundDirectoryName.Replace("\\", "/");
                if (!inboundDirectoryName.EndsWith("/"))
                {
                    inboundDirectoryName += "/";
                }
                if (!Directory.Exists(inboundDirectoryName))
                {
                    Directory.CreateDirectory(inboundDirectoryName);
                }
                #endregion

                #region 下载文件
                FtpHelper ftp = new FtpHelper(ftpServer, ftpPort, ftpInboundFolder, ftpUser, ftpPass);
                string[] fileList = ftp.GetFileList();
                foreach (string fileName in fileList)
                {
                    try
                    {
                        ftp.Download(localTempFolder, fileName);
                        log.Info("Move file from folder: " + localTempFolder + fileName + " to folder: " + inboundDirectoryName + fileName);
                        File.Move(localTempFolder + fileName, inboundDirectoryName + fileName);
                        ftp.Delete(fileName);
                    }
                    catch (Exception ex)
                    {
                        log.Error("Download file:" + fileName, ex);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                log.Error("Download files from ftpServer:" + ftpServer, ex);
            }
            #endregion

            string[] inboundFiles = Directory.GetFiles(inboundDirectoryName);
            if (inboundFiles != null && inboundFiles.Length > 0)
            {
                foreach (string inboundFile in inboundFiles)
                {
                    string fileName = inboundFile.Substring(inboundFile.LastIndexOf("/"));

                    
                    try
                    {
                        ProcessSingleFile(inboundDirectoryName, inboundFile);

                        if (!Directory.Exists(archiveDirectoryName))
                        {
                            Directory.CreateDirectory(archiveDirectoryName);
                        }

                        if (File.Exists(archiveDirectoryName + fileName))
                        {
                            File.Delete(archiveDirectoryName + fileName);
                        }

                        File.Move(inboundFile, archiveDirectoryName + fileName);
                    }
                    catch (Exception ex)
                    {
                        log.Error("Unexpected error occour when processing inbound file " + inboundFile, ex);
                        if (!Directory.Exists(errorDirectoryName))
                        {
                            Directory.CreateDirectory(errorDirectoryName);
                        }

                        if (File.Exists(errorDirectoryName + fileName))
                        {
                            File.Delete(errorDirectoryName + fileName);
                        }

                        File.Move(inboundFile, errorDirectoryName + fileName);
                    }
                }
            }
        }

        [Transaction(TransactionMode.RequiresNew)]
        public virtual void ProcessSingleFile(string inboundDirectoryName, string inboundFileName)
        {
            log.Info("Start inbound file " + inboundFileName);
            FlatFileReader reader = new FlatFileReader(inboundFileName, Encoding.ASCII, "\t");

            try
            {
                OrderHead orderHead = null;
                OrderDetail orderDetail = null;
                string shiftCode = string.Empty;
                Hu hu = null;

                string[] fields = reader.ReadLine();
                while (fields != null)
                {
                    string prodLine = fields[0];
                    string itemCode = fields[1];
                    string huId = fields[2];
                    decimal qty = decimal.Parse(fields[3]);
                    string itemHuId = fields[4];
                    string onlineDate = fields[5];
                    string onlineTime = fields[6];
                    string offlineDate = fields[7];
                    string offlineTime = fields[8];
                    string customerCode = fields[9];
                    string customerLoc = fields[10];

                    if (orderHead == null)
                    {
                        #region 查找工单
                        shiftCode = BarcodeHelper.GetShiftCode(huId);

                        DetachedCriteria criteria = DetachedCriteria.For<OrderHead>();
                        criteria.CreateAlias("Flow", "f");
                        //criteria.CreateAlias("Shift", "s");

                        criteria.Add(Expression.Like("f.Code", prodLine, MatchMode.End));
                        criteria.Add(Expression.Eq("s.Code", shiftCode));
                        criteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));

                        criteria.AddOrder(Order.Asc("StartTime"));

                        IList<OrderHead> orderHeadList = this.criteriaMgr.FindAll<OrderHead>(criteria);
                        #endregion

                        if (orderHeadList != null && orderHeadList.Count > 0)
                        {
                            foreach(OrderHead targetOrderHead in orderHeadList) 
                            {
                                orderHead = targetOrderHead;                               

                                #region 查找工单明细
                                IList<OrderDetail> orderDetailList = orderHead.OrderDetails;
                                foreach (OrderDetail targetOrderDetail in orderDetailList)
                                {
                                    if (targetOrderDetail.Item.Code == itemCode)
                                    {
                                        log.Info("Find match wo " + orderHead.OrderNo);
                                        orderDetail = targetOrderDetail;
                                        orderDetail.CurrentReceiveQty = qty;
                                        break;
                                    }
                                }                                
                                #endregion

                                if (orderDetail != null)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            throw new BusinessErrorException("No active wo find for prodline + " + prodLine + ", shift " + shiftCode);
                        }

                        if (orderDetail != null)
                        {
                            #region 创建外包装条码
                            if (this.huMgr.LoadHu(huId) == null)
                            {
                                log.Info("Insert hu " + huId + " into database.");
                                hu = ResolveAndCreateHu(huId, orderDetail, qty);
                                orderDetail.HuId = hu.HuId;

                                Receipt receipt = new Receipt();
                                ReceiptDetail receiptDetail = new ReceiptDetail();
                                receiptDetail.OrderLocationTransaction = this.orderLocationTransactionMgr.GetOrderLocationTransaction(orderDetail.Id, BusinessConstants.IO_TYPE_IN)[0];
                                receiptDetail.HuId = hu.HuId;
                                receiptDetail.ReceivedQty = qty;
                                receiptDetail.Receipt = receipt;
                                receiptDetail.LotNo = hu.LotNo;

                                #region 找Out的OrderLocTrans，填充MaterialFulshBack
                                IList<OrderLocationTransaction> orderLocTransList = this.orderLocationTransactionMgr.GetOrderLocationTransaction(orderDetail.Id, BusinessConstants.IO_TYPE_OUT);
                                foreach (OrderLocationTransaction orderLocTrans in orderLocTransList)
                                {
                                    MaterialFlushBack material = new MaterialFlushBack();
                                    material.OrderLocationTransaction = orderLocTrans;
                                    if (orderLocTrans.UnitQty != 0)
                                    {
                                        material.Qty = qty;
                                    }
                                    receiptDetail.AddMaterialFlushBack(material);
                                }
                                #endregion
                                receipt.AddReceiptDetail(receiptDetail);

                                this.orderManager.ReceiveOrder(receipt, this.userMgr.GetMonitorUser());
                            }
                            else
                            {
                                throw new BusinessErrorException("Hu " + huId + " already exist in database.");
                            }
                            #endregion
                        }
                        else
                        {
                            throw new BusinessErrorException("No item found for item code " + itemCode + " for prodline + " + prodLine + ", shift " + shiftCode);
                        }
                    }

                    #region 创建内包装条码
                    if (this.huMgr.LoadHu(itemHuId) == null)
                    {
                        log.Info("Insert hu " + itemHuId + " into database.");
                        CreateItemHu(itemHuId, orderDetail, hu.LotNo, hu.ManufactureDate);
                    }
                    else
                    {
                        throw new BusinessErrorException("Hu " + itemHuId + " already exist in database.");
                    }
                    #endregion

                    fields = reader.ReadLine();
                }
            }
            finally
            {
                reader.Dispose();
            }
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
    }
}
