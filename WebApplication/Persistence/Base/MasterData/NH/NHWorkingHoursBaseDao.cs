using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.MasterData.NH
{
    public class NHWorkingHoursBaseDao : NHDaoBase, IWorkingHoursBaseDao
    {
        public NHWorkingHoursBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateWorkingHours(WorkingHours entity)
        {
            Create(entity);
        }

        public virtual IList<WorkingHours> GetAllWorkingHours()
        {
            return FindAll<WorkingHours>();
        }

        public virtual WorkingHours LoadWorkingHours(Int32 id)
        {
            return FindById<WorkingHours>(id);
        }

        public virtual void UpdateWorkingHours(WorkingHours entity)
        {
            Update(entity);
        }

        public virtual void DeleteWorkingHours(Int32 id)
        {
            string hql = @"from WorkingHours entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteWorkingHours(WorkingHours entity)
        {
            Delete(entity);
        }

        public virtual void DeleteWorkingHours(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from WorkingHours entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteWorkingHours(IList<WorkingHours> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (WorkingHours entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteWorkingHours(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
