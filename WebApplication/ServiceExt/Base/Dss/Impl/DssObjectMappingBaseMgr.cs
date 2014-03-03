using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Dss;
using com.Sconit.Persistence.Dss;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Dss.Impl
{
    [Transactional]
    public class DssObjectMappingBaseMgr : SessionBase, IDssObjectMappingBaseMgr
    {
        protected IDssObjectMappingDao entityDao;
        
        public DssObjectMappingBaseMgr(IDssObjectMappingDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateDssObjectMapping(DssObjectMapping entity)
        {
            entityDao.CreateDssObjectMapping(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual DssObjectMapping LoadDssObjectMapping(Int32 id)
        {
            return entityDao.LoadDssObjectMapping(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<DssObjectMapping> GetAllDssObjectMapping()
        {
            return entityDao.GetAllDssObjectMapping();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateDssObjectMapping(DssObjectMapping entity)
        {
            entityDao.UpdateDssObjectMapping(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssObjectMapping(Int32 id)
        {
            entityDao.DeleteDssObjectMapping(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssObjectMapping(DssObjectMapping entity)
        {
            entityDao.DeleteDssObjectMapping(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssObjectMapping(IList<Int32> pkList)
        {
            entityDao.DeleteDssObjectMapping(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssObjectMapping(IList<DssObjectMapping> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteDssObjectMapping(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
