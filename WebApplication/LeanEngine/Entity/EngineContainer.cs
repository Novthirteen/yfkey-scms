using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeanEngine.Entity
{
    /// <summary>
    /// Parameters container
    /// </summary>
    public class EngineContainer : EntityBase
    {
        private List<ItemFlow> _itemFlows;
        public List<ItemFlow> ItemFlows
        {
            get
            {
                if (_itemFlows == null)
                    _itemFlows = new List<ItemFlow>();
                return _itemFlows;
            }
            set
            {
                _itemFlows = value;
            }
        }

        private List<Plans> _plans;
        public List<Plans> Plans
        {
            get
            {
                if (_plans == null)
                    _plans = new List<Plans>();
                return _plans;
            }
            set
            {
                _plans = value;
            }
        }

        private List<InvBalance> _invBalance;
        public List<InvBalance> InvBalances
        {
            get
            {
                if (_invBalance == null)
                    _invBalance = new List<InvBalance>();
                return _invBalance;
            }
            set
            {
                _invBalance = value;
            }
        }

        private List<DemandChain> _demandChains;
        public List<DemandChain> DemandChains
        {
            get
            {
                if (_demandChains == null)
                    _demandChains = new List<DemandChain>();
                return _demandChains;
            }
            set
            {
                _demandChains = value;
            }
        }
    }
}
