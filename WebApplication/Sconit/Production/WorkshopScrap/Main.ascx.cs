using System;
using System.Collections;
using System.Collections.Generic;
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
using com.Sconit.Entity;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Distribution;
using com.Sconit.Service.Distribution;

public partial class Production_Feed_Main : MainModuleBase
{

    public Production_Feed_Main()
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucNew.CreateEvent += new System.EventHandler(this.CreateBack_Render);
        this.ucInspectOrderInfo.BackEvent += new System.EventHandler(this.Back_Render);
     
    }


    void CreateBack_Render(object sender, EventArgs e)
    {
        this.ucInspectOrderInfo.InitPageParameter((string)sender,true);
        this.ucInspectOrderInfo.Visible = true;
        this.ucNew.Visible = false;
       
    }

    void Back_Render(object sender, EventArgs e)
    {
        this.ucInspectOrderInfo.Visible = false;
        this.ucNew.Visible = true;
        this.ucNew.UpdateView();
    }
  
}
