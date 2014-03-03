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
    public class NHRegionBaseDao : NHDaoBase, IRegionBaseDao
    {
        public NHRegionBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateRegion(Region entity)
        {
            Create(entity);
        }

        public virtual IList<Region> GetAllRegion()
        {
            return GetAllRegion(false);
        }

        public virtual IList<Region> GetAllRegion(bool includeInactive)
        {
           string hql = @" from Region entity ";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1 ";
            }
            IList<Region> result = FindAllWithCustomQuery<Region>(hql);
            return result;
        }

        public virtual Region LoadRegion(String code)
        {
            return FindById<Region>(code);
        }

        public virtual void UpdateRegion(Region entity)
        {
            Update(entity);
        }

        public virtual void DeleteRegion(String code)
        {
            string hql = @" from Region entity where entity.Code = ? ";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteRegion(Region entity)
        {
            Delete(entity);
        }

        public virtual void DeleteRegion(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append(" from Region entity where entity.Code in (" );
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteRegion(IList<Region> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Region entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteRegion(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
