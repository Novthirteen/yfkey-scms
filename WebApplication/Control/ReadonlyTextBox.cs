using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Reflection;
using com.Sconit.Entity.Exception;

namespace com.Sconit.Control
{
    [ToolboxData("<{0}:ReadonlyTextBox runat=server></{0}:ReadonlyTextBox>")]
    public class ReadonlyTextBox : System.Web.UI.WebControls.TextBox
    {
        #region property
        [Category("Custmize")]
        [Description("CodeField")]
        [Browsable(true)]
        public string CodeField
        {
            get
            {
                return (ViewState["CodeField"] != null ? (string)ViewState["CodeField"] : "");
            }
            set
            {
                ViewState["CodeField"] = value;
            }
        }

        [Category("Custmize")]
        [Description("CodeFieldFormat")]
        [Browsable(true)]
        public string CodeFieldFormat
        {
            get
            {
                return (ViewState["CodeFieldFormat"] != null ? (string)ViewState["CodeFieldFormat"] : "");
            }
            set
            {
                ViewState["CodeFieldFormat"] = value;
            }
        }

        [Category("Custmize")]
        [Description("DescField")]
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
        [Description("DescFieldFormat")]
        [Browsable(true)]
        public string DescFieldFormat
        {
            get
            {
                return (ViewState["DescFieldFormat"] != null ? (string)ViewState["DescFieldFormat"] : "");
            }
            set
            {
                ViewState["DescFieldFormat"] = value;
            }
        }

        [Category("Custmize")]
        [Description("ShowDescFieldOnly")]
        [Browsable(true)]
        public Boolean ShowDescFieldOnly
        {
            get
            {
                return (Boolean)ViewState["ShowDescFieldOnly"];
            }
            set
            {
                ViewState["ShowDescFieldOnly"] = value;
            }
        }
        #endregion

        public ReadonlyTextBox()
        {
            this.DataBinding += new EventHandler(ReadonlyTextBox_DataBinding);
            this.ReadOnly = true;
            this.ShowDescFieldOnly = false;
        }

        public void ReadonlyTextBox_DataBind(string code, string desc)
        {
            code = code != null ? code.Trim() : string.Empty;
            desc = desc != null ? desc.Trim() : string.Empty;

            if (this.ShowDescFieldOnly || code == string.Empty)
            {
                this.Text = desc;
            }
            else if (desc == string.Empty)
            {
                this.Text = code;
            }
            else
            {
                this.Text = code + " [" + desc + "]";
            }
        }

        private void ReadonlyTextBox_DataBinding(object sender, EventArgs e)
        {
            object dataItem = ((FormView)(((System.Web.UI.Control)(this)).BindingContainer)).DataItem;
            object codeValue = getValue(this.CodeField, this.CodeFieldFormat, dataItem);
            object descValue = getValue(this.DescField, this.DescFieldFormat, dataItem);

            string code = codeValue != null ? codeValue.ToString() : string.Empty;
            string desc = descValue != null ? descValue.ToString() : string.Empty;

            this.ReadonlyTextBox_DataBind(code, desc);
        }

        private string getValue(string field, string format, object dataItem)
        {
            object value = null;
            if (field != string.Empty)
            {
                value = dataItem;
                string[] fieldArray = field.Split('.');
                foreach (string singleField in fieldArray)
                {
                    if (value != null)
                    {
                        PropertyInfo singlePropInfo = getPropertyInfo(value, singleField);
                        value = singlePropInfo.GetValue(value, null);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            if (value != null)
            {
                if (format != null && format != string.Empty)
                {
                    return string.Format(format, value);
                }
                else
                {
                    return value.ToString();
                }
            }
            else
            {
                return null;
            }
        }

        private PropertyInfo getPropertyInfo(Object obj, string field)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(field);
            if (propertyInfo == null)
            {

                throw new TechnicalException("Bind Filed:" + field + " of ReadonlyTextBox:" + this.ID + " is not valided");
            }
            return propertyInfo;
        }
    }
}
