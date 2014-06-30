using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;

public partial class NewMrp_ShiftPlan_PreviewDetail : ModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    public void BindList(IList<OrderDetail> orderDetailList)
    {
        this.GV_List.DataSource = orderDetailList;
        this.GV_List.DataBind();
    }

    public void CollectData(OrderHead orderHead)
    {
        foreach (GridViewRow gvr in GV_List.Rows)
        {
            int flowDetailId = int.Parse(((HiddenField)gvr.FindControl("hfFlowDetailId")).Value);
            string itemCode = ((Label)gvr.FindControl("lblItemCode")).Text;
            decimal reqQty = decimal.Parse(((TextBox)gvr.FindControl("tbOrderedQty")).Text);

            orderHead.GetOrderDetailByFlowDetailIdAndItemCode(flowDetailId, itemCode).RequiredQty = reqQty;
            orderHead.GetOrderDetailByFlowDetailIdAndItemCode(flowDetailId, itemCode).OrderedQty = reqQty;
        }
    }

    public void CollectDataByFlow(Flow flow)
    {
        IList<FlowDetail> flowdetList = new List<FlowDetail>();
        foreach (GridViewRow gvr in GV_List.Rows)
        {
            int flowDetailId = int.Parse(((HiddenField)gvr.FindControl("hfFlowDetailId")).Value);
            string itemCode = ((Label)gvr.FindControl("lblItemCode")).Text;
            decimal reqQty = decimal.Parse(((TextBox)gvr.FindControl("tbOrderedQty")).Text);
            flowdetList.Add(flow.FlowDetails.FirstOrDefault(d => d.Id == flowDetailId));
        }
        flow.FlowDetails = flowdetList;
    }

}
