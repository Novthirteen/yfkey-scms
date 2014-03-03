using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Transportation;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Transportation.NH
{
    public class NHTransportationRouteBaseDao : NHDaoBase, ITransportationRouteBaseDao
    {
        public NHTransportationRouteBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateTransportationRoute(TransportationRoute entity)
        {
            Create(entity);
        }

        public virtual IList<TransportationRoute> GetAllTransportationRoute()
        {
            return GetAllTransportationRoute(false);
        }

        public virtual IList<TransportationRoute> GetAllTransportationRoute(bool includeInactive)
        {
            string hql = @"from TransportationRoute entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<TransportationRoute> result = FindAllWithCustomQuery<TransportationRoute>(hql);
            return result;
        }

        public virtual TransportationRoute LoadTransportationRoute(String code)
        {
            return FindById<TransportationRoute>(code);
        }

        public virtual void UpdateTransportationRoute(TransportationRoute entity)
        {
            Update(entity);
        }

        public virtual void DeleteTransportationRoute(String code)
        {
            string hql = @"from TransportationRoute entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteTransportationRoute(TransportationRoute entity)
        {
            Delete(entity);
        }

        public virtual void DeleteTransportationRoute(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from TransportationRoute entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteTransportationRoute(IList<TransportationRoute> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (TransportationRoute entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteTransportationRoute(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
