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
    public class NHUserRoleBaseDao : NHDaoBase, IUserRoleBaseDao
    {
        public NHUserRoleBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateUserRole(UserRole entity)
        {
            Create(entity);
        }

        public virtual IList<UserRole> GetAllUserRole()
        {
            return FindAll<UserRole>();
        }

        public virtual UserRole LoadUserRole(Int32 id)
        {
            return FindById<UserRole>(id);
        }

        public virtual void UpdateUserRole(UserRole entity)
        {
            Update(entity);
        }

        public virtual void DeleteUserRole(Int32 id)
        {
            string hql = @"from UserRole entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteUserRole(UserRole entity)
        {
            Delete(entity);
        }

        public virtual void DeleteUserRole(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from UserRole entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteUserRole(IList<UserRole> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (UserRole entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteUserRole(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
