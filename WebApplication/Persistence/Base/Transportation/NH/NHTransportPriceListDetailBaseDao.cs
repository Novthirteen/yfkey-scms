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
    public class NHTransportPriceListDetailBaseDao : NHDaoBase, ITransportPriceListDetailBaseDao
    {
        public NHTransportPriceListDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateTransportPriceListDetail(TransportPriceListDetail entity)
        {
            Create(entity);
        }

        public virtual IList<TransportPriceListDetail> GetAllTransportPriceListDetail()
        {
            return FindAll<TransportPriceListDetail>();
        }

        public virtual TransportPriceListDetail LoadTransportPriceListDetail(Int32 id)
        {
            return FindById<TransportPriceListDetail>(id);
        }

        public virtual void UpdateTransportPriceListDetail(TransportPriceListDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteTransportPriceListDetail(Int32 id)
        {
            string hql = @"from TransportPriceListDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteTransportPriceListDetail(TransportPriceListDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteTransportPriceListDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from TransportPriceListDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteTransportPriceListDetail(IList<TransportPriceListDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (TransportPriceListDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteTransportPriceListDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
