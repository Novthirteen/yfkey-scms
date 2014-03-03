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
    public class NHFlowTrackBaseDao : NHDaoBase, IFlowTrackBaseDao
    {
        public NHFlowTrackBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateFlowTrack(FlowTrack entity)
        {
            Create(entity);
        }

        public virtual IList<FlowTrack> GetAllFlowTrack()
        {
            return GetAllFlowTrack(false);
        }

        public virtual IList<FlowTrack> GetAllFlowTrack(bool includeInactive)
        {
            string hql = @"from FlowTrack entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<FlowTrack> result = FindAllWithCustomQuery<FlowTrack>(hql);
            return result;
        }

        public virtual FlowTrack LoadFlowTrack(Int32 id)
        {
            return FindById<FlowTrack>(id);
        }

        public virtual void UpdateFlowTrack(FlowTrack entity)
        {
            Update(entity);
        }

        public virtual void DeleteFlowTrack(Int32 id)
        {
            string hql = @"from FlowTrack entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteFlowTrack(FlowTrack entity)
        {
            Delete(entity);
        }

        public virtual void DeleteFlowTrack(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from FlowTrack entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteFlowTrack(IList<FlowTrack> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (FlowTrack entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteFlowTrack(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
