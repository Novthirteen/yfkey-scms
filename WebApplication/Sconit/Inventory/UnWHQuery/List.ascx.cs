using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;

public partial class Inventory_UnWHQuery_List : ListModuleBase
{
   
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
            
        }
    }
}
