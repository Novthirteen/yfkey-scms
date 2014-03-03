using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IMesScmsStationBoxMgr
    {

        MesScmsStationBox LoadMesScmsStationBox(int id);

        void UpdateMesScmsStationBox(MesScmsStationBox entity);

        IList<MesScmsStationBox> GetUpdateMesScmsStationBox();

        void Complete(MesScmsStationBox mesScmsStationBox);
    }
}
