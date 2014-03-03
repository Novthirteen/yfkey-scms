using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Utility;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class HuOddMgr : HuOddBaseMgr, IHuOddMgr
    {
        private ICriteriaMgr criteriaMgr;
        public HuOddMgr(IHuOddDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public HuOdd CreateHuOdd(ReceiptDetail receiptDetail, LocationLotDetail locationLotDetail, User user)
        {
            OrderLocationTransaction orderLocationTransaction = receiptDetail.OrderLocationTransaction;
            OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
            DateTime dateTimeNow = DateTime.Now;

            HuOdd huOdd = new HuOdd();
            huOdd.OrderDetail = orderDetail;
            huOdd.LocationLotDetail = locationLotDetail;
            huOdd.OddQty = receiptDetail.ReceivedQty.Value;
            huOdd.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
            huOdd.CreatedQty = 0;
            huOdd.CreateUser = user;
            huOdd.CreateDate = dateTimeNow;
            huOdd.LastModifyUser = user;
            huOdd.LastModifyDate = dateTimeNow;

            this.CreateHuOdd(huOdd);
            return huOdd;
        }


        [Transaction(TransactionMode.Unspecified)]
        public IList<HuOdd> GetHuOdd(Item item, decimal unitCount, Uom uom, Location locFrom, Location locTo, string status)
        {
            DetachedCriteria criteria = DetachedCriteria.For<HuOdd>();
            criteria.CreateAlias("OrderDetail", "od");
            criteria.CreateAlias("od.Item", "item");
            criteria.CreateAlias("od.Uom", "uom");
            criteria.CreateAlias("LocationLotDetail", "lld");
            criteria.CreateAlias("lld.Location", "locTo");

            criteria.Add(Expression.Eq("item.Code", item.Code));
            criteria.Add(Expression.Eq("od.UnitCount", unitCount));
            criteria.Add(Expression.Eq("uom.Code", uom.Code));
            criteria.Add(Expression.Eq("locTo.Code", locTo.Code));
            criteria.Add(Expression.Eq("Status", status));

            criteria.AddOrder(Order.Asc("CreateDate"));

            IList<HuOdd> huOddList = this.criteriaMgr.FindAll<HuOdd>(criteria);

            if (huOddList != null && huOddList.Count > 0)
            {
                //过滤来源库位
                IList<HuOdd> filteredHuOddList = new List<HuOdd>();

                foreach (HuOdd huOdd in huOddList)
                {
                    if (LocationHelper.IsLocationEqual(huOdd.OrderDetail.DefaultLocationFrom, locFrom))
                    {
                        filteredHuOddList.Add(huOdd);
                    }
                }

                return filteredHuOddList;
            }

            return null;
        }        
        #endregion Customized Methods

        #region Private Methods
       
        #endregion
    }
}