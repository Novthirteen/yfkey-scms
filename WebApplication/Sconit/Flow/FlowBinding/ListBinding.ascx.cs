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
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using NHibernate.Expression;

public partial class MasterData_FlowBinding_ListBinding : ListModuleBase
{
    
    public event EventHandler ListEditEvent;

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }   

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (ListEditEvent != null)
        {
            int id = int.Parse(((LinkButton)sender).CommandArgument);
            ListEditEvent(id, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(((LinkButton)sender).CommandArgument);
        TheFlowBindingMgr.DeleteFlowBinding(id);
        ShowSuccessMessage("MasterData.Flow.Binding.DeleteBinding.Successfully", id.ToString());
        UpdateView();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }
}
