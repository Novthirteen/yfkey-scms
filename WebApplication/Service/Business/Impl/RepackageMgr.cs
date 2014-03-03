using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Report;

namespace com.Sconit.Service.Business.Impl
{
    public class RepackageMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private IRepackMgr repackMgr;
        private IUserMgr userMgr;
        private ILanguageMgr languageMgr;
        private IReportMgr reportMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;

        public RepackageMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            IRepackMgr repackMgr,
            IUserMgr userMgr,
            ILanguageMgr languageMgr,
            IReportMgr reportMgr,
            IEntityPreferenceMgr entityPreferenceMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.repackMgr = repackMgr;
            this.userMgr = userMgr;
            this.languageMgr = languageMgr;
            this.reportMgr = reportMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
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
            this.CreateRepack(resolver);
        }

        protected override void ExecuteCancel(Resolver resolver)
        {
            executeMgr.CancelRepackOperation(resolver);
        }

        /// <summary>
        /// 翻箱
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public void CreateRepack(Resolver resolver)
        {
            IList<RepackDetail> repackDetailList = executeMgr.ConvertTransformerListToRepackDetail(resolver.Transformers);
            if (repackDetailList.Count == 0)
            {
                throw new BusinessErrorException("MasterData.Inventory.Repack.Error.RepackDetailEmpty");
            }
            Repack repack = repackMgr.CreateRepack(repackDetailList, userMgr.LoadUser(resolver.UserCode, false, true));
            resolver.Code = repack.RepackNo;
            resolver.Transformers = null;
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
            resolver.Result = languageMgr.TranslateMessage("MasterData.Inventory.Repack.Successfully", resolver.UserCode);


            #region 打印
            if (resolver.IsCSClient)
            {
                IList<Hu> huList = new List<Hu>();
                repack = repackMgr.LoadRepack(resolver.Code, true);
                foreach (RepackDetail repackDet in repack.RepackDetails)
                {
                    if (repackDet.IOType == BusinessConstants.IO_TYPE_OUT && repackDet.LocationLotDetail.Hu != null
                        && repackDet.LocationLotDetail.Hu.PrintCount == 0)
                    {
                        huList.Add(repackDet.LocationLotDetail.Hu);
                    }
                }
                resolver.PrintUrl = PrintHu(huList,resolver.UserCode);
            }
            #endregion
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void ExecutePrint(Resolver resolver)
        {
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void GetReceiptNotes(Resolver resolver)
        {
        }

        private string PrintHu(IList<Hu> huList,string userCode)
        {
            
            IList<object> list = new List<object>();
            list.Add(huList);
            list.Add(userCode);

            string huTemplate = entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_HU_TEMPLATE).Value;

            string printUrl = reportMgr.WriteToFile(huTemplate, list);
            return printUrl;
        }
    }
}
