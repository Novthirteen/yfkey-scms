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
    public class LocationTransactionViewBaseMgr : SessionBase, ILocationTransactionViewBaseMgr
    {
        protected ILocationTransactionViewDao entityDao;
        
        public LocationTransactionViewBaseMgr(ILocationTransactionViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateLocationTransactionView(LocationTransactionView entity)
        {
            entityDao.CreateLocationTransactionView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual LocationTransactionView LoadLocationTransactionView(Int32 id)
        {
            return entityDao.LoadLocationTransactionView(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<LocationTransactionView> GetAllLocationTransactionView()
        {
            return entityDao.GetAllLocationTransactionView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateLocationTransactionView(LocationTransactionView entity)
        {
            entityDao.UpdateLocationTransactionView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationTransactionView(Int32 id)
        {
            entityDao.DeleteLocationTransactionView(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationTransactionView(LocationTransactionView entity)
        {
            entityDao.DeleteLocationTransactionView(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationTransactionView(IList<Int32> pkList)
        {
            entityDao.DeleteLocationTransactionView(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteLocationTransactionView(IList<LocationTransactionView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteLocationTransactionView(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
