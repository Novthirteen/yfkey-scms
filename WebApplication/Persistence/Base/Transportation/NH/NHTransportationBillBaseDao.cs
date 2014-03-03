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
    public class NHTransportationBillBaseDao : NHDaoBase, ITransportationBillBaseDao
    {
        public NHTransportationBillBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateTransportationBill(TransportationBill entity)
        {
            Create(entity);
        }

        public virtual IList<TransportationBill> GetAllTransportationBill()
        {
            return FindAll<TransportationBill>();
        }

        public virtual TransportationBill LoadTransportationBill(String billNo)
        {
            return FindById<TransportationBill>(billNo);
        }

        public virtual void UpdateTransportationBill(TransportationBill entity)
        {
            Update(entity);
        }

        public virtual void DeleteTransportationBill(String billNo)
        {
            string hql = @"from TransportationBill entity where entity.BillNo = ?";
            Delete(hql, new object[] { billNo }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteTransportationBill(TransportationBill entity)
        {
            Delete(entity);
        }

        public virtual void DeleteTransportationBill(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from TransportationBill entity where entity.BillNo in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteTransportationBill(IList<TransportationBill> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (TransportationBill entity in entityList)
            {
                pkList.Add(entity.BillNo);
            }

            DeleteTransportationBill(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
