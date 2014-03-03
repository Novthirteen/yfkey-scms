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
    public class ScmsBomMgr : IScmsBomMgr
    {
        public IScmsBomDao entityDao { get; set; }
        public IScmsTableIndexMgr scmsTableIndexMgr { get; set; }


        [Transaction(TransactionMode.Requires)]
        public virtual void CreateScmsBom(ScmsBom entity)
        {
            entityDao.Create(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ScmsBom LoadScmsBom(string bomCode,string itemCode)
        {
            string queryHql = "FROM ScmsBom ENTITY WHERE ENTITY.Bom=" + bomCode + " and ENTITY.ItemCode=" + itemCode;
            IList<ScmsBom> bomList = entityDao.FindAllWithCustomQuery<ScmsBom>(queryHql);
            return bomList != null && bomList.Count > 0 ? bomList[0] : null;
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateScmsBom(ScmsBom entity)
        {
            entityDao.Update(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteScmsBom(ScmsBom entity)
        {
            entityDao.Delete(entity);
        }
    }
}
