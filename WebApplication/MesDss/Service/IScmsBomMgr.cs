using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IScmsBomMgr
    {
        void CreateScmsBom(ScmsBom entity);

        ScmsBom LoadScmsBom(string bomCode,string itemCode);

        void UpdateScmsBom(ScmsBom entity);

        void DeleteScmsBom(ScmsBom entity);
    }
}
