using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Exception;
using System.Collections;
using com.Sconit.Entity.MasterData;
using System.Web.UI;
using System.ComponentModel;
using com.Sconit.Utility;

namespace com.Sconit.Control
{
    [ToolboxData("<{0}:CodeMstrDropDownList runat=server></{0}:CodeMstrDropDownList>")]
    public class CodeMstrDropDownList : com.Sconit.Control.DropDownList
    {
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
        [Description("Whether display a blank option or not")]
        [DefaultValue(false)]
        [Browsable(true)]
        public Boolean IncludeBlankOption
        {
            get
            {
                return (ViewState["IncludeBlankOption"] != null ? (bool)ViewState["IncludeBlankOption"] : false);
            }
            set
            {
                ViewState["IncludeBlankOption"] = value;
            }
        }

        [Category("Custmize")]
        [Description("The value of blank option, active only when the property IncludeBlankOption is set true.")]
        [DefaultValue("")]
        [Browsable(true)]
        public String BlankOptionValue
        {
            get
            {
                return (ViewState["BlankOptionValue"] != null ? (string)ViewState["BlankOptionValue"] : "");
            }
            set
            {
                ViewState["BlankOptionValue"] = value;
            }
        }

        [Category("Custmize")]
        [Description("The description of blank option, active only when the property IncludeBlankOption is set true.")]
        [DefaultValue("All")]
        [Browsable(true)]
        public String BlankOptionDesc
        {
            get
            {
                return (ViewState["BlankOptionDesc"] != null ? (string)ViewState["BlankOptionDesc"] : "");
            }
            set
            {
                ViewState["BlankOptionDesc"] = value;
            }
        }

        protected override void InitList()
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
            else
            {
                this.ServiceParameter = "string:" + this.Code;
            }

            this.DescField = "Description";
            this.ValueField = "Value";

            IList<CodeMaster> list = ReflectHelper.InvokeServiceMethod(this.ServicePath, this.ServiceMethod, this.ServiceParameter) as IList<CodeMaster>;


            //IEnumerable<CodeMaster> i = list.OrderBy(codeMaster => codeMaster.Seq);
            this.Items.Clear();

            if (this.IncludeBlankOption)
            {
                //因为list是CachedList，不能直接往里面插值，所以做了个倒手。:(
                CodeMaster codeMstr = new CodeMaster();
                codeMstr.Code = this.BlankOptionValue;
                codeMstr.Description = this.BlankOptionDesc;

                IList<CodeMaster> newList = list;
                list = new List<CodeMaster>();
                list.Add(codeMstr);
                foreach (CodeMaster codeMaster in newList)
                {
                    list.Add(codeMaster);
                }
            }

            this.DataSource = list;
            this.DataTextField = this.DescField;
            this.DataValueField = this.ValueField;
            base.DataBind();
            #region 默认值
            if (this.DefaultSelectedValue == null && list != null)
            {
                foreach (CodeMaster codeMaster in list)
                {
                    if (codeMaster.IsDefault)
                    {
                        this.DefaultSelectedValue = codeMaster.Value;
                        break;
                    }
                }
            }
            if (this.DefaultSelectedValue != null)
            {
                this.SelectedValue = this.DefaultSelectedValue;
            }
            #endregion


        }

        public override void DataBind()
        {
            InitList();
        }
    }
}
