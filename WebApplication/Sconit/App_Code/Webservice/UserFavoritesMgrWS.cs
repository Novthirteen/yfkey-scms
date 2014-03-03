using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Service.MasterData;
using System.Web.Services;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using System.IO;


/// <summary>
/// Summary description for UserFavoriteMgrWS
/// </summary>
[WebService(Namespace = "http://com.Sconit.Webservice/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class UserFavoritesMgrWS : BaseWS
{
    public UserFavoritesMgrWS()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [WebMethod]
    public void AddUserFavorite(string userCode, string type, string pageName, string pageUrl, string pageImage, bool hasPermission)
    {
        if (TheFavoritesMgr.CheckFavoritesUniqueExist(userCode, type, pageName))
        {
            TheFavoritesMgr.DeleteFavorites(userCode, type, pageName);
        }
        if (hasPermission)
        {
            Favorites fav = new Favorites();
            fav.User = TheUserMgr.LoadUser(userCode);
            fav.Type = type;
            fav.PageName = pageName;
            fav.PageUrl = pageUrl;
            fav.PageImage = pageImage;
            TheFavoritesMgr.CreateFavorites(fav);
        }
    }

    [WebMethod]
    public string ListUserFavorites(string userCode, string type)
    {
        string listf = string.Empty;
        string icon = string.Empty;
        IList<Favorites> ifavorites = TheFavoritesMgr.GetFavorites(userCode, type);
        int count = int.Parse(TheEntityPreferenceMgr.LoadEntityPreference("HistoryNo").Value);
        count = count < ifavorites.Count ? count : ifavorites.Count;
        for (int i = 0; i < count; i++)
        {
            Favorites fav = ifavorites[i];
            icon = fav.PageImage;
            if (!File.Exists(Server.MapPath("~/Images/Nav/" + fav.PageImage + ".png")))
            {
                icon = "Default";
            }
            icon = "<img src = 'Images/Nav/" + icon + ".png' />";
            listf += "<li class='div-favorite'><span onclick ='DeleteFavorite(" + fav.Id + ")'>XX</span>" + icon;
            listf += "<a href = 'Main.aspx" + fav.PageUrl + "' target = right title = " + fav.PageImage + ">" + fav.PageName + "</a></li>";
        }
        return listf;
    }

    [WebMethod]
    public void DeleteUserFavorite(int id)
    {
        TheFavoritesMgr.DeleteFavorites(id);
    }
}
