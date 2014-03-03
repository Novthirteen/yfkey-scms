using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;

public partial class Inventory_PrintHu_OrderDetailList : ModuleBase
{
    public event EventHandler PrintEvent;

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

    public void InitPageParameter(OrderHead orderHead)
    {
        this.GV_List.DataSource = orderHead.OrderDetails;

        #region 设置默认LotNo
        string lotNo = LotNoHelper.GenerateLotNo();
        foreach (OrderDetail orderDetail in orderHead.OrderDetails)
        {
            orderDetail.HuLotNo = lotNo;
        }
        #endregion

        this.GV_List.DataBind();
    }

    public void PrintCallBack()
    {
        IList<OrderDetail> orderDetailList = this.PopulateOrderDetailList();
        this.PrintEvent(orderDetailList, null);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IList<OrderDetail> orderDetailList = PopulateOrderDetailList();
        IList<OrderDetail> targetOrderDetailList = new List<OrderDetail>();

        if (orderDetailList != null && orderDetailList.Count > 0)
        {
            foreach (OrderDetail orderDetail in orderDetailList)
            {
                if (orderDetail.OrderedQty > 0)
                {
                    targetOrderDetailList.Add(orderDetail);
                }
            }
        }

        if (targetOrderDetailList.Count == 0)
        {
            this.ShowErrorMessage("Inventory.Error.PrintHu.OrderDetail.Required");
            return;
        }

        IList<Hu> huList = null;

        #region  内/外包装
        string packageType = null;
        RadioButtonList rblPackageType = (RadioButtonList)this.Parent.FindControl("rblPackageType");
        if (rblPackageType.SelectedValue == "0")
        {
            packageType = BusinessConstants.CODE_MASTER_PACKAGETYPE_INNER;
        }
        else if (rblPackageType.SelectedValue == "1")
        {
            packageType = BusinessConstants.CODE_MASTER_PACKAGETYPE_OUTER;
        }
        #endregion
        if (this.ModuleType == BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_SUPPLIER)
        {
            huList = TheHuMgr.CreateHu(targetOrderDetailList, this.CurrentUser, null, packageType);
        }
        else
        {
            EntityPreference entityPreference = this.TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_COMPANY_ID_MARK);
            huList = TheHuMgr.CreateHu(targetOrderDetailList, this.CurrentUser, entityPreference.Value,packageType);
        }

        String huTemplate = "";
        if (this.ModuleType == BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION)
        {
            huTemplate = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_HU_TEMPLATE).Value;
        }
        else if (targetOrderDetailList != null
                    && targetOrderDetailList.Count > 0
                    && targetOrderDetailList[0].OrderHead != null
                    && targetOrderDetailList[0].OrderHead.HuTemplate != null
                    && targetOrderDetailList[0].OrderHead.HuTemplate.Length > 0)
        {
            huTemplate = targetOrderDetailList[0].OrderHead.HuTemplate;
        }

        if (huTemplate != null && huTemplate.Length>0)
        {
            IList<object> huDetailObj = new List<object>();
            huDetailObj.Add(huList);
            huDetailObj.Add(CurrentUser.Code);

            string barCodeUrl = "";
            if (packageType == BusinessConstants.CODE_MASTER_PACKAGETYPE_OUTER)
            {
                barCodeUrl = TheReportMgr.WriteToFile(huTemplate, huDetailObj, huTemplate);
            }
            else
            {
                barCodeUrl = TheReportMgr.WriteToFile("Inside" + huTemplate, huDetailObj, "Inside" + huTemplate);
            }
            Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + barCodeUrl + "'); </script>");

            this.ShowSuccessMessage("Inventory.PrintHu.Successful");
        }
    }

    //返回订单明细
    private IList<OrderDetail> PopulateOrderDetailList()
    {
        if (this.GV_List.Rows != null && this.GV_List.Rows.Count > 0)
        {
            IList<OrderDetail> orderDetailList = new List<OrderDetail>();

            foreach (GridViewRow row in this.GV_List.Rows)
            {
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked)
                {
                    HiddenField hfId = (HiddenField)row.FindControl("hfId");
                    TextBox tbLotNo = (TextBox)row.FindControl("tbLotNo");
                    TextBox tbOrderQty = (TextBox)row.FindControl("tbOrderQty");

                    OrderDetail orderDetail = this.TheOrderDetailMgr.LoadOrderDetail(int.Parse(hfId.Value));
                    orderDetail.HuLotNo = tbLotNo.Text.Trim() != string.Empty ? tbLotNo.Text.Trim() : null;
                    orderDetail.OrderedQty = tbOrderQty.Text.Trim() != string.Empty ? decimal.Parse(tbOrderQty.Text.Trim()) : 0;

                    orderDetailList.Add(orderDetail);
                }
            }

            return orderDetailList;
        }

        return null;
    }
}
