using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ClientWorkingHoursMgr : ClientWorkingHoursBaseMgr, IClientWorkingHoursMgr
    {
        private ICriteriaMgr criteriaMgr;
        public ClientWorkingHoursMgr(IClientWorkingHoursDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        public IList<ClientWorkingHours> GetAllClientWorkingHours(string OrderHeadId)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ClientWorkingHours));
            criteria.Add(Expression.Eq("ClientOrderHead.Id", OrderHeadId));
            IList<ClientWorkingHours> clientWorkingHoursList = criteriaMgr.FindAll<ClientWorkingHours>(criteria);
            if (clientWorkingHoursList.Count > 0)
            {
                return clientWorkingHoursList;
            }
            return null;
        }


        #endregion Customized Methods
    }
}