using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Distribution;
using com.Sconit.Persistence.Distribution;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Distribution.Impl
{
    [Transactional]
    public class InProcessLocationDetailBaseMgr : SessionBase, IInProcessLocationDetailBaseMgr
    {
        protected IInProcessLocationDetailDao entityDao;
        
        public InProcessLocationDetailBaseMgr(IInProcessLocationDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateInProcessLocationDetail(InProcessLocationDetail entity)
        {
            entityDao.CreateInProcessLocationDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual InProcessLocationDetail LoadInProcessLocationDetail(Int32 id)
        {
            return entityDao.LoadInProcessLocationDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<InProcessLocationDetail> GetAllInProcessLocationDetail()
        {
            return entityDao.GetAllInProcessLocationDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateInProcessLocationDetail(InProcessLocationDetail entity)
        {
            entityDao.UpdateInProcessLocationDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetail(Int32 id)
        {
            entityDao.DeleteInProcessLocationDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetail(InProcessLocationDetail entity)
        {
            entityDao.DeleteInProcessLocationDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetail(IList<Int32> pkList)
        {
            entityDao.DeleteInProcessLocationDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetail(IList<InProcessLocationDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteInProcessLocationDetail(entityList);
        }   
        
        [Transaction(TransactionMode.Unspecified)]
        public virtual InProcessLocationDetail LoadInProcessLocationDetail(com.Sconit.Entity.Distribution.InProcessLocation inProcessLocation, com.Sconit.Entity.MasterData.OrderLocationTransaction orderLocationTransaction, String lotNo)
        {
            return entityDao.LoadInProcessLocationDetail(inProcessLocation, orderLocationTransaction, lotNo);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationDetail(String inProcessLocationIpNo, Int32 orderLocationTransactionId, String lotNo)
        {
            entityDao.DeleteInProcessLocationDetail(inProcessLocationIpNo, orderLocationTransactionId, lotNo);
        }   
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual InProcessLocationDetail LoadInProcessLocationDetail(String inProcessLocationIpNo, Int32 orderLocationTransactionId, String lotNo)
        {
            return entityDao.LoadInProcessLocationDetail(inProcessLocationIpNo, orderLocationTransactionId, lotNo);
        }
        #endregion Method Created By CodeSmith
    }
}
