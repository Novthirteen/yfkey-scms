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
    public class NumberControlBaseMgr : SessionBase, INumberControlBaseMgr
    {
        protected INumberControlDao entityDao;
        
        public NumberControlBaseMgr(INumberControlDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateNumberControl(NumberControl entity)
        {
            entityDao.CreateNumberControl(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual NumberControl LoadNumberControl(String code)
        {
            return entityDao.LoadNumberControl(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<NumberControl> GetAllNumberControl()
        {
            return entityDao.GetAllNumberControl();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateNumberControl(NumberControl entity)
        {
            entityDao.UpdateNumberControl(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteNumberControl(String code)
        {
            entityDao.DeleteNumberControl(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteNumberControl(NumberControl entity)
        {
            entityDao.DeleteNumberControl(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteNumberControl(IList<String> pkList)
        {
            entityDao.DeleteNumberControl(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteNumberControl(IList<NumberControl> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteNumberControl(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
