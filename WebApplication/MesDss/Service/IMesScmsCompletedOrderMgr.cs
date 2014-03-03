using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IMesScmsCompletedOrderMgr
    {

        MesScmsCompletedOrder LoadMesScmsCompletedOrder(string code);

        void UpdateMesScmsCompletedOrder(MesScmsCompletedOrder entity);

        IList<MesScmsCompletedOrder> GetUpdateMesScmsCompletedOrder();

        void Complete(MesScmsCompletedOrder mesScmsCompletedOrder);
    }
}
