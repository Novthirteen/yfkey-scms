using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Mes;
using com.Sconit.Entity.Mes;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.Exception;


//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes.Impl
{
    [Transactional]
    public class ByMaterialMgr : ByMaterialBaseMgr, IByMaterialMgr
    {
        private ICriteriaMgr criteriaMgr;
        public ByMaterialMgr(IByMaterialDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        public override void CreateByMaterial(ByMaterial byMaterial)
        {
            #region ºÏ≤ÈOrder∫ÕItem
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocationTransaction));
            criteria.CreateAlias("OrderDetail", "od");
            criteria.CreateAlias("od.OrderHead", "oh");
            criteria.Add(Expression.Eq("Item.Code", byMaterial.Item.Code));
            criteria.Add(Expression.Eq("oh.OrderNo", byMaterial.OrderNo));
            criteria.Add(Expression.Eq("oh.Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
            criteria.Add(Expression.Eq("oh.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
            criteria.Add(Expression.Eq("TransactionType", BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO));
            criteria.Add(Expression.Eq("TagNo", byMaterial.TagNo));
            IList<OrderLocationTransaction> orderLocTransList = criteriaMgr.FindAll<OrderLocationTransaction>(criteria);

            if (orderLocTransList.Count == 0)
            {
                throw new BusinessErrorException("Mes.ByMaterial.OrderItem.NotExist");
            }
            #endregion
            else
            {
                OrderLocationTransaction ol = orderLocTransList[0];
                ol.Cartons = ol.Cartons + 1;
                base.CreateByMaterial(byMaterial);
            }
        }

        #endregion Customized Methods
    }
}