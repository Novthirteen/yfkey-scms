using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity.Transportation;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
 
using System.IO;
public partial class Transportation_Bill_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    private int DecimalLength
    {
        get
        {
            return (int)ViewState["DecimalLength"];
        }
        set
        {
            ViewState["DecimalLength"] = value;
        }
    }

    public string PartyCode
    {
        get
        {
            return (string)ViewState["PartyCode"];
        }
        set
        {
            ViewState["PartyCode"] = value;
        }
    }

    public string BillNo
    {
        get
        {
            return (string)ViewState["BillNo"];
        }
        set
        {
            ViewState["BillNo"] = value;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        TransportationBill transportationBill = this.TheTransportationBillMgr.LoadTransportationBill(this.BillNo, true);
        IList<object> list = new List<object>();
        if (transportationBill != null)
        {
            list.Add(transportationBill);
            list.Add(transportationBill.TransportationBillDetails);
        }
        string barCodeUrl = WriteToFile(list);
       // barCodeUrl = TheReportMgr.WriteToFile("TransportationBill.xls", list);/*20110420 Tag 先做标记，后期补充  TransportationBill.xls没有开发*/
        
        Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + barCodeUrl + "'); </script>");
        this.ShowSuccessMessage("Transportation.TransportationBill.Print.Successful");
    }

    /// <summary>
    /// 金董春：添加打印功能，添加了TransportationBill.xls模板打印功能 2012-5-28
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public string WriteToFile(IList<object> list)
    {
        string path=Server.MapPath(".")+@"\Reports\Templates\YFKExcelTemplates\TransportationBill.xls";
        if (File.Exists(path))
        {
            TransportationBill tb = (TransportationBill)list[0];
            string filename = @"/Reports/Templates/TempFiles/temp_" + DateTime.Now.ToString("yyyyMMddhhmmss") + tb.BillNo + ".xls";
            string _wpath = Server.MapPath(".") + filename;
            File.Copy(path, _wpath);
            FileStream file = new FileStream(_wpath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            Sheet sheet = hssfworkbook.GetSheet("sheet1");
            NPOI.SS.UserModel.CellStyle normalStyle=setCellstyle(hssfworkbook,new string[]{"Border","Center"});
            NPOI.SS.UserModel.CellStyle dateStyle = setCellstyle(hssfworkbook, new string[] { "Border", "Center" ,"DateTime"});
            Cell cell=sheet.GetRow(8).GetCell(2);
            cell.SetCellValue(tb.BillAddress.Party.Name);
            cell = sheet.GetRow(8).GetCell(8);
            cell.SetCellValue(tb.BillNo);
            int i = 10;
            decimal cnt = 0;
            foreach (TransportationBillDetail tbd in tb.TransportationBillDetails)
            {
                Row row = sheet.CreateRow(i);
               
                TransportationOrder tord = TheTransportationOrderMgr.LoadTransportationOrder(tbd.ActBill.OrderNo); 
                row.CreateCell(0).SetCellValue(tord.CreateDate);//运输日期
                row.CreateCell(1).SetCellValue(tord.TransportationRoute!=null?tord.TransportationRoute.Description:"");//运输路线
                row.CreateCell(2).SetCellValue(tbd.ActBill.PricingMethod!=null?tbd.ActBill.PricingMethod:"");//运输形式
                row.CreateCell(3).SetCellValue(tord.OrderNo);//运单号码
                row.CreateCell(4).SetCellValue(tbd.ActBill.EffectiveDate);//生效日期
                row.CreateCell(5).SetCellValue(tbd.ActBill.UnitPrice.ToString("F2"));//单价
                row.CreateCell(6).SetCellValue(tbd.ActBill.Currency.Name);//币种
                row.CreateCell(7).SetCellValue(tbd.ActBill.BillQty.ToString("F0"));//开票数
                row.CreateCell(8).SetCellValue(tbd.ActBill.BillAmount.ToString("F2"));//金额
                cnt = Convert.ToInt32(tbd.ActBill.BillAmount) + cnt;
                for (int y = 0; y < 9; y++)
                {
                    row.GetCell(y).CellStyle = normalStyle;
                }
                row.GetCell(0).CellStyle = dateStyle;
                row.GetCell(4).CellStyle = dateStyle;
                i++;

            }
            if (i <= 20)
            {
                for (int j = i; j < 21; j++)
                {
                    Row row = sheet.CreateRow(j);
                    for(int y=0;y<9;y++)
                    {
                        row.CreateCell(y).CellStyle = normalStyle;
                    }
                }
                i = 20;
            }
            Row _row = sheet.CreateRow(i + 1);
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(i+1,i+1,6,7));
            _row.CreateCell(6).SetCellValue("合计发票金额：");
            _row.GetCell(6).CellStyle.Alignment = HorizontalAlignment.RIGHT;
            _row.CreateCell(8).SetCellValue(cnt.ToString("F2"));
            MemoryStream ms = new MemoryStream();
            hssfworkbook.Write(ms);
            
           // Response.AddHeader("Content-Disposition", string.Format("attachment;filename=TempWorkBook.xls"));
            // Response.BinaryWrite(ms.ToArray());Reports/Templates/TempFiles
           
            FileStream f = new FileStream(_wpath, FileMode.Open, FileAccess.Write);
            byte[] data = ms.ToArray();
            f.Write(data,0,data.Length);
            f.Close();
            f.Dispose();
            hssfworkbook = null;
            ms.Close();
            ms.Dispose();
            return "http://"+Request.Url.Authority+ filename;
        }

        return "";
    }
    /// <summary>
    /// 设置excel中单元格的样式
    /// djin--2012-5-25
    /// </summary>
    /// <param name="hssfworkbook"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public NPOI.SS.UserModel.CellStyle setCellstyle(HSSFWorkbook hssfworkbook, string[] options)
    {
        NPOI.SS.UserModel.CellStyle style3 = hssfworkbook.CreateCellStyle();
        foreach (string s in options)
        {
            if (s == "Border")
            {
                style3.BorderBottom = CellBorderType.THIN;
                style3.BorderLeft = CellBorderType.THIN;
                style3.BorderRight = CellBorderType.THIN;
                style3.BorderTop = CellBorderType.THIN;
            }
            if (s == "Center")
            {
                style3.VerticalAlignment = VerticalAlignment.CENTER;
                style3.Alignment = HorizontalAlignment.CENTER;
            }
            if (s == "DateTime")
            {
                NPOI.SS.UserModel.DataFormat format = hssfworkbook.CreateDataFormat();
                style3.DataFormat = format.GetFormat("yyyy-m-d");
            }
        }
        return style3;
    }

    public void InitPageParameter(string billNo)
    {
        this.BillNo = billNo;
        this.ODS_TransportationBill.SelectParameters["billNo"].DefaultValue = billNo;
        this.ODS_TransportationBill.SelectParameters["includeDetail"].DefaultValue = true.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucNewSearch.BackEvent += new EventHandler(AddBack_Render);

    }

    protected void FV_TransportationBill_DataBound(object sender, EventArgs e)
    {
        TransportationBill transportationBill = (TransportationBill)((FormView)(sender)).DataItem;
        UpdateView(transportationBill);
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TransportationBillDetail transportationBillDetail = (TransportationBillDetail)e.Row.DataItem;
            TransportationOrder to = TheTransportationOrderMgr.LoadTransportationOrder(transportationBillDetail.ActBill.OrderNo);
            if(to!=null)
            {
            Label lb_createDate = (System.Web.UI.WebControls.Label)e.Row.FindControl("lb_createDate");//运输日期
                lb_createDate.Text = to.CreateDate.ToString("yyyy-MM-dd")!=null?to.CreateDate.ToString("yyyy-MM-dd"):"";
            Label lb_route = (System.Web.UI.WebControls.Label)e.Row.FindControl("lb_route");//运输路线
                lb_route.Text = to.TransportationRoute!=null?to.TransportationRoute.Description:"";
            Label lb_pricingMethod = (System.Web.UI.WebControls.Label)e.Row.FindControl("lb_pricingMethod");//运输方式
                lb_pricingMethod.Text = to.PricingMethod!=null?to.PricingMethod:"";
            Label lb_OrderNo = (System.Web.UI.WebControls.Label)e.Row.FindControl("lb_OrderNo");//运单号码
                lb_OrderNo.Text = to.OrderNo!=null?to.OrderNo:"";
            }
            TextBox tbAmount = (TextBox)e.Row.FindControl("tbAmount");
            tbAmount.Attributes["oldValue"] = tbAmount.Text;

            TextBox tbQty = (TextBox)e.Row.FindControl("tbQty");
            TextBox tbDiscountRate = (TextBox)e.Row.FindControl("tbDiscountRate");
            TextBox tbDiscount = (TextBox)e.Row.FindControl("tbDiscount");

            if (transportationBillDetail.Bill.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                tbQty.ReadOnly = true;
                tbAmount.ReadOnly = true;
                tbDiscountRate.ReadOnly = true;
                tbDiscount.ReadOnly = true;
            }

            /*
             * 
             * 1.TransType=Transportation 价格单明细（承运商） 或  短拨费（区域）时
             * a.PricingMethod=M3或KG  按数量
             * b.SHIPT   按金额
             * 2.TransType=WarehouseLease(固定费用) 按金额
             * 3.TransType=Operation(操作费) 按数量
             */
            if (transportationBillDetail.TransType == BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_OPERATION
                ||
                (transportationBillDetail.TransType == BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_TRANSPORTATION
                && (transportationBillDetail.ActBill.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_M3 || transportationBillDetail.ActBill.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_KG)
                )
                )
            {
                tbQty.Attributes["onchange"] = "qtyChanged(this);";
                tbQty.Attributes["onmouseup"] = "if(!readOnly)select();";
                tbAmount.Attributes["onfocus"] = "this.blur();";
            }
            else
            {
                tbQty.Attributes["onfocus"] = "this.blur();";
                tbAmount.Attributes["onmouseup"] = "if(!readOnly)select();";
            }
        }
    }

    protected void lbRefBillNo_Click(object sender, EventArgs e)
    {
        string refBillNo = ((LinkButton)(sender)).CommandArgument;
        InitPageParameter(refBillNo);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            IList<TransportationBillDetail> transportationBillDetailList = PopulateData(false);
            TransportationBill transportationBill = this.TheTransportationBillMgr.LoadTransportationBill(this.BillNo);
            TextBox tbExternalBillNo = this.FV_TransportationBill.FindControl("tbExternalBillNo") as TextBox;
            if (tbExternalBillNo.Text.Trim() != string.Empty)
            {
                transportationBill.ExternalBillNo = tbExternalBillNo.Text.Trim();
            }
            else
            {
                transportationBill.ExternalBillNo = null;
            }

            if (this.tbTotalDiscount.Text.Trim() != string.Empty)
            {
                transportationBill.Discount = decimal.Parse(this.tbTotalDiscount.Text.Trim());
            }
            else
            {
                transportationBill.Discount = null;
            }
            transportationBill.TransportationBillDetails = transportationBillDetailList;
            this.TheTransportationBillMgr.UpdateTransportationBill(transportationBill, this.CurrentUser);
            this.ShowSuccessMessage("Transportation.TransportationBill.UpdateSuccessfully", this.BillNo);
            this.FV_TransportationBill.DataBind();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            this.TheTransportationBillMgr.ReleaseTransportationBill(this.BillNo, this.CurrentUser);
            this.ShowSuccessMessage("Transportation.TransportationBill.ReleaseSuccessfully", this.BillNo);
            this.FV_TransportationBill.DataBind();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            this.TheTransportationBillMgr.DeleteTransportationBill(this.BillNo, this.CurrentUser);
            this.ShowSuccessMessage("Transportation.TransportationBill.DeleteSuccessfully", this.BillNo);
            this.BackEvent(this, e);
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            this.TheTransportationBillMgr.CloseTransportationBill(this.BillNo, this.CurrentUser);
            this.ShowSuccessMessage("Transportation.TransportationBill.CloseSuccessfully", this.BillNo);
            this.FV_TransportationBill.DataBind();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.TheTransportationBillMgr.CancelTransportationBill(this.BillNo, this.CurrentUser);
            this.ShowSuccessMessage("Transportation.TransportationBill.CancelSuccessfully", this.BillNo);
            this.FV_TransportationBill.DataBind();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnVoid_Click(object sender, EventArgs e)
    {
        try
        {
            this.TheTransportationBillMgr.VoidTransportationBill(this.BillNo, this.CurrentUser);
            this.ShowSuccessMessage("Transportation.TransportationBill.VoidSuccessfully", this.BillNo);
            this.FV_TransportationBill.DataBind();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (this.BackEvent != null)
        {
            this.BackEvent(this, e);
        }
    }

    protected void btnAddDetail_Click(object sender, EventArgs e)
    {
        IDictionary<string, string> actionParameter = new Dictionary<string, string>();
        actionParameter.Add("PartyCode", this.PartyCode);
        this.ucNewSearch.QuickSearch(actionParameter);
        this.ucNewSearch.Visible = true;
    }

    protected void btnDeleteDetail_Click(object sender, EventArgs e)
    {
        try
        {
            IList<TransportationBillDetail> transportationBillDetailList = PopulateData(true);
            this.TheTransportationBillMgr.DeleteTransportationBillDetail(transportationBillDetailList, this.CurrentUser);
            this.ShowSuccessMessage("Transportation.TransportationBill.DeleteBillDetailSuccessfully");
            this.FV_TransportationBill.DataBind();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void AddBack_Render(object sender, EventArgs e)
    {
        this.ucNewSearch.Visible = false;
        this.FV_TransportationBill.DataBind();
    }

    private void UpdateView(TransportationBill transportationBill)
    {
        #region 根据状态显示按钮
        if (transportationBill.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
        {
            this.btnSave.Visible = true;
            this.btnSubmit.Visible = true;
            this.btnDelete.Visible = true;
            this.btnClose.Visible = false;
            this.btnCancel.Visible = false;
            this.btnVoid.Visible = false;
            this.btnAddDetail.Visible = true;
            this.btnDeleteDetail.Visible = true;
            //this.btnPrint.Visible = false;
        }
        else if (transportationBill.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
        {
            this.btnSave.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnDelete.Visible = false;
            this.btnClose.Visible = true;
            this.btnCancel.Visible = true;
            this.btnVoid.Visible = false;
            this.btnAddDetail.Visible = false;
            this.btnDeleteDetail.Visible = false;
            //this.btnPrint.Visible = true;
        }
        else if (transportationBill.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL)
        {
            this.btnSave.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnDelete.Visible = false;
            this.btnClose.Visible = false;
            this.btnCancel.Visible = false;
            this.btnVoid.Visible = false;
            this.btnAddDetail.Visible = false;
            this.btnDeleteDetail.Visible = false;
            //this.btnPrint.Visible = false;
        }
        else if (transportationBill.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)
        {
            this.btnSave.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnDelete.Visible = false;
            this.btnClose.Visible = false;
            this.btnCancel.Visible = false;
            //this.btnPrint.Visible = false;
            if (transportationBill.BillType == BusinessConstants.CODE_TRANSPORTATION_TRANSPORTATIONBILL_TYPE_VALUE_CANCEL)
            {
                this.btnVoid.Visible = false; ;
            }
            else
            {
                this.btnVoid.Visible = true;
            }
            this.btnAddDetail.Visible = false;
            this.btnDeleteDetail.Visible = false;
        }
        else if (transportationBill.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_VOID)
        {
            this.btnSave.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnDelete.Visible = false;
            this.btnClose.Visible = false;
            this.btnCancel.Visible = false;
            this.btnVoid.Visible = false;
            this.btnAddDetail.Visible = false;
            this.btnDeleteDetail.Visible = false;
            //this.btnPrint.Visible = false;
        }
        #endregion

        #region 根据状态隐藏/显示字段
        if (transportationBill.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
        {
            this.Gv_List.Columns[0].Visible = false;
            this.tbTotalDiscountRate.ReadOnly = true;
            this.tbTotalDiscount.ReadOnly = true;

            TextBox tbExternalBillNo = this.FV_TransportationBill.FindControl("tbExternalBillNo") as TextBox;
            tbExternalBillNo.ReadOnly = true;
        }
        else
        {
            this.Gv_List.Columns[0].Visible = true;
            this.tbTotalDiscountRate.ReadOnly = false;
            this.tbTotalDiscount.ReadOnly = false;

            TextBox tbExternalBillNo = this.FV_TransportationBill.FindControl("tbExternalBillNo") as TextBox;
            tbExternalBillNo.ReadOnly = false;
        }
        #endregion

        #region 给总金额和折扣赋值
        this.tbTotalDetailAmount.Text = transportationBill.TotalBillDetailAmount.ToString("F2");
        this.tbTotalAmount.Text = transportationBill.TotalBillAmount.ToString("F2");
        this.tbTotalDiscount.Text = (transportationBill.Discount.HasValue ? transportationBill.Discount.Value : 0).ToString("F2");
        this.tbTotalDiscountRate.Text = transportationBill.TotalBillDiscountRate.ToString("F2");
        #endregion

        #region 初始化弹出窗口  
        this.PartyCode = transportationBill.BillAddress.Party.Code;
        this.ucNewSearch.InitPageParameter(true, transportationBill);
        #endregion

        UpdateDetailView(transportationBill.TransportationBillDetails);
    }

    private void UpdateDetailView(IList<TransportationBillDetail> transportationBillDetailList)
    {
        this.Gv_List.DataSource = transportationBillDetailList;
        this.Gv_List.DataBind();
    }

    private IList<TransportationBillDetail> PopulateData(bool isChecked)
    {
        if (this.Gv_List.Rows != null && this.Gv_List.Rows.Count > 0)
        {
            IList<TransportationBillDetail> transportationBillDetailList = new List<TransportationBillDetail>();
            foreach (GridViewRow row in this.Gv_List.Rows)
            {
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked || !isChecked)
                {
                    HiddenField hfId = row.FindControl("hfId") as HiddenField;
                    TextBox tbQty = row.FindControl("tbQty") as TextBox;
                    TextBox tbAmount = row.FindControl("tbAmount") as TextBox;
                    TextBox tbDiscount = row.FindControl("tbDiscount") as TextBox;

                    TransportationBillDetail transportationBillDetail = new TransportationBillDetail();
                    transportationBillDetail.Id = int.Parse(hfId.Value);
                    transportationBillDetail.BilledQty = decimal.Parse(tbQty.Text);
                    transportationBillDetail.Amount = decimal.Parse(tbAmount.Text);
                    transportationBillDetail.Discount = decimal.Parse(tbDiscount.Text);

                    transportationBillDetailList.Add(transportationBillDetail);
                }
            }
            return transportationBillDetailList;
        }

        return null;
    }
}
