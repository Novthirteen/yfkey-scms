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
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Service.MasterData;

public partial class Warehouse_CheckASN_List : ListModuleBase
{
    public EventHandler ViewEvent;
    public EventHandler EditEvent;
    public EventHandler CloseEvent;

 

    public override void UpdateView()
    {


        this.GV_List.Execute();
        this.GV_List.Visible = true;
        this.gp.Visible = true;


    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lbtnView_Click(object sender, EventArgs e)
    {
       
            string ipNo = ((LinkButton)sender).CommandArgument;
            if (ipNo != string.Empty)
            {
                InProcessLocation ip = TheInProcessLocationMgr.LoadInProcessLocation(ipNo);
                if (ip.CurrentActivity == null)
                {
                    ip.CurrentActivity = CurrentUser.Code + "|" + DateTime.Now.ToString("yyMMddhhmmss");
                    TheInProcessLocationMgr.UpdateInProcessLocation(ip);
                    ShowSuccessMessage("InProcessLocation.Confirme.Successfully", ipNo);
                    this.GV_List.Execute();
                }
                else
                {
                    ShowWarningMessage("InProcessLocation.Confirme.Warning", ipNo);
                }
            }
         
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string ipNo = ((LinkButton)sender).CommandArgument;
            EditEvent(ipNo, e);
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                InProcessLocation ip = (InProcessLocation)e.Row.DataItem;
                e.Row.FindControl("lbtnView").Visible = ip.CurrentActivity == null ? true : false;
                e.Row.FindControl("ShowConFirm").Visible = ip.CurrentActivity == null ? false : true;
            }
         
    }

    
}
