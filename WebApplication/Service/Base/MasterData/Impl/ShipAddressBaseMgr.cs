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
    public class ShipAddressBaseMgr : SessionBase, IShipAddressBaseMgr
    {
        protected IShipAddressDao entityDao;
        
        public ShipAddressBaseMgr(IShipAddressDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateShipAddress(ShipAddress entity)
        {
            entityDao.CreateShipAddress(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ShipAddress LoadShipAddress(String code)
        {
            return entityDao.LoadShipAddress(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ShipAddress> GetAllShipAddress()
        {
            return entityDao.GetAllShipAddress(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ShipAddress> GetAllShipAddress(bool includeInactive)
        {
            return entityDao.GetAllShipAddress(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateShipAddress(ShipAddress entity)
        {
            entityDao.UpdateShipAddress(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShipAddress(String code)
        {
            entityDao.DeleteShipAddress(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShipAddress(ShipAddress entity)
        {
            entityDao.DeleteShipAddress(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShipAddress(IList<String> pkList)
        {
            entityDao.DeleteShipAddress(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShipAddress(IList<ShipAddress> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteShipAddress(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
