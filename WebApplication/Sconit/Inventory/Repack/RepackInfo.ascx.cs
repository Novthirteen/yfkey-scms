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
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class Inventory_Repack_RepackInfo : ModuleBase
{
    public event EventHandler BackEvent;

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string repackNo = this.lbRepackNo.Text;
        IList<object> list = new List<object>();
        Repack repack = TheRepackMgr.LoadRepack(repackNo, true);
        list.Add(repack);
        string printUrl = TheReportMgr.WriteToFile("Repack.xls", list);
        Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + printUrl + "'); </script>");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(sender, e);
        }
    }

    public void InitPageParameter(string repackNo)
    {
        Repack repack = TheRepackMgr.LoadRepack(repackNo);
        this.lbRepackNo.Text = repack.RepackNo;
        this.lbCreateUser.Text = repack.CreateUser.Name;
        this.lbCreateDate.Text = repack.CreateDate.ToString("yyyy-MM-dd");
        this.lblRepackNo.Text = RepackHelper.GetRepackLabel(this.RepackType, true);
       
    }

   
}
