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
using com.Sconit.Service.Procurement;
using com.Sconit.Entity.MasterData;

public partial class ManageSconit_LeanEngine_List : ListModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }


    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

        }
    }

    protected void lbtnView_Click(object sender, EventArgs e)
    {
        string flowCode = ((LinkButton)sender).CommandArgument;
        OrderHead order = TheLeanEngineMgr.PreviewGenOrder(flowCode);
        if (order != null)
        {
            this.ucView.Visible = true;
            this.ucView.InitPageParameter(order);
        }
    }

}
