using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IScmsWorkOrderNewMgr
    {
        void CreateScmsWorkOrder(ScmsWorkOrderNew entity);

        ScmsWorkOrderNew LoadScmsWorkOrder(string orderNo, string itemCode);

        void UpdateScmsWorkOrder(ScmsWorkOrderNew entity);

        void DeleteScmsWorkOrder(ScmsWorkOrderNew entity);
    }
}
