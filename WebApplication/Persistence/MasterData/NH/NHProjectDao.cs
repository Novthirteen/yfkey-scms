using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;

namespace com.Sconit.Persistence.MasterData.NH
{
   public class NHProjectDao:NHProjectBaseDao,IProjectDao
    {
        public NHProjectDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }





    }
}
