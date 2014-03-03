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
    public class BillAgingViewBaseMgr : SessionBase, IBillAgingViewBaseMgr
    {
        protected IBillAgingViewDao entityDao;
        
        public BillAgingViewBaseMgr(IBillAgingViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBillAgingView(BillAgingView entity)
        {
            entityDao.CreateBillAgingView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual BillAgingView LoadBillAgingView(Int32 id)
        {
            return entityDao.LoadBillAgingView(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<BillAgingView> GetAllBillAgingView()
        {
            return entityDao.GetAllBillAgingView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBillAgingView(BillAgingView entity)
        {
            entityDao.UpdateBillAgingView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillAgingView(Int32 id)
        {
            entityDao.DeleteBillAgingView(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillAgingView(BillAgingView entity)
        {
            entityDao.DeleteBillAgingView(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillAgingView(IList<Int32> pkList)
        {
            entityDao.DeleteBillAgingView(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillAgingView(IList<BillAgingView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteBillAgingView(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
