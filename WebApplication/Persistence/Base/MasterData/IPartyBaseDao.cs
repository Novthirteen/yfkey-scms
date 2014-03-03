using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IPartyBaseDao
    {
        #region Method Created By CodeSmith

        void CreateParty(Party entity);

        Party LoadParty(String code);
	
		IList<Party> GetAllParty();

        IList<Party> GetAllParty(bool includeInactive);
	
        void UpdateParty(Party entity);
        
        void DeleteParty(String code);
    
        void DeleteParty(Party entity);
    
        void DeleteParty(IList<String> pkList);
    
        void DeleteParty(IList<Party> entityList);    
        #endregion Method Created By CodeSmith
    }
}
