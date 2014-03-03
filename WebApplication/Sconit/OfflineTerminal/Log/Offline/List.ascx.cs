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

public partial class MasterData_Client_Log_Offline_List : ListModuleBase
{
    public EventHandler ViewEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void UpdateView()
    {
        this.GV_List.Execute();

    }

    protected void lbtnView_Click(object sender, EventArgs e)
    {
        if (ViewEvent != null)
        {
            string code = ((LinkButton)sender).CommandArgument;
            ViewEvent(code, e);
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((String)(DataBinder.Eval(e.Row.DataItem, "OrderType")) == BusinessConstants.TRANSFORMER_MODULE_TYPE_OFFLINE)
            {
                LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                lbtnDelete.Visible = false;

                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                lbtnEdit.Visible = false;
            }
        }
    }
}
