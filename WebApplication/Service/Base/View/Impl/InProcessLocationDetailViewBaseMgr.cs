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
    public class InProcessLocationDetailViewBaseMgr : SessionBase, IInProcessLocationDetailViewBaseMgr
    {
        protected IInProcessLocationDetailViewDao entityDao;
        
        public InProcessLocationDetailViewBaseMgr(IInProcessLocationDetailViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateInProcessLocationDetailView(InProcessLocationDetailView entity)
        {
            entityDao.CreateInProcessLocationDetailView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual InProcessLocationDetailView LoadInProcessLocationDetailView(Int32 id)
        {
            return entityDao.LoadInProcessLocationDetailView(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<InProcessLocationDetailView> GetAllInProcessLocationDetailView()
        {
            return entityDao.GetAllInProcessLocationDetailView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateInProcessLocationDetailView(InProcessLocationDetailView entity)
        {
            entityDao.UpdateInProcessLocationDetailView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetailView(Int32 id)
        {
            entityDao.DeleteInProcessLocationDetailView(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetailView(InProcessLocationDetailView entity)
        {
            entityDao.DeleteInProcessLocationDetailView(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetailView(IList<Int32> pkList)
        {
            entityDao.DeleteInProcessLocationDetailView(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetailView(IList<InProcessLocationDetailView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteInProcessLocationDetailView(entityList);
        }   
        
        [Transaction(TransactionMode.Unspecified)]
        public virtual InProcessLocationDetailView LoadInProcessLocationDetailView(com.Sconit.Entity.Distribution.InProcessLocation inProcessLocation)
        {
            return entityDao.LoadInProcessLocationDetailView(inProcessLocation);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetailView(String inProcessLocationIpNo)
        {
            entityDao.DeleteInProcessLocationDetailView(inProcessLocationIpNo);
        }   
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual InProcessLocationDetailView LoadInProcessLocationDetailView(String inProcessLocationIpNo)
        {
            return entityDao.LoadInProcessLocationDetailView(inProcessLocationIpNo);
        }
        
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetailView(IList<com.Sconit.Entity.Distribution.InProcessLocation> UniqueList)
        {
            entityDao.DeleteInProcessLocationDetailView(UniqueList);
        }   
        #endregion Method Created By CodeSmith
    }
}
