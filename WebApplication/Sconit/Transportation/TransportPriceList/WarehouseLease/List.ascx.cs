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

public partial class Transportation_TransportPriceList_WarehouseLease_List : ListModuleBase
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
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string Id = ((LinkButton)sender).CommandArgument;
            EditEvent(Id, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string Id = ((LinkButton)sender).CommandArgument;
        try
        {
            TheTransportPriceListDetailMgr.DeleteTransportPriceListDetail(Convert.ToInt32(Id));
            ShowSuccessMessage("Transportation.TransportPriceListDetail.DeleteWarehouseLease.Successfully"); 
            UpdateView();
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("Transportation.TransportPriceListDetail.DeleteWarehouseLease.Fail"); 
        }
       
    }
}
