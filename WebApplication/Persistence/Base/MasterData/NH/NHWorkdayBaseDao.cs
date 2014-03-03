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
    public class NHWorkdayBaseDao : NHDaoBase, IWorkdayBaseDao
    {
        public NHWorkdayBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateWorkday(Workday entity)
        {
            Create(entity);
        }

        public virtual IList<Workday> GetAllWorkday()
        {
            return FindAll<Workday>();
        }

        public virtual Workday LoadWorkday(Int32 id)
        {
            return FindById<Workday>(id);
        }

        public virtual void UpdateWorkday(Workday entity)
        {
            Update(entity);
        }

        public virtual void DeleteWorkday(Int32 id)
        {
            string hql = @"from Workday entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteWorkday(Workday entity)
        {
            Delete(entity);
        }

        public virtual void DeleteWorkday(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Workday entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteWorkday(IList<Workday> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (Workday entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteWorkday(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
