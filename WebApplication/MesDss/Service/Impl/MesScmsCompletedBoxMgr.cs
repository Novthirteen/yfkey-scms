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
    public class MesScmsCompletedBoxMgr : IMesScmsCompletedBoxMgr
    {
        public IMesScmsCompletedBoxDao entityDao { get; set; }
        public IMesScmsTableIndexMgr mesScmsTableIndexMgr { get; set; }



        [Transaction(TransactionMode.Unspecified)]
        public virtual MesScmsCompletedBox LoadMesScmsCompletedBox(string code)
        {
            return entityDao.FindById<MesScmsCompletedBox>(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMesScmsCompletedBox(MesScmsCompletedBox entity)
        {
            entityDao.Update(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MesScmsCompletedBox> GetUpdateMesScmsCompletedBox()
        {
            return entityDao.FindAllWithCustomQuery<MesScmsCompletedBox>("FROM MesScmsCompletedBox ENTITY WHERE ENTITY.Flag IN (" + MesDssConstants.MES_SCMS_FLAG_FTPC_UPDATED + "," + MesDssConstants.MES_SCMS_FLAG_FTPC_DELETED + ")");
        }

        [Transaction(TransactionMode.Requires)]
        public void Complete(MesScmsCompletedBox mesScmsCompletedBox)
        {
            mesScmsCompletedBox.Flag = MesDssConstants.MES_SCMS_FLAG_SCMS_UPDATED;
            this.UpdateMesScmsCompletedBox(mesScmsCompletedBox);
        }

    }
}
