using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeanEngine.Entity;
using LeanEngine.OAE;
using LeanEngine.Utility;

namespace LeanEngine
{
    /// <summary>
    /// Main interface of the Lean Engine
    /// </summary>
    public interface IEngine
    {
        IOAE GetProcessor(Enumerators.Strategy strategy);

        void TellMeDemands(EngineContainer container);

        List<Orders> DemandToOrders(List<ItemFlow> itemFlows);
    }
}
