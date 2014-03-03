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
using System.Collections.Generic;
using com.Sconit.Service.MasterData;
using System.Text.RegularExpressions;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;

/// <summary>
/// Summary description for SearchModuleBase
/// </summary>
namespace com.Sconit.Web
{
    public abstract class SearchModuleBase : ModuleBase
    {
        private INamedQueryMgr TheNamedQueryMgr
        {
            get
            {
                return GetService<INamedQueryMgr>("NamedQueryMgr.service");
            }
        }

        public SearchModuleBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected abstract void InitPageParameter(IDictionary<string, string> parameters);

        protected abstract void DoSearch();

        public void QuickSearch(IDictionary<string, string> parameters)
        {
            InitPageParameter(parameters);
            DoSearch();
        }

        protected void SaveNamedQuery(string queryName, IDictionary<string, string> searchParameters)
        {
            NamedQuery namedQuery = new NamedQuery();
            namedQuery.User = this.CurrentUser;
            namedQuery.QueryName = queryName;
            string mid = this.Page.Request.QueryString["mid"];
            string[] segment = Regex.Split(mid, "__", RegexOptions.IgnoreCase);
            namedQuery.UserControlPath = segment[0];
            for (int i = 1; i < segment.Length; i++)
            {
                if (segment[i].Substring(0, segment[i].IndexOf("--")) == "mp")
                {
                    namedQuery.ModuleParameter = segment[i].Substring(segment[i].IndexOf("--") + 2);
                    break;
                }
            }

            if (searchParameters != null && searchParameters.Count > 0)
            {
                foreach(string key in searchParameters.Keys)
                {
                    if (namedQuery.ActionParameter == null)
                    {
                        namedQuery.ActionParameter = key + "-" + searchParameters[key];
                    }
                    else
                    {
                        namedQuery.ActionParameter += "_" + key + "-" + searchParameters[key];
                    }
                }
            }

            try
            {
                this.TheNamedQueryMgr.CreateNamedQuery(namedQuery);
                this.ShowSuccessMessage("Common.Message.NamedQuery.SaveSuccessful", queryName);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage("Common.Message.NamedQuery.Failed", queryName);
            }
        }
    }
}
