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
    public class NHPermissionCategoryBaseDao : NHDaoBase, IPermissionCategoryBaseDao
    {
        public NHPermissionCategoryBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreatePermissionCategory(PermissionCategory entity)
        {
            Create(entity);
        }

        public virtual PermissionCategory LoadPermissionCategory(String code)
        {
			return FindById<PermissionCategory>(code);
        }

        public virtual void UpdatePermissionCategory(PermissionCategory entity)
        {
            Update(entity);
        }

		public virtual void DeletePermissionCategory(String code)
		{
			string hql = @"from PermissionCategory entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
		}

		public virtual void DeletePermissionCategory(PermissionCategory entity)
        {
            Delete(entity);
        }
		
		public virtual void DeletePermissionCategory(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from PermissionCategory entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeletePermissionCategory(IList<PermissionCategory> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (PermissionCategory entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeletePermissionCategory(pkList);
        }

        #endregion Method Created By CodeSmith
    }
}
