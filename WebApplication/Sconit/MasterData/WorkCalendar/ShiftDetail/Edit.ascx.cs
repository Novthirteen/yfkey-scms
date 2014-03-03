using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;

public partial class MasterData_WorkCalendar_ShiftDetail_Edit : EditModuleBase
{
    public event EventHandler EditEvent;
    public event EventHandler BackEvent;

    protected string code
    {
        get { return (string)ViewState["code"]; }
        set { ViewState["code"] = value; }
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
        this.ODS_ShiftDetail.SelectParameters["Id"].DefaultValue = code;
        this.ODS_ShiftDetail.DeleteParameters["Id"].DefaultValue = code;
        this.FV_ShiftDetail.DataBind();
    }

    protected void FV_ShiftDetail_DataBound(object sender, EventArgs e)
    {
        ShiftDetail shiftDetail = (ShiftDetail)((FormView)sender).DataItem;
        ((TextBox)(this.FV_ShiftDetail.FindControl("tbCode"))).Text = shiftDetail.Shift.Code;
        ((TextBox)(this.FV_ShiftDetail.FindControl("tbShiftName"))).Text = shiftDetail.Shift.ShiftName;
        ((TextBox)(this.FV_ShiftDetail.FindControl("tbMemo"))).Text = shiftDetail.Shift.Memo;
        ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_ShiftDetail.FindControl("ddlDaysAdd"))).SelectedValue = shiftDetail.DaysAdd;
    }

    protected void ODS_ShiftDetail_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        ShiftDetail shiftDetail = (ShiftDetail)e.InputParameters[0];
        string shiftCode = ((TextBox)(this.FV_ShiftDetail.FindControl("tbCode"))).Text;
        shiftDetail.Shift = TheShiftMgr.LoadShift(shiftCode);
        if (((TextBox)(this.FV_ShiftDetail.FindControl("tbStartDate"))).Text.Trim() == string.Empty)
        {
            shiftDetail.StartDate = null;
        }
        if (((TextBox)(this.FV_ShiftDetail.FindControl("tbEndDate"))).Text.Trim() == string.Empty)
        {
            shiftDetail.EndDate = null;
        }
        com.Sconit.Control.CodeMstrDropDownList ddlDaysAdd = ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_ShiftDetail.FindControl("ddlDaysAdd")));
        shiftDetail.DaysAdd = ddlDaysAdd.SelectedValue;
    }

    protected void ODS_ShiftDetail_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (EditEvent != null)
        {
            EditEvent(this, null);
        }
        ShowSuccessMessage("MasterData.WorkCalendar.Update.Successfully");
    }

    protected void ODS_ShiftDetail_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.WorkCalendar.Delete.Successfully");
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.WorkCalendar.Delete.Failed");
            e.ExceptionHandled = true;
        }
    }
}
