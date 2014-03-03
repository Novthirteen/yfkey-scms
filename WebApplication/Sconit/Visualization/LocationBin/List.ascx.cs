using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Utility;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;

public partial class Visualization_LocationBin_List : ModuleBase
{
    private object[] Param
    {
        get { return (object[])ViewState["Param"]; }
        set { ViewState["Param"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(object sender)
    {
        this.Param = (object[])sender;
        string location = (string)Param[0];
        string area = (string)Param[1];
        string bin = (string)Param[2];
        string item = (string)Param[3];
        string huId = (string)Param[4];
        string lotNo = (string)Param[5];

        #region DetachedCriteria
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(LocationBin));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(LocationBin))
            .SetProjection(Projections.Count("Id"));

        //区域权限
        SecurityHelper.SetRegionSearchCriteria(selectCriteria, selectCountCriteria, "Region", this.CurrentUser.Code);

        if (location != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Location", location));
            selectCountCriteria.Add(Expression.Eq("Location", location));
            this.GV_List.Columns[0].Visible = false;
            this.GV_List.Columns[1].Visible = false;
        }
        else
        {
            this.GV_List.Columns[0].Visible = true;
            this.GV_List.Columns[1].Visible = true;
        }
        if (area != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Area", area));
            selectCountCriteria.Add(Expression.Eq("Area", area));
        }
        if (bin != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Bin", bin));
            selectCountCriteria.Add(Expression.Eq("Bin", bin));
        }

        #endregion

        IList<LocationBin> locationBinList = TheCriteriaMgr.FindAll<LocationBin>(selectCriteria);
        this.GV_List.DataSource = locationBinList;
        this.GV_List.DataBind();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LocationBin flowView = (LocationBin)e.Row.DataItem;
            if (flowView.HuCount == 0)
            {
                LinkButton lbHuView = (LinkButton)e.Row.FindControl("lbHuView");
                lbHuView.Enabled = false;
            }
            if (flowView.ItemCount == 0)
            {
                LinkButton lbItemView = (LinkButton)e.Row.FindControl("lbItemView");
                lbItemView.Enabled = false;
            }
        }

    }

    protected void GV_List_DataBound(object sender, EventArgs e)
    {
        GridViewHelper.GV_MergeTableCell(this.GV_List, new int[] { 0, 1, 2, 3 }, true);
    }

    protected void lbItemView_Click(object sender, EventArgs e)
    {
        LinkButton lbItemView = (LinkButton)sender;
        string[] param = lbItemView.CommandArgument.Split(',');
        string location = param[0];
        string bin = param[1];
        this.ucItemView.InitPageParameter(location, bin, (string)Param[3]);
    }

    protected void lbHuView_Click(object sender, EventArgs e)
    {
        LinkButton lbHuView_Click = (LinkButton)sender;
        string[] param = lbHuView_Click.CommandArgument.Split(',');
        string location = param[0];
        string bin = param[1];
        this.ucHuView.InitPageParameter(location, bin, (string)Param[3], (string)Param[4], (string)Param[5]);
    }
}
