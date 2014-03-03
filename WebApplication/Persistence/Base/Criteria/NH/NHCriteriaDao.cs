using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using com.Sconit.Persistence.Criteria;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Impl;
using System.Reflection;

namespace com.Sconit.Persistence.Criteria.NH
{
    public class NHCriteriaDao : NHDaoBase, ICriteriaDao
    {
        public NHCriteriaDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IList FindAll(DetachedCriteria criteria)
        {
            return criteria.GetExecutableCriteria(GetSession()).List();
        }

        public IList FindAll(DetachedCriteria criteria, int firstRow, int maxRows)
        {
            ICriteria c = criteria.GetExecutableCriteria(GetSession());
            c.SetFirstResult(firstRow);
            c.SetMaxResults(maxRows);
            return c.List();
        }

        public IList<T> FindAll<T>(DetachedCriteria criteria)
        {
            return criteria.GetExecutableCriteria(GetSession()).List<T>();
        }

        public IList<T> FindAll<T>(DetachedCriteria criteria, int firstRow, int maxRows)
        {
            ICriteria c = criteria.GetExecutableCriteria(GetSession());
            c.SetFirstResult(firstRow);
            c.SetMaxResults(maxRows);
            return c.List<T>();
        }
    }
}
