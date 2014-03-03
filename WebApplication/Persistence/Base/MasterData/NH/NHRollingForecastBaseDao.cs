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
    public class NHRollingForecastBaseDao : NHDaoBase, IRollingForecastBaseDao
    {
        public NHRollingForecastBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateRollingForecast(RollingForecast entity)
        {
            Create(entity);
        }

        public virtual IList<RollingForecast> GetAllRollingForecast()
        {
            return FindAll<RollingForecast>();
        }

        public virtual RollingForecast LoadRollingForecast(Int32 id)
        {
            return FindById<RollingForecast>(id);
        }

        public virtual void UpdateRollingForecast(RollingForecast entity)
        {
            Update(entity);
        }

        public virtual void DeleteRollingForecast(Int32 id)
        {
            string hql = @"from RollingForecast entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteRollingForecast(RollingForecast entity)
        {
            Delete(entity);
        }

        public virtual void DeleteRollingForecast(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from RollingForecast entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteRollingForecast(IList<RollingForecast> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (RollingForecast entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteRollingForecast(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
