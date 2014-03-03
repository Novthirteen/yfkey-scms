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
    public class CarrierBaseMgr : SessionBase, ICarrierBaseMgr
    {
        public ICarrierDao entityDao { get; set; }

        public CarrierBaseMgr(ICarrierDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateCarrier(Carrier entity)
        {
            entityDao.CreateCarrier(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Carrier LoadCarrier(String code)
        {
            return entityDao.LoadCarrier(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Carrier> GetAllCarrier()
        {
            return entityDao.GetAllCarrier();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCarrier(Carrier entity)
        {
            entityDao.UpdateCarrier(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCarrier(String code)
        {
            entityDao.DeleteCarrier(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCarrier(Carrier entity)
        {
            entityDao.DeleteCarrier(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCarrier(IList<String> pkList)
        {
            entityDao.DeleteCarrier(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCarrier(IList<Carrier> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteCarrier(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
