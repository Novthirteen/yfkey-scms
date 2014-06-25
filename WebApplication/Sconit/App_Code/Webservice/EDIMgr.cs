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
using com.Sconit.Service.EDI;
using System.IO;

/// <summary>
/// Summary description for EDIMgr
/// </summary>
[WebService(Namespace = "http://com.Sconit.Webservice")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class EDIMgr : BaseWS
{
    
    public EDIMgr()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void RunBat()
    {
        this.TheEDIMgr.RunBat();
    }

    [WebMethod]
    public void LoadEDI()
    {
        this.TheEDIMgr.LoadEDI(TheUserMgr.GetMonitorUser());
    
    }

    [WebMethod]
    public void TransformationPlan()
    {
        this.TheEDIMgr.TransformationPlan(TheUserMgr.GetMonitorUser());
    
    }

    [WebMethod]
    public void ReadEDIFordPlanASN()
    {
        this.TheEDIMgr.ReadEDIFordPlanASN();
    
    }
    
    

}

