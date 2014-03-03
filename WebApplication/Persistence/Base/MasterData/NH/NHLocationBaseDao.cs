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
    public class NHLocationBaseDao : NHDaoBase, ILocationBaseDao
    {
        public NHLocationBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateLocation(Location entity)
        {
            Create(entity);
        }

        public virtual IList<Location> GetAllLocation()
        {
            return GetAllLocation(false);
        }

        public virtual IList<Location> GetAllLocation(bool includeInactive)
        {
            string hql = @"from Location entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<Location> result = FindAllWithCustomQuery<Location>(hql);
            return result;
        }

        public virtual Location LoadLocation(String code)
        {
            return FindById<Location>(code);
        }

        public virtual void UpdateLocation(Location entity)
        {
            Update(entity);
        }

        public virtual void DeleteLocation(String code)
        {
            string hql = @"from Location entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteLocation(Location entity)
        {
            Delete(entity);
        }

        public virtual void DeleteLocation(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Location entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteLocation(IList<Location> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Location entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteLocation(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
