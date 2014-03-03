using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IMesScmsCompletedBoxMgr
    {

        MesScmsCompletedBox LoadMesScmsCompletedBox(string code);

        void UpdateMesScmsCompletedBox(MesScmsCompletedBox entity);

        IList<MesScmsCompletedBox> GetUpdateMesScmsCompletedBox();

        void Complete(MesScmsCompletedBox mesScmsCompletedBox);
    }
}
