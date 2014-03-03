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
using com.Sconit.Entity.Transportation;
using com.Sconit.Service.Transportation;

public partial class Transportation_Vehicle_New : NewModuleBase
{
    private Vehicle vehicle;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        Controls_TextBox tbCarrier = (Controls_TextBox)(this.FV_Vehicle.FindControl("tbCarrier"));
        tbCarrier.ServiceParameter = "string:" + this.CurrentUser.Code;
    }
    
    public void PageCleanup()
    {
        ((TextBox)(this.FV_Vehicle.FindControl("tbCode"))).Text = string.Empty;
        ((CheckBox)(this.FV_Vehicle.FindControl("cbIsActive"))).Checked = true;
        ((Controls_TextBox)(this.FV_Vehicle.FindControl("tbCarrier"))).Text = string.Empty;
        ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_Vehicle.FindControl("ddlType"))).SelectedIndex = 0;
        ((TextBox)(this.FV_Vehicle.FindControl("tbDriver"))).Text = string.Empty;
        ((TextBox)(this.FV_Vehicle.FindControl("tbMobilePhone"))).Text = string.Empty;
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
            case "cvCode":
                if (TheVehicleMgr.LoadVehicle(args.Value) != null)
                {
                    ShowWarningMessage("Transportation.Vehicle.Code.Exists", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvCarrier":
                if (TheCarrierMgr.LoadCarrier(args.Value) == null)
                {
                    ShowWarningMessage("Transportation.Carrier.Code.NotExist", args.Value);
                    args.IsValid = false;
                }
                break;
            default:
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_Vehicle_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Controls_TextBox tbCarrier = ((Controls_TextBox)(this.FV_Vehicle.FindControl("tbCarrier")));
        com.Sconit.Control.CodeMstrDropDownList ddlType = ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_Vehicle.FindControl("ddlType")));

        vehicle = (Vehicle)e.InputParameters[0];
        vehicle.Code = vehicle.Code.Trim();
        vehicle.Carrier = TheCarrierMgr.LoadCarrier(tbCarrier.Text.Trim());
        vehicle.Type = ddlType.SelectedValue;
        vehicle.Driver = vehicle.Driver.Trim();
        vehicle.MobilePhone = vehicle.MobilePhone.Trim();
    }

    protected void ODS_Vehicle_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(vehicle.Code, e);
            ShowSuccessMessage("Transportation.Vehicle.AddVehicle.Successfully", vehicle.Code);
        }
    }
}
