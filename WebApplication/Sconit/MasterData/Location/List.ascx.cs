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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;

public partial class MasterData_Location_List : ListModuleBase
{
    public EventHandler EditEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string code = ((LinkButton)sender).CommandArgument;
            EditEvent(code, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string code = ((LinkButton)sender).CommandArgument;
        try
        {
            TheLocationMgr.DeleteLocation(code);
            ShowSuccessMessage("MasterData.Location.DeleteLocation.Successfully", code);
            UpdateView();
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.Location.DeleteLocation.Fail", code);
        }

    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((String)(DataBinder.Eval(e.Row.DataItem, "Code")) == BusinessConstants.SYSTEM_LOCATION_INSPECT
                || (String)(DataBinder.Eval(e.Row.DataItem, "Code")) == BusinessConstants.SYSTEM_LOCATION_REJECT)
            {
                LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                lbtnDelete.Visible = false;

                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                lbtnEdit.Visible = false;
            }
        }
    }
}
