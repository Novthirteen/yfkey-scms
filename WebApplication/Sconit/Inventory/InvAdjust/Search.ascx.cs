﻿using System;
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
using com.Sconit.Entity;
using NHibernate.Expression;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Utility;

public partial class Inventory_InvAdjust_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            IList<CodeMaster> statusList = GetStatusGroup();
            statusList.Insert(0, new CodeMaster()); //添加空选项
            this.ddlStatus.DataSource = statusList;
            this.ddlStatus.DataBind();
        }
        this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            string location = this.tbLocation.Text.Trim() != string.Empty ? this.tbLocation.Text.Trim() : string.Empty;
            string startDate = this.tbStartDate.Text.Trim() != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
            string endDate = this.tbEndDate.Text.Trim() != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;
            string orderNo = this.tbOrderNo.Text.Trim() != string.Empty ? this.tbOrderNo.Text.Trim() : string.Empty;
            string status = this.ddlStatus.SelectedValue.Trim();

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(CycleCount));
            selectCriteria.CreateAlias("Location", "l");
            selectCriteria.CreateAlias("l.Region", "r");

            SecurityHelper.SetRegionSearchCriteria(selectCriteria, "r.Code", this.CurrentUser.Code);//区域权限

            if (location != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Location.Code", location));
            }
            if (startDate != string.Empty)
            {
                selectCriteria.Add(Expression.Ge("EffectiveDate", DateTime.Parse(startDate)));
            }
            if (endDate != string.Empty)
            {
                selectCriteria.Add(Expression.Le("EffectiveDate", DateTime.Parse(endDate)));
            }
            if (orderNo != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Code", orderNo, MatchMode.Start));
            }
            if (status != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Status", status));
            }

            DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(selectCriteria);
            selectCountCriteria.SetProjection(Projections.Count("Code"));
            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Region"))
        {
            this.tbRegion.Text = actionParameter["Region"];
        }
        if (actionParameter.ContainsKey("Location"))
        {
            this.tbLocation.Text = actionParameter["Location"];
        }
        if (actionParameter.ContainsKey("StartDate"))
        {
            this.tbStartDate.Text = actionParameter["StartDate"];
        }
        if (actionParameter.ContainsKey("EndDate"))
        {
            this.tbEndDate.Text = actionParameter["EndDate"];
        }
        if (actionParameter.ContainsKey("OrderNo"))
        {
            this.tbOrderNo.Text = actionParameter["OrderNo"];
        }
        if (actionParameter.ContainsKey("Status"))
        {
            this.ddlStatus.SelectedValue = actionParameter["Status"];
        }
    }

    private IList<CodeMaster> GetStatusGroup()
    {
        IList<CodeMaster> statusGroup = new List<CodeMaster>();
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE));

        return statusGroup;
    }

    private CodeMaster GetStatus(string statusValue)
    {
        return TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_STATUS, statusValue);
    }
}
