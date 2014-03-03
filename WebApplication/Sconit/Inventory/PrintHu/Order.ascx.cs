using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;

public partial class Inventory_PrintHu_Order : ModuleBase
{
    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    private string CurrentOrderNo
    {
        get
        {
            return (string)ViewState["CurrentOrderNo"];
        }
        set
        {
            ViewState["CurrentOrderNo"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ucList.ModuleType = this.ModuleType;
        }
    }

    protected void tbOrderNo_TextChanged(Object sender, EventArgs e)
    {
        try
        {
            if (this.CurrentOrderNo == null || this.CurrentOrderNo != this.tbOrderNo.Text.Trim())
            {
                OrderHead currentOrderHead = TheOrderMgr.LoadOrder(this.tbOrderNo.Text.Trim(), this.CurrentUser);

                if (currentOrderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL
                    || currentOrderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE
                    || currentOrderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)
                {
                    this.ShowErrorMessage("Inventory.Error.PrintHu.OrderStatus", currentOrderHead.OrderNo, currentOrderHead.Status);
                    return;
                }

                if (currentOrderHead.SubType != BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML)
                {
                    this.ShowErrorMessage("Inventory.Error.PrintHu.OrderSubType", currentOrderHead.OrderNo, currentOrderHead.SubType);
                    return;
                }

                currentOrderHead.OrderDetails = TheOrderDetailMgr.GetOrderDetail(currentOrderHead);
                this.CurrentOrderNo = currentOrderHead.OrderNo;

                this.ucList.InitPageParameter(currentOrderHead);
                this.ucList.Visible = true;
            }
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }
}
