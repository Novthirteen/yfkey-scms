using System;
using System.Collections;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;

[ValidationPropertyAttribute("Text"), ControlValueProperty("Text")]
public partial class Controls_TextBox : System.Web.UI.UserControl, IEditableTextControl
{
    
    private HtmlGenericControl objscript = new HtmlGenericControl("script");

    private object _dataSource;
    public object DataSource
    {
        set { this._dataSource = value; }
        get { return this._dataSource; }
    }

    private string _descField;
    public string DescField
    {
        set { this._descField = value; }
        get { return this._descField; }
    }

    private string _valueField;
    public string ValueField
    {
        set { this._valueField = value; }
        get { return this._valueField; }
    }

    private string _imageUrlField;
    public string ImageUrlField
    {
        set { this._imageUrlField = value; }
        get { return this._imageUrlField; }
    }

    private bool _mustMatch;
    public bool MustMatch
    {
        set { this._mustMatch = value; }
        get { return this._mustMatch; }
    }

    private int _minChars;
    public int MinChars
    {
        set { this._minChars = value; }
        get { return this._minChars; }
    }

    private int _width;
    public int Width
    {
        set { this._width = value; }
        get { return this._width; }
    }

    private bool _autoFill;
    public bool AutoFill
    {
        set { this._autoFill = value; }
        get { return this._autoFill; }
    }

    private string _servicePath;
    public string ServicePath
    {
        set { this._servicePath = value; }
        get { return this._servicePath; }
    }

    private string _serviceMethod;
    public string ServiceMethod
    {
        set { this._serviceMethod = value; }
        get { return this._serviceMethod; }
    }

    private string _serviceParameters;
    public string ServiceParameter
    {
        set { this._serviceParameters = value; }
        get { return this._serviceParameters; }
    }

    public string CssClass
    {
        set { this.divSuggest.Attributes["class"] = "suggestInput" + value; }
        get { return this.divSuggest.Attributes["class"]; }
    }

    public Unit InputWidth
    {
        set { this.suggest.Width = value; }
        get { return this.suggest.Width; }
    }

    private bool _isCascadeClear = true;
    public bool IsCascadeClear
    {
        set { this._isCascadeClear = value; }
        get { return this._isCascadeClear; }
    }

    public bool ReadOnly
    {
        set { this.suggest.ReadOnly = value; }
        get { return this.suggest.ReadOnly; }
    }

    public override void DataBind()
    {
        // base.DataBind();
        objscript.InnerText = GetDataFromDS();
        BindCascadeData();
    }

    public bool AutoPostBack
    {
        set { this.suggest.AutoPostBack = value; }
        get { return this.suggest.AutoPostBack; }
    }

    public TextBox SuggestTextBox
    {
        set { this.suggest = value; }
        get { return this.suggest; }
    }

    #region TextChanged实现
    // private static readonly object EventCustomTextChanged = new Object();
    public event EventHandler TextChanged
    {
        add
        {
            this.suggest.TextChanged += value;
            //Events.AddHandler(EventCustomTextChanged, value);
        }
        remove
        {
            this.suggest.TextChanged -= value;
            //Events.RemoveHandler(EventCustomTextChanged, value);
        }
    }

    public string Text
    {
        get
        {
            return this.suggest.Text;
        }
        set
        {
            this.suggest.Text = value;
        }
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //定义这个Link的各项属性
        objscript.ID = ID;
        objscript.Attributes["type"] = "text/javascript";
        //objscript.Attributes["defer"] = "true";
        //把script控件加到PlaceHolder控件中去
        //默认都是完全匹配
       // this.MustMatch = true;
        objscript.InnerText = GetDataFromDS();
        phLocaldata.Controls.Add(objscript);

        BindCascadeData();

        if (IsCascadeClear && this.ServiceParameter != null && this.ServiceParameter != string.Empty)
        {
            this.SetCascadeClear();
        }
        //}
    }

    private string GenServiceParameter()
    {
        string result = "'";
        if (this.ServiceParameter != string.Empty && this.ServiceParameter != null)
        {
            result = ProcessServiceParameter(result);
        }
        result += "'";

        return result;
    }

