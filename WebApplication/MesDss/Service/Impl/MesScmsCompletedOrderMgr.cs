using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity.Exception;
using com.Mes.Dss.Persistence;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service.Impl
{
    [Transactional]
    public class MesScmsCompletedOrderMgr : IMesScmsCompletedOrderMgr
    {
        public IMesScmsCompletedOrderDao entityDao { get; set; }
        public IMesScmsTableIndexMgr mesScmsTableIndexMgr { get; set; }



        [Transaction(TransactionMode.Unspecified)]
        public virtual MesScmsCompletedOrder LoadMesScmsCompletedOrder(string code)
        {
            return entityDao.FindById<MesScmsCompletedOrder>(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMesScmsCompletedOrder(MesScmsCompletedOrder entity)
        {
            entityDao.Update(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MesScmsCompletedOrder> GetUpdateMesScmsCompletedOrder()
        {
            return entityDao.FindAllWithCustomQuery<MesScmsCompletedOrder>("FROM MesScmsCompletedOrder ENTITY WHERE ENTITY.Flag IN (" + MesDssConstants.MES_SCMS_FLAG_FTPC_UPDATED + "," + MesDssConstants.MES_SCMS_FLAG_FTPC_DELETED + ")");
        }

        [Transaction(TransactionMode.Requires)]
        public void Complete(MesScmsCompletedOrder mesScmsCompletedOrder)
        {
            mesScmsCompletedOrder.Flag = MesDssConstants.MES_SCMS_FLAG_SCMS_UPDATED;
            this.UpdateMesScmsCompletedOrder(mesScmsCompletedOrder);
        }

    }
}
