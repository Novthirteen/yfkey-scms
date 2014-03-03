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
    public class NHTransportationAddressBaseDao : NHDaoBase, ITransportationAddressBaseDao
    {
        public NHTransportationAddressBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateTransportationAddress(TransportationAddress entity)
        {
            Create(entity);
        }

        public virtual IList<TransportationAddress> GetAllTransportationAddress()
        {
            return FindAll<TransportationAddress>();
        }

        public virtual TransportationAddress LoadTransportationAddress(Int32 id)
        {
            return FindById<TransportationAddress>(id);
        }

        public virtual void UpdateTransportationAddress(TransportationAddress entity)
        {
            Update(entity);
        }

        public virtual void DeleteTransportationAddress(Int32 id)
        {
            string hql = @"from TransportationAddress entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteTransportationAddress(TransportationAddress entity)
        {
            Delete(entity);
        }

        public virtual void DeleteTransportationAddress(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from TransportationAddress entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteTransportationAddress(IList<TransportationAddress> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (TransportationAddress entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteTransportationAddress(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
