using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using System.Drawing;
using System.Text;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Service.Criteria;
using System.Collections;
using com.Sconit.Entity.Distribution;
using com.Sconit.Service.Distribution;
using com.Sconit.Service.Report;

public partial class PrintMonitor : System.Web.UI.Page
{
    protected User CurrentUser
    {
        get
        {
            return (new SessionHelper(Page)).CurrentUser;
        }
    }

    private IReportMgr TheReportMgr
    {
        get
        {
            return ServiceLocator.GetService<IReportMgr>("ReportMgr.service");
        }
    }
    protected ICriteriaMgr TheCriteriaMgr
    {
        get
        {
            return ServiceLocator.GetService<ICriteriaMgr>("CriteriaMgr.service");
        }
    }
    protected IOrderHeadMgr TheOrderHeadMgr
    {
        get
        {
            return ServiceLocator.GetService<IOrderHeadMgr>("OrderHeadMgr.service");
        }
    }
    protected IPickListMgr ThePickListMgr
    {
        get
        {
            return ServiceLocator.GetService<IPickListMgr>("PickListMgr.service");
        }
    }
    protected IInspectOrderMgr TheInspectOrderMgr
    {
        get
        {
            return ServiceLocator.GetService<IInspectOrderMgr>("InspectOrderMgr.service");
        }
    }
    protected IInProcessLocationMgr TheInProcessLocationMgr
    {
        get
        {
            return ServiceLocator.GetService<IInProcessLocationMgr>("InProcessLocationMgr.service");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["Current_User"] == null)
        {
            this.Response.Redirect("~/Logoff.aspx");
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Response.Cookies["autoPrintCookie"].Expires = DateTime.Now.AddDays(7);
        try
        {
            Print();
        }
        catch(Exception)
        { 
             //noth
        }
    }

    private void Print()
    {
        StringBuilder url = new StringBuilder();
        foreach (Permission permission in this.CurrentUser.PagePermission)
        {
            if (permission.Category.Code == BusinessConstants.PERMISSION_PAGE_VALUE_AUTOPRINT)
            {
                if (Request.Cookies["autoPrintCookie"] != null && Request.Cookies["autoPrintCookie"][permission.Code] != null)
                {
                    string status = Request.Cookies["autoPrintCookie"][permission.Code].ToString();
                    if (status.ToLower() == "true")
                    {
                        if (permission.Code == BusinessConstants.PERMISSION_PAGE_AUTOPRINT_VALUE_PAGE_PRODUCTIONORDERPRINT)
                        {
                            AppendPrintUrlOrder(url, true);
                        }
                        if (permission.Code == BusinessConstants.PERMISSION_PAGE_AUTOPRINT_VALUE_PAGE_PROCUREMENTORDERPRINT)
                        {
                            AppendPrintUrlOrder(url, false);
                        }
                        if (permission.Code == BusinessConstants.PERMISSION_PAGE_AUTOPRINT_VALUE_PAGE_PICKLISTPRINT)
                        {
                            AppendPrintUrlPicklist(url);
                        }
                        if (permission.Code == BusinessConstants.PERMISSION_PAGE_AUTOPRINT_VALUE_PAGE_INSPECTIONPRINT)
                        {
                            AppendPrintUrlInspectionOrder(url);
                        }
                        if (permission.Code == BusinessConstants.PERMISSION_PAGE_AUTOPRINT_VALUE_PAGE_ASNPRINT)
                        {
                            AppendPrintUrlASN(url);
                        }
                    }
                }
            }
        }

        if (url.ToString().Trim() != string.Empty)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>Print('" + url.ToString() + "'); </script>");
        }
    }

    private void AppendPrintUrl(StringBuilder url, string newUrl)
    {
        if (url.ToString().Trim() != string.Empty)
        {
            url.Append("|");
        }
        url.Append(newUrl);
    }

