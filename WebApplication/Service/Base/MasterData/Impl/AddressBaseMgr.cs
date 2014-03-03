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
    public class AddressBaseMgr : SessionBase, IAddressBaseMgr
    {
        protected IAddressDao entityDao;
        
        public AddressBaseMgr(IAddressDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateAddress(Address entity)
        {
            entityDao.CreateAddress(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Address LoadAddress(String code)
        {
            return entityDao.LoadAddress(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Address> GetAllAddress()
        {
            return entityDao.GetAllAddress(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Address> GetAllAddress(bool includeInactive)
        {
            return entityDao.GetAllAddress(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateAddress(Address entity)
        {
            entityDao.UpdateAddress(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAddress(String code)
        {
            entityDao.DeleteAddress(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAddress(Address entity)
        {
            entityDao.DeleteAddress(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAddress(IList<String> pkList)
        {
            entityDao.DeleteAddress(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAddress(IList<Address> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteAddress(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
