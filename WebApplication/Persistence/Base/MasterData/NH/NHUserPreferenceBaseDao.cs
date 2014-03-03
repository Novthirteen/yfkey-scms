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
    public class NHUserPreferenceBaseDao : NHDaoBase, IUserPreferenceBaseDao
    {
        public NHUserPreferenceBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateUserPreference(UserPreference entity)
        {
            Create(entity);
        }
		
		public virtual IList<UserPreference> GetAllUserPreference()
		{
			return FindAll<UserPreference>();
		}
		
        public virtual UserPreference LoadUserPreference(com.Sconit.Entity.MasterData.User user, String code)
        {
            string hql = @" from UserPreference entity where entity.User.Code = ? and entity.Code = ?";
            IList<UserPreference> result = FindAllWithCustomQuery<UserPreference>(hql, new object[] { user.Code, code }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }
    
        public virtual UserPreference LoadUserPreference(String userCode, String code)
        {
            string hql = @" from UserPreference entity where entity.User.Code = ? and entity.Code = ?";
            IList<UserPreference> result = FindAllWithCustomQuery<UserPreference>(hql, new object[] { userCode, code }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
      if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void UpdateUserPreference(UserPreference entity)
        {
            Update(entity);
        }

        public virtual void DeleteUserPreference(com.Sconit.Entity.MasterData.User user, String code)
        {
            string hql = @" from UserPreference entity where entity.User.Code = ? and entity.Code = ?";
            Delete(hql, new object[] { user.Code, code }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
        }
    
        public virtual void DeleteUserPreference(String userCode, String code)
        {
            string hql = @" from UserPreference entity where entity.User.Code = ? and entity.Code = ?";
            Delete(hql, new object[] { userCode, code }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
        }

        public virtual void DeleteUserPreference(UserPreference entity)
        {
            Delete(entity);
        }

        public virtual void DeleteUserPreference(IList<UserPreference> entityList)
        {
            foreach (UserPreference entity in entityList)
            {
                DeleteUserPreference(entity);
            }
        }
    
    
        #endregion Method Created By CodeSmith
    }
}
