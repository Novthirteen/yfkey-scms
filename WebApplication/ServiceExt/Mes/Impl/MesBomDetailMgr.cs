using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Utility;
using NHibernate.Expression;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Mes;
using com.Sconit.Persistence.Mes;

using System.Linq;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes.Impl
{
    [Transactional]
    public class MesBomDetailMgr : MesBomDetailBaseMgr, IMesBomDetailMgr
    {
        #region ±‰¡ø
        private ICriteriaMgr criteriaMgr;
        private IUomConversionMgr uomConversionMgr;
        private IMesBomMgr mesBomMgr;
        private IItemMgr itemMgr;
        private IRoutingMgr routingMgr;
        private IRoutingDetailMgr routingDetailMgr;

        private IUserMgr useMgr;

        #endregion

        public MesBomDetailMgr(IMesBomDetailDao entityDao,
            IMesBomMgr mesBomMgr,
            IItemKitMgr itemKitMgr,
            ICriteriaMgr criteriaMgr,
            IUomConversionMgr uomConversionMgr,
            IRoutingMgr routingMgr,
            IRoutingDetailMgr routingDetailMgr,
            IItemMgr itemMgr,
            IUserMgr useMgr)
            : base(entityDao)
        {
            this.mesBomMgr = mesBomMgr;
            this.itemMgr = itemMgr;
            this.criteriaMgr = criteriaMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.routingMgr = routingMgr;
            this.routingDetailMgr = routingDetailMgr;;
            this.useMgr = useMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public MesBomDetail GetBomDetail(string bomCode, string itemCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(MesBomDetail));
            criteria.Add(Expression.Eq("Bom.Code", bomCode));
            criteria.Add(Expression.Eq("Item.Code", itemCode));
            criteria.AddOrder(Order.Desc("StartDate"));

            IList<MesBomDetail> bomDetailList = criteriaMgr.FindAll<MesBomDetail>(criteria);
            if (bomDetailList != null && bomDetailList.Count > 0)
            {
                return bomDetailList[0];
            }
            else
            {
                return null;
            }
        }
        
        [Transaction(TransactionMode.Unspecified)]
        public IList<MesBom> GetRelatedBomDetail(MesBomDetail mesBomDetail)
        {
            List<MesBom> mesBomList = new List<MesBom>();
            if (mesBomDetail.Item.IsMes)
            {
                mesBomList.Add(mesBomDetail.Bom);
            }
            IList<MesBomDetail> mesBomDetailList = this.GetAllBomDetailList(null, mesBomDetail.Bom.Code);
            if (mesBomDetailList != null && mesBomDetailList.Count > 0)
            {
                foreach (MesBomDetail mb in mesBomDetailList)
                {
                    mesBomList.AddRange(GetRelatedBomDetail(mb));
                }
            }
            return mesBomList;
        }


        [Transaction(TransactionMode.Unspecified)]
        public List<MesBomDetail> GetBomDetailList(MesBom mesBom)
        {
            MesBom topBom = mesBomMgr.LoadBom(mesBom.Code, true);
            List<MesBomDetail> mesBomDetailList = new List<MesBomDetail>();
            if (topBom.BomDetails != null && topBom.BomDetails.Count > 0)
            {
                foreach (MesBomDetail mesBomDetail in topBom.BomDetails)
                {
                    if (mesBomDetail.StructureType == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_N)
                    {
                        mesBomDetailList.Add(mesBomDetail);
                    }
                    else if (mesBomDetail.StructureType == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_X)
                    {
                        List<MesBomDetail> xBomDetail = GetBomDetailList(mesBomDetail.Bom.Code, mesBomDetail.Item.Code);
                        mesBomDetailList.AddRange(xBomDetail);
                    }
                }
            }

            var query = mesBomDetailList.Select(l => new
            {
                Bom = l.Bom,
                Item = l.Item,
                RateQty = (from m in mesBomDetailList
                           where m.Bom.Code.Trim().ToUpper() == l.Bom.Code.Trim().ToUpper()
                           && m.Item.Code.Trim().ToUpper() == l.Item.Code.Trim().ToUpper()
                           && m.IsActive == l.IsActive
                           select m.RateQty).Sum(),
                IsActive = l.IsActive
            }).Distinct();

            List<MesBomDetail> list = new List<MesBomDetail>();
            foreach (var item in query)
            {
                MesBomDetail mesBomDetail = new MesBomDetail();
                mesBomDetail.Bom = item.Bom;
                mesBomDetail.Item = item.Item;
                mesBomDetail.RateQty = item.RateQty;
                mesBomDetail.IsActive = item.IsActive;
                list.Add(mesBomDetail);
            }
            return list;

        }

        [Transaction(TransactionMode.Unspecified)]
        public List<MesBomDetail> GetBomDetailList(string bomCode, string itemCode)
        {
            List<MesBomDetail> mesBomDetailList = new List<MesBomDetail>();
            IList<MesBomDetail> bomDetailList = GetNextLevelBomDetail(itemCode);
            foreach (MesBomDetail scmsBomDetail in bomDetailList)
            {
                if (scmsBomDetail.StructureType == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_N)
                {
                    MesBomDetail bomDetail = new MesBomDetail();
                    bomDetail.Bom = mesBomMgr.LoadBom(bomCode);
                    bomDetail.Item = scmsBomDetail.Item;
                    bomDetail.RateQty = scmsBomDetail.RateQty;
                    bomDetail.IsActive = scmsBomDetail.IsActive;
                    mesBomDetailList.Add(bomDetail);

                }
                else if (scmsBomDetail.StructureType == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_X)
                {
                    List<MesBomDetail> xBomDetailList = GetBomDetailList(bomCode, scmsBomDetail.Item.Code);
                    mesBomDetailList.AddRange(xBomDetailList);
                }
            }

            return mesBomDetailList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<MesBomDetail> GetNextLevelBomDetail(string bomCode)
        {
            DetachedCriteria detachedCriteria = DetachedCriteria.For<MesBomDetail>();
            detachedCriteria.Add(Expression.Eq("Bom.Code", bomCode));
            return criteriaMgr.FindAll<MesBomDetail>(detachedCriteria);
        }


        [Transaction(TransactionMode.Unspecified)]
        private IList<MesBomDetail> GetAllBomDetailList(string bomCode, string itemCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(MesBomDetail));
            if (bomCode != null && bomCode != string.Empty)
            {
                criteria.Add(Expression.Eq("Bom.Code", bomCode));
            }
            if (itemCode != null && itemCode != string.Empty)
            {
                criteria.Add(Expression.Eq("Item.Code", itemCode));
            }
            criteria.AddOrder(Order.Desc("StartDate"));

            return criteriaMgr.FindAll<MesBomDetail>(criteria);

        }

        #endregion
    }
}