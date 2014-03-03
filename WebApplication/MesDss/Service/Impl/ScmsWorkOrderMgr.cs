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
    public class ScmsWorkOrderMgr : IScmsWorkOrderMgr
    {
        public IScmsWorkOrderDao entityDao { get; set; }
        public IScmsTableIndexMgr scmsTableIndexMgr { get; set; }


        [Transaction(TransactionMode.Requires)]
        public virtual void CreateScmsWorkOrder(ScmsWorkOrder entity)
        {
            entityDao.Create(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ScmsWorkOrder LoadScmsWorkOrder(string ordeNo,string itemCode)
        {
            string queryHql = "FROM ScmsWorkOrder ENTITY WHERE ENTITY.OrderNo='" + ordeNo + "' and ENTITY.ItemCode='" + itemCode + "'";
            IList<ScmsWorkOrder> scmsWorkOrderList = entityDao.FindAllWithCustomQuery<ScmsWorkOrder>(queryHql);
            return scmsWorkOrderList != null && scmsWorkOrderList.Count > 0 ? scmsWorkOrderList[0] : null;
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateScmsWorkOrder(ScmsWorkOrder entity)
        {
            entityDao.Update(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteScmsWorkOrder(ScmsWorkOrder entity)
        {
            entityDao.Delete(entity);
        }
    }
}
