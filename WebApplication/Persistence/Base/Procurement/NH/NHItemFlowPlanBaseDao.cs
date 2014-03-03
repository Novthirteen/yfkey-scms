using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.Procurement;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Procurement.NH
{
    public class NHItemFlowPlanBaseDao : NHDaoBase, IItemFlowPlanBaseDao
    {
        public NHItemFlowPlanBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateItemFlowPlan(ItemFlowPlan entity)
        {
            Create(entity);
        }

        public virtual IList<ItemFlowPlan> GetAllItemFlowPlan()
        {
            return FindAll<ItemFlowPlan>();
        }

        public virtual ItemFlowPlan LoadItemFlowPlan(Int32 id)
        {
            return FindById<ItemFlowPlan>(id);
        }

        public virtual void UpdateItemFlowPlan(ItemFlowPlan entity)
        {
            Update(entity);
        }

        public virtual void DeleteItemFlowPlan(Int32 id)
        {
            string hql = @"from ItemFlowPlan entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteItemFlowPlan(ItemFlowPlan entity)
        {
            Delete(entity);
        }

        public virtual void DeleteItemFlowPlan(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ItemFlowPlan entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteItemFlowPlan(IList<ItemFlowPlan> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ItemFlowPlan entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteItemFlowPlan(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
