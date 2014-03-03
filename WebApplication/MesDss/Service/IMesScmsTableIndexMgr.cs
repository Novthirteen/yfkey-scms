using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IMesScmsTableIndexMgr
    {

        void UpdateMesScmsTableIndex(string tableName, DateTime lastModifyDate);

        void CreateMesScmsTableIndex(MesScmsTableIndex entity);

        MesScmsTableIndex LoadMesScmsTableIndex(string tableName);

        void UpdateMesScmsTableIndex(MesScmsTableIndex entity);

        void DeleteMesScmsTableIndex(MesScmsTableIndex entity);

        IList<MesScmsTableIndex> GetUpdateMesScmsTableIndex();

        void Complete(MesScmsTableIndex mesScmsTableIndex);
    }
}
