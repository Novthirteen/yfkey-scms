using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface ILocationLotDetailViewBaseDao
    {
        #region Method Created By CodeSmith

        void CreateLocationLotDetailView(LocationLotDetailView entity);

        LocationLotDetailView LoadLocationLotDetailView(Int32 id);
  
        IList<LocationLotDetailView> GetAllLocationLotDetailView();
  
        void UpdateLocationLotDetailView(LocationLotDetailView entity);
        
        void DeleteLocationLotDetailView(Int32 id);
    
        void DeleteLocationLotDetailView(LocationLotDetailView entity);
    
        void DeleteLocationLotDetailView(IList<Int32> pkList);
    
        void DeleteLocationLotDetailView(IList<LocationLotDetailView> entityList);    
        #endregion Method Created By CodeSmith
    }
}
