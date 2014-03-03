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
using com.Sconit.Service.Criteria;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;


public partial class PickList_Batch_Search : SearchModuleBase
{

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        string pickListNo = this.tbPickListNo.Text != string.Empty ? this.tbPickListNo.Text.Trim() : string.Empty;
        string startDate = this.tbStartDate.Text != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
        string endDate = this.tbEndDate.Text != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;


        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(PickList));
        selectCriteria.Add(Expression.Or(Expression.Eq("Status",BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS),
            Expression.Eq("Status",BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE)));
        selectCriteria.CreateAlias("PartyFrom", "pf");
        selectCriteria.CreateAlias("PartyTo", "pt");


        //partyFrom
        DetachedCriteria[] pfCrieteria = SecurityHelper.GetPartyPermissionCriteria(this.CurrentUser.Code,
                   BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION);

        selectCriteria.Add(
            Expression.Or(
                Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
        ));

        ////partyTo
        //DetachedCriteria[] ptCrieteria = SecurityHelper.GetPartyPermissionCriteria(this.CurrentUser.Code,
        //          BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_CUSTOMER);

        //selectCriteria.Add(
        //    Expression.Or(
        //        Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
        //        Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
        //));

        if (pickListNo != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("PickListNo", pickListNo));

        }

        if (startDate != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));

        }
        if (endDate != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(endDate).AddDays(1)));
        }

        IList<PickList> pickList = TheCriteriaMgr.FindAll<PickList>(selectCriteria);
       

        this.ucList.BindDataSource(pickList);
        this.ucList.Visible = true;

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        IList<PickList> pickListList = this.ucList.PopulateSelectedData();
        if (pickListList == null || pickListList.Count == 0)
        {
            ShowErrorMessage("PickList.Error.NoData.Selected");
            return;
        }

        try
        {
            foreach (PickList pickList in pickListList)
            {
                ThePickListMgr.ManualClosePickList(pickList, this.CurrentUser);
            }
            ShowSuccessMessage("PickList.BatchClose.Successfully");
            DoSearch();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }
}
