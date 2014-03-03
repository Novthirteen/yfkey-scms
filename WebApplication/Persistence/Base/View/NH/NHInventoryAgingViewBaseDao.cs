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
    public class NHInventoryAgingViewBaseDao : NHDaoBase, IInventoryAgingViewBaseDao
    {
        public NHInventoryAgingViewBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateInventoryAgingView(InventoryAgingView entity)
        {
            Create(entity);
        }

        public virtual IList<InventoryAgingView> GetAllInventoryAgingView()
        {
            return FindAll<InventoryAgingView>();
        }

        public virtual InventoryAgingView LoadInventoryAgingView(Int32 id)
        {
            return FindById<InventoryAgingView>(id);
        }

        public virtual void UpdateInventoryAgingView(InventoryAgingView entity)
        {
            Update(entity);
        }

        public virtual void DeleteInventoryAgingView(Int32 id)
        {
            string hql = @"from InventoryAgingView entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteInventoryAgingView(InventoryAgingView entity)
        {
            Delete(entity);
        }

        public virtual void DeleteInventoryAgingView(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from InventoryAgingView entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteInventoryAgingView(IList<InventoryAgingView> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (InventoryAgingView entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteInventoryAgingView(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
