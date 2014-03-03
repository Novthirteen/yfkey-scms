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
    public class DssInboundControlBaseMgr : SessionBase, IDssInboundControlBaseMgr
    {
        protected IDssInboundControlDao entityDao;
        
        public DssInboundControlBaseMgr(IDssInboundControlDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateDssInboundControl(DssInboundControl entity)
        {
            entityDao.CreateDssInboundControl(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual DssInboundControl LoadDssInboundControl(Int32 id)
        {
            return entityDao.LoadDssInboundControl(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<DssInboundControl> GetAllDssInboundControl()
        {
            return entityDao.GetAllDssInboundControl();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateDssInboundControl(DssInboundControl entity)
        {
            entityDao.UpdateDssInboundControl(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssInboundControl(Int32 id)
        {
            entityDao.DeleteDssInboundControl(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssInboundControl(DssInboundControl entity)
        {
            entityDao.DeleteDssInboundControl(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssInboundControl(IList<Int32> pkList)
        {
            entityDao.DeleteDssInboundControl(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssInboundControl(IList<DssInboundControl> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteDssInboundControl(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
