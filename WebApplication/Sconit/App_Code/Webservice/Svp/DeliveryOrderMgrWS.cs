using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using com.Sconit.Entity.Svp;
using com.Sconit.Entity.Svp.Condition;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

[WebService(Namespace = "http://sd.service.integration.symphony")]
[SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.SoapAction)]
[System.Web.Services.WebServiceBindingAttribute(Name = "DeliveryOrderServiceServiceSoapBinding", Namespace = "http://sd.service.integration.symphony")]
public class DeliveryOrderMgrWS : BaseWS
{
    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("sendDeliveryOrder", RequestNamespace = "http://sd.service.integration.symphony", ResponseNamespace = "http://sd.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "sendDeliveryOrder")]
    [return: System.Xml.Serialization.XmlElementAttribute("return")]
    public bool sendDeliveryOrder(DeliveryOrder order, string userCode)
    {
        IList<OrderDetail> orderDetailList = new List<OrderDetail>();
        if (order != null && order.deliveryOrderItems != null && order.deliveryOrderItems.Length > 0)
        {
            foreach (DeliveryOrderItem item in order.deliveryOrderItems)
            {
                OrderDetail orderDetail = TheOrderDetailMgr.LoadOrderDetail(int.Parse(item.id));
                orderDetail.CurrentShipQty = Convert.ToDecimal(item.deliveryQuantity);
                orderDetailList.Add(orderDetail);
            }
            TheOrderMgr.ShipOrder(orderDetailList, userCode);
        }


        return true;
    }
}
