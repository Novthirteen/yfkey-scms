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
using com.Sconit.Service.Procurement;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Control;
using com.Sconit.Entity;

public partial class Visualization_SupplyChainRouting_TreeView : ListModuleBase
{
    private SupplyChain supplyChain;
    private IList<SupplyChainDetail> supplyChainDetailList;

    protected void Page_Load(object sender, System.EventArgs e)
    {
    }
    public override void UpdateView()
    {
    }

    public void ShowTreeView(object sender)
    {
        string flowCode = ((object[])sender)[0].ToString();
        string itemCode = ((object[])sender)[1].ToString();

        if (TheFlowMgr.LoadFlow(flowCode) == null)
        {
            ShowErrorMessage("Common.Business.Warn.FlowInvalid");
            return;
        }
        if (TheItemMgr.LoadItem(itemCode) == null)
        {
            ShowErrorMessage("Common.Business.Warn.ItemInvalid");
            return;
        }

        supplyChain = TheSupplyChainMgr.GenerateSupplyChain(flowCode, itemCode)[0];
        supplyChainDetailList = supplyChain.SupplyChainDetails;

        if (supplyChainDetailList != null && supplyChainDetailList.Count > 0)
        {
            MyOrgNode RootNode = new MyOrgNode();
            GenChildOrgNode(RootNode);
            OrgChartTreeView.Node = RootNode;
        }
    }

    private void GenChildOrgNode(MyOrgNode RootNode)
    {
        SupplyChainDetail supplyChainDetail = supplyChainDetailList[0];
        RootNode.Code = supplyChainDetail.Flow.PartyTo.Name;
        RootNode.Name = supplyChainDetail.LocationTo == null ? string.Empty : supplyChainDetail.LocationTo.Name;
        RootNode.Memo1 = supplyChainDetail.FlowDetail.Item.Code;
        RootNode.Memo1Tooltip = supplyChainDetail.FlowDetail.Item.Description;
        RootNode.Memo2 = supplyChainDetail.Flow.Description;
        RootNode.Memo2Tooltip = supplyChainDetail.Flow.Code;
        createChildNode(RootNode, supplyChainDetail.Id);
    }

    private void createChildNode(MyOrgNode parentnode, int Id)
    {
        foreach (SupplyChainDetail supplyChainDetail in supplyChainDetailList)
        {
            if (supplyChainDetail.ParentId == Id)
            {
                MyOrgNode subOrgNode = new MyOrgNode();
                subOrgNode.Code = supplyChainDetail.Flow.PartyTo.Name;
                subOrgNode.Name = supplyChainDetail.LocationTo == null ? string.Empty : supplyChainDetail.LocationTo.Name;
                subOrgNode.Memo1 = supplyChainDetail.FlowDetail.Item.Code;
                subOrgNode.Memo1Tooltip = supplyChainDetail.FlowDetail.Item.Description;
                subOrgNode.Memo2 = supplyChainDetail.Flow.Description;
                subOrgNode.Memo2Tooltip = supplyChainDetail.Flow.Code;

                parentnode.Nodes.Add(subOrgNode);
                createChildNode(subOrgNode, supplyChainDetail.Id);
            }
        }
    }
}
