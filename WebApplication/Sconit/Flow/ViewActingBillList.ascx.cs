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
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;

public partial class MasterData_Flow_ViewActingBillList : ListModuleBase
{
    public EventHandler EditEvent;

    public string OrderType
    {
        get
        {
            return (string)ViewState["OrderType"];
        }
        set
        {
            ViewState["OrderType"] = value;
        }
    }

    public string FlowCode
    {
        get
        {
            return (string)ViewState["FlowCode"];
        }
        set
        {
            ViewState["FlowCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    public override void UpdateView()
    {
        
        List<PriceListDetail> priceListDetailList = new List<PriceListDetail>();
        PriceList priceList = null;
        Flow flow = TheFlowMgr.LoadFlow(this.FlowCode, true);
        if (this.OrderType == BusinessConstants.BILL_TRANS_TYPE_PO)
        {
            priceList = flow.PriceListFrom;
        }
        else if (this.OrderType == BusinessConstants.BILL_TRANS_TYPE_SO)
        {
            priceList = flow.PriceListTo;
        }
        if (priceList != null)
        {
            if (flow.FlowDetails != null && flow.FlowDetails.Count > 0)
            {
                foreach (FlowDetail flowDetail in flow.FlowDetails)
                {
                    PriceListDetail priceListDetail = ThePriceListDetailMgr.GetLastestPriceListDetail(priceList, flowDetail.Item, DateTime.Now, flow.Currency);
                    if (priceListDetail != null)
                    {
                        priceListDetailList.Add(priceListDetail);
                    }
                }
            }
        }
        if (priceListDetailList.Count > 0)
        {
            this.GV_List.DataSource = priceListDetailList;
            this.GV_List.DataBind();
        }
        else
        {
            this.Parent.Visible = false;
        }
    }




}
