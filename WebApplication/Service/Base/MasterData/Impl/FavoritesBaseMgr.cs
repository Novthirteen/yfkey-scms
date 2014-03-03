using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class FavoritesBaseMgr : SessionBase, IFavoritesBaseMgr
    {
        protected IFavoritesDao entityDao;
        
        public FavoritesBaseMgr(IFavoritesDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateFavorites(Favorites entity)
        {
            entityDao.CreateFavorites(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Favorites LoadFavorites(Int32 id)
        {
            return entityDao.LoadFavorites(id);
        }

		[Transaction(TransactionMode.Requires)]
        public virtual IList<Favorites> GetAllFavorites()
        {
            return entityDao.GetAllFavorites();
        }
		
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateFavorites(Favorites entity)
        {
            entityDao.UpdateFavorites(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFavorites(Int32 id)
        {
            entityDao.DeleteFavorites(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFavorites(Favorites entity)
        {
            entityDao.DeleteFavorites(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFavorites(IList<Int32> pkList)
        {
            entityDao.DeleteFavorites(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFavorites(IList<Favorites> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteFavorites(entityList);
        }   
        
        [Transaction(TransactionMode.Unspecified)]
        public virtual Favorites LoadFavorites(com.Sconit.Entity.MasterData.User user, String type, String pageName)
        {
            return entityDao.LoadFavorites(user, type, pageName);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFavorites(String userCode, String type, String pageName)
        {
            entityDao.DeleteFavorites(userCode, type, pageName);
        }   
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual Favorites LoadFavorites(String userCode, String type, String pageName)
        {
            return entityDao.LoadFavorites(userCode, type, pageName);
        }
        #endregion Method Created By CodeSmith
    }
}
