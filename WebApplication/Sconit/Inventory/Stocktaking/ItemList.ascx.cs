using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity;

public partial class Inventory_Stocktaking_ItemList : ModuleBase
{

    protected IList<Item> ItemList
    {
        get
        {
            return (IList<Item>)ViewState["ItemList"];
        }
        set
        {
            ViewState["ItemList"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Item item = (Item)e.Row.DataItem;
            Label lblItemCode = (Label)(e.Row.FindControl("lblItemCode"));
            Controls_TextBox tbItemCode = (Controls_TextBox)(e.Row.FindControl("tbItemCode"));
            if (item.IsBlank)
            {
                lblItemCode.Visible = false;
                tbItemCode.Visible = true;
            }
            e.Row.FindControl("lbtnAdd").Visible = item.IsBlank;
            e.Row.FindControl("lbtnDelete").Visible = !item.IsBlank;
        }
    }

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)sender).Parent.Parent);

        string code = ((Controls_TextBox)row.FindControl("tbItemCode")).Text.Trim();
        if (code == string.Empty)
        {
            ShowErrorMessage("MasterData.Order.OrderDetail.ItemCode.Required");
            return;
        }
        var count = ItemList.Where(i => i.Code == code).Count();
        if (count > 0)
        {
            ShowErrorMessage("Common.Business.Error.EntityExist", code);
            return;
        }

        Item item = TheItemMgr.LoadItem(code);
        this.ItemList.Add(item);
        BindData(true);
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string code = ((LinkButton)sender).CommandArgument;
        foreach (Item item in this.ItemList)
        {
            if (item.Code == code)
            {
                this.ItemList.Remove(item);
                break;
            }
        }

        BindData(true);

    }

    public void InitPageParameter(CycleCount cycleCount)
    {

        this.ItemList = new List<Item>();

        if (cycleCount.Items != null && cycleCount.Items != string.Empty)
        {
            string[] itemArr = cycleCount.Items.Split('|');
            foreach (string item in itemArr)
            {
                Item oldItem = TheItemMgr.LoadItem(item);
                this.ItemList.Add(oldItem);

            }
        }
        this.GV_List.Columns[4].Visible = cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
        BindData(cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE);
    }

    private void BindData(bool includeBlank)
    {
        IList<Item> itemList = new List<Item>();
        IListHelper.AddRange<Item>(itemList, this.ItemList);
        if (includeBlank)
        {
            Item newItem = new Item();
            newItem.IsBlank = true;
            itemList.Add(newItem);
        }
        
        this.GV_List.DataSource = itemList;
        this.GV_List.DataBind();
    }

    public string GetItems()
    {
        string items = string.Empty;
        foreach (Item item in this.ItemList)
        {
            if (items == string.Empty)
            {
                items += item.Code;
            }
            else
            {
                items += "|" + item.Code;
            }

        }
        return items;
    }

}
