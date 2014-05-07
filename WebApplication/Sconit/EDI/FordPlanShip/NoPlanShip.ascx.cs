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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using System.Collections.Generic;
using com.Sconit.Entity.EDI;
using com.Sconit.Entity.Exception;

public partial class EDI_FordPlan_NoPlanShip : ListModuleBase
{

    public event EventHandler BackEvent;


    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:true,bool:true,bool:false,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM;
    }

    public override void UpdateView()
    {
        List<EDIFordPlan> returnList = new List<EDIFordPlan>();
        string flowCode = this.tbFlow.Text.Trim();
        Flow currentFlow = null;
        if (!string.IsNullOrEmpty(flowCode))
        {
            currentFlow = TheFlowMgr.LoadFlow(flowCode, this.CurrentUser.Code, true);
            if (currentFlow != null && currentFlow.FlowDetails != null && currentFlow.FlowDetails.Count > 0)
            {
                foreach (var f in currentFlow.FlowDetails)
                {
                    EDIFordPlan r = new EDIFordPlan();
                    r.Item = f.Item.Code;
                    r.ItemDesc = f.Item.Description;
                    r.RefItem = f.ReferenceItemCode;
                    if (!string.IsNullOrEmpty(r.RefItem))
                    {
                        r.Uom = f.Uom.Code;
                        r.CustomerCode = "BVT8A";
                    }
                    //r.SupplierCode = f.ShipFrom;
                    r.TransportationMethod = f.TransModeCode;
                    r.EquipmentNum = f.ConveyanceNumber;
                    r.CarrierCode = f.CarrierCode;
                    try
                    {
                        r.GrossWeight = Convert.ToDecimal(f.GrossWeight);
                    }
                    catch (Exception)
                    {
                        r.GrossWeight = 0;
                    }
                    try
                    {
                        r.NetWeight = Convert.ToDecimal(f.NetWeight);
                    }
                    catch (Exception)
                    {
                        r.NetWeight = 0;
                    }
                    r.WeightUom = f.WeightUom;
                    r.OutPackType = f.PackagingCode;
                    r.InPackType = f.PackagingCode;
                    try
                    {
                        r.OutPackQty = Convert.ToDecimal(f.LadingQuantity);
                    }
                    catch (Exception)
                    {
                        r.OutPackQty = 0;
                    }
                    try
                    {
                        r.PerLoadQty = Convert.ToDecimal(f.UnitsPerContainer);
                    }
                    catch (Exception)
                    {
                        r.PerLoadQty = 0;
                    }
                    returnList.Add(r);
                }
            }

        }
        this.GV_List.DataSource = returnList;
        this.GV_List.DataBind();
    }

    protected void tbFlow_TextChanged(Object sender, EventArgs e)
    {
        this.UpdateView();
    }

    protected void btnShip_Click(object sender, EventArgs e)
    {
        try
        {
            Flow currentFlow = new Flow();            
            string flowCode = this.tbFlow.Text.Trim();
            if (string.IsNullOrEmpty(flowCode))
            {
                throw new BusinessErrorException("发货路线不能为空。");
            }
            else
            {
                currentFlow = TheFlowMgr.LoadFlow(flowCode, this.CurrentUser.Code, true);
                if (currentFlow == null)
                {
                    throw new BusinessErrorException("发货路线填写不正确。");
                }
            }
            List<EDIFordPlan> shipEDIFordPlanList = this.GetShipEDIFordPlan();
            if (shipEDIFordPlanList == null || shipEDIFordPlanList.Count == 0)
            {
                throw new BusinessErrorException("发货的有效数据为0，发货失败。");
            }
            TheEDIMgr.ShipEDIFordPlan(shipEDIFordPlanList, this.CurrentUser.Code, currentFlow);
            ShowSuccessMessage("发货成功。");
            this.tbFlow.Text = string.Empty;
            this.UpdateView();
            //if (ShipEvent != null)
            //{
            //    ShipEvent(new object[] { this.CacheResolver.Code, cbPrintAsn.Checked }, null);
            //}

        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    private List<EDIFordPlan> GetShipEDIFordPlan()
    {
        List<EDIFordPlan> eDIFordPlanList = new List<EDIFordPlan>();
        foreach (GridViewRow gvr in GV_List.Rows)
        {
            EDIFordPlan eDIFordPlan = new EDIFordPlan();
            CheckBox cbCheckBoxGroup = (CheckBox)gvr.FindControl("CheckBoxGroup");
            if (cbCheckBoxGroup.Checked)
            {
                eDIFordPlan.Item = ((HiddenField)gvr.FindControl("ftItem")).Value;
                eDIFordPlan.ItemDesc = ((HiddenField)gvr.FindControl("ftItemDesc")).Value;
                eDIFordPlan.RefItem = ((HiddenField)gvr.FindControl("ftRefItem")).Value;

                #region  收货工厂
                if (string.IsNullOrEmpty(((TextBox)gvr.FindControl("tbCustomerCode")).Text.Trim()))
                {
                    throw new BusinessErrorException(string.Format("物料号{0}收货工厂填写有误。", eDIFordPlan.Item));
                }
                else
                {
                    eDIFordPlan.CustomerCode = ((TextBox)gvr.FindControl("tbCustomerCode")).Text.Trim();
                }
                #endregion

                #region  发货工厂
                if (string.IsNullOrEmpty(((TextBox)gvr.FindControl("tbSupplierCode")).Text.Trim()))
                {
                    throw new BusinessErrorException(string.Format("物料号{0}发货工厂填写有误。", eDIFordPlan.Item));
                }
                else
                {
                    eDIFordPlan.SupplierCode = ((TextBox)gvr.FindControl("tbSupplierCode")).Text.Trim();
                }
                #endregion

                #region  采购订单号
                //if (string.IsNullOrEmpty(((TextBox)gvr.FindControl("tbPurchaseOrder")).Text.Trim()))
                //{
                //    throw new BusinessErrorException(string.Format("物料号{0}采购订单号填写有误。", eDIFordPlan.Item));
                //}
                //else
                //{
                //    eDIFordPlan.PurchaseOrder = ((TextBox)gvr.FindControl("tbPurchaseOrder")).Text.Trim();
                //}
                //售后备件发货 采购订单号 默认填写
                eDIFordPlan.PurchaseOrder = "111111";
                #endregion

                #region   中间商
                eDIFordPlan.IntermediateConsignee = ((TextBox)gvr.FindControl("tbIntermediateConsignee")).Text.Trim();
                #endregion

                #region   本次发货量
                try
                {
                    eDIFordPlan.ShipQty = decimal.Parse(((TextBox)gvr.FindControl("tbShipQty")).Text.Trim());
                }
                catch (Exception e)
                {
                    throw new BusinessErrorException(string.Format("版本号{0}物料号{1}本次发货量填写有误。", eDIFordPlan.Control_Num, eDIFordPlan.Item));
                }
                #endregion

                #region   发货累计数
                try
                {
                    eDIFordPlan.CurrenCumQty = decimal.Parse(((TextBox)gvr.FindControl("tbCurrenCumQty")).Text.Trim());
                }
                catch (Exception e)
                {
                    throw new BusinessErrorException(string.Format("物料号{0}发货累计数填写有误。", eDIFordPlan.Item));
                }
                #endregion

                #region   装箱单号
                if (string.IsNullOrEmpty(((TextBox)gvr.FindControl("tbShipmentID")).Text.Trim()))
                {
                    throw new BusinessErrorException(string.Format("版本号{0}物料号{1}装箱单号填写有误。", eDIFordPlan.Control_Num, eDIFordPlan.Item));
                }
                else
                {
                    eDIFordPlan.ShipmentID = ((TextBox)gvr.FindControl("tbShipmentID")).Text.Trim();
                }
                #endregion

                #region   提单号
                if (string.IsNullOrEmpty(((TextBox)gvr.FindControl("tbLadingNum")).Text.Trim()))
                {
                    throw new BusinessErrorException(string.Format("版本号{0}物料号{1}提单号填写有误。", eDIFordPlan.Control_Num, eDIFordPlan.Item));
                }
                else
                {
                    eDIFordPlan.LadingNum = ((TextBox)gvr.FindControl("tbLadingNum")).Text.Trim();
                }
                #endregion

                #region   单位
                if (string.IsNullOrEmpty(((TextBox)gvr.FindControl("tbInUom")).Text.Trim()))
                {
                    throw new BusinessErrorException(string.Format("版本号{0}物料号{1}单位填写有误。", eDIFordPlan.Control_Num, eDIFordPlan.Item));
                }
                else
                {
                    eDIFordPlan.Uom = ((TextBox)gvr.FindControl("tbInUom")).Text.Trim();
                }
                #endregion

                eDIFordPlan.Purpose = ((System.Web.UI.HtmlControls.HtmlSelect)gvr.FindControl("tbPurpose")).Value;

                #region   发货总毛重
                try
                {
                    eDIFordPlan.GrossWeight = decimal.Parse(((TextBox)gvr.FindControl("tbGrossWeight")).Text.Trim()) * eDIFordPlan.ShipQty;
                }
                catch (Exception e)
                {
                    throw new BusinessErrorException(string.Format("版本号{0}物料号{1}发货总毛重填写有误。", eDIFordPlan.Control_Num, eDIFordPlan.Item));
                }
                #endregion

                #region   发货总净重
                try
                {
                    eDIFordPlan.NetWeight = decimal.Parse(((TextBox)gvr.FindControl("tbNetWeight")).Text.Trim()) * eDIFordPlan.ShipQty;
                }
                catch (Exception e)
                {
                    throw new BusinessErrorException(string.Format("版本号{0}物料号{1}发货总净重填写有误。", eDIFordPlan.Control_Num, eDIFordPlan.Item));
                }
                #endregion

                #region   毛重净重单位
                if (string.IsNullOrEmpty(((TextBox)gvr.FindControl("tbWeightUom")).Text.Trim()))
                {
                    throw new BusinessErrorException(string.Format("版本号{0}物料号{1}毛重净重单位填写有误。", eDIFordPlan.Control_Num, eDIFordPlan.Item));
                }
                else {
                    eDIFordPlan.WeightUom = ((TextBox)gvr.FindControl("tbWeightUom")).Text.Trim();
                }
                #endregion

                #region   外包装类型
                eDIFordPlan.OutPackType = ((TextBox)gvr.FindControl("tbOutPackType")).Text.Trim();
                #endregion

                #region   外包装数量
                try
                {
                    eDIFordPlan.OutPackQty = decimal.Parse(((TextBox)gvr.FindControl("tbOutPackQty")).Text.Trim());
                }
                catch (Exception e)
                {
                    eDIFordPlan.OutPackQty = null;
                }
                #endregion

                #region   承运商
                if (string.IsNullOrEmpty(((TextBox)gvr.FindControl("tbCarrierCode")).Text.Trim()))
                {
                    throw new BusinessErrorException(string.Format("版本号{0}物料号{1}承运商填写有误。", eDIFordPlan.Control_Num, eDIFordPlan.Item));
                }
                else
                {
                    eDIFordPlan.CarrierCode = ((TextBox)gvr.FindControl("tbCarrierCode")).Text.Trim();
                }
                #endregion

                #region   运输方式
                if (string.IsNullOrEmpty(((TextBox)gvr.FindControl("tbTransportationMethod")).Text.Trim()))
                {
                    throw new BusinessErrorException(string.Format("版本号{0}物料号{1}运输方式填写有误。", eDIFordPlan.Control_Num, eDIFordPlan.Item));
                }
                else
                {
                    eDIFordPlan.TransportationMethod = ((TextBox)gvr.FindControl("tbTransportationMethod")).Text.Trim();
                }
                #endregion

                #region   运载媒介
                eDIFordPlan.EquipmentDesc = eDIFordPlan.TransportationMethod;
                if (eDIFordPlan.TransportationMethod == "M")
                {
                    eDIFordPlan.EquipmentDesc = "TL";
                }
                else if (eDIFordPlan.TransportationMethod == "O")
                {
                    eDIFordPlan.EquipmentDesc = "CN";
                }
                else if (eDIFordPlan.TransportationMethod == "A")
                {
                    eDIFordPlan.EquipmentDesc = "AF";
                }
                //if (string.IsNullOrEmpty(((TextBox)gvr.FindControl("tbEquipmentDesc")).Text.Trim()))
                //{
                //    throw new BusinessErrorException(string.Format("版本号{0}物料号{1}运载媒介填写有误。", eDIFordPlan.Control_Num, eDIFordPlan.Item));
                //}
                //else
                //{
                    //eDIFordPlan.EquipmentDesc = ((TextBox)gvr.FindControl("tbEquipmentDesc")).Text.Trim();
               // }
                #endregion

                #region   运载媒介序列号
                if (string.IsNullOrEmpty(((TextBox)gvr.FindControl("tbEquipmentNum")).Text.Trim()))
                {
                    throw new BusinessErrorException(string.Format("版本号{0}物料号{1}运载媒介序列号填写有误。", eDIFordPlan.Control_Num, eDIFordPlan.Item));
                }
                else
                {
                    eDIFordPlan.EquipmentNum = ((TextBox)gvr.FindControl("tbEquipmentNum")).Text.Trim();
                }
                #endregion

                #region   内包装类型
                eDIFordPlan.InPackType = ((TextBox)gvr.FindControl("tbOutPackType")).Text.Trim();
                #endregion

                #region   每个包装数量
                try
                {
                    eDIFordPlan.PerLoadQty = decimal.Parse(((TextBox)gvr.FindControl("tbPerLoadQty")).Text.Trim());
                }
                catch (Exception e)
                {
                    eDIFordPlan.PerLoadQty = null;
                }
                #endregion

                #region   内包装数量
                eDIFordPlan.InPackQty = eDIFordPlan.PerLoadQty == null ? null : (decimal?)Convert.ToDecimal((eDIFordPlan.ShipQty.Value % eDIFordPlan.PerLoadQty.Value == 0 ? eDIFordPlan.ShipQty.Value / eDIFordPlan.PerLoadQty.Value : Convert.ToInt32(eDIFordPlan.ShipQty.Value) / Convert.ToInt32(eDIFordPlan.PerLoadQty.Value) + 1));
                //try
                //{
                //    eDIFordPlan.InPackQty = decimal.Parse(((TextBox)gvr.FindControl("tbInPackQty")).Text.Trim());
                //}
                //catch (Exception e)
                //{
                //    eDIFordPlan.InPackQty = null;
                //}
                #endregion

                #region   机场代码
                eDIFordPlan.AirportCode = ((TextBox)gvr.FindControl("tbAirportCode")).Text.Trim();
                if (eDIFordPlan.TransportationMethod == "A")
                {
                    if (string.IsNullOrEmpty(eDIFordPlan.AirportCode))
                    {
                        throw new BusinessErrorException(string.Format("版本号{0}物料号{1},在选择空运的时候机场代码不能为空。", eDIFordPlan.Control_Num, eDIFordPlan.Item));
                    }
                }
                else
                {
                    eDIFordPlan.AirportCode = string.Empty;
                }
                #endregion

                eDIFordPlanList.Add(eDIFordPlan);
            }
            
        }
        return eDIFordPlanList;
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

}
