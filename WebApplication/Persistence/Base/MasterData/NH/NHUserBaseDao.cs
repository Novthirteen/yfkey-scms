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
    public class NHUserBaseDao : NHDaoBase, IUserBaseDao
    {
        public NHUserBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateUser(User entity)
        {
            Create(entity);
        }

        public virtual IList<User> GetAllUser()
        {
            return GetAllUser(false);
        }

        public virtual IList<User> GetAllUser(bool includeInactive)
        {
            string hql = @" from User entity ";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1 ";
            }
            IList<User> result = FindAllWithCustomQuery<User>(hql);
            return result;
        }

        public virtual User LoadUser(string code)
        {
            return FindById<User>(code);
        }

        public virtual void UpdateUser(User entity)
        {
            Update(entity);
        }

        public virtual void DeleteUser(string code)
		{
            string hql = @"from User entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
		}

		public virtual void DeleteUser(User entity)
        {
            Delete(entity);
        }

        public virtual void DeleteUser(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from User entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteUser(IList<User> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (User entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteUser(pkList);
        }

        #endregion Method Created By CodeSmith
    }
}
