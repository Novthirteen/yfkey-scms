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
    public class ScmsMaterialBarcodeMgr : IScmsMaterialBarcodeMgr
    {
        public IScmsMaterialBarcodeDao entityDao { get; set; }
        public IScmsTableIndexMgr scmsTableIndexMgr { get; set; }


        [Transaction(TransactionMode.Requires)]
        public virtual void CreateScmsMaterialBarcode(ScmsMaterialBarcode entity)
        {
            entityDao.Create(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ScmsMaterialBarcode LoadScmsMaterialBarcode(string code)
        {
            return entityDao.FindById<ScmsMaterialBarcode>(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateScmsMaterialBarcode(ScmsMaterialBarcode entity)
        {
            entityDao.Update(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteScmsMaterialBarcode(ScmsMaterialBarcode entity)
        {
            entityDao.Delete(entity);
        }
    }
}
