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
    public class NHFlowViewBaseDao : NHDaoBase, IFlowViewBaseDao
    {
        public NHFlowViewBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateFlowView(FlowView entity)
        {
            Create(entity);
        }

        public virtual IList<FlowView> GetAllFlowView()
        {
            return FindAll<FlowView>();
        }

        public virtual FlowView LoadFlowView(Int32 id)
        {
            return FindById<FlowView>(id);
        }

        public virtual void UpdateFlowView(FlowView entity)
        {
            Update(entity);
        }

        public virtual void DeleteFlowView(Int32 id)
        {
            string hql = @"from FlowView entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteFlowView(FlowView entity)
        {
            Delete(entity);
        }

        public virtual void DeleteFlowView(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from FlowView entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteFlowView(IList<FlowView> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (FlowView entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteFlowView(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
