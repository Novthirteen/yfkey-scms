using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;

public partial class Warehouse_InProcessLocation_AjaxViewMain : MainModuleBase
{
    public event EventHandler SaveEvent;
    public event EventHandler BackEvent;
    public event EventHandler CloseEvent;

    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }
    private string IpNo
    {
        get { return (string)ViewState["IpNo"]; }
        set { ViewState["IpNo"] = value; }
    }
    private string Action
    {
        get { return (string)ViewState["Action"]; }
        set { ViewState["Action"] = value; }
    }

    public string AsnType
    {
        get { return (string)ViewState["AsnType"]; }
        set { ViewState["AsnType"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucDetailList.BackEvent += new EventHandler(this.DetailListBack_Render);
        if (!IsPostBack)
        {
            this.ucEdit.AsnType = this.AsnType;
        }
    }

    public void InitPageParameter(string ipNo)
    {
        this.InitPageParameter(ipNo, false);
    }

    public void InitPageParameter(string ipNo,string action)
    {
        this.IpNo = ipNo;
        this.Action = action;
        this.ucEdit.Action = action;
        this.ucEdit.InitPageParameter(ipNo);
        this.ucDetailList.InitPageParameter(ipNo);
        this.btnClose.Visible = true;
    }

    public void InitPageParameter(string ipNo, bool printASN)
    {
        InProcessLocation ip = TheInProcessLocationMgr.LoadInProcessLocation(ipNo, true);
        this.InitPageParameter(ip, printASN);
    }
    public void InitPageParameter(InProcessLocation ip)
    {
        this.InitPageParameter(ip, false);
    }
    public void InitPageParameter(InProcessLocation ip, bool printASN)
    {
        this.IpNo = ip.IpNo;
        this.Action = "View";
        this.ucEdit.Action = "View";
        this.btnClose.Visible = false;
        this.ucEdit.InitPageParameter(ip.IpNo);
        this.ucDetailList.InitPageParameter(ip);
        //if (ip.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
        //{
        //    this.btnUpdate.Visible = true;
        //}
        if (printASN)
        {
            this.PrintASN(ip);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        this.ucEdit.UpdateInProcessLocation();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
       
        this.PrintASN();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        this.ucEdit.CloseInProcessLocation();
        this.Visible = false;
        ShowSuccessMessage("InprocessLocation.Close.Successfully");
        if (CloseEvent != null)
        {
            CloseEvent(this, null);
        }
    }

    void DetailListBack_Render(object sender, EventArgs e)
    {
        this.Visible = false;
        if (BackEvent != null)
        {
            BackEvent(this, null);
        }
    }

    private void PrintASN()
    {
        InProcessLocation inProcessLocation = TheInProcessLocationMgr.LoadInProcessLocation(IpNo, true);
        this.PrintASN(inProcessLocation);
    }
    private void PrintASN(InProcessLocation inProcessLocation)
    {
       // inProcessLocation.InProcessLocationDetails = TheInProcessLocationDetailMgr.SummarizeInProcessLocationDetails(inProcessLocation.InProcessLocationDetails);
        if (inProcessLocation.AsnTemplate == null || inProcessLocation.AsnTemplate == string.Empty)
        {
            ShowErrorMessage("ASN.PrintError.NoASNTemplate");
            return;
        }

        IList<object> list = new List<object>();
        list.Add(inProcessLocation);
        list.Add(inProcessLocation.InProcessLocationDetails);

       //报表url
        string asnUrl = TheReportMgr.WriteToFile(inProcessLocation.AsnTemplate, list);
        //客户端打印
        //如果在UpdatePanel中调用JavaScript需要使用 ScriptManager.RegisterClientScriptBlock
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + asnUrl + "'); </script>", false);
        //Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + asnUrl + "'); </script>");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }
}
