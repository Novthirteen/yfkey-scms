using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Utility;

namespace com.Sconit.Service.Business.Impl
{
    public class FlushBackMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private IProductLineInProcessLocationDetailMgr productLineInProcessLocationDetailMgr;
        private IUserMgr userMgr;
        private ILanguageMgr languageMgr;
        private IHuMgr huMgr;

        public FlushBackMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            IProductLineInProcessLocationDetailMgr productLineInProcessLocationDetailMgr,
            IUserMgr userMgr,
            ILanguageMgr languageMgr,
            IHuMgr huMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.productLineInProcessLocationDetailMgr = productLineInProcessLocationDetailMgr;
            this.userMgr = userMgr;
            this.languageMgr = languageMgr;
            this.huMgr = huMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
            setBaseMgr.FillResolverByFlow(resolver);
            if (resolver.OrderType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                throw new BusinessErrorException("Flow.ShipReturn.Error.FlowTypeIsNotDistribution", resolver.OrderType);
            }
        }

        protected override void GetDetail(Resolver resolver)
        {
            setDetailMgr.SetMateria(resolver);
            var q = resolver.Transformers.Where(t => t.Qty > 0);
            resolver.Transformers = q.ToList();
        }

        protected override void SetDetail(Resolver resolver)
        {
            if (resolver.CodePrefix == string.Empty)
            {
                throw new BusinessErrorException("Common.Business.Error.ScanProductLineFirst");
            }
        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
            User user = userMgr.LoadUser(resolver.UserCode, false, true);
            IDictionary<string, decimal> itemDictionary = new Dictionary<string, decimal>();
            foreach (Transformer transformer in resolver.Transformers)
            {
                itemDictionary.Add(transformer.ItemCode, transformer.CurrentQty);
            }
            productLineInProcessLocationDetailMgr.RawMaterialBackflush(resolver.Code, itemDictionary, user);
            resolver.Result = languageMgr.TranslateMessage("MasterData.BackFlush.Successfully", resolver.UserCode, resolver.Code);
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
