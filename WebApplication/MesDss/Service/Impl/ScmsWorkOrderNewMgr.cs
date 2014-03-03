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
    public class ScmsWorkOrderNewMgr : IScmsWorkOrderNewMgr
    {
        public IScmsWorkOrderDaoNew entityDao { get; set; }
        public IScmsTableIndexMgr scmsTableIndexMgr { get; set; }


        [Transaction(TransactionMode.Requires)]
        public virtual void CreateScmsWorkOrder(ScmsWorkOrderNew entity)
        {
            entityDao.Create(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ScmsWorkOrderNew LoadScmsWorkOrder(string ordeNo, string itemCode)
        {
            string queryHql = "FROM ScmsWorkOrder ENTITY WHERE ENTITY.OrderNo='" + ordeNo + "' and ENTITY.ItemCode='" + itemCode + "'";
            IList<ScmsWorkOrderNew> scmsWorkOrderList = entityDao.FindAllWithCustomQuery<ScmsWorkOrderNew>(queryHql);
            return scmsWorkOrderList != null && scmsWorkOrderList.Count > 0 ? scmsWorkOrderList[0] : null;
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateScmsWorkOrder(ScmsWorkOrderNew entity)
        {
            entityDao.Update(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteScmsWorkOrder(ScmsWorkOrderNew entity)
        {
            entityDao.Delete(entity);
        }
    }
}
