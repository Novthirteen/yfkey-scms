using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Utility;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class PermissionMgr : PermissionBaseMgr, IPermissionMgr
    {
        private ICriteriaMgr criteriaMgr;
        public PermissionMgr(IPermissionDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public Permission GetPermission(string code)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Permission));
            criteria.Add(Expression.Eq("Code", code));
            IList list = criteriaMgr.FindAll(criteria);
            if (list != null && list.Count > 0)
            {
                return (Permission)list[0];
            }
            return null;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Permission> GetALlPermissionsByCategory(string categoryCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Permission));
            if (categoryCode != null)
            {
                criteria.Add(Expression.Eq("Category.Code", categoryCode));
            }
            return criteriaMgr.FindAll<Permission>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Permission> GetALlPermissionsByCategory(string categoryCode, User user)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Permission));

            DetachedCriteria[] pCrieteria = SecurityHelper.GetPermissionCriteriaByCategory(user, categoryCode);

            criteria.Add(
                Expression.Or(
                    Subqueries.PropertyIn("Code", pCrieteria[0]),
                    Subqueries.PropertyIn("Code", pCrieteria[1])));

            return criteriaMgr.FindAll<Permission>(criteria);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeletePermission(string code)
        {
            this.entityDao.DeletePermission(code);
        }

        #endregion Customized Methods
    }
}