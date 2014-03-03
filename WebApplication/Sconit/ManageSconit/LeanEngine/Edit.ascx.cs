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
using com.Sconit.Entity.Batch;

public partial class ManageSconit_LeanEngine_Edit : EditModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DataBind();
    }

    private void DataBind()
    {
        BatchTrigger batchTrigger = TheBatchTriggerMgr.LoadLeanEngineTrigger();
        if (batchTrigger != null)
        {
            this.tbPrevFireTime.Text = batchTrigger.PreviousFireTime.HasValue ? batchTrigger.PreviousFireTime.Value.ToString("yyyy-MM-dd HH:mm") : string.Empty;
            this.tbNextFireTime.Text = batchTrigger.NextFireTime.HasValue ? batchTrigger.NextFireTime.Value.ToString("yyyy-MM-dd HH:mm") : string.Empty;
            this.tbInterval.Text = batchTrigger.Interval.ToString();
            this.lblIntervalType.Value = batchTrigger.IntervalType;
        }
    }
}
