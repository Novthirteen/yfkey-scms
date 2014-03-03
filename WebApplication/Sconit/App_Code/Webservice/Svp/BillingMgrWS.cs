using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using System.Web.Services.Description;

using com.Sconit.Entity.Svp;
using com.Sconit.Entity.Svp.Condition;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Svp;
using NHibernate.Expression;
using com.Sconit.Utility;

/// <summary>
/// Summary description for BillingServiceImpl
/// </summary>
[WebService(Namespace = "http://mm.service.integration.symphony")]
[SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.SoapAction)]
[System.Web.Services.WebServiceBindingAttribute(Name = "BillingServiceServiceSoapBinding", Namespace = "http://mm.service.integration.symphony")]
public class BillingMgrWS : BaseWS
{

    #region 排序字段定义和初始化
    private static Dictionary<string, string> orderbyFiledsMapping = new Dictionary<string, string>();
    static BillingMgrWS()
    {
        orderbyFiledsMapping.Add("BillingNo", "billCode");
        orderbyFiledsMapping.Add("PartyTo", "supplierCode");
        orderbyFiledsMapping.Add("BillingDate", "createDate");      //没有开票日期，用createdate代替	
        orderbyFiledsMapping.Add("CreateDate", "createDate");
        orderbyFiledsMapping.Add("CreateUser", "createUserName");
    }
    #endregion

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getBilling", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "getBilling")]
    [return: System.Xml.Serialization.XmlArrayAttribute("return")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute(Namespace = "http://mm.domain.integration.symphony")]
    public Billing[] getBilling(GetBillingRequest request)
    {
        DetachedCriteria criteria = DetachedCriteria.For<Bill>();

        if (request.orderby != null)
        {
            RequestOrderByHelper.ConverToCriteria(request.orderby, orderbyFiledsMapping, criteria);
        }

        IList<Bill> billList = TheCriteriaMgr.FindAll<Bill>(criteria, request.beginRowIndex, request.beginRowIndex + request.rowSize);
        List<Billing> billingList = new List<Billing>();

        foreach (Bill bill in billList)
        {
            Billing billing = fillBill(bill);
            foreach (BillDetail billDetail in bill.BillDetails)
            {
                BillingItem billingItem = fillBillDetail(billDetail);
                billing.AddBillingItem(billingItem);
            }
            billingList.Add(billing);
        }

        return billingList.ToArray();
      
    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getBillingCount", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "getBillingCount")]
    [return: System.Xml.Serialization.XmlElementAttribute("return")]
    public int getBillingCount(GetBillingRequest request)
    {
        DetachedCriteria criteria = DetachedCriteria.For<Bill>().SetProjection(Projections.ProjectionList()
               .Add(Projections.Count("OrderNo")));
        IList list = TheCriteriaMgr.FindAll(criteria, request.beginRowIndex, request.beginRowIndex + request.rowSize);

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


    private Billing fillBill(Bill bill)
    {

        com.Sconit.Entity.Svp.Party supplier = new com.Sconit.Entity.Svp.Party();
        supplier.id = bill.BillAddress.Party.Code;
        supplier.code = bill.BillAddress.Party.Code;
        supplier.description = bill.BillAddress.Party.Name;

        supplier.address = bill.BillAddress.Code;
        supplier.contact = bill.BillAddress.ContactPersonName;
        supplier.telephone = bill.BillAddress.TelephoneNumber;
        supplier.mobilephone = bill.BillAddress.MobilePhone;
        supplier.fax = bill.BillAddress.Fax;
        supplier.postCode = bill.BillAddress.PostalCode;

        com.Sconit.Entity.Svp.User user = new com.Sconit.Entity.Svp.User();
        user.name = bill.CreateUser.Name;

        com.Sconit.Entity.Svp.Billing billing = new com.Sconit.Entity.Svp.Billing();
        billing.partyFrom = supplier;
        billing.id = bill.BillNo;
        billing.billingNo = bill.BillNo;
        billing.billingDateSpecified = true;
        billing.billingDate = bill.CreateDate;
        billing.createDateSpecified = true;
        billing.createDate = bill.CreateDate;
        billing.createUser = user;

        return billing;
    }

    private BillingItem fillBillDetail(BillDetail billDetail)
    {
        com.Sconit.Entity.Svp.BillingItem item = new com.Sconit.Entity.Svp.BillingItem();

        item.memo = string.Empty;
        item.billingQuantity =Convert.ToDouble(billDetail.BilledQty);
        item.unitPrice = billDetail.UnitPrice;
        item.unitPriceSpecified = true;

       UOM u = new UOM();
        u.abbreviation = billDetail.ActingBill.Uom.Code;
        u.description = billDetail.ActingBill.Uom.Description;

        Material m = new Material();
        m.quantityUOM = u;
        m.id = billDetail.ActingBill.Item.Code;
        m.no = billDetail.ActingBill.Item.Code;
        m.name = billDetail.ActingBill.Item.Description;
        m.description = billDetail.ActingBill.Item.Description;

        RequisitionOrder requisitionOrder = new RequisitionOrder();
        requisitionOrder.requisitionOrderNo = billDetail.ActingBill.OrderNo;

        DeliveryOrder deliveryOrder = new DeliveryOrder();
        deliveryOrder.requisitionOrder = requisitionOrder;

        ReceivingNote receiveNote = new ReceivingNote();
        receiveNote.id = billDetail.ActingBill.ReceiptNo;
        receiveNote.receivingNo = billDetail.ActingBill.ReceiptNo;
        receiveNote.deliveryOrder = deliveryOrder;

        ReceivingNoteItem receiveItem = new ReceivingNoteItem();
        receiveItem.material = m;
        receiveItem.receiveQuantity = Convert.ToDouble(billDetail.BilledQty);
        receiveItem.receiveQuantitySpecified = true;
        receiveItem.totalBillingQuantity = Convert.ToDouble( billDetail.BilledQty);
        receiveItem.totalBillingQuantitySpecified = true;
        receiveItem.receivingNote = receiveNote;

        item.material = m;
        item.receivingNoteItem = receiveItem;

        return item;
    }
}
