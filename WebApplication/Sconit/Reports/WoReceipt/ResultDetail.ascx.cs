using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using System.Collections;
using com.Sconit.Entity.Exception;

public partial class MasterData_Reports_WoReceipt_ResultDetail : ListModuleBase
{

    public override void UpdateView()
    {
      
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    public void UpdateView(IList<WoReceiptView> woReceiptList)
    {
        this.GV_List.DataSource = woReceiptList;
        this.GV_List.DataBind();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

   
}