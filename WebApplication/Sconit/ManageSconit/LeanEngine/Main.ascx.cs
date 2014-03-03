using System;
using com.Sconit.Web;
using System.ServiceProcess;

public partial class ManageSconit_LeanEngine_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucStartStop.StartEvent += new EventHandler(ucStartStop_StartEvent);
        this.ucStartStop.StopEvent += new EventHandler(ucStartStop_StopEvent);
    }

    //The event handler when user click button "Start"
    void ucStartStop_StartEvent(object sender, EventArgs e)
    {
        this.ucEditMain.Visible = true;
    }

    //The event handler when user click button "Stop"
    void ucStartStop_StopEvent(object sender, EventArgs e)
    {
        this.ucEditMain.Visible = false;
    }

}
