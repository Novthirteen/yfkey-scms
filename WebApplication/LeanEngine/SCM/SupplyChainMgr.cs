using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeanEngine.Entity;
using LeanEngine.Utility;

namespace LeanEngine.SCM
{
    public class SupplyChainMgr : ISupplyChainMgr
    {
        /// <summary>
        /// Build supply chain by finished good itemcode
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="ItemFlows"></param>
        /// <returns></returns>
        public List<SupplyChain> BuildSupplyChain(string itemCode, List<ItemFlow> ItemFlows)
        {
            if (itemCode == null || ItemFlows == null || ItemFlows.Count == 0)
                return null;

            List<SupplyChain> supplyChains = new List<SupplyChain>();

            int bomLevel = 0;
            var query = ItemFlows.Where(i => Utilities.StringEq(i.Item, itemCode)).ToList();

            List<string> locList = this.LocSort(query);

            foreach (var loc in locList)
            {
                var q2 = query.Where(i => Utilities.StringEq(i.LocTo, loc)).ToList();
                foreach (var itemFlow in q2)
                {
                    SupplyChain supplyChain = new SupplyChain();
                    supplyChain.BomLevel = bomLevel;
                    supplyChain.ItemFlow = itemFlow;
                    supplyChain.Item = itemFlow.Item;
                    supplyChain.Loc = itemFlow.LocFrom;

                    supplyChains.Add(supplyChain);
                }
            }

            return supplyChains;
        }

        public List<string> LocSort(List<ItemFlow> ItemFlows)
        {
            if (ItemFlows == null || ItemFlows.Count == 0)
                throw new BusinessException("ItemFlow is key infomation, it can't be empty!");

            ItemFlows.Sort(ItemFlowComparer);
            List<string> locList = new List<string>();
            foreach (var itemFlow in ItemFlows)
            {
                locList.Add(itemFlow.LocTo);
                locList.Add(itemFlow.LocFrom);
            }

            locList.Distinct().ToList<string>();
            return locList;
        }

        private List<SupplyChain> SupplyChainBuilder(string itemCode, List<ItemFlow> ItemFlows, int bomLevel, List<SupplyChain> supplyChains)
        {
            var qItemFlow = ItemFlows.Where(i => Utilities.StringEq(i.Item, itemCode)).ToList();

            List<string> locList = this.LocSort(qItemFlow);
            foreach (var loc in locList)
            {
                if (loc == null)
                    continue;

                var q2 = qItemFlow.Where(i => Utilities.StringEq(i.LocFrom, loc)).ToList<ItemFlow>();
                foreach (var itemFlow in q2)
                {
                    SupplyChain supplyChain = new SupplyChain();
                    supplyChain.BomLevel = bomLevel;
                    supplyChain.ItemFlow = itemFlow;
                    supplyChain.Item = itemFlow.Item;
                    supplyChain.Loc = itemFlow.LocFrom;

                    supplyChains.Add(supplyChain);
                }
            }

            return supplyChains;
        }

        private int ItemFlowComparer(ItemFlow x, ItemFlow y)
        {
            if (x.LocTo == null)
            {
                if (y.LocTo == null)
                    return 0;
                else
                    return -1;
            }
            else if (x.LocFrom == null)
            {
                if (y.LocFrom == null)
                    return 0;
                else
                    return 1;
            }
            else if (y.LocFrom != null && y.LocTo != null)
            {
                if (Utilities.StringEq(x.LocFrom, y.LocTo))
                    return -1;
                else if (Utilities.StringEq(x.LocTo, y.LocFrom))
                    return 1;
            }

            return 0;
        }
    }
}
