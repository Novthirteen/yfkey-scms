using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;

public partial class Finance_Bill_List : ListModuleBase
{
    public EventHandler ViewEvent;

    public bool IsExport
    {
        get { return ViewState["IsExport"] == null ? false : (bool)ViewState["IsExport"]; }
        set { ViewState["IsExport"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }

    public override void UpdateView()
    {
        if (this.IsExport)
        {
            string dateTime = DateTime.Now.ToString("ddhhmmss");
            if (this.IsExport)
            {
                this.GV_List.Columns[6].Visible = false;
            }
            this.ExportXLS(this.GV_List, "KPOrder" + dateTime + ".xls");
        }
        else
        {
            this.GV_List.Execute();
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          
        }
    }

  
    protected void lbtnView_Click(object sender, EventArgs e)
    {
        if (ViewEvent != null)
        {
            decimal orderId = decimal.Parse(((LinkButton)sender).CommandArgument);
            ViewEvent(orderId, null);
        }
    }

   
}
