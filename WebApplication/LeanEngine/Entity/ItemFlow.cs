using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeanEngine.Utility;

namespace LeanEngine.Entity
{
    public class ItemFlow : EntityBase
    {
        #region Input data
        public int ID { get; set; }

        public Flow Flow { get; set; }
        public Bom Bom { get; set; }
        //public List<DemandChain> DemandChains { get; set; }

        public string Item { get; set; }
        public string LocFrom { get; set; }
        public string LocTo { get; set; }

        public decimal MaxInv { get; set; }
        public decimal SafeInv { get; set; }

        public decimal UC { get; set; }
        public decimal MinLotSize { get; set; }
        public decimal OrderLotSize { get; set; }
        public Enumerators.RoundUp RoundUp { get; set; }

        public List<string> DemandSources { get; set; }
        #endregion

        #region Output data
        public decimal ReqQty { get; set; }
        public decimal OrderQty { get; set; }
        public List<decimal> OrderQtyList { get; set; }

        public List<OrderTracer> OrderTracers { get; set; }

        private bool _isEmergency = false;
        /// <summary>
        /// default value: false
        /// </summary>
        public bool IsEmergency
        {
            get { return _isEmergency; }
            set { _isEmergency = value; }
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
            ItemFlow another = obj as ItemFlow;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.ID == another.ID);
            }
        }

        public void AddOrderTracer(OrderTracer entity)
        {
            if (entity != null)
            {
                if (this.OrderTracers == null)
                    this.OrderTracers = new List<OrderTracer>();

                this.OrderTracers.Add(entity);
            }
        }
        public void AddOrderTracer(List<OrderTracer> list)
        {
            if (list != null && list.Count > 0)
            {
                if (this.OrderTracers == null)
                    this.OrderTracers = new List<OrderTracer>();

                this.OrderTracers.AddRange(list);
            }
        }
    }
}
