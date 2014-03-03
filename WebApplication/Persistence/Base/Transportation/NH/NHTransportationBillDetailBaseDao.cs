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
    public class NHTransportationBillDetailBaseDao : NHDaoBase, ITransportationBillDetailBaseDao
    {
        public NHTransportationBillDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateTransportationBillDetail(TransportationBillDetail entity)
        {
            Create(entity);
        }

        public virtual IList<TransportationBillDetail> GetAllTransportationBillDetail()
        {
            return FindAll<TransportationBillDetail>();
        }

        public virtual TransportationBillDetail LoadTransportationBillDetail(Int32 id)
        {
            return FindById<TransportationBillDetail>(id);
        }

        public virtual void UpdateTransportationBillDetail(TransportationBillDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteTransportationBillDetail(Int32 id)
        {
            string hql = @"from TransportationBillDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteTransportationBillDetail(TransportationBillDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteTransportationBillDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from TransportationBillDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteTransportationBillDetail(IList<TransportationBillDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (TransportationBillDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteTransportationBillDetail(pkList);
        }


        public virtual TransportationBillDetail LoadTransportationBillDetail(String bill, Int32 tActingBill)
        {
            string hql = @"from TransportationBillDetail entity where entity.Bill = ? and entity.TActingBill = ?";
            IList<TransportationBillDetail> result = FindAllWithCustomQuery<TransportationBillDetail>(hql, new object[] { bill, tActingBill }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32 });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void DeleteTransportationBillDetail(String bill, Int32 tActingBill)
        {
            string hql = @"from TransportationBillDetail entity where entity.Bill = ? and entity.TActingBill = ?";
            Delete(hql, new object[] { bill, tActingBill }, new IType[] { NHibernateUtil.String, NHibernateUtil.Int32 });
        }

        #endregion Method Created By CodeSmith
    }
}
