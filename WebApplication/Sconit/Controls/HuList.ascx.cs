using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;

public partial class Controls_HuList : ModuleBase
{
    public event EventHandler HuInput;
    //public event EventHandler RowDeleting;
    public event EventHandler Closed;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void BindList(List<TransformerDetail> transformerDetailList, bool addNewRow)
    {
        List<TransformerDetail> list = CloneHelper.DeepClone<List<TransformerDetail>>(transformerDetailList);

        if (list == null)
            list = new List<TransformerDetail>();

        if (addNewRow)
        {
            //增加新行
            list.Add(new TransformerDetail());
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
        if (Closed != null)
        {
            Closed(null, null);
        }
    }

    protected void tbHuId_TextChanged(object sender, EventArgs e)
    {
        this.lblMessage.Text = string.Empty;

        TextBox _editControl = (TextBox)sender;
        string input = _editControl.Text.Trim();

        if (HuInput != null)
        {
            HuInput(input, null);
        }
    }

    //protected void lbtnDelete_Click(object sender, EventArgs e)
    //{
    //    int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
    //    if (RowDeleting != null)
    //    {
    //        RowDeleting(rowIndex, null);
    //    }
    //}

    private void InitialInput()
    {
        int _rowIndex = GV_List.Rows.Count - 1;
        Label _displayControl = (Label)this.GV_List.Rows[_rowIndex].FindControl("lblHuId");
        TextBox _editControl = (TextBox)this.GV_List.Rows[_rowIndex].FindControl("tbHuId");
        //LinkButton _linkButton = (LinkButton)this.GV_List.Rows[_rowIndex].FindControl("lbtnDeleteHu");
        //_linkButton.Visible = false;
        _displayControl.Visible = false;
        _editControl.Visible = true;
        _editControl.Text = string.Empty;
        _editControl.Focus();
    }

}
