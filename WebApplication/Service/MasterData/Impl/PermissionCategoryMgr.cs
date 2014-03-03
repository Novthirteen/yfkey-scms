using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class PermissionCategoryMgr : PermissionCategoryBaseMgr, IPermissionCategoryMgr
    {
        private ICriteriaMgr criteriaMgr;
        public PermissionCategoryMgr(IPermissionCategoryDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        public IList<PermissionCategory> GetCategoryByType(string type)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(PermissionCategory));
            criteria.Add(Expression.Eq("Type", type));
            return criteriaMgr.FindAll<PermissionCategory>(criteria);
        }


        #endregion Customized Methods
    }
}