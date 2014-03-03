using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using Castle.Services.Transaction;

namespace com.Sconit.Service.Business.Impl
{
    public class OnlineMgr : AbstractBusinessMgr
    {
        private ISetBaseMgr setBaseMgr;
        private ISetDetailMgr setDetailMgr;
        private IExecuteMgr executeMgr;
        private IOrderMgr orderMgr;

        public OnlineMgr(
            ISetBaseMgr setBaseMgr,
            ISetDetailMgr setDetailMgr,
            IExecuteMgr executeMgr,
            IOrderMgr orderMgr
            )
        {
            this.setBaseMgr = setBaseMgr;
            this.setDetailMgr = setDetailMgr;
            this.executeMgr = executeMgr;
            this.orderMgr = orderMgr;
        }

        protected override void SetBaseInfo(Resolver resolver)
        {
            this.StartOrder(resolver);
        }

        protected override void GetDetail(Resolver resolver)
        {
        }

        protected override void SetDetail(Resolver resolver)
        {
            this.StartOrder(resolver);
        }

        protected override void ExecuteSubmit(Resolver resolver)
        {
        }

        protected override void ExecuteCancel(Resolver resolver)
        {
        }

        private void StartOrder(Resolver resolver)
        {
            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER)
            {
                setBaseMgr.FillResolverByOrder(resolver);

                #region 校验
                if (resolver.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
                    throw new BusinessErrorException("Common.Business.Error.StatusError", resolver.Code, resolver.Status);

                if (resolver.OrderType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                    throw new BusinessErrorException("Order.Error.OrderOfflineIsNotProduction", resolver.Code, resolver.OrderType);
                #endregion

                orderMgr.StartOrder(resolver.Input, resolver.UserCode);
                resolver.Result = DateTime.Now.ToString("HH:mm:ss");
                resolver.Code = resolver.Input;
            }
            else
            {
                throw new BusinessErrorException("Common.Business.Error.BarCodeInvalid");
            }

        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void ExecutePrint(Resolver resolver)
        {
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override void GetReceiptNotes(Resolver resolver)
        {


        }
    }
}
