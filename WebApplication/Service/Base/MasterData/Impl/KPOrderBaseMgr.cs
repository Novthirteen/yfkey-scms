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
    public class KPOrderBaseMgr : SessionBase, IKPOrderBaseMgr
    {
        protected IKPOrderDao entityDao;
        
        public KPOrderBaseMgr(IKPOrderDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateKPOrder(KPOrder entity)
        {
            entityDao.CreateKPOrder(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual KPOrder LoadKPOrder(Decimal oRDER_ID)
        {
            return entityDao.LoadKPOrder(oRDER_ID);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<KPOrder> GetAllKPOrder()
        {
            return entityDao.GetAllKPOrder();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateKPOrder(KPOrder entity)
        {
            entityDao.UpdateKPOrder(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteKPOrder(Decimal oRDER_ID)
        {
            entityDao.DeleteKPOrder(oRDER_ID);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteKPOrder(KPOrder entity)
        {
            entityDao.DeleteKPOrder(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteKPOrder(IList<Decimal> pkList)
        {
            entityDao.DeleteKPOrder(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteKPOrder(IList<KPOrder> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteKPOrder(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
