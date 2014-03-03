using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using Castle.Services.Transaction;

namespace com.Sconit.Service.Business.Impl
{
    public class InspectionMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private IUserMgr userMgr;
        private IInspectOrderMgr inspectOrderMgr;
        private ILanguageMgr languageMgr;

        public InspectionMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            IUserMgr userMgr,
            IInspectOrderMgr inspectOrderMgr,
            ILanguageMgr languageMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.userMgr = userMgr;
            this.inspectOrderMgr = inspectOrderMgr;
            this.languageMgr = languageMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
        }

        protected override void GetDetail(Resolver resolver)
        {
        }

        protected override void SetDetail(Resolver resolver)
        {
            LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(resolver.Input, resolver.UserCode);
            TransformerDetail transformerDetail = TransformerHelper.ConvertLocationLotDetailToTransformerDetail(locationLotDetail, false);
            resolver.AddTransformerDetail(transformerDetail);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
            this.CreateInspectOrder(resolver);
        }

        protected override void ExecuteCancel(Resolver resolver)
        {
            executeMgr.CancelOperation(resolver);
        }

        /// <summary>
        /// 报验单
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public void CreateInspectOrder(Resolver resolver)
        {
            IList<LocationLotDetail> locationLotDetailList = executeMgr.ConvertTransformersToLocationLotDetails(resolver.Transformers, false);
            if (locationLotDetailList.Count == 0)
            {
                throw new BusinessErrorException("MasterData.Inventory.Repack.Error.RepackDetailEmpty");
            }
            User user = userMgr.LoadUser(resolver.UserCode, false, true);
            InspectOrder inspectOrder = inspectOrderMgr.CreateInspectOrder(locationLotDetailList, user);
            resolver.Result = languageMgr.TranslateMessage("MasterData.InspectOrder.Create.Successfully", resolver.UserCode, inspectOrder.InspectNo);
            resolver.Transformers = null;
            resolver.Code = inspectOrder.InspectNo;
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
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
