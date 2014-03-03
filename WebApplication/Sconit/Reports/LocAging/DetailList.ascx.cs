using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using com.Sconit.Utility;
using NHibernate.Transform;

public partial class Reports_LocAging_DetailList : ListModuleBase
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
        this.Visible = false;
    }
}
