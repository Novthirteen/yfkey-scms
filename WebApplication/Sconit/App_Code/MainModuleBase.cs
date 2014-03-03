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
using com.Sconit.Web;
using System.Collections.Generic;

/// <summary>
/// Summary description for MainModuleBase
/// </summary>
namespace com.Sconit.Web
{
    public abstract class MainModuleBase : ModuleBase
    {
        protected IDictionary<string, string> ModuleParameter;
        private string _action;
        protected string Action
        {
            get
            {
                return (string)ViewState["Action"];
            }
            set
            {
                ViewState["Action"] = value;
            }
        }

        private IDictionary<string, string> _actionParameter;
        protected IDictionary<string, string> ActionParameter
        {
            get
            {
                return _actionParameter;
            }
            set
            {
                _actionParameter = value;
            }
        }

        public MainModuleBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public MainModuleBase(IDictionary<string, string> mp, string act, IDictionary<string, string> ap)
        {
            this.ModuleParameter = mp;
            this.Action = act;
            this.ActionParameter = ap;
        }

        //public abstract void CallAction();

        //public virtual void CallAction(string act, IDictionary<string, string> ap)
        //{
        //    this.Action = act;
        //    this.ActionParameter = ap;
        //}
    }
}
