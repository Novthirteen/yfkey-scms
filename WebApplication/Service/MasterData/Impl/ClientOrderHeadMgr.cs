using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ClientOrderHeadMgr : ClientOrderHeadBaseMgr, IClientOrderHeadMgr
    {
        private IClientOrderDetailMgr clientOrderDetailMgr;
        private IClientWorkingHoursMgr clientWorkingHoursMgr;

        public ClientOrderHeadMgr(IClientOrderHeadDao entityDao, IClientOrderDetailMgr clientOrderDetailMgr, IClientWorkingHoursMgr clientWorkingHoursMgr)
            : base(entityDao)
        {
            this.clientOrderDetailMgr = clientOrderDetailMgr;
            this.clientWorkingHoursMgr = clientWorkingHoursMgr;
        }

        #region Customized Methods

        //TODO: Add other methods here.
        [Transaction(TransactionMode.Unspecified)]
        public void CreateClientOrderHead(ClientOrderHead clientOrderHead, IList<ClientOrderDetail> clientOrderDetailList, IList<ClientWorkingHours> clientWorkingHoursList)
        {
            CreateClientOrderHead(clientOrderHead);
            foreach (ClientOrderDetail clientOrderDetail in clientOrderDetailList)
            {
                clientOrderDetailMgr.CreateClientOrderDetail(clientOrderDetail);
            }
            if (clientWorkingHoursList!=null && clientWorkingHoursList.Count>0)
            {
                foreach (ClientWorkingHours clientWorkingHours in clientWorkingHoursList)
                {
                    clientWorkingHoursMgr.CreateClientWorkingHours(clientWorkingHours);
                }
            }
        }

        #endregion Customized Methods
    }
}