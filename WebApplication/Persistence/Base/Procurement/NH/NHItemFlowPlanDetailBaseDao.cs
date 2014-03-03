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
    public class NHItemFlowPlanDetailBaseDao : NHDaoBase, IItemFlowPlanDetailBaseDao
    {
        public NHItemFlowPlanDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateItemFlowPlanDetail(ItemFlowPlanDetail entity)
        {
            Create(entity);
        }

        public virtual IList<ItemFlowPlanDetail> GetAllItemFlowPlanDetail()
        {
            return FindAll<ItemFlowPlanDetail>();
        }

        public virtual ItemFlowPlanDetail LoadItemFlowPlanDetail(Int32 id)
        {
            return FindById<ItemFlowPlanDetail>(id);
        }

        public virtual void UpdateItemFlowPlanDetail(ItemFlowPlanDetail entity)
        {
            Update(entity);
        }

        public virtual void DeleteItemFlowPlanDetail(Int32 id)
        {
            string hql = @"from ItemFlowPlanDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteItemFlowPlanDetail(ItemFlowPlanDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeleteItemFlowPlanDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ItemFlowPlanDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteItemFlowPlanDetail(IList<ItemFlowPlanDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ItemFlowPlanDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteItemFlowPlanDetail(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
