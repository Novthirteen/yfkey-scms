using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IMesScmsStationShelfMgr
    {

        MesScmsStationShelf LoadMesScmsStationShelf(string code);

        void UpdateMesScmsStationShelf(MesScmsStationShelf entity);


        IList<MesScmsStationShelf> GetUpdateMesScmsStationShelf();

        void Complete(MesScmsStationShelf mesScmsStationShelf);
    }
}
