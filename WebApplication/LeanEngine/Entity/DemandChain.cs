using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeanEngine.Entity
{
    public class DemandChain : EntityBase
    {
        public DemandChain(string loc, string item, string flowCode)
        {
            this.Loc = loc;
            this.Item = item;
            this.FlowCode = flowCode;
        }

        #region Input data
        public int ID { get; set; }
        public string Code { get; set; }

        public string Loc { get; set; }
        public string Item { get; set; }
        public string FlowCode { get; set; }

        public decimal MaxInv { get; set; }
        public decimal SafeInv { get; set; }

        public int BomLevel { get; set; }

        public double AbsoluteLeadTime { get; set; }

        private decimal _absoluteQtyPer = 1;
        /// <summary>
        /// default value: 1
        /// </summary>
        public decimal AbsoluteQtyPer
        {
            get { return _absoluteQtyPer; }
            set { _absoluteQtyPer = value; }
        }

        public bool IsTrace { get; set; }

        public List<ItemFlow> ItemFlows { get; set; }
        #endregion

        #region Output data
        public double RelativeLeadTime { get; set; }

        private decimal _relativeQtyPer = 1;
        /// <summary>
        /// default value: 1
        /// </summary>
        public decimal RelativeQtyPer
        {
            get { return _relativeQtyPer; }
            set { _relativeQtyPer = value; }
        }

        #endregion


        public override int GetHashCode()
        {
            if (ID != 0)
            {
                return ID.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            DemandChain another = obj as DemandChain;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.ID == another.ID);
            }
        }


        public void SetRelativeLeadTime(DemandChain demandChainBase)
        {
            this.RelativeLeadTime = demandChainBase.AbsoluteLeadTime - this.AbsoluteLeadTime;
        }
        public void SetRelativeQtyPer(DemandChain demandChainBase)
        {
            if (this.AbsoluteQtyPer == 0)
                this.AbsoluteQtyPer = 1;

            this.RelativeQtyPer = demandChainBase.AbsoluteQtyPer / this.AbsoluteQtyPer;
        }

    }
}
