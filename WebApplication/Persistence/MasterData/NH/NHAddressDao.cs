using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using com.Sconit.Entity.MasterData;
using NHibernate.Type;
using NHibernate;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.MasterData.NH
{
    public class NHAddressDao : NHAddressBaseDao, IAddressDao
    {
        public NHAddressDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Customized Methods

        public ShipAddress LoadShipAddress(string code)
        {
            return FindById<ShipAddress>(code);
        }

        public BillAddress LoadBillAddress(string code)
        {
            return FindById<BillAddress>(code);
        }

        public void DeleteAddressByParent(String parentCode)
        {
            string hql = @"from Address entity where entity.Party.Code = ?";
            Delete(hql, new object[] { parentCode }, new IType[] { NHibernateUtil.String });
        }


        #endregion Customized Methods
    }
}
