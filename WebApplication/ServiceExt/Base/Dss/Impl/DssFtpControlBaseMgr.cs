using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Dss;
using com.Sconit.Persistence.Dss;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Dss.Impl
{
    [Transactional]
    public class DssFtpControlBaseMgr : SessionBase, IDssFtpControlBaseMgr
    {
        protected IDssFtpControlDao entityDao;
        
        public DssFtpControlBaseMgr(IDssFtpControlDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateDssFtpControl(DssFtpControl entity)
        {
            entityDao.CreateDssFtpControl(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual DssFtpControl LoadDssFtpControl(Int32 id)
        {
            return entityDao.LoadDssFtpControl(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<DssFtpControl> GetAllDssFtpControl()
        {
            return entityDao.GetAllDssFtpControl();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateDssFtpControl(DssFtpControl entity)
        {
            entityDao.UpdateDssFtpControl(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssFtpControl(Int32 id)
        {
            entityDao.DeleteDssFtpControl(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssFtpControl(DssFtpControl entity)
        {
            entityDao.DeleteDssFtpControl(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssFtpControl(IList<Int32> pkList)
        {
            entityDao.DeleteDssFtpControl(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssFtpControl(IList<DssFtpControl> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteDssFtpControl(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
