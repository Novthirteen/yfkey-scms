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
    public class NHTransportPriceListBaseDao : NHDaoBase, ITransportPriceListBaseDao
    {
        public NHTransportPriceListBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateTransportPriceList(TransportPriceList entity)
        {
            Create(entity);
        }

        public virtual IList<TransportPriceList> GetAllTransportPriceList()
        {
            return GetAllTransportPriceList(false);
        }

        public virtual IList<TransportPriceList> GetAllTransportPriceList(bool includeInactive)
        {
            string hql = @"from TransportPriceList entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<TransportPriceList> result = FindAllWithCustomQuery<TransportPriceList>(hql);
            return result;
        }

        public virtual TransportPriceList LoadTransportPriceList(String code)
        {
            return FindById<TransportPriceList>(code);
        }

        public virtual void UpdateTransportPriceList(TransportPriceList entity)
        {
            Update(entity);
        }

        public virtual void DeleteTransportPriceList(String code)
        {
            string hql = @"from TransportPriceList entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteTransportPriceList(TransportPriceList entity)
        {
            Delete(entity);
        }

        public virtual void DeleteTransportPriceList(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from TransportPriceList entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteTransportPriceList(IList<TransportPriceList> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (TransportPriceList entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteTransportPriceList(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
