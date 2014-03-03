using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using com.Sconit.Persistence.MasterData;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.Mes.NH
{
    public class NHMesBomDetailDao : NHMesBomDetailBaseDao, IMesBomDetailDao
    {
        public NHMesBomDetailDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}
