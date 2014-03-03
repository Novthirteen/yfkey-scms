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
    public class PickListBaseMgr : SessionBase, IPickListBaseMgr
    {
        protected IPickListDao entityDao;
        
        public PickListBaseMgr(IPickListDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreatePickList(PickList entity)
        {
            entityDao.CreatePickList(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual PickList LoadPickList(String pickListNo)
        {
            return entityDao.LoadPickList(pickListNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<PickList> GetAllPickList()
        {
            return entityDao.GetAllPickList();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdatePickList(PickList entity)
        {
            entityDao.UpdatePickList(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePickList(String pickListNo)
        {
            entityDao.DeletePickList(pickListNo);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePickList(PickList entity)
        {
            entityDao.DeletePickList(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePickList(IList<String> pkList)
        {
            entityDao.DeletePickList(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePickList(IList<PickList> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeletePickList(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
