using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Procurement
{
    public class SupplyChainDetail
    {
        private SupplyChain _supplyChain;
        public SupplyChain SupplyChain
        {
            get
            {
                return _supplyChain;
            }
            set
            {
                _supplyChain = value;
            }
        }
        private Int32 _id;
        public Int32 Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private Int32 _parentId;
        public Int32 ParentId
        {
            get
            {
                return _parentId;
            }
            set
            {
                _parentId = value;
            }
        }
        private Flow _flow;
        public Flow Flow
        {
            get
            {
                return _flow;
            }
            set
            {
                _flow = value;
            }
        }
        private FlowDetail _flowDetail;
        public FlowDetail FlowDetail
        {
            get
            {
                return _flowDetail;
            }
            set
            {
                _flowDetail = value;
            }
        }
        private Location _locationTo;
        public Location LocationTo
        {
            get
            {
                return _locationTo;
            }
            set
            {
                _locationTo = value;
            }
        }
        private decimal _quantityPer;
        public decimal QuantityPer
        {
            get
            {
                return _quantityPer;
            }
            set
            {
                _quantityPer = value;
            }
        }
        private string _planType;
        public string PlanType
        {
            get
            {
                return _planType;
            }
            set
            {
                _planType = value;
            }
        }

    }
}
