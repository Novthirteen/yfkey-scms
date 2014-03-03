using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;

public partial class Warehouse_PutAway_List : ModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InitialAll();
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    public List<TransformerDetail> GetList()
    {
        List<TransformerDetail> transformerDetailList = new List<TransformerDetail>();
        foreach (GridViewRow gvr in GV_List.Rows)
        {
            TransformerDetail transformerDetail = new TransformerDetail();
            transformerDetail.Id = int.Parse(((HiddenField)gvr.FindControl("hfId")).Value);
            transformerDetail.HuId = ((Label)gvr.FindControl("lblHuId")).Text.Trim();
            transformerDetail.LotNo = ((Label)gvr.FindControl("lblLotNo")).Text.Trim();
            transformerDetail.ItemCode = ((Label)gvr.FindControl("lblItemCode")).Text.Trim();
            transformerDetail.ItemDescription = ((Label)gvr.FindControl("lblItemDescription")).Text.Trim();
            transformerDetail.UomCode = ((Label)gvr.FindControl("lblUOM")).Text.Trim();
            transformerDetail.Qty = decimal.Parse(((Label)gvr.FindControl("lblQty")).Text.Trim());
            transformerDetail.StorageBinCode = ((Label)gvr.FindControl("lblStorageBinCode")).Text.Trim();
            transformerDetailList.Add(transformerDetail);
        }

        return transformerDetailList;
    }

    public void BindList(List<TransformerDetail> transformerDetailList)
    {
        this.GV_List.DataSource = transformerDetailList;
        this.GV_List.DataBind();
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        List<TransformerDetail> transformerDetailList = this.GetList();
        int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        transformerDetailList.RemoveAt(rowIndex);
        this.BindList(transformerDetailList);
    }

    private void InitialAll()
    {
    }
}
