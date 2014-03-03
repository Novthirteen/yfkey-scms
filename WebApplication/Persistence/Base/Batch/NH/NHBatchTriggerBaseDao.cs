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
    public class NHBatchTriggerBaseDao : NHDaoBase, IBatchTriggerBaseDao
    {
        public NHBatchTriggerBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBatchTrigger(BatchTrigger entity)
        {
            Create(entity);
        }

        public virtual IList<BatchTrigger> GetAllBatchTrigger()
        {
            return FindAll<BatchTrigger>();
        }

        public virtual BatchTrigger LoadBatchTrigger(Int32 id)
        {
            return FindById<BatchTrigger>(id);
        }

        public virtual void UpdateBatchTrigger(BatchTrigger entity)
        {
            Update(entity);
        }

        public virtual void DeleteBatchTrigger(Int32 id)
        {
            string hql = @"from BatchTrigger entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteBatchTrigger(BatchTrigger entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBatchTrigger(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from BatchTrigger entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBatchTrigger(IList<BatchTrigger> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (BatchTrigger entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteBatchTrigger(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
