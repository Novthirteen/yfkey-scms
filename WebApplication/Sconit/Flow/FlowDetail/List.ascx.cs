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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity;

public partial class MasterData_FlowDetail_List : ListModuleBase
{
    public event EventHandler ListEditEvent;
    public event EventHandler ListViewEvent;

    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    public string FlowCode
    {
        get
        {
            return (string)ViewState["FlowCode"];
        }
        set
        {
            ViewState["FlowCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateView();
        }
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();

        HideFields();
    }

    private void HideFields()
    {
        if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT)
        {
            this.GV_List.Columns[5].Visible = false;
            this.GV_List.Columns[7].Visible = true;
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS)
        {
            this.GV_List.Columns[5].Visible = false;
          
   
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
        {
            this.GV_List.Columns[6].Visible = false;
            this.GV_List.Columns[7].Visible = true;
          
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
        {
            this.GV_List.Columns[5].HeaderText = "${MasterData.Flow.FlowDetail.LocFrom.Production}";
            this.GV_List.Columns[6].HeaderText = "${MasterData.Flow.FlowDetail.LocTo.Production}";

        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
        {

        }
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(((LinkButton)sender).CommandArgument);
        TheFlowDetailMgr.DeleteFlowDetail(id);
        ShowSuccessMessage("MasterData.Flow.FlowDetail.DeleteFlowDetail.Successfully");
        UpdateView();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        int id = int.Parse(((LinkButton)sender).CommandArgument);
        ListEditEvent(id, e);
    }

    protected void lbtnView_Click(object sender, EventArgs e)
    {
        int id = int.Parse(((LinkButton)sender).CommandArgument);
        ListViewEvent(id, e);
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FlowDetail flowDetail = (FlowDetail)e.Row.DataItem;
            if (flowDetail.Flow.Code != this.FlowCode)
            {
                //引用明细，隐藏Edit和Delete Link
                LinkButton lbtnEdit = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].FindControl("lbtnEdit");
                LinkButton lbtnDelete = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].FindControl("lbtnDelete");
                lbtnEdit.Visible = false;
                lbtnDelete.Visible = false;
            }
            else
            {
                //非引用明细，隐藏View Link
                LinkButton lbtnView = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].FindControl("lbtnView");
                lbtnView.Visible = false;
            }
        }
    }
}
