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
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Exception;
using com.Sconit.Service.Distribution;


/// <summary>
/// Summary description for SmartDeviceMgrWS
/// </summary>
[WebService(Namespace = "http://com.Sconit.Webservice")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SmartDeviceMgrWS : BaseWS
{
    private static log4net.ILog log;

    public SmartDeviceMgrWS()
    {

        log4net.Config.XmlConfigurator.Configure();
        log = log4net.LogManager.GetLogger("Log.Webservice");
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public virtual Resolver ScanBarcode(Resolver resolver)
    {
        try
        {
            resolver.IsCSClient = false;
            resolver = TheResolverMgr.Resolve(resolver);
            return resolver;
        }
        catch (BusinessErrorException ex)
        {
            string exMessage = RenderingLanguage(ex.Message, resolver.UserCode, ex.MessageParams);
            throw new SoapException(exMessage, SoapException.ServerFaultCode, string.Empty);
        }
        catch (Exception ex)
        {
            //throw new Exception("", ex);
            //log.Error("Kill Excell Process fail", ex);
            throw new SoapException(ex.Message, SoapException.ServerFaultCode, string.Empty);
        }
    }

    #region User
    [WebMethod]
    public User LoadUser(string userCode)
    {
        try
        {
            User user = TheUserMgr.LoadUser(userCode, false, false);
            return user;
        }
        catch (BusinessErrorException ex)
        {
            string exMessage = RenderingLanguage(ex.Message, userCode, ex.MessageParams);
            throw new SoapException(exMessage, SoapException.ServerFaultCode, string.Empty);
        }
    }

    [WebMethod]
    public Permission[] GetUserPermissions(string categoryCode, string userCode)
    {
        try
        {
            User user = TheUserMgr.LoadUser(userCode);
            return ThePermissionMgr.GetALlPermissionsByCategory(categoryCode, user).ToArray();
        }
        catch (BusinessErrorException ex)
        {
            string exMessage = RenderingLanguage(ex.Message, userCode, ex.MessageParams);
            throw new SoapException(exMessage, SoapException.ServerFaultCode, string.Empty);
        }
    }

    [WebMethod]
    public UserPreference LoadUserPerference(string userCode, string preCode)
    {
        try
        {
            return TheUserPreferenceMgr.LoadUserPreference(userCode, preCode);
        }
        catch (BusinessErrorException ex)
        {
            string exMessage = RenderingLanguage(ex.Message, userCode, ex.MessageParams);
            throw new SoapException(exMessage, SoapException.ServerFaultCode, string.Empty);
        }
    }

    #endregion

}

