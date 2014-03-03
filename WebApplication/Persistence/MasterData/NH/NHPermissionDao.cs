using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.MasterData.NH
{
    public class NHPermissionDao : NHPermissionBaseDao, IPermissionDao
    {
        public NHPermissionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Customized Methods

        public virtual void DeletePermission(string code)
        {
            string hql = @"from Permission entity where entity.Code= ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        #endregion Customized Methods
    }
}
