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
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.Report;

namespace com.Sconit.Service.Business.Impl
{
    public class PickMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private IPickListMgr pickListMgr;
        private ILanguageMgr languageMgr;
        private IPickListDetailMgr pickListDetailMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private ICriteriaMgr criteriaMgr;
        private IReportMgr reportMgr;

        public PickMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            IPickListMgr pickListMgr,
            ILanguageMgr languageMgr,
            IPickListDetailMgr pickListDetailMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            ICriteriaMgr criteriaMgr,
            IReportMgr reportMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.pickListMgr = pickListMgr;
            this.languageMgr = languageMgr;
            this.pickListDetailMgr = pickListDetailMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.criteriaMgr = criteriaMgr;
            this.reportMgr = reportMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_PICKLIST)
            {
                setBaseMgr.FillResolverByPickList(resolver);

                if (resolver.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
                {
                    throw new BusinessErrorException("Common.Business.Error.StatusError", resolver.Code, resolver.Status);
                }
            }
            else
            {
                throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
            }
        }

        protected override void GetDetail(Resolver resolver)
        {
            PickList pickList = pickListMgr.LoadPickList(resolver.Input, true, true);

            resolver.Transformers = TransformerHelper.ConvertPickListDetailsToTransformers(pickList.PickListDetails);
            resolver.Result = languageMgr.TranslateMessage("Common.Business.PickList", resolver.UserCode) + ":" + resolver.Code;
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }

        protected override void SetDetail(Resolver resolver)
        {
            setDetailMgr.MatchShip(resolver);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
            this.PickList(resolver);
        }

        protected override void ExecuteCancel(Resolver resolver)
        {
            executeMgr.CancelOperation(resolver);
        }

        /// <summary>
        /// 拣货
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void PickList(Resolver resolver)
        {
            PickList pickList = pickListMgr.CheckAndLoadPickList(resolver.Code);
            pickList.PickListDetails = new List<PickListDetail>();
            if (resolver.Transformers != null)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    PickListDetail pickListDetail = pickListDetailMgr.LoadPickListDetail(transformer.Id, true);
                    if (transformer != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            if (transformerDetail != null && transformerDetail.HuId != null && transformerDetail.HuId != string.Empty
                                && transformerDetail.CurrentQty != 0)
                            {
                                PickListResult pickListResult = new PickListResult();
                                //pickListResult.LocationLotDetail = locationLotDetailMgr.LoadLocationLotDetail(transformerDetail.LocationLotDetId);
                                pickListResult.LocationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(transformerDetail.HuId);
                                pickListResult.PickListDetail = pickListDetail;
                                pickListResult.Qty = transformerDetail.CurrentQty * pickListDetail.OrderLocationTransaction.UnitQty;
                                pickListDetail.AddPickListResult(pickListResult);
                            }
                        }
                    }
                    pickList.AddPickListDetail(pickListDetail);
                }
            }
            pickListMgr.DoPick(pickList, resolver.UserCode);
            resolver.Result = languageMgr.TranslateMessage("MasterData.PickList.Pick.Successfully", resolver.UserCode, resolver.Code);
            resolver.Transformers = null;
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void ExecutePrint(Resolver resolver)
        {
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void GetReceiptNotes(Resolver resolver)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(PickList));
            criteria.Add(Expression.Eq("IsPrinted", false));
            OrderHelper.SetOpenOrderStatusCriteria(criteria, "Status");//订单状态
            SecurityHelper.SetRegionSearchCriteria(criteria, "PartyFrom.Code", resolver.UserCode); //区域权限

            IList<PickList> pickList = criteriaMgr.FindAll<PickList>(criteria, 0, 5);

            List<ReceiptNote> receiptNotes = new List<ReceiptNote>();
            if (pickList != null && pickList.Count > 0)
            {
                foreach (PickList pl in pickList)
                {
                    string newUrl = reportMgr.WriteToFile("PickList.xls", pl.PickListNo);
                    pl.IsPrinted = true;//to be refactored
                    pickListMgr.UpdatePickList(pl);
                    ReceiptNote receiptNote = PickList2ReceiptNote(pl);
                    receiptNote.PrintUrl = newUrl;
                    receiptNotes.Add(receiptNote);
                }
            }
            resolver.ReceiptNotes = receiptNotes;
        }

        private ReceiptNote PickList2ReceiptNote(PickList pickList)
        {
            ReceiptNote receiptNote = new ReceiptNote();
            receiptNote.OrderNo = pickList.PickListNo;
            receiptNote.CreateDate = pickList.CreateDate;
            receiptNote.CreateUser = pickList.CreateUser == null ? string.Empty : pickList.CreateUser.Code;
            receiptNote.Status = pickList.Status;

            return receiptNote;
        }
    }
}
