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
    public class FinanceCalendarBaseMgr : SessionBase, IFinanceCalendarBaseMgr
    {
        protected IFinanceCalendarDao entityDao;
        public FinanceCalendarBaseMgr(IFinanceCalendarDao entityDao)
        {
            this.entityDao = entityDao;
        }
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateFinanceCalendar(FinanceCalendar entity)
        {
            entityDao.CreateFinanceCalendar(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual FinanceCalendar LoadFinanceCalendar(Int32 id)
        {
            return entityDao.LoadFinanceCalendar(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<FinanceCalendar> GetAllFinanceCalendar()
        {
            return entityDao.GetAllFinanceCalendar();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateFinanceCalendar(FinanceCalendar entity)
        {
            entityDao.UpdateFinanceCalendar(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFinanceCalendar(Int32 id)
        {
            entityDao.DeleteFinanceCalendar(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFinanceCalendar(FinanceCalendar entity)
        {
            entityDao.DeleteFinanceCalendar(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFinanceCalendar(IList<Int32> pkList)
        {
            entityDao.DeleteFinanceCalendar(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFinanceCalendar(IList<FinanceCalendar> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteFinanceCalendar(entityList);
        }   
        
        [Transaction(TransactionMode.Unspecified)]
        public virtual FinanceCalendar LoadFinanceCalendar(Int32 financeYear, Int32 financeMonth)
        {
            return entityDao.LoadFinanceCalendar(financeYear, financeMonth);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFinanceCalendar(Int32 financeYear, Int32 financeMonth)
        {
            entityDao.DeleteFinanceCalendar(financeYear, financeMonth);
        }   
        #endregion Method Created By CodeSmith
    }
}
