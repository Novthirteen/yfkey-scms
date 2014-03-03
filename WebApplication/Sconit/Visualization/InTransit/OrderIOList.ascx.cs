using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.Distribution;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;
using NHibernate.Expression;
using com.Sconit.Utility;
using com.Sconit.Entity.View;

public partial class Visualization_InTransit_OrderIOList : ListModuleBase
{
    public event EventHandler BackEvent;

    #region ViewState
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
    public DateTime? EndDate
    {
        get { return (DateTime?)ViewState["EndDate"]; }
        set { ViewState["EndDate"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string itemCode, string loc, string ioType, DateTime? endDate)
    {
        this.Item = itemCode;
        this.Location = loc;
        this.IOType = ioType;
        this.EndDate = endDate;

        this.SetCriteria();
        this.UpdateView();
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
        HiddenRows();
    }

    private void SetCriteria()
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocationTransaction));
        criteria.CreateAlias("OrderDetail", "od");
        criteria.CreateAlias("od.OrderHead", "oh");

        OrderHelper.SetOpenOrderStatusCriteria(criteria, "oh.Status");//订单状态
        if (this.EndDate.HasValue)
        {
            if (IOType == BusinessConstants.IO_TYPE_IN)
                criteria.Add(Expression.Le("oh.WindowTime", this.EndDate.Value));
            else
                criteria.Add(Expression.Lt("oh.StartTime", this.EndDate.Value));
        }
        criteria.Add(Expression.Eq("Item.Code", this.Item));
        criteria.Add(Expression.Eq("Location.Code", this.Location));
        criteria.Add(Expression.Eq("IOType", this.IOType));

        DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(criteria);
        selectCountCriteria.SetProjection(Projections.Count("Id"));
        SetSearchCriteria(criteria, selectCountCriteria);
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

    /// <summary>
    /// 隐藏数量为0的待发待收数
    /// </summary>
    private void HiddenRows()
    {
        foreach (GridViewRow row in this.GV_List.Rows)
        {
            Label lblRemainReceivedQty = (Label)row.FindControl("lblRemainReceivedQty");
            Label lblRemainShippedQty = (Label)row.FindControl("lblRemainShippedQty");
            if (IOType == BusinessConstants.IO_TYPE_IN)
            {
                if (lblRemainReceivedQty.Text == "0")
                {
                    row.Visible = false;
                }
            }
            else if (IOType == BusinessConstants.IO_TYPE_OUT)
            {
                if (lblRemainShippedQty.Text == "0")
                {
                    row.Visible = false;
                }
            }
        }
    }
}
