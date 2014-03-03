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
using NHibernate.Expression;
using com.Sconit.Entity;

public partial class MasterData_Uom_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbUomConvClickEvent += new System.EventHandler(this.TabUomConvClick_Render);
        this.ucTabNavigator.lbUomClickEvent += new System.EventHandler(this.TabUomClick_Render);
        if (!IsPostBack)
        {
            if (this.Action == BusinessConstants.PAGE_LIST_ACTION)
            {
                this.ucUom.QuickSearch();
            }
        }
    }

    private void TabUomConvClick_Render(object sender, EventArgs e)
    {
        this.ucUomConv.Visible = true;
        this.ucUom.Visible = false;
    }

    private void TabUomClick_Render(object sender, EventArgs e)
    {
        this.ucUomConv.Visible = false;
        this.ucUom.Visible = true;    
    }

}
