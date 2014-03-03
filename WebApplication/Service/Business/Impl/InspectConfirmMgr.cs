using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using Castle.Services.Transaction;

namespace com.Sconit.Service.Business.Impl
{
    public class InspectConfirmMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private IInspectOrderMgr inspectOrderMgr;
        private IInspectOrderDetailMgr inspectOrderDetailMgr;
        private ILanguageMgr languageMgr;
        private IHuMgr huMgr;

        public InspectConfirmMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            IInspectOrderMgr inspectOrderMgr,
            IInspectOrderDetailMgr inspectOrderDetailMgr,
            ILanguageMgr languageMgr,
            IHuMgr huMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.inspectOrderMgr = inspectOrderMgr;
            this.languageMgr = languageMgr;
            this.huMgr = huMgr;
            this.inspectOrderDetailMgr = inspectOrderDetailMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {

        }

        protected override void GetDetail(Resolver resolver)
        {

        }

        protected override void SetDetail(Resolver resolver)
        {
            InspectOrderDetail inspectOrderDetail = inspectOrderDetailMgr.CheckAndGetInspectOrderDetail(resolver.Input);
            TransformerDetail transformerDetail = TransformerHelper.ConvertInspectDetailToTransformerDetail(inspectOrderDetail);
            resolver.AddTransformerDetail(transformerDetail);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void ExecuteSubmit(Resolver resolver)
        {
            IList<InspectOrderDetail> inspectDetailList = executeMgr.ConvertResolverToInspectOrderDetails(resolver);
            inspectOrderMgr.ProcessInspectOrder(inspectDetailList, resolver.UserCode);
            resolver.Result = languageMgr.TranslateMessage("MasterData.InspectOrder.Confirm.Successfully", resolver.UserCode, resolver.Code);
            resolver.Transformers = null;
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
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
