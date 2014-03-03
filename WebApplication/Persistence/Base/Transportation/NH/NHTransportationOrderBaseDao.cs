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
    public class NHTransportationOrderBaseDao : NHDaoBase, ITransportationOrderBaseDao
    {
        public NHTransportationOrderBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateTransportationOrder(TransportationOrder entity)
        {
            Create(entity);
        }

        public virtual IList<TransportationOrder> GetAllTransportationOrder()
        {
            return FindAll<TransportationOrder>();
        }

        public virtual TransportationOrder LoadTransportationOrder(String orderNo)
        {
            return FindById<TransportationOrder>(orderNo);
        }

        public virtual void UpdateTransportationOrder(TransportationOrder entity)
        {
            Update(entity);
        }

        public virtual void DeleteTransportationOrder(String orderNo)
        {
            string hql = @"from TransportationOrder entity where entity.OrderNo = ?";
            Delete(hql, new object[] { orderNo }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteTransportationOrder(TransportationOrder entity)
        {
            Delete(entity);
        }

        public virtual void DeleteTransportationOrder(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from TransportationOrder entity where entity.OrderNo in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteTransportationOrder(IList<TransportationOrder> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (TransportationOrder entity in entityList)
            {
                pkList.Add(entity.OrderNo);
            }

            DeleteTransportationOrder(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
