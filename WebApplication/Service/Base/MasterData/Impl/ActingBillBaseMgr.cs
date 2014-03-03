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
    public class ActingBillBaseMgr : SessionBase, IActingBillBaseMgr
    {
        protected IActingBillDao entityDao;
        
        public ActingBillBaseMgr(IActingBillDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateActingBill(ActingBill entity)
        {
            entityDao.CreateActingBill(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ActingBill LoadActingBill(Int32 id)
        {
            return entityDao.LoadActingBill(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ActingBill> GetAllActingBill()
        {
            return entityDao.GetAllActingBill();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateActingBill(ActingBill entity)
        {
            entityDao.UpdateActingBill(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteActingBill(Int32 id)
        {
            entityDao.DeleteActingBill(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteActingBill(ActingBill entity)
        {
            entityDao.DeleteActingBill(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteActingBill(IList<Int32> pkList)
        {
            entityDao.DeleteActingBill(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteActingBill(IList<ActingBill> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteActingBill(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
