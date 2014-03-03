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
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Transportation;
using com.Sconit.Persistence;
using Microsoft.ApplicationBlocks.Data;
public partial class Transportation_TransportationOrder_InProcessLocationList : ModuleBase
{

    public EventHandler EditEvent;





    protected void Page_Load(object sender, EventArgs e)
    {

       
        
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string ipNo = ((LinkButton)sender).CommandArgument;
            EditEvent(ipNo, e);
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    public void InitPageParameter(string route)
    {
        IList<InProcessLocation> avaliableInPorcessLocationList = TheInProcessLocationMgr.GetAvaliableInProcessLocation(route);
        
        this.GV_List.DataSource = avaliableInPorcessLocationList;
        this.GV_List.DataBind();
    }

    public void InitPageParameter(string orderNo, bool isEdit)
    {
        if (isEdit)
        {
            IList<InProcessLocation> ipList = new List<InProcessLocation>();
            TransportationOrder order = TheTransportationOrderMgr.LoadTransportationOrder(orderNo, true);
            if(order.OrderDetails!= null && order.OrderDetails.Count>0)
            {
                foreach(TransportationOrderDetail orderDetail in order.OrderDetails)
                {
                    ipList.Add(orderDetail.InProcessLocation);
                }
            }
            this.GV_List.Columns[0].Visible = false;
            this.GV_List.DataSource = ipList;
            this.GV_List.DataBind();
            
        }
    }

    public List<InProcessLocation> PopulateInProcessLocationList()
    {
        List<InProcessLocation> ipList = new List<InProcessLocation>();
        if (this.GV_List.Rows != null && this.GV_List.Rows.Count > 0)
        {
            foreach (GridViewRow row in this.GV_List.Rows)
            {
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    Label tbIpNo = row.FindControl("tbIpNo") as Label;

                    InProcessLocation ip = TheInProcessLocationMgr.LoadInProcessLocation(tbIpNo.Text.Trim());
                    ipList.Add(ip);
                }
            }
         
        }
        return ipList;
    }

}
