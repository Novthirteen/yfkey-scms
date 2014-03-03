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
    public class MesScmsShelfPartMgr : IMesScmsShelfPartMgr
    {
        public IMesScmsShelfPartDao entityDao { get; set; }
        public IMesScmsTableIndexMgr mesScmsTableIndexMgr { get; set; }



        [Transaction(TransactionMode.Unspecified)]
        public virtual MesScmsShelfPart LoadMesScmsShelfPart(string shelfCode, string itemCode)
        {
            string queryHql = "FROM MesScmsShelfPart ENTITY WHERE ENTITY.ShelfNo='" + shelfCode + "' and ENTITY.ItemCode='" + itemCode + "'";
            IList<MesScmsShelfPart> shelfPartList = entityDao.FindAllWithCustomQuery<MesScmsShelfPart>(queryHql);
            return shelfPartList != null && shelfPartList.Count > 0 ? shelfPartList[0] : null;
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMesScmsShelfPart(MesScmsShelfPart entity)
        {
            entityDao.Update(entity);

        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MesScmsShelfPart> GetUpdateMesScmsShelfPart()
        {
            return entityDao.FindAllWithCustomQuery<MesScmsShelfPart>("FROM MesScmsShelfPart ENTITY WHERE ENTITY.Flag IN (" + MesDssConstants.MES_SCMS_FLAG_FTPC_UPDATED + "," + MesDssConstants.MES_SCMS_FLAG_FTPC_DELETED + ")");
        }

        [Transaction(TransactionMode.Requires)]
        public void Complete(MesScmsShelfPart mesScmsShelfPart)
        {
            mesScmsShelfPart.Flag = MesDssConstants.MES_SCMS_FLAG_SCMS_UPDATED;
            this.UpdateMesScmsShelfPart(mesScmsShelfPart);
        }

    }
}
