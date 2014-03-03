using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;

public partial class Reports_CSLoc_List : ListModuleBase
{
    public event EventHandler DetailEvent;

    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    public void Export()
    {
        this.ExportXLS(GV_List);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                this.GV_List.Columns[1].HeaderText = "${Reports.CSLoc.PartyFrom}";
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                this.GV_List.Columns[1].HeaderText = "${Reports.CSLoc.PartyTo}";
            }
        }
    }

    protected void lbtnDetail_Click(object sender, EventArgs e)
    {
        DetailEvent(((LinkButton)sender).CommandArgument, e);
    }
}
