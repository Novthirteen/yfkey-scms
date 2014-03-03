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
    public class NHBillTransactionBaseDao : NHDaoBase, IBillTransactionBaseDao
    {
        public NHBillTransactionBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBillTransaction(BillTransaction entity)
        {
            Create(entity);
        }

        public virtual IList<BillTransaction> GetAllBillTransaction()
        {
            return FindAll<BillTransaction>();
        }

        public virtual BillTransaction LoadBillTransaction(Int32 id)
        {
            return FindById<BillTransaction>(id);
        }

        public virtual void UpdateBillTransaction(BillTransaction entity)
        {
            Update(entity);
        }

        public virtual void DeleteBillTransaction(Int32 id)
        {
            string hql = @"from BillTransaction entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteBillTransaction(BillTransaction entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBillTransaction(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from BillTransaction entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBillTransaction(IList<BillTransaction> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (BillTransaction entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteBillTransaction(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
