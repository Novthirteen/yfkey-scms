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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using System.Text;

public partial class Visualization__FlowDetailTrack_List : ListModuleBase
{
    public EventHandler ViewEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lbtnView_Click(object sender, EventArgs e)
    {
        if (ViewEvent != null)
        {
            string id = ((LinkButton)sender).CommandArgument;
            ViewEvent(id, e);
        }
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

}
