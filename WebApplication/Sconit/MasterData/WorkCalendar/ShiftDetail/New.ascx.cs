using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;

public partial class MasterData_WorkCalendar_ShiftDetail_New : NewModuleBase
{
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    public void InitPageParameter(string shiftCode)
    {
        Shift shift = TheShiftMgr.LoadShift(shiftCode);
        ((TextBox)(this.FV_ShiftDetail.FindControl("tbCode"))).Text = shift.Code;
        ((TextBox)(this.FV_ShiftDetail.FindControl("tbShiftName"))).Text = shift.ShiftName;
        ((TextBox)(this.FV_ShiftDetail.FindControl("tbMemo"))).Text = shift.Memo;
        
    }

    protected void ODS_ShiftDetail_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        ShiftDetail shiftDetail = (ShiftDetail)e.InputParameters[0];
        string shiftCode = ((TextBox)(this.FV_ShiftDetail.FindControl("tbCode"))).Text;
        shiftDetail.Shift = TheShiftMgr.LoadShift(shiftCode);
        if (shiftDetail.Shift == null)
        {
            ShowErrorMessage("MasterData.WorkCalendar.ErrorMessage.ShiftNotExist");
            e.Cancel = true;
        }
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

    protected void ODS_ShiftDetail_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(this, e);
            this.PageCleanup();
            ShowSuccessMessage("MasterData.WorkCalendar.Insert.Successfully");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    private void PageCleanup()
    {
        ((TextBox)(this.FV_ShiftDetail.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_ShiftDetail.FindControl("tbShiftName"))).Text = string.Empty;
        ((TextBox)(this.FV_ShiftDetail.FindControl("tbShiftTime"))).Text = string.Empty;
        ((TextBox)(this.FV_ShiftDetail.FindControl("tbMemo"))).Text = string.Empty;
        ((TextBox)(this.FV_ShiftDetail.FindControl("tbStartDate"))).Text = string.Empty;
        ((TextBox)(this.FV_ShiftDetail.FindControl("tbEndDate"))).Text = string.Empty;
        ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_ShiftDetail.FindControl("ddlDaysAdd"))).DataBind();
    }
}
