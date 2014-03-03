using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity;
using NHibernate.Expression;
using com.Sconit.Entity.Svp.Condition;

namespace com.Sconit.Utility
{

    public static class RequestOrderByHelper
    {
        public static DetachedCriteria ConverToCriteria(EntityOrderby[] orderbyField, Dictionary<string, string> orderbyFiledsMapping, DetachedCriteria criteria)
        {
            foreach (EntityOrderby orderby in orderbyField)
            {
                if (orderbyFiledsMapping.ContainsKey(orderby.orderbyField))
                {
                    if (orderby.asc)
                    {
                        criteria.AddOrder(Order.Asc(orderbyFiledsMapping[orderby.orderbyField]));
                    }
                    else
                    {
                        criteria.AddOrder(Order.Desc(orderbyFiledsMapping[orderby.orderbyField]));
                    }
                  
                }
            }
            return criteria;
        }
    }
}
