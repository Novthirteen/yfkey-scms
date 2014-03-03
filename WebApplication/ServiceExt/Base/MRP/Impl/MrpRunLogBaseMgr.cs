using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MRP;
using com.Sconit.Persistence.MRP;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP.Impl
{
    [Transactional]
    public class MrpRunLogBaseMgr : SessionBase, IMrpRunLogBaseMgr
    {
        protected IMrpRunLogDao entityDao;

        public MrpRunLogBaseMgr(IMrpRunLogDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateMrpRunLog(MrpRunLog entity)
        {
            entityDao.CreateMrpRunLog(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual MrpRunLog LoadMrpRunLog(Int32 id)
        {
            return entityDao.LoadMrpRunLog(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MrpRunLog> GetAllMrpRunLog()
        {
            return entityDao.GetAllMrpRunLog();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMrpRunLog(MrpRunLog entity)
        {
            entityDao.UpdateMrpRunLog(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpRunLog(Int32 id)
        {
            entityDao.DeleteMrpRunLog(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpRunLog(MrpRunLog entity)
        {
            entityDao.DeleteMrpRunLog(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpRunLog(IList<Int32> pkList)
        {
            entityDao.DeleteMrpRunLog(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpRunLog(IList<MrpRunLog> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteMrpRunLog(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
