using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class Inventory_InspectOrder_InspectOrderInfo : ModuleBase
{
    public event EventHandler BackEvent;

    private bool IsPartQualified
    {
        get
        {
            return (bool)ViewState["IsPartQualified"];
        }
        set
        {
            ViewState["IsPartQualified"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(sender, e);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string inspectNo = this.lbInspectNo.Text;
        string printUrl = TheReportMgr.WriteToFile("InspectOrder.xls", inspectNo);
        Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + printUrl + "'); </script>");
        this.ShowSuccessMessage("MasterData.Inventory.InspectOrder.Print.Successful");
    }

    protected void btnUnqualifiedPrint_Click(object sender, EventArgs e)
    {
        string inspectNo = this.lbInspectNo.Text;
        IList<object> list = new List<object>();
        IList<InspectResult> inspectResultList = this.ucDetailList.PopulateUnqualifiedInspectOrder();
        if (inspectResultList == null || inspectResultList.Count == 0)
        {
            ShowWarningMessage("MasterData.Inventory.InspectOrder.Unqualified.Empty");
            return;
        }
        InspectOrder inspectOrder = TheInspectOrderMgr.LoadInspectOrder(inspectNo);
        list.Add(inspectOrder);
        list.Add(inspectResultList);
        string printUrl = TheReportMgr.WriteToFile("BelowBrade.xls", list);
        Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + printUrl + "'); </script>");
        this.ShowSuccessMessage("MasterData.Inventory.InspectOrder.Unqualified.Print.Successful");

    }


    protected void btnInspect_Click(object sender, EventArgs e)
    {
        try
        {
            TheInspectOrderMgr.PendInspectOrder(this.ucDetailList.PopulateInspectOrder(null), this.CurrentUser);
            this.ShowSuccessMessage("MasterData.InspectOrder.Process.Successfully", this.lbInspectNo.Text);
            this.InitPageParameter(this.lbInspectNo.Text);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }

    }

    protected void btnQualified_Click(object sender, EventArgs e)
    {
        try
        {
            TheInspectOrderMgr.PendInspectOrder(this.ucDetailList.PopulateInspectOrder(true), this.CurrentUser);
            this.ShowSuccessMessage("MasterData.InspectOrder.Process.Successfully", this.lbInspectNo.Text);
            this.InitPageParameter(this.lbInspectNo.Text);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }

    }

    protected void btnUnQualified_Click(object sender, EventArgs e)
    {
        try
        {
            TheInspectOrderMgr.PendInspectOrder(this.ucDetailList.PopulateInspectOrder(false), this.CurrentUser);
            this.ShowSuccessMessage("MasterData.InspectOrder.Process.Successfully", this.lbInspectNo.Text);
            this.InitPageParameter(this.lbInspectNo.Text);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }

    }

    public void InitPageParameter(string inspectNo)
    {

        InitPageParameter(inspectNo, false);
    }

    public void InitPageParameter(string inspectNo, bool isWorkShop)
    {
        InspectOrder inspectOrder = TheInspectOrderMgr.LoadInspectOrder(inspectNo);
        this.lbInspectNo.Text = inspectOrder.InspectNo;
        this.lbCreateUser.Text = inspectOrder.CreateUser.Name;
        this.lbCreateDate.Text = inspectOrder.CreateDate.ToString("yyyy-MM-dd");
        this.lbStatus.Text = inspectOrder.Status;

        this.IsPartQualified = bool.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_ALLOW_PART_QUALIFIED).Value);

        this.btnQualified.Visible = (inspectOrder.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE) && !this.IsPartQualified && inspectOrder.IsDetailHasHu && !isWorkShop;
        this.btnUnqalified.Visible = (inspectOrder.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE) && !this.IsPartQualified && inspectOrder.IsDetailHasHu && !isWorkShop;
        this.btnInspect.Visible = (inspectOrder.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE) && (this.IsPartQualified || !inspectOrder.IsDetailHasHu) && !isWorkShop;
        this.btnUnqualifiedPrint.Visible = !isWorkShop;

        this.ucDetailList.IsPartQualified = this.IsPartQualified;
        this.ucDetailList.InitPageParameter(inspectNo, isWorkShop);
    }


}
