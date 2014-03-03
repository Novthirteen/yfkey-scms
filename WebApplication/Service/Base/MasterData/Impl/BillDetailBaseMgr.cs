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
    public class BillDetailBaseMgr : SessionBase, IBillDetailBaseMgr
    {
        protected IBillDetailDao entityDao;
        
        public BillDetailBaseMgr(IBillDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBillDetail(BillDetail entity)
        {
            entityDao.CreateBillDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual BillDetail LoadBillDetail(Int32 id)
        {
            return entityDao.LoadBillDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<BillDetail> GetAllBillDetail()
        {
            return entityDao.GetAllBillDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBillDetail(BillDetail entity)
        {
            entityDao.UpdateBillDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillDetail(Int32 id)
        {
            entityDao.DeleteBillDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillDetail(BillDetail entity)
        {
            entityDao.DeleteBillDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillDetail(IList<Int32> pkList)
        {
            entityDao.DeleteBillDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillDetail(IList<BillDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteBillDetail(entityList);
        }   
        
        [Transaction(TransactionMode.Unspecified)]
        public virtual BillDetail LoadBillDetail(com.Sconit.Entity.MasterData.Bill billNo, com.Sconit.Entity.MasterData.ActingBill actingBill)
        {
            return entityDao.LoadBillDetail(billNo, actingBill);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillDetail(String billNoBillNo, Int32 actingBillId)
        {
            entityDao.DeleteBillDetail(billNoBillNo, actingBillId);
        }   
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual BillDetail LoadBillDetail(String billNoBillNo, Int32 actingBillId)
        {
            return entityDao.LoadBillDetail(billNoBillNo, actingBillId);
        }
        #endregion Method Created By CodeSmith
    }
}
