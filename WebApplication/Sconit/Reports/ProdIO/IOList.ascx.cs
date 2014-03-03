using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.View;

public partial class Reports_ProdIO_IOList : ModuleBase
{
    public List<OrderLocTransView> DataSource
    {
        set
        {
            this.GV_List.DataSource = value;
            this.GV_List.DataBind();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
}