    private string GenScriptMgrParameter()
    {
        string result = "{";
        result += "'serviceMgr':'" + this.ServicePath + "', ";
        result += "'serviceMethod':'" + this.ServiceMethod + "', ";

        if (this.ServiceParameter != string.Empty && this.ServiceParameter != null)
        {
            string fieldValue = string.Empty;

            fieldValue = ProcessServiceParameter(fieldValue);

            result += "'serviceParameter':'" + fieldValue + "', ";
        }
        else
        {
            result += "'serviceParameter':'', ";
        }

        result += "'valueField':'" + this.ValueField + "', ";
        result += "'descField':'" + this.DescField + "', ";
        if (this.ImageUrlField != null && this.ImageUrlField.Trim() != string.Empty)
        {
            result += "'imageUrlField':'" + this.ImageUrlField + "', ";
        }
        result += "'inputValue': " + this.suggest.ClientID + "_inputValue";
        result += "}";

        return result;
    }

    private void SetCascadeClear()
    {
        string[] paras = this.ServiceParameter.Split(',');

        for (int i = 0; i < paras.Length; i++)
        {
            string paraType = paras[i].Split(':')[0];
            string paraValue = paras[i].Split(':')[1];

            if (paraValue.StartsWith("#"))
            {
                Control targetControl = this.NamingContainer.FindControl(paraValue.Substring(1));
                if (targetControl is TextBox)
                {
                    ((TextBox)targetControl).Attributes["onchange"] += "$('#" + this.suggest.ClientID + "').attr('value', '');";
                }
                else if (targetControl is DropDownList)
                {
                    ((DropDownList)targetControl).Attributes["onchange"] += "$('#" + this.suggest.ClientID + "').attr('value', '');";
                }
                else if (targetControl is CheckBox)
                {
                    throw new NotImplementedException();
                }
                else if (targetControl is RadioButton)
                {
                    throw new NotImplementedException();
                }
                else if (targetControl is Controls_TextBox)
                {
                    ((Controls_TextBox)(targetControl)).suggest.Attributes["onchange"] += "$('#" + this.suggest.ClientID + "').attr('value', '');";
                }
                else if (targetControl is HiddenField)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    throw new TechnicalException("Unsupport control type:" + targetControl.GetType() + " of ServiceParameter");
                }
            }
        }
    }

    private string ProcessServiceParameter(string result)
    {
        string[] paras = this.ServiceParameter.Split(',');

        for (int i = 0; i < paras.Length; i++)
        {
            string paraType = paras[i].Split(':')[0];
            string paraValue = paras[i].Split(':')[1];

            result += paraType.Trim() + TextBoxHelper.TypeValueSeperator;

            if (paraValue.StartsWith("#"))
            {
                Control targetControl = this.NamingContainer.FindControl(paraValue.Substring(1));
                if (targetControl is TextBox)
                {
                    result += "' + $('#" + targetControl.ClientID + "').val() + '";
                }
                else if (targetControl is DropDownList)
                {
                    result += "' + $('#" + targetControl.ClientID + "').val() + '";
                }
                else if (targetControl is CheckBox)
                {
                    throw new NotImplementedException();
                }
                else if (targetControl is RadioButton)
                {
                    throw new NotImplementedException();
                }
                else if (targetControl is Controls_TextBox)
                {
                    result += "' + $('#" + targetControl.ClientID + "_suggest" + "').val() + '";
                }
                else if (targetControl is HiddenField)
                {
                    result += "' + $('#" + targetControl.ClientID + "_suggest" + "').val() + '";
                }
                else
                {
                    throw new TechnicalException("Unsupport control type:" + targetControl.GetType() + " of ServiceParameter");
                }
            }
            else
            {
                result += paraValue.Trim();
            }

            if (i != paras.Length - 1)
            {
                result += TextBoxHelper.ParameterSeperator;
            }
        }

        return result;
    }

    private string GetDataFromDS()
    {
        string data = "var " + this.suggest.ClientID + "_datas = null;";
        data += "var " + this.suggest.ClientID + "_oldParameters = null;";
        data += TextBoxHelper.GetOption(this.suggest.ClientID, this._minChars, this._width, this._autoFill, this._mustMatch, 200);

        data += GetWebServiceData();

        return data;
    }

    private PropertyInfo getPropertyInfo(PropertyInfo propertyInfo, Object obj, string field, string description)
    {
        // if (propertyInfo == null)
        // {
        propertyInfo = obj.GetType().GetProperty(field);
        if (propertyInfo == null)
        {
            throw new TechnicalException(description + "specified:" + field + " is not valided");
        }
        //  }
        return propertyInfo;
    }

    private void BindCascadeData()
    {
        if (ServicePath != null && ServicePath != string.Empty
            && ServiceMethod != null && ServiceMethod != string.Empty)
        {
            this.suggest.Attributes["onfocus"] = @"Get_" + this.suggest.ClientID + @"_Data();";
            this.suggest.Attributes["ondblclick"] = "this.value=''";
            // this.suggestDiv.Attributes["onclick"] = @"Get_" + this.suggest.ClientID + @"_Data();";            
        }
    }

    private string GetWebServiceData()
    {
        if (this.ServiceMethod == "GetCacheAllItem")
        {
            return @"function Get_" + this.suggest.ClientID + @"_Data()
                {
                    $('#" + this.suggestDiv.ClientID + @"').css('background','transparent url(Images/animated_loading.gif) no-repeat scroll 1px -2px');
                    var " + this.suggest.ClientID + @"_inputValue=$('#" + this.suggest.ClientID + @"').val();
                    var " + this.suggest.ClientID + @"_newParameters = " + GenServiceParameter() + @";
                    if (" + this.suggest.ClientID + @"_oldParameters == " + this.suggest.ClientID + @"_newParameters )
                    {
                        $('#" + this.ClientID + @"_suggest').trigger('click');
                        $('#" + this.suggestDiv.ClientID + @"').css('background','transparent url(Images/Icon/icon_sugg.png) no-repeat scroll 1px -2px');
                        return;
                    }

                    var para = parent.topFrame.$('#data').attr('value');
                    para = eval(para);
                    //alert(para);
                    var data = {data : para};
                    $('#" + this.ClientID + @"_suggest').setOptions(data);
                    $('#" + this.ClientID + @"_suggest').trigger('click');
                    $('#" + this.suggestDiv.ClientID + @"').css('background','transparent url(Images/Icon/icon_sugg.png) no-repeat scroll 1px -2px');
                 
                }";
        }
        else
        {
            return @"function Get_" + this.suggest.ClientID + @"_Data()
                {
                    $('#" + this.suggestDiv.ClientID + @"').css('background','transparent url(Images/animated_loading.gif) no-repeat scroll 1px -2px');
                    var " + this.suggest.ClientID + @"_inputValue=$('#" + this.suggest.ClientID + @"').val();
                    var " + this.suggest.ClientID + @"_newParameters = " + GenServiceParameter() + @";
                    if (" + this.suggest.ClientID + @"_oldParameters == " + this.suggest.ClientID + @"_newParameters )
                    {
                        $('#" + this.ClientID + @"_suggest').trigger('click');
                        $('#" + this.suggestDiv.ClientID + @"').css('background','transparent url(Images/Icon/icon_sugg.png) no-repeat scroll 1px -2px');
                        return;
                    }

                    $('#" + this.suggestDiv.ClientID + @"').data('oldInputValue', " + this.suggest.ClientID + @"_inputValue);
                        " + this.suggest.ClientID + @"_oldParameters = " + this.suggest.ClientID + @"_newParameters;
                        Sys.Net.WebServiceProxy.invoke(
                        'Webservice/ScriptMgrProxy.asmx',
                        '" + GetMethodName() + @"',
                        false, 
                        " + GenScriptMgrParameter() + @",
                        function OnSucceeded(result, eventArgs)
                        {        
                           var para = eval(result);
                           //alert(para);
                           var data = {data : para};
                           //alert(data);
                           $('#" + this.ClientID + @"_suggest').setOptions(data);
                           $('#" + this.ClientID + @"_suggest').trigger('click');
                           $('#" + this.suggestDiv.ClientID + @"').css('background','transparent url(Images/Icon/icon_sugg.png) no-repeat scroll 1px -2px');
                         },
                        function OnFailed(error)
                        {
                           
                        }
                    );
                }";
        }
        // alert(error.get_message());
    }

    private string GetMethodName()
    {
        if (this.ImageUrlField != null)
        {
            return "GetItemData";
        }
        return "GetTextBoxData";
    }
}
