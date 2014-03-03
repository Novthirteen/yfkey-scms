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
    public class MesScmsStationShelfMgr : IMesScmsStationShelfMgr
    {
        public IMesScmsStationShelfDao entityDao { get; set; }
        public IMesScmsTableIndexMgr mesScmsTableIndexMgr { get; set; }


        [Transaction(TransactionMode.Unspecified)]
        public virtual MesScmsStationShelf LoadMesScmsStationShelf(string code)
        {
            return entityDao.FindById<MesScmsStationShelf>(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMesScmsStationShelf(MesScmsStationShelf entity)
        {
            entityDao.Update(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MesScmsStationShelf> GetUpdateMesScmsStationShelf()
        {
            return entityDao.FindAllWithCustomQuery<MesScmsStationShelf>("FROM MesScmsStationShelf ENTITY WHERE ENTITY.Flag IN (" + MesDssConstants.MES_SCMS_FLAG_FTPC_UPDATED + "," + MesDssConstants.MES_SCMS_FLAG_FTPC_DELETED + ")");
        }

        [Transaction(TransactionMode.Requires)]
        public void Complete(MesScmsStationShelf mesScmsStationShelf)
        {
            mesScmsStationShelf.Flag = MesDssConstants.MES_SCMS_FLAG_SCMS_UPDATED;
            this.UpdateMesScmsStationShelf(mesScmsStationShelf);
        }

    }
}
