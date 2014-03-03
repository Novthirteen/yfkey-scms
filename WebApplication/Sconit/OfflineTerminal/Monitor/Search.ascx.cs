using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;

public partial class OfflineTerminal_Monitor_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {


    }

    protected override void DoSearch()
    { }
}
