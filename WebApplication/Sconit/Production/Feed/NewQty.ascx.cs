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
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Entity.Production;

public partial class Production_Feed_NewQty : ModuleBase
{
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbProductLine.ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            TheProductLineInProcessLocationDetailMgr.RawMaterialIn(this.tbProductLine.Text.Trim(), GetMaterialInList(), this.CurrentUser);
            ShowSuccessMessage("MasterData.Feed.MaterialIn.Successfully");
            InitPageParameter();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(sender, e);
        }

    }

    protected void tbProductLine_TextChanged(object sender, EventArgs e)
    {
        InitPageParameter();
    }

    public void InitPageParameter()
    {
        #region 已投料
        IList<ProductLineInProcessLocationDetail> productLineIpList = new List<ProductLineInProcessLocationDetail>();
        if (tbProductLine.Text.Trim() != string.Empty)
        {
            productLineIpList = TheProductLineInProcessLocationDetailMgr.GetProductLineInProcessLocationDetail(this.tbProductLine.Text.Trim(), BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE);
        }
        this.GV_List_Feeded.DataSource = productLineIpList;
        this.GV_List_Feeded.DataBind();
        #endregion

        #region 新投料
        //todo 根据flow得到Material的list
        IList<BomDetail> bomDetailList = new List<BomDetail>();
        IList<MaterialIn> materialInList = new List<MaterialIn>();
        if (this.tbProductLine.Text.Trim() != string.Empty)
        {
            bomDetailList = TheFlowMgr.GetBatchFeedBomDetail(this.tbProductLine.Text.Trim());
        }


        //暂时放在页面，后面再抽出来
        Flow flow = TheFlowMgr.LoadFlow(this.tbProductLine.Text.Trim(), true);
        if (bomDetailList != null && bomDetailList.Count > 0)
        {
            foreach (BomDetail bomDetail in bomDetailList)
            {
                MaterialIn materialIn = new MaterialIn();
                materialIn.Location = bomDetail.Location;
                materialIn.Operation = bomDetail.Operation;
                materialIn.RawMaterial = bomDetail.Item;

                //来源库位查找逻辑BomDetail-->RoutingDetail-->FlowDetail-->Flow
                Location bomLocFrom = bomDetail.Location;

                if (flow.Routing != null)
                {
                    //在Routing上查找，并检验Routing上的工序和BOM上的是否匹配
                    RoutingDetail routingDetail = TheRoutingDetailMgr.LoadRoutingDetail(flow.Routing, bomDetail.Operation, bomDetail.Reference);
                    if (routingDetail != null)
                    {
                        if (bomLocFrom == null)
                        {
                            bomLocFrom = routingDetail.Location;
                        }
                    }
                }

                if (bomLocFrom == null)
                {
                    bomLocFrom = bomDetail.DefaultLocation;

                }
                materialIn.Location = bomLocFrom;
                bool isInclude = false;
                foreach (MaterialIn m in materialInList)
                {
                    if (m.Location.Code == materialIn.Location.Code && m.Operation == materialIn.Operation && m.RawMaterial.Code == materialIn.RawMaterial.Code)
                    {
                        isInclude = true;
                    }
                }
                if (!isInclude)
                {
                    materialInList.Add(materialIn);
                }
            }
        }
        this.GV_List.DataSource = materialInList;
        this.GV_List.DataBind();
        #endregion
    }

    private IList<MaterialIn> GetMaterialInList()
    {
        IList<MaterialIn> materialInList = new List<MaterialIn>();
        for (int i = 0; i < this.GV_List.Rows.Count; i++)
        {
            GridViewRow row = this.GV_List.Rows[i];
            string itemCode = ((Label)row.FindControl("lblItemCode")).Text.Trim();

            Label lblOperation = (Label)row.FindControl("lblOperation");
            int? operation = null;
            if (lblOperation.Text.Trim() != string.Empty)
            {
                operation = int.Parse(lblOperation.Text.Trim());
            }

            Label lblLocation = (Label)row.FindControl("lblLocation");

            TextBox tbQty = (TextBox)row.FindControl("tbQty");
            decimal qty = tbQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbQty.Text.Trim());

            MaterialIn materialIn = new MaterialIn();
            materialIn.RawMaterial = TheItemMgr.LoadItem(itemCode);
            materialIn.Operation = operation;
            materialIn.Qty = qty;

            if (lblLocation.Text.Trim() != string.Empty)
            {
                materialIn.Location = TheLocationMgr.LoadLocation(lblLocation.Text.Trim());
            }
            if (materialIn.Qty != 0)
            {
                materialInList.Add(materialIn);
            }
        }
        return materialInList;
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(((LinkButton)sender).CommandArgument);
        try
        {
            TheProductLineInProcessLocationDetailMgr.DeleteProductLineInProcessLocationDetail(id);
            this.ShowSuccessMessage("MasterData.Feed.MaterialIn.Delete.Successfully");
            InitPageParameter();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }

    }

}
