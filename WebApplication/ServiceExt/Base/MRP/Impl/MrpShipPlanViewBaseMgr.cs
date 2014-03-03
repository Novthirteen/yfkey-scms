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
    public class MrpShipPlanViewBaseMgr : SessionBase, IMrpShipPlanViewBaseMgr
    {
        protected IMrpShipPlanViewDao entityDao;

        public MrpShipPlanViewBaseMgr(IMrpShipPlanViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateMrpShipPlanView(MrpShipPlanView entity)
        {
            entityDao.CreateMrpShipPlanView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual MrpShipPlanView LoadMrpShipPlanView(String flow, String item, String uom, Decimal uC, DateTime startTime, DateTime windowTime, DateTime effDate)
        {
            return entityDao.LoadMrpShipPlanView(flow, item, uom, uC, startTime, windowTime, effDate);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MrpShipPlanView> GetAllMrpShipPlanView()
        {
            return entityDao.GetAllMrpShipPlanView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMrpShipPlanView(MrpShipPlanView entity)
        {
            entityDao.UpdateMrpShipPlanView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpShipPlanView(String flow, String item, String uom, Decimal uC, DateTime startTime, DateTime windowTime, DateTime effDate)
        {
            entityDao.DeleteMrpShipPlanView(flow, item, uom, uC, startTime, windowTime, effDate);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpShipPlanView(MrpShipPlanView entity)
        {
            entityDao.DeleteMrpShipPlanView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpShipPlanView(IList<MrpShipPlanView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteMrpShipPlanView(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
