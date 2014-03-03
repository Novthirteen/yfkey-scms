using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Procurement
{
    public class SupplyChain
    {
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
        private IList<SupplyChainDetail> _supplyChainDetails;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<SupplyChainDetail> SupplyChainDetails
        {
            get
            {
                return _supplyChainDetails;
            }
            set
            {
                _supplyChainDetails = value;
            }
        }

        public void AddSupplyChainDetail(SupplyChainDetail supplyChainDetail)
        {
            if (this.SupplyChainDetails == null)
            {
                this.SupplyChainDetails = new List<SupplyChainDetail>();
            }

            this.SupplyChainDetails.Add(supplyChainDetail);
        }

        public void AddRangeSupplyChainDetail(IList<SupplyChainDetail> supplyChainDetailList)
        {
            foreach (SupplyChainDetail supplyChainDetail in supplyChainDetailList)
            {
                this.AddSupplyChainDetail(supplyChainDetail);
            }
        }
    }
}
