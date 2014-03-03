using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Batch;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Batch.NH
{
    public class NHBatchRunLogBaseDao : NHDaoBase, IBatchRunLogBaseDao
    {
        public NHBatchRunLogBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBatchRunLog(BatchRunLog entity)
        {
            Create(entity);
        }

        public virtual IList<BatchRunLog> GetAllBatchRunLog()
        {
            return FindAll<BatchRunLog>();
        }

        public virtual BatchRunLog LoadBatchRunLog(Int32 id)
        {
            return FindById<BatchRunLog>(id);
        }

        public virtual void UpdateBatchRunLog(BatchRunLog entity)
        {
            Update(entity);
        }

        public virtual void DeleteBatchRunLog(Int32 id)
        {
            string hql = @"from BatchRunLog entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteBatchRunLog(BatchRunLog entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBatchRunLog(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from BatchRunLog entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBatchRunLog(IList<BatchRunLog> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (BatchRunLog entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteBatchRunLog(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
