using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using System.Collections;
using NPOI.SS.UserModel;
using com.Sconit.Utility;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;


public partial class Order_OrderHead_Distribution_QuickImport : ModuleBase
{
    public event EventHandler BackEvent;
    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    public string ModuleSubType
    {
        get
        {
            return (string)ViewState["ModuleSubType"];
        }
        set
        {
            ViewState["ModuleSubType"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ModuleSubType = (string)ViewState["ModuleSubType"];
        this.ModuleType=  (string)ViewState["ModuleSubType"];
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            HSSFWorkbook excel = new HSSFWorkbook(fileUpload.PostedFile.InputStream);
            Sheet sheet = excel.GetSheetAt(0);
            IEnumerator rows = sheet.GetRowEnumerator();
            ImportHelper.JumpRows(rows, 10);
            //发货路线	窗口时间 外部订单号 参考订单号 物料号	订单数

            #region 列定义
            int colFlow = 1;//发货路线
            int colExtOrderNo = 2;//外部订单号
            int colItem = 3;// 物料号
            int colQty = 4;//订单数
            #endregion
            int rowCount = 10;
            //IList<Exception> exceptionList = new List<Exception>();
            //Exception exceptio = new Exception();
            string errorMessage = string.Empty;
            IList<OrderHead> orderHeadList = new List<OrderHead>();
            while (rows.MoveNext())
            {
                rowCount++;
                HSSFRow row = (HSSFRow)rows.Current;
                if (!TheImportMgr.CheckValidDataRow(row, 1, 4))
                {
                    break;//边界
                }
                string flowCode = string.Empty;
                string extOrderNo = string.Empty;
                string itemCode = string.Empty;
                decimal qty = 0;
                OrderHead orderHead = new OrderHead();
                Flow currentFlow = new Flow();

                #region 读取数据
                #region 发货路线
                flowCode = row.GetCell(colFlow) != null ? row.GetCell(colFlow).StringCellValue : string.Empty;
                if (string.IsNullOrEmpty(flowCode))
                {
                    errorMessage += string.Format("第{0}行:发货路线不能为空。<br/>", rowCount);
                    continue;
                }
                else
                {
                    currentFlow = TheFlowMgr.LoadFlow(flowCode, this.CurrentUser.Code, true);
                }
                #endregion

                #region 读取窗口时间
                //try
                //{
                //    string readwindowTime = row.GetCell(colWindowTime).StringCellValue;
                //    if (DateTime.TryParse(readwindowTime, out windowTime))
                //    {
                //        orderHead.WindowTime = windowTime;
                //    }
                //    else
                //    {
                //        errorMessage += string.Format("第{0}行:窗口时间填写有误。<br/>", rowCount);
                //        continue;
                //    }
                //}
                //catch
                //{
                //    errorMessage += string.Format("第{0}行:窗口时间填写有误。<br/>", rowCount);
                //    continue;
                //}
                #endregion

                extOrderNo = row.GetCell(colExtOrderNo) != null ? row.GetCell(colExtOrderNo).StringCellValue : string.Empty;

                //refOrderNo = row.GetCell(colRefOrderNo) != null ? row.GetCell(colRefOrderNo).StringCellValue : string.Empty;

                #region 读取物料代码
                itemCode = row.GetCell(colItem) != null ? row.GetCell(colItem).StringCellValue : string.Empty;
                if (itemCode == null || itemCode.Trim() == string.Empty)
                {
                    errorMessage += string.Format("第{0}行:物料代码不能为空。<br/>", rowCount);
                    //ShowErrorMessage(string.Format("第{0}行:物料代码不能为空。", rowCount));
                    continue;
                }
                else
                {
                    currentFlow.FlowDetails = currentFlow.FlowDetails.Where(f => f.Item.Code == itemCode).ToList();
                    if (currentFlow.FlowDetails.Count == 0)
                    {
                        errorMessage += string.Format("第{0}行:物料代码{1}在路线{2}中不存在。<br/>", rowCount, itemCode, flowCode);
                        continue;
                    }
                }

                #endregion

                #region 读取数量
                try
                {
                    qty = Convert.ToDecimal(row.GetCell(colQty).NumericCellValue);
                }
                catch
                {
                    //ShowErrorMessage(string.Format("第{0}行:订单数量填写有误。<br/>", rowCount));
                    errorMessage += string.Format("第{0}行:订单数量填写有误。<br/>", rowCount);
                    continue;
                }
                #endregion
                #endregion

                #region 填充数据
                orderHead = TheOrderMgr.TransferFlow2Order(currentFlow);
                orderHead.SubType = this.ModuleSubType;
                orderHead.WindowTime = System.DateTime.Now;
                orderHead.Priority = "Normal";
                orderHead.Type = this.ModuleType;
                orderHead.StartTime = System.DateTime.Now;
                orderHead.ExternalOrderNo = extOrderNo;
                //orderHead.ReferenceOrderNo = refOrderNo;
                orderHead.IsAutoRelease = true;
                orderHead.IsAutoStart = true;
                orderHead.IsAutoShip = true;
                orderHead.IsAutoReceive = true;
                orderHead.StartLatency = 0;
                orderHead.CompleteLatency = 0;
                foreach (OrderDetail det in orderHead.OrderDetails)
                {
                    det.RequiredQty = qty;
                    det.OrderedQty = qty;
                }
                orderHeadList.Add(orderHead);
                #endregion
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }
            if (orderHeadList.Count == 0)
            {
                throw new Exception("导入的有效数据为0.");
            }
            string successMessage = TheOrderMgr.QuickImportDistributionOrder(orderHeadList,this.CurrentUser.Code,this.ModuleSubType);
            ShowSuccessMessage(successMessage);
            //var groups = (from tak in orderHeadList
            //              group tak by new
            //              {
            //                  tak.Flow,
            //                  tak.ExternalOrderNo,
            //              }
            //                  into result
            //                  select new
            //                  {
            //                      Flow = result.Key.Flow,
            //                      ExternalOrderNo = result.Key.ExternalOrderNo,
            //                      list = result.ToList()
            //                  }
            //               ).ToList();
            //string orderNos = "导入成功，生成送货单号：";
            //foreach (var order in groups)
            //{
            //    OrderHead newOrderHead = order.list.First();
            //    IList<OrderDetail> detList = new List<OrderDetail>();
            //    //OrderHead newOrderHead = new OrderHead();
            //    foreach (var d in order.list)
            //    {
            //        OrderDetail det = d.OrderDetails.First();
            //        det.OrderHead = newOrderHead;
            //        detList.Add(det);
            //    }
            //    newOrderHead.OrderDetails = detList;

            //    orderNos += CreateOrder(newOrderHead);
            //}
            //ShowSuccessMessage(orderNos);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
            return;
        }

    }

    private string CreateOrder(OrderHead orderHead)
    {
        IList<OrderDetail> resultOrderDetailList = new List<OrderDetail>();
        foreach (OrderDetail orderDetail in orderHead.OrderDetails)
        {
            if (orderDetail.OrderedQty != 0)
            {
                if (orderDetail.Item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
                {
                    IList<Item> newItemList = new List<Item>(); //填充套件子件
                    decimal? convertRate = null;
                    IList<ItemKit> itemKitList = null;

                    var maxSequence = orderHead.OrderDetails.Max(o => o.Sequence);
                    itemKitList = this.TheItemKitMgr.GetChildItemKit(orderDetail.Item);
                    for (int i = 0; i < itemKitList.Count; i++)
                    {
                        Item item = itemKitList[i].ChildItem;
                        if (!convertRate.HasValue)
                        {
                            if (itemKitList[i].ParentItem.Uom.Code != orderDetail.Item.Uom.Code)
                            {
                                convertRate = this.TheUomConversionMgr.ConvertUomQty(orderDetail.Item, orderDetail.Item.Uom, 1, itemKitList[i].ParentItem.Uom);
                            }
                            else
                            {
                                convertRate = 1;
                            }
                        }
                        OrderDetail newOrderDetail = new OrderDetail();

                        newOrderDetail.OrderHead = orderDetail.OrderHead;
                        newOrderDetail.Sequence = maxSequence + (i + 1);
                        newOrderDetail.IsBlankDetail = false;
                        newOrderDetail.Item = item;
                        newOrderDetail.Uom = item.Uom;
                        newOrderDetail.UnitCount = orderDetail.Item.UnitCount * itemKitList[i].Qty * convertRate.Value;
                        newOrderDetail.OrderedQty = orderDetail.OrderedQty * itemKitList[i].Qty * convertRate.Value;
                        newOrderDetail.PackageType = orderDetail.PackageType;

                        resultOrderDetailList.Add(newOrderDetail);
                    }
                }
                else
                {
                    resultOrderDetailList.Add(orderDetail);
                }
            }
        }
        orderHead.OrderDetails = resultOrderDetailList;
        //TheOrderMgr.CreateOrder(orderHead, this.CurrentUser);

        Receipt receipt = TheOrderMgr.QuickReceiveOrder(orderHead.Flow, orderHead.OrderDetails, this.CurrentUser.Code, this.ModuleSubType, orderHead.WindowTime, orderHead.StartTime, false, orderHead.ReferenceOrderNo, orderHead.ExternalOrderNo);
        if (receipt.ReceiptDetails != null && receipt.ReceiptDetails.Count > 0)
        {
            orderHead = receipt.ReceiptDetails[0].OrderLocationTransaction.OrderDetail.OrderHead;
        }
        //ShowSuccessMessage("Receipt.Receive.Successfully", receipt.ReceiptNo);
        //return receipt.ReceiptNo;
        
        return receipt.InProcessLocations.First().IpNo;

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}