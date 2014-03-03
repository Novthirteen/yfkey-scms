using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;

public partial class OfflineTerminal_Log_Offline_View : EditModuleBase
{
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {


    }

    public void InitPageParameter(string Id)
    {
        IList<ClientOrderDetail> clientOrderDetailList = TheClientOrderDetailMgr.GetAllClientOrderDetail(Id);
        this.GV_OfflineLog.DataSource = clientOrderDetailList;
        this.GV_OfflineLog.DataBind();

        IList<ClientWorkingHours> clientWorkingHoursList = TheClientWorkingHoursMgr.GetAllClientWorkingHours(Id);
        if (clientWorkingHoursList != null && clientWorkingHoursList.Count > 0)
        {
            fieldsetWorkingHours.Visible = true;
            this.GV_WorkingHours.DataSource = clientWorkingHoursList;
            this.GV_WorkingHours.DataBind();
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }


}
