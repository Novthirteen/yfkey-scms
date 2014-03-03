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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Entity.Transportation;
using com.Sconit.Service.Transportation;
using com.Sconit.Entity.Exception;
using System.Collections.Generic;
using com.Sconit.Entity.Distribution;

public partial class Transportation_TransportationOrder_New : NewModuleBase
{
    private TransportationOrder transportationOrder;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void PageCleanup()
    {
        this.tbRoute.Text = string.Empty;
        this.ucList.Visible = false;
        this.btnConfirm.Visible = false;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            IList<InProcessLocation> ipList = this.ucList.PopulateInProcessLocationList();
            if (ipList.Count == 0)
            {
                ShowErrorMessage("TransportationOrder.Error.DetailEmpty");
                return;
            }
            TransportationOrder order = TheTransportationOrderMgr.CreateTransportationOrder(this.tbRoute.Text.Trim(), ipList, this.CurrentUser);
            if (CreateEvent != null)
            {
                CreateEvent(order.OrderNo, e);
            }
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }


    }

    protected void tbRoute_TextChanged(Object sender, EventArgs e)
    {
        try
        {
            string route = this.tbRoute.Text.Trim();
            if (route != null && route != string.Empty)
            {
                this.ucList.InitPageParameter(route);
                this.ucList.Visible = true;
                this.btnConfirm.Visible = true;
            }
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }



}
