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
using com.Sconit.Entity.Exception;
using System.Data.SqlClient;

public partial class Finance_Bill_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;



    protected void Page_Load(object sender, EventArgs e)
    {

        this.tbPartyFrom.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT + ",string:" + this.CurrentUser.Code;

        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }



    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        Button btn = (Button)sender;
        if (SearchEvent != null)
        {
            if (btn == this.btnExport)
            {
                object criteriaParam = this.CollectDetailParam(true);
                SearchEvent(criteriaParam, null);
            }
            else
            {
                DoSearch();
            }
        }
    }



    protected override void DoSearch()
    {
        object criteriaParam = this.CollectDetailParam(false);
        SearchEvent(criteriaParam, null);

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }


    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {

    }

    private object CollectDetailParam(bool isExport)
    {

        string billNo = this.tbBillNo.Text != string.Empty ? this.tbBillNo.Text.Trim() : string.Empty;
        string startDate = this.tbStartDate.Text != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
        string endDate = this.tbEndDate.Text != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;


        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(KPOrder));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(KPOrder))
            .SetProjection(Projections.Count("ORDER_ID"));

        #region partyFrom

        if (this.tbPartyFrom != null && this.tbPartyFrom.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("PARTY_FROM_ID", this.tbPartyFrom.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("PARTY_FROM_ID", this.tbPartyFrom.Text.Trim()));
        }
        else
        {
            SecurityHelper.SetPartySearchCriteria(selectCriteria, "PARTY_FROM_ID", this.CurrentUser.Code);
            SecurityHelper.SetPartySearchCriteria(selectCountCriteria, "PARTY_FROM_ID", this.CurrentUser.Code);
        }
        #endregion

        if (billNo != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("QAD_ORDER_ID", billNo));
            selectCountCriteria.Add(Expression.Eq("QAD_ORDER_ID", billNo));
        }

        if (startDate != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("ORDER_PUB_DATE", DateTime.Parse(startDate)));
            selectCountCriteria.Add(Expression.Ge("ORDER_PUB_DATE", DateTime.Parse(startDate)));
        }
        if (endDate != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("ORDER_PUB_DATE", DateTime.Parse(endDate).AddDays(1)));
            selectCountCriteria.Add(Expression.Lt("ORDER_PUB_DATE", DateTime.Parse(endDate).AddDays(1)));
        }


        return new object[] { selectCriteria, selectCountCriteria, isExport, false };
    }

    protected void btnImportKPOrder_Click(object sender, EventArgs e)
    {
        fs01.Visible = true;
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(this.tbImportStartDate.Text))
            {
                throw new Exception("请填写开票日期。");
            }
            DateTime startTime = DateTime.Parse(this.tbImportStartDate.Text);

            TheKPOrderMgr.ImportKPOrder(startTime);
            ShowSuccessMessage("操作成功");
            fs01.Visible = false;
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        fs01.Visible = false;
    }

}
