using System;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Web;

public partial class Distribution_PickList_List : ListModuleBase
{
    public EventHandler EditEvent;
    public EventHandler ShipEvent;
    public int ListType
    {
        get { return (int)ViewState["ListType"]; }
        set { ViewState["ListType"] = value; }
    }
    public bool IsExport
    {
        get { return (bool)ViewState["IsExport"]; }
        set { ViewState["IsExport"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void UpdateView()
    {
        if (!IsExport)
        {
            if (ListType == 0)
            {
                this.GV_List.Execute();
                this.GV_List.Visible = true;
                this.gp.Visible = true;
                this.GV_Detail.Visible = false;
                this.gp_Detail.Visible = false;
                this.GV_Result.Visible = false;
                this.gp_Result.Visible = false;
            }
            else if (ListType == 1)
            {
                this.GV_Detail.Execute();
                this.GV_List.Visible = false;
                this.GV_Detail.Visible = true;
                this.gp.Visible = false;
                this.gp_Detail.Visible = true;
                this.GV_Result.Visible = false;
                this.gp_Result.Visible = false;
            }
            else if (ListType == 2)
            {
                this.GV_Result.Execute();
                this.GV_Result.Visible = true;
                this.gp_Result.Visible = true;
                this.GV_List.Visible = false;
                this.gp.Visible = false;
                this.GV_Detail.Visible = false;
                this.gp_Detail.Visible = false;
            }
        }
        else
        {
            Export();
        }
    }

    private void Export()
    {
        string dateTime = DateTime.Now.ToString("ddhhmmss");

        if (ListType == 0)
        {
            if (GV_List.FindPager().RecordCount > 0)
            {
                GV_List.Columns.RemoveAt(GV_List.Columns.Count - 1);
            }
            this.ExportXLS(GV_List, "PickListGroup" + dateTime + ".xls");
        }
        else if (ListType == 1)
        {
            this.ExportXLS(GV_Detail, "PickListDetail" + dateTime + ".xls");
        }
        else if (ListType == 2)
        {
            this.ExportXLS(GV_Result, "PickListResult" + dateTime + ".xls");
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PickList pickList = (PickList)e.Row.DataItem;
            if (pickList.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                ((LinkButton)e.Row.FindControl("lbtnDelete")).Visible = true;
            }
        }
    }

    protected void GV_Detail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (IsExport && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        }
    }

    protected void GV_Result_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (IsExport && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[10].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        }
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string pickListNo = ((LinkButton)sender).CommandArgument;
            EditEvent(pickListNo, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string pickListNo = ((LinkButton)sender).CommandArgument;
        try
        {
            ThePickListMgr.DeletePickList(pickListNo, this.CurrentUser);
            ShowSuccessMessage("MasterData.PickList.DeletePickList.Successfully", pickListNo);
            UpdateView();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }
}
