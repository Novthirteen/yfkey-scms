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
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;

public partial class MasterData_WorkCalendar_Shift_New : NewModuleBase
{
    private Shift shift;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    public void PageCleanup()
    {
        ((TextBox)(this.FV_Shift.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_Shift.FindControl("tbShiftName"))).Text = string.Empty;
        ((TextBox)(this.FV_Shift.FindControl("tbShiftTime"))).Text = string.Empty;
        ((TextBox)(this.FV_Shift.FindControl("tbMemo"))).Text = string.Empty;
        ((TextBox)(this.FV_Shift.FindControl("tbStartDate"))).Text = string.Empty;
        ((TextBox)(this.FV_Shift.FindControl("tbEndDate"))).Text = string.Empty;
    }

    protected void ODS_Shift_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        shift = (Shift)e.InputParameters[0];
        Shift checkShift = TheShiftMgr.LoadShift(shift.Code);
        if (checkShift != null)
        {
            ShowErrorMessage("Common.Business.Error.EntityExist", shift.Code);
            e.Cancel = true;
        }
    }

    protected void ODS_Shift_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(shift.Code, e);
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
}
