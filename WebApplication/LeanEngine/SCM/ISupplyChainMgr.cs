using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeanEngine.Entity;

namespace LeanEngine.SCM
{
    public interface ISupplyChainMgr
    {
        List<SupplyChain> BuildSupplyChain(string itemCode, List<ItemFlow> ItemFlows);
    }
}
