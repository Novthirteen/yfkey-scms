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
    public class BillBaseMgr : SessionBase, IBillBaseMgr
    {
        protected IBillDao entityDao;
        
        public BillBaseMgr(IBillDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBill(Bill entity)
        {
            entityDao.CreateBill(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Bill LoadBill(String billNo)
        {
            return entityDao.LoadBill(billNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Bill> GetAllBill()
        {
            return entityDao.GetAllBill();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBill(Bill entity)
        {
            entityDao.UpdateBill(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBill(String billNo)
        {
            entityDao.DeleteBill(billNo);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBill(Bill entity)
        {
            entityDao.DeleteBill(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBill(IList<String> pkList)
        {
            entityDao.DeleteBill(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBill(IList<Bill> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteBill(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
