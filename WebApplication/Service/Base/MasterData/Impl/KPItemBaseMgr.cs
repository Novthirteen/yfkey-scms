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
    public class KPItemBaseMgr : SessionBase, IKPItemBaseMgr
    {
        protected IKPItemDao entityDao;
        
        public KPItemBaseMgr(IKPItemDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateKPItem(KPItem entity)
        {
            entityDao.CreateKPItem(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual KPItem LoadKPItem(Decimal iTEM_SEQ_ID)
        {
            return entityDao.LoadKPItem(iTEM_SEQ_ID);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<KPItem> GetAllKPItem()
        {
            return entityDao.GetAllKPItem();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateKPItem(KPItem entity)
        {
            entityDao.UpdateKPItem(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteKPItem(Decimal iTEM_SEQ_ID)
        {
            entityDao.DeleteKPItem(iTEM_SEQ_ID);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteKPItem(KPItem entity)
        {
            entityDao.DeleteKPItem(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteKPItem(IList<Decimal> pkList)
        {
            entityDao.DeleteKPItem(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteKPItem(IList<KPItem> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteKPItem(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
