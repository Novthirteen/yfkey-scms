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
using com.Sconit.Utility;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;

public partial class Order_GoodsReceipt_OrderReceipt_List : ListModuleBase
{
    public EventHandler EditEvent;

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

    public override void UpdateView()
    {
        this.GV_List.Columns[1].HeaderText = OrderHelper.GetOrderLabel(this.ModuleType);
        this.GV_List.Columns[2].HeaderText = OrderHelper.GetOrderPartyFromLabel(this.ModuleType);
        this.GV_List.Columns[3].HeaderText = OrderHelper.GetOrderPartyToLabel(this.ModuleType);
        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            this.GV_List.Columns[3].Visible = false;

        }
        this.GV_List.Execute();

      
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string orderNo = ((LinkButton)sender).CommandArgument;
            EditEvent(orderNo, e);
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
            HiddenField hfModuleSubType = (HiddenField)e.Row.FindControl("hfModuleSubType");
            if (hfModuleSubType.Value == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN)
            {
                lbtnEdit.Text = "${Common.Button.Return}";
            }
        }
    }
}
