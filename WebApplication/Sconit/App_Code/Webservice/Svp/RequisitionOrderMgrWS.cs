using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using NHibernate.Expression;

using com.Sconit.Entity.Svp;
using com.Sconit.Entity.Svp.Condition;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Criteria;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Utility;

[WebService(Namespace = "http://mm.service.integration.symphony")]
[SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.SoapAction)]
[System.Web.Services.WebServiceBindingAttribute(Name = "RequisitionOrderServiceServiceSoapBinding", Namespace = "http://mm.service.integration.symphony")]
public class RequisitionOrderMgrWS : BaseWS
{
     #region 排序字段和单据类型定义和初始化
        private static Dictionary<string, string> orderTypeMapping = new Dictionary<string, string>();
        private static Dictionary<string, string> orderTypeChineseCharacterMapping = new Dictionary<string, string>();
        private static Dictionary<string, string> orderbyFiledsMapping = new Dictionary<string, string>();
        static RequisitionOrderMgrWS()
        {
            orderTypeMapping.Add("RCT_PO", "GPOrder");
            orderTypeMapping.Add("RCT_PO_RT", "GPRTNOrder");
            orderTypeMapping.Add("RCT_PO_ADJ", "GPADJOrder");

            orderTypeChineseCharacterMapping.Add("RCT_PO", "要货单");
            orderTypeChineseCharacterMapping.Add("RCT_PO_RT", "要货退货单");
            orderTypeChineseCharacterMapping.Add("RCT_PO_ADJ", "要货调整单");

            orderbyFiledsMapping.Add("RequisitionOrderNo", "OrderNo");
            orderbyFiledsMapping.Add("EffectiveDate", "StartTime");
            orderbyFiledsMapping.Add("DemandDeliverDate", "WindowTime");
            orderbyFiledsMapping.Add("DemandDeliverAddr", "ShipTo");
            orderbyFiledsMapping.Add("Type", "Type");
            orderbyFiledsMapping.Add("Status", "Status");
            orderbyFiledsMapping.Add("IsPrint", "IsPrinted");
            orderbyFiledsMapping.Add("SupplierPartyCode", "PartyFrom.Code");
            orderbyFiledsMapping.Add("OrderType", "Type");
        }
        #endregion
    
    
    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getRequisitionOrder", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "getRequisitionOrder")]
    [return: System.Xml.Serialization.XmlArrayAttribute("return")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute(Namespace = "http://mm.domain.integration.symphony")]
    public RequisitionOrder[] getRequisitionOrder(GetRequisitionOrderRequest request)
    {
        DetachedCriteria criteria = DetachedCriteria.For<OrderHead>();
       
        if (request.orderby != null)
        {
            RequestOrderByHelper.ConverToCriteria(request.orderby, orderbyFiledsMapping, criteria);
        }

        if (request.requisitionOrderNo != null && request.requisitionOrderNo != string.Empty)
        {
            criteria.Add(Expression.Eq("OrderNo", request.requisitionOrderNo));
            request.rowSize = 1;
        }
        else
        {
            if (request.requisitionOrderStatus != null && request.requisitionOrderStatus.Length > 0)
            {

                for (int i = 0; i < request.requisitionOrderStatus.Count(); i++)
                {
                    if (request.requisitionOrderStatus[i] == "In_Process")
                    {
                        request.requisitionOrderStatus[i] = BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS;
                    }
                }

                criteria.Add(Expression.In("Status", request.requisitionOrderStatus));
            }
            else
            {
                criteria.Add(Expression.Not(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)));
            }

          
        }

        if (request.supplier != null && request.supplier != string.Empty)
        {
            criteria.Add(Expression.Like("PartyFrom.Name", request.supplier));
        }

        if (request.supplierPartyCode != null && request.supplierPartyCode != string.Empty)
        {
            criteria.Add(Expression.Eq("PartyFrom.Code", request.supplierPartyCode));
        }

        if (request.customer != null && request.customer != string.Empty)
        {
            criteria.Add(Expression.Like("PartyTo.Name", request.supplierPartyCode));
        }

        if (request.customerPartyCode != null && request.customerPartyCode != string.Empty)
        {
            criteria.Add(Expression.Eq("PartyTo.Code", request.customerPartyCode));
        }

        if (request.effectiveDateFromSpecified)
        {
            criteria.Add(Expression.Ge("ReleaseDate", request.effectiveDateFrom));
        }

