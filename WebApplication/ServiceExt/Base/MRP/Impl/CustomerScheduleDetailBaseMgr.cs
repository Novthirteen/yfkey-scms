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
    public class CustomerScheduleDetailBaseMgr : SessionBase, ICustomerScheduleDetailBaseMgr
    {
        protected ICustomerScheduleDetailDao entityDao;

        public CustomerScheduleDetailBaseMgr(ICustomerScheduleDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateCustomerScheduleDetail(CustomerScheduleDetail entity)
        {
            entityDao.CreateCustomerScheduleDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual CustomerScheduleDetail LoadCustomerScheduleDetail(Int32 id)
        {
            return entityDao.LoadCustomerScheduleDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<CustomerScheduleDetail> GetAllCustomerScheduleDetail()
        {
            return entityDao.GetAllCustomerScheduleDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCustomerScheduleDetail(CustomerScheduleDetail entity)
        {
            entityDao.UpdateCustomerScheduleDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerScheduleDetail(Int32 id)
        {
            entityDao.DeleteCustomerScheduleDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerScheduleDetail(CustomerScheduleDetail entity)
        {
            entityDao.DeleteCustomerScheduleDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerScheduleDetail(IList<Int32> pkList)
        {
            entityDao.DeleteCustomerScheduleDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerScheduleDetail(IList<CustomerScheduleDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteCustomerScheduleDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
