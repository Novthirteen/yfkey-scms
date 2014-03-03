using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;

public partial class MRP_ShiftPlan_Manual_Shift : ModuleBase
{
    public DateTime Date { get; set; }
    public string RegionCode { get; set; }

    public string ShiftCode
    {
        get { return this.ddlShift.SelectedValue; }
        set { this.ddlShift.SelectedValue = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindList(this.Date, this.RegionCode);
        }
    }

    public void BindList(DateTime date, string regionCode)
    {
        IList<Shift> shiftList = new List<Shift>();
        shiftList = TheWorkCalendarMgr.GetShiftByDate(date, regionCode, null);
        if (shiftList == null || shiftList.Count == 0)
            shiftList.Add(new Shift());

        this.ddlShift.DataSource = shiftList;
        this.ddlShift.DataBind();
    }
}
