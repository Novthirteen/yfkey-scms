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
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Web;
using com.Sconit.Entity.Exception;

public partial class Hu_List : EditModuleBase
{
    public event EventHandler HuSaveEvent;

    protected List<string> HuIdList
    {
        get
        {
            return (List<string>)ViewState["HuIdList"];
        }
        set
        {
            ViewState["HuIdList"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter()
    {
        this.lblMessage.Text = string.Empty;
        IList<Hu> _huList = new List<Hu>();

        InitialHuIdInput(_huList);
    }

    protected void btn_Hu_Back_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }

    protected void btn_Hu_Create_Click(object sender, EventArgs e)
    {
        if (this.HuSaveEvent != null)
        {
            this.HuSaveEvent(new object[] { this.GetHuList() }, null);
        }
        this.Visible = false;
    }

    protected void tbHuId_TextChanged(object sender, EventArgs e)
    {
        this.lblMessage.Text = string.Empty;

        TextBox _editControl = (TextBox)sender;
        string huId = _editControl.Text.Trim().ToUpper();
        IList<Hu> huList = this.GetHuList();
        Hu hu = TheHuMgr.LoadHu(huId);

        if (hu != null && huList.IndexOf(hu) < 0)
        {
            huList.Add(hu);
        }
        else
        {
            this.lblMessage.Text = Resources.Language.MasterDataHuNotExist + " (" + huId + ")";
        }

        this.InitialHuIdInput(huList);
    }

    private void InitialHuIdInput(IList<Hu> huList)
    {
        //增加新行
        huList.Add(new Hu());
        this.GV_List.DataSource = huList;
        this.GV_List.DataBind();

        int _rowIndex = huList.Count - 1;
        Label _displayControl = (Label)this.GV_List.Rows[_rowIndex].FindControl("lblHuId");
        TextBox _editControl = (TextBox)this.GV_List.Rows[_rowIndex].FindControl("tbHuId");
        LinkButton _linkButton = (LinkButton)this.GV_List.Rows[_rowIndex].FindControl("lbtnDeleteHu");
        _linkButton.Visible = false;
        _displayControl.Visible = false;
        _editControl.Visible = true;
        _editControl.Focus();
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        IList<Hu> huList = this.GetHuList();
        int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        huList.RemoveAt(rowIndex);

        InitialHuIdInput(huList);
    }

    private IList<Hu> GetHuList()
    {
        IList<Hu> huList = new List<Hu>();
        foreach (GridViewRow gvr in GV_List.Rows)
        {
            Label _displayControl = (Label)gvr.FindControl("lblHuId");

            string huId = string.Empty;
            if (_displayControl.Visible)
            {
                huId = _displayControl.Text.Trim();
                Hu hu = TheHuMgr.LoadHu(huId);
                if (hu != null)
                {
                    huList.Add(hu);
                }
            }
        }

        return huList;
    }

}
