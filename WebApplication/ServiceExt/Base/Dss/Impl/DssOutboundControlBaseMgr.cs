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
    public class DssOutboundControlBaseMgr : SessionBase, IDssOutboundControlBaseMgr
    {
        protected IDssOutboundControlDao entityDao;
        
        public DssOutboundControlBaseMgr(IDssOutboundControlDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateDssOutboundControl(DssOutboundControl entity)
        {
            entityDao.CreateDssOutboundControl(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual DssOutboundControl LoadDssOutboundControl(Int32 id)
        {
            return entityDao.LoadDssOutboundControl(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<DssOutboundControl> GetAllDssOutboundControl()
        {
            return entityDao.GetAllDssOutboundControl(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<DssOutboundControl> GetAllDssOutboundControl(bool includeInactive)
        {
            return entityDao.GetAllDssOutboundControl(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateDssOutboundControl(DssOutboundControl entity)
        {
            entityDao.UpdateDssOutboundControl(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssOutboundControl(Int32 id)
        {
            entityDao.DeleteDssOutboundControl(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssOutboundControl(DssOutboundControl entity)
        {
            entityDao.DeleteDssOutboundControl(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssOutboundControl(IList<Int32> pkList)
        {
            entityDao.DeleteDssOutboundControl(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssOutboundControl(IList<DssOutboundControl> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteDssOutboundControl(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
