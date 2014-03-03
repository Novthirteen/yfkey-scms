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
    public class ActingBillViewBaseMgr : SessionBase, IActingBillViewBaseMgr
    {
        protected IActingBillViewDao entityDao;
        
        public ActingBillViewBaseMgr(IActingBillViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateActingBillView(ActingBillView entity)
        {
            entityDao.CreateActingBillView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ActingBillView LoadActingBillView(Int32 id)
        {
            return entityDao.LoadActingBillView(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ActingBillView> GetAllActingBillView()
        {
            return entityDao.GetAllActingBillView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateActingBillView(ActingBillView entity)
        {
            entityDao.UpdateActingBillView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteActingBillView(Int32 id)
        {
            entityDao.DeleteActingBillView(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteActingBillView(ActingBillView entity)
        {
            entityDao.DeleteActingBillView(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteActingBillView(IList<Int32> pkList)
        {
            entityDao.DeleteActingBillView(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteActingBillView(IList<ActingBillView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteActingBillView(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
