using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using System.IO;
using com.Sconit.Utility;
using com.Sconit.Entity.Dss;
using System.Text;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

public partial class Finance_Bill_Edit : ListModuleBase
{
    public event EventHandler BackEvent;

    public decimal OrderId
    {
        get
        {
            return (decimal)ViewState["OrderId"];
        }
        set
        {
            ViewState["OrderId"] = value;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            KPOrder kpOrder = TheKPOrderMgr.LoadKPOrder(this.OrderId, true);
            IList<object> list = new List<object>();
            if (kpOrder != null)
            {
                list.Add(kpOrder);
                list.Add(kpOrder.KPItems);
            }

            string barCodeUrl = "";
            //测试
            //if (kpOrder.SYS_CODE == "YK")
            //{
            //    barCodeUrl = WriteToFile("Bill_BJ.xls", list);
            //}
            if (kpOrder.SYS_CODE == "YK")
            {
                barCodeUrl = TheReportMgr.WriteToFile("Bill.xls", list);
            }
            if (kpOrder.SYS_CODE == "BJ")
            {
                barCodeUrl = WriteToFile("Bill_BJ.xls", list);
            }
                Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + barCodeUrl + "'); </script>");

            kpOrder.ORDER_PRINT = "Y";
            kpOrder.PRINT_MODIFY_DATE = DateTime.Now;
            TheKPOrderMgr.UpdateKPOrder(kpOrder);

            this.ShowSuccessMessage("MasterData.Bill.Print.Successful");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    public string WriteToFile(string template,IList<object> list)
    {
        string path = Server.MapPath(".") + @"\Reports\Templates\YFKExcelTemplates\" + template;
        if (File.Exists(path))
        {
            KPOrder kpOrder = (KPOrder)list[0];
            string filename = @"/Reports/Templates/TempFiles/temp_" + DateTime.Now.ToString("yyyyMMddhhmmss") + kpOrder.ORDER_ID + ".xls";
            string _wpath = Server.MapPath(".") + filename;
            File.Copy(path, _wpath);
            FileStream file = new FileStream(_wpath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            Sheet sheet = hssfworkbook.GetSheet("sheet1");
            CellStyle normalLeftStyle = setCellstyle(hssfworkbook, new string[] { "Border", "Left" });
            CellStyle normalRightStyle = setCellstyle(hssfworkbook, new string[] { "Border", "Right" });
            CellStyle NoneLeftStyle = setCellstyle(hssfworkbook, new string[] { "BorderNone", "Left" });
            CellStyle NoneRightStyle = setCellstyle(hssfworkbook, new string[] { "BorderNone", "Right" });
            CellStyle dateStyle = setCellstyle(hssfworkbook, new string[] { "Border", "Left", "DateTime" });
            Cell cell = sheet.GetRow(8).GetCell(2);
            cell.SetCellValue(kpOrder.PARTY_FROM_ID);
            cell = sheet.GetRow(8).GetCell(10);
            cell.SetCellValue(kpOrder.QAD_ORDER_ID); 
            int i = 10;
            decimal cnt = 0;
           
            foreach (KPItem kpitem in kpOrder.KPItems)
            {
                Row row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(kpitem.PURCHASE_ORDER_ID);     //采购单
                row.GetCell(0).CellStyle = normalLeftStyle;
                row.CreateCell(1).SetCellValue(kpitem.PART_CODE);     //零件号
                row.GetCell(1).CellStyle = normalLeftStyle;
                row.CreateCell(2).SetCellValue(kpitem.INCOMING_ORDER_ID);     //入库单号
                row.GetCell(2).CellStyle = normalLeftStyle;
                row.CreateCell(3).SetCellValue(kpitem.SEQ_ID);     //序号
                row.GetCell(3).CellStyle = normalLeftStyle;
                row.CreateCell(4).SetCellValue((double)kpitem.INCOMING_QTY);     //入库数量
                row.GetCell(4).CellStyle = normalLeftStyle;
                row.CreateCell(5).SetCellValue((double)kpitem.PRICE);     //采购单价
                row.GetCell(5).CellStyle = normalRightStyle;
                row.CreateCell(6).SetCellValue(kpitem.UM);     //单位
                row.GetCell(6).CellStyle = normalRightStyle;
                row.CreateCell(7).SetCellValue((double)kpitem.PRICE1);     //发票单价
                row.GetCell(7).CellStyle = normalRightStyle;
                row.CreateCell(8).SetCellValue(kpitem.PRICE2.ToString());     //发票单价@金额  
                row.GetCell(8).CellStyle = normalRightStyle;
                row.CreateCell(9).SetCellValue(kpitem.PART_NAME);     //零件名称
                row.GetCell(9).CellStyle = normalLeftStyle;
                row.CreateCell(10).SetCellValue(kpitem.DELIVER_ORDER_ID);    //送货单号
                row.GetCell(10).CellStyle = normalLeftStyle;
                row.CreateCell(11).SetCellValue((DateTime)kpitem.INCOMING_DATE);    //入库日期
                row.GetCell(11).CellStyle = dateStyle;

                cnt =(decimal)kpitem.PRICE2 + cnt;                

                i++;
            }            

            Row _row = sheet.CreateRow(i);
            _row.CreateCell(1).SetCellValue("采购员：");
            _row.CreateCell(6).SetCellValue("主管：");
            _row.CreateCell(9).SetCellValue("合计发票金额：");
            _row.CreateCell(10).SetCellValue(cnt.ToString());
            _row.GetCell(1).CellStyle = NoneRightStyle;
            _row.GetCell(6).CellStyle = NoneRightStyle;
            _row.GetCell(9).CellStyle = NoneRightStyle;
            _row.GetCell(10).CellStyle = NoneLeftStyle;

            MemoryStream ms = new MemoryStream();
            hssfworkbook.Write(ms);

            FileStream f = new FileStream(_wpath, FileMode.Open, FileAccess.Write);
            byte[] data = ms.ToArray();
            f.Write(data, 0, data.Length);
            f.Close();
            f.Dispose();
            hssfworkbook = null;
            ms.Close();
            ms.Dispose();
            return "http://" + Request.Url.Authority +  filename;
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
    public CellStyle setCellstyle(HSSFWorkbook hssfworkbook, string[] options)
    {
        CellStyle style3 = hssfworkbook.CreateCellStyle();
        foreach (string s in options)
        {
            if (s == "Border")
            {
                style3.BorderBottom = CellBorderType.THIN;
                style3.BorderLeft = CellBorderType.THIN;
                style3.BorderRight = CellBorderType.THIN;
                style3.BorderTop = CellBorderType.THIN;
            }
            if (s == "BorderNone")
            {
                style3.BorderBottom = CellBorderType.NONE;
                style3.BorderLeft = CellBorderType.NONE;
                style3.BorderRight = CellBorderType.NONE;
                style3.BorderTop = CellBorderType.NONE;
            }
            if (s == "Left")
            {
                style3.VerticalAlignment = VerticalAlignment.CENTER;
                style3.Alignment = HorizontalAlignment.LEFT;
            }
            if (s == "Right")
            {
                style3.VerticalAlignment = VerticalAlignment.CENTER;
                style3.Alignment = HorizontalAlignment.RIGHT;
            }
            if (s == "DateTime")
            {
                DataFormat format = hssfworkbook.CreateDataFormat();
                style3.DataFormat = format.GetFormat("yyyy-mm-dd");
            }
        }
        Font font3 = hssfworkbook.CreateFont();
        font3.FontHeightInPoints = 9;
        style3.SetFont(font3);
        return style3;
    }


    protected void btnSubmitInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            KPOrder kpOrder = TheKPOrderMgr.LoadKPOrder(this.OrderId, true);

            decimal invoiceAmountWithoutTax = Convert.ToDecimal(tbInvoiceAmountWithoutTax.Text.Trim());
            

            #region 只校验不含税金额
            if (System.Math.Abs(kpOrder.TotalAmount  - invoiceAmountWithoutTax) > 1)
            {
                ShowErrorMessage("MasterData.Bill.InvoiceAmountWithoutTax.AmountMustLessThanOne");
                return;
            }

            #endregion

            kpOrder.InvoiceCount = Convert.ToInt32(tbInvoceCount.Text.Trim());
      
            kpOrder.InvoiceDate = DateTime.Parse(tbInvoiceDate.Text.Trim());
            kpOrder.InvoiceNumber = tbInvoiceNumber.Text.Trim();
            kpOrder.InvoiceRemark = tbInvoiceRemark.Text.Trim();
            kpOrder.InvoiceTax = tbInvoiceTax.Text.Trim();
            kpOrder.InvoiceAmountWithoutTax = invoiceAmountWithoutTax;

            kpOrder.InvoiceAmount =Convert.ToDecimal(tbInvoiceAmount.Text.Trim());
            kpOrder.InvoiceStatus = BusinessConstants.CODE_MASTER_INVOICE_STATUS_VALUE_INPROCESS;
            TheKPOrderMgr.UpdateKPOrder(kpOrder);
            this.ShowSuccessMessage("MasterData.Bill.SubmitInvoice.Successful");
            UpdateView();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnRejectInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            KPOrder kpOrder = TheKPOrderMgr.LoadKPOrder(this.OrderId, true);
            kpOrder.InvoiceStatus = BusinessConstants.CODE_MASTER_INVOICE_STATUS_VALUE_REJECTED;
            TheKPOrderMgr.UpdateKPOrder(kpOrder);
            this.ShowSuccessMessage("MasterData.Bill.RejectInvoice.Successful");
            UpdateView();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnApproveInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            KPOrder kpOrder = TheKPOrderMgr.LoadKPOrder(this.OrderId, true);
            kpOrder.InvoiceStatus = BusinessConstants.CODE_MASTER_INVOICE_STATUS_VALUE_APPROVED;
            TheKPOrderMgr.UpdateKPOrder(kpOrder);
            CreateFile(kpOrder);
            this.ShowSuccessMessage("MasterData.Bill.ApproveInvoice.Successful");
            UpdateView();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        KPOrder kpOrder = TheKPOrderMgr.LoadKPOrder(this.OrderId, true);
        this.Gv_List.DataSource = kpOrder.KPItems;
        this.ExportXLS(this.Gv_List, "KPOrder" + DateTime.Now.ToString("ddhhmmss") + ".xls");
    }

    public void InitPageParameter(decimal orderId)
    {
        this.OrderId = orderId;
        KPOrder kpOrder = TheKPOrderMgr.LoadKPOrder(orderId, true);
        this.tbOrderId.Text = kpOrder.QAD_ORDER_ID;
        this.tbTotalAmount.Text = kpOrder.TotalAmount.ToString("0.########");
        if (kpOrder.ORDER_PUB_DATE != null)
        {
            this.tbCreateDate.Text = ((DateTime)kpOrder.ORDER_PUB_DATE).ToString("yyyy-MM-dd");
        }

        UpdateView();

        this.Gv_List.DataSource = kpOrder.KPItems;
        this.Gv_List.DataBind();

    }

    public override void UpdateView()
    {
        KPOrder kpOrder = TheKPOrderMgr.LoadKPOrder(this.OrderId);
        this.tbInvoceCount.Text = kpOrder.InvoiceCount.ToString();
        this.tbInvoiceAmount.Text = kpOrder.InvoiceAmount.ToString("F2");
        this.tbInvoiceDate.Text = kpOrder.InvoiceDate.ToString();
        this.tbInvoiceNumber.Text = kpOrder.InvoiceNumber;
        this.tbInvoiceRemark.Text = kpOrder.InvoiceRemark;
        this.tbInvoiceTax.Text = Convert.ToDecimal(kpOrder.InvoiceTax).ToString("F2");
        this.tbInvoiceAmountWithoutTax.Text = kpOrder.InvoiceAmountWithoutTax.ToString("F2");

        if ( kpOrder.InvoiceStatus == null || kpOrder.InvoiceStatus == string.Empty||
            kpOrder.InvoiceStatus == BusinessConstants.CODE_MASTER_INVOICE_STATUS_VALUE_REJECTED)
        {
            this.btnSubmitInvoice.Visible = true;
            this.btnRejectInvoice.Visible = false;
            this.btnApproveInvoice.Visible = false;

            this.tbInvoceCount.ReadOnly = false;
            this.tbInvoiceAmountWithoutTax.ReadOnly = false;
            this.tbInvoiceDate.ReadOnly = false;
            this.tbInvoiceNumber.ReadOnly = false;
            this.tbInvoiceRemark.ReadOnly = false;
            this.tbInvoiceTax.ReadOnly = false;

            this.tbInvoiceTax.Attributes["onchange"] += "calculate();";
            this.tbInvoiceAmountWithoutTax.Attributes["onchange"] += "calculate();";

        }
        else if (kpOrder.InvoiceStatus == BusinessConstants.CODE_MASTER_INVOICE_STATUS_VALUE_INPROCESS)
        {
            this.btnSubmitInvoice.Visible = false;
            this.btnRejectInvoice.Visible = true;
            this.btnApproveInvoice.Visible = true;

            this.tbInvoceCount.ReadOnly = true;
            this.tbInvoiceAmountWithoutTax.ReadOnly = true;
            this.tbInvoiceDate.ReadOnly = true;
            this.tbInvoiceNumber.ReadOnly = true;
            this.tbInvoiceRemark.ReadOnly = true;
            this.tbInvoiceTax.ReadOnly = true;
        }
        else if (kpOrder.InvoiceStatus == BusinessConstants.CODE_MASTER_INVOICE_STATUS_VALUE_APPROVED)
        {
            this.btnSubmitInvoice.Visible = false;
            this.btnRejectInvoice.Visible = false;
            this.btnApproveInvoice.Visible = false;

            this.tbInvoceCount.ReadOnly = true;
            this.tbInvoiceAmountWithoutTax.ReadOnly = true;
            this.tbInvoiceDate.ReadOnly = true;
            this.tbInvoiceNumber.ReadOnly = true;
            this.tbInvoiceRemark.ReadOnly = true;
            this.tbInvoiceTax.ReadOnly = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }



    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (this.BackEvent != null)
        {
            this.BackEvent(this, e);
        }
    }

    private void CreateFile(KPOrder kpOrder)
    {
        
        //随便写写，谁叫他们不给钱的
        #region 抽取数据导入文件
        string fileFolder="D:\\Dss\\out\\";
        string fileName = "SCONIT_QAD_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "_KPCONF.REQ";
        string[] line1 = new string[] 
            { 
                "1",
                kpOrder.QAD_ORDER_ID,
                DateTime.Now.ToShortDateString()
            };

        string[][] data = new string[][] { line1 };
        StreamWriter streamWriter = new StreamWriter(fileFolder + fileName, false, Encoding.GetEncoding(Encoding.Default.WebName));
        FlatFileWriter flatFileWriter = new FlatFileWriter(streamWriter, Environment.NewLine, "|");
        flatFileWriter.Write(data);
        flatFileWriter.Dispose();
        #endregion
    }
}
