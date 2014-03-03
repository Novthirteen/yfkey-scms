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
    public class ScmsTableIndexMgr : IScmsTableIndexMgr
    {
        public IScmsTableIndexDao entityDao { get; set; }

        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.MesDssOut");

        [Transaction(TransactionMode.Requires)]
        public void UpdateScmsTableIndex(string tableName, DateTime lastModifyDate)
        {
            ScmsTableIndex scmsTableIndex = this.LoadScmsTableIndex(tableName);
            if (scmsTableIndex == null)
            {
                log.Error("tableName[" + tableName + "] is not found");
            }
            else
            {
                if (scmsTableIndex.Flag == MesDssConstants.SCMS_TABLEINDEX_FLAG_FTPC_UPDATING)
                {
                    //表示更新中
                    log.Error("tableName[" + tableName + "] is updating " + lastModifyDate.ToString());
                }
                else
                {
                    scmsTableIndex.Flag = MesDssConstants.SCMS_TABLEINDEX_FLAG_SCMS_UPDATED;
                    scmsTableIndex.LastModifyDate = lastModifyDate;
                    this.UpdateScmsTableIndex(scmsTableIndex);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateScmsTableIndex(ScmsTableIndex entity)
        {
            entityDao.Create(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ScmsTableIndex LoadScmsTableIndex(string tableName)
        {
            return entityDao.FindById<ScmsTableIndex>(tableName);
        }


        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateScmsTableIndex(ScmsTableIndex entity)
        {
            entityDao.Update(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteScmsTableIndex(ScmsTableIndex entity)
        {
            entityDao.Delete(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ScmsTableIndex> GetUpdateScmsTableIndex()
        {
            return entityDao.FindAllWithCustomQuery<ScmsTableIndex>(@"FROM ScmsTableIndex ENTITY WHERE ENTITY.Flag in (" + MesDssConstants.SCMS_TABLEINDEX_FLAG_FTPC_UPDATED + "," + MesDssConstants.SCMS_TABLEINDEX_FLAG_SCMS_UPDATED + ")");
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void Complete(ScmsTableIndex scmsTableIndex)
        {
            scmsTableIndex.Flag = MesDssConstants.SCMS_TABLEINDEX_FLAG_SCMS_UPDATED;
            scmsTableIndex.LastModifyDate = DateTime.Now;
            this.UpdateScmsTableIndex(scmsTableIndex);
        }

    }
}
