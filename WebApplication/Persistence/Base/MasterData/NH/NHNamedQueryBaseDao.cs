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
    public class NHNamedQueryBaseDao : NHDaoBase, INamedQueryBaseDao
    {
        public NHNamedQueryBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateNamedQuery(NamedQuery entity)
        {
            Create(entity);
        }

        public virtual IList<NamedQuery> GetAllNamedQuery()
        {
            return FindAll<NamedQuery>();
        }

        public virtual NamedQuery LoadNamedQuery(com.Sconit.Entity.MasterData.User user, String queryName)
        {
            string hql = @"from NamedQuery entity where entity.User.Code = ? and entity.QueryName = ?";
            IList<NamedQuery> result = FindAllWithCustomQuery<NamedQuery>(hql, new object[] { user.Code, queryName }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual NamedQuery LoadNamedQuery(String userCode, String queryName)
        {
            string hql = @"from NamedQuery entity where entity.User.Code = ? and entity.QueryName = ?";
            IList<NamedQuery> result = FindAllWithCustomQuery<NamedQuery>(hql, new object[] { userCode, queryName }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void UpdateNamedQuery(NamedQuery entity)
        {
            Update(entity);
        }

        public virtual void DeleteNamedQuery(com.Sconit.Entity.MasterData.User user, String queryName)
        {
            string hql = @"from NamedQuery entity where entity.User.Code = ? and entity.QueryName = ?";
            Delete(hql, new object[] { user.Code, queryName }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
        }

        public virtual void DeleteNamedQuery(String userCode, String queryName)
        {
            string hql = @"from NamedQuery entity where entity.User.Code = ? and entity.QueryName = ?";
            Delete(hql, new object[] { userCode, queryName }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
        }

        public virtual void DeleteNamedQuery(NamedQuery entity)
        {
            Delete(entity);
        }

        public virtual void DeleteNamedQuery(IList<NamedQuery> entityList)
        {
            foreach (NamedQuery entity in entityList)
            {
                DeleteNamedQuery(entity);
            }
        }


        #endregion Method Created By CodeSmith
    }
}
