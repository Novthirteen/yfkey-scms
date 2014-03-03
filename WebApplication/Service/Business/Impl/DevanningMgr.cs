using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;

namespace com.Sconit.Service.Business.Impl
{
    public class DevanningMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private IRepackMgr repackMgr;
        private IUserMgr userMgr;
        private ILanguageMgr languageMgr;

        public DevanningMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            IRepackMgr repackMgr,
            IUserMgr userMgr,
            ILanguageMgr languageMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.repackMgr = repackMgr;
            this.userMgr = userMgr;
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
            setDetailMgr.MatchRepack(resolver);
        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
            this.CreateDevanning(resolver);
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
        /// <summary>
        /// 拆箱
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public void CreateDevanning(Resolver resolver)
        {
            IList<RepackDetail> repackDetailList = executeMgr.ConvertTransformerListToRepackDetail(resolver.Transformers);
            if (repackDetailList.Count == 0)
            {
                throw new BusinessErrorException("MasterData.Inventory.Repack.Error.RepackDetailEmpty");
            }
            //KSS 客户化需求, 拆箱并上架
            foreach (RepackDetail repackDetail in repackDetailList)
            {
                repackDetail.StorageBinCode = resolver.BinCode;
            }
            Repack repack = repackMgr.CreateDevanning(repackDetailList, this.userMgr.LoadUser(resolver.UserCode, false, true));
            resolver.Code = repack.RepackNo;
            resolver.Result = languageMgr.TranslateMessage("MasterData.Inventory.Devanning.Successfully", resolver.UserCode, resolver.Transformers[0].TransformerDetails[0].HuId);
            resolver.Transformers = null;
        }

    }
}
