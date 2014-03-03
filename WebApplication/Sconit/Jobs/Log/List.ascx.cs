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
using com.Sconit.Entity.Batch;
using com.Sconit.Entity;


public partial class Jobs_Log_List : ListModuleBase
{
   

    protected void Page_Load(object sender, EventArgs e)
    {


    }
    public override void UpdateView()
    {
        this.GV_List.Execute();
        
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        this.Parent.Visible = false;
    }
    
}
