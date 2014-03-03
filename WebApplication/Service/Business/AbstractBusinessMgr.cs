using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.MasterData;
using NHibernate.Expression;
using Castle.Services.Transaction;

namespace com.Sconit.Service.Business
{
    public abstract class AbstractBusinessMgr : IBusinessMgr
    {
        private Dictionary<string, int> dicBarcodeHead = new Dictionary<string, int>();

        public AbstractBusinessMgr()
        {
            dicBarcodeHead.Add(BusinessConstants.BARCODE_HEAD_DEFAULT, 0);
            #region Order Entity
            dicBarcodeHead.Add(BusinessConstants.CODE_PREFIX_ORDER, 1);
            dicBarcodeHead.Add(BusinessConstants.CODE_PREFIX_PICKLIST, 1);
            dicBarcodeHead.Add(BusinessConstants.CODE_PREFIX_ASN, 1);
            dicBarcodeHead.Add(BusinessConstants.CODE_PREFIX_INSPECTION, 1);
            dicBarcodeHead.Add(BusinessConstants.BARCODE_HEAD_FLOW, 1);
            dicBarcodeHead.Add(BusinessConstants.CODE_PREFIX_CYCCNT, 1);
            #endregion

            #region Command
            dicBarcodeHead.Add(BusinessConstants.BARCODE_HEAD_OK, 2);
            dicBarcodeHead.Add(BusinessConstants.BARCODE_HEAD_CANCEL, 2);
            dicBarcodeHead.Add(BusinessConstants.BARCODE_HEAD_PRINT, 2);
            #endregion

            #region List
            dicBarcodeHead.Add(BusinessConstants.BARCODE_HEAD_NOTE, 3);
            #endregion

            #region Bin Location
            dicBarcodeHead.Add(BusinessConstants.BARCODE_HEAD_BIN, 4);
            dicBarcodeHead.Add(BusinessConstants.BARCODE_HEAD_LOCATION, 4);
            #endregion


        }

        public void Process(Resolver resolver)
        {
            if (!dicBarcodeHead.ContainsKey(resolver.BarcodeHead))
            {
                throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
            }
            switch (dicBarcodeHead[resolver.BarcodeHead])
            {
                case 1:
                    SetBaseInfo(resolver);
                    GetDetail(resolver);
                    break;
                case 2:
                    Execute(resolver);
                    break;
                case 3:
                    GetReceiptNotes(resolver);
                    break;
                case 4:
                    SetBaseInfo(resolver);
                    break;
                default:
                    resolver.Input = resolver.Input;
                    SetDetail(resolver);//Hu,match or add
                    break;
            }
        }

        protected abstract void SetBaseInfo(Resolver resolver);
        protected abstract void GetDetail(Resolver resolver);
        protected abstract void SetDetail(Resolver resolver);
        protected abstract void ExecuteSubmit(Resolver resolver);
        protected abstract void ExecuteCancel(Resolver resolver);
        protected abstract void ExecutePrint(Resolver resolver);
        protected abstract void GetReceiptNotes(Resolver resolver);

        private void Execute(Resolver resolver)
        {
            if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_OK)
            {
                ExecuteSubmit(resolver);
            }
            else if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_CANCEL)
            {
                ExecuteCancel(resolver);
            }
            else if (resolver.BarcodeHead == BusinessConstants.BARCODE_HEAD_PRINT)
            {
                ExecutePrint(resolver);
            }
        }
    }
}
