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
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;

[WebService(Namespace = "http://mm.service.integration.symphony")]
[SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.SoapAction)]
[System.Web.Services.WebServiceBindingAttribute(Name = "ReceivingNoteServiceServiceSoapBinding", Namespace = "http://mm.service.integration.symphony")]
public class ReceivingNoteMgrWS : BaseWS
{

    private static Dictionary<string, string> receivingNoteOrderbyFiledsMapping = new Dictionary<string, string>();
    private static Dictionary<string, string> receivingNoteDetailOrderbyFiledsMapping = new Dictionary<string, string>();
    static ReceivingNoteMgrWS()
    {
        receivingNoteOrderbyFiledsMapping.Add("CustPartyCode", "PartyTo.Code");
        receivingNoteOrderbyFiledsMapping.Add("ReceiveDate", "CreateDate");
        receivingNoteOrderbyFiledsMapping.Add("ReceivingNo", "ReceiptNo");

        receivingNoteDetailOrderbyFiledsMapping.Add("CustPartyCode", "PartyTo.Code");
        receivingNoteDetailOrderbyFiledsMapping.Add("ReceivingNo", "ReceiptNo");
        receivingNoteDetailOrderbyFiledsMapping.Add("ReceiveQuantity", "ReceivedQty");
    }

    private static string TRANSACTION_TYPE = "'RCT-PO', 'RCT-PO-ADJ', 'RCT-PO-RT', 'RCT-TR', 'RCT-TR-ADJ', 'RCT-TR-RT', 'RCT-CN', 'RCT-CN-ADJ', 'RCT-CN-RT' ";


    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getReceivingNoteDetail", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "getReceivingNoteDetail")]
    [return: System.Xml.Serialization.XmlArrayAttribute("return")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute(Namespace = "http://mm.domain.integration.symphony")]
    public ReceivingNoteItem[] getReceivingNoteDetail(GetReceivingNoteDetailRequest request)
    {
        DetachedCriteria criteria = DetachedCriteria.For<ReceiptDetail>();
        criteria.Add(Expression.Eq("ReceiptNo",request.receivingNo));
        IList<ReceiptDetail> reciptDetailList = TheCriteriaMgr.FindAll<ReceiptDetail>(criteria);
        List<ReceivingNoteItem> receivingNoteItemList = new List<ReceivingNoteItem>();
        foreach (ReceiptDetail receiptDetail in reciptDetailList)
        {
            ReceivingNoteItem item = new ReceivingNoteItem();

            //set unit of messure
            UOM uom = new UOM();
            uom.id = receiptDetail.OrderLocationTransaction.Item.Uom.Code;
            uom.abbreviation = receiptDetail.OrderLocationTransaction.Item.Uom.Name;
            uom.description = receiptDetail.OrderLocationTransaction.Item.Uom.Name;

            //set supplier Item
            Material suppItem = new Material();
            suppItem.id = receiptDetail.OrderLocationTransaction.OrderDetail.ReferenceItemCode;
            suppItem.no = receiptDetail.OrderLocationTransaction.OrderDetail.ReferenceItemCode;
            suppItem.description = receiptDetail.OrderLocationTransaction.OrderDetail.ReferenceItemCode;
            suppItem.quantityUOM = uom;

            //set Item
            Material material = new Material();
            material.id = receiptDetail.OrderLocationTransaction.Item.Code;
            material.no = receiptDetail.OrderLocationTransaction.Item.Code;
            material.name = receiptDetail.OrderLocationTransaction.Item.Description;
            material.description = receiptDetail.OrderLocationTransaction.Item.Description;
            material.quantityUOM = uom;

            item.material = material;


            item.receiveQuantity = Convert.ToDouble(receiptDetail.ReceivedQty);
            item.receiveQuantitySpecified = true;
            
            //不知道怎么取
            item.totalBillingQuantity = Convert.ToDouble(receiptDetail.ReceivedQty);
            item.totalBillingQuantitySpecified = true;
            item.billingStatus = string.Empty;

            item.unitCount = Convert.ToDouble(receiptDetail.OrderLocationTransaction.OrderDetail.UnitCount);
            item.unitCountSpecified = true;
        }
        return null;
    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getReceivingNoteCount", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "getReceivingNoteCount")]
    [return: System.Xml.Serialization.XmlElementAttribute("return")]
    public int getReceivingNoteCount(GetReceivingNoteRequest request)
    {
        DetachedCriteria criteria = DetachedCriteria.For<Receipt>().SetProjection(Projections.ProjectionList()
               .Add(Projections.Count("ReceiptNo")));

        if (request.supplier != null && request.supplier != string.Empty)
        {
            criteria.Add(Expression.Like("PartyFrom.Name", request.supplier));
        }
        if (request.supplierPartyCode != null && request.supplierPartyCode != string.Empty)
        {
            criteria.Add(Expression.Eq("PartyFrom.Code", request.supplierPartyCode));
        }
        if (request.customerPartyCode != null && request.customerPartyCode != string.Empty)
        {
            criteria.Add(Expression.Eq("PartyTo.Code", request.customerPartyCode));
        }

        if (request.customer != null && request.customer != string.Empty)
        {
            criteria.Add(Expression.Like("PartyTo.Name", request.customer));
        }

        if (request.receivingNo != null && request.receivingNo != string.Empty)
        {
            criteria.Add(Expression.Eq("ReceiptNo", request.receivingNo));
        }

        if (request.receiveDateFromSpecified)
        {
            criteria.Add(Expression.Ge("CreateDate", request.receiveDateFrom));
        }

        if (request.receiveDateToSpecified)
        {
            criteria.Add(Expression.Le("CreateDate", request.receiveDateTo));
        }

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

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getReceivingNote", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "getReceivingNote")]
    [return: System.Xml.Serialization.XmlArrayAttribute("return")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute(Namespace = "http://mm.domain.integration.symphony")]
    public ReceivingNote[] getReceivingNote(GetReceivingNoteRequest request)
    {

        DetachedCriteria criteria = DetachedCriteria.For<Receipt>();
        criteria.AddOrder(Order.Desc("ReceiptNo"));

        if (request.supplier != null && request.supplier != string.Empty)
        {
            criteria.Add(Expression.Like("PartyFrom.Name", request.supplier));
        }
        if (request.supplierPartyCode != null && request.supplierPartyCode != string.Empty)
        {
            criteria.Add(Expression.Eq("PartyFrom.Code", request.supplierPartyCode));
        }
        if (request.customerPartyCode != null && request.customerPartyCode != string.Empty)
        {
            criteria.Add(Expression.Eq("PartyTo.Code", request.customerPartyCode));
        }

        if (request.customer != null && request.customer != string.Empty)
        {
            criteria.Add(Expression.Like("PartyTo.Name", request.customer));
        }

        if (request.receivingNo != null && request.receivingNo != string.Empty)
        {
            criteria.Add(Expression.Eq("ReceiptNo", request.receivingNo));
        }

        if (request.receiveDateFromSpecified)
        {
            criteria.Add(Expression.Ge("CreateDate", request.receiveDateFrom));
        }

        if (request.receiveDateToSpecified)
        {
            criteria.Add(Expression.Le("CreateDate", request.receiveDateTo));
        }


        IList<Receipt> receiptList = TheCriteriaMgr.FindAll<Receipt>(criteria);

        List<ReceivingNote> noteList = new List<ReceivingNote>();
        ReceivingNote note = new ReceivingNote();
        foreach (Receipt receipt in receiptList)
        {
            note.id = receipt.ReceiptNo;
            note.receivingNo = receipt.ReceiptNo;
            note.receiveDate = receipt.CreateDate;
            note.receiveDateSpecified = true;

            DeliveryOrder deliveryOrder = new DeliveryOrder();
            deliveryOrder.deliveryOrderNo = receipt.ReferenceIpNo;
            note.deliveryOrder = deliveryOrder;

            com.Sconit.Entity.Svp.Party partyFrom = new com.Sconit.Entity.Svp.Party();
            partyFrom.code = receipt.PartyFrom.Code;
            partyFrom.id = receipt.PartyFrom.Code;
            partyFrom.description = receipt.PartyFrom.Name;
            note.partyFrom = partyFrom;

            com.Sconit.Entity.Svp.Party partyTo = new com.Sconit.Entity.Svp.Party();
            partyTo.code = receipt.PartyTo.Code;
            partyTo.id = receipt.PartyTo.Code;
            partyTo.description = receipt.PartyTo.Name;
            note.partyTo = partyTo;

            com.Sconit.Entity.Svp.User receiveUser = new com.Sconit.Entity.Svp.User();
            receiveUser.id = receipt.CreateUser.Code;
            receiveUser.name = receipt.CreateUser.Name;
            note.receiveUser = receiveUser;

            noteList.Add(note);
        }

        return noteList.ToArray();

    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getReceivingNoteDetailCount", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "getReceivingNoteDetailCount")]
    [return: System.Xml.Serialization.XmlElementAttribute("return")]
    public int getReceivingNoteDetailCount(GetReceivingNoteDetailRequest requset)
    {
        DetachedCriteria criteria = DetachedCriteria.For<ReceiptDetail>().SetProjection(Projections.ProjectionList()
             .Add(Projections.Count("ReceiptNo")));

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
}
