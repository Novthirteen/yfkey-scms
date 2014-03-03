using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;
using Castle.Services.Transaction;

namespace com.Sconit.Service.Business.Impl
{
    /// <summary>
    /// 解析条码,指定Service.Impl
    /// </summary>
    public class ResolverMgr : IResolverMgr
    {
        private IDictionary<string, IBusinessMgr> dicService = new Dictionary<string, IBusinessMgr>();
        //private static log4net.ILog log = log4net.LogManager.GetLogger("Log.Resolver");

        private IBusinessMgr shipMgr;
        private IBusinessMgr receiveMgr;
        private IBusinessMgr pickupMgr;
        private IBusinessMgr putAwayMgr;
        private IBusinessMgr onlineMgr;
        private IBusinessMgr offlineMgr;
        private IBusinessMgr inspectMgr;
        private IBusinessMgr transferMgr;
        private IBusinessMgr pickMgr;
        private IBusinessMgr repackageMgr;
        private IBusinessMgr returnMgr;
        private IBusinessMgr materialInMgr;
        private IBusinessMgr flushBackMgr;
        private IBusinessMgr inspectionMgr;
        private IBusinessMgr devanningMgr;
        private IBusinessMgr stockTakingMgr;
        private IBusinessMgr pickListOnlineMgr;
        private IBusinessMgr reuseMgr;
        private IBusinessMgr inspectConfirmNgr;
        private IBusinessMgr printingMgr;
        private IBusinessMgr batchDevanningMgr;

        public ResolverMgr(
            IBusinessMgr shipMgr,
            IBusinessMgr receiveMgr,
            IBusinessMgr pickupMgr,
            IBusinessMgr putAwayMgr,
            IBusinessMgr onlineMgr,
            IBusinessMgr offlineMgr,
            IBusinessMgr inspectMgr,
            IBusinessMgr transferMgr,
            IBusinessMgr pickMgr,
            IBusinessMgr repackageMgr,
            IBusinessMgr returnMgr,
            IBusinessMgr materialInMgr,
            IBusinessMgr flushBackMgr,
            IBusinessMgr inspectionMgr,
            IBusinessMgr devanningMgr,
            IBusinessMgr stockTakingMgr,
            IBusinessMgr pickListOnlineMgr,
            IBusinessMgr reuseMgr,
            IBusinessMgr inspectConfirmNgr,
            IBusinessMgr printingMgr,
            IBusinessMgr batchDevanningMgr
            )
        {
            this.shipMgr = shipMgr;
            this.receiveMgr = receiveMgr;
            this.pickupMgr = pickupMgr;
            this.putAwayMgr = putAwayMgr;
            this.onlineMgr = onlineMgr;
            this.offlineMgr = offlineMgr;
            this.inspectMgr = inspectMgr;
            this.transferMgr = transferMgr;
            this.pickMgr = pickMgr;
            this.repackageMgr = repackageMgr;
            this.returnMgr = returnMgr;
            this.materialInMgr = materialInMgr;
            this.flushBackMgr = flushBackMgr;
            this.inspectionMgr = inspectionMgr;
            this.devanningMgr = devanningMgr;
            this.stockTakingMgr = stockTakingMgr;
            this.pickListOnlineMgr = pickListOnlineMgr;
            this.reuseMgr = reuseMgr;
            this.inspectConfirmNgr = inspectConfirmNgr;
            this.printingMgr = printingMgr;
            this.batchDevanningMgr = batchDevanningMgr;

            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP, shipMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE, receiveMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKUP, pickupMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PUTAWAY, putAwayMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_ONLINE, onlineMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_OFFLINE, offlineMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECT, inspectMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_TRANSFER, transferMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST, pickMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_REPACK, repackageMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPRETURN, returnMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN, returnMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPCONFIRM, receiveMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_MATERIALIN, materialInMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_FLUSHBACK, flushBackMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECTION, inspectionMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING, devanningMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_STOCKTAKING, stockTakingMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PRODUCTIONRECEIVE, receiveMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLISTONLINE, pickListOnlineMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_HUSTATUS, stockTakingMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_REUSE, reuseMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPORDER, shipMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECTCONFIM, inspectConfirmNgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_PRINTING, printingMgr);
            dicService.Add(BusinessConstants.TRANSFORMER_MODULE_TYPE_BATCHDEVANNING, batchDevanningMgr);

        }

