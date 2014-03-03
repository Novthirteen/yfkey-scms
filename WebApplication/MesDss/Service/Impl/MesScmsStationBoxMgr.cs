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
    public class MesScmsStationBoxMgr : IMesScmsStationBoxMgr
    {
        public IMesScmsStationBoxDao entityDao { get; set; }
        public IMesScmsTableIndexMgr mesScmsTableIndexMgr { get; set; }


        [Transaction(TransactionMode.Unspecified)]
        public virtual MesScmsStationBox LoadMesScmsStationBox(int id)
        {
            return entityDao.FindById<MesScmsStationBox>(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMesScmsStationBox(MesScmsStationBox entity)
        {
            entityDao.Update(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MesScmsStationBox> GetUpdateMesScmsStationBox()
        {
            return entityDao.FindAllWithCustomQuery<MesScmsStationBox>("FROM MesScmsStationBox ENTITY WHERE ENTITY.Flag IN (" + MesDssConstants.MES_SCMS_FLAG_FTPC_UPDATED + "," + MesDssConstants.MES_SCMS_FLAG_FTPC_DELETED + ") ORDER BY ENTITY.ScanTime ASC");
        }


        [Transaction(TransactionMode.Requires)]
        public void Complete(MesScmsStationBox mesScmsStationBox)
        {
            mesScmsStationBox.Flag = MesDssConstants.MES_SCMS_FLAG_SCMS_UPDATED;
            this.UpdateMesScmsStationBox(mesScmsStationBox);
        }

    }
}
