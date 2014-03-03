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
using com.Sconit.Utility;
using NHibernate.Transform;
using System.Collections;

public partial class Finance_PlanBill_SO_Match : ModuleBase
{
    public event EventHandler Saved;

    private List<Transformer> MatchList
    {
        get { return ViewState["MatchList"] != null ? (List<Transformer>)ViewState["MatchList"] : new List<Transformer>(); }
        set { ViewState["MatchList"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(DetachedCriteria criteria)
    {
        IList<PlannedBill> plannedBillList = TheCriteriaMgr.FindAll<PlannedBill>(criteria);
        this.MatchList = this.ConvertPlannedBillToTransformer(plannedBillList);
        this.BindList();
        this.tbItemCode.Focus();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.Match();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.Visible = false;
        if (Saved != null)
        {
            IList<PlannedBill> plannedBillList = this.ConvertTransformerToPlannedBill(this.GetList());
            Saved(plannedBillList, null);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        int Id = this.GetList()[rowIndex].Id;
        foreach (Transformer transformer in MatchList)
        {
            if (transformer.Id == Id)
            {
                transformer.CurrentQty = 0;
                break;
            }
        }
        this.BindList();
    }

    private List<Transformer> GetList()
    {
        List<Transformer> transformerList = new List<Transformer>();
        foreach (Transformer transformer in MatchList)
        {
            if (transformer.CurrentQty > 0)
                transformerList.Add(transformer);
        }

        return transformerList;
    }

    private void BindList()
    {
        List<Transformer> transformerList = this.GetList();
        this.GV_List.DataSource = transformerList;
        this.GV_List.DataBind();
    }

    private bool CheckExist(string itemCode)
    {
        foreach (Transformer transformer in MatchList)
        {
            if (transformer.ItemCode.Trim().ToUpper() == itemCode.Trim().ToUpper())
                return true;
        }
        return false;
    }

    private void Match()
    {
        if (this.tbItemCode.Text.Trim() == string.Empty)
        {
            this.tbItemCode.Focus();
            return;
        }
        string itemCode = this.tbItemCode.Text.Trim();

        if (this.tbQty.Text.Trim() == string.Empty)
        {
            if (!this.CheckExist(itemCode))
            {
                ShowErrorMessage("MasterData.PlannedBill.Match.Error.ItemNotExist", itemCode);
                this.tbItemCode.Text = string.Empty;
                this.tbItemCode.Focus();
            }
            else
            {
                this.tbQty.Focus();
            }
            return;
        }
        decimal qty = decimal.Parse(this.tbQty.Text);
        decimal inputQty = qty;
        int count = 0;

        foreach (Transformer transformer in MatchList)
        {
            if (transformer.ItemCode.Trim().ToUpper() == itemCode.Trim().ToUpper() && transformer.Qty > transformer.CurrentQty)
            {
                decimal actingQty = transformer.Qty - transformer.CurrentQty;
                decimal matchQty = actingQty < qty ? actingQty : qty;
                transformer.CurrentQty += matchQty;

                count++;
                qty -= matchQty;

                if (qty <= 0)
                    break;
            }
        }

        this.BindList();
        if (count == 0)
        {
            ShowErrorMessage("MasterData.PlannedBill.Match.Error", itemCode, inputQty.ToString("0.########"));
        }
        else if (qty > 0)
        {
            ShowWarningMessage("MasterData.PlannedBill.Match.Warning", itemCode, inputQty.ToString("0.########"), count.ToString(), qty.ToString("0.########"));
        }
        else
        {
            ShowSuccessMessage("MasterData.PlannedBill.Match.Successfully", itemCode, inputQty.ToString("0.########"), count.ToString());
        }

        this.tbItemCode.Text = string.Empty;
        this.tbQty.Text = string.Empty;
        this.tbItemCode.Focus();
    }

    private List<Transformer> ConvertPlannedBillToTransformer(IList<PlannedBill> plannedBillList)
    {
        if (plannedBillList == null || plannedBillList.Count == 0)
            return null;

        List<Transformer> transformerList = new List<Transformer>();
        foreach (PlannedBill plannedBill in plannedBillList)
        {
            Transformer transformer = new Transformer();
            transformer.Id = plannedBill.Id;
            transformer.OrderNo = plannedBill.ExternalReceiptNo;
            transformer.ItemCode = plannedBill.Item.Code;
            transformer.ItemDescription = plannedBill.Item.Description;
            transformer.UomCode = plannedBill.Uom.Code;
            decimal actingQty = plannedBill.ActingQty.HasValue ? plannedBill.ActingQty.Value : 0;
            transformer.Qty = plannedBill.PlannedQty - actingQty;

            transformerList.Add(transformer);
        }

        return transformerList;
    }

    private IList<PlannedBill> ConvertTransformerToPlannedBill(List<Transformer> transformerList)
    {
        if (transformerList == null || transformerList.Count == 0)
            return null;

        IList<PlannedBill> plannedBillList = new List<PlannedBill>();
        foreach (Transformer transformer in transformerList)
        {
            PlannedBill plannedBill = ThePlannedBillMgr.LoadPlannedBill(transformer.Id);
            plannedBill.CurrentActingQty = transformer.CurrentQty;

            plannedBillList.Add(plannedBill);
        }

        return plannedBillList;
    }

}
