using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate.Expression;

namespace com.Sconit.Persistence.Criteria
{
    public interface IArchCriteriaDao
    {
        IList FindAll(DetachedCriteria criteria);

        IList FindAll(DetachedCriteria criteria, int firstRow, int maxRows);

        IList<T> FindAll<T>(DetachedCriteria criteria);

        IList<T> FindAll<T>(DetachedCriteria criteria, int firstRow, int maxRows);
    }
}
