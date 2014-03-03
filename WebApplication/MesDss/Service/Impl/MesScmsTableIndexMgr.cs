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
    public class MesScmsTableIndexMgr : IMesScmsTableIndexMgr
    {
        public IMesScmsTableIndexDao entityDao { get; set; }

        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.MesDssIn");

        [Transaction(TransactionMode.Requires)]
        public void UpdateMesScmsTableIndex(string tableName, DateTime lastModifyDate)
        {
            MesScmsTableIndex mesScmsTableIndex = this.LoadMesScmsTableIndex(tableName);
            if (mesScmsTableIndex == null)
            {
                log.Error("tableName[" + tableName + "] is not found");
            }
            else
            {
                mesScmsTableIndex.Flag = MesDssConstants.MES_SCMS_TABLEINDEX_FLAG_SCMS_UPDATED;
                mesScmsTableIndex.LastModifyDate = lastModifyDate;
                this.UpdateMesScmsTableIndex(mesScmsTableIndex);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateMesScmsTableIndex(MesScmsTableIndex entity)
        {
            entityDao.Create(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual MesScmsTableIndex LoadMesScmsTableIndex(string tableName)
        {
            return entityDao.FindById<MesScmsTableIndex>(tableName);
        }


        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMesScmsTableIndex(MesScmsTableIndex entity)
        {
            entityDao.Update(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMesScmsTableIndex(MesScmsTableIndex entity)
        {
            entityDao.Delete(entity);
        }


        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MesScmsTableIndex> GetUpdateMesScmsTableIndex()
        {
            return entityDao.FindAllWithCustomQuery<MesScmsTableIndex>(@"FROM MesScmsTableIndex ENTITY WHERE ENTITY.Flag in (" + MesDssConstants.MES_SCMS_TABLEINDEX_FLAG_FTPC_UPDATED + "," + MesDssConstants.MES_SCMS_TABLEINDEX_FLAG_SCMS_UPDATED + ") order by  ENTITY.Sequence asc");
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void Complete(MesScmsTableIndex mesScmsTableIndex)
        {
            mesScmsTableIndex.Flag = MesDssConstants.MES_SCMS_TABLEINDEX_FLAG_SCMS_UPDATED;
            mesScmsTableIndex.LastModifyDate = DateTime.Now;
            this.UpdateMesScmsTableIndex(mesScmsTableIndex);
        }
    }
}
