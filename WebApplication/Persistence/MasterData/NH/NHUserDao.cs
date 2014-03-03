using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.MasterData.NH
{
    public class NHUserDao : NHUserBaseDao, IUserDao
    {
        public NHUserDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Customized Methods

        public User FindUserByCode(String code)
        {
            string hql = @"from User as user where user.Code = ?";

            IList<User> users = 
                FindAllWithCustomQuery<User>(hql, code, NHibernate.NHibernateUtil.String);

            if (users != null && users.Count > 0)
            {
                return users[0];
            }

            return null;
        }

        #endregion Customized Methods
    }
}
