using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.View;
using System.Text;
using com.Sconit.Entity.MRP;
using System.Reflection;

public partial class MRP_Schedule_CustomerScheduleReport_Main : MainModuleBase
{
    public event EventHandler lbRunMrpClickEvent;
    private int seq = 1;
    private bool isExport = false;
    private IList<ItemReference> itemRefs;

    private DateTime StartDate
    {
        get { return this.tbStartDate.Text.Trim() == string.Empty ? DateTime.Today : DateTime.Parse(this.tbStartDate.Text.Trim()); }
    }

    private string PartyCode
    {
        get { return (string)ViewState["PartyCode"]; }
        set { ViewState["PartyCode"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:true,bool:true,bool:false,bool:false,bool:false,string:"
            + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.isExport = true;
        this.btnSearch_Click(null, null);
       // this.ExportXLS(this.GV_List);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string flowCode = this.tbFlow.Text.Trim();
        if (flowCode == string.Empty)
        {
            ShowErrorMessage("Common.Business.Error.Require.Flow");
            return;
        }
        Flow flow = TheFlowMgr.LoadFlow(flowCode);
        if (flow.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            this.PartyCode = flow.PartyTo.Code;
        }
        else
        {
            this.PartyCode = flow.PartyFrom.Code;
        }
        IList<CustomerScheduleDetail> customerScheduleDetails = TheCustomerScheduleDetailMgr.GetCustomerScheduleDetails(flowCode, this.StartDate);
        ScheduleView scheduleView = TheCustomerScheduleDetailMgr.TransferCustomerScheduleDetails2ScheduleView(customerScheduleDetails, this.StartDate);

        itemRefs = TheItemReferenceMgr.GetAllItemReference();

        this.GV_List_DataBind(scheduleView);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 6; i < this.GV_List.Columns.Count; i++)
            {
                e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("*", "<br/>");
            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((Label)e.Row.FindControl("lblSequence")).Text = (e.Row.RowIndex + 1).ToString();
            string itemCode = ((Label)e.Row.FindControl("lblItemCode")).Text;
            seq++;
            e.Row.Cells[2].Text = this.TheItemReferenceMgr.GetItemReferenceByItem(itemCode, this.PartyCode, null);
            //e.Row.Cells[2].Text = this.GetItemReference(itemCode, this.PartyCode, null);

            if (isExport)
            {
                e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                e.Row.Cells[2].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                e.Row.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            }
        }
    }

    private void GV_List_DataBind(ScheduleView scheduleView)
    {
        this.fld_Group.Visible = true;
        IList<ScheduleBody> scheduleBodys = scheduleView.ScheduleBodys;
        IList<ScheduleHead> scheduleHeads = scheduleView.ScheduleHeads;

        if (scheduleHeads != null && scheduleHeads.Count > 0)
        {
            int i = 0;
            foreach (ScheduleHead scheduleHead in scheduleHeads)
            {
                string qty = "Qty" + i.ToString();

                PropertyInfo[] scheduleBodyPropertyInfo = typeof(ScheduleBody).GetProperties();
                foreach (PropertyInfo pi in scheduleBodyPropertyInfo)
                {
                    if (pi.Name != null && StringHelper.Eq(pi.Name.ToLower(), qty))
                    {
                        BoundField bfColumn = new BoundField();
                        bfColumn.DataField = qty;
                        bfColumn.DataFormatString = "{0:0.##}";
                        bfColumn.HeaderText = scheduleHead.DateHead;
                        this.GV_List.Columns.Add(bfColumn);
                        break;
                    }
                }
                i++;
            }
            this.ltl_GV_List_Result.Visible = false;
        }
        else
        {
            this.ltl_GV_List_Result.Visible = true;
        }

        this.GV_List.DataSource = scheduleBodys;
        this.GV_List.DataBind();
    }

    private string GetItemReference(string itemCode, string partyCode1, string partyCode2)
    {
        var q = itemRefs.Where(i => i.Item.Code == itemCode && i.Party != null);
        if (q.Count() == 1)
        {
            return q.First().ReferenceCode;
        }
        else if (q.Count() > 1)
        {
            var q1 = q.Where(i => i.Party.Code == partyCode1);
            if (q.Count() > 0)
            {
                return q1.First().ReferenceCode;
            }
            else
            {
                var q2 = q.Where(i => i.Party.Code == partyCode2);
                if (q2.Count() > 0)
                {
                    return q2.First().ReferenceCode;
                }
            }
        }
        q = itemRefs.Where(i => i.Item.Code == itemCode && i.Party == null);
        if (q.Count() > 0)
        {
            return q.First().ReferenceCode;
        }
        return null;
    }
}

