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
    public class ClientOrderDetailBaseMgr : SessionBase, IClientOrderDetailBaseMgr
    {
        protected IClientOrderDetailDao entityDao;
        
        public ClientOrderDetailBaseMgr(IClientOrderDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateClientOrderDetail(ClientOrderDetail entity)
        {
            entityDao.CreateClientOrderDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ClientOrderDetail LoadClientOrderDetail(Int32 id)
        {
            return entityDao.LoadClientOrderDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ClientOrderDetail> GetAllClientOrderDetail()
        {
            return entityDao.GetAllClientOrderDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateClientOrderDetail(ClientOrderDetail entity)
        {
            entityDao.UpdateClientOrderDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientOrderDetail(Int32 id)
        {
            entityDao.DeleteClientOrderDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientOrderDetail(ClientOrderDetail entity)
        {
            entityDao.DeleteClientOrderDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientOrderDetail(IList<Int32> pkList)
        {
            entityDao.DeleteClientOrderDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientOrderDetail(IList<ClientOrderDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteClientOrderDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
