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
    public class NHBatchJobParameterBaseDao : NHDaoBase, IBatchJobParameterBaseDao
    {
        public NHBatchJobParameterBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBatchJobParameter(BatchJobParameter entity)
        {
            Create(entity);
        }

        public virtual IList<BatchJobParameter> GetAllBatchJobParameter()
        {
            return FindAll<BatchJobParameter>();
        }

        public virtual BatchJobParameter LoadBatchJobParameter(Int32 id)
        {
            return FindById<BatchJobParameter>(id);
        }

        public virtual void UpdateBatchJobParameter(BatchJobParameter entity)
        {
            Update(entity);
        }

        public virtual void DeleteBatchJobParameter(Int32 id)
        {
            string hql = @"from BatchJobParameter entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteBatchJobParameter(BatchJobParameter entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBatchJobParameter(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from BatchJobParameter entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBatchJobParameter(IList<BatchJobParameter> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (BatchJobParameter entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteBatchJobParameter(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
