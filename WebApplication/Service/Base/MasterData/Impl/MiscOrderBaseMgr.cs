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
    public class MiscOrderBaseMgr : SessionBase, IMiscOrderBaseMgr
    {
        protected IMiscOrderDao entityDao;
        
        public MiscOrderBaseMgr(IMiscOrderDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateMiscOrder(MiscOrder entity)
        {
            entityDao.CreateMiscOrder(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual MiscOrder LoadMiscOrder(String orderNo)
        {
            return entityDao.LoadMiscOrder(orderNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MiscOrder> GetAllMiscOrder()
        {
            return entityDao.GetAllMiscOrder();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMiscOrder(MiscOrder entity)
        {
            entityDao.UpdateMiscOrder(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMiscOrder(String orderNo)
        {
            entityDao.DeleteMiscOrder(orderNo);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMiscOrder(MiscOrder entity)
        {
            entityDao.DeleteMiscOrder(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMiscOrder(IList<String> pkList)
        {
            entityDao.DeleteMiscOrder(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMiscOrder(IList<MiscOrder> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteMiscOrder(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
