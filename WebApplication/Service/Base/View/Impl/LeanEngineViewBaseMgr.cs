using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.View;
using com.Sconit.Persistence.View;

//TODO: Add other using statements here.

namespace com.Sconit.Service.View.Impl
{
    [Transactional]
    public class LeanEngineViewBaseMgr : SessionBase, ILeanEngineViewBaseMgr
    {
        protected ILeanEngineViewDao entityDao;
        
        public LeanEngineViewBaseMgr(ILeanEngineViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateLeanEngineView(LeanEngineView entity)
        {
            entityDao.CreateLeanEngineView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual LeanEngineView LoadLeanEngineView(Int32 flowDetId)
        {
            return entityDao.LoadLeanEngineView(flowDetId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<LeanEngineView> GetAllLeanEngineView()
        {
            return entityDao.GetAllLeanEngineView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateLeanEngineView(LeanEngineView entity)
        {
            entityDao.UpdateLeanEngineView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLeanEngineView(Int32 flowDetId)
        {
            entityDao.DeleteLeanEngineView(flowDetId);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLeanEngineView(LeanEngineView entity)
        {
            entityDao.DeleteLeanEngineView(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLeanEngineView(IList<Int32> pkList)
        {
            entityDao.DeleteLeanEngineView(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLeanEngineView(IList<LeanEngineView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteLeanEngineView(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
