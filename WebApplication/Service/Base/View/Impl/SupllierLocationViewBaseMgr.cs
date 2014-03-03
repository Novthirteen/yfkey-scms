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
    public class SupllierLocationViewBaseMgr : SessionBase, ISupllierLocationViewBaseMgr
    {
        protected ISupllierLocationViewDao entityDao;
        
        public SupllierLocationViewBaseMgr(ISupllierLocationViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateSupllierLocationView(SupllierLocationView entity)
        {
            entityDao.CreateSupllierLocationView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual SupllierLocationView LoadSupllierLocationView(Int32 id)
        {
            return entityDao.LoadSupllierLocationView(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<SupllierLocationView> GetAllSupllierLocationView()
        {
            return entityDao.GetAllSupllierLocationView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateSupllierLocationView(SupllierLocationView entity)
        {
            entityDao.UpdateSupllierLocationView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSupllierLocationView(Int32 id)
        {
            entityDao.DeleteSupllierLocationView(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSupllierLocationView(SupllierLocationView entity)
        {
            entityDao.DeleteSupllierLocationView(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSupllierLocationView(IList<Int32> pkList)
        {
            entityDao.DeleteSupllierLocationView(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSupllierLocationView(IList<SupllierLocationView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteSupllierLocationView(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
