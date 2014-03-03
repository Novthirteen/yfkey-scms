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
    public class NHFlowBindingBaseDao : NHDaoBase, IFlowBindingBaseDao
    {
        public NHFlowBindingBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateFlowBinding(FlowBinding entity)
        {
            Create(entity);
        }

        public virtual IList<FlowBinding> GetAllFlowBinding()
        {
            return FindAll<FlowBinding>();
        }

        public virtual FlowBinding LoadFlowBinding(Int32 id)
        {
            return FindById<FlowBinding>(id);
        }

        public virtual void UpdateFlowBinding(FlowBinding entity)
        {
            Update(entity);
        }

        public virtual void DeleteFlowBinding(Int32 id)
        {
            string hql = @"from FlowBinding entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteFlowBinding(FlowBinding entity)
        {
            Delete(entity);
        }

        public virtual void DeleteFlowBinding(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from FlowBinding entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteFlowBinding(IList<FlowBinding> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (FlowBinding entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteFlowBinding(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
