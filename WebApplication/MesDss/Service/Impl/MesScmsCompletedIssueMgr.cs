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
using System.Text;
//TODO: Add other using statements here.

namespace com.Mes.Dss.Service.Impl
{
    [Transactional]
    public class MesScmsCompletedIssueMgr : IMesScmsCompletedIssueMgr
    {
        public IMesScmsCompletedIssueDao entityDao { get; set; }
        public IMesScmsTableIndexMgr mesScmsTableIndexMgr { get; set; }



        [Transaction(TransactionMode.Unspecified)]
        public virtual MesScmsCompletedIssue LoadMesScmsCompletedIssue(string code)
        {
            return entityDao.FindById<MesScmsCompletedIssue>(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMesScmsCompletedIssue(MesScmsCompletedIssue entity)
        {
            entityDao.Update(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wo">¹¤µ¥ WO_NUMBER</param>
        /// <param name="huid">box_number</param>
        /// <returns></returns>
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MesScmsCompletedIssue> GetUpdateMesScmsCompletedIssue(string wo, string huid)
        {
            StringBuilder sql = new StringBuilder();
             sql.Append("FROM MesScmsCompletedIssue ENTITY WHERE ENTITY.Flag IN (" + MesDssConstants.MES_SCMS_FLAG_FTPC_UPDATED + "," + MesDssConstants.MES_SCMS_FLAG_FTPC_DELETED + ") and ");
            sql.Append(" ENTITY.OrderNo='" + wo + "' and ENTITY.HuId='" + huid + "'");
            return entityDao.FindAllWithCustomQuery<MesScmsCompletedIssue>(sql.ToString());
        }

        [Transaction(TransactionMode.Requires)]
        public void Complete(MesScmsCompletedIssue mesScmsCompletedIssue)
        {
            mesScmsCompletedIssue.Flag = MesDssConstants.MES_SCMS_FLAG_SCMS_UPDATED;
            this.UpdateMesScmsCompletedIssue(mesScmsCompletedIssue);
        }

    }
}
