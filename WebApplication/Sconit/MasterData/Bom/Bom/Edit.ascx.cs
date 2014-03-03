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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;

public partial class MasterData_Bom_Bom_Edit : EditModuleBase
{
    private Bom bom;
    public event EventHandler BackEvent;

    protected string code
    {
        get
        {
            return (string)ViewState["code"];
        }
        set
        {
            ViewState["code"] = value;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void InitPageParameter(string code)
    {
        this.code = code;
        this.ODS_Bom.SelectParameters["Code"].DefaultValue = code;
        this.ODS_Bom.DeleteParameters["Code"].DefaultValue = code;
        this.UpdateView();
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;

        switch (cv.ID)
        {
            case "cvUom":
                if (TheUomMgr.LoadUom(args.Value) == null)
                {
                    ShowWarningMessage("MasterData.Bom.WarningMessage.UomInvalid", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvRegion":
                if (args.Value.Trim() != "")
                {
                    if (TheRegionMgr.LoadRegion(args.Value) == null)
                    {
                        ShowWarningMessage("MasterData.Bom.WarningMessage.RegionInvalid", args.Value);
                        args.IsValid = false;
                    }
                }
                break;
            default:
                break;
        }
    }

    protected void FV_Bom_DataBound(object sender, EventArgs e)
    {
        this.UpdateView();
    }

    private void UpdateView()
    {
        bom = TheBomMgr.LoadBom(this.code);
        Controls_TextBox tbUom = (Controls_TextBox)(this.FV_Bom.FindControl("tbUom"));
        TextBox tbRegion = (TextBox)(this.FV_Bom.FindControl("tbRegion"));

        if (bom.Uom != null)
        {
            tbUom.Text = bom.Uom.Code;
        }
        if (bom.Region != null)
        {
            tbRegion.Text = bom.Region.Code;
        }
    }

    protected void ODS_Bom_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string uom = ((Controls_TextBox)(this.FV_Bom.FindControl("tbUom"))).Text.Trim();
        string region = ((TextBox)(this.FV_Bom.FindControl("tbRegion"))).Text.Trim();

        bom = (Bom)e.InputParameters[0];
        bom.Uom = TheUomMgr.LoadUom(uom);
        if (region == "")
        {
            bom.Region = null;
        }
        else
        {
            bom.Region = TheRegionMgr.LoadRegion(region);
        }
    }

    protected void ODS_Bom_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.WorkCalendar.Update.Successfully", bom.Code);
    }

    protected void ODS_Bom_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.Bom.Delete.Successfully", bom.Code);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.Bom.Delete.Failed", bom.Code);
            e.ExceptionHandled = true;
        }
    }
}
