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
    public class NHRolePermissionBaseDao : NHDaoBase, IRolePermissionBaseDao
    {
        public NHRolePermissionBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateRolePermission(RolePermission entity)
        {
            Create(entity);
        }

        public virtual IList<RolePermission> GetAllRolePermission()
        {
            return FindAll<RolePermission>();
        }

        public virtual RolePermission LoadRolePermission(Int32 id)
        {
            return FindById<RolePermission>(id);
        }

        public virtual void UpdateRolePermission(RolePermission entity)
        {
            Update(entity);
        }

        public virtual void DeleteRolePermission(Int32 id)
        {
            string hql = @"from RolePermission entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteRolePermission(RolePermission entity)
        {
            Delete(entity);
        }

        public virtual void DeleteRolePermission(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from RolePermission entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteRolePermission(IList<RolePermission> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (RolePermission entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteRolePermission(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
