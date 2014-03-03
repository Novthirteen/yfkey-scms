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
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class Inventory_InspectOrder_NewQty : ModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;


    private IDictionary<string, decimal> InspectItemDic
    {
        get
        {
            return (IDictionary<string, decimal>)ViewState["InspectItemDic"];
        }
        set
        {
            ViewState["InspectItemDic"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbLocation.ServiceParameter = "string:" + this.CurrentUser.Code;
        if (!IsPostBack)
        {
            InspectItemDic = new Dictionary<string, decimal>();
        }
    }


    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        UpdateInspectItemDic();

        bool hasDetail = false;
        if (this.InspectItemDic.Count == 0)
        {

        }
        else
        {
            foreach (string itemCode in this.InspectItemDic.Keys)
            {
                if (InspectItemDic[itemCode] != 0)
                {
                    hasDetail = true;
                    break;
                }
            }
        }
        if (!hasDetail)
        {
            ShowErrorMessage("MasterData.InspectOrder.Detail.Empty");
            return;
        }

        try
        {
            InspectOrder inspectOrder = TheInspectOrderMgr.CreateInspectOrder(this.tbLocation.Text.Trim(), this.InspectItemDic, this.CurrentUser);
            ShowSuccessMessage("MasterData.InspectOrder.Create.Successfully", inspectOrder.InspectNo);
            if (CreateEvent != null)
            {
                CreateEvent(inspectOrder.InspectNo, e);
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(sender, e);
        }

    }

    protected void tbItemCode_TextChanged(object sender, EventArgs e)
    {
        string itemCode = ((TextBox)sender).Text.Trim();
        GridViewRow row = (GridViewRow)((TextBox)sender).BindingContainer;
        TextBox tbInspectQty = (TextBox)row.FindControl("tbInspectQty");
        decimal inspectQty = tbInspectQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbInspectQty.Text.Trim());
        if (itemCode != string.Empty)
        {
            Item item = TheItemMgr.LoadItem(itemCode);
            if (item != null)
            {
                if (InspectItemDic.ContainsKey(itemCode))
                {
                    ShowErrorMessage("MasterData.Production.Feed.Item.Exists", itemCode);
                    return;
                }
                InspectItemDic.Add(itemCode, inspectQty);
                UpdateInspectItemDic();
                this.InitPageParameter();
            }
        }

    }
    public void UpdateView()
    {
        this.tbLocation.Text = string.Empty;
        this.InitPageParameter();
    }

    public void InitPageParameter()
    {
       
        IList<InspectItem> inspectItemList = new List<InspectItem>();
        foreach (string itemCode in this.InspectItemDic.Keys)
        {
            InspectItem inspectItem = new InspectItem();
            inspectItem.IsBlank = false;
            inspectItem.InspectQty = InspectItemDic[itemCode];
            inspectItem.Item = TheItemMgr.LoadItem(itemCode);
            inspectItemList.Add(inspectItem);
        }

        //新行
        InspectItem blankInspectItem = new InspectItem();
        blankInspectItem.IsBlank = true;
        inspectItemList.Add(blankInspectItem);

        this.GV_List.DataSource = inspectItemList;
        this.GV_List.DataBind();

    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            InspectItem inspectItem = (InspectItem)e.Row.DataItem;

            if (inspectItem.IsBlank)
            {
                e.Row.FindControl("lblItemCode").Visible = false;
                e.Row.FindControl("tbItemCode").Visible = true;
            }
            else
            {
                e.Row.FindControl("lblItemCode").Visible = true;
                e.Row.FindControl("tbItemCode").Visible = false;
            }

        }
    }

    private void UpdateInspectItemDic()
    {
        for (int i = 0; i < this.GV_List.Rows.Count - 1; i++)
        {
            GridViewRow row = this.GV_List.Rows[i];
            string itemCode = ((Label)row.FindControl("lblItemCode")).Text.Trim();
            TextBox tbInspectQty = (TextBox)row.FindControl("tbInspectQty");
            decimal inspectQty = tbInspectQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbInspectQty.Text.Trim());
            InspectItemDic[itemCode] = inspectQty;
        }
    }

}
