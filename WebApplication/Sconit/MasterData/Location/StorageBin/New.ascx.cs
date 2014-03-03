using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity.MasterData;
using com.Sconit.Web;

public partial class MasterData_Location_StorageBin_New : NewModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(string locationCode, string areaCode)
    {
        Controls_TextBox tbAreaCode = (Controls_TextBox)(this.FV_StorageBin.FindControl("tbAreaCode"));
        TextBox tbAreaCode1 = (TextBox)(this.FV_StorageBin.FindControl("tbAreaCode1"));

        ((CheckBox)(this.FV_StorageBin.FindControl("cbIsActive"))).Checked = true;
        ((TextBox)(this.FV_StorageBin.FindControl("tbLocationCode"))).Text = locationCode;
        ((TextBox)(this.FV_StorageBin.FindControl("tbBinCode"))).Text = string.Empty;
        ((TextBox)(this.FV_StorageBin.FindControl("tbBinDescription"))).Text = string.Empty;


        tbAreaCode.Text = areaCode;
        tbAreaCode1.Text = areaCode;
        //if (areaCode != null && areaCode != string.Empty)
        //{
        //    tbAreaCode.Visible = false;
        //    tbAreaCode1.Visible = true;
        //}
        //else
        //{
        //    tbAreaCode.Visible = true;
        //    tbAreaCode1.Visible = false;
        //}
        //this.ucBin.InitPageParameter(area.Code);
    }
    //新增库格
    protected void ODS_StorageBin_Inserting(object source, ObjectDataSourceMethodEventArgs e)
    {
        StorageBin storageBin = (StorageBin)e.InputParameters[0];
        string binCode = ((TextBox)(this.FV_StorageBin.FindControl("tbBinCode"))).Text.Trim();
        string binDescription = ((TextBox)(this.FV_StorageBin.FindControl("tbBinDescription"))).Text.Trim();
        string areaCode = ((Controls_TextBox)(this.FV_StorageBin.FindControl("tbAreaCode"))).Text;
        if (binCode == null || binCode == string.Empty)
        {
            ShowErrorMessage("MasterData.Location.Bin.Required.Code");
            e.Cancel = true;
            return;
        }
        storageBin.Code = binCode;
        storageBin.Description = binDescription;
        storageBin.Area = TheStorageAreaMgr.LoadStorageArea(areaCode);
        ShowSuccessMessage("MasterData.Location.Bin.AddBin.Successfully", binCode);

    }
    protected void ODS_StorageBin_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //this.ucBin.InitPageParameter(((TextBox)(this.FV_StorageArea.FindControl("tbAreaCode"))).Text.Trim(), true);

        if (CreateEvent != null)
        {
            CreateEvent((object)(((Controls_TextBox)(this.FV_StorageBin.FindControl("tbAreaCode"))).Text), null);
            this.Visible = false;
        }
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;

        switch (cv.ID)
        {
            case "cvBinCode":
                string binCode = args.Value.Trim();
                if (TheStorageBinMgr.LoadStorageBin(binCode) != null)
                {
                    ShowErrorMessage("MasterData.Location.Bin.IsExist", binCode);
                    args.IsValid = false;
                }
                break;
            case "cvAreaCode":
                string areaCode = args.Value.Trim();
                if (TheStorageAreaMgr.LoadStorageArea(areaCode) == null)
                {
                    ShowErrorMessage("MasterData.Location.Area.IsNotExist", areaCode);
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
        this.Visible = false;
    }
}
