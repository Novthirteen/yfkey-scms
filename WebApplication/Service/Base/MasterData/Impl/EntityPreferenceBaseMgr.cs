using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class EntityPreferenceBaseMgr : SessionBase, IEntityPreferenceBaseMgr
    {
        protected IEntityPreferenceDao entityDao;
        
        public EntityPreferenceBaseMgr(IEntityPreferenceDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateEntityPreference(EntityPreference entity)
        {
            entityDao.CreateEntityPreference(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual EntityPreference LoadEntityPreference(String code)
        {
            return entityDao.LoadEntityPreference(code);
        }

		[Transaction(TransactionMode.Requires)]
        public virtual IList<EntityPreference> GetAllEntityPreference()
        {
            return entityDao.GetAllEntityPreference();
        }
		
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateEntityPreference(EntityPreference entity)
        {
            entityDao.UpdateEntityPreference(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteEntityPreference(String code)
        {
            entityDao.DeleteEntityPreference(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteEntityPreference(EntityPreference entity)
        {
            entityDao.DeleteEntityPreference(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteEntityPreference(IList<String> pkList)
        {
            entityDao.DeleteEntityPreference(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteEntityPreference(IList<EntityPreference> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteEntityPreference(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
