using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IScmsTableIndexMgr
    {

        void UpdateScmsTableIndex(string tableName, DateTime lastModifyDate);

        void CreateScmsTableIndex(ScmsTableIndex entity);

        ScmsTableIndex LoadScmsTableIndex(string tableName);

        void UpdateScmsTableIndex(ScmsTableIndex entity);

        void DeleteScmsTableIndex(ScmsTableIndex entity);

        IList<ScmsTableIndex> GetUpdateScmsTableIndex();

        void Complete(ScmsTableIndex scmsTableIndex);

    }
}
