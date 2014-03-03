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
    public class ClientWorkingHoursBaseMgr : SessionBase, IClientWorkingHoursBaseMgr
    {
        protected IClientWorkingHoursDao entityDao;
        
        public ClientWorkingHoursBaseMgr(IClientWorkingHoursDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateClientWorkingHours(ClientWorkingHours entity)
        {
            entityDao.CreateClientWorkingHours(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ClientWorkingHours LoadClientWorkingHours(Int32 id)
        {
            return entityDao.LoadClientWorkingHours(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ClientWorkingHours> GetAllClientWorkingHours()
        {
            return entityDao.GetAllClientWorkingHours();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateClientWorkingHours(ClientWorkingHours entity)
        {
            entityDao.UpdateClientWorkingHours(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientWorkingHours(Int32 id)
        {
            entityDao.DeleteClientWorkingHours(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientWorkingHours(ClientWorkingHours entity)
        {
            entityDao.DeleteClientWorkingHours(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientWorkingHours(IList<Int32> pkList)
        {
            entityDao.DeleteClientWorkingHours(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientWorkingHours(IList<ClientWorkingHours> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteClientWorkingHours(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
