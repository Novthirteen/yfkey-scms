using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using Castle.Services.Transaction;

namespace com.Sconit.Service.Business.Impl
{
    public class PickupMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private ILocationMgr locationMgr;
        private ILanguageMgr languageMgr;

        public PickupMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            ILocationMgr locationMgr,
            ILanguageMgr languageMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.locationMgr = locationMgr;
            this.languageMgr = languageMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
            throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
        }

        protected override void GetDetail(Resolver resolver)
        {
        }

        protected override void SetDetail(Resolver resolver)
        {
            //setDetailMgr.CheckHuInTransformerDetails(resolver);
            LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(resolver.Input, resolver.UserCode);
            //Hu hu = locationLotDetail.Hu;
            //hu.Qty = locationLotDetail.Qty / hu.UnitQty;
            if (this.locationMgr.IsHuOcuppyByPickList(resolver.Input))
            {
                throw new BusinessErrorException("Order.Error.PickUp.HuOcuppied", resolver.Input);
            }

            //已经下架
            if (locationLotDetail.StorageBin == null)
            {
                throw new BusinessErrorException("Warehouse.PickUp.Error.IsAlreadyPickUp", resolver.Input);
            }
            TransformerDetail transformerDetail = TransformerHelper.ConvertLocationLotDetailToTransformerDetail(locationLotDetail, false);
            resolver.AddTransformerDetail(transformerDetail);
            //transformerDetail.CurrentQty = transformerDetail.Qty;
            //int maxSeq = setDetailMgr.FindMaxSeq(resolver.Transformers);
            //transformerDetail.Sequence = maxSeq + 1;
            //resolver.Transformers[0].TransformerDetails.Add(transformerDetail);
            //resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
            if (resolver.Transformers == null || resolver.Transformers.Count < 1)
            {
                throw new BusinessErrorException("PickUp.Error.HuDetailEmpty");
            }

            IList<LocationLotDetail> locationLotDetailList = executeMgr.ConvertTransformersToLocationLotDetails(resolver.Transformers, false);
            locationMgr.InventoryPick(locationLotDetailList, resolver.UserCode);
            resolver.Result = languageMgr.TranslateMessage("Warehouse.PickUp.Successfully", resolver.UserCode);
            resolver.Transformers = null;
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
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
