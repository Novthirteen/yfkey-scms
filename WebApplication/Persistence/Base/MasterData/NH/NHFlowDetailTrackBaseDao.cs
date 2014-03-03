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
    public class NHFlowDetailTrackBaseDao : NHDaoBase, IFlowDetailTrackBaseDao
    {
        public NHFlowDetailTrackBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateFlowDetailTrack(FlowDetailTrack entity)
        {
            Create(entity);
        }

        public virtual IList<FlowDetailTrack> GetAllFlowDetailTrack()
        {
            return FindAll<FlowDetailTrack>();
        }

        public virtual FlowDetailTrack LoadFlowDetailTrack(Int32 id)
        {
            return FindById<FlowDetailTrack>(id);
        }

        public virtual void UpdateFlowDetailTrack(FlowDetailTrack entity)
        {
            Update(entity);
        }

        public virtual void DeleteFlowDetailTrack(Int32 id)
        {
            string hql = @"from FlowDetailTrack entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteFlowDetailTrack(FlowDetailTrack entity)
        {
            Delete(entity);
        }

        public virtual void DeleteFlowDetailTrack(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from FlowDetailTrack entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteFlowDetailTrack(IList<FlowDetailTrack> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (FlowDetailTrack entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteFlowDetailTrack(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
