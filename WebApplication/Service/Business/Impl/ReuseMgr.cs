using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.View;

namespace com.Sconit.Service.Business.Impl
{
    public class ReuseMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private IFlowMgr flowMgr;
        private IHuMgr huMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private ILanguageMgr languageMgr;
        private IOrderMgr orderMgr;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        private IUserMgr userMgr;

        public ReuseMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            IFlowMgr flowMgr,
            IHuMgr huMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            ILanguageMgr languageMgr,
            IOrderMgr orderMgr,
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            IUserMgr userMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.flowMgr = flowMgr;
            this.huMgr = huMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.languageMgr = languageMgr;
            this.orderMgr = orderMgr;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.userMgr = userMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
            if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_FLOW)
            {
                setBaseMgr.FillResolverByFlow(resolver);
                if (resolver.OrderType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                {
                    throw new BusinessErrorException("Flow.Error.FlowTypeIsNotProductLine", resolver.OrderType);
                }
            }
            else
            {
                throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
            }
        }

        protected override void GetDetail(Resolver resolver)
        {

        }

        protected override void SetDetail(Resolver resolver)
        {
            List<string> flowTypes = new List<string>();
            flowTypes.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION);
            bool isHaslocationLotDetail = locationLotDetailMgr.CheckHuLocationExist(resolver.Input);
            if (!isHaslocationLotDetail)
            {
                throw new BusinessErrorException("Hu.Error.NoInventory", resolver.Input);
            }
            Hu hu = huMgr.CheckAndLoadHu(resolver.Input);
            FlowView flowView = null;
            //如果是扫描Bin,根据Hu和Bin匹配出flow
            if (resolver.CodePrefix == null || resolver.CodePrefix.Trim() == string.Empty)
            {
                //确定flow和flowView
                flowView = flowMgr.CheckAndLoadFlowView(null, resolver.UserCode, string.Empty, null, hu, flowTypes);
                setBaseMgr.FillResolverByFlow(resolver, flowView.Flow);
            }
            //如果已经确定了Flow
            else
            {
                //根据Flow和Hu匹配出flowView
                flowView = flowMgr.CheckAndLoadFlowView(resolver.Code, null, null, null, hu, flowTypes);
            }
            setDetailMgr.MatchHuByFlowView(resolver, flowView, hu);
        }

        [Transaction(TransactionMode.Requires)]
        protected override void ExecuteSubmit(Resolver resolver)
        {
            IList<Hu> huList = new List<Hu>();
            if (resolver.Transformers != null)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer.TransformerDetails != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            Hu hu = huMgr.LoadHu(transformerDetail.HuId);
                            huList.Add(hu);
                        }
                    }
                }
            }
            if (huList.Count > 0)
            {
                orderMgr.CreateOrder(resolver.Code, resolver.UserCode, huList);
                resolver.Result = languageMgr.TranslateMessage("Order.Reuse.Successfully", resolver.UserCode);
                resolver.Transformers = null;
                resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
            }
            else
            {
                throw new BusinessErrorException("Common.Business.Error.OprationFailed");
            }
        }

        protected override void ExecuteCancel(Resolver resolver)
        {
            executeMgr.CancelOperation(resolver);
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
