using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.MRP;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.MRP.NH
{
    public class NHMrpReceivePlanBaseDao : NHDaoBase, IMrpReceivePlanBaseDao
    {
        public NHMrpReceivePlanBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateMrpReceivePlan(MrpReceivePlan entity)
        {
            Create(entity);
        }

        public virtual IList<MrpReceivePlan> GetAllMrpReceivePlan()
        {
            return FindAll<MrpReceivePlan>();
        }

        public virtual MrpReceivePlan LoadMrpReceivePlan(Int32 id)
        {
            return FindById<MrpReceivePlan>(id);
        }

        public virtual void UpdateMrpReceivePlan(MrpReceivePlan entity)
        {
            Update(entity);
        }

        public virtual void DeleteMrpReceivePlan(Int32 id)
        {
            string hql = @"from MrpReceivePlan entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteMrpReceivePlan(MrpReceivePlan entity)
        {
            Delete(entity);
        }

        public virtual void DeleteMrpReceivePlan(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from MrpReceivePlan entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteMrpReceivePlan(IList<MrpReceivePlan> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (MrpReceivePlan entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteMrpReceivePlan(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
