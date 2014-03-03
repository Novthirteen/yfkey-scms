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
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;

public partial class MasterData_OrderBinding_ListBinding : ListModuleBase
{

    public event EventHandler BackEvent;

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

    public string OrderNo
    {
        get
        {
            return (string)ViewState["OrderNo"];
        }
        set
        {
            ViewState["OrderNo"] = value;
        }
    }

    public override void UpdateView()
    {
        OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(this.OrderNo);
        IList<OrderBinding> orderBindingList = TheOrderBindingMgr.GetOrderBinding(this.OrderNo);
        if (orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
        {
            OrderBinding orderBinding = new OrderBinding();
            orderBinding.IsBlank = true;
            orderBindingList.Add(orderBinding);
            this.GV_List.Columns[4].Visible = true;
        }
        else
        {
            this.GV_List.Columns[4].Visible = false;
        }

        this.Visible = orderBindingList.Count == 0 ? false : true;

        this.GV_List.DataSource = orderBindingList;
        this.GV_List.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            OrderBinding orderBinding = (OrderBinding)e.Row.DataItem;
            if (orderBinding.IsBlank)
            {
                ((Label)e.Row.FindControl("lbFlow")).Visible = false;
                ((Controls_TextBox)e.Row.FindControl("tbFlow")).Visible = true;

                ((Label)e.Row.FindControl("lblBindingType")).Visible = false;
                DropDownList ddlBindingType = (DropDownList)e.Row.FindControl("ddlBindingType");
                ddlBindingType.Visible = true;
                ddlBindingType.DataSource = BindingTypeDataBind();
                ddlBindingType.DataBind();

                ((Label)e.Row.FindControl("lbRemark")).Visible = false;
                ((TextBox)e.Row.FindControl("tbRemark")).Visible = true;

                ((LinkButton)e.Row.FindControl("lbtnDelete")).Visible = false;
            }
            if (orderBinding.BindedOrderNo != null)
            {
                ((LinkButton)e.Row.FindControl("lbtnDelete")).Visible = false;
            }
        }
    }

    protected void GV_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //添加
        if (e.CommandName.Equals("AddBinding"))
        {
            int index = int.Parse(e.CommandArgument.ToString());
            GridViewRow row = this.GV_List.Rows[index];

            string flowCode = ((Controls_TextBox)row.FindControl("tbFlow")).Text.Trim();
            if (flowCode == string.Empty)
            {
                ShowErrorMessage("MasterData.Order.Binding.Flow.Empty");
                return;
            }
            string bindingType = ((DropDownList)row.FindControl("ddlBindingType")).SelectedValue;
            string remark = ((TextBox)row.FindControl("tbRemark")).Text.Trim();

            #region 检查是否存在
            OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(this.OrderNo);
            if (orderHead.Flow == flowCode)
            {
                ShowErrorMessage("MasterData.Order.Binding.Self");
                return;
            }
            for (int i = 0; i < index; i++)
            {
                GridViewRow oneRow = this.GV_List.Rows[i];
                string oneFlowCode = ((Label)oneRow.FindControl("lbFlow")).Text.Trim();
                if (flowCode == oneFlowCode)
                {
                    ShowErrorMessage("MasterData.Order.Binding.Flow.Exists");
                    return;
                }
            }
            #endregion

            OrderBinding orderBinding = new OrderBinding();
            orderBinding.OrderHead = orderHead;
            orderBinding.BindedFlow = TheFlowMgr.LoadFlow(flowCode);
            orderBinding.BindingType = bindingType;
            orderBinding.Remark = remark;
            TheOrderBindingMgr.CreateOrderBinding(orderBinding);
            ShowSuccessMessage("MasterData.Order.Binding.Add.Successfully");
            UpdateView();
        }
        else if (e.CommandName.Equals("DeleteBinding"))
        {
            int id = int.Parse(e.CommandArgument.ToString());
            TheOrderBindingMgr.DeleteOrderBinding(id);
            ShowSuccessMessage("MasterData.Order.Binding.Delete.Successfully");
            UpdateView();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (this.BackEvent != null)
        {
            this.BackEvent(this, e);
        }
    }

    private IList<CodeMaster> BindingTypeDataBind()
    {
        IList<CodeMaster> bindingGroup = new List<CodeMaster>();

        bindingGroup.Add(TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_BINDING_TYPE, BusinessConstants.CODE_MASTER_BINDING_TYPE_VALUE_SUBMIT));
        bindingGroup.Add(TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_BINDING_TYPE, BusinessConstants.CODE_MASTER_BINDING_TYPE_VALUE_RECEIVE_ASYN));
        bindingGroup.Add(TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_BINDING_TYPE, BusinessConstants.CODE_MASTER_BINDING_TYPE_VALUE_RECEIVE_SYN));

        return bindingGroup;
    }
}
