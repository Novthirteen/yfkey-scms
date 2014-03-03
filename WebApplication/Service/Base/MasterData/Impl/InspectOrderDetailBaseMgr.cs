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
    public class InspectOrderDetailBaseMgr : SessionBase, IInspectOrderDetailBaseMgr
    {
        protected IInspectOrderDetailDao entityDao;
        
        public InspectOrderDetailBaseMgr(IInspectOrderDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateInspectOrderDetail(InspectOrderDetail entity)
        {
            entityDao.CreateInspectOrderDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual InspectOrderDetail LoadInspectOrderDetail(Int32 id)
        {
            return entityDao.LoadInspectOrderDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<InspectOrderDetail> GetAllInspectOrderDetail()
        {
            return entityDao.GetAllInspectOrderDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateInspectOrderDetail(InspectOrderDetail entity)
        {
            entityDao.UpdateInspectOrderDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInspectOrderDetail(Int32 id)
        {
            entityDao.DeleteInspectOrderDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInspectOrderDetail(InspectOrderDetail entity)
        {
            entityDao.DeleteInspectOrderDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInspectOrderDetail(IList<Int32> pkList)
        {
            entityDao.DeleteInspectOrderDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInspectOrderDetail(IList<InspectOrderDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteInspectOrderDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