        if (request.effectiveDateToSpecified)
        {
            criteria.Add(Expression.Le("ReleaseDate", request.effectiveDateTo));
        }

      
        IList<OrderHead> orderHeadList = TheCriteriaMgr.FindAll<OrderHead>(criteria, request.beginRowIndex, request.beginRowIndex + request.rowSize);
        List<RequisitionOrder> orderList = new List<RequisitionOrder>();

        foreach (OrderHead orderHead in orderHeadList)
        {
            RequisitionOrder requisitionOrder = fillOrderHead(orderHead);
            foreach(OrderDetail orderDetail in orderHead.OrderDetails)
            {
                RequisitionOrderItem requisitionOrderItem = fillOrderDetail(orderDetail);
                requisitionOrder.addRequisitionOrderItem(requisitionOrderItem);
            }
            orderList.Add(requisitionOrder);
        }

        return orderList.ToArray();
    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("updateRequisitionOrderPrintStatus", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "updateRequisitionOrderPrintStatus")]
    [return: System.Xml.Serialization.XmlElementAttribute("return")]
    public bool updateRequisitionOrderPrintStatus(string requisitionOrderNo, string printStatus)
    {
        OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(requisitionOrderNo);
        orderHead.IsPrinted = printStatus == "Printed";
        TheOrderHeadMgr.UpdateOrderHead(orderHead);
        return true;
    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getRequisitionOrderCount", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "getRequisitionOrderCount")]
    [return: System.Xml.Serialization.XmlElementAttribute("return")]
    public int getRequisitionOrderCount(GetRequisitionOrderRequest request)
    {
        DetachedCriteria criteria = DetachedCriteria.For<OrderHead>().SetProjection(Projections.ProjectionList()
               .Add(Projections.Count("OrderNo")));

        if (request.requisitionOrderNo != null && request.requisitionOrderNo != string.Empty)
        {
            criteria.Add(Expression.Eq("OrderNo", request.requisitionOrderNo));
        }

        if (request.supplier != null && request.supplier != string.Empty)
        {
            criteria.Add(Expression.Like("PartyFrom.Name", request.supplier));
        }

        if (request.supplierPartyCode != null && request.supplierPartyCode != string.Empty)
        {
            criteria.Add(Expression.Eq("PartyFrom.Code", request.supplierPartyCode));
        }

        if (request.customer != null && request.customer != string.Empty)
        {
            criteria.Add(Expression.Like("PartyTo.Name", request.supplierPartyCode));
        }

        if (request.customerPartyCode != null && request.customerPartyCode != string.Empty)
        {
            criteria.Add(Expression.Eq("PartyTo.Code", request.customerPartyCode));
        }

        if (request.effectiveDateFromSpecified)
        {
            criteria.Add(Expression.Ge("StartTime", request.effectiveDateFrom));
        }

        if (request.effectiveDateToSpecified)
        {
            criteria.Add(Expression.Le("StartTime", request.effectiveDateTo));
        }

        if (request.requisitionOrderStatus != null && request.requisitionOrderStatus.Length > 0)
        {
            criteria.Add(Expression.In("Status", request.requisitionOrderStatus));
        }
        else
        {
            criteria.Add(Expression.Not(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)));
        }
       
        IList list = TheCriteriaMgr.FindAll(criteria);

        int count = 0;
        if (list != null && list.Count > 0)
        {
            if (list[0] is int)
            {
                count = int.Parse(list[0].ToString());
            }
        }
        return count;
    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("confirmRequisitionOrder", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "confirmRequisitionOrder")]
    [return: System.Xml.Serialization.XmlElementAttribute("return")]
    public bool confirmRequisitionOrder(string requisitionOrderNo,string userCode)
    {
     
        TheOrderMgr.StartOrder(requisitionOrderNo, userCode);
        return true;

    }

    private RequisitionOrder fillOrderHead(OrderHead orderHead)
    {
        RequisitionOrder order = new RequisitionOrder();

        com.Sconit.Entity.Svp.User createUser = new com.Sconit.Entity.Svp.User();
        createUser.name = orderHead.CreateUser.Name;

        com.Sconit.Entity.Svp.User planner = new com.Sconit.Entity.Svp.User();
        planner.name = orderHead.CreateUser.Name;

        com.Sconit.Entity.Svp.Party partyFrom = new com.Sconit.Entity.Svp.Party();
        partyFrom.id = orderHead.PartyFrom.Code;
        partyFrom.code = orderHead.PartyFrom.Code;
        partyFrom.description = orderHead.PartyFrom.Name;

        if (orderHead.ShipFrom != null)
        {
            partyFrom.address = orderHead.ShipFrom.Address;
            partyFrom.contact = orderHead.ShipFrom.ContactPersonName;
            partyFrom.telephone = orderHead.ShipFrom.TelephoneNumber;
            partyFrom.mobilephone = orderHead.ShipFrom.MobilePhone;
            partyFrom.fax = orderHead.ShipFrom.Fax;
            partyFrom.postCode = orderHead.ShipFrom.PostalCode;
        }

        com.Sconit.Entity.Svp.Party partyTo = new com.Sconit.Entity.Svp.Party();
        partyTo.id = orderHead.PartyTo.Code;
        partyTo.code = orderHead.PartyTo.Code;
        partyTo.description = orderHead.PartyTo.Name;

        if (orderHead.ShipTo != null)
        {
            partyTo.address = orderHead.ShipTo.Address;
            partyTo.contact = orderHead.ShipTo.ContactPersonName;
            partyTo.telephone = orderHead.ShipTo.TelephoneNumber;
            partyTo.mobilephone = orderHead.ShipTo.MobilePhone;
            partyTo.fax = orderHead.ShipTo.Fax;
            partyTo.postCode = orderHead.ShipTo.PostalCode;
        }

        order.id = orderHead.OrderNo;
        order.requisitionOrderNo = orderHead.OrderNo;
        order.type = orderHead.Type;
        order.orderType = orderHead.SubType;
        order.priority = orderHead.Priority;
        order.createUser = createUser;
        order.createDate = orderHead.CreateDate;
        order.createDateSpecified = true;
        order.effectiveDate = orderHead.StartTime;
        order.effectiveDateSpecified = true;
        order.demandDeliverDate = orderHead.WindowTime;
        order.demandDeliverDateSpecified = true;

        string t = (orderHead.DockDescription == null || orderHead.DockDescription == string.Empty) ?
            orderHead.PartyTo.Code : orderHead.PartyTo.Code + "@" + orderHead.DockDescription;

        order.demandDeliverAddr = orderHead.PartyTo.Name + " " + t + " "
            + (orderHead.DockDescription != null ? orderHead.DockDescription : string.Empty);

        order.partyFrom = partyFrom;
        order.partyTo = partyTo;
        if (orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
        {
            order.status = "In_Process";
        }else{
        order.status = orderHead.Status;
        }
        order.planner = planner;

        order.print = orderHead.IsPrinted;
        order.printSpecified = true;

        return order;
    }

    private RequisitionOrderItem fillOrderDetail(OrderDetail orderDetail)
    {

        RequisitionOrderItem item = new RequisitionOrderItem();

        
        item.id = orderDetail.Id.ToString();
        item.sequenceNo = orderDetail.Sequence;
        item.sequenceNoSpecified = true;
        item.memo = string.Empty;
        item.requiredQuantity = Convert.ToDouble(orderDetail.OrderedQty);
        item.requiredQuantitySpecified = true;
        item.deliveredQuantity = 0;
        if (orderDetail.ShippedQty != null)
        {
            item.deliveredQuantity = Convert.ToDouble(orderDetail.ShippedQty);
        }
        item.deliveredQuantitySpecified = true;
        item.receivedQuantity = 0;
        if (orderDetail.ReceivedQty != null)
        {
            item.receivedQuantity = Convert.ToDouble(orderDetail.ReceivedQty);
        }
        item.receivedQuantitySpecified = true;
        item.unitCount = Convert.ToDouble(orderDetail.UnitCount);
        item.unitCountSpecified = true;

        UOM u = new UOM();
        u.abbreviation = orderDetail.Uom.Code;
        u.description = orderDetail.Uom.Description;

        Material m = new Material();
        m.quantityUOM = u;
        m.id = orderDetail.Item.Code;
        m.no = orderDetail.Item.Code;
        m.name = orderDetail.Item.Description;
        m.description = orderDetail.Item.Description;

        Material supM = new Material();
        supM.quantityUOM = u;
        supM.id = orderDetail.ReferenceItemCode;
        supM.no = orderDetail.ReferenceItemCode;
        supM.name = orderDetail.ReferenceItemCode;
        supM.description = orderDetail.ReferenceItemCode;

        string2MaterialMapEntry[] mapEntry = new string2MaterialMapEntry[1];
        mapEntry[0] = new string2MaterialMapEntry();
        mapEntry[0].key = string.Empty;
        mapEntry[0].value = supM;
        m.supplierMaterials = mapEntry;

        item.material = m;

        return item;
    }
}
