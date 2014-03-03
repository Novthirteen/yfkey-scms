using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;

public partial class Order_GoodsReceipt_ViewReceipt_AjaxViewMain : MainModuleBase
{
    public event EventHandler BackEvent;

    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }
    private string ReceiptNo
    {
        get { return (string)ViewState["ReceiptNo"]; }
        set { ViewState["ReceiptNo"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ucList.ModuleType = this.ModuleType;
        }
    }

    public void InitPageParameter(string receiptNo, bool printReceipt)
    {
        Receipt receipt = TheReceiptMgr.LoadReceipt(receiptNo, true);
        this.InitPageParameter(receipt, printReceipt);
    }
    public void InitPageParameter(Receipt receipt, bool printReceipt)
    {
        this.ReceiptNo = receipt.ReceiptNo;
        this.ucEdit.InitPageParameter(ReceiptNo);
        this.ucList.InitPageParameter(receipt);
        if (printReceipt)
        {
            this.PrintReceipt(receipt);
        }
    }
    public void InitPageParameter(Resolver resolver, bool printReceipt)
    {
        this.ReceiptNo = resolver.Code;
        this.ucEdit.InitPageParameter(resolver.Code);
        this.ucList.InitPageParameter(resolver);
        if (printReceipt)
        {
            Receipt receipt = TheReceiptMgr.LoadReceipt(resolver.Code, true);
            this.PrintReceipt(receipt);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        this.PrintReceipt();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
        if (BackEvent != null)
        {
            BackEvent(this, null);
        }
    }

    private void PrintReceipt()
    {
        Receipt receipt = TheReceiptMgr.LoadReceipt(ReceiptNo, true);
        this.PrintReceipt(receipt);
    }

    private void PrintReceipt(Receipt receipt)
    {
        receipt.ReceiptDetails = TheReceiptDetailMgr.SummarizeReceiptDetails(receipt.ReceiptDetails);

        IList<object> list = new List<object>();
        list.Add(receipt);
        list.Add(receipt.ReceiptDetails);
        //TheReportReceiptNoteMgr.FillValues(receipt.ReceiptTemplate, list);
        //报表url
        string strUrl = TheReportMgr.WriteToFile(receipt.ReceiptTemplate, list);
        //客户端打印
        //如果在UpdatePanel中调用JavaScript需要使用 ScriptManager.RegisterClientScriptBlock
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + strUrl + "'); </script>", false);
        //Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + strUrl + "'); </script>");

        receipt.IsPrinted = true;
        TheReceiptMgr.UpdateReceipt(receipt);
    }
}
