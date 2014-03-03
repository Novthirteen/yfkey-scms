using System;
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
    public class WorkCenterMgr : WorkCenterBaseMgr, IWorkCenterMgr
    {
        
        private ICriteriaMgr criteriaMgr;
        public WorkCenterMgr(IWorkCenterDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        public IList<WorkCenter> GetWorkCenter(string regionCode)
        {
            return GetWorkCenter(regionCode, false);
        }

        public IList<WorkCenter> GetWorkCenter(Region region)
        {
            return GetWorkCenter(region.Code, false);
        }

        public IList<WorkCenter> GetWorkCenter(string regionCode, bool includeInactive)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(WorkCenter));
            criteria.CreateAlias("Party", "Region");
            criteria.Add(Expression.Eq("Region.Code", regionCode));
            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }
            return criteriaMgr.FindAll<WorkCenter>(criteria);
        }

        public IList<WorkCenter> GetWorkCenter(Region region, bool includeInactive)
        {
            return GetWorkCenter(region.Code, includeInactive);
        }

        public void DeleteWorkCenterByParent(String parentCode)
        {
            entityDao.DeleteWorkCenterByParent(parentCode);
        }

        #endregion Customized Methods
    }
}