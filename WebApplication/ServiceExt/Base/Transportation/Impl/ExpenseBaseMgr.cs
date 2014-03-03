using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Transportation;
using com.Sconit.Persistence.Transportation;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class ExpenseBaseMgr : SessionBase, IExpenseBaseMgr
    {
        protected IExpenseDao entityDao;
        
        public ExpenseBaseMgr(IExpenseDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateExpense(Expense entity)
        {
            entityDao.CreateExpense(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Expense LoadExpense(String code)
        {
            return entityDao.LoadExpense(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Expense> GetAllExpense()
        {
            return entityDao.GetAllExpense();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateExpense(Expense entity)
        {
            entityDao.UpdateExpense(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteExpense(String code)
        {
            entityDao.DeleteExpense(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteExpense(Expense entity)
        {
            entityDao.DeleteExpense(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteExpense(IList<String> pkList)
        {
            entityDao.DeleteExpense(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteExpense(IList<Expense> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteExpense(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
