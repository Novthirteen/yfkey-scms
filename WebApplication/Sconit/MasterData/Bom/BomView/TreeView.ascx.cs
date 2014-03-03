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
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using Whidsoft.WebControls;
using com.Sconit.Control;
using com.Sconit.Entity;

public partial class MasterData_Bom_BomView_TreeView : ListModuleBase
{
    private Bom bom;
    private Item item;
    private IList<BomDetail> bomDetailList;

    protected void Page_Load(object sender, System.EventArgs e)
    {
    }
    public override void UpdateView()
    {
    }

    public void ShowTreeView(object sender)
    {
        string itemCode = ((object[])sender)[0].ToString();
        string date = ((object[])sender)[1].ToString();
        string viewType = ((object[])sender)[2].ToString();
        DateTime effDate = DateTime.Now;

        item = TheItemMgr.LoadItem(itemCode);
        if (item == null || item.Bom == null)
        {
            bom = TheBomMgr.LoadBom(itemCode);
        }
        else
        {
            bom = item.Bom;
        }
        if (bom == null)
        {
            ShowErrorMessage("MasterData.BomDetail.ErrorMessage.BomNotExist");
            this.fld.Visible = false;
            return;
        }
        else
        {
            this.fld.Visible = true;
        }
        try
        {
            effDate = Convert.ToDateTime(date);
        }
        catch (Exception)
        {
            ShowWarningMessage("MasterData.BomView.WarningMessage.DateInvalid");
            return;
        }

        bomDetailList = TheBomDetailMgr.GetTreeBomDetail(bom.Code, effDate);

        if (bomDetailList != null && bomDetailList.Count > 0)
        {
            MyOrgNode RootNode = new MyOrgNode();
            GenChildOrgNode(RootNode);
            OrgChartBomTreeView.Node = RootNode;
        }
    }

    private void GenChildOrgNode(MyOrgNode RootNode)
    {
        decimal BaseQty = 1;
        RootNode.Code = bom.Code;
        RootNode.Name = bom.Description;
        RootNode.Memo1 = BaseQty.ToString() + " " + bom.Uom.Code;
        createChildNode(RootNode, bom.Code, BaseQty);
    }

    private void createChildNode(MyOrgNode parentnode, string compCode, decimal BaseQty)
    {
        foreach (BomDetail bomDetail in bomDetailList)
        {
            if (bomDetail.Bom.Code.ToLower() == compCode.ToLower())
            {
                bomDetail.CalculatedQty = bomDetail.CalculatedQty * BaseQty;
                MyOrgNode subOrgNode = new MyOrgNode();
                subOrgNode.Code = bomDetail.Item.Code;
                subOrgNode.Name = bomDetail.Item.Description;
                subOrgNode.Memo1 = bomDetail.CalculatedQty.ToString("0.########") + " " + bomDetail.Uom.Code;
                subOrgNode.Memo2 = bomDetail.Item.Type;
                parentnode.Nodes.Add(subOrgNode);
                createChildNode(subOrgNode, bomDetail.Item.Code, bomDetail.CalculatedQty);
            }
        }
    }
}
