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
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Entity.Transportation;

public partial class Transportation_Vehicle_Edit : EditModuleBase
{
    private Vehicle vehicle;
    public event EventHandler BackEvent;

    protected string VehicleCode
    {
        get
        {
            return (string)ViewState["VehicleCode"];
        }
        set
        {
            ViewState["VehicleCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string code)
    {
        this.VehicleCode = code;
        this.ODS_Vehicle.SelectParameters["code"].DefaultValue = this.VehicleCode;
        this.ODS_Vehicle.DeleteParameters["code"].DefaultValue = this.VehicleCode;
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
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

    protected void FV_Vehicle_DataBound(object sender, EventArgs e)
    {
        if (VehicleCode != null && VehicleCode != string.Empty)
        {
            Vehicle vehicle = (Vehicle)((FormView)sender).DataItem;

            Controls_TextBox tbCarrier = (Controls_TextBox)this.FV_Vehicle.FindControl("tbCarrier");
            tbCarrier.ServiceParameter = "string:" + this.CurrentUser.Code;
            tbCarrier.DataBind();

            if (vehicle.Carrier != null && vehicle.Carrier.Code.Trim() != string.Empty)
            {
                tbCarrier.Text = vehicle.Carrier.Code;
            }

            com.Sconit.Control.CodeMstrDropDownList ddlType = (com.Sconit.Control.CodeMstrDropDownList)this.FV_Vehicle.FindControl("ddlType");

            if (vehicle.Type != null)
            {
                ddlType.SelectedValue = vehicle.Type;
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_Vehicle_Updating(object sender, ObjectDataSourceMethodEventArgs e)
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

    protected void ODS_Vehicle_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Transportation.Vehicle.UpdateVehicle.Successfully", VehicleCode);
        
    }

    protected void ODS_Vehicle_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Transportation.Vehicle.DeleteVehicle.Successfully", VehicleCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Transportation.Vehicle.DeleteVehicle.Fail", VehicleCode);
            e.ExceptionHandled = true;
        }
    }
}
