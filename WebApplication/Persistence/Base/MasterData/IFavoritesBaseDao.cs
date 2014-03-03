using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IFavoritesBaseDao
    {
        #region Method Created By CodeSmith

        void CreateFavorites(Favorites entity);

        Favorites LoadFavorites(Int32 id);
	
		IList<Favorites> GetAllFavorites();
	
        void UpdateFavorites(Favorites entity);
        
        void DeleteFavorites(Int32 id);
    
        void DeleteFavorites(Favorites entity);
    
        void DeleteFavorites(IList<Int32> pkList);
    
        void DeleteFavorites(IList<Favorites> entityList);    
        
        Favorites LoadFavorites(com.Sconit.Entity.MasterData.User user, String type, String pageName);
    
        void DeleteFavorites(String userCode, String type, String pageName);
        
        Favorites LoadFavorites(String userCode, String type, String pageName);
        #endregion Method Created By CodeSmith
    }
}
