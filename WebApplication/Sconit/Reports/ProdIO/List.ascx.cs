using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using System.Collections;
using com.Sconit.Utility;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using com.Sconit.Entity;
using NHibernate.Transform;

public partial class Reports_ProdIO_List : ReportListModuleBase
{
    private string Flow
    {
        get { return (string)ViewState["Flow"]; }
        set { ViewState["Flow"] = value; }
    }
    private string Region
    {
        get { return (string)ViewState["Region"]; }
        set { ViewState["Region"] = value; }
    }
    private string StartDate
    {
        get { return (string)ViewState["StartDate"]; }
        set { ViewState["StartDate"] = value; }
    }
    private string EndDate
    {
        get { return (string)ViewState["EndDate"]; }
        set { ViewState["EndDate"] = value; }
    }
    private string Item
    {
        get { return (string)ViewState["Item"]; }
        set { ViewState["Item"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void Export()
    {
        this.ExportXLS(GV_List);
    }

    public void InitPageParameter(object sender)
    {
        this.Flow = (string)((object[])sender)[0];
        this.Region = (string)((object[])sender)[1];
        this.StartDate = (string)((object[])sender)[2];
        this.EndDate = (string)((object[])sender)[3];
        this.Item = (string)((object[])sender)[4];
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    public override IList GetDataSource(int pageSize, int pageIndex)
    {
        IList<OrderDetailView> list = TheOrderDetailViewMgr.GetProdIO(Flow, Region, StartDate, EndDate, Item, this.CurrentUser.Code, pageSize, pageIndex);

        return IListHelper.ConvertToList(list);
    }

    public override int GetDataCount()
    {
        return TheOrderDetailViewMgr.GetProdIOCount(Flow, Region, StartDate, EndDate, Item, this.CurrentUser.Code);
    }
}
