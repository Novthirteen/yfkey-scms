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
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    public bool IsSupplier
    {
        get { return ViewState["IsSupplier"] != null ? (bool)ViewState["IsSupplier"] : false; }
        set { ViewState["IsSupplier"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void BindDataSource(DetachedCriteria selectCriteria, bool isExport)
    {
        IList<PlannedBill> plannedBills = TheCriteriaMgr.FindAll<PlannedBill>(selectCriteria);

        if (IsSupplier)
        {
            plannedBills = (from p in plannedBills
                            group p by new { p.Item, p.OrderNo, p.ReceiptNo, p.BillAddress, p.Uom, p.CreateDate, p.UnitPrice, p.Currency } into g
                            select new PlannedBill
                            {
                                Item = g.Key.Item,
                                OrderNo = g.Key.OrderNo,
                                ReceiptNo = g.Key.ReceiptNo,
                                BillAddress = g.Key.BillAddress,
                                Uom = g.Key.Uom,
                                CreateDate = g.Key.CreateDate,
                                UnitPrice = g.Key.UnitPrice,
                                Currency = g.Key.Currency,
                                PlannedQty = g.Sum(p => p.PlannedQty),
                                PlannedAmount = g.Sum(p => p.PlannedAmount),
                                ActingQty = g.Sum(p => p.ActingQty),
                                ActingAmount = g.Sum(p => p.ActingAmount)
                            })
                            //.Where(p => this.CurrentUser.HasPermission(p.BillAddress.Party.Code))
                            .ToList();
        }
        plannedBills = plannedBills.OrderBy(p => p.CreateDate).OrderBy(p => p.Item.Code).ToList();
        this.GV_List.DataSource = plannedBills;
        this.GV_List.DataBind();
        UpdateView();
        if (isExport && GV_List.Rows.Count > 0)
        {
            string dateTime = DateTime.Now.ToString("MMddhhmmss");
            int lastColumnIndex = GV_List.Columns.Count - 1;
            DataControlField dataControlField0 = null;
            //DataControlField dataControlFieldLast = null;
            if (!IsSupplier)
            {
                dataControlField0 = GV_List.Columns[0];
                //dataControlFieldLast = GV_List.Columns[lastColumnIndex];
                //GV_List.Columns.RemoveAt(lastColumnIndex);
                GV_List.Columns.RemoveAt(0);

            }

            this.ExportXLS(GV_List, "PlanBill" + dateTime + ".xls");

            if (!IsSupplier)
            {
                GV_List.Columns.Insert(0, dataControlField0);
                //GV_List.Columns.Insert(lastColumnIndex-1, dataControlFieldLast);
            }
        }

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
        if (IsSupplier)
        {
            this.GV_List.Columns[0].Visible = false;  //选择
            this.GV_List.Columns[10].Visible = false; //单价
            this.GV_List.Columns[11].Visible = false; //币种
            this.GV_List.Columns[13].Visible = false; //待结额
            this.GV_List.Columns[15].Visible = false; //已结额
            this.GV_List.Columns[16].Visible = false; //结算数
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox tbCurrentActingQty = (TextBox)e.Row.FindControl("tbCurrentActingQty");
            Label lbActingQty = (Label)e.Row.FindControl("lbActingQty");
            Label lbPlannedQty = (Label)e.Row.FindControl("lbPlannedQty");
            decimal plannedQty = decimal.Parse(lbPlannedQty.Text.Trim());
            decimal actingQty = lbActingQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(lbActingQty.Text.Trim());
            tbCurrentActingQty.Text = (plannedQty - actingQty).ToString();
        }
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
}
