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
    public class NHBatchJobDetailBaseDao : NHDaoBase, IBatchJobDetailBaseDao
    {
        public NHBatchJobDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBatchJobDetail(BatchJobDetail entity)
        {
            Create(entity);
        }

        public virtual IList<BatchJobDetail> GetAllBatchJobDetail()
        {
            return FindAll<BatchJobDetail>();
        }

        public virtual BatchJobDetail LoadBatchJobDetail(Int32 id)
        {
            return FindById<BatchJobDetail>(id);
        }

        public virtual void UpdateBatchJobDetail(BatchJobDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteBatchJobDetail(Int32 id)
        {
            string hql = @"from BatchJobDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteBatchJobDetail(BatchJobDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBatchJobDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from BatchJobDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBatchJobDetail(IList<BatchJobDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (BatchJobDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteBatchJobDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
