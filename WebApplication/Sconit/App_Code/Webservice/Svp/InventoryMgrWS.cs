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
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Service.View;
using com.Sconit.Entity.View;

[WebService(Namespace = "http://mm.service.integration.symphony")]
[SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.SoapAction)]
[System.Web.Services.WebServiceBindingAttribute(Name = "InventoryServiceServiceSoapBinding", Namespace = "http://mm.service.integration.symphony")]
public class InventoryMgrWS : BaseWS
{
    protected static Dictionary<string, string> orderbyFiledsMapping = new Dictionary<string, string>();
    static InventoryMgrWS()
    {
        orderbyFiledsMapping.Add("ItemCode", "itemCode");
        orderbyFiledsMapping.Add("SupplierCode", "supplierCode");
        orderbyFiledsMapping.Add("LocationCode", "locationCode");
        orderbyFiledsMapping.Add("PartyCode", "partyCode");
    }


    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getSupplierInventory", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "getSupplierInventory")]
    [return: System.Xml.Serialization.XmlArrayAttribute("return")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute(Namespace = "http://mm.domain.integration.symphony")]
    public InventoryItem[] getSupplierInventory(GetSupplierInventoryRequest request)
    {
        DetachedCriteria criteria = DetachedCriteria.For<SupllierLocationView>();


        if (request.supplierCode != null && request.supplierCode != string.Empty)
        {
            criteria.Add(Expression.Like("PartyFrom.Code", request.supplierCode));
        }

        if (request.company != null && request.company != string.Empty)
        {
            criteria.Add(Expression.Like("PartyFrom.Name", request.company));
        }

        IList<SupllierLocationView> supplierLocationList = TheCriteriaMgr.FindAll<SupllierLocationView>(criteria, request.beginRowIndex, request.beginRowIndex + request.rowSize);
        List<InventoryItem> iItemList = new List<InventoryItem>();

        foreach (SupllierLocationView supplierLocation in supplierLocationList)
        {

            //set region					
            com.Sconit.Entity.Svp.Party party = new com.Sconit.Entity.Svp.Party();
            party.code = supplierLocation.PartyTo.Code;
            party.id = supplierLocation.PartyTo.Code;
            party.description = supplierLocation.PartyTo.Name;
            party.type = supplierLocation.PartyTo.Type;

            //set supplier
            com.Sconit.Entity.Svp.Party supplier = new com.Sconit.Entity.Svp.Party();
            supplier.id = supplierLocation.PartyFrom.Code;
            supplier.code = supplierLocation.PartyFrom.Code;
            supplier.description = supplierLocation.PartyFrom.Name;
            supplier.type = supplierLocation.PartyFrom.Type;

            //set unit of messure
            UOM uom = new UOM();
            uom.id = supplierLocation.Item.Code;
            uom.abbreviation = supplierLocation.Item.Code;
            uom.description = supplierLocation.Item.Description;

            //set supplier Item
            string refItemCode = TheItemReferenceMgr.GetItemReferenceByItem(supplierLocation.Item.Code, supplierLocation.PartyFrom.Code, supplierLocation.PartyTo.Code);
            Material suppItem = new Material();
            suppItem.id = refItemCode;
            suppItem.no = refItemCode;
            suppItem.description = refItemCode;
            suppItem.quantityUOM = uom;

            //set Item
            Material material = new Material();
            material.id = supplierLocation.Item.Code;
            material.no = supplierLocation.Item.Code;
            material.name = supplierLocation.Item.Description;
            material.description = supplierLocation.Item.Description;
            suppItem.quantityUOM = uom;

            //set supplier material
            string2MaterialMapEntry[] suppItemMap = new string2MaterialMapEntry[1];
            suppItemMap[0] = new string2MaterialMapEntry();
            suppItemMap[0].key = supplier.code;
            suppItemMap[0].value = suppItem;
            material.supplierMaterials = suppItemMap;

            //set location
            com.Sconit.Entity.Svp.Location location = new com.Sconit.Entity.Svp.Location();
            location.id = supplierLocation.Location.Code;
            location.name = supplierLocation.Location.Name;

            //set inventory
            InventoryItem iItem = new InventoryItem();
            iItem.ownerParty = party;
            iItem.supplier = supplier;
            iItem.material = material;
            iItem.location = location;
            iItem.quantityOnHand = Convert.ToDouble(supplierLocation.Qty);
            iItem.quantityOnHandSpecified = true;
            iItem.unitCount = Convert.ToDouble(supplierLocation.Item.UnitCount);
            iItem.unitCountSpecified = true;

            iItemList.Add(iItem);
        }

        return iItemList.ToArray();

    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getInventoryCount", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "getInventoryCount")]
    [return: System.Xml.Serialization.XmlElementAttribute("return")]
    public int getInventoryCount(GetInventoryRequest request)
    {
        //不用，不需要
        return 0;
    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getInventory", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "getInventory")]
    [return: System.Xml.Serialization.XmlArrayAttribute("return")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute(Namespace = "http://mm.domain.integration.symphony")]
    public InventoryItem[] getInventory(GetInventoryRequest arg0)
    {
        //不用，不需要
        return null;
    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getSupplierInventoryCount", RequestNamespace = "http://mm.service.integration.symphony", ResponseNamespace = "http://mm.service.integration.symphony", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Action = "getSupplierInventoryCount")]
    [return: System.Xml.Serialization.XmlElementAttribute("return")]
    public int getSupplierInventoryCount(GetSupplierInventoryRequest request)
    {
        DetachedCriteria criteria = DetachedCriteria.For<SupllierLocationView>();


        if (request.supplierCode != null && request.supplierCode != string.Empty)
        {
            criteria.Add(Expression.Like("PartyFrom.Code", request.supplierCode));
        }

        if (request.company != null && request.company != string.Empty)
        {
            criteria.Add(Expression.Like("PartyFrom.Name", request.company));
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

  
}
