using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;

public partial class MasterData_Location_StorageArea_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string LocationCode
    {
        get { return (string)ViewState["LocationCode"]; }
        set { ViewState["LocationCode"] = value; }
    }

    protected string AreaCode
    {
        get { return (string)ViewState["AreaCode"]; }
        set { ViewState["AreaCode"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
        if (BackEvent != null)
        {
            BackEvent((object)AreaCode, null);
        }
    }

    public void InitPageParameter(StorageArea area)
    {
        this.ODS_FV_StorageArea.SelectParameters["Code"].DefaultValue = area.Code;
        this.LocationCode = area.Location.Code;
        this.AreaCode = area.Code;
        //((TextBox)(this.FV_StorageArea.FindControl("tbAreaCode"))).Text = area.Code;
        //((TextBox)(this.FV_StorageArea.FindControl("tbLocationCode"))).Text = this.LocationCode;
        //((TextBox)(this.FV_StorageArea.FindControl("tbAreaDescription"))).Text = area.Description;
        //((CheckBox)(this.FV_StorageArea.FindControl("cbIsActive"))).Checked = area.IsActive;
        //((CheckBox)(this.FV_StorageBin.FindControl("cbIsActive"))).Checked = true;
        //this.ucBin.InitPageParameter(area.Code);
    }

    // 编辑库区
    protected void ODS_StorageArea_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.Location.UpdataArea.Successfully", ((TextBox)(this.FV_StorageArea.FindControl("tbAreaCode"))).Text);
        btnBack_Click(this, e);
    }

    protected void ODS_StorageArea_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        StorageArea area = (StorageArea)e.InputParameters[0];
        if (area != null)
        {
            string location = this.LocationCode;
            area.Location = TheLocationMgr.LoadLocation(location);
            area.Description = area.Description.Trim();
        }
    }

    protected void ODS_StorageArea_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.Location.DeleteArea.Successfully", ((TextBox)(this.FV_StorageArea.FindControl("tbAreaCode"))).Text);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.Location.DeleteArea.Fail", ((TextBox)(this.FV_StorageArea.FindControl("tbAreaCode"))).Text);
            e.ExceptionHandled = true;
        }
    }

   
}
