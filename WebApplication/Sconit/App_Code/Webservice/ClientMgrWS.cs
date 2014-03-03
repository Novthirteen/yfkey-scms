using System;
using System.Collections;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;

/// <summary>
/// Summary description for ClientMgrWS
/// </summary>
[WebService(Namespace = "http://com.Sconit.Webservice")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ClientMgrWS : SmartDeviceMgrWS
{
    public ClientMgrWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    #region Employee
    [WebMethod]
    public Employee LoadEmployee(string employeeCode)
    {
        try
        {
            return TheEmployeeMgr.LoadEmployee(employeeCode, false);
        }
        catch (BusinessErrorException ex)
        {
            string exMessage = RenderingLanguage(ex.Message, string.Empty, ex.MessageParams);
            throw new SoapException(exMessage, SoapException.ServerFaultCode, string.Empty);
        }
        catch
        {
            return null;
        }
    }

    [WebMethod]
    public override Resolver ScanBarcode(Resolver resolver)
    {
        try
        {
            resolver.IsCSClient = true;
            resolver = TheResolverMgr.Resolve(resolver);

            //KSS 客户化,只有手持扫描枪需要B/S打印监控
            //if ((resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP || resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPORDER)
            //    && resolver.NeedPrintAsn && resolver.PrintUrl != null && resolver.PrintUrl.Trim() != string.Empty)
            //{
            //    //resolver.PrintUrl = null;
            //    InProcessLocation inProcessLocation = TheInProcessLocationMgr.LoadInProcessLocation(resolver.Code);
            //    inProcessLocation.IsPrinted = true;//to be refactored
            //    TheInProcessLocationMgr.UpdateInProcessLocation(inProcessLocation);
            //}
            return resolver;
        }
        catch (BusinessErrorException ex)
        {
            string exMessage = RenderingLanguage(ex.Message, resolver.UserCode, ex.MessageParams);
            throw new SoapException(exMessage, SoapException.ServerFaultCode, string.Empty);
        }
        catch (Exception ex)
        {
            throw new SoapException(ex.Message, SoapException.ServerFaultCode, string.Empty);
        }
    }



    #endregion

}

