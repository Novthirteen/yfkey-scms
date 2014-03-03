using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;

namespace com.Sconit.Utility
{
    public static class CloneHelper
    {
        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T DeepClone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);                
                return (T)formatter.Deserialize(stream);
            }
        }       

        public static void CopyProperty(object sourceObj, object targetObj)
        {
            PropertyInfo[] sourcePropertyInfoAry = sourceObj.GetType().GetProperties();
            PropertyInfo[] targetPropertyInfoAry = targetObj.GetType().GetProperties();

            foreach (PropertyInfo sourcePropertyInfo in sourcePropertyInfoAry)
            {
                foreach (PropertyInfo targetPropertyInfo in targetPropertyInfoAry)
                {
                    if (sourcePropertyInfo.Name == targetPropertyInfo.Name)
                    {
                        if (targetPropertyInfo.CanWrite && sourcePropertyInfo.CanRead)
                        {
                            targetPropertyInfo.SetValue(targetObj, sourcePropertyInfo.GetValue(sourceObj, null), null);
                        }
                    }
                }
            }
        }

        public static void CopyProperty(object sourceObj, object targetObj, string[] fields)
        {
            CopyProperty(sourceObj, targetObj, fields, false);
        }

        public static void CopyProperty(object sourceObj, object targetObj, string[] fields, bool fieldsExclude)
        {
            PropertyInfo[] sourcePropertyInfoAry = sourceObj.GetType().GetProperties();
            PropertyInfo[] targetPropertyInfoAry = targetObj.GetType().GetProperties();

            foreach (PropertyInfo sourcePropertyInfo in sourcePropertyInfoAry)
            {
                foreach (PropertyInfo targetPropertyInfo in targetPropertyInfoAry)
                {
                    if (sourcePropertyInfo.Name == targetPropertyInfo.Name)
                    {
                        bool nameInFields = false;
                        foreach (string field in fields)
                        {
                            if (sourcePropertyInfo.Name == field)
                            {
                                nameInFields = true; 
                                break;
                            }
                        }

                        if ((!fieldsExclude && nameInFields)
                            || (fieldsExclude && !nameInFields))
                        {
                            if (targetPropertyInfo.CanWrite && sourcePropertyInfo.CanRead)
                            {
                                targetPropertyInfo.SetValue(targetObj, sourcePropertyInfo.GetValue(sourceObj, null), null);
                            }
                        }
                    }
                }
            }
        }
    }
}
