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
    public class NHTransportationOrderDetailBaseDao : NHDaoBase, ITransportationOrderDetailBaseDao
    {
        public NHTransportationOrderDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateTransportationOrderDetail(TransportationOrderDetail entity)
        {
            Create(entity);
        }

        public virtual IList<TransportationOrderDetail> GetAllTransportationOrderDetail()
        {
            return FindAll<TransportationOrderDetail>();
        }

        public virtual TransportationOrderDetail LoadTransportationOrderDetail(Int32 id)
        {
            return FindById<TransportationOrderDetail>(id);
        }

        public virtual void UpdateTransportationOrderDetail(TransportationOrderDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteTransportationOrderDetail(Int32 id)
        {
            string hql = @"from TransportationOrderDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteTransportationOrderDetail(TransportationOrderDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteTransportationOrderDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from TransportationOrderDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteTransportationOrderDetail(IList<TransportationOrderDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (TransportationOrderDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteTransportationOrderDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
