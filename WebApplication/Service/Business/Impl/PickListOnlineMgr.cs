using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using Castle.Services.Transaction;

namespace com.Sconit.Service.Business.Impl
{
    public class PickListOnlineMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private IPickListMgr pickListMgr;
        private IUserMgr userMgr;

        public PickListOnlineMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            IPickListMgr pickListMgr,
            IUserMgr userMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.pickListMgr = pickListMgr;
            this.userMgr = userMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
            this.StartPickList(resolver);
        }

        protected override void GetDetail(Resolver resolver)
        {
        }

        protected override void SetDetail(Resolver resolver)
        {
            this.StartPickList(resolver);
        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
        }

        protected override void ExecuteCancel(Resolver resolver)
        {
        }

        private void StartPickList(Resolver resolver)
        {
            if (resolver.BarcodeHead == BusinessConstants.CODE_PREFIX_PICKLIST)
            {
                //setBaseMgr.FillResolverByPickList(resolver);
                pickListMgr.StartPickList(resolver.Input, userMgr.CheckAndLoadUser(resolver.UserCode));
                resolver.Result = DateTime.Now.ToString("HH:mm:ss");
                resolver.Code = resolver.Input;
            }
            else
            {
                throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
            }

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
