using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.Distribution;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Entity.MasterData;
using System.Collections;

public partial class Visualization_InTransit_InTransitList : ListModuleBase
{
    public event EventHandler BackEvent;

    public string Item
    {
        get { return (string)ViewState["Item"]; }
        set { ViewState["Item"] = value; }
    }
    public string Location
    {
        get { return (string)ViewState["Location"]; }
        set { ViewState["Location"] = value; }
    }
    public string IOType
    {
        get { return (string)ViewState["IOType"]; }
        set { ViewState["IOType"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string itemCode, string loc, string ioType)
    {
        this.Item = itemCode;
        this.Location = loc;
        this.IOType = ioType;

        this.SetCriteria();
        this.UpdateView();
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    private void SetCriteria()
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(InProcessLocationDetail));
        criteria.CreateAlias("OrderLocationTransaction", "olt");
        criteria.CreateAlias("InProcessLocation", "ip");
        criteria.Add(Expression.Eq("ip.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
        criteria.Add(Expression.Eq("ip.Type", BusinessConstants.CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_NORMAL));

        if (this.IOType == BusinessConstants.IO_TYPE_IN)
        {
            DetachedCriteria subCriteria = DetachedCriteria.For(typeof(OrderLocationTransaction));
            subCriteria.CreateAlias("OrderDetail", "od");
            subCriteria.CreateAlias("od.OrderHead", "oh");
            OrderHelper.SetOpenOrderStatusCriteria(subCriteria, "oh.Status");
            subCriteria.Add(Expression.Eq("IOType", BusinessConstants.IO_TYPE_IN));
            subCriteria.Add(Expression.Eq("Item.Code", this.Item));
            subCriteria.Add(Expression.Eq("Location.Code", this.Location));
            subCriteria.SetProjection(Projections.ProjectionList().Add(Projections.GroupProperty("OrderDetail.Id")));
            
            criteria.Add(Subqueries.PropertyIn("olt.OrderDetail.Id", subCriteria));
        }
        else
        {
            criteria.Add(Expression.Eq("olt.Item.Code", this.Item));
            criteria.Add(Expression.Eq("olt.Location.Code", this.Location));
        }

        DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(criteria);
        selectCountCriteria.SetProjection(Projections.Count("Id"));
        this.SetSearchCriteria(criteria, selectCountCriteria);
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
        //if (this.BackEvent != null)
        //{
        //    this.BackEvent(this, e);
        //}
    }
}
