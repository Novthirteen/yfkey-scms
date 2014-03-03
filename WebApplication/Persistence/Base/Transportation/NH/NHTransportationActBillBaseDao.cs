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
    public class NHTransportationActBillBaseDao : NHDaoBase, ITransportationActBillBaseDao
    {
        public NHTransportationActBillBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateTransportationActBill(TransportationActBill entity)
        {
            Create(entity);
        }

        public virtual IList<TransportationActBill> GetAllTransportationActBill()
        {
            return FindAll<TransportationActBill>();
        }

        public virtual TransportationActBill LoadTransportationActBill(Int32 id)
        {
            return FindById<TransportationActBill>(id);
        }

        public virtual void UpdateTransportationActBill(TransportationActBill entity)
        {
            Update(entity);
        }

        public virtual void DeleteTransportationActBill(Int32 id)
        {
            string hql = @"from TransportationActBill entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteTransportationActBill(TransportationActBill entity)
        {
            Delete(entity);
        }

        public virtual void DeleteTransportationActBill(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from TransportationActBill entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteTransportationActBill(IList<TransportationActBill> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (TransportationActBill entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteTransportationActBill(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
