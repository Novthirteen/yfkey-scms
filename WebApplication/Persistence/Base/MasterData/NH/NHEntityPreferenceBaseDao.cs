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
    public class NHEntityPreferenceBaseDao : NHDaoBase, IEntityPreferenceBaseDao
    {
        public NHEntityPreferenceBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateEntityPreference(EntityPreference entity)
        {
            Create(entity);
        }
		
		public virtual IList<EntityPreference> GetAllEntityPreference()
		{
			return FindAll<EntityPreference>();
		}
		
        public virtual EntityPreference LoadEntityPreference(String code)
        {
            return FindById<EntityPreference>(code);
        }

        public virtual void UpdateEntityPreference(EntityPreference entity)
        {
            Update(entity);
        }

        public virtual void DeleteEntityPreference(String code)
        {
            string hql = @"from EntityPreference entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteEntityPreference(EntityPreference entity)
        {
            Delete(entity);
        }
    
        public virtual void DeleteEntityPreference(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from EntityPreference entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteEntityPreference(IList<EntityPreference> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (EntityPreference entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteEntityPreference(pkList);
        }
    
    
        #endregion Method Created By CodeSmith
    }
}
