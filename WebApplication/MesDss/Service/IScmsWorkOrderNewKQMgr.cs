using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IScmsWorkOrderNewKQMgr
    {
        void CreateScmsWorkOrder(ScmsWorkOrderNewKQ entity);

        ScmsWorkOrderNewKQ LoadScmsWorkOrder(string orderNo, string itemCode);

        void UpdateScmsWorkOrder(ScmsWorkOrderNewKQ entity);

        void DeleteScmsWorkOrder(ScmsWorkOrderNewKQ entity);
    }
}
