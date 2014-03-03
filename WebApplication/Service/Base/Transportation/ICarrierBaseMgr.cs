using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Transportation;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Transportation
{
    public interface ICarrierBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateCarrier(Carrier entity);

        Carrier LoadCarrier(String code);

        IList<Carrier> GetAllCarrier();
    
        void UpdateCarrier(Carrier entity);

        void DeleteCarrier(String code);
    
        void DeleteCarrier(Carrier entity);
    
        void DeleteCarrier(IList<String> pkList);
    
        void DeleteCarrier(IList<Carrier> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
