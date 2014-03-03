using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Transportation;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Transportation
{
    public interface IVehicleBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateVehicle(Vehicle entity);

        Vehicle LoadVehicle(String code);

        IList<Vehicle> GetAllVehicle();
    
        IList<Vehicle> GetAllVehicle(bool includeInactive);
      
        void UpdateVehicle(Vehicle entity);

        void DeleteVehicle(String code);
    
        void DeleteVehicle(Vehicle entity);
    
        void DeleteVehicle(IList<String> pkList);
    
        void DeleteVehicle(IList<Vehicle> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
