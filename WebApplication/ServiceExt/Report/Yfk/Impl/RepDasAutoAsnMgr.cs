using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity.Distribution;
using com.Sconit.Service.Distribution;
namespace com.Sconit.Service.Report.Yfk.Impl
{
    public class RepDasAutoAsnMgr : RepTemplate1
    {
        private IOrderHeadMgr orderHeadMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private ILanguageMgr languageMgr;
        private ILocationTransactionMgr locationTranMgr;
        private ICriteriaMgr criteriaMgr;
        private IItemReferenceMgr itemrefMgr;
        private IInProcessLocationMgr inpMgr;
        public RepDasAutoAsnMgr(String reportTemplateFolder,
            IOrderHeadMgr orderHeadMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            ILanguageMgr languageMgr,
            ILocationTransactionMgr locationTranMgr,
           ICriteriaMgr criteriaMgr,
            IItemReferenceMgr itemrefMgr,
            IInProcessLocationMgr inpMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.orderHeadMgr = orderHeadMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.locationTranMgr = locationTranMgr;
            this.criteriaMgr = criteriaMgr;
            this.itemrefMgr = itemrefMgr;
            this.inpMgr = inpMgr;
        }

        /**
         * 填充报表
         * 
         * Param list [0]OrderHead
         * Param list [0]IList<OrderDetail>           
         */
        [Transaction(TransactionMode.Requires)]
        protected override bool FillValuesImpl(String templateFileName, IList<object> list)
        {
            try
            {

                if (list == null || list.Count < 2) return false;

                InProcessLocation orderHead = (InProcessLocation)(list[0]);
                IList<InProcessLocationDetail> orderDetails = (IList<InProcessLocationDetail>)(list[1]);


                if (orderHead == null
                    || orderDetails == null || orderDetails.Count == 0)
                {
                    return false;
                }







                this.FillHead(orderHead);
                   
                   
             

                this.sheet.DisplayGridlines = false;
                this.sheet.IsPrintGridlines = false;
                //this.workbook.RemoveSheetAt(sheetindex);
                //if (orderHead.IsPrinted == null || orderHead.IsPrinted == false)
                //{
                //    orderHead.IsPrinted = true;
                //    orderHeadMgr.UpdateOrderHead(orderHead);
                //}


            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        /*
         * 填充报表头
         * 
         * Param repack 报验单头对象
         */
        private void FillHead(InProcessLocation ip)
        {



            this.SetRowCell( 7, 27, ip.CreateDate.ToShortDateString());
            //this.SetRowCell( 10, 30, "5500032357");
            this.SetRowCell( 17, 5, ip.InProcessLocationDetails.Count.ToString());

            var detail = from i in ip.InProcessLocationDetails
                         group i by new { i.OrderLocationTransaction.Item.Code, i.OrderLocationTransaction.Item.Description, i.OrderLocationTransaction.Item.Uom }
                             into g
                             select new { g.Key.Code, g.Key.Description, g.Key.Uom, qty = g.Sum(i => i.Qty) };

            int offset = 0;
            foreach (var row in detail)
            {
                this.SetRowCell( 27 + offset, 3, row.Description);
                this.SetRowCell( 27 + offset, 12, ((ItemReference)(itemrefMgr.GetItemReference(row.Code, ip.PartyTo.Code)[0])).ReferenceCode);
                this.SetRowCell( 27 + offset, 20, row.Uom.Description);
                this.SetRowCell( 27 + offset, 25, Math.Round(row.qty, 0).ToString());
                offset++;
            }
        }


        public override void CopyPageValues(int pageIndex)
        {


        }



        public override IList<object> GetDataList(string code)
        {
            IList<object> list = new List<object>();
            OrderHead orderHead = orderHeadMgr.LoadOrderHead(code, true);
            if (orderHead != null)
            {
                list.Add(orderHead);
                list.Add(orderHead.OrderDetails);
            }
            return list;
        }

    }
}
