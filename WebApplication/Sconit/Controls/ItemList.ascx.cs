using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;

public partial class Controls_ItemList : ModuleBase
{
    public event EventHandler ItemInput;
    public event EventHandler QtyChanged;

    public bool EditMode
    {
        get { return (bool)ViewState["EditMode"]; }
        set { ViewState["EditMode"] = value; }
    }

    private Label GetItemCodeLabel(GridViewRow gvr)
    {
        return (Label)gvr.FindControl("lblItemCode");
    }
    private TextBox GetItemCodeTextBox(GridViewRow gvr)
    {
        return (TextBox)gvr.FindControl("tbItemCode");
    }
    private TextBox GetCurrentQtyTextBox(GridViewRow gvr)
    {
        return (TextBox)gvr.FindControl("tbCurrentQty");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void BindList(List<Transformer> transformerList, bool addNewRow)
    {
        List<Transformer> list = CloneHelper.DeepClone<List<Transformer>>(transformerList);

        if (list == null)
            list = new List<Transformer>();

        if (addNewRow)
        {
            //增加新行
            list.Add(new Transformer());
        }
        this.GV_List.DataSource = list;
        this.GV_List.DataBind();

        if (addNewRow)
        {
            this.InitialInput();
        }
    }

    public void ShowResolverMessage(string message)
    {
        this.lblMessage.Text = message;
        this.InitialInput();
    }

    public void ItemInputCallBack(List<Transformer> transformerList)
    {
        this.BindList(transformerList, false);
        this.GetCurrentQtyTextBox(GV_List.Rows[GV_List.Rows.Count - 1]).Focus();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Transformer transformer = (Transformer)e.Row.DataItem;
            bool includeDetail = (transformer.TransformerDetails != null && transformer.TransformerDetails.Count > 0);
            GetCurrentQtyTextBox(e.Row).ReadOnly = (!EditMode) || includeDetail;
            //GetCurrentQtyTextBox(e.Row).Text = transformer.CurrentQty != 0 ? transformer.CurrentQty.ToString("0.########") : string.Empty;
        }
    }

    protected void tbItemCode_TextChanged(object sender, EventArgs e)
    {
        this.lblMessage.Text = string.Empty;

        TextBox _editControl = (TextBox)sender;
        string input = _editControl.Text.Trim();

        if (ItemInput != null)
        {
            ItemInput(input, null);
        }
    }

    protected void tbCurrentQty_TextChanged(object sender, EventArgs e)
    {
        this.lblMessage.Text = string.Empty;

        TextBox _editControl = (TextBox)sender;
        int rowIndex = ((GridViewRow)(_editControl.NamingContainer)).RowIndex;
        string input = _editControl.Text.Trim();

        if (input != string.Empty)
        {
            decimal qty = decimal.Parse(input);

            if (QtyChanged != null)
            {
                QtyChanged(new object[] { qty, rowIndex }, null);
            }
        }
    }

    private void InitialInput()
    {
        int _rowIndex = GV_List.Rows.Count - 1;
        Label _displayControl = GetItemCodeLabel(GV_List.Rows[_rowIndex]);
        TextBox _editControl = GetItemCodeTextBox(GV_List.Rows[_rowIndex]);
        _displayControl.Visible = false;
        _editControl.Visible = true;
        _editControl.Text = string.Empty;
        _editControl.Focus();
    }
}
