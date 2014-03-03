using System;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using System.Collections.Generic;

namespace com.Sconit.Utility
{

    /// <summary>
    /// Summary description for SessionHelper
    /// </summary>
    public class SessionHelper
    {
        private readonly int UserSelectCriteriaCacheSize = 5;
        private System.Web.UI.Page _page;

        public SessionHelper(Page page)
        {
            _page = page;
        }

        public void AddUserSelectCriteria(string cacheKey, DetachedCriteria selectCriteria, DetachedCriteria selectCountCriteria)
        {
            AddUserSelectCriteria(cacheKey, selectCriteria, selectCountCriteria, null);
        }

        public void AddUserSelectCriteria(string cacheKey, DetachedCriteria selectCriteria, DetachedCriteria selectCountCriteria, IDictionary<string, string> alias)
        {
            if (_page.Session["SelectCriteriaCache"] == null)
            {
                _page.Session["SelectCriteriaCache"] =  new Dictionary<string, CachedSelectCriteria>();
            }

            IDictionary<string, CachedSelectCriteria> selectCriteriaCache = (IDictionary<string, CachedSelectCriteria>)_page.Session["SelectCriteriaCache"];
            if (!selectCriteriaCache.ContainsKey(cacheKey) && selectCriteriaCache.Count > UserSelectCriteriaCacheSize) 
            {
                //CacheÇå³ý×îÔçµÄUserSelectCriteria
                DateTime lastestTime = DateTime.Now;
                string lastestkey = string.Empty;
                foreach (string key in selectCriteriaCache.Keys)
                {
                    
                    CachedSelectCriteria cache = selectCriteriaCache[key];
                    if (lastestTime > cache.LastAccessTime)
                    {
                        lastestkey = key;
                    }
                }
                selectCriteriaCache.Remove(lastestkey);
            }

            if (selectCriteriaCache.ContainsKey(cacheKey))
            {
                selectCriteriaCache[cacheKey] = new CachedSelectCriteria(DateTime.Now, selectCriteria, selectCountCriteria, alias);
            }
            else
            {
                selectCriteriaCache.Add(cacheKey, new CachedSelectCriteria(DateTime.Now, selectCriteria, selectCountCriteria, alias));
            }
        }

        public DetachedCriteria GetUserSelectCriteria(string cacheKey)
        {
            if (_page.Session["SelectCriteriaCache"] != null)
            {
                IDictionary<string, CachedSelectCriteria> selectCriteriaCache = (IDictionary<string, CachedSelectCriteria>)_page.Session["SelectCriteriaCache"];
                if (selectCriteriaCache.ContainsKey(cacheKey))
                {
                    CachedSelectCriteria cache = selectCriteriaCache[cacheKey];
                    cache.LastAccessTime = DateTime.Now;

                    return cache.SelectCriteria;
                }
            }

            return null;
        }

        public DetachedCriteria GetUserSelectCountCriteria(string cacheKey)
        {
            if (_page.Session["SelectCriteriaCache"] != null)
            {
                IDictionary<string, CachedSelectCriteria> selectCriteriaCache = (IDictionary<string, CachedSelectCriteria>)_page.Session["SelectCriteriaCache"];
                if (selectCriteriaCache.ContainsKey(cacheKey))
                {
                    CachedSelectCriteria cache = selectCriteriaCache[cacheKey];
                    cache.LastAccessTime = DateTime.Now;

                    return cache.SelectCountCriteria;
                }
            }

            return null;
        }

        public IDictionary<string, string> GetUserAlias(string cacheKey)
        {
            if (_page.Session["SelectCriteriaCache"] != null)
            {
                IDictionary<string, CachedSelectCriteria> selectCriteriaCache = (IDictionary<string, CachedSelectCriteria>)_page.Session["SelectCriteriaCache"];
                if (selectCriteriaCache.ContainsKey(cacheKey))
                {
                    CachedSelectCriteria cache = selectCriteriaCache[cacheKey];
                    cache.LastAccessTime = DateTime.Now;

                    return cache.Alias;
                }
            }

            return null;
        }

        public User CurrentUser
        {
            get
            {
                if (_page.Session["Current_User"] == null)
                {
                    FormsAuthentication.SignOut();
                    _page.Session.RemoveAll();

                    //this code for the request redirect. need to refactor.
                    if (GetQueryString(_page).Length > 0)
                    {
                        _page.Session["RequestUrl"] = "Default.aspx" + "?" + GetQueryString(_page);
                    }
                    _page.Response.Redirect("Logoff.aspx");
                }

                return _page.Session["Current_User"] as User;
            }
            set
            {
                _page.Session["Current_User"] = value;
            }
        }

        /*
        public string CurrentModuleName
        {
            get
            {
                if (_page.Session["CurrentModuleName"] == null)
                {
                    FormsAuthentication.SignOut();
                    _page.Session.RemoveAll();
                    _page.Response.Redirect("Login.aspx");
                }

                return _page.Session["CurrentModuleName"] as string;
            }
            set
            {
                _page.Session["CurrentModuleName"] = value;
            }
        }
         * */

        private string GetQueryString(Page page)
        {
            string queryString = "";

            NameValueCollection qs = page.Request.QueryString;

            foreach (string key in qs.AllKeys)
                foreach (string value in qs.GetValues(key))
                    queryString += page.Server.UrlEncode(key) + "=" + page.Server.UrlEncode(value) + "&";

            return queryString.TrimEnd('&');
        }
    }

    class CachedSelectCriteria
    {
        private DateTime lastAccessTime;
        public DateTime LastAccessTime
        {
            get
            {
                return lastAccessTime;
            }
            set
            {
                lastAccessTime = value;
            }
        }
        private DetachedCriteria selectCriteria;
        public DetachedCriteria SelectCriteria
        {
            get
            {
                return selectCriteria;
            }
            set
            {
                selectCriteria = value;
            }
        }
        private DetachedCriteria selectCountCriteria;
        public DetachedCriteria SelectCountCriteria
        {
            get
            {
                return selectCountCriteria;
            }
            set
            {
                selectCountCriteria = value;
            }
        }
        private IDictionary<string, string> aliasDict;
        public IDictionary<string, string> Alias
        {
            get
            {
                return aliasDict;
            }
            set
            {
                aliasDict = value;
            }
        }
        public CachedSelectCriteria(DateTime lastAccessTime, DetachedCriteria selectCriteria, DetachedCriteria selectCountCriteria)
        {
            this.lastAccessTime = lastAccessTime;
            this.selectCriteria = selectCriteria;
            this.selectCountCriteria = selectCountCriteria;
        }

        public CachedSelectCriteria(DateTime lastAccessTime, DetachedCriteria selectCriteria, DetachedCriteria selectCountCriteria, IDictionary<string, string> alias)
        {
            this.lastAccessTime = lastAccessTime;
            this.selectCriteria = selectCriteria;
            this.selectCountCriteria = selectCountCriteria;
            this.Alias = alias;
        }
    }

}