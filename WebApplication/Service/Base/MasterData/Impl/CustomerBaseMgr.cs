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
    public class CustomerBaseMgr : SessionBase, ICustomerBaseMgr
    {
        protected ICustomerDao entityDao;
        
        public CustomerBaseMgr(ICustomerDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateCustomer(Customer entity)
        {
            entityDao.CreateCustomer(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Customer LoadCustomer(String code)
        {
            return entityDao.LoadCustomer(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Customer> GetAllCustomer()
        {
            return entityDao.GetAllCustomer(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Customer> GetAllCustomer(bool includeInactive)
        {
            return entityDao.GetAllCustomer(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCustomer(Customer entity)
        {
            entityDao.UpdateCustomer(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomer(String code)
        {
            entityDao.DeleteCustomer(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomer(Customer entity)
        {
            entityDao.DeleteCustomer(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomer(IList<String> pkList)
        {
            entityDao.DeleteCustomer(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomer(IList<Customer> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteCustomer(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
