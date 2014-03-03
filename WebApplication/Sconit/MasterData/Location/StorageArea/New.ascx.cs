using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;

public partial class MasterData_Location_StorageArea_New : NewModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
        this.Visible = false;
    }

    protected void ODS_StorageArea_Inserting(object source, ObjectDataSourceMethodEventArgs e)
    {
        StorageArea storageArea = (StorageArea)e.InputParameters[0];
        string locationCode = ((TextBox)(this.FV_StorageArea.FindControl("tbLocationCode"))).Text.Trim();
        string areaCode = ((TextBox)(this.FV_StorageArea.FindControl("tbAreaCode"))).Text.Trim();
        string areaDescription = ((TextBox)(this.FV_StorageArea.FindControl("tbAreaDescription"))).Text.Trim();
        if (areaCode == null || areaCode == string.Empty)
        {
            ShowErrorMessage("MasterData.Location.Area.Required.Code");
            e.Cancel = true;
            return;
        }
        storageArea.Code = areaCode;
        storageArea.Description = areaDescription;
        storageArea.Location = TheLocationMgr.LoadLocation(locationCode);
        ShowSuccessMessage("MasterData.Location.Area.AddArea.Successfully", areaCode);
        if (CreateEvent != null)
        {
            CreateEvent(storageArea, null);
            this.Visible = false;
        }
    }

    public void InitPageParameter(string code)
    {
        ((TextBox)(this.FV_StorageArea.FindControl("tbLocationCode"))).Text = code;
        ((TextBox)(this.FV_StorageArea.FindControl("tbAreaCode"))).Text = string.Empty;
        ((TextBox)(this.FV_StorageArea.FindControl("tbAreaDescription"))).Text = string.Empty;
        ((CheckBox)(this.FV_StorageArea.FindControl("cbIsActive"))).Checked = true;
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;

        switch (cv.ID)
        {
            case "cvAreaCode":
                string areaCode = args.Value.Trim();
                if (TheStorageAreaMgr.LoadStorageArea(areaCode) != null)
                {
                    ShowErrorMessage("MasterData.Location.Area.IsExist", areaCode);
                    args.IsValid = false;
                }
                break;
            default:
                break;
        }
    }
}