    private void AppendPrintUrlOrder(StringBuilder url, bool isWorkOrder)
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderHead));

        criteria.CreateAlias("PartyFrom", "pf");
        criteria.CreateAlias("PartyTo", "pt");

        criteria.Add(Expression.Eq("IsPrinted", false));

        OrderHelper.SetOpenOrderStatusCriteria(criteria, "Status");//订单状态
        SecurityHelper.SetPartySearchCriteria(criteria, "PartyFrom.Code", this.CurrentUser.Code); //区域或者供应商权限
        //SecurityHelper.SetRegionSearchCriteria(criteria, "PartyTo.Code", this.CurrentUser.Code); //区域权限
        if (isWorkOrder)
        {
            criteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
        }
        else
        {
            criteria.Add(Expression.In("Type", new string[] { 
                    BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, 
                    BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER }));
        }

        IList<OrderHead> orderHeadList = TheCriteriaMgr.FindAll<OrderHead>(criteria);
        if (orderHeadList != null && orderHeadList.Count > 0)
        {
            foreach (OrderHead orderHead in orderHeadList)
            {
                if (orderHead.OrderTemplate != null && orderHead.OrderTemplate.Trim() != string.Empty)
                {
                    string newUrl = TheReportMgr.WriteToFile(orderHead.OrderTemplate, orderHead.OrderNo);
                    AppendPrintUrl(url, newUrl);
                    orderHead.IsPrinted = true;//to be refactored
                    TheOrderHeadMgr.UpdateOrderHead(orderHead);
                }
            }
        }
    }

    private void AppendPrintUrlPicklist(StringBuilder url)
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(PickList));

        criteria.Add(Expression.Eq("IsPrinted", false));
        OrderHelper.SetOpenOrderStatusCriteria(criteria, "Status");//订单状态
        SecurityHelper.SetRegionSearchCriteria(criteria, "PartyFrom.Code", this.CurrentUser.Code); //区域权限
        //SecurityHelper.SetRegionSearchCriteria(criteria, "PartyTo.Code", this.CurrentUser.Code); //区域权限

        IList<PickList> pickList = TheCriteriaMgr.FindAll<PickList>(criteria);
        if (pickList != null && pickList.Count > 0)
        {
            foreach (PickList pl in pickList)
            {
                string newUrl = TheReportMgr.WriteToFile("PickList.xls", pl.PickListNo);//to be refactored
                AppendPrintUrl(url, newUrl);
                pl.IsPrinted = true;//to be refactored
                ThePickListMgr.UpdatePickList(pl);
            }
        }
    }

    private void AppendPrintUrlInspectionOrder(StringBuilder url)
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(InspectOrder));

        criteria.Add(Expression.Eq("IsPrinted", false));
        criteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
        //SecurityHelper.SetRegionSearchCriteria(criteria, "PartyFrom.Code", this.CurrentUser.Code); //区域权限
        //SecurityHelper.SetRegionSearchCriteria(criteria, "PartyTo.Code", this.CurrentUser.Code); //区域权限

        IList<InspectOrder> inspectOrderList = TheCriteriaMgr.FindAll<InspectOrder>(criteria);
        if (inspectOrderList != null && inspectOrderList.Count > 0)
        {
            foreach (InspectOrder inspectOrder in inspectOrderList)
            {
                string newUrl = TheReportMgr.WriteToFile("InspectOrder.xls", inspectOrder.InspectNo);//to be refactored
                AppendPrintUrl(url, newUrl);
                inspectOrder.IsPrinted = true;//to be refactored
                TheInspectOrderMgr.UpdateInspectOrder(inspectOrder);
            }
        }
    }


    private void AppendPrintUrlASN(StringBuilder url)
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(InProcessLocation));

        criteria.Add(Expression.Eq("IsPrinted", false));
        criteria.Add(Expression.Eq("NeedPrintAsn", true));
        criteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
        criteria.Add(Expression.Eq("AsnTemplate", "ASN.xls"));
        criteria.Add(Expression.Or(Expression.Eq("OrderType", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION),
                                   Expression.Eq("OrderType", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)));
        SecurityHelper.SetRegionSearchCriteria(criteria, "PartyFrom.Code", this.CurrentUser.Code); //区域权限

        IList<InProcessLocation> inProcessLocationList = TheCriteriaMgr.FindAll<InProcessLocation>(criteria);
        if (inProcessLocationList != null && inProcessLocationList.Count > 0)
        {
            foreach (InProcessLocation inProcessLocation in inProcessLocationList)
            {
                IList<object> list = new List<object>();
                list.Add(inProcessLocation);
                list.Add(inProcessLocation.InProcessLocationDetails);

                string printUrl = TheReportMgr.WriteToFile(inProcessLocation.AsnTemplate, list);//to be refactored
                AppendPrintUrl(url, printUrl);
                inProcessLocation.IsPrinted = true;//to be refactored
                TheInProcessLocationMgr.UpdateInProcessLocation(inProcessLocation);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Print();
    }
}
