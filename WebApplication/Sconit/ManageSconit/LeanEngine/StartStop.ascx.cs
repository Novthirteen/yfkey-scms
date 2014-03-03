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
using com.Sconit.Service.Procurement;
using com.Sconit.Web;
using com.Sconit.Entity.Batch;
using com.Sconit.Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Entity.Exception;

public partial class ManageSconit_LeanEngine_StartStop : ModuleBase
{
    public event EventHandler StartEvent;
    public event EventHandler StopEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BatchTrigger batchTrigger = TheBatchTriggerMgr.LoadLeanEngineTrigger();
            if (batchTrigger != null)
            {
                bool isPause = (batchTrigger.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_PAUSE);
                if (!isPause)
                    StartEvent(sender, null);
                this.InitialUI(!isPause);
            }
        }
    }

    protected void ibStart_Click(object sender, ImageClickEventArgs e)
    {
        //TheLeanEngineMgr.OrderGenerate();
        this.StartStopService(true);
        StartEvent(sender, null);
    }

    protected void ibStop_Click(object sender, ImageClickEventArgs e)
    {
        this.StartStopService(false);
        StopEvent(sender, null);
    }

    private void StartStopService(bool enable)
    {
        try
        {
            BatchTrigger batchTrigger = TheBatchTriggerMgr.LoadLeanEngineTrigger();
            if (batchTrigger != null)
            {
                batchTrigger.Status = enable ? BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS : BusinessConstants.CODE_MASTER_STATUS_VALUE_PAUSE;
                TheBatchTriggerMgr.UpdateBatchTrigger(batchTrigger);
                ShowSuccessMessage("MasterData.Jobs.Trigger.StopSuccessfully", batchTrigger.BatchJobDetail.Name);
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    private void InitialUI(bool enable)
    {
        if (enable)
        {
            this.lbStatus.Text = "${LeanEngine.Status.Running}";
            this.ibStart.Visible = false;
            this.ibStop.Visible = true;
        }
        else
        {
            this.lbStatus.Text = "${LeanEngine.Status.NotRun}";
            this.ibStart.Visible = true;
            this.ibStop.Visible = false;
        }
    }
}
