using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using Geekees.Common.Controls;
using com.Sconit.Entity.MRP;

public partial class MRP_Schedule_ProductionSchedule_Search : ListModuleBase
{
    public event EventHandler lbNewClickEvent;

    private DateTime StartDate
    {
        get { return this.tbStartDate.Text == string.Empty ? DateTime.Today : DateTime.Parse(this.tbStartDate.Text); }
    }

    private DateTime EndDate
    {
        get { return this.tbEndDate.Text == string.Empty ? DateTime.Today.AddDays(1) : DateTime.Parse(this.tbEndDate.Text); }
    }

    private List<string> StatusList
    {
        get { return this.astvMyTree.GetCheckedNodes().Select(a => a.NodeValue).ToList(); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
       
        if (!IsPostBack)
        {
            ASTreeViewNode astCREATE = this.GetASTNode(BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE);
            astCREATE.CheckedState = ASTreeViewCheckboxState.Checked;
            this.astvMyTree.RootNode.AppendChild(astCREATE);

            ASTreeViewNode astSUBMIT = this.GetASTNode(BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT);
            astSUBMIT.CheckedState = ASTreeViewCheckboxState.Checked;
            this.astvMyTree.RootNode.AppendChild(astSUBMIT);

            ASTreeViewNode astCANCEL = this.GetASTNode(BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL);
            this.astvMyTree.RootNode.AppendChild(astCANCEL);

            this.astvMyTree.InitialDropdownText = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE + "," + BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT;
            this.astvMyTree.DropdownText = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE + "," + BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT;

            this.tbStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
        }
    }

    public override void UpdateView()
    {
        
    }

    private ASTreeViewNode GetASTNode(string status)
    {
        CodeMaster codeMaster = TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_STATUS, status);
        ASTreeViewNode ast = new ASTreeViewNode(codeMaster.Description, codeMaster.Value);
        return ast;
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal ltlReleaseUser = (Literal)e.Row.FindControl("ltlReleaseUser");
            if (ltlReleaseUser.Text != string.Empty)
            {
                ltlReleaseUser.Text = TheUserMgr.LoadUser(ltlReleaseUser.Text).CodeName;
            }
            Literal ltlCreateUser = (Literal)e.Row.FindControl("ltlCreateUser");
            ltlCreateUser.Text = TheUserMgr.LoadUser(ltlCreateUser.Text).CodeName;
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        lbNewClickEvent(null, null);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    public void DoSearch()
    {
        IList<CustomerSchedule> customerScheduleList = TheCustomerScheduleMgr.GetCustomerSchedules(this.tbFlow.Text.Trim(), tbRefOrderNo.Text.Trim(), StatusList, StartDate, EndDate);

        this.GV_List.DataSource = customerScheduleList;
        this.GV_List.DataBind();
        this.fld_Details.Visible = true;
        this.ltl_Result.Visible = customerScheduleList.Count == 0;
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (lbNewClickEvent != null)
        {
            string customerSheduleId = ((LinkButton)sender).CommandArgument;
            lbNewClickEvent(customerSheduleId, e);
        }
    }

}
