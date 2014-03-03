using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IScmsMaterialBarcodeMgr
    {
        void CreateScmsMaterialBarcode(ScmsMaterialBarcode entity);

        ScmsMaterialBarcode LoadScmsMaterialBarcode(string code);

        void UpdateScmsMaterialBarcode(ScmsMaterialBarcode entity);

        void DeleteScmsMaterialBarcode(ScmsMaterialBarcode entity);
    }
}
