using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;

public partial class Finance_Bill_NewTabNavigator : ModuleBase
{
    public event EventHandler lbSingleClickEvent;
    public event EventHandler lbBatchClickEvent;
    public event EventHandler lbRecalculateClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lbSingle_Click(object sender, EventArgs e)
    {
        if (lbSingleClickEvent != null)
        {
            lbSingleClickEvent(this, e);

            this.tab_single.Attributes["class"] = "ajax__tab_active";
            this.tab_batch.Attributes["class"] = "ajax__tab_inactive";
            this.tab_recalculate.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbBatch_Click(object sender, EventArgs e)
    {
        if (lbBatchClickEvent != null)
        {
            lbBatchClickEvent(this, e);

            this.tab_single.Attributes["class"] = "ajax__tab_inactive";
            this.tab_batch.Attributes["class"] = "ajax__tab_active";
            this.tab_recalculate.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbRecalculate_Click(object sender, EventArgs e)
    {
        if (lbRecalculateClickEvent != null)
        {
            lbRecalculateClickEvent(this, e);

            this.tab_single.Attributes["class"] = "ajax__tab_inactive";
            this.tab_batch.Attributes["class"] = "ajax__tab_inactive";
            this.tab_recalculate.Attributes["class"] = "ajax__tab_active";
        }
    }
}
