using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IMesScmsShelfPartMgr
    {

        MesScmsShelfPart LoadMesScmsShelfPart(string shelfCode,string itemCode);

        void UpdateMesScmsShelfPart(MesScmsShelfPart entity);

        IList<MesScmsShelfPart> GetUpdateMesScmsShelfPart();

        void Complete(MesScmsShelfPart mesScmsShelfPart);
    }
}
