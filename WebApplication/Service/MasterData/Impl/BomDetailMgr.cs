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


//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class BomDetailMgr : BomDetailBaseMgr, IBomDetailMgr
    {
        #region ����
        private ICriteriaMgr criterialMgr;
        private IUomConversionMgr uomConversionMgr;
        private IBomMgr bomMgr;
        private IItemMgr itemMgr;
        private IRoutingMgr routingMgr;
        private IRoutingDetailMgr routingDetailMgr;

        private string[] BomCompDetail = new string[] { 
            "Item", 
            "Operation", 
            "Reference", 
            "StructureType", 
            "StartDate", 
            "EndDate", 
            "Uom", 
            "RateQty", 
            "ScrapPercentage", 
            "NeedPrint", 
            "Priority", 
            "Location", 
            "IsShipScanHu", 
            "HuLotSize", 
            "OptionalItemGroup",
            "CalculatedQty" };
        #endregion

        public BomDetailMgr(IBomDetailDao entityDao,
            IBomMgr bomMgr,
            IItemKitMgr itemKitMgr,
            ICriteriaMgr criterialMgr,
            IUomConversionMgr uomConversionMgr,
            IRoutingMgr routingMgr,
            IRoutingDetailMgr routingDetailMgr,
            IItemMgr itemMgr)
            : base(entityDao)
        {
            this.bomMgr = bomMgr;
            this.itemMgr = itemMgr;
            this.criterialMgr = criterialMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.routingMgr = routingMgr;
            this.routingDetailMgr = routingDetailMgr;

        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public IList<BomDetail> GetNextLevelBomDetail(string bomCode, DateTime efftiveDate)
        {
            //NullableDateTime nullableEffDate = new NullableDateTime(efftiveDate);

            DetachedCriteria detachedCriteria = DetachedCriteria.For<BomDetail>();
            detachedCriteria.Add(Expression.Eq("Bom.Code", bomCode));
            detachedCriteria.Add(Expression.Le("StartDate", efftiveDate));
            detachedCriteria.Add(Expression.Or(Expression.Ge("EndDate", efftiveDate), Expression.IsNull("EndDate")));

            IList<BomDetail> bomDetailList = criterialMgr.FindAll<BomDetail>(detachedCriteria);

            return this.GetNoOverloadBomDetail(bomDetailList);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<BomDetail> GetFlatBomDetail(string bomCode, DateTime efftiveDate)
        {
            IList<BomDetail> flatBomDetailList = new List<BomDetail>();
            IList<BomDetail> nextBomDetailList = this.GetNextLevelBomDetail(bomCode, efftiveDate);

            if (nextBomDetailList != null && nextBomDetailList.Count > 0)
            {
                foreach (BomDetail nextBomDetail in nextBomDetailList)
                {
                    nextBomDetail.CalculatedQty = nextBomDetail.RateQty * (1 + nextBomDetail.ScrapPercentage);
                    ProcessCurrentBomDetail(flatBomDetailList, nextBomDetail, efftiveDate);
                }
            }
            return flatBomDetailList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public bool CheckUniqueExist(string parCode, string compCode, int Operation, string Reference, DateTime startTime)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(BomDetail));
            criteria.Add(Expression.Eq("Bom.Code", parCode));
            criteria.Add(Expression.Eq("Item.Code", compCode));
            criteria.Add(Expression.Eq("Operation", Operation));
            criteria.Add(Expression.Eq("Reference", Reference));
            criteria.Add(Expression.Ge("StartDate", startTime));

            IList bomDetails = criterialMgr.FindAll(criteria);
            if (bomDetails.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<BomDetail> GetTreeBomDetail(string bomCode, DateTime effDate)
        {
            IList<BomDetail> treeBomDetailList = new List<BomDetail>();
            IList<BomDetail> nextBomDetailList = this.GetNextLevelBomDetail(bomCode, effDate);
            if (nextBomDetailList != null && nextBomDetailList.Count > 0)
            {
                foreach (BomDetail nextBomDetail in nextBomDetailList)
                {
                    nextBomDetail.CalculatedQty = nextBomDetail.RateQty * (1 + nextBomDetail.ScrapPercentage);
                    treeBomDetailList = this.GetAllBomDetailTree(nextBomDetailList, effDate);
                }
            }

            return this.GetNoOverloadBomDetail(treeBomDetailList);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<BomDetail> GetTopLevelBomDetail(string itemCode, DateTime effDate)
        {
            IList<BomDetail> returnBomDetailList = new List<BomDetail>();
            //IList<BomDetail> bomDetailList = new List<BomDetail>();
            //IList<BomDetail> lastLevelBomDetailList = new List<BomDetail>();
            //lastLevelBomDetailList = this.GetLastLevelBomDetail(itemCode, effDate);
            //if (lastLevelBomDetailList != null && lastLevelBomDetailList.Count > 0)
            //{
            //    foreach (BomDetail bomDetail in lastLevelBomDetailList)
            //    {
            //        bomDetailList = this.GetTopLevelBomDetail(bomDetail.Bom.Code, effDate);
            //        if (lastLevelBomDetailList != null && lastLevelBomDetailList.Count > 0)
            //        {
            //            foreach (BomDetail bd in bomDetailList)
            //            {
            //                returnBomDetailList.Add(bd);
            //            }
            //        }
            //        else
            //        {
            //            returnBomDetailList.Add(bomDetail);
            //        }
            //    }
            //}

            return returnBomDetailList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<BomDetail> GetLastLevelBomDetail(string itemCode, DateTime effDate)
        {
            //NullableDateTime nullableEffDate = new NullableDateTime(effDate);

            DetachedCriteria criteria = DetachedCriteria.For<BomDetail>();
            criteria.Add(Expression.Eq("Item.Code", itemCode));
            criteria.Add(Expression.Le("StartDate", effDate));
            criteria.Add(Expression.Or(Expression.Ge("EndDate", effDate), Expression.IsNull("EndDate")));

            IList<BomDetail> bomDetailList = criterialMgr.FindAll<BomDetail>(criteria);

            return this.GetNoOverloadBomDetail(bomDetailList);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<BomDetail> GetBomView_Nml(Item item, DateTime effDate)
        {
            IList<BomDetail> bomViewList = new List<BomDetail>();
            IList<BomDetail> bomDetailList = new List<BomDetail>();
            IList<BomDetail> lastLevelBomDetailList = this.GetLastLevelBomDetail(item.Code, effDate);
            if (lastLevelBomDetailList != null && lastLevelBomDetailList.Count > 0)
            {
                foreach (BomDetail bomDetail in lastLevelBomDetailList)
                {
                    bomDetailList = this.GetNextLevelBomDetail(bomDetail.Bom.Code, effDate);
                    foreach (BomDetail bd in bomDetailList)
                    {
                        bd.CalculatedQty = bd.RateQty * (1 + bd.ScrapPercentage);
                        bomViewList.Add(bd);
                    }
                }
            }

            string nextLevelBomCode = (item.Bom != null ? item.Bom.Code : item.Code);
            bomDetailList = this.GetNextLevelBomDetail(nextLevelBomCode, effDate);
            if (bomDetailList != null && bomDetailList.Count > 0)
            {
                foreach (BomDetail bd in bomDetailList)
                {
                    bd.CalculatedQty = bd.RateQty * (1 + bd.ScrapPercentage);
                    bomViewList.Add(bd);
                }
            }

            return this.GetNoOverloadBomDetail(bomViewList);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<BomDetail> GetBomView_Cost(string itemCode, DateTime effDate)
        {
            Item item = itemMgr.LoadItem(itemCode);
            string bomCode = item.Bom != null ? item.Bom.Code : item.Code;
            IList<BomDetail> bomDetailList = this.GetFlatBomDetail(bomCode, effDate);
            IList<BomDetail> bomViewList = new List<BomDetail>();
            IList<BomDetail> costBomViewList = new List<BomDetail>();
            bomViewList = this.GetAllBomDetailTree(bomDetailList, effDate);
            bomViewList = this.GetNoOverloadBomDetail(bomViewList);
            if (bomViewList != null && bomViewList.Count > 0)
            {
                bomViewList = this.GetCostBomDetail(bomViewList);
                Bom bom = bomMgr.LoadBom(bomCode);
                foreach (BomDetail bomDetail in bomViewList)
                {
                    BomDetail costBomDetail = new BomDetail();
                    CloneHelper.CopyProperty(bomDetail, costBomDetail, BomCompDetail);
                    costBomDetail.Bom = bom;

                    costBomViewList.Add(costBomDetail);
                }
            }

            return this.GetNoOverloadBomDetail(costBomViewList);
        }

        [Transaction(TransactionMode.Unspecified)]
        public BomDetail GetDefaultBomDetailForAbstractItem(Item abstractItem, Routing routing, DateTime effDate)
        {
            return GetDefaultBomDetailForAbstractItem(abstractItem, routing, effDate, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public BomDetail GetDefaultBomDetailForAbstractItem(string abstractItemCode, Routing routing, DateTime effDate)
        {
            return GetDefaultBomDetailForAbstractItem(abstractItemCode, routing, effDate, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public BomDetail GetDefaultBomDetailForAbstractItem(Item abstractItem, Routing routing, DateTime effDate, Location defaultLocationFrom)
        {
            string bomCode = this.bomMgr.FindBomCode(abstractItem);
            IList<BomDetail> bomDetailList = this.GetNextLevelBomDetail(bomCode, effDate);
            if (bomDetailList != null && bomDetailList.Count > 0)
            {
                bomDetailList = IListHelper.Sort<BomDetail>(bomDetailList, "Priority"); //����Priority��������

                foreach (BomDetail bomDetail in bomDetailList)
                {
                    #region ��Դ��λ�����߼�BomDetail-->RoutingDetail-->defaultLocationFrom
                    //defaultLocationFrom = FlowDetail-->Flow
                    Location bomLocation = bomDetail.Location;

                    if (bomLocation == null)
                    {
                        RoutingDetail routingDetail = routingDetailMgr.LoadRoutingDetail(routing, bomDetail.Operation, bomDetail.Reference);
                        if (routingDetail != null)
                        {
                            if (bomLocation == null)
                            {
                                bomLocation = routingDetail.Location;
                            }
                        }
                    }

                    if (bomLocation == null)
                    {
                        bomLocation = defaultLocationFrom;
                    }
                    #endregion

                    //���û���ҵ���λ��ֱ��������һ��bomDetail
                    if (bomLocation != null)
                    {
                        if (!bomLocation.AllowNegativeInventory)
                        {
                            //���������
                            //todo �����
                            throw new NotImplementedException();
                        }
                        else
                        {
                            //������棬ֱ�ӷ���
                            return bomDetail;
                        }
                    }
                }
            }

            return null;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<BomDetail> GetBomDetailListForAbstractItem(Item abstractItem, Routing routing, DateTime effDate, Location defaultLocationFrom)
        {
            string bomCode = this.bomMgr.FindBomCode(abstractItem);
            IList<BomDetail> bomDetailList = this.GetNextLevelBomDetail(bomCode, effDate);
            if (bomDetailList != null && bomDetailList.Count > 0)
            {
                bomDetailList = IListHelper.Sort<BomDetail>(bomDetailList, "Priority"); //����Priority��������

                foreach (BomDetail bomDetail in bomDetailList)
                {
                    #region ��Դ��λ�����߼�BomDetail-->RoutingDetail-->defaultLocationFrom
                    //defaultLocationFrom = FlowDetail-->Flow
                    Location bomLocation = bomDetail.Location;

                    if (bomLocation == null)
                    {
                        RoutingDetail routingDetail = routingDetailMgr.LoadRoutingDetail(routing, bomDetail.Operation, bomDetail.Reference);
                        if (routingDetail != null)
                        {
                            if (bomLocation == null)
                            {
                                bomLocation = routingDetail.Location;
                            }
                        }
                    }

                    if (bomLocation == null)
                    {
                        bomLocation = defaultLocationFrom;
                    }
                    bomDetail.Location = bomLocation;
                    #endregion
                }
            }

            return bomDetailList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<BomDetail> GetBomDetailListForAbstractItem(string abstractItemCode, Routing routing, DateTime effDate, Location defaultLocationFrom)
        {
            Item abstractItem = this.itemMgr.LoadItem(abstractItemCode);
            return GetBomDetailListForAbstractItem(abstractItem, routing, effDate, defaultLocationFrom);
        }

        [Transaction(TransactionMode.Unspecified)]
        public BomDetail GetDefaultBomDetailForAbstractItem(string abstractItemCode, Routing routing, DateTime effDate, Location defaultLocationFrom)
        {
            Item abstractItem = this.itemMgr.LoadItem(abstractItemCode);
            return GetDefaultBomDetailForAbstractItem(abstractItem, routing, effDate, defaultLocationFrom);
        }

        [Transaction(TransactionMode.Unspecified)]
        public BomDetail GetDefaultBomDetailForAbstractItem(Item abstractItem, string routingCode, DateTime effDate)
        {
            return GetDefaultBomDetailForAbstractItem(abstractItem, routingCode, effDate, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public BomDetail GetDefaultBomDetailForAbstractItem(string abstractItemCode, string routingCode, DateTime effDate)
        {
            return GetDefaultBomDetailForAbstractItem(abstractItemCode, routingCode, effDate, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public BomDetail GetDefaultBomDetailForAbstractItem(Item abstractItem, string routingCode, DateTime effDate, Location defaultLocationFrom)
        {
            Routing routing = this.routingMgr.LoadRouting(routingCode);
            return GetDefaultBomDetailForAbstractItem(abstractItem, routing, effDate, defaultLocationFrom);
        }

        [Transaction(TransactionMode.Unspecified)]
        public BomDetail GetDefaultBomDetailForAbstractItem(string abstractItemCode, string routingCode, DateTime effDate, Location defaultLocationFrom)
        {
            Item abstractItem = this.itemMgr.LoadItem(abstractItemCode);
            Routing routing = this.routingMgr.LoadRouting(routingCode);
            return GetDefaultBomDetailForAbstractItem(abstractItem, routing, effDate, defaultLocationFrom);
        }


        [Transaction(TransactionMode.Unspecified)]
        public BomDetail LoadBomDetail(string bomCode, string itemCode, string reference, DateTime date)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(BomDetail));
            criteria.Add(Expression.Eq("Bom.Code", bomCode));
            criteria.Add(Expression.Eq("Item.Code", itemCode));
            criteria.Add(Expression.Eq("Reference", reference));
            criteria.Add(Expression.Le("StartDate", date));
            criteria.Add(Expression.Or(Expression.IsNull("EndDate"), Expression.Ge("EndDate", date)));
            criteria.AddOrder(Order.Desc("StartDate"));

            IList<BomDetail> bomDetailList = criterialMgr.FindAll<BomDetail>(criteria);
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
        public BomDetail LoadBomDetail(string bomCode, string itemCode, string reference)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(BomDetail));
            criteria.Add(Expression.Eq("Bom.Code", bomCode));
            criteria.Add(Expression.Eq("Item.Code", itemCode));
            criteria.Add(Expression.Eq("Reference", reference));
            criteria.AddOrder(Order.Desc("StartDate"));

            IList<BomDetail> bomDetailList = criterialMgr.FindAll<BomDetail>(criteria);
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
        public BomDetail GetBomDetail(string bomCode, string itemCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(BomDetail));
            criteria.Add(Expression.Eq("Bom.Code", bomCode));
            criteria.Add(Expression.Eq("Item.Code", itemCode));
            criteria.AddOrder(Order.Desc("StartDate"));

            IList<BomDetail> bomDetailList = criterialMgr.FindAll<BomDetail>(criteria);
            if (bomDetailList != null && bomDetailList.Count > 0)
            {
                return bomDetailList[0];
            }
            else
            {
                return null;
            }
        }
        #endregion Customized Methods



        #region Private Methods
        private void ProcessCurrentBomDetail(IList<BomDetail> flatBomDetailList, BomDetail currentBomDetail, DateTime efftiveDate)
        {
            if (currentBomDetail.StructureType == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_N) //��ͨ�ṹ(N)
            {
                ProcessCurrentBomDetailByItemType(flatBomDetailList, currentBomDetail, efftiveDate);
            }
            else if (currentBomDetail.StructureType == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_O) //ѡ���(O)
            {
                currentBomDetail.OptionalItemGroup = currentBomDetail.Bom.Code;   //Ĭ����BomCode��Ϊѡ��������
                ProcessCurrentBomDetailByItemType(flatBomDetailList, currentBomDetail, efftiveDate);
            }
            else if (currentBomDetail.StructureType == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_X) //��ṹ(X)
            {
                //�������ṹ(X)�������Լ��ӵ����ر���������·ֽ�
                NestingGetNextLevelBomDetail(flatBomDetailList, currentBomDetail, efftiveDate);
            }
            else
            {
                throw new TechnicalException("no such kind fo bomdetail structure type " + currentBomDetail.StructureType);
            }
        }

        private void ProcessCurrentBomDetailByItemType(IList<BomDetail> flatBomDetailList, BomDetail currentBomDetail, DateTime efftiveDate)
        {
            if (currentBomDetail.Item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_X)
            {
                //����������(X)���������·ֽ�
                NestingGetNextLevelBomDetail(flatBomDetailList, currentBomDetail, efftiveDate);
            }
            else if (currentBomDetail.Item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_A)
            {
                //todo ������費��Ҫ�ֽ⣿�Ƿ���������ϻس�ʱ��ָ����
                flatBomDetailList.Add(currentBomDetail);
            }
            else if (currentBomDetail.Item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
            {
                //K���͵�Item���ܳ�����Bom�ṹ��
                throw new BusinessErrorException("Bom.Error.ItemTypeKInBom", currentBomDetail.Bom.Code);

                //������������·ֽ�
                //NestingGetNextLevelBomDetail(flatBomDetailList, currentBomDetail, efftiveDate);
            }
            else
            {
                //ֱ�Ӽ��뵽flatBomDetailList
                flatBomDetailList.Add(currentBomDetail);
            }
        }

        private void NestingGetNextLevelBomDetail(IList<BomDetail> flatBomDetailList, BomDetail currentBomDetail, DateTime efftiveDate)
        {
            string nextLevelBomCode = this.bomMgr.FindBomCode(currentBomDetail.Item);
            IList<BomDetail> nextBomDetailList = this.GetNextLevelBomDetail(nextLevelBomCode, efftiveDate);

            if (nextBomDetailList != null && nextBomDetailList.Count > 0)
            {
                foreach (BomDetail nextBomDetail in nextBomDetailList)
                {
                    if (nextBomDetail.Item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
                    {
                        //K���͵�Item���ܳ�����Bom�ṹ��
                        throw new BusinessErrorException("Bom.Error.ItemTypeKInBom", nextBomDetail.Bom.Code);
                    }

                    //��ǰ�Ӽ���Uom���²�Bom��Uom��ƥ�䣬��Ҫ����λת��
                    if (currentBomDetail.Uom.Code.ToUpper() != nextBomDetail.Bom.Uom.Code.ToUpper())
                    {
                        //��λ����
                        nextBomDetail.CalculatedQty = uomConversionMgr.ConvertUomQty(currentBomDetail.Item, currentBomDetail.Uom, 1, nextBomDetail.Bom.Uom)
                            * currentBomDetail.CalculatedQty * nextBomDetail.RateQty * (1 + nextBomDetail.ScrapPercentage);
                    }
                    else
                    {
                        nextBomDetail.CalculatedQty = nextBomDetail.RateQty * currentBomDetail.CalculatedQty * (1 + nextBomDetail.ScrapPercentage);
                    }

                    nextBomDetail.OptionalItemGroup = currentBomDetail.OptionalItemGroup;

                    ProcessCurrentBomDetail(flatBomDetailList, nextBomDetail, efftiveDate);
                }
            }
            else
            {
                throw new BusinessErrorException("Bom.Error.NotFoundForItem", currentBomDetail.Item.Code);
            }
        }

        private IList<BomDetail> GetNoOverloadBomDetail(IList<BomDetail> bomDetailList)
        {
            //����BomCode��ItemCode��Operation��Reference��ͬ��BomDetail��ֻȡStartDate���ġ�
            if (bomDetailList != null && bomDetailList.Count > 0)
            {
                IList<BomDetail> noOverloadBomDetailList = new List<BomDetail>();
                foreach (BomDetail bomDetail in bomDetailList)
                {
                    int overloadIndex = -1;
                    for (int i = 0; i < noOverloadBomDetailList.Count; i++)
                    {
                        //�ж�BomCode��ItemCode��Operation��Reference�Ƿ���ͬ
                        if (noOverloadBomDetailList[i].Bom.Code == bomDetail.Bom.Code
                            && noOverloadBomDetailList[i].Item.Code == bomDetail.Item.Code
                            && noOverloadBomDetailList[i].Operation == bomDetail.Operation
                            && noOverloadBomDetailList[i].Reference == bomDetail.Reference)
                        {
                            //������ͬ�ģ���¼λ�á�
                            overloadIndex = i;
                            break;
                        }
                    }

                    if (overloadIndex == -1)
                    {
                        //û����ͬ�ļ�¼��ֱ�Ӱ�BomDetail���뷵���б�
                        noOverloadBomDetailList.Add(bomDetail);
                    }
                    else
                    {
                        //����ͬ�ļ�¼���ж�bomDetail.StartDate�ͽ�����еĴ�
                        if (noOverloadBomDetailList[overloadIndex].StartDate < bomDetail.StartDate)
                        {
                            //bomDetail.StartDate���ڽ�����еģ��滻�����
                            noOverloadBomDetailList[overloadIndex] = bomDetail;
                        }
                    }
                }
                return noOverloadBomDetailList;
            }
            else
            {
                return null;
            }
        }

        private IList<BomDetail> GetAllBomDetailTree(IList<BomDetail> treeBomDetailList, DateTime effDate)
        {
            IList<BomDetail> bomDetailList = new List<BomDetail>();
            foreach (BomDetail bomDetail in treeBomDetailList)
            {
                bomDetail.CalculatedQty = bomDetail.RateQty * (1 + bomDetail.ScrapPercentage);
                bomDetailList.Add(bomDetail);

                IList<BomDetail> tempBomDetailList = new List<BomDetail>();
                string nextLevelBomCode = (bomDetail.Item.Bom != null ? bomDetail.Item.Bom.Code : bomDetail.Item.Code);
                tempBomDetailList = this.GetNextLevelBomDetail(nextLevelBomCode, effDate);
                if (tempBomDetailList != null && tempBomDetailList.Count > 0)
                {
                    IList<BomDetail> nextBomDetailList = new List<BomDetail>();
                    nextBomDetailList = this.GetAllBomDetailTree(tempBomDetailList, effDate);
                    foreach (BomDetail bd in nextBomDetailList)
                    {
                        bd.CalculatedQty = bd.RateQty * (1 + bd.ScrapPercentage);
                        bomDetailList.Add(bd);
                    }
                }
            }

            return bomDetailList;
        }

        private IList<BomDetail> GetCostBomDetail(IList<BomDetail> bomDetailList)
        {
            IList<BomDetail> costBomDetailList = new List<BomDetail>();
            foreach (BomDetail bomDetail in bomDetailList)
            {
                bool isExist = false;
                int index = bomDetailList.IndexOf(bomDetail);
                foreach (BomDetail bd in bomDetailList)
                {
                    int bdIndex = bomDetailList.IndexOf(bd);
                    if (bdIndex <= index)
                    {
                        continue;
                    }

                    string nextLevelBomCode = (bomDetail.Item.Bom != null ? bomDetail.Item.Bom.Code : bomDetail.Item.Code);
                    if (nextLevelBomCode == bd.Bom.Code)
                    {
                        isExist = true;
                        bomDetailList[bdIndex].CalculatedQty = bomDetail.CalculatedQty * bd.CalculatedQty;
                        break;
                    }
                }

                if (!isExist)
                {
                    costBomDetailList.Add(bomDetail);
                }
            }

            return this.GetNoOverloadCostBomDetail(costBomDetailList);
        }

        private IList<BomDetail> GetNoOverloadCostBomDetail(IList<BomDetail> bomDetailList)
        {
            IList<BomDetail> noOverloadBomDetailList = new List<BomDetail>();
            //����ItemCode,UOM��ͬ��BomDetail,���ۼ�
            if (bomDetailList != null && bomDetailList.Count > 0)
            {
                foreach (BomDetail bomDetail in bomDetailList)
                {
                    int overloadIndex = -1;
                    for (int i = 0; i < noOverloadBomDetailList.Count; i++)
                    {
                        //�ж�BomCode��ItemCode��Operation��Reference�Ƿ���ͬ
                        if (noOverloadBomDetailList[i].Item.Code == bomDetail.Item.Code
                            && noOverloadBomDetailList[i].Uom.Code == bomDetail.Uom.Code)
                        {
                            //������ͬ�ģ���¼λ�á�
                            overloadIndex = i;
                            break;
                        }
                    }

                    if (overloadIndex == -1)
                    {
                        //û����ͬ�ļ�¼��ֱ�Ӱ�BomDetail���뷵���б�
                        noOverloadBomDetailList.Add(bomDetail);
                    }
                    else
                    {
                        //����ͬ�ļ�¼�ۼ�
                        noOverloadBomDetailList[overloadIndex].CalculatedQty += bomDetail.CalculatedQty;
                    }
                }
            }

            return noOverloadBomDetailList;
        }
        
        #endregion
    }
}