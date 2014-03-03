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
    public class MiscOrderDetailBaseMgr : SessionBase, IMiscOrderDetailBaseMgr
    {
        protected IMiscOrderDetailDao entityDao;
        
        public MiscOrderDetailBaseMgr(IMiscOrderDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateMiscOrderDetail(MiscOrderDetail entity)
        {
            entityDao.CreateMiscOrderDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual MiscOrderDetail LoadMiscOrderDetail(Int32 id)
        {
            return entityDao.LoadMiscOrderDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MiscOrderDetail> GetAllMiscOrderDetail()
        {
            return entityDao.GetAllMiscOrderDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMiscOrderDetail(MiscOrderDetail entity)
        {
            entityDao.UpdateMiscOrderDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMiscOrderDetail(Int32 id)
        {
            entityDao.DeleteMiscOrderDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMiscOrderDetail(MiscOrderDetail entity)
        {
            entityDao.DeleteMiscOrderDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMiscOrderDetail(IList<Int32> pkList)
        {
            entityDao.DeleteMiscOrderDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMiscOrderDetail(IList<MiscOrderDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteMiscOrderDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
