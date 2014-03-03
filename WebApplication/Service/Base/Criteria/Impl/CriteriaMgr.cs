using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using com.Sconit.Persistence.Criteria;
using NHibernate.Expression;

namespace com.Sconit.Service.Criteria.Impl
{
    public class CriteriaMgr : SessionBase, ICriteriaMgr
    {
        private ICriteriaDao _criteriaDao;

        public CriteriaMgr(ICriteriaDao criteriaDao)
        {
            _criteriaDao = criteriaDao;
        }

        public IList FindAll(DetachedCriteria criteria)
        {
            return _criteriaDao.FindAll(criteria);
        }

        public IList FindAll(DetachedCriteria criteria, int firstRow, int maxRows)
        {
            return _criteriaDao.FindAll(criteria, firstRow, maxRows);
        }

        public IList<T> FindAll<T>(DetachedCriteria criteria)
        {
            return _criteriaDao.FindAll<T>(criteria);
        }

        public IList<T> FindAll<T>(DetachedCriteria criteria, int firstRow, int maxRows)
        {
            return _criteriaDao.FindAll<T>(criteria, firstRow, maxRows);
        }
    }
}
