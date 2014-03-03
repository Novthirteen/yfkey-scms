using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IEntityPreferenceBaseDao
    {
        #region Method Created By CodeSmith

        void CreateEntityPreference(EntityPreference entity);

        EntityPreference LoadEntityPreference(String code);
	
		IList<EntityPreference> GetAllEntityPreference();
	
        void UpdateEntityPreference(EntityPreference entity);
        
        void DeleteEntityPreference(String code);
    
        void DeleteEntityPreference(EntityPreference entity);
    
        void DeleteEntityPreference(IList<String> pkList);
    
        void DeleteEntityPreference(IList<EntityPreference> entityList);    
        #endregion Method Created By CodeSmith
    }
}
