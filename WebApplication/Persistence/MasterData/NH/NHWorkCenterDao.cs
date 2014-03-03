using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate.Type;
using NHibernate;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.MasterData.NH
{
    public class NHWorkCenterDao : NHWorkCenterBaseDao, IWorkCenterDao
    {
        public NHWorkCenterDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Customized Methods

        public void DeleteWorkCenterByParent(String parentCode)
        {
            string hql = @"from WorkCenter entity where entity.Party.Code = ?";
            Delete(hql, new object[] { parentCode }, new IType[] { NHibernateUtil.String });
        }

        #endregion Customized Methods
    }
}
