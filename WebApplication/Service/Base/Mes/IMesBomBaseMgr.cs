using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Mes;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes
{
    public interface IMesBomBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateBom(MesBom entity);

        MesBom LoadBom(String code);

        IList<MesBom> GetAllBom();

        IList<MesBom> GetAllBom(bool includeInactive);

        void UpdateBom(MesBom entity);

        void DeleteBom(String code);

        void DeleteBom(MesBom entity);
    
        void DeleteBom(IList<String> pkList);
    
        void DeleteBom(IList<MesBom> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
