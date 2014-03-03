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
    public class ScmsPartMgr : IScmsPartMgr
    {
        public IScmsPartDao entityDao { get; set; }
        public IScmsTableIndexMgr scmsTableIndexMgr { get; set; }


        [Transaction(TransactionMode.Requires)]
        public virtual void CreateScmsPart(ScmsPart entity)
        {
            entityDao.Create(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ScmsPart LoadScmsPart(string code)
        {
            return entityDao.FindById<ScmsPart>(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateScmsPart(ScmsPart entity)
        {
            entityDao.Update(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteScmsPart(ScmsPart entity)
        {
            entityDao.Delete(entity);
        }
    }
}
