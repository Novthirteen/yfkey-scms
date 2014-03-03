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
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;

public partial class MasterData_PriceList_PriceList_List : ListModuleBase
{
    public EventHandler EditEvent;
    public string PriceListType
    {
        get
        {
            return (string)ViewState["PriceListType"];
        }
        set
        {
            ViewState["PriceListType"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_SALES)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Text = "${MasterData.Customer.Code}";
                e.Row.Cells[3].Text = "${MasterData.Customer.Name}";
            }
        }
        else if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_PURCHASE)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Text = "${MasterData.Supplier.Code}";
                e.Row.Cells[3].Text = "${MasterData.Supplier.Name}";
            }
        }

    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string code = ((LinkButton)sender).CommandArgument;
            EditEvent(code, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string code = ((LinkButton)sender).CommandArgument;
        try
        {
            ThePriceListMgr.DeletePriceList(code);
            ShowSuccessMessage("MasterData.PriceList.Delete.Successfully", code);
            UpdateView();
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.PriceList.Delete.Failed", code);
        }
    }
}
