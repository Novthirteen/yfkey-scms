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
    public class NHRoutingDetailBaseDao : NHDaoBase, IRoutingDetailBaseDao
    {
        public NHRoutingDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateRoutingDetail(RoutingDetail entity)
        {
            Create(entity);
        }

        public virtual IList<RoutingDetail> GetAllRoutingDetail()
        {
            return FindAll<RoutingDetail>();
        }

        public virtual RoutingDetail LoadRoutingDetail(Int32 id)
        {
            return FindById<RoutingDetail>(id);
        }

        public virtual void UpdateRoutingDetail(RoutingDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteRoutingDetail(Int32 id)
        {
            string hql = @"from RoutingDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteRoutingDetail(RoutingDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteRoutingDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from RoutingDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteRoutingDetail(IList<RoutingDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (RoutingDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteRoutingDetail(pkList);
        }


        public virtual RoutingDetail LoadRoutingDetail(com.Sconit.Entity.MasterData.Routing routing, Int32 operation, String reference)
        {
            string hql = @"from RoutingDetail entity where entity.Routing.Code = ? and entity.Operation = ? and entity.Reference = ?";
            IList<RoutingDetail> result = FindAllWithCustomQuery<RoutingDetail>(hql, new object[] { routing.Code, operation, reference }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void DeleteRoutingDetail(String routingCode, Int32 operation, String reference)
        {
            string hql = @"from RoutingDetail entity where entity.Routing.Code = ? and entity.Operation = ? and entity.Reference = ?";
            Delete(hql, new object[] { routingCode, operation, reference }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32, NHibernateUtil.String });
        }

        public virtual RoutingDetail LoadRoutingDetail(String routingCode, Int32 operation, String reference)
        {
            string hql = @"from RoutingDetail entity where entity.Routing.Code = ? and entity.Operation = ? and entity.Reference = ?";
            IList<RoutingDetail> result = FindAllWithCustomQuery<RoutingDetail>(hql, new object[] { routingCode, operation, reference }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        #endregion Method Created By CodeSmith
    }
}
