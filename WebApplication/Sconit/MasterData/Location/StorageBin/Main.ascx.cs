using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;

public partial class MasterData_Location_StorageBin_Main : ModuleBase
{
    public event EventHandler BackEvent;

    private string AreaCode
    {
        get { return (string)ViewState["AreaCode"]; }
        set { ViewState["AreaCode"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.GV_Bin.EditIndex = -1;
        this.ucNew.CreateEvent += new System.EventHandler(this.CreateBack_Render);
        this.ucNew.BackEvent += new System.EventHandler(this.BackEvent_Render);
    }
    private void CreateBack_Render(object sender, EventArgs e)
    {
        this.AreaCode = (string)sender;
        this.tbAreaCode.Text = this.AreaCode;
        this.divList.Visible = true;
        DoSearch();
    }
    private void BackEvent_Render(object sender, EventArgs e)
    {
        this.divList.Visible = true;
    }

    public void InitPageParameter(string LocationCode, string AreaCode)
    {
        this.divList.Visible = true;
        this.ucNew.Visible = false;
        this.AreaCode = AreaCode;
        this.tbLocationCode.Text = LocationCode;
        if (AreaCode != null && AreaCode != string.Empty)
        {
            //this.tbAreaCode.Visible = false;
            //this.tbAreaCode1.Visible = true;
            this.tbAreaCode1.Text = AreaCode;
            this.tbAreaCode.Text = AreaCode;
            DoSearch();
        }
       
        //else
        //{
        //    this.tbAreaCode.Visible = true;
        //    this.tbAreaCode1.Visible = false;
        //}
    }

    private void DoSearch()
    {
        IList<StorageBin> storageBinList = new List<StorageBin>();
        if (this.AreaCode == null || this.AreaCode == string.Empty)
        {
            storageBinList = TheStorageBinMgr.GetStorageBin(TheLocationMgr.LoadLocation(this.tbLocationCode.Text));
        }
        else
        {
            storageBinList = TheStorageBinMgr.GetStorageBin(this.tbAreaCode.Text);
        }

        this.GV_Bin.DataSource = storageBinList;
        this.GV_Bin.DataBind();
        if (storageBinList.Count == 0)
        {
            this.ltlMessage.Visible = true;
        }
        else
        {
            this.ltlMessage.Visible = false;
        }
    }

    protected void GV_Bin_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GV_Bin.EditIndex = e.NewEditIndex;
        this.DoSearch();
    }

    protected void GV_Bin_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        StorageBin storageBin = new StorageBin();

        if(this.AreaCode != null && this.AreaCode != string.Empty)
        {
            storageBin.Area = TheStorageAreaMgr.LoadStorageArea(this.AreaCode);
        }
        storageBin.Code = ((Literal)this.GV_Bin.Rows[e.RowIndex].FindControl("ltlCode")).Text.Trim();
        storageBin.Description = ((TextBox)this.GV_Bin.Rows[e.RowIndex].FindControl("tbDescription")).Text.Trim();
        storageBin.IsActive = ((CheckBox)this.GV_Bin.Rows[e.RowIndex].FindControl("cbActive")).Checked;
        TheStorageBinMgr.UpdateStorageBin(storageBin);
        this.GV_Bin.EditIndex = -1;

        this.DoSearch();
        ShowSuccessMessage("MasterData.Location.UpdataBin.Successfully", storageBin.Code);
    }

    protected void GV_Bin_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        this.GV_Bin.EditIndex = -1;
        this.DoSearch();
    }

    protected void GV_Bin_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string binCode = ((Literal)this.GV_Bin.Rows[e.RowIndex].FindControl("ltlCode")).Text.Trim();
        try
        {
            TheStorageBinMgr.DeleteStorageBin(binCode);
            this.DoSearch();
            ShowSuccessMessage("MasterData.Location.DeleteBin.Successfully", binCode);
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.Location.DeleteBin.Fail", binCode);
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.divList.Visible = false;
        this.ucNew.Visible = true;
        this.ucNew.InitPageParameter(this.tbLocationCode.Text, this.AreaCode);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.AreaCode = this.tbAreaCode.Text.Trim();
        this.DoSearch();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}
