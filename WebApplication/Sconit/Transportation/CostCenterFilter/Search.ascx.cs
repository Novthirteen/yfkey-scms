using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity.Transportation;

public partial class CostCenterFilter_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ddlStatus.DataSource = this.GetAllStatus();
            this.ddlStatus.DataBind();

            this.tbStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        tbParty.ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Code"))
        {
            this.tbCode.Text = actionParameter["Code"];
        }
    }

    public IList<CodeMaster> GetAllStatus()
    {
        IList<CodeMaster> statusGroup = new List<CodeMaster>();

        statusGroup.Add(new CodeMaster());//空行

        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_VOID));

        return statusGroup;
    }
    private CodeMaster GetStatus(string statusValue)
    {
        return TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_STATUS, statusValue);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        string code = this.tbCode.Text != string.Empty ? this.tbCode.Text.Trim() : string.Empty;
        string status = this.ddlStatus.SelectedValue;
        string partyCode = this.tbParty.Text != string.Empty ? this.tbParty.Text.Trim() : string.Empty;
        string externalBillNo = this.tbExternalBillNo.Text != string.Empty ? this.tbExternalBillNo.Text.Trim() : string.Empty;
        string startDate = this.tbStartDate.Text != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
        string endDate = this.tbEndDate.Text != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(TransportationBill));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(TransportationBill))
                .SetProjection(Projections.Count("BillNo"));

            selectCriteria.CreateAlias("BillAddress", "ba");
            selectCountCriteria.CreateAlias("BillAddress", "ba");
            selectCriteria.CreateAlias("ba.Party", "pf");
            selectCountCriteria.CreateAlias("ba.Party", "pf");

            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Like("BillNo", code, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("BillNo", code, MatchMode.Start));
            }
            if (status != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Status", status));
                selectCountCriteria.Add(Expression.Eq("Status", status));
            }
            if (partyCode != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("pf.Code", partyCode));
                selectCountCriteria.Add(Expression.Eq("pf.Code", partyCode));
            }
            if (externalBillNo != string.Empty)
            {
                selectCriteria.Add(Expression.Like("ExternalBillNo", externalBillNo, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("ExternalBillNo", externalBillNo, MatchMode.Start));
            }
            if (startDate != string.Empty)
            {
                selectCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
                selectCountCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
            }
            if (endDate != string.Empty)
            {
                selectCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(endDate).AddDays(1).AddMilliseconds(-1)));
                selectCountCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(endDate).AddDays(1).AddMilliseconds(-1)));
            }

            DetachedCriteria[] partyCrieteria = SecurityHelper.GetPartyPermissionCriteria(this.CurrentUser.Code,
                BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION,
                BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_CARRIER);

            selectCriteria.Add(
                Expression.Or(
                  Expression.Or(
                      Subqueries.PropertyIn("pf.Code", partyCrieteria[0]),
                      Subqueries.PropertyIn("pf.Code", partyCrieteria[1])
                                ),
                      Expression.IsNull("pf.Code")
                              )
                );

            selectCountCriteria.Add(
                Expression.Or(
                  Expression.Or(
                      Subqueries.PropertyIn("pf.Code", partyCrieteria[0]),
                      Subqueries.PropertyIn("pf.Code", partyCrieteria[1])
                                ),
                      Expression.IsNull("pf.Code")
                              )
                );

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }
}
