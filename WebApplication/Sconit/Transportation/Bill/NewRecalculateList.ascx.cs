using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity.Transportation;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;

public partial class Transportation_Bill_NewRecalculateList : ListModuleBase
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
        this.GV_List.DataSource = transportationActBillList;
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
                    TransportationActBill transportationActBill = TheTransportationActBillMgr.LoadTransportationActBill(int.Parse(hfId.Value));
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
        if (this.GV_List.DataSource != null)
        {
            this.lblNoRecordFound.Visible = false;
            this.GV_List.DataBind();
        }
        else
        {
            this.GV_List.Visible = false;
            this.lblNoRecordFound.Visible = true;
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
}
