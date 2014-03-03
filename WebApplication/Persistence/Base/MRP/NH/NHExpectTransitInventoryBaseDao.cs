using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.MRP;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.MRP.NH
{
    public class NHExpectTransitInventoryBaseDao : NHDaoBase, IExpectTransitInventoryBaseDao
    {
        public NHExpectTransitInventoryBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateExpectTransitInventory(ExpectTransitInventory entity)
        {
            Create(entity);
        }

        public virtual IList<ExpectTransitInventory> GetAllExpectTransitInventory()
        {
            return FindAll<ExpectTransitInventory>();
        }

        public virtual ExpectTransitInventory LoadExpectTransitInventory(Int32 id)
        {
            return FindById<ExpectTransitInventory>(id);
        }

        public virtual void UpdateExpectTransitInventory(ExpectTransitInventory entity)
        {
            Update(entity);
        }

        public virtual void DeleteExpectTransitInventory(Int32 id)
        {
            string hql = @"from ExpectTransitInventory entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteExpectTransitInventory(ExpectTransitInventory entity)
        {
            Delete(entity);
        }

        public virtual void DeleteExpectTransitInventory(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ExpectTransitInventory entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteExpectTransitInventory(IList<ExpectTransitInventory> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ExpectTransitInventory entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteExpectTransitInventory(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
