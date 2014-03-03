using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using System.Reflection;

namespace com.Sconit.Utility
{
    public static class EntityHelper
    {
        public static bool EntityPropertyEquals(EntityBase entity1, EntityBase entity2, string property)
        {
            if (!entity1.GetType().Equals(entity2.GetType()))
            {
                throw new TechnicalException("Type:" + entity1.GetType() +" of Entity1 is not as same as Type:" + entity2.GetType() + " of Entity2");
            }

            object value1 = entity1;
            object value2 = entity2;
            string[] fieldArray = property.Split('.');
            foreach (string singleField in fieldArray)
            {
                if (value1 == null && value2 == null)
                {
                    return true;
                }

                PropertyInfo singlePropInfo = getPropertyInfo(value1, singleField);
                value1 = singlePropInfo.GetValue(value1, null);
                value2 = singlePropInfo.GetValue(value2, null);

                if ((value1 != null && value1.Equals(value2)) || value2 != null)
                {
                    return false;
                }
            }

            return true;
        }

        private static PropertyInfo getPropertyInfo(Object obj, string field)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(field);
            if (propertyInfo == null)
            {

                throw new TechnicalException("Filed:" + field + " of " + obj.GetType() + " is not valided");
            }
            return propertyInfo;
        }
    }
}
