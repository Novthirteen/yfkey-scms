using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;

public partial class Hu_TransformerDetail : ModuleBase
{
    public event EventHandler QtyChangeEvent;

    public bool ReadOnly { get; set; }

    #region GridViewRow Control Value
    private string GetHuId(GridViewRow gvr)
    {
        return ((Label)gvr.FindControl("lblHuId")).Text;
    }
    private TextBox GetCurrentQtyTextBox(GridViewRow gvr)
    {
        return (TextBox)gvr.FindControl("tbCurrentQty");
    }
    private decimal GetCurrentQty(GridViewRow gvr)
    {
        return GetCurrentQtyTextBox(gvr).Text.Trim() != string.Empty ? decimal.Parse(GetCurrentQtyTextBox(gvr).Text.Trim()) : 0;
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TransformerDetail transformerDetail = (TransformerDetail)e.Row.DataItem;
            if (transformerDetail.CurrentQty == 0)
            {
                GetCurrentQtyTextBox(e.Row).Text = string.Empty;
                GetCurrentQtyTextBox(e.Row).ReadOnly = true;
            }
            else
            {
                GetCurrentQtyTextBox(e.Row).ReadOnly = this.ReadOnly;
            }

            //if (transformerDetail.IsQualified.HasValue)
            //{
            //    Label lbIsQualified = (Label)e.Row.FindControl("lbIsQualified");
            //    TextBox tbCurrentQty = (TextBox)e.Row.FindControl("tbCurrentQty");
            //    HiddenField hfIsQualified = (HiddenField)e.Row.FindControl("hfIsQualified");
            //    lbIsQualified.Visible = true;
            //    hfIsQualified.Value = ((bool)transformerDetail.IsQualified).ToString();
            //    if ((bool)transformerDetail.IsQualified)
            //    {
                    
            //        lbIsQualified.Text = TheLanguageMgr.TranslateMessage("MasterData.InspectOrder.Qualified", this.CurrentUser);
            //        tbCurrentQty.Text = transformerDetail.CurrentQty.ToString("F2");
            //    }
            //    else
            //    {
            //        lbIsQualified.Text = TheLanguageMgr.TranslateMessage("MasterData.InspectOrder.Unqualified", this.CurrentUser);
            //        tbCurrentQty.Text = transformerDetail.CurrentRejectQty.ToString("F2");
            //    }
            //}
        }
    }

    protected void tbQty_TextChanged(object sender, EventArgs e)
    {
        if (QtyChangeEvent != null)
        {
            QtyChangeEvent(sender, null);
        }
    }

    public decimal SumQty()
    {
        decimal totalQty = 0;
        foreach (GridViewRow gvr in GV_List.Rows)
        {
            decimal qty = this.GetCurrentQty(gvr);
            totalQty += qty;
        }

        return totalQty;
    }

    public List<TransformerDetail> GetHuList()
    {
        List<TransformerDetail> huList = new List<TransformerDetail>();
        foreach (GridViewRow gvr in GV_List.Rows)
        {
            TransformerDetail transformerDetail = new TransformerDetail();
            transformerDetail.HuId = this.GetHuId(gvr);
            transformerDetail.LotNo = ((Label)gvr.FindControl("lblLotNo")).Text;
            transformerDetail.StorageBinCode = ((Label)gvr.FindControl("lblStorageBinCode")).Text;
            HiddenField hfQty = (HiddenField)gvr.FindControl("hfQty");
            transformerDetail.Qty = hfQty.Value != string.Empty ? decimal.Parse(hfQty.Value) : 0;
            //if ((HiddenField)gvr.FindControl("hfIsqualified") != null && ((HiddenField)gvr.FindControl("hfIsqualified")).Value != string.Empty)
            //{
            //    transformerDetail.IsQualified = bool.Parse(((HiddenField)gvr.FindControl("hfIsqualified")).Value);
            //}
            transformerDetail.CurrentQty = this.GetCurrentQty(gvr);
            if ((HiddenField)gvr.FindControl("hfId") != null && ((HiddenField)gvr.FindControl("hfId")).Value != string.Empty)
            {
                transformerDetail.Id = int.Parse(((HiddenField)gvr.FindControl("hfId")).Value);
            }
            huList.Add(transformerDetail);
        }

        return huList;
    }

    public void GV_DataBind(List<TransformerDetail> huList)
    {
        if (huList != null && huList.Count > 0)
        {
            this.GV_List.DataSource = huList;
            this.GV_List.DataBind();
        }
    }
}
