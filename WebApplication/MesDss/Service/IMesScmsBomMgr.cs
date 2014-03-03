using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IMesScmsBomMgr
    {

        MesScmsBom LoadMesScmsBom(string bom, string itemCode, string tagNo, string prodLine);

        void UpdateMesScmsBom(MesScmsBom entity);

        IList<MesScmsBom> GetUpdateMesScmsBom();

        void Complete(MesScmsBom mesScmsBom);
    }
}
