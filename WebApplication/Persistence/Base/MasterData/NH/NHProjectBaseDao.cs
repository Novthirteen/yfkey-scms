using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Persistence.MasterData.NH
{
  public  class NHProjectBaseDao:NHDaoBase,IProjectBaseDao
    {
        public NHProjectBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }


        public virtual IList<ProjectMstr> GetAllItem()
        {
            return GetAllItem(false);
        }

        public virtual IList<ProjectMstr> GetAllItem(bool includeInactive)
        {

            return FindAll<ProjectMstr>();
          
          
        }
    }
}
