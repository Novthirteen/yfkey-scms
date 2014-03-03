using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.Report;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Entity.Distribution;

namespace com.Sconit.Service.Business.Impl
{
    public class BatchDevanningMgr : AbstractBusinessMgr
    {
        public IRepackMgr repackMgr;
        public ILocationLotDetailMgr locationLotDetailMgr;
        public IUserMgr userMgr;

        public BatchDevanningMgr(IRepackMgr repackMgr,
           ILocationLotDetailMgr locationLotDetailMgr,
            IUserMgr userMgr)
        {
            this.repackMgr = repackMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.userMgr = userMgr;

        }

        protected override void GetReceiptNotes(Resolver resolver)
        {
            try
            {
                if (resolver.Input != null && resolver.Input.Trim() != string.Empty)
                {
                    //清空，不然老是重打
                    resolver.ReceiptNotes = null;
                    CreateRepack(resolver);
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void CreateRepack(Resolver resolver)
        {
            string[] huIdArr = resolver.Input.Split(',');
            List<ReceiptNote> receiptNotes = new List<ReceiptNote>();

            foreach (string huId in huIdArr)
            {
                try
                {
                    IList<RepackDetail> repackDetailList = new List<RepackDetail>();
                    LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(huId);
                    RepackDetail inRepackDetail = new RepackDetail();
                    inRepackDetail.LocationLotDetail = locationLotDetail;
                    inRepackDetail.Hu = locationLotDetail.Hu;
                    inRepackDetail.IOType = BusinessConstants.IO_TYPE_IN;
                    inRepackDetail.Qty = inRepackDetail.Hu.Qty * inRepackDetail.Hu.UnitQty;
                    repackDetailList.Add(inRepackDetail);

                    RepackDetail outRepackDetail = new RepackDetail();
                    outRepackDetail.itemCode = inRepackDetail.Hu.Item.Code;
                    outRepackDetail.IOType = BusinessConstants.IO_TYPE_OUT;
                    outRepackDetail.Qty = inRepackDetail.Qty;
                    repackDetailList.Add(outRepackDetail);

                    Repack repack = repackMgr.CreateDevanning(repackDetailList, userMgr.CheckAndLoadUser(resolver.UserCode));

                    ReceiptNote receiptNote = Repack2ReceiptNote(repack);
                    receiptNotes.Add(receiptNote);
                }

                catch (Exception ex)
                {
                    continue;
                }
            }
            if (resolver.ReceiptNotes == null)
            {
                resolver.ReceiptNotes = receiptNotes;
            }
            else
            {
                IListHelper.AddRange<ReceiptNote>(resolver.ReceiptNotes, receiptNotes);
            }
        }

   


        private ReceiptNote Repack2ReceiptNote(Repack repack)
        {
            ReceiptNote receiptNote = new ReceiptNote();
            receiptNote.OrderNo = repack.RepackNo;
            receiptNote.CreateDate = repack.CreateDate;
            receiptNote.CreateUser = repack.CreateUser == null ? string.Empty : repack.CreateUser.Code;
            
            return receiptNote;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {

        }
        protected override void GetDetail(Resolver resolver)
        {

        }
        protected override void SetDetail(Resolver resolver)
        {

        }
        protected override void ExecuteSubmit(Resolver resolver)
        {

        }
        protected override void ExecuteCancel(Resolver resolver)
        {

        }
        protected override void ExecutePrint(Resolver resolver)
        {

        }
    }
}
