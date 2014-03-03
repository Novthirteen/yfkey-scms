using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;

public partial class Finance_Bill_NewRecalculateList : ListModuleBase
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

    public void BindDataSource(IList<ActingBill> actingBillList)
    {
        this.GV_List.DataSource = actingBillList;
        this.UpdateView();
    }

    public IList<ActingBill> PopulateSelectedData()
    {
        if (this.GV_List.Rows != null && this.GV_List.Rows.Count > 0)
        {
            IList<ActingBill> actingBillList = new List<ActingBill>();
            foreach (GridViewRow row in this.GV_List.Rows)
            {

                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    HiddenField hfId = row.FindControl("hfId") as HiddenField;
                    ActingBill actingBill = TheActingBillMgr.LoadActingBill(int.Parse(hfId.Value));
                    actingBillList.Add(actingBill);
                }
            }
            return actingBillList;
        }

        return null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.ModuleType == BusinessConstants.BILL_TRANS_TYPE_PO)
            {
                this.GV_List.Columns[3].Visible = false;
            }
            else if (this.ModuleType == BusinessConstants.BILL_TRANS_TYPE_SO)
            {
                this.GV_List.Columns[1].HeaderText = "${MasterData.ActingBill.Customer}";
                this.GV_List.Columns[2].Visible = false;
            }

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
