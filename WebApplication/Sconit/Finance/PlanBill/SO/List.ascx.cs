using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;

public partial class Finance_PlanBill_List : ListModuleBase
{
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
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void BindDataSource(DetachedCriteria selectCriteria)
    {
        IList<PlannedBill> plannedBillList = TheCriteriaMgr.FindAll<PlannedBill>(selectCriteria);
        this.AutoCalculate(plannedBillList);
        this.GV_List.DataSource = plannedBillList;
        this.GV_List.DataBind();
        UpdateView();
    }

    public void BindDataSource(IList<PlannedBill> plannedBillList)
    {
        this.GV_List.DataSource = plannedBillList;
        this.GV_List.DataBind();
        UpdateView();
    }

    public override void UpdateView()
    {
        if (this.GV_List.DataSource != null && ((IList<PlannedBill>)this.GV_List.DataSource).Count > 0)
        {
            this.lblNoRecordFound.Visible = false;
        }
        else
        {
            this.lblNoRecordFound.Visible = true;
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    TextBox tbCurrentActingQty = (TextBox)e.Row.FindControl("tbCurrentActingQty");
        //    Label lbActingQty = (Label)e.Row.FindControl("lbActingQty");
        //    Label lbPlannedQty = (Label)e.Row.FindControl("lbPlannedQty");
        //    decimal plannedQty = decimal.Parse(lbPlannedQty.Text.Trim());
        //    decimal actingQty = lbActingQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(lbActingQty.Text.Trim());
        //    tbCurrentActingQty.Text = (plannedQty - actingQty).ToString();
        //}
    }

    public IList<PlannedBill> PopulateSelectedData()
    {
        if (this.GV_List.Rows != null && this.GV_List.Rows.Count > 0)
        {
            IList<PlannedBill> plannedBillList = new List<PlannedBill>();
            foreach (GridViewRow row in this.GV_List.Rows)
            {

                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    HiddenField hfId = row.FindControl("hfId") as HiddenField;
                    TextBox tbCurrentActingQty = row.FindControl("tbCurrentActingQty") as TextBox;

                    PlannedBill plannedBill = ThePlannedBillMgr.LoadPlannedBill(int.Parse(hfId.Value));
                    plannedBill.CurrentActingQty = tbCurrentActingQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbCurrentActingQty.Text.Trim());
                    plannedBillList.Add(plannedBill);

                }
            }
            return plannedBillList;
        }

        return null;
    }

    private void AutoCalculate(IList<PlannedBill> plannedBillList)
    {
        if (plannedBillList != null && plannedBillList.Count > 0)
        {
            foreach (PlannedBill plannedBill in plannedBillList)
            {
                decimal actingQty = plannedBill.ActingQty.HasValue ? plannedBill.ActingQty.Value : 0;
                plannedBill.CurrentActingQty = plannedBill.PlannedQty > actingQty ? plannedBill.PlannedQty - actingQty : 0;
            }
        }
    }

}
