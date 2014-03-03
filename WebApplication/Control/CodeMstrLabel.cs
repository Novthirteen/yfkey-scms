using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using com.Sconit.Utility;
using com.Sconit.Entity.MasterData;
using System.ComponentModel;
using com.Sconit.Entity.Exception;

namespace com.Sconit.Control
{
    [ToolboxData("<{0}:CodeMstrLabel runat=server></{0}:CodeMstrLabel>")]
    public class CodeMstrLabel : System.Web.UI.WebControls.Label
    {
        #region Properties
        [Category("Custmize")]
        [Description("Code of CodeMaster")]
        [Browsable(true)]
        public string Code
        {
            get
            {
                return (ViewState["Code"] != null ? (string)ViewState["Code"] : "");
            }
            set
            {
                ViewState["Code"] = value;
            }
        }

        [Category("Custmize")]
        [Description("Value of CodeMaster")]
        [Browsable(true)]
        public string Value
        {
            get
            {
                return (ViewState["Value"] != null ? (string)ViewState["Value"] : "");
            }
            set
            {
                ViewState["Value"] = value;
            }
        }

        [Category("Custmize")]
        [Description("Service Path")]
        [Browsable(true)]
        public string ServicePath
        {
            get
            {
                return (ViewState["ServicePath"] != null ? (string)ViewState["ServicePath"] : "");
            }
            set
            {
                ViewState["ServicePath"] = value;
            }
        }

        [Category("Custmize")]
        [Description("Service Method")]
        [Browsable(true)]
        public string ServiceMethod
        {
            get
            {
                return (ViewState["ServiceMethod"] != null ? (string)ViewState["ServiceMethod"] : "");
            }
            set
            {
                ViewState["ServiceMethod"] = value;
            }
        }
        #endregion

        #region Constructor & Dispose
        public CodeMstrLabel()
        {
            this.PreRender += new EventHandler(Lable_PreRender);
        }

        void Lable_PreRender(object sender, EventArgs e)
        {
            PreRenderText();
        }

        protected virtual void PreRenderText()
        {
            if (this.ServicePath == null || this.ServicePath == string.Empty)
            {
                this.ServicePath = "com.Sconit.Web.CodeMasterMgrProxy";
            }

            if (this.ServiceMethod == null || this.ServiceMethod == string.Empty)
            {
                this.ServiceMethod = "GetCachedCodeMaster";
            }

            if (this.Code == null || this.Code == string.Empty)
            {
                throw new TechnicalException("Code not specified.");
            }

            //if (this.Value == null || this.Value == string.Empty)
            //{
            //    //throw new TechnicalException("Value not specified.");
            //    return;
            //}

            string serviceParameters = "string:" + this.Code + ",string:" + this.Value;

            try
            {
                CodeMaster codeMaster = ReflectHelper.InvokeServiceMethod(this.ServicePath, this.ServiceMethod, serviceParameters) as CodeMaster;
                if (codeMaster != null)
                {
                    //this.Text = codeMaster.Value + " [" + codeMaster.Description + "]";
                    this.Text = codeMaster.Description;
                }
            }
            catch
            {
            }
        }
        #endregion
    }
}
