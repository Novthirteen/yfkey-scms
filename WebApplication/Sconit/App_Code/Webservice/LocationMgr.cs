using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Utility;

/// <summary>
/// Summary description for LocationMgr
/// </summary>
[WebService(Namespace = "http://com.Sconit.Webservice")]
[SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.SoapAction)]
[System.Web.Services.WebServiceBindingAttribute(Name = "LocationMgr", Namespace = "http://com.Sconit.Webservice")]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class LocationMgr : BaseWS
{
    public LocationMgr()
    {
        
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public LocationDetail[] FindLocationDetail(string[] locationCode, string[] itemCode, DateTime effectiveDate, string userCode)
    {   
        try
        {
            IList<LocationDetail> locationDetailList = TheLocationDetailMgr.FindLocationDetail(IListHelper.ConvertToList(locationCode), IListHelper.ConvertToList(itemCode), effectiveDate, userCode);

            if (locationDetailList != null && locationDetailList.Count > 0)
            {
                return locationDetailList.ToArray();
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw new SoapException(ex.Message, SoapException.ServerFaultCode, Context.Request.Url.AbsoluteUri);
        }
    }


    [WebMethod]
    public LocationTransaction[] FindLocationTransaction(string[] transType,
           string[] loc, DateTime effdateStart, DateTime effDateEnd, DateTime createDateStart, DateTime createDateEnd,
           string partyFrom, string partyTo, string[] itemCode,
           string[] orderNo, string[] recNo, string createUser, string[] ipNo, string userCode)
    {
        try
        {
            IList<LocationTransaction> LocationTransactionList = TheLocationMgr.GetLocationTransaction(transType,
                    loc,  effdateStart,  effDateEnd,  createDateStart,  createDateEnd,
                    partyFrom,  partyTo,  itemCode, orderNo,  recNo,  createUser,  ipNo, userCode);

            if (LocationTransactionList != null && LocationTransactionList.Count > 0)
            {
                return LocationTransactionList.ToArray();
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw new SoapException(ex.Message, SoapException.ServerFaultCode, Context.Request.Url.AbsoluteUri);
        }
    }

    [WebMethod]
    public void RunShipPlan(string userCode)
    {
        GetService<com.Sconit.Service.MRP.IMrpMgr>("MrpMgr.service").RunShipPlan(TheUserMgr.LoadUser(userCode, false, false));
    }
}

