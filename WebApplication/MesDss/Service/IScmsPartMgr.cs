using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IScmsPartMgr
    {
        void CreateScmsPart(ScmsPart entity);

        ScmsPart LoadScmsPart(string code);

        void UpdateScmsPart(ScmsPart entity);

        void DeleteScmsPart(ScmsPart entity);
    }
}
