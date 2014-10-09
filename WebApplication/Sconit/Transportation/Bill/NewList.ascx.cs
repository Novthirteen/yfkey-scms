using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity.Transportation;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;

public partial class Transportation_Bill_NewList : ListModuleBase
{
    private int DecimalLength
    {
        get
        {
            return (int)ViewState["DecimalLength"];
        }
        set
        {
            ViewState["DecimalLength"] = value;
        }
    }

    public void BindDataSource(IList<TransportationActBill> transportationActBillList)
    {
        if (transportationActBillList != null)
        {
            var q = from a in transportationActBillList
                    orderby a.ExternalReceiptNo
                    select a;

            this.GV_List.DataSource = q.ToList<TransportationActBill>();
        }
        else
        {
            this.GV_List.DataSource = null;
        }

        this.UpdateView();
    }

    public IList<TransportationActBill> PopulateSelectedData()
    {
        if (this.GV_List.Rows != null && this.GV_List.Rows.Count > 0)
        {
            IList<TransportationActBill> transportationActBillList = new List<TransportationActBill>();
            foreach (GridViewRow row in this.GV_List.Rows)
            {
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    HiddenField hfId = row.FindControl("hfId") as HiddenField;
                    TextBox tbQty = row.FindControl("tbQty") as TextBox;
                    TextBox tbAmount = row.FindControl("tbAmount") as TextBox;
                    TextBox tbDiscount = row.FindControl("tbDiscount") as TextBox;

                    TransportationActBill transportationActBill = new TransportationActBill();
                    transportationActBill.Id = int.Parse(hfId.Value);
                    transportationActBill.CurrentBillQty = decimal.Parse(tbQty.Text);
                    transportationActBill.CurrentBillAmount = decimal.Parse(tbAmount.Text);
                    transportationActBill.CurrentDiscount = decimal.Parse(tbDiscount.Text);

                    transportationActBillList.Add(transportationActBill);
                }
            }
            return transportationActBillList;
        }

        return null;
    }

   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EntityPreference entityPreference = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_AMOUNT_DECIMAL_LENGTH);
            DecimalLength = int.Parse(entityPreference.Value);
        }
    }

    public override void UpdateView()
    {
        this.GV_List.DataBind();
        if (this.GV_List.DataSource != null)
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TransportationActBill transportationActBill = (TransportationActBill)e.Row.DataItem;

            decimal billAmount = transportationActBill.BillAmount;
            decimal unitPrice = transportationActBill.UnitPrice;

            decimal remailQty = transportationActBill.BillQty - transportationActBill.BilledQty;
            decimal remailAmount = transportationActBill.BillAmount - transportationActBill.BilledAmount;
            //decimal discount = unitPrice * remailQty - remailAmount;

            TextBox tbQty = e.Row.FindControl("tbQty") as TextBox;
            //TextBox tbDiscountRate = e.Row.FindControl("tbDiscountRate") as TextBox;
            //TextBox tbDiscount = e.Row.FindControl("tbDiscount") as TextBox;
            TextBox tbAmount = e.Row.FindControl("tbAmount") as TextBox;

            /*
             * 
             * 1.TransType=Transportation 价格单明细（承运商） 或  短拨费（区域）时
             * a.PricingMethod=M3或KG  按数量
             * b.SHIPT   按金额
             * 2.TransType=WarehouseLease(固定费用) 按金额
             * 3.TransType=Operation(操作费) 按数量
             */
            if (transportationActBill.TransType == BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_OPERATION
                ||
                (transportationActBill.TransType == BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_TRANSPORTATION
                && (transportationActBill.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_M3 || transportationActBill.PricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_KG)
                )
                )
            {
                tbQty.Attributes["onchange"] = "CalCulateRowAmount(this, 'tbQty', 'BaseOnDiscountRate', 'hfUnitPrice', 'tbQty', 'tbDiscount', 'tbDiscountRate', 'tbAmount',false);";
                tbQty.Attributes["onmouseup"] = "if(!readOnly)select();";
                tbAmount.Attributes["onfocus"] = "this.blur();";

                tbQty.Text = remailQty.ToString();
            }
            else
            {
                tbQty.Attributes["onfocus"] = "this.blur();";
                tbQty.Text = "1";
                tbAmount.Attributes["onmouseup"] = "if(!readOnly)select();";
            }
            //if (unitPrice != 0 && remailQty != 0)
            //{
            //    tbDiscountRate.Text = (Math.Round(discount / (unitPrice * remailQty), this.DecimalLength, MidpointRounding.AwayFromZero) * 100).ToString("F2");
            //}
            //tbDiscount.Text = discount.ToString("F2");
            tbAmount.Text = (Math.Floor(remailAmount * 100) / 100).ToString("F2");
            tbAmount.Attributes["oldValue"] = tbAmount.Text;
        }
    }
}
