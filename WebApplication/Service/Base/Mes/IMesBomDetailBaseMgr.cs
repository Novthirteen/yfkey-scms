using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Mes;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes
{
    public interface IMesBomDetailBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateBomDetail(MesBomDetail entity);

        MesBomDetail LoadBomDetail(Int32 id);

        IList<MesBomDetail> GetAllBomDetail();

        void UpdateBomDetail(MesBomDetail entity);

        void DeleteBomDetail(Int32 id);
    
        void DeleteBomDetail(MesBomDetail entity);
    
        void DeleteBomDetail(IList<Int32> pkList);

        void DeleteBomDetail(IList<MesBomDetail> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
