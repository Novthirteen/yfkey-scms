using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Service;
using com.Sconit.Service.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Service.Distribution;


namespace com.Sconit.Service.Report.Yfk.Impl
{
    [Transactional]
    public class RepASNMgr : RepTemplate1
    {

        private IInProcessLocationMgr inProcessLocationMgr;

        private ICodeMasterMgr codeMasterMgr;
        private IItemReferenceMgr itemReferenceMgr;
        private IShipAddressMgr shipAddressMgr;


        public RepASNMgr(String reportTemplateFolder, ICodeMasterMgr codeMasterMgr,
            IInProcessLocationMgr inProcessLocationMgr, IItemReferenceMgr itemReferenceMgr, IShipAddressMgr shipAddressMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.codeMasterMgr = codeMasterMgr;
            this.inProcessLocationMgr = inProcessLocationMgr;
            this.itemReferenceMgr = itemReferenceMgr;
            this.shipAddressMgr = shipAddressMgr;

            //明细部分的行数
            this.pageDetailRowCount = 12;
            //列数   1起始
            this.columnCount = 17;
            //报表头的行数  1起始
            this.headRowCount = 9;
            //报表尾的行数  1起始
            this.bottomRowCount = 7;

        }

        /**
         * 填充报表
         * 
         * Param list [0]InProcessLocation
         *            [1]inProcessLocationDetailList
         */
        [Transaction(TransactionMode.Requires)]
        protected override bool FillValuesImpl(String templateFileName, IList<object> list)
        {
            try
            {

                if (list == null || list.Count < 2) return false;

                InProcessLocation inProcessLocation = (InProcessLocation)list[0];
                IList<InProcessLocationDetail> inProcessLocationDetailList = (IList<InProcessLocationDetail>)list[1];

                if (inProcessLocation == null
                    || inProcessLocationDetailList == null || inProcessLocationDetailList.Count == 0)
                {
                    return false;
                }
                //ASN号:
                List<Transformer> transformerList = Utility.TransformerHelper.ConvertInProcessLocationDetailsToTransformers(inProcessLocationDetailList);


                this.CopyPage(transformerList.Count);

                this.FillHead(inProcessLocation);

                int pageIndex = 1;
                int rowIndex = 0;
                int rowTotal = 0;

                foreach (Transformer transformer in transformerList)
                {
                    //零件号
                    this.SetRowCell(pageIndex, rowIndex, 1, transformer.ItemCode);
                    //客户零件号
                    //IList<ItemReference> referenceItemList = itemReferenceMgr.GetItemReference(transformer.ItemCode);
                    //if (referenceItemList != null && referenceItemList.Count > 0)
                    //{
                    //    this.SetRowCell(pageIndex, rowIndex, 2, referenceItemList[0].ReferenceCode);
                    //}
                    this.SetRowCell(pageIndex, rowIndex, 2, transformer.ReferenceItemCode);

                    //零件名称
                    this.SetRowCell(pageIndex, rowIndex, 3, transformer.ItemDescription);
                    //库位
                    this.SetRowCell(pageIndex, rowIndex, 4, transformer.LocationFromCode);
                    //包装箱 类型
                    //this.SetRowCell(pageIndex, rowIndex, 5, transformer.Cartons.ToString("0.########"));
                    //长
                    //宽
                    //高
                    //单位
                    this.SetRowCell(pageIndex, rowIndex, 9, transformer.UomCode);
                    //实送数量
                    this.SetRowCell(pageIndex, rowIndex, 10, transformer.Qty.ToString("0.########"));
                    //实收数量
                    //装箱数(transformer.Qty / transformer.UnitCount).ToString("0.########")
                    this.SetRowCell(pageIndex, rowIndex, 12, (transformer.Qty / transformer.UnitCount).ToString("0.########"));
                    //件箱数
                    //单价
                    //总金额
                    //备注

                    if (this.isPageBottom(rowIndex, rowTotal))//页的最后一行
                    {
                        //承运经办人
                        //发货人
                        //收货经办人
                        //送货日期
                        //送货日期
                        //出门日期
                        //执勤门卫
                        //客户代码
                        this.SetRowCell(pageIndex, this.pageDetailRowCount + 4, 10, inProcessLocation.PartyTo == null ? string.Empty : inProcessLocation.PartyTo.Code);
                        //开单人
                        this.SetRowCell(pageIndex, this.pageDetailRowCount + 6, 1, inProcessLocation.CreateUser.Name);
                        //开单日期
                        this.SetRowCell(pageIndex, this.pageDetailRowCount + 6, 3, inProcessLocation.CreateDate.ToString("yyyy    MM    dd"));

                        this.sheet.SetRowBreak(this.GetRowIndexAbsolute(pageIndex, this.pageDetailRowCount + this.bottomRowCount - 1));

                        pageIndex++;
                        rowIndex = 0;
                    }
                    else
                    {
                        rowIndex++;
                    }
                    rowTotal++;
                }

                this.sheet.DisplayGridlines = false;
                this.sheet.IsPrintGridlines = false;

                //打印标记
                if (inProcessLocation.IsPrinted == null || inProcessLocation.IsPrinted == false)
                {
                    inProcessLocation.IsPrinted = true;
                }

                inProcessLocation.PrintCount += 1;
                inProcessLocationMgr.UpdateInProcessLocation(inProcessLocation);

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
         * Param pageIndex 页号
         * Param orderHead 订单头对象
         * Param orderDetails 订单明细对象
         */
        private void FillHead(InProcessLocation inProcessLocation)
        {

            //首部
            //No.
            string ipNo = inProcessLocation.IpNo;
            if (inProcessLocation.PrintCount > 0)
            {
                ipNo += "      (" + inProcessLocation.PrintCount.ToString() + ")";
            }
            this.SetRowCell(3, 14, ipNo);
            //收货单位
            this.SetRowCell(5, 2, inProcessLocation.PartyTo.Name);
            if (inProcessLocation.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
            {
                //移库路线上没有shipto,暂时取默认的地址
                ShipAddress shipAddress = shipAddressMgr.GetDefaultShipAddress(inProcessLocation.PartyTo);
                this.SetRowCell(5, 9, shipAddress == null ? string.Empty : shipAddress.Address);
            }
            else
            {
                //地址
                this.SetRowCell(5, 9, inProcessLocation.ShipTo == null ? string.Empty : inProcessLocation.ShipTo.Address);
            }
            //卸货地点
            //运输方法
            //车号
            //介绍信编号
            //货物出门性质
            CodeMaster codeMaster = codeMasterMgr.LoadCodeMaster(BusinessConstants.CODE_MASTER_ORDER_TYPE, inProcessLocation.OrderType);
            this.SetRowCell(6, 16, codeMaster.Description);

        }

        /**
           * 需要拷贝的数据与合并单元格操作
           * 
           * Param pageIndex 页号
           */
        public override void CopyPageValues(int pageIndex)
        {
            //客户代码
            this.CopyCell(pageIndex, 25 - this.headRowCount, 10, "K26");
            //开单人
            this.CopyCell(pageIndex, 27 - this.headRowCount, 1, "B28");
            //开单日期
            this.CopyCell(pageIndex, 27 - this.headRowCount, 3, "D28");
        }

        public override IList<object> GetDataList(string code)
        {
            IList<object> list = new List<object>();
            InProcessLocation inProcessLocation = inProcessLocationMgr.LoadInProcessLocation(code, true);
            if (inProcessLocation != null)
            {
                list.Add(inProcessLocation);
            }
            return list;
        }

    }


}
