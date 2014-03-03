using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Transportation;
using com.Sconit.Persistence.Transportation;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class VehicleBaseMgr : SessionBase, IVehicleBaseMgr
    {
        public IVehicleDao entityDao { get; set; }
        
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateVehicle(Vehicle entity)
        {
            entityDao.CreateVehicle(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Vehicle LoadVehicle(String code)
        {
            return entityDao.LoadVehicle(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Vehicle> GetAllVehicle()
        {
            return entityDao.GetAllVehicle(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Vehicle> GetAllVehicle(bool includeInactive)
        {
            return entityDao.GetAllVehicle(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateVehicle(Vehicle entity)
        {
            entityDao.UpdateVehicle(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteVehicle(String code)
        {
            entityDao.DeleteVehicle(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteVehicle(Vehicle entity)
        {
            entityDao.DeleteVehicle(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteVehicle(IList<String> pkList)
        {
            entityDao.DeleteVehicle(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteVehicle(IList<Vehicle> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteVehicle(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
