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
    public class NHWorkCenterBaseDao : NHDaoBase, IWorkCenterBaseDao
    {
        public NHWorkCenterBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateWorkCenter(WorkCenter entity)
        {
            Create(entity);
        }

        public virtual IList<WorkCenter> GetAllWorkCenter()
        {
            return GetAllWorkCenter(false);
        }

        public virtual IList<WorkCenter> GetAllWorkCenter(bool includeInactive)
        {
            string hql = @"from WorkCenter entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<WorkCenter> result = FindAllWithCustomQuery<WorkCenter>(hql);
            return result;
        }

        public virtual WorkCenter LoadWorkCenter(String code)
        {
            return FindById<WorkCenter>(code);
        }

        public virtual void UpdateWorkCenter(WorkCenter entity)
        {
            Update(entity);
        }

        public virtual void DeleteWorkCenter(String code)
        {
            string hql = @"from WorkCenter entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteWorkCenter(WorkCenter entity)
        {
            Delete(entity);
        }

        public virtual void DeleteWorkCenter(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from WorkCenter entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteWorkCenter(IList<WorkCenter> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (WorkCenter entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteWorkCenter(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
