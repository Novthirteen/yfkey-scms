using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;

public partial class MasterData_Location_StorageArea_Main : MainModuleBase
{
    public event EventHandler EditEvent;
    public event EventHandler BackEvent;

    protected string LocationCode
    {
        get { return (string)ViewState["LocationCode"]; }
        set { ViewState["LocationCode"] = value; }
    }

    public void InitPageParameter(string code)
    {
        this.LocationCode = code;
        this.tbLocationCode.Text = code;
        this.DoSearch();
        this.divAreaList.Visible = true;
        this.ucEdit.Visible = false;
        this.ucNew.Visible = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucNew.CreateEvent += new System.EventHandler(this.CreateBack_Render);
        this.ucEdit.BackEvent += new System.EventHandler(this.EditBack_Render);
    }

    private void CreateBack_Render(object sender, EventArgs e)
    {
        this.divAreaList.Visible = false;
        this.ucEdit.InitPageParameter((StorageArea)sender);
        this.ucEdit.Visible = true;
    }

    private void EditBack_Render(object sender, EventArgs e)
    {
        this.divAreaList.Visible = true;
        DoSearch();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        this.ucNew.Visible = true;
        this.ucNew.InitPageParameter(this.LocationCode);
    }

    private void DoSearch()
    {
        IList<StorageArea> storageAreaList = TheStorageAreaMgr.GetStorageArea(this.LocationCode, this.tbAreaCode.Text.Trim());

        this.GV_List.DataSource = storageAreaList;
        this.GV_List.DataBind();
        //if (storageAreaList.Count > 0)
        //{
        //    this.fldsgv.Visible = true;
        //}
        //else
        //{
        //    this.fldsgv.Visible = false;
        //}
        this.fldsgv.Visible = true;
        if (storageAreaList == null || storageAreaList.Count == 0)
        {
            this.ltlMessage.Visible = true;
        }
        else
        {
            this.ltlMessage.Visible = false;
        }
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        string code = ((LinkButton)sender).CommandArgument;
        StorageArea storageArea = TheStorageAreaMgr.LoadStorageArea(code);
        this.ucEdit.InitPageParameter(storageArea);
        this.ucEdit.Visible = true;
        this.divAreaList.Visible = false;
        this.EditEvent((object)code, e);
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string code = ((LinkButton)sender).CommandArgument;
        try
        {
            TheStorageAreaMgr.DeleteStorageArea(code);
            ShowSuccessMessage("MasterData.Location.DeleteArea.Successfully", code);
            DoSearch();
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.Location.DeleteArea.Fail", code);
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Literal ltlCode = (Literal)e.Row.FindControl("ltlCode");
        //    MasterData_Location_StorageBin_Main GV_Bin = (MasterData_Location_StorageBin_Main)e.Row.FindControl("ucBin");

        //    GV_Bin.InitPageParameter(ltlCode.Text);
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
