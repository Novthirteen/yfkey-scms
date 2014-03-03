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
    public class InspectMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private IInspectOrderMgr inspectOrderMgr;
        private ILanguageMgr languageMgr;
        private IHuMgr huMgr;

        public InspectMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            IInspectOrderMgr inspectOrderMgr,
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
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_INSPECTION)
            {
                InspectOrder inspectOrder = inspectOrderMgr.CheckAndLoadInspectOrder(resolver.Input);
                if (inspectOrder.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                {
                    throw new BusinessErrorException("InspectOrder.Error.StatusIsNotValid", resolver.Input, inspectOrder.Status);
                }
                resolver.Code = inspectOrder.InspectNo;
                resolver.IsScanHu = inspectOrder.IsDetailHasHu;
                resolver.Status = inspectOrder.Status;
                if (resolver.IsScanHu)
                {
                    resolver.PickBy = BusinessConstants.CODE_MASTER_PICKBY_HU;
                }
                else
                {
                    resolver.PickBy = BusinessConstants.CODE_MASTER_PICKBY_ITEM;
                }
            }
            else
            {
                throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
            }
        }

        protected override void GetDetail(Resolver resolver)
        {
            InspectOrder inspectOrder = inspectOrderMgr.LoadInspectOrder(resolver.Code, true);
            resolver.Transformers = TransformerHelper.ConvertInspectDetailToTransformer(inspectOrder.InspectOrderDetails);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
            resolver.Result = languageMgr.TranslateMessage("MasterData.Inventory.CurrentInspectOrder", resolver.UserCode, resolver.Code);
        }

        protected override void SetDetail(Resolver resolver)
        {
            if (resolver.CodePrefix == string.Empty)
            {
                throw new BusinessErrorException("Common.Business.Error.ScanFlowFirst");
            }
            setDetailMgr.MatchInspet(resolver);
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void ExecuteSubmit(Resolver resolver)
        {
            IList<InspectOrderDetail> inspectDetailList = executeMgr.ConvertResolverToInspectOrderDetails(resolver);
            inspectOrderMgr.PendInspectOrder(inspectDetailList, resolver.UserCode);
            resolver.Result = languageMgr.TranslateMessage("MasterData.InspectOrder.Process.Successfully", resolver.UserCode, resolver.Code);
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
