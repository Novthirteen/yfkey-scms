using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ShipAddressMgr : ShipAddressBaseMgr, IShipAddressMgr
    {
        private ICriteriaMgr criteriaMgr;
        public ShipAddressMgr(IShipAddressDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public ShipAddress GetDefaultShipAddress(string partyCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ShipAddress>();
            criteria.Add(Expression.Eq("Party.Code", partyCode));
            criteria.Add(Expression.Eq("IsPrimary", true));

            IList<ShipAddress> shipAddressList = this.criteriaMgr.FindAll<ShipAddress>(criteria, 0, 1);

            if (shipAddressList != null && shipAddressList.Count > 0)
            {
                return shipAddressList[0];
            }

            return null;
        }

        [Transaction(TransactionMode.Unspecified)]
        public ShipAddress GetDefaultShipAddress(Party party)
        {
            return GetDefaultShipAddress(party.Code);
        }

        #endregion Customized Methods
    }
}