using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;

public partial class Hu_HuInput : ModuleBase
{
    public event EventHandler QtyChangeEvent;

    public bool ReadOnly
    {
        get { return ViewState["ReadOnly"] == null ? true : (bool)ViewState["ReadOnly"]; }
        set { ViewState["ReadOnly"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void HuInput(Hu newHu)
    {
        IList<Hu> huList = this.GetHuList();
        huList.Add(newHu);

        this.GV_List.DataSource = huList;
        this.GV_List.DataBind();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox tbQty = (TextBox)e.Row.FindControl("tbQty");
            tbQty.ReadOnly = this.ReadOnly;
            //tbQty.ReadOnly = true;
        }
    }

    protected void tbQty_TextChanged(object sender, EventArgs e)
    {
        if (QtyChangeEvent != null)
        {
            QtyChangeEvent(sender, null);
        }
    }

    public bool CheckExist(string huId)
    {
        if (GV_List.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in GV_List.Rows)
            {
                Label lblHuId = (Label)gvr.FindControl("lblHuId");
                if (lblHuId.Text.Trim().ToUpper() == huId.Trim().ToUpper())
                    return true;
            }
        }
        return false;
    }

    public decimal SumQty()
    {
        decimal totalQty = 0;
        if (GV_List.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in GV_List.Rows)
            {
                TextBox tbQty = (TextBox)gvr.FindControl("tbQty");
                decimal qty = tbQty.Text.Trim() != string.Empty ? decimal.Parse(tbQty.Text.Trim()) : 0;
                totalQty += qty;
            }
        }
        return totalQty;
    }

    public IList<Hu> GetHuList()
    {
        IList<Hu> huList = new List<Hu>();
        if (GV_List.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in GV_List.Rows)
            {
                Label lblHuId = (Label)gvr.FindControl("lblHuId");
                Label lblLotNo = (Label)gvr.FindControl("lblLotNo");
                TextBox tbQty = (TextBox)gvr.FindControl("tbQty");

                Hu hu = new Hu();
                hu.HuId = lblHuId.Text;
                hu.LotNo = lblLotNo.Text;
                hu.Qty = tbQty.Text.Trim() != string.Empty ? decimal.Parse(tbQty.Text.Trim()) : 0;

                if (hu.Qty != 0)
                    huList.Add(hu);
            }
        }
        return huList;
    }

}
