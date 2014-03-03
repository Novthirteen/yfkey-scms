using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.View;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.View.NH
{
    public class NHBillAgingViewBaseDao : NHDaoBase, IBillAgingViewBaseDao
    {
        public NHBillAgingViewBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBillAgingView(BillAgingView entity)
        {
            Create(entity);
        }

        public virtual IList<BillAgingView> GetAllBillAgingView()
        {
            return FindAll<BillAgingView>();
        }

        public virtual BillAgingView LoadBillAgingView(Int32 id)
        {
            return FindById<BillAgingView>(id);
        }

        public virtual void UpdateBillAgingView(BillAgingView entity)
        {
            Update(entity);
        }

        public virtual void DeleteBillAgingView(Int32 id)
        {
            string hql = @"from BillAgingView entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteBillAgingView(BillAgingView entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBillAgingView(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from BillAgingView entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBillAgingView(IList<BillAgingView> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (BillAgingView entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteBillAgingView(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
