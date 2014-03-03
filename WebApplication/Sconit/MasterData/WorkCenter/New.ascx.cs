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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using com.Sconit.Entity;

public partial class MasterData_WorkCenter_New : NewModuleBase
{
    private WorkCenter workCenter;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    public string ParentCode
    {
        get
        {
            return (string)ViewState["ParentCode"];
        }
        set
        {
            ViewState["ParentCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void PageCleanup()
    {
        ((TextBox)(this.FV_WorkCenter.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbName"))).Text = string.Empty;

        ((TextBox)(this.FV_WorkCenter.FindControl("tbLaborBurdenPercent"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbLaborBurdenRate"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbSetupBurdenPercent"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbSetupBurdenRate"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbLaborRate"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbMachine"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbMachineQty"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbMachineBurdenRate"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbMachineSetupBurdenRate"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbRunCrew"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbSetupCrew"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbSetupRate"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbQueueTime"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbWaitTime"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbPercentEfficiency"))).Text = string.Empty;
        ((TextBox)(this.FV_WorkCenter.FindControl("tbPercentUtilization"))).Text = string.Empty;
        ((CheckBox)(this.FV_WorkCenter.FindControl("tbIsActive"))).Checked = false;
        ((Literal)(this.FV_WorkCenter.FindControl("lbCurrentParty"))).Text = this.ParentCode;
        Controls_TextBox tbType = (Controls_TextBox)(this.FV_WorkCenter.FindControl("tbType"));
        tbType.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_WORKCENTER_TYPE;
        tbType.DataBind();
    }

    protected void ODS_WorkCenter_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        workCenter = (WorkCenter)e.InputParameters[0];
        workCenter.Party = ThePartyMgr.LoadParty(this.ParentCode);
        workCenter.Type = ((Controls_TextBox)(this.FV_WorkCenter.FindControl("tbType"))).Text;
    }

    protected void ODS_WorkCenter_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(workCenter.Code, e);
            ShowSuccessMessage("MasterData.WorkCenter.AddWorkCenter.Successfully", workCenter.Code);
        }
    }

    protected void checkWorkCenterExists(object source, ServerValidateEventArgs args)
    {
        string code = ((TextBox)(this.FV_WorkCenter.FindControl("tbCode"))).Text;
        //if (PartyMgr.GetPartyCount(code) != 0)
        //{
        //    args.IsValid = false;
        //}
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }


}
