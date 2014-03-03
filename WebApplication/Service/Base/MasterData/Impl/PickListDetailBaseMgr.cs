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
    public class PickListDetailBaseMgr : SessionBase, IPickListDetailBaseMgr
    {
        protected IPickListDetailDao entityDao;
        
        public PickListDetailBaseMgr(IPickListDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreatePickListDetail(PickListDetail entity)
        {
            entityDao.CreatePickListDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual PickListDetail LoadPickListDetail(Int32 id)
        {
            return entityDao.LoadPickListDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<PickListDetail> GetAllPickListDetail()
        {
            return entityDao.GetAllPickListDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdatePickListDetail(PickListDetail entity)
        {
            entityDao.UpdatePickListDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePickListDetail(Int32 id)
        {
            entityDao.DeletePickListDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePickListDetail(PickListDetail entity)
        {
            entityDao.DeletePickListDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePickListDetail(IList<Int32> pkList)
        {
            entityDao.DeletePickListDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePickListDetail(IList<PickListDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeletePickListDetail(entityList);
        }   
      
        #endregion Method Created By CodeSmith
    }
}
