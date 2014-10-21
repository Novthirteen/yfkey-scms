using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using System.Collections.Generic;
using com.Sconit.Web;
using com.Sconit.Entity;

/// <summary>
/// Summary description for FlowMgrWS
/// </summary>
[WebService(Namespace = "http://com.Sconit.Webservice/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class FlowMgrWS : BaseWS
{
    public FlowMgrWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public FlowDetailProxy GenerateFlowDetailProxy(string flowCode, string itemCode, string moduleType, string partyFromCode, string partyToCode, bool changeRef, DateTime startTime)
    {
        IList<FlowDetail> flowDetailList = TheFlowDetailMgr.GetFlowDetail(flowCode, true);

        if (flowDetailList != null && flowDetailList.Count > 0)
        {
            foreach (FlowDetail fd in flowDetailList)
            {
                if (fd.Item.Code == itemCode)
                {
                    FlowDetailProxy flowDetailProxy = new FlowDetailProxy();
                    flowDetailProxy.ItemCode = fd.Item.Code;
                    flowDetailProxy.ItemDescription = fd.Item.Description;
                    if (changeRef)
                    {
                        flowDetailProxy.ItemReferenceCode = fd.ReferenceItemCode;
                    }
                    flowDetailProxy.UomCode = fd.Uom.Code;
                    flowDetailProxy.UnitCount = fd.UnitCount;
                    flowDetailProxy.HuLotSize = fd.HuLotSize == null ?  Convert.ToInt32(fd.UnitCount) : fd.HuLotSize;
                    if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                    {
                        if (fd.DefaultPriceListFrom != null)
                        {
                            flowDetailProxy.PriceListCode = fd.DefaultPriceListFrom.Code;
                            PriceListDetail priceListDetailFrom = ThePriceListDetailMgr.GetLastestPriceListDetail(fd.DefaultPriceListFrom, fd.Item, startTime, fd.Flow.Currency, fd.Uom);
                            if (priceListDetailFrom != null)
                            {
                                flowDetailProxy.UnitPrice = priceListDetailFrom.UnitPrice;
                                flowDetailProxy.PriceListDetailId = priceListDetailFrom.Id;
                            }
                        }

                    }
                    else if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                    {
                        if (fd.DefaultPriceListTo != null)
                        {
                            flowDetailProxy.PriceListCode = fd.DefaultPriceListTo.Code;
                            PriceListDetail priceListDetailTo = ThePriceListDetailMgr.GetLastestPriceListDetail(fd.DefaultPriceListTo, fd.Item, startTime, fd.Flow.Currency, fd.Uom);
                            if (priceListDetailTo != null)
                            {
                                flowDetailProxy.UnitPrice = priceListDetailTo.UnitPrice;
                                flowDetailProxy.PriceListDetailId = priceListDetailTo.Id;
                            }
                        }
                        flowDetailProxy.PackageVolumn = fd.PackageVolumn;
                    }

                    if (changeRef)
                    {
                        if (fd.ReferenceItemCode != null && fd.ReferenceItemCode != string.Empty)
                        {
                            flowDetailProxy.ItemReferenceCode = fd.ReferenceItemCode;
                        }
                        else
                        {
                            flowDetailProxy.ItemReferenceCode = TheItemReferenceMgr.GetItemReferenceByItem(itemCode, partyToCode, partyFromCode);
                        }
                    }
                    return flowDetailProxy;
                }
            }
        }

        Item item = TheItemMgr.LoadItem(itemCode);
        if (item != null)
        {
            Flow flow = TheFlowMgr.LoadFlow(flowCode);
            FlowDetailProxy flowDetailProxy = new FlowDetailProxy();
            flowDetailProxy.ItemCode = item.Code;
            flowDetailProxy.ItemDescription = item.Description;
            flowDetailProxy.UomCode = item.Uom.Code;

            if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                if (flow.PriceListFrom != null)
                {
                    flowDetailProxy.PriceListCode = flow.PriceListFrom.Code;
                    PriceListDetail priceListDetailFrom = ThePriceListDetailMgr.GetLastestPriceListDetail(flow.PriceListFrom, item, startTime, flow.Currency, item.Uom);
                    if (priceListDetailFrom != null)
                    {
                        flowDetailProxy.UnitPrice = priceListDetailFrom.UnitPrice;
                        flowDetailProxy.PriceListDetailId = priceListDetailFrom.Id;
                    }
                }
            }
            else if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                if (flow.PriceListTo != null)
                {
                    flowDetailProxy.PriceListCode = flow.PriceListTo.Code;
                    PriceListDetail priceListDetailTo = ThePriceListDetailMgr.GetLastestPriceListDetail(flow.PriceListTo, item, startTime, flow.Currency, item.Uom);
                    if (priceListDetailTo != null)
                    {
                        flowDetailProxy.UnitPrice = priceListDetailTo.UnitPrice;
                        flowDetailProxy.PriceListDetailId = priceListDetailTo.Id;
                    }
                }


            }

            if (changeRef)
            {
                flowDetailProxy.ItemReferenceCode = TheItemReferenceMgr.GetItemReferenceByItem(itemCode, partyToCode, partyFromCode);

            }
            flowDetailProxy.UnitCount = item.UnitCount;
            flowDetailProxy.HuLotSize = Convert.ToInt32(item.UnitCount);
            return flowDetailProxy;
        }

        return null;
    }

    [WebMethod]
    public FlowDetailProxy GenerateFlowDetailProxyByReferenceItem(string flowCode, string refItemCode, string partyFromCode, string partyToCode, string moduleType, bool changeRef, DateTime startTime)
    {
        Item item = null;
        if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            item = TheItemReferenceMgr.GetItemReferenceByRefItem(refItemCode, partyFromCode, partyToCode);
        }
        else if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            item = TheItemReferenceMgr.GetItemReferenceByRefItem(refItemCode, partyToCode, partyFromCode);
        }
        else
        {
            item = TheItemReferenceMgr.GetItemReferenceByRefItem(refItemCode, partyFromCode, partyFromCode);
        }
        if (item != null)
        {
            return GenerateFlowDetailProxy(flowCode, item.Code, moduleType, partyFromCode, partyToCode, changeRef, startTime);
        }
        else
            return null;
    }

    [WebMethod]
    public decimal GetUnitPriceByUom(string priceListCode, string itemCode, DateTime startTime, string currencyCode, string uomCode)
    {

        decimal unitPrice = 0;
        if (priceListCode != string.Empty)
        {
            PriceListDetail priceListDetail = ThePriceListDetailMgr.GetLastestPriceListDetail(priceListCode, itemCode, startTime, currencyCode, uomCode);
            if (priceListDetail != null)
            {
                unitPrice = priceListDetail.UnitPrice;
            }
        }
        return unitPrice;
    }

    [WebMethod]
    public Flow LoadFlow(string flowCode, string userCode)
    {
        return TheFlowMgr.LoadFlow(flowCode, userCode);
    }
    [WebMethod]
    public FlowDetail[] LoadFlowDetail(string flowCode)
    {
        return TheFlowDetailMgr.GetFlowDetail(flowCode).ToArray();
    }
}

