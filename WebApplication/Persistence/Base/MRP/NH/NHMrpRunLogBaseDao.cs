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
    public class NHMrpRunLogBaseDao : NHDaoBase, IMrpRunLogBaseDao
    {
        public NHMrpRunLogBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateMrpRunLog(MrpRunLog entity)
        {
            Create(entity);
        }

        public virtual IList<MrpRunLog> GetAllMrpRunLog()
        {
            return FindAll<MrpRunLog>();
        }

        public virtual MrpRunLog LoadMrpRunLog(Int32 id)
        {
            return FindById<MrpRunLog>(id);
        }

        public virtual void UpdateMrpRunLog(MrpRunLog entity)
        {
            Update(entity);
        }

        public virtual void DeleteMrpRunLog(Int32 id)
        {
            string hql = @"from MrpRunLog entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteMrpRunLog(MrpRunLog entity)
        {
            Delete(entity);
        }

        public virtual void DeleteMrpRunLog(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from MrpRunLog entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteMrpRunLog(IList<MrpRunLog> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (MrpRunLog entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteMrpRunLog(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
