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
    public class EmployeeBaseMgr : SessionBase, IEmployeeBaseMgr
    {
        protected IEmployeeDao entityDao;
        
        public EmployeeBaseMgr(IEmployeeDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateEmployee(Employee entity)
        {
            entityDao.CreateEmployee(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Employee LoadEmployee(String code)
        {
            return entityDao.LoadEmployee(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Employee> GetAllEmployee()
        {
            return entityDao.GetAllEmployee();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateEmployee(Employee entity)
        {
            entityDao.UpdateEmployee(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteEmployee(String code)
        {
            entityDao.DeleteEmployee(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteEmployee(Employee entity)
        {
            entityDao.DeleteEmployee(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteEmployee(IList<String> pkList)
        {
            entityDao.DeleteEmployee(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteEmployee(IList<Employee> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteEmployee(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
