using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using Castle.Services.Transaction;

namespace com.Sconit.Service.Business.Impl
{
    public class PutAwayMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private ILanguageMgr languageMgr;
        private IStorageBinMgr storageBinMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private ILocationMgr locationMgr;
        private IHuMgr huMgr;

        public PutAwayMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            ILanguageMgr languageMgr,
            IStorageBinMgr storageBinMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            ILocationMgr locationMgr,
            IHuMgr huMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.languageMgr = languageMgr;
            this.storageBinMgr = storageBinMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.locationMgr = locationMgr;
            this.huMgr = huMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
            if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_BIN)
            {
                setBaseMgr.FillResolverByBin(resolver);
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
            Hu hu = huMgr.CheckAndLoadHu(resolver.Input);
            if (this.locationMgr.IsHuOcuppyByPickList(resolver.Input))
            {
                throw new BusinessErrorException("Order.Error.PickUp.HuOcuppied", resolver.Input);
            }
            if (resolver.BinCode == string.Empty || resolver.BinCode == null)
            {
                throw new BusinessErrorException("Warehouse.PutAway.PlzScanBin");
            }
            if (hu.StorageBin != null && hu.StorageBin.Trim() != string.Empty && hu.StorageBin == resolver.BinCode)
            {
                throw new BusinessErrorException("Warehouse.PutAway.AlreadyInThisBin");
            }
            //校验Bin
            StorageBin bin = storageBinMgr.CheckAndLoadStorageBin(resolver.BinCode);
            LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(resolver.Input, string.Empty, bin.Area.Location.Code);
            locationLotDetail.NewStorageBin = bin;
            TransformerDetail transformerDetail = TransformerHelper.ConvertLocationLotDetailToTransformerDetail(locationLotDetail, true);
            resolver.AddTransformerDetail(transformerDetail);
        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
            if (resolver.Transformers == null || resolver.Transformers.Count < 1)
            {
                throw new BusinessErrorException("PutAway.Error.HuDetailEmpty");
            }

            IList<LocationLotDetail> locationLotDetailList = executeMgr.ConvertTransformersToLocationLotDetails(resolver.Transformers, true);
            locationMgr.InventoryPut(locationLotDetailList, resolver.UserCode);
            resolver.Result = languageMgr.TranslateMessage("Warehouse.PutAway.Successfully", resolver.UserCode);
            resolver.Transformers = null;
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        protected override void ExecuteCancel(Resolver resolver)
        {
            executeMgr.CancelOperation(resolver);
        }

        /// <summary>
        /// 初始化上架 Transformers和TransformerDetails
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        private List<Transformer> FillPutAway(Resolver resolver)
        {
            List<Transformer> transformerList = resolver.Transformers;
            if (transformerList == null)
            {
                transformerList = new List<Transformer>();
                transformerList.Add(new Transformer());
                transformerList[0].TransformerDetails = new List<TransformerDetail>();
            }
            return transformerList;
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
