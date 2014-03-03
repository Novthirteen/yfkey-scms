using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Entity.Transportation;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;

public partial class Transportation_Bill_NewBatchSearch : ModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageCleanUp();

        }
        tbPartyCode.ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string partyCode = this.tbPartyCode.Text != string.Empty ? this.tbPartyCode.Text.Trim() : string.Empty;
        string expenseNo = this.tbExpenseNo.Text != string.Empty ? this.tbExpenseNo.Text.Trim() : string.Empty;
        string startDate = this.tbStartDate.Text != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
        string endDate = this.tbEndDate.Text != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;
        string itemCode = this.tbItemCode.Text != string.Empty ? this.tbItemCode.Text.Trim() : string.Empty;
        string currency = this.tbCurrency.Text != string.Empty ? this.tbCurrency.Text.Trim() : string.Empty;

        DateTime? effDateFrom = null;
        if (startDate != string.Empty)
        {
            effDateFrom = DateTime.Parse(startDate);
        }

        DateTime? effDateTo = null;
        if (endDate != string.Empty)
        {
            effDateTo = DateTime.Parse(endDate).AddDays(1).AddMilliseconds(-1);
        }

        //重新计价
        bool needRecalculate = bool.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_RECALCULATE_WHEN_TRANSPORTATIONBILL).Value);
        if (needRecalculate)
        {
            IList<TransportationActBill> allTransportationActBillList = TheTransportationActBillMgr.GetTransportationActBill(partyCode, expenseNo, effDateFrom, effDateTo, itemCode, currency, null, true);

            TheTransportationActBillMgr.RecalculatePrice(allTransportationActBillList, this.CurrentUser);
        }

        IList<TransportationActBill> transportationActBillList = TheTransportationActBillMgr.GetTransportationActBill(partyCode, expenseNo, effDateFrom, effDateTo, itemCode, currency, null);

        if (transportationActBillList != null && transportationActBillList.Count > 0)
        {

            foreach (TransportationActBill transportationActBill in transportationActBillList)
            {
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
                    transportationActBill.CurrentBillQty = transportationActBill.BillQty - transportationActBill.BilledQty;
                    decimal orgAmount = transportationActBill.UnitPrice * transportationActBill.CurrentBillQty;
                    transportationActBill.CurrentDiscount = orgAmount - (transportationActBill.BillAmount - transportationActBill.BilledAmount);
                }
                else
                {
                    transportationActBill.CurrentBillAmount = transportationActBill.BillAmount - transportationActBill.BilledAmount;
                    transportationActBill.CurrentDiscount = 0;
                }
            }

            IList<TransportationBill> transportationBillList = this.TheTransportationBillMgr.CreateTransportationBill(transportationActBillList, this.CurrentUser, (this.IsRelease.Checked ? BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT : BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
            ;

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(TransportationBill));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(TransportationBill))
                .SetProjection(Projections.Count("BillNo"));

            selectCriteria.Add(Expression.Eq("CreateDate", transportationBillList[0].CreateDate));
            selectCriteria.Add(Expression.Eq("CreateUser.Code", this.CurrentUser.Code));
            selectCountCriteria.Add(Expression.Eq("CreateDate", transportationBillList[0].CreateDate));
            selectCountCriteria.Add(Expression.Eq("CreateUser.Code", this.CurrentUser.Code));

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);

            this.ShowSuccessMessage("Transportation.TransportationBill.BatchCreateSuccessfully");
        }
        else
        {
            this.ShowErrorMessage("TransportationBill.Error.EmptyBillDetail");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        BackEvent(this, null);
        this.PageCleanUp();
    }

    private void PageCleanUp()
    {
        this.tbStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
        this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        this.tbPartyCode.Text = string.Empty;
        this.tbExpenseNo.Text = string.Empty;
        this.tbItemCode.Text = string.Empty;
        this.tbCurrency.Text = string.Empty;
    }
}