        #region Public Method
        //[Transaction(TransactionMode.Unspecified)]
        public Resolver Resolve(Resolver resolver)
        {
            //inputResolver.Result = null;
            resolver.Command = null;
            this.CheckValid(resolver);

            //Resolver resolver = CloneHelper.DeepClone<Resolver>(inputResolver);

            this.AnalyzeBarcode(resolver);

            IBusinessMgr processer = dicService[resolver.ModuleType];
            processer.Process(resolver);

            resolver.Input = null;
            return resolver;
        }
        #endregion

        #region Private Method
        private void CheckValid(Resolver resolver)
        {
            if (resolver == null)
            {
                throw new TechnicalException("resolver is null");
                //log.Error("resolver is null");
            }

            if (resolver.UserCode == null || resolver.UserCode.Trim() == string.Empty)
            {
                throw new TechnicalException("resolver.UserCode is null");
                //log.Error("resolver.UserCode is null");
            }

            if (resolver.ModuleType == null || resolver.ModuleType.Trim() == string.Empty)
            {
                throw new TechnicalException("resolver.ModuleType is null");
                //log.Error("resolver.ModuleType is null");
            }
        }

        private void AnalyzeBarcode(Resolver resolver)
        {
            string barcode = resolver.Input;
            if (barcode == null || barcode.Trim() == string.Empty)
            {
                throw new TechnicalException("resolver.Input is null");
            }

            //Special,"$"
            if (barcode.StartsWith(BusinessConstants.BARCODE_SPECIAL_MARK))
            {
                if (barcode.Length < 2)
                {
                    throw new TechnicalException("resolver.Input.length < 2 when ");
                }
                resolver.BarcodeHead = barcode.Substring(1, 1);
                resolver.Input = barcode.Substring(2, barcode.Length - 2);
                string codePrefix = barcode.Substring(1, 1);
                if (codePrefix == BusinessConstants.BARCODE_HEAD_FLOW)
                {
                    resolver.CodePrefix = codePrefix;
                }
            }
            else
            {
                //Order
                if (barcode.StartsWith(BusinessConstants.CODE_PREFIX_ORDER))
                {                    
                    if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_PICKLIST)
                    {
                        resolver.Transformers = null;
                    }
                    resolver.BarcodeHead = BusinessConstants.CODE_PREFIX_ORDER;
                    resolver.CodePrefix = BusinessConstants.CODE_PREFIX_ORDER;
                }
                //PickList
                else if (barcode.StartsWith(BusinessConstants.CODE_PREFIX_PICKLIST))
                {
                    resolver.BarcodeHead = BusinessConstants.CODE_PREFIX_PICKLIST;
                    resolver.CodePrefix = BusinessConstants.CODE_PREFIX_PICKLIST;
                }
                //ASN
                else if (barcode.StartsWith(BusinessConstants.CODE_PREFIX_ASN))
                {
                    resolver.BarcodeHead = BusinessConstants.CODE_PREFIX_ASN;
                    resolver.CodePrefix = BusinessConstants.CODE_PREFIX_ASN;
                }
                //Inspection
                else if (barcode.StartsWith(BusinessConstants.CODE_PREFIX_INSPECTION))
                {
                    resolver.BarcodeHead = BusinessConstants.CODE_PREFIX_INSPECTION;
                    resolver.CodePrefix = BusinessConstants.CODE_PREFIX_INSPECTION;
                }
                //StockTaking
                else if (barcode.StartsWith(BusinessConstants.CODE_PREFIX_CYCCNT))
                {
                    resolver.BarcodeHead = BusinessConstants.CODE_PREFIX_CYCCNT;
                    resolver.CodePrefix = BusinessConstants.CODE_PREFIX_CYCCNT;
                }
                else
                {
                    resolver.BarcodeHead = BusinessConstants.BARCODE_HEAD_DEFAULT;
                }
            }
        }
        #endregion
    }
}
