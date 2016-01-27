using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using com.Sconit.Persistence.Criteria;
using NHibernate.Expression;
using Castle.Services.Transaction;

namespace com.Sconit.Service.Criteria.Impl
{
    [Transactional]
    public class CriteriaMgr : SessionBase, ICriteriaMgr
    {
        private ICriteriaDao _criteriaDao;

        public CriteriaMgr(ICriteriaDao criteriaDao)
        {
            _criteriaDao = criteriaDao;
        }

        [Transaction(TransactionMode.Unspecified, IsolationMode.ReadUncommitted)]
        public IList FindAll(DetachedCriteria criteria)
        {
            return _criteriaDao.FindAll(criteria);
        }

        [Transaction(TransactionMode.Unspecified, IsolationMode.ReadUncommitted)]
        public IList FindAll(DetachedCriteria criteria, int firstRow, int maxRows)
        {
            return _criteriaDao.FindAll(criteria, firstRow, maxRows);
        }

        [Transaction(TransactionMode.Unspecified, IsolationMode.ReadUncommitted)]
        public IList<T> FindAll<T>(DetachedCriteria criteria)
        {
            return _criteriaDao.FindAll<T>(criteria);
        }

        [Transaction(TransactionMode.Unspecified, IsolationMode.ReadUncommitted)]
        public IList<T> FindAll<T>(DetachedCriteria criteria, int firstRow, int maxRows)
        {
            return _criteriaDao.FindAll<T>(criteria, firstRow, maxRows);
        }
    }
}
