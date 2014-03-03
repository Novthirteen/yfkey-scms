using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Production;
using Castle.Services.Transaction;

namespace com.Sconit.Service.Business.Impl
{
    public class MaterialInMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private IUserMgr userMgr;
        private IItemMgr itemMgr;
        private ILocationMgr locationMgr;
        private IProductLineInProcessLocationDetailMgr productLineInProcessLocationDetailMgr;
        private ILanguageMgr languageMgr;
        private IBomDetailMgr bomDetailMgr;
        private IHuMgr huMgr;
        private IFlowMgr flowMgr;
        private IRoutingDetailMgr routingDetailMgr;

        public MaterialInMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            IUserMgr userMgr,
            IItemMgr itemMgr,
            ILocationMgr locationMgr,
            IProductLineInProcessLocationDetailMgr productLineInProcessLocationDetailMgr,
            ILanguageMgr languageMgr,
            IBomDetailMgr bomDetailMgr,
            IHuMgr huMgr,
            IFlowMgr flowMgr,
            IRoutingDetailMgr routingDetailMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.userMgr = userMgr;
            this.itemMgr = itemMgr;
            this.locationMgr = locationMgr;
            this.productLineInProcessLocationDetailMgr = productLineInProcessLocationDetailMgr;
            this.languageMgr = languageMgr;
            this.bomDetailMgr = bomDetailMgr;
            this.huMgr = huMgr;
            this.flowMgr = flowMgr;
            this.routingDetailMgr = routingDetailMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
            setBaseMgr.FillResolverByFlow(resolver);
            if (resolver.OrderType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                throw new BusinessErrorException("Flow.ShipReturn.Error.FlowTypeIsNotDistribution", resolver.OrderType);
            }
        }

        /// <summary>
        /// Todo:需要删除重复项?
        /// </summary>
        /// <param name="resolver"></param>
        protected override void GetDetail(Resolver resolver)
        {
            setDetailMgr.SetMateria(resolver);
        }

        /// <summary>
        /// 仅校验投料的物料号,库位是否一致,不校验单位单包装等信息
        /// todo:不允许投入的又有数量又有Hu //可以前台控制
        /// </summary>
        /// <param name="resolver"></param>
        protected override void SetDetail(Resolver resolver)
        {
            if (resolver.CodePrefix == string.Empty)
            {
                throw new BusinessErrorException("Common.Business.Error.ScanProductLineFirst");
            }
            LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(resolver.Input, resolver.UserCode);
            TransformerDetail transformerDetail = TransformerHelper.ConvertLocationLotDetailToTransformerDetail(locationLotDetail, false);
            var query = resolver.Transformers.Where
                    (t => (t.ItemCode == transformerDetail.ItemCode && t.LocationCode == transformerDetail.LocationCode));
            if (query.Count() < 1)
            {
                throw new BusinessErrorException("Warehouse.HuMatch.NotMatch", transformerDetail.HuId);
            }

            #region 先进先出校验
            Flow flow = flowMgr.CheckAndLoadFlow(resolver.Code);
            if (flow.IsGoodsReceiveFIFO)
            {
                Hu hu = huMgr.CheckAndLoadHu(resolver.Input);
                IList<string> huIdList = new List<string>();
                if (resolver.Transformers != null && resolver.Transformers.Count > 0)
                {
                    foreach (Transformer transformer in resolver.Transformers)
                    {
                        if (transformer.TransformerDetails != null && transformer.TransformerDetails.Count > 0)
                        {
                            foreach (TransformerDetail det in transformer.TransformerDetails)
                            {
                                if (det.CurrentQty != decimal.Zero)
                                {
                                    huIdList.Add(det.HuId);
                                }
                            }
                        }
                    }
                }
                string maxLot = setDetailMgr.CheckFIFO(hu, huIdList);
                if (maxLot != string.Empty && maxLot != null)
                {
                    throw new BusinessErrorException("FIFO.ERROR", hu.HuId, maxLot);
                }
            }
            #endregion

            resolver.AddTransformerDetail(transformerDetail);

        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
            IList<MaterialIn> materialInList = executeMgr.ConvertTransformersToMaterialIns(resolver.Transformers);
            productLineInProcessLocationDetailMgr.RawMaterialIn(resolver.Code, materialInList, userMgr.CheckAndLoadUser(resolver.UserCode));
            resolver.Transformers = null;
            resolver.Result = languageMgr.TranslateMessage("MasterData.MaterialIn.Successfully", resolver.UserCode, resolver.Code);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }

        protected override void ExecuteCancel(Resolver resolver)
        {
            executeMgr.CancelRepackOperation(resolver);
        }
        
        [Transaction(TransactionMode.Unspecified)]
        protected override void ExecutePrint(Resolver resolver)
        {
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void GetReceiptNotes(Resolver resolver)
        {
        }
    }
}
