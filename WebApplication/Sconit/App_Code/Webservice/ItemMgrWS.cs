using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Entity;
using System.Collections.Generic;
/// <summary>
/// Summary description for ItemManagerWS
/// </summary>
[WebService(Namespace = "http://com.Sconit.Webservice/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ItemMgrWS : BaseWS
{  
    public ItemMgrWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public Item GenerateItemProxy(string itemCode)
    {
        Item item = this.TheItemMgr.LoadItem(itemCode);
        Item simpleItem = new Item();
        simpleItem.Code = item.Code;
        simpleItem.Desc1 = item.Desc1;
        simpleItem.Desc2 = item.Desc2;
        simpleItem.Uom = item.Uom;
        return simpleItem;
    }
}

