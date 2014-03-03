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
    public class MesScmsBomMgr : IMesScmsBomMgr
    {
        public IMesScmsBomDao entityDao { get; set; }
        public IMesScmsTableIndexMgr mesScmsTableIndexMgr { get; set; }


        [Transaction(TransactionMode.Unspecified)]
        public virtual MesScmsBom LoadMesScmsBom(string bom, string itemCode, string tagNo, string prodLine)
        {
            string queryHql = "FROM MesScmsBom ENTITY WHERE ENTITY.Bom= '" + bom + "' and ENTITY.ItemCode='" + itemCode
                                + "' and ENTITY.TagNo='" + tagNo + "' and ENTITY.ProductLine='" + prodLine + "'";
            IList<MesScmsBom> bomList = entityDao.FindAllWithCustomQuery<MesScmsBom>(queryHql);
            return bomList != null && bomList.Count > 0 ? bomList[0] : null;
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMesScmsBom(MesScmsBom entity)
        {
            entityDao.Update(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MesScmsBom> GetUpdateMesScmsBom()
        {
            return entityDao.FindAllWithCustomQuery<MesScmsBom>("FROM MesScmsBom ENTITY WHERE ENTITY.Flag IN (" + MesDssConstants.MES_SCMS_FLAG_FTPC_UPDATED + "," + MesDssConstants.MES_SCMS_FLAG_FTPC_DELETED + ") ");
        }


        [Transaction(TransactionMode.Requires)]
        public void Complete(MesScmsBom mesScmsStationBox)
        {
            mesScmsStationBox.Flag = MesDssConstants.MES_SCMS_FLAG_SCMS_UPDATED;
            this.UpdateMesScmsBom(mesScmsStationBox);
        }

    }
}
