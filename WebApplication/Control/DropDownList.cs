using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI;
using System.Web.Compilation;
using com.Sconit.Utility;
using System.Collections;

namespace com.Sconit.Control
{
    [ToolboxData("<{0}:DropDownList runat=server></{0}:DropDownList>")]
    public class DropDownList : System.Web.UI.WebControls.DropDownList
    {
        #region Properties
        [Category("Custmize")]
        [Description("Description Field")]
        [Browsable(true)]
        public string DescField
        {
            get
            {
                return (ViewState["DescField"] != null ? (string)ViewState["DescField"] : "");
            }
            set
            {
                ViewState["DescField"] = value;
            }
        }

        [Category("Custmize")]
        [Description("Value Field")]
        [Browsable(true)]
        public string ValueField
        {
            get
            {
                return (ViewState["ValueField"] != null ? (string)ViewState["ValueField"] : "");
            }
            set
            {
                ViewState["ValueField"] = value;
            }
        }

        [Category("Custmize")]
        [Description("Default Selected Value")]
        [Browsable(true)]
        public string DefaultSelectedValue
        {
            get
            {
                return (string)ViewState["DefaultSelectedValue"];
            }
            set
            {
                ViewState["DefaultSelectedValue"] = value;
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

        [Category("Custmize")]
        [Description("Service Parameter")]
        [Browsable(true)]
        public string ServiceParameter
        {
            get
            {
                return (ViewState["ServiceParameter"] != null ? (string)ViewState["ServiceParameter"] : "");
            }
            set
            {
                ViewState["ServiceParameter"] = value;
            }
        }

        #endregion

        #region Constructor & Dispose
        public DropDownList()
        {
            this.Init += new EventHandler(DropDownList_Init);
        }

        void DropDownList_Init(object sender, EventArgs e)
        {
            InitList();
        }

        protected virtual void InitList()
        {
            this.Items.Clear();

            this.DataSource = ReflectHelper.InvokeServiceMethod(this.ServicePath, this.ServiceMethod, this.ServiceParameter);
            this.DataTextField = this.DescField;
            this.DataValueField = this.ValueField;
            if (this.DefaultSelectedValue != null && this.DefaultSelectedValue != string.Empty)
            {
                this.SelectedValue = this.DefaultSelectedValue;
            }
            this.DataBind();
        }

        #endregion

        
    }
}
