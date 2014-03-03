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
    public class NHItemFlowPlanTrackBaseDao : NHDaoBase, IItemFlowPlanTrackBaseDao
    {
        public NHItemFlowPlanTrackBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateItemFlowPlanTrack(ItemFlowPlanTrack entity)
        {
            Create(entity);
        }

        public virtual IList<ItemFlowPlanTrack> GetAllItemFlowPlanTrack()
        {
            return FindAll<ItemFlowPlanTrack>();
        }

        public virtual ItemFlowPlanTrack LoadItemFlowPlanTrack(Int32 id)
        {
            return FindById<ItemFlowPlanTrack>(id);
        }

        public virtual void UpdateItemFlowPlanTrack(ItemFlowPlanTrack entity)
        {
            Update(entity);
        }

        public virtual void DeleteItemFlowPlanTrack(Int32 id)
        {
            string hql = @"from ItemFlowPlanTrack entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteItemFlowPlanTrack(ItemFlowPlanTrack entity)
        {
            Delete(entity);
        }

        public virtual void DeleteItemFlowPlanTrack(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ItemFlowPlanTrack entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteItemFlowPlanTrack(IList<ItemFlowPlanTrack> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (ItemFlowPlanTrack entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteItemFlowPlanTrack(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
