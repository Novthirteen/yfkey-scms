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
    public class NHRoutingBaseDao : NHDaoBase, IRoutingBaseDao
    {
        public NHRoutingBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateRouting(Routing entity)
        {
            Create(entity);
        }

        public virtual IList<Routing> GetAllRouting()
        {
            return GetAllRouting(false);
        }

        public virtual IList<Routing> GetAllRouting(bool includeInactive)
        {
            string hql = @"from Routing entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<Routing> result = FindAllWithCustomQuery<Routing>(hql);
            return result;
        }

        public virtual Routing LoadRouting(String code)
        {
            return FindById<Routing>(code);
        }

        public virtual void UpdateRouting(Routing entity)
        {
            Update(entity);
        }

        public virtual void DeleteRouting(String code)
        {
            string hql = @"from Routing entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteRouting(Routing entity)
        {
            Delete(entity);
        }

        public virtual void DeleteRouting(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Routing entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteRouting(IList<Routing> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Routing entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteRouting(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
