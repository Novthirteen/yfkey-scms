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
using com.Sconit.Web;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;
using NHibernate.Expression;
using com.Sconit.Utility;
using Geekees.Common.Controls;

public partial class Order_OrderHead_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;
    public event EventHandler BtnImportClick;

    private IDictionary<string, string> parameter = new Dictionary<string, string>();
    private List<string> statusList = new List<string>();
    private List<string> typeList = new List<string>();

    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    public string ModuleSubType
    {
        get { return (string)ViewState["ModuleSubType"]; }
        set { ViewState["ModuleSubType"] = value; }
    }

    public int StatusGroupId
    {
        get { return (int)ViewState["StatusGroupId"]; }
        set { ViewState["StatusGroupId"] = value; }
    }

    //新品
    public bool NewItem
    {
        get { return (bool)ViewState["NewItem"]; }
        set { ViewState["NewItem"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbPartyFrom.ServiceParameter = "string:" + this.ModuleType + ",string:" + this.CurrentUser.Code;
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:false,bool:false,bool:true,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH;

        if (!IsPostBack)
        {
            if (this.StatusGroupId == 7)
            {
                this.btnNew.Visible = false;
                this.btnImport.Visible = false;
            }
            IList<CodeMaster> statusList = GetStatusGroup(this.StatusGroupId);

            IList<CodeMaster> orderSubTypeList = GetorderSubTypeGroup(this.ModuleType);
            orderSubTypeList.Insert(0, new CodeMaster()); //添加空选项
            this.ddlSubType.DataSource = orderSubTypeList;
            this.ddlSubType.DataBind();
            GenerateTree();
        }
    }

    private void GenerateTree()
    {
        IList<CodeMaster> statusList = GetStatusGroup(this.StatusGroupId);
        foreach (CodeMaster status in statusList)
        {
            this.astvMyTree.RootNode.AppendChild(new ASTreeViewLinkNode(status.Description, status.Value, string.Empty));
        }
        if (this.StatusGroupId == 7 || this.StatusGroupId == 4)
        {
            this.astvMyTree.RootNode.ChildNodes[0].CheckedState = ASTreeViewCheckboxState.Checked;
            this.astvMyTree.RootNode.ChildNodes[1].CheckedState = ASTreeViewCheckboxState.Checked;
            this.astvMyTree.RootNode.ChildNodes[3].CheckedState = ASTreeViewCheckboxState.Checked;
            this.astvMyTree.InitialDropdownText = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE + "," + BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS + "," + BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT;
            this.astvMyTree.DropdownText = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE + "," + BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS + "," + BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT;
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (NewEvent != null)
        {
            NewEvent(sender, e);
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (BtnImportClick != null)
        {
            BtnImportClick(sender, e);
        }
    }

    protected void btnNamedQuery_Click(object sender, EventArgs e)
    {
        IDictionary<string, string> actionParameter = new Dictionary<string, string>();
        if (this.tbOrderNo.Text != string.Empty)
        {
            actionParameter.Add("OrderNo", this.tbOrderNo.Text);
        }
        if (this.ddlPriority.Text != string.Empty)
        {
            actionParameter.Add("Priority", this.ddlPriority.SelectedValue);
        }
        if (this.tbPartyFrom.Text != string.Empty)
        {
            actionParameter.Add("PartyFrom", this.tbPartyFrom.Text);
        }

        if (this.tbLocFrom.Text != string.Empty)
        {
            actionParameter.Add("LocFrom", this.tbLocFrom.Text);
        }
        if (this.tbLocTo.Text != string.Empty)
        {
            actionParameter.Add("LocTo", this.tbLocTo.Text);
        }
        if (this.tbFlow.Text != string.Empty)
        {
            actionParameter.Add("Flow", this.tbFlow.Text);
        }

        if (this.tbCreateUser.Text != string.Empty)
        {
            actionParameter.Add("CreateUser", this.tbCreateUser.Text);
        }
        this.SaveNamedQuery(this.tbNamedQuery.Text, actionParameter);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        if (SearchEvent != null)
        {
            if (btn == this.btnExport)
            {
                FillParameter();
                if (this.rblListFormat.SelectedValue == "Detail")
                {
                    object[] criteriaParam = CriteriaHelper.CollectDetailParam(this.parameter, statusList, typeList, NewItem);
                    Array.Resize<object>(ref criteriaParam, 5);
                    criteriaParam[4] = true;
                    SearchEvent(criteriaParam, null);
                }
                else
                {
                    object[] criteriaParam = CriteriaHelper.CollectMasterParam(this.parameter, statusList, typeList, NewItem);
                    Array.Resize<object>(ref criteriaParam, 5);
                    criteriaParam[4] = true;
                    SearchEvent(criteriaParam, null);
                }
            }
            else
            {
                DoSearch();
            }
        }
    }

    private void FillParameter()
    {
        typeList.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION);

        #region status
        List<ASTreeViewNode> nodes = this.astvMyTree.GetCheckedNodes();
        foreach (ASTreeViewNode node in nodes)
        {
            statusList.Add(node.NodeValue);
        }
        if (statusList.Count > 0)
        {
        }
        else if (this.parameter.ContainsKey("Status"))
        {
            statusList.Add(this.parameter["Status"]);
        }
        else
        {
            if (this.StatusGroupId == 7)
            {
                statusList.Add(BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE);
                statusList.Add(BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT);
                statusList.Add(BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS);
            }
            else
            {
                #region 根据StatusGroupId限制查询订单的状态
                foreach (CodeMaster status in GetStatusGroup(this.StatusGroupId))
                {
                    statusList.Add(status.Value);
                }
                #endregion
            }
        }
        #endregion

        this.ModuleSubType = this.ddlSubType.SelectedValue;

        this.parameter.Clear();
        this.parameter.Add("OrderNo", this.tbOrderNo.Text.Trim());
        this.parameter.Add("Flow", this.tbFlow.Text.Trim());
        this.parameter.Add("PartyFrom", this.tbPartyFrom.Text.Trim());
        this.parameter.Add("ModuleType", this.ModuleType);
        this.parameter.Add("LocationFrom", this.tbLocFrom.Text.Trim());
        this.parameter.Add("LocationTo", this.tbLocTo.Text.Trim());
        this.parameter.Add("ModuleSubType", this.ModuleSubType);
        this.parameter.Add("Priority", this.ddlPriority.SelectedValue);
        this.parameter.Add("CreateUser", this.tbCreateUser.Text.Trim());
        this.parameter.Add("StartDate", this.tbStartDate.Text.Trim());
        this.parameter.Add("CurrentUser", this.CurrentUser.Code);
    }
    protected override void DoSearch()
    {
        FillParameter();
        if (this.rblListFormat.SelectedValue == "Detail")
        {
            object[] criteriaParam = CriteriaHelper.CollectDetailParam(this.parameter, statusList, typeList, NewItem);
            Array.Resize<object>(ref criteriaParam, 5);
            criteriaParam[4] = false;
            SearchEvent(criteriaParam, null);
        }
        else
        {
            object[] criteriaParam = CriteriaHelper.CollectMasterParam(this.parameter, statusList, typeList, NewItem);
            Array.Resize<object>(ref criteriaParam, 5);
            criteriaParam[4] = false;
            SearchEvent(criteriaParam, null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        this.parameter = actionParameter;

        if (actionParameter.ContainsKey("OrderNo"))
        {
            this.tbOrderNo.Text = actionParameter["OrderNo"];
        }
        if (actionParameter.ContainsKey("Priority"))
        {
            this.ddlPriority.SelectedValue = actionParameter["Priority"];
        }
        if (actionParameter.ContainsKey("PartyFrom"))
        {
            this.tbPartyFrom.Text = actionParameter["PartyFrom"];
        }

        if (actionParameter.ContainsKey("LocFrom"))
        {
            this.tbLocFrom.Text = actionParameter["LocFrom"];
        }
        if (actionParameter.ContainsKey("LocTo"))
        {
            this.tbLocTo.Text = actionParameter["LocTo"];
        }
    }

    private IList<CodeMaster> GetStatusGroup(int statusGroupId)
    {
        IList<CodeMaster> statusGroup = new List<CodeMaster>();
        switch (statusGroupId)
        {
            case 1:   //新建
                statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
                statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT));
                break;
            case 2:   //发货
                statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
                break;
            case 3:   //收货
                statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
                break;
            case 4:   //All
            case 7:   //首页/订单跟踪
                statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
                statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT));
                statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL));
                statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
                statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE));
                statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE));
                break;
            case 5:   //生产上线/取消
                statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT));
                statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
                statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL));
                break;
            default:
                break;
        }

        return statusGroup;
    }

    private IList<CodeMaster> GetorderSubTypeGroup(string moduleType)
    {
        IList<CodeMaster> orderSubTypeGroup = new List<CodeMaster>();
        if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
            || moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            orderSubTypeGroup.Add(GetorderSubType(BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML));
            orderSubTypeGroup.Add(GetorderSubType(BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN));
            orderSubTypeGroup.Add(GetorderSubType(BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_ADJ));
        }
        else if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            orderSubTypeGroup.Add(GetorderSubType(BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML));
            orderSubTypeGroup.Add(GetorderSubType(BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RWO));
        }
        else
        {

        }

        return orderSubTypeGroup;
    }

    private CodeMaster GetStatus(string statusValue)
    {
        return TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_STATUS, statusValue);
    }

    private CodeMaster GetorderSubType(string type)
    {
        return TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE, type);
    }

    //DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(OrderHead));
    //   DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(OrderHead))
    //       .SetProjection(Projections.Count("OrderNo"));
    //   IDictionary<string, string> alias = new Dictionary<string, string>();
    //   selectCriteria.CreateAlias("PartyFrom", "pf");

    //   selectCountCriteria.CreateAlias("PartyFrom", "pf");

    //   alias.Add("PartyFrom", "pf");


    //   #region 设置订单Type查询条件

    //   selectCriteria.Add(Expression.In("Type", new string[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION }));
    //   selectCountCriteria.Add(Expression.In("Type", new string[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION }));

    //   #endregion

    //   #region 设置订单SubType查询条件
    //   this.ModuleSubType = this.ddlSubType.SelectedValue;
    //   if (this.ModuleSubType != string.Empty)
    //   {
    //       selectCriteria.Add(Expression.Eq("SubType", this.ModuleSubType));
    //       selectCountCriteria.Add(Expression.Eq("SubType", this.ModuleSubType));
    //   }
    //   #endregion

    //   if (tbOrderNo != null && tbOrderNo.Text != string.Empty)
    //   {
    //       selectCriteria.Add(Expression.Like("OrderNo", tbOrderNo.Text.Trim(), MatchMode.Start));
    //       selectCountCriteria.Add(Expression.Like("OrderNo", tbOrderNo.Text.Trim(), MatchMode.Start));
    //   }

    //   if (ddlPriority != null && ddlPriority.SelectedIndex != 0)
    //   {
    //       selectCriteria.Add(Expression.Eq("Priority", ddlPriority.SelectedValue));
    //       selectCountCriteria.Add(Expression.Eq("Priority", ddlPriority.SelectedValue));
    //   }


    //   selectCriteria.Add(Expression.Eq("IsNewItem", this.NewItem));
    //   selectCountCriteria.Add(Expression.Eq("IsNewItem", this.NewItem));

    //   #region partyFrom
    //   SecurityHelper.SetPartyFromSearchCriteria(
    //       selectCriteria, selectCountCriteria, (this.tbPartyFrom != null ? this.tbPartyFrom.Text : null), this.ModuleType, this.CurrentUser.Code);
    //   #endregion



    //   if (tbLocFrom != null && tbLocFrom.Text != string.Empty)
    //   {
    //       selectCriteria.Add(Expression.Eq("LocationFrom.Code", tbLocFrom.Text.Trim()));
    //       selectCountCriteria.Add(Expression.Eq("LocationFrom.Code", tbLocFrom.Text.Trim()));
    //   }

    //   if (tbLocTo != null && tbLocTo.Text != string.Empty)
    //   {
    //       selectCriteria.Add(Expression.Eq("LocationTo.Code", tbLocTo.Text.Trim()));
    //       selectCountCriteria.Add(Expression.Eq("LocationTo.Code", tbLocTo.Text.Trim()));
    //   }

    //   if (tbFlow != null && tbFlow.Text != string.Empty)
    //   {
    //       selectCriteria.Add(Expression.Eq("Flow.Code", tbFlow.Text.Trim()));
    //       selectCountCriteria.Add(Expression.Eq("Flow.Code", tbFlow.Text.Trim()));
    //   }

    //   if (tbCreateUser != null && tbCreateUser.Text != string.Empty)
    //   {
    //       selectCriteria.Add(Expression.Eq("CreateUser.Code", tbCreateUser.Text.Trim()));
    //       selectCountCriteria.Add(Expression.Eq("CreateUser.Code", tbCreateUser.Text.Trim()));
    //   }

    //   if (tbStartDate != null && tbStartDate.Text.Trim() != string.Empty)
    //   {
    //       selectCriteria.Add(Expression.Ge("StartTime", DateTime.Parse(tbStartDate.Text.Trim())));
    //       selectCriteria.Add(Expression.Lt("StartTime", DateTime.Parse(tbStartDate.Text.Trim()).AddDays(1)));

    //       selectCountCriteria.Add(Expression.Ge("StartTime", DateTime.Parse(tbStartDate.Text.Trim())));
    //       selectCountCriteria.Add(Expression.Lt("StartTime", DateTime.Parse(tbStartDate.Text.Trim()).AddDays(1)));
    //   }


    //   List<ASTreeViewNode> nodes = this.astvMyTree.GetCheckedNodes();
    //   List<string> nodeList = new List<string>();

    //   foreach (ASTreeViewNode node in nodes)
    //   {
    //       nodeList.Add(node.NodeValue);
    //   }

    //   if (nodeList.Count > 0)
    //   {
    //       selectCriteria.Add(Expression.In("Status", nodeList));
    //       selectCountCriteria.Add(Expression.In("Status", nodeList));
    //   }
    //   else if (this.parameter.ContainsKey("Status"))
    //   {
    //       selectCriteria.Add(Expression.Eq("Status", this.parameter["Status"]));
    //       selectCountCriteria.Add(Expression.Eq("Status", this.parameter["Status"]));
    //   }
    //   else
    //   {
    //       #region 根据StatusGroupId限制查询订单的状态
    //       IList statusList = new ArrayList();
    //       if (this.StatusGroupId == 7)
    //       {
    //           statusList.Add(BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE);
    //           statusList.Add(BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT);
    //           statusList.Add(BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS);
    //       }
    //       else
    //       {
    //           foreach (CodeMaster status in GetStatusGroup(this.StatusGroupId))
    //           {
    //               statusList.Add(status.Value);
    //           }
    //       }
    //       selectCriteria.Add(Expression.In("Status", statusList));
    //       selectCountCriteria.Add(Expression.In("Status", statusList));
    //       #endregion
    //   }

    //   SearchEvent((new object[] { selectCriteria, selectCountCriteria, alias }), null);
}
