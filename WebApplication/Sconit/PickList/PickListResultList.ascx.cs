using System;
using System.Collections;
using System.Web.UI.WebControls;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Web;

public partial class Order_PickList_PickListResultList : ModuleBase
{
    public event EventHandler ShipEvent;

    public string PickBy
    {
        get
        {
            return (string)ViewState["PickBy"];
        }
        set
        {
            ViewState["PickBy"] = value;
        }
    }

    public string PickListNo
    {
        get
        {
            return (string)ViewState["PickListNo"];
        }
        set
        {
            ViewState["PickListNo"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(PickList pickList)
    {
        this.PickBy = pickList.PickBy;
        this.PickListNo = pickList.PickListNo;
        IList<PickListResult> pickListResultList = new List<PickListResult>();
        foreach (PickListDetail pickListDetail in pickList.PickListDetails)
        {
            IList<PickListResult> plResultList = ThePickListResultMgr.GetPickListResult(pickListDetail);
            if (plResultList != null && plResultList.Count > 0)
            {
                foreach (PickListResult pickListResult in plResultList)
                {
                    pickListResult.Qty = pickListResult.Qty;
                    pickListResult.ItemCode = pickListResult.PickListDetail.Item.Code;
                    pickListResult.ItemDescription = pickListResult.PickListDetail.Item.Description;
                    pickListResult.LocationCode = pickListResult.LocationLotDetail.Location.Code;
                    pickListResult.UomCode = pickListResult.PickListDetail.Uom.Code;
                    pickListResult.UnitCount = pickListResult.PickListDetail.UnitCount;
                    pickListResult.PickListNo = pickListResult.PickListDetail.PickList.PickListNo;
                    pickListResult.HuId = pickListResult.LocationLotDetail.Hu.HuId;
                    pickListResult.Status = pickListResult.PickListDetail.PickList.Status;
                    pickListResult.OrderNo = pickListResult.PickListDetail.OrderLocationTransaction.OrderDetail.OrderHead.OrderNo;
                    pickListResult.StorageBinCode = pickListResult.PickListDetail.StorageBin.Code;
                    pickListResult.LotNo = pickListResult.PickListDetail.LotNo;

                    pickListResultList.Add(pickListResult);
                }
            }
        }
        InitPageParameter(pickListResultList);
    }

    public void InitPageParameter(IList<PickListResult> pickListResults)
    {
        this.GV_List.DataSource = pickListResults;
        this.GV_List.DataBind();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}