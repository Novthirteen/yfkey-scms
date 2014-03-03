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
    public class ReceiptBaseMgr : SessionBase, IReceiptBaseMgr
    {
        protected IReceiptDao entityDao;
        
        public ReceiptBaseMgr(IReceiptDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateReceipt(Receipt entity)
        {
            entityDao.CreateReceipt(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Receipt LoadReceipt(String receiptNo)
        {
            return entityDao.LoadReceipt(receiptNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Receipt> GetAllReceipt()
        {
            return entityDao.GetAllReceipt();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateReceipt(Receipt entity)
        {
            entityDao.UpdateReceipt(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteReceipt(String receiptNo)
        {
            entityDao.DeleteReceipt(receiptNo);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteReceipt(Receipt entity)
        {
            entityDao.DeleteReceipt(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteReceipt(IList<String> pkList)
        {
            entityDao.DeleteReceipt(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteReceipt(IList<Receipt> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteReceipt(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
