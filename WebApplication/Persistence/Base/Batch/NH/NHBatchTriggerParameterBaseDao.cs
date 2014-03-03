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
    public class NHBatchTriggerParameterBaseDao : NHDaoBase, IBatchTriggerParameterBaseDao
    {
        public NHBatchTriggerParameterBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBatchTriggerParameter(BatchTriggerParameter entity)
        {
            Create(entity);
        }

        public virtual IList<BatchTriggerParameter> GetAllBatchTriggerParameter()
        {
            return FindAll<BatchTriggerParameter>();
        }

        public virtual BatchTriggerParameter LoadBatchTriggerParameter(Int32 id)
        {
            return FindById<BatchTriggerParameter>(id);
        }

        public virtual void UpdateBatchTriggerParameter(BatchTriggerParameter entity)
        {
            Update(entity);
        }

        public virtual void DeleteBatchTriggerParameter(Int32 id)
        {
            string hql = @"from BatchTriggerParameter entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteBatchTriggerParameter(BatchTriggerParameter entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBatchTriggerParameter(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from BatchTriggerParameter entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBatchTriggerParameter(IList<BatchTriggerParameter> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (BatchTriggerParameter entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteBatchTriggerParameter(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
