using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

namespace com.Sconit.Utility
{
    /// <summary>
    /// IList工具类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class IListHelper
    {
        public static IList<T> ConvertToList<T>(T[] array)
        {
            if (array == null)
            {
                return null;
            }

            IList<T> result = new List<T>();
            foreach (T o in array)
            {
                result.Add(o);
            }

            return result;
        }

        public static IList<T> ConvertToList<T>(IList list)
        {
            if (list == null) return null;

            IList<T> result = new List<T>();
            foreach (object o in list)
            {
                result.Add((T)o);
            }

            return result;
        }

        public static IList ConvertToList<T>(IList<T> list)
        {
            if (list == null) return null;

            IList result = new ArrayList();
            foreach (T o in list)
            {
                result.Add(o);
            }

            return result;
        }

        public static IList<T> Sort<T>(IList<T> list, string sortPropertyName)
        {
            return Sort(list, sortPropertyName, false);
        }

        public static IList<T> Sort<T>(IList<T> list, string sortPropertyName, bool isDesc)
        {
            if (list.Count == 0) return list;
            for (int i = 1; i < list.Count; i++)
            {
                T t = list[i];
                int j = i;
                while ((j > 0) && Compare<T>(list[j - 1], t, sortPropertyName, isDesc) < 0)
                {
                    list[j] = list[j - 1];
                    --j;
                }
                list[j] = t;
            }
            return list;
        }

        public static void AddRange<T>(IList<T> sourceList, IList<T> addedlist)
        {
            if (sourceList == null)
            {
                sourceList = new List<T>();
            }

            if (addedlist != null && addedlist.Count > 0)
            {
                foreach (T t in addedlist)
                {
                    sourceList.Add(t);
                }
            }
        }

        public static IList<T> Merge<T>(IList<T> list1, IList<T> list2)
        {
            IList<T> result = new List<T>();

            if (list1 != null && list1.Count > 0)
            {
                foreach (T t in list1)
                {
                    result.Add(t);
                }
            }

            if (list2 != null && list2.Count > 0)
            {
                foreach (T t in list2)
                {
                    result.Add(t);
                }
            }

            return result;
        }

        // In ListUtil.cs
        public static IList<T> Filter<T>(IList<T> source, Predicate<T> predicate)
        {
            List<T> ret = new List<T>();
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    ret.Add(item);
                }
            }
            return ret;
        }

        /// <summary>
        /// 比较大小 返回值 小于零则X小于Y，等于零则X等于Y，大于零则X大于Y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int Compare<T>(T x, T y, string sortPropertyName, bool isDesc)
        {
            if (string.IsNullOrEmpty(sortPropertyName)) throw new ArgumentNullException("没有指字对象的排序字段属性名!");
            PropertyInfo property = typeof(T).GetProperty(sortPropertyName);
            if (property == null) throw new ArgumentNullException("在对象中没有找到指定属性!");

            switch (property.PropertyType.ToString())
            {
                case "System.Int32":
                    int int1 = 0;
                    int int2 = 0;
                    if (property.GetValue(x, null) != null)
                    {
                        int1 = Convert.ToInt32(property.GetValue(x, null));
                    }
                    if (property.GetValue(y, null) != null)
                    {
                        int2 = Convert.ToInt32(property.GetValue(y, null));
                    }
                    if (!isDesc)
                    {
                        return int2.CompareTo(int1);
                    }
                    else
                    {
                        return int1.CompareTo(int2);
                    }
                case "System.Double":
                    double double1 = 0;
                    double double2 = 0;
                    if (property.GetValue(x, null) != null)
                    {
                        double1 = Convert.ToDouble(property.GetValue(x, null));
                    }
                    if (property.GetValue(y, null) != null)
                    {
                        double2 = Convert.ToDouble(property.GetValue(y, null));
                    }
                    if (!isDesc)
                    {
                        return double2.CompareTo(double1);
                    }
                    else
                    {
                        return double1.CompareTo(double2);
                    }
                case "System.String":
                    string string1 = string.Empty;
                    string string2 = string.Empty;
                    if (property.GetValue(x, null) != null)
                    {
                        string1 = property.GetValue(x, null).ToString();
                    }
                    if (property.GetValue(y, null) != null)
                    {
                        string2 = property.GetValue(y, null).ToString();
                    }
                    if (!isDesc)
                    {
                        return string2.CompareTo(string1);
                    }
                    else
                    {
                        return string1.CompareTo(string2);
                    }
                case "System.DateTime":
                    DateTime DateTime1 = DateTime.Now;
                    DateTime DateTime2 = DateTime.Now;
                    if (property.GetValue(x, null) != null)
                    {
                        DateTime1 = Convert.ToDateTime(property.GetValue(x, null));
                    }
                    if (property.GetValue(y, null) != null)
                    {
                        DateTime2 = Convert.ToDateTime(property.GetValue(y, null));
                    }
                    if (!isDesc)
                    {
                        return DateTime2.CompareTo(DateTime1);
                    }
                    else
                    {
                        return DateTime1.CompareTo(DateTime2);
                    }
            }
            return 0;
        }
        public static DataTable ConvertToDataTable<T>(IList<T> i_objlist)
        {
            if (i_objlist == null || i_objlist.Count <= 0)
            {
                return null;
            }
            DataTable dt = new DataTable(typeof(T).Name);
            DataColumn column;
            DataRow row;

            System.Reflection.PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (T t in i_objlist)
            {
                if (t == null)
                {
                    continue;
                }

                row = dt.NewRow();

                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    System.Reflection.PropertyInfo pi = myPropertyInfo[i];

                    string name = pi.Name;

                    if (dt.Columns[name] == null)
                    {
                        column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }

                    row[name] = pi.GetValue(t, null);
                }

                dt.Rows.Add(row);
            }
            return dt;
        }
    }


}
