using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;

public partial class Finance_Bill_List : ListModuleBase
{
    public EventHandler EditEvent;
    public EventHandler ViewEvent;

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
        if (!IsPostBack)
        {
            if (ModuleType == BusinessConstants.BILL_TRANS_TYPE_SO)
            {
                this.GV_List.Columns[2].HeaderText = "${MasterData.Bill.Customer}";
            }
        }
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Bill bill = (Bill)e.Row.DataItem;
            if (bill.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                com.Sconit.Control.LinkButton lbtnDelete = e.Row.FindControl("lbtnDelete") as com.Sconit.Control.LinkButton;
                lbtnDelete.Visible = false;
            }
        }
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string billNo = ((com.Sconit.Control.LinkButton)sender).CommandArgument;
            EditEvent(billNo, null);
        }
    }

    protected void lbtnView_Click(object sender, EventArgs e)
    {
        if (ViewEvent != null)
        {
            string billNo = ((com.Sconit.Control.LinkButton)sender).CommandArgument;
            ViewEvent(billNo, null);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string billNo = ((com.Sconit.Control.LinkButton)sender).CommandArgument;
        try
        {
            TheBillMgr.DeleteBill(billNo, this.CurrentUser);
            ShowSuccessMessage("MasterData.Bill.DeleteSuccessfully", billNo);
            UpdateView();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }
}
