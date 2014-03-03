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
    public class UomBaseMgr : SessionBase, IUomBaseMgr
    {
        protected IUomDao entityDao;
        
        public UomBaseMgr(IUomDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateUom(Uom entity)
        {
            entityDao.CreateUom(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Uom LoadUom(String code)
        {
            return entityDao.LoadUom(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Uom> GetAllUom()
        {
            return entityDao.GetAllUom();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateUom(Uom entity)
        {
            entityDao.UpdateUom(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUom(String code)
        {
            entityDao.DeleteUom(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUom(Uom entity)
        {
            entityDao.DeleteUom(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUom(IList<String> pkList)
        {
            entityDao.DeleteUom(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUom(IList<Uom> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteUom(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
