using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MRP;
using com.Sconit.Persistence.MRP;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP.Impl
{
    [Transactional]
    public class CustomerScheduleBaseMgr : SessionBase, ICustomerScheduleBaseMgr
    {
        protected ICustomerScheduleDao entityDao;

        public CustomerScheduleBaseMgr(ICustomerScheduleDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateCustomerSchedule(CustomerSchedule entity)
        {
            entityDao.CreateCustomerSchedule(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual CustomerSchedule LoadCustomerSchedule(Int32 id)
        {
            return entityDao.LoadCustomerSchedule(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<CustomerSchedule> GetAllCustomerSchedule()
        {
            return entityDao.GetAllCustomerSchedule();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCustomerSchedule(CustomerSchedule entity)
        {
            entityDao.UpdateCustomerSchedule(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerSchedule(Int32 id)
        {
            entityDao.DeleteCustomerSchedule(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerSchedule(CustomerSchedule entity)
        {
            entityDao.DeleteCustomerSchedule(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerSchedule(IList<Int32> pkList)
        {
            entityDao.DeleteCustomerSchedule(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerSchedule(IList<CustomerSchedule> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteCustomerSchedule(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
