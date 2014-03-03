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
    public class NHPermissionBaseDao : NHDaoBase, IPermissionBaseDao
    {
        public NHPermissionBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreatePermission(Permission entity)
        {
            Create(entity);
        }

        public virtual Permission LoadPermission(Int32 id)
        {
			return FindById<Permission>(id);
        }

        public virtual IList<Permission> GetAllPermission()
        {
            return FindAll<Permission>();
        }

        public virtual void UpdatePermission(Permission entity)
        {
            Update(entity);
        }

		public virtual void DeletePermission(Int32 id)
		{
			string hql = @"from Permission entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
		}

		public virtual void DeletePermission(Permission entity)
        {
            Delete(entity);
        }
		
		public virtual void DeletePermission(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Permission entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeletePermission(IList<Permission> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (Permission entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeletePermission(pkList);
        }
		
        //public virtual Permission LoadPermission(String code)
        //{
        //    return FindById(typeof(Permission), code) as Permission;
        //}
		
		
        #endregion Method Created By CodeSmith
    }
}
