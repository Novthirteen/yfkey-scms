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
    public class InProcessLocationDetailTrackViewBaseMgr : SessionBase, IInProcessLocationDetailTrackViewBaseMgr
    {
        protected IInProcessLocationDetailTrackViewDao entityDao;
        
        public InProcessLocationDetailTrackViewBaseMgr(IInProcessLocationDetailTrackViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateInProcessLocationDetailTrackView(InProcessLocationDetailTrackView entity)
        {
            entityDao.CreateInProcessLocationDetailTrackView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual InProcessLocationDetailTrackView LoadInProcessLocationDetailTrackView(Int32 id)
        {
            return entityDao.LoadInProcessLocationDetailTrackView(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<InProcessLocationDetailTrackView> GetAllInProcessLocationDetailTrackView()
        {
            return entityDao.GetAllInProcessLocationDetailTrackView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateInProcessLocationDetailTrackView(InProcessLocationDetailTrackView entity)
        {
            entityDao.UpdateInProcessLocationDetailTrackView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetailTrackView(Int32 id)
        {
            entityDao.DeleteInProcessLocationDetailTrackView(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetailTrackView(InProcessLocationDetailTrackView entity)
        {
            entityDao.DeleteInProcessLocationDetailTrackView(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetailTrackView(IList<Int32> pkList)
        {
            entityDao.DeleteInProcessLocationDetailTrackView(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetailTrackView(IList<InProcessLocationDetailTrackView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteInProcessLocationDetailTrackView(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
