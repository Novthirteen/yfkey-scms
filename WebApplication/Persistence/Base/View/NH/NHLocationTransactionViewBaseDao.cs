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
    public class NHLocationTransactionViewBaseDao : NHDaoBase, ILocationTransactionViewBaseDao
    {
        public NHLocationTransactionViewBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateLocationTransactionView(LocationTransactionView entity)
        {
            Create(entity);
        }

        public virtual IList<LocationTransactionView> GetAllLocationTransactionView()
        {
            return FindAll<LocationTransactionView>();
        }

        public virtual LocationTransactionView LoadLocationTransactionView(Int32 id)
        {
            return FindById<LocationTransactionView>(id);
        }

        public virtual void UpdateLocationTransactionView(LocationTransactionView entity)
        {
            Update(entity);
        }

        public virtual void DeleteLocationTransactionView(Int32 id)
        {
            string hql = @"from LocationTransactionView entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteLocationTransactionView(LocationTransactionView entity)
        {
            Delete(entity);
        }

        public virtual void DeleteLocationTransactionView(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from LocationTransactionView entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteLocationTransactionView(IList<LocationTransactionView> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (LocationTransactionView entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteLocationTransactionView(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
