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
    public class NHFlowBaseDao : NHDaoBase, IFlowBaseDao
    {
        public NHFlowBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateFlow(Flow entity)
        {
            Create(entity);
        }

        public virtual IList<Flow> GetAllFlow()
        {
            return GetAllFlow(false);
        }

        public virtual IList<Flow> GetAllFlow(bool includeInactive)
        {
            string hql = @"from Flow entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<Flow> result = FindAllWithCustomQuery<Flow>(hql);
            return result;
        }

        public virtual Flow LoadFlow(String code)
        {
            return FindById<Flow>(code);
        }

        public virtual void UpdateFlow(Flow entity)
        {
            Update(entity);
        }

        public virtual void DeleteFlow(String code)
        {
            string hql = @"from Flow entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteFlow(Flow entity)
        {
            Delete(entity);
        }

        public virtual void DeleteFlow(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Flow entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteFlow(IList<Flow> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Flow entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteFlow(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
