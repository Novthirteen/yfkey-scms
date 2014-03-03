using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using com.Sconit.Entity.Exception;
using System.Web.Compilation;

namespace com.Sconit.Utility
{
    public static class ReflectHelper
    {
        public static object InvokeServiceMethod(string servicePath, string serviceMethod, string serviceParameters)
        {
            ConstructorInfo constructorInfo = BuildManager.GetType(servicePath, true).GetConstructor(System.Type.EmptyTypes);           
            object result = null;

            if (serviceParameters != null && serviceParameters != string.Empty)
            {
                string[] paras = serviceParameters.Split(',');
                Type[] parasType = new Type[paras.Length];
                object[] parasValue = new object[paras.Length];

                for (int i = 0; i < paras.Length; i++)
                {
                    string para = paras[i];
                    string type = para.Split(':')[0];
                    string value = para.Split(':')[1];
                    if (type == "bool")
                    {
                        parasType[i] = typeof(bool);
                        try
                        {
                            parasValue[i] = bool.Parse(value);
                        }
                        catch (FormatException)
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
                        catch (FormatException)
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
                        catch (FormatException)
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
                        catch (FormatException)
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
                        catch (FormatException)
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
                        catch (FormatException)
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
                        catch (FormatException)
                        {
                            parasValue[i] = null;
                        }
                    }
                    else
                    {
                        throw new TechnicalException("Unsupport parameter type:" + type);
                    }
                }

                MethodInfo serviceMethodInfo = BuildManager.GetType(servicePath, true).GetMethod(serviceMethod, parasType);
                

                if (serviceMethodInfo.IsStatic)
                {
                    result = serviceMethodInfo.Invoke(null, parasValue);
                }
                else
                {
                    if (constructorInfo != null)
                    {
                        if (constructorInfo.IsStatic)
                        {
                            result = serviceMethodInfo.Invoke(null, parasValue);
                        }
                        else
                        {
                            object obj = constructorInfo.Invoke(null);
                            result = serviceMethodInfo.Invoke(obj, parasValue);
                        }
                    }
                    else
                    {
                        throw new TechnicalException(servicePath + " specified has not zero parameter constructor");
                    }
                }
            }
            else
            {
                MethodInfo serviceMethodInfo = BuildManager.GetType(servicePath, true).GetMethod(serviceMethod, null);

                if (serviceMethodInfo.IsStatic)
                {
                    result = serviceMethodInfo.Invoke(null, null);
                }
                else
                {
                    if (constructorInfo != null)
                    {
                        if (constructorInfo.IsStatic)
                        {
                            result = serviceMethodInfo.Invoke(null, null);
                        }
                        else
                        {
                            object obj = constructorInfo.Invoke(null);
                            result = serviceMethodInfo.Invoke(obj, null);
                        }
                    }
                    else
                    {
                        throw new TechnicalException(servicePath + " specified has not zero parameter constructor");
                    }
                }
            }

            return result;
        }

        public static T GetProperty<T>(Object obj, string field)
        {
            string[] splitedField = field.Split('.');
            Object targetObj = obj;

            foreach(string singleField in splitedField)
            {
                PropertyInfo propertyInfo = targetObj.GetType().GetProperty(singleField);
                if (propertyInfo == null)
                {
                    throw new TechnicalException("Object " + obj.GetType().ToString() + " does not have property " + field);
                }

                if (targetObj == null)
                {
                    return (T)targetObj;
                    //throw new TechnicalException("Encounter null value when cycle reflect Object " + obj.GetType().ToString());
                }

                targetObj = propertyInfo.GetValue(obj, null);
            }

            return (T)targetObj;
        }
    }
}
