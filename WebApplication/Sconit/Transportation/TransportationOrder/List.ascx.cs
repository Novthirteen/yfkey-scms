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
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.Transportation;

public partial class Transportation_TransportationOrder_List : ListModuleBase
{
    public EventHandler EditEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TransportationOrder order = (TransportationOrder)e.Row.DataItem;
            ((Label)(e.Row.FindControl("lblShipFrom"))).Text = order.TransportationRoute!= null? order.TransportationRoute.ShipFrom.FullAddress:string.Empty;
            ((Label)(e.Row.FindControl("lblShipTo"))).Text = order.TransportationRoute!= null? order.TransportationRoute.ShipTo.FullAddress:string.Empty;
        }
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string id = ((LinkButton)sender).CommandArgument;
            EditEvent(id, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string orderNo = ((LinkButton)sender).CommandArgument;
        try
        {

            TheTransportationOrderMgr.DeleteTransportationOrder(orderNo);
            ShowSuccessMessage("Transportation.TransportationOrder.DeleteTransportationOrder.Successfully");
            UpdateView();
        }
        catch
        {
            ShowErrorMessage("Transportation.TransportationOrder.DeleteTransportationOrder.Fail");
        }

    }
}
