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


public partial class Inventory_InspectOrder_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;
    private IDictionary<string, string> dicParam;

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbLocation.ServiceParameter = "string:" + this.CurrentUser.Code;
        if (!IsPostBack)
        {
            this.StatusDataBind();
            this.tbStartDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (NewEvent != null)
        {
            NewEvent(sender, e);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.dicParam = new Dictionary<string, string>();

        this.dicParam.Add("inspectNo", this.tbInspectNo.Text.Trim());
        this.dicParam.Add("startDate", this.tbStartDate.Text.Trim());
        this.dicParam.Add("endDate", this.tbEndDate.Text.Trim());
        this.dicParam.Add("createUser", this.tbCreateUser.Text.Trim());
        this.dicParam.Add("status", this.ddlStatus.SelectedValue);
        this.dicParam.Add("location", this.tbLocation.Text.Trim());
        this.dicParam.Add("item", this.tbItemCode.Text.Trim());
        this.dicParam.Add("disposition", ddlDisposition.SelectedValue);
        this.dicParam.Add("ipNo", this.tbIpNo.Text.Trim());
        this.dicParam.Add("receiptNo", this.tbReceiptNo.Text.Trim());

        int rblListIndex = this.rblListFormat.SelectedIndex;
        if ((this.dicParam["location"] != string.Empty || this.dicParam["item"] != string.Empty || this.dicParam["disposition"] != string.Empty)
            && rblListIndex == 0)
        {
            rblListIndex = 1;
        }
        this.rblListFormat.SelectedIndex = rblListIndex;

        Button btn = (Button)sender;
        if (SearchEvent != null)
        {
            if (btn == this.btnExport)
            {
                if (this.rblListFormat.SelectedValue == "Detail")
                {
                    object criteriaParam = this.CollectDetailParam(true);
                    SearchEvent(criteriaParam, null);
                }
                else
                {
                    object criteriaParam = this.CollectMasterParam(true);
                    SearchEvent(criteriaParam, null);
                }
            }
            else
            {
                DoSearch();
            }
        }
    }
    protected override void DoSearch()
    {
        if (this.rblListFormat.SelectedValue == "Detail")
        {
            object criteriaParam = this.CollectDetailParam(false);
            SearchEvent(criteriaParam, null);
        }
        else
        {
            object criteriaParam = this.CollectMasterParam(false);
            SearchEvent(criteriaParam, null);
        }
    }

    private object CollectMasterParam(bool isExport)
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(InspectOrder));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(InspectOrder))
            .SetProjection(Projections.ProjectionList()
           .Add(Projections.Count("InspectNo")));

        if (this.dicParam["inspectNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("InspectNo", this.dicParam["inspectNo"]));
            selectCountCriteria.Add(Expression.Eq("InspectNo", this.dicParam["inspectNo"]));
        }

        if (this.dicParam["startDate"] != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(this.dicParam["startDate"])));
            selectCountCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(this.dicParam["startDate"])));
        }
        if (this.dicParam["endDate"] != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(this.dicParam["endDate"]).AddDays(1)));
            selectCountCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(this.dicParam["endDate"]).AddDays(1)));
        }

        if (this.dicParam["status"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Status", this.dicParam["status"]));
            selectCountCriteria.Add(Expression.Eq("Status", this.dicParam["status"]));
        }
        if (this.dicParam["createUser"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("CreateUser.Code", this.dicParam["createUser"]));
            selectCountCriteria.Add(Expression.Eq("CreateUser.Code", this.dicParam["createUser"]));
        }
        if (this.dicParam["ipNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("IpNo", this.dicParam["ipNo"]));
            selectCountCriteria.Add(Expression.Eq("IpNo", this.dicParam["ipNo"]));
        }
        if (this.dicParam["receiptNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("ReceiptNo", this.dicParam["receiptNo"]));
            selectCountCriteria.Add(Expression.Eq("ReceiptNo", this.dicParam["receiptNo"]));
        }
        return new object[] { selectCriteria, selectCountCriteria, isExport, true };
    }

    private object CollectDetailParam(bool isExport)
    {

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(InspectOrderDetail));
        selectCriteria.CreateAlias("InspectOrder", "o");
        selectCriteria.CreateAlias("o.CreateUser", "u");
        selectCriteria.CreateAlias("LocationLotDetail", "t");
        selectCriteria.CreateAlias("t.Item", "i");
        selectCriteria.CreateAlias("LocationFrom", "lf");
        selectCriteria.CreateAlias("LocationTo", "lt");

        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(InspectOrderDetail)).SetProjection(Projections.Count("Id"));
        selectCountCriteria.CreateAlias("InspectOrder", "o");
        selectCountCriteria.CreateAlias("o.CreateUser", "u");
        selectCountCriteria.CreateAlias("LocationLotDetail", "t");
        selectCountCriteria.CreateAlias("t.Item", "i");
        selectCountCriteria.CreateAlias("LocationFrom", "lf");

        if (this.dicParam["location"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("lf.Code", this.dicParam["location"]));
            selectCountCriteria.Add(Expression.Eq("lf.Code", this.dicParam["location"]));
        }
        else
        {
            SecurityHelper.SetRegionSearchCriteria(selectCriteria, "lf.Region", this.CurrentUser.Code);
            SecurityHelper.SetRegionSearchCriteria(selectCountCriteria, "lf.Region", this.CurrentUser.Code);
        }

        //selectCriteria.Add(Expression.Eq("o.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
        //selectCountCriteria.Add(Expression.Eq("o.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
        if (this.dicParam["startDate"] != string.Empty)
        {
            DateTime startDate = DateTime.Parse(this.dicParam["startDate"]);
            selectCriteria.Add(Expression.Ge("o.CreateDate", startDate));
            selectCountCriteria.Add(Expression.Ge("o.CreateDate", startDate));
        }

        if (this.dicParam["endDate"] != string.Empty)
        {
            DateTime endDate = DateTime.Parse(this.dicParam["endDate"]);
            selectCriteria.Add(Expression.Lt("o.CreateDate", endDate.AddDays(1)));
            selectCountCriteria.Add(Expression.Lt("o.CreateDate", endDate.AddDays(1)));
        }

        if (this.dicParam["item"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("i.Code", this.dicParam["item"]));
            selectCountCriteria.Add(Expression.Eq("i.Code", this.dicParam["item"]));
        }
        if (this.dicParam["inspectNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("o.InspectNo", this.dicParam["inspectNo"]));
            selectCountCriteria.Add(Expression.Eq("o.InspectNo", this.dicParam["inspectNo"]));
        }
        if (this.dicParam["createUser"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("u.Code", this.dicParam["createUser"]));
            selectCountCriteria.Add(Expression.Eq("u.Code", this.dicParam["createUser"]));
        }

        if (this.dicParam["status"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("o.Status", this.dicParam["status"]));
            selectCountCriteria.Add(Expression.Eq("o.Status", this.dicParam["status"]));
        }

        if (this.dicParam["disposition"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Disposition", this.dicParam["disposition"]));
            selectCountCriteria.Add(Expression.Eq("Disposition", this.dicParam["disposition"]));
        }

        if (this.dicParam["ipNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("o.IpNo", this.dicParam["ipNo"]));
            selectCountCriteria.Add(Expression.Eq("o.IpNo", this.dicParam["ipNo"]));
        }
        if (this.dicParam["receiptNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("o.ReceiptNo", this.dicParam["receiptNo"]));
            selectCountCriteria.Add(Expression.Eq("o.ReceiptNo", this.dicParam["receiptNo"]));
        }

        return new object[] { selectCriteria, selectCountCriteria, isExport, false };
    }

    private void StatusDataBind()
    {
        this.ddlStatus.DataSource = this.GetStatusGroup();
        this.ddlStatus.DataBind();
    }
    private IList<CodeMaster> GetStatusGroup()
    {
        IList<CodeMaster> statusGroup = new List<CodeMaster>();

        statusGroup.Add(new CodeMaster()); //添加空选项
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE));

        return statusGroup;
    }
    private CodeMaster GetStatus(string statusValue)
    {
        return TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_STATUS, statusValue);
    }
}
