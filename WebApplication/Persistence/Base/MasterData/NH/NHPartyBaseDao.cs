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
    public class NHPartyBaseDao : NHDaoBase, IPartyBaseDao
    {
        public NHPartyBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateParty(Party entity)
        {
            Create(entity);
        }
		
		public virtual IList<Party> GetAllParty()
		{
			return GetAllParty(false);
		}
		
		public virtual IList<Party> GetAllParty(bool includeInactive)
		{
            string hql = @"from Party entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<Party> result = FindAllWithCustomQuery<Party>(hql);
			return result;
		}	
		
        public virtual Party LoadParty(String code)
        {
            return FindById<Party>(code);
        }

        public virtual void UpdateParty(Party entity)
        {
            Update(entity);
        }

        public virtual void DeleteParty(String code)
        {
            string hql = @"from Party entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteParty(Party entity)
        {
            Delete(entity);
        }
    
        public virtual void DeleteParty(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Party entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteParty(IList<Party> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Party entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteParty(pkList);
        }
    
    
        #endregion Method Created By CodeSmith
    }
}
