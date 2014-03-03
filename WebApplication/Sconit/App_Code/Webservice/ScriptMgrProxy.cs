using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;
using System.IO;

/// <summary>
/// Summary description for ScriptMgrProxy
/// </summary>
[WebService(Namespace = "http://com.Sconit.Webservice/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ScriptMgrProxy : BaseWS
{

    public ScriptMgrProxy()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetItemData(string serviceMgr, string serviceMethod, string serviceParameter, string valueField, string descField, string imageUrlField, string inputValue)
    {
        ISession session = GetService<ISession>(serviceMgr);

        object obj = null;
        if (serviceParameter != null && serviceParameter != string.Empty)
        {
            string[] paras = serviceParameter.Split(TextBoxHelper.ParameterSeperator);
            Type[] parasType = new Type[paras.Length];
            object[] parasValue = new object[paras.Length];

            for (int i = 0; i < paras.Length; i++)
            {
                string para = paras[i];
                string type = para.Split(TextBoxHelper.TypeValueSeperator)[0];
                string value = para.Split(TextBoxHelper.TypeValueSeperator)[1];
                if (type == "bool")
                {
                    parasType[i] = typeof(bool);
                    try
                    {
                        parasValue[i] = bool.Parse(value);
                    }
                    catch (FormatException ex)
                    {
                        parasValue[i] = null;
                    }
                }
                else if (type == "string")
                {
                    parasType[i] = typeof(string);
                    parasValue[i] = value;
                }
                else if (type == "int")
                {
                    parasType[i] = typeof(int);
                    try
                    {
                        parasValue[i] = int.Parse(value);
                    }
                    catch (FormatException ex)
                    {
                        parasValue[i] = null;
                    }
                }
                else if (type == "long")
                {
                    parasType[i] = typeof(long);
                    try
                    {
                        parasValue[i] = long.Parse(value);
                    }
                    catch (FormatException ex)
                    {
                        parasValue[i] = null;
                    }
                }
                else if (type == "decimal")
                {
                    parasType[i] = typeof(decimal);
                    try
                    {
                        parasValue[i] = decimal.Parse(value);
                    }
                    catch (FormatException ex)
                    {
                        parasValue[i] = null;
                    }
                }
                else if (type == "DateTime")
                {
                    parasType[i] = typeof(DateTime);
                    try
                    {
                        parasValue[i] = DateTime.Parse(value);
                    }
                    catch (FormatException ex)
                    {
                        parasValue[i] = null;
                    }
                }
                else if (type == "double")
                {
                    parasType[i] = typeof(double);
                    try
                    {
                        parasValue[i] = double.Parse(value);
                    }
                    catch (FormatException ex)
                    {
                        parasValue[i] = null;
                    }
                }
                else if (type == "float")
                {
                    parasType[i] = typeof(float);
                    try
                    {
                        parasValue[i] = float.Parse(value);
                    }
                    catch (FormatException ex)
                    {
                        parasValue[i] = null;
                    }
                }
                else
                {
                    throw new TechnicalException("Unsupport parameter type:" + type);
                }
            }

            MethodInfo methodInfo = session.GetType().GetMethod(serviceMethod, parasType);
            obj = methodInfo.Invoke(session, parasValue);
        }
        else
        {
            MethodInfo methodInfo = session.GetType().GetMethod(serviceMethod, new Type[0]);
            obj = methodInfo.Invoke(session, null);
        }

        if (obj is IList)
        {
            IList list = obj as IList;

            string desc = string.Empty;
            string value = string.Empty;
            string imageUrl = string.Empty;

            string data = "[";
            for (int i = 0; i < list.Count; i++)
            {
                desc = GetFieldValue("DescField", descField, list, i);
                desc = desc.Replace("'", "");
                value = GetFieldValue("ValueField", valueField, list, i);
                if (imageUrlField == null)
                {
                    data += TextBoxHelper.GenSingleData(desc, value) + (i < (list.Count - 1) ? "," : string.Empty);
                }
                else
                {
                    imageUrl = GetFieldValue("ImageUrlField", imageUrlField, list, i);
                    data += TextBoxHelper.GenSingleData(desc, value, imageUrl) + (i < (list.Count - 1) ? "," : string.Empty);
                }
            }
            data += "]";
            return Server.HtmlEncode(data);
        }
        else if (obj == null)
        {
            //do nothing
            return string.Empty;
        }
        else
        {
            throw new TechnicalException("Unsupported return Type, only IList is accecpt");
        }
    }

    [WebMethod]
    public string GetTextBoxData(string serviceMgr, string serviceMethod, string serviceParameter, string valueField, string descField, string inputValue)
    {
        return GetItemData(serviceMgr, serviceMethod, serviceParameter, valueField, descField, null, inputValue);
    }


    private string GetFieldValue(string type, string valueField, IList list, int i)
    {
        string value = string.Empty;
        PropertyInfo valueFieldInfo = null;
        string[] valueFieldArray = valueField.Split('.');
        if (valueFieldArray.Length == 1)
        {
            if (list[i] is string)
            {
                //如果类型为string，直接作为value
                value = list[i] as string;
            }
            else
            {
                if (valueFieldInfo == null)
                {
                    valueFieldInfo = getPropertyInfo(valueFieldInfo, list[i], valueField, type);
                }
                if (valueFieldInfo.GetValue(list[i], null) == null)
                {
                    value = string.Empty;
                }
                else
                {
                    value = valueFieldInfo.GetValue(list[i], null).ToString();
                }
            }
        }
        else if (valueFieldArray.Length > 1)
        {
            valueFieldInfo = getPropertyInfo(valueFieldInfo, list[i], valueFieldArray[0], type);
            Object newValueObj = (Object)(valueFieldInfo.GetValue(list[i], null));
            for (int j = 1; j < valueFieldArray.Length; j++)
            {
                valueFieldInfo = getPropertyInfo(valueFieldInfo, newValueObj, valueFieldArray[j], type);
                newValueObj = (Object)(valueFieldInfo.GetValue(newValueObj, null));
            }
            value = newValueObj as string;
        }
        return value;
    }

    private PropertyInfo getPropertyInfo(PropertyInfo propertyInfo, Object obj, string field, string description)
    {
        // if (propertyInfo == null)
        // {
        propertyInfo = obj.GetType().GetProperty(field);
        if (propertyInfo == null)
        {
            throw new TechnicalException(description + " specified:" + field + " is not valided");
        }
        //  }
        return propertyInfo;
    }
}

