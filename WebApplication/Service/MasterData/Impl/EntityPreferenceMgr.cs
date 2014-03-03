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
    public class EntityPreferenceMgr : EntityPreferenceBaseMgr, IEntityPreferenceMgr
    {
        private ICriteriaMgr criteriaMgr;
        public EntityPreferenceMgr(IEntityPreferenceDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        public IList<EntityPreference> GetAllEntityPreferenceOrderBySeq()
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(EntityPreference)).AddOrder(Order.Asc("Seq"));
            return criteriaMgr.FindAll<EntityPreference>(criteria);
        }

        #endregion Customized Methods
    }
}