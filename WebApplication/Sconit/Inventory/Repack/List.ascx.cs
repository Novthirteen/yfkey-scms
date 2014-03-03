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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.Distribution;

public partial class Inventory_Repack_List : ListModuleBase
{
    public string RepackType
    {
        get
        {
            return (string)ViewState["RepackType"];
        }
        set
        {
            ViewState["RepackType"] = value;
        }
    }
    public event EventHandler ViewEvent;
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public override void UpdateView()
    {
        this.GV_List.Execute();
        this.GV_List.Columns[1].HeaderText = RepackHelper.GetRepackLabel(this.RepackType);
    }


    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void lbtnView_Click(object sender, EventArgs e)
    {
        if (ViewEvent != null)
        {
            string repackNo = ((LinkButton)sender).CommandArgument;
            ViewEvent(repackNo, e);
        }
    }
}
