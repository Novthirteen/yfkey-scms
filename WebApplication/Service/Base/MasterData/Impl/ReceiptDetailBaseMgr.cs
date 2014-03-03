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
    public class ReceiptDetailBaseMgr : SessionBase, IReceiptDetailBaseMgr
    {
        protected IReceiptDetailDao entityDao;
        
        public ReceiptDetailBaseMgr(IReceiptDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateReceiptDetail(ReceiptDetail entity)
        {
            entityDao.CreateReceiptDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ReceiptDetail LoadReceiptDetail(Int32 id)
        {
            return entityDao.LoadReceiptDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ReceiptDetail> GetAllReceiptDetail()
        {
            return entityDao.GetAllReceiptDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateReceiptDetail(ReceiptDetail entity)
        {
            entityDao.UpdateReceiptDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteReceiptDetail(Int32 id)
        {
            entityDao.DeleteReceiptDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteReceiptDetail(ReceiptDetail entity)
        {
            entityDao.DeleteReceiptDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteReceiptDetail(IList<Int32> pkList)
        {
            entityDao.DeleteReceiptDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteReceiptDetail(IList<ReceiptDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteReceiptDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
