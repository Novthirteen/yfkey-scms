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
    public class NHFavoritesBaseDao : NHDaoBase, IFavoritesBaseDao
    {
        public NHFavoritesBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateFavorites(Favorites entity)
        {
            Create(entity);
        }
		
		public virtual IList<Favorites> GetAllFavorites()
		{
			return FindAll<Favorites>();
		}
		
        public virtual Favorites LoadFavorites(Int32 id)
        {
            return FindById<Favorites>(id);
        }

        public virtual void UpdateFavorites(Favorites entity)
        {
            Update(entity);
        }

        public virtual void DeleteFavorites(Int32 id)
        {
            string hql = @"from Favorites entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteFavorites(Favorites entity)
        {
            Delete(entity);
        }
    
        public virtual void DeleteFavorites(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Favorites entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteFavorites(IList<Favorites> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (Favorites entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteFavorites(pkList);
        }
    
        
        public virtual Favorites LoadFavorites(com.Sconit.Entity.MasterData.User user, String type, String pageName)
        {
            string hql = @"from Favorites entity where entity.User.Code = ? and entity.Type = ? and entity.PageName = ?";
            IList<Favorites> result = FindAllWithCustomQuery<Favorites>(hql, new object[] { user.Code, type, pageName }, new IType[] { NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }
        
        public virtual void DeleteFavorites(String userCode, String type, String pageName)
        {
            string hql = @"from Favorites entity where entity.User.Code = ? and entity.Type = ? and entity.PageName = ?";
            Delete(hql, new object[] { userCode, type, pageName }, new IType[] { NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.String });
        }   
    
        public virtual Favorites LoadFavorites(String userCode, String type, String pageName)
        {
            string hql = @"from Favorites entity where entity.User.Code = ? and entity.Type = ? and entity.PageName = ?";
            IList<Favorites> result = FindAllWithCustomQuery<Favorites>(hql, new object[] { userCode, type, pageName }, new IType[] { NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }
    
        #endregion Method Created By CodeSmith
    }
}
