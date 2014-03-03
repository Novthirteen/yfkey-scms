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
    public class NHTransportationRouteDetailBaseDao : NHDaoBase, ITransportationRouteDetailBaseDao
    {
        public NHTransportationRouteDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateTransportationRouteDetail(TransportationRouteDetail entity)
        {
            Create(entity);
        }

        public virtual IList<TransportationRouteDetail> GetAllTransportationRouteDetail()
        {
            return FindAll<TransportationRouteDetail>();
        }

        public virtual TransportationRouteDetail LoadTransportationRouteDetail(Int32 id)
        {
            return FindById<TransportationRouteDetail>(id);
        }

        public virtual void UpdateTransportationRouteDetail(TransportationRouteDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteTransportationRouteDetail(Int32 id)
        {
            string hql = @"from TransportationRouteDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteTransportationRouteDetail(TransportationRouteDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteTransportationRouteDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from TransportationRouteDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteTransportationRouteDetail(IList<TransportationRouteDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (TransportationRouteDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteTransportationRouteDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
