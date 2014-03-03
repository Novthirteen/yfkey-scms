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
    public class NHUserPermissionBaseDao : NHDaoBase, IUserPermissionBaseDao
    {
        public NHUserPermissionBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateUserPermission(UserPermission entity)
        {
            Create(entity);
        }
		
		public virtual IList<UserPermission> GetAllUserPermission()
		{
			return FindAll<UserPermission>();
		}
		
        public virtual UserPermission LoadUserPermission(Int32 id)
        {
            return FindById<UserPermission>(id);
        }

        public virtual void UpdateUserPermission(UserPermission entity)
        {
            Update(entity);
        }

        public virtual void DeleteUserPermission(Int32 id)
        {
            string hql = @"from UserPermission entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteUserPermission(UserPermission entity)
        {
            Delete(entity);
        }
    
        public virtual void DeleteUserPermission(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from UserPermission entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteUserPermission(IList<UserPermission> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (UserPermission entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteUserPermission(pkList);
        }
    
    
        #endregion Method Created By CodeSmith
    }
}
