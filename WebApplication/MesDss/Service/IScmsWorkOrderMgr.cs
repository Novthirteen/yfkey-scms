using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IScmsWorkOrderMgr
    {
        void CreateScmsWorkOrder(ScmsWorkOrder entity);

        ScmsWorkOrder LoadScmsWorkOrder(string orderNo,string itemCode);

        void UpdateScmsWorkOrder(ScmsWorkOrder entity);

        void DeleteScmsWorkOrder(ScmsWorkOrder entity);
    }
}
