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
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Transportation;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;

public partial class Transportation_ValuateOrder_List : ListModuleBase
{
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
            ((Label)(e.Row.FindControl("lblShipFrom"))).Text = order.TransportationRoute != null ? order.TransportationRoute.ShipFrom.FullAddress : string.Empty;
            ((Label)(e.Row.FindControl("lblShipTo"))).Text = order.TransportationRoute != null ? order.TransportationRoute.ShipTo.FullAddress : string.Empty;
        }
    }

    public IList<TransportationOrder> PopulateSelectedData()
    {
        if (this.GV_List.Rows != null && this.GV_List.Rows.Count > 0)
        {
            IList<TransportationOrder> transportationOrderList = new List<TransportationOrder>();
            foreach (GridViewRow row in this.GV_List.Rows)
            {
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    HiddenField hfOrderNo = row.FindControl("hfOrderNo") as HiddenField;

                    TransportationOrder transportationOrder = new TransportationOrder();
                    transportationOrder.OrderNo = hfOrderNo.Value;

                    transportationOrderList.Add(transportationOrder);
                }
            }
            return transportationOrderList;
        }

        return null;
    }
}
