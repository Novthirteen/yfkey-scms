using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Utility;
using NHibernate.Expression;
using NPOI.HSSF.UserModel;
using System.IO;


//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class HuMgr : HuBaseMgr, IHuMgr
    {
        private ICriteriaMgr criteriaMgr;
        private INumberControlMgr numberControlMgr;
        private IItemMgr itemMgr;
        private IUomMgr uomMgr;
        private IUomConversionMgr uomConversionMgr;
        private IUserMgr userMgr;


        public HuMgr(IHuDao entityDao,
            ICriteriaMgr criteriaMgr,
            INumberControlMgr numberControlMgr,
            IItemMgr itemMgr,
            IUomMgr uomMgr,
            IUomConversionMgr uomConversionMgr,
            IUserMgr userMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.numberControlMgr = numberControlMgr;
            this.itemMgr = itemMgr;
            this.uomMgr = uomMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.userMgr = userMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Requires)]
        public IList<Hu> CreateHu(IList<FlowDetail> flowDetailList, User user)
        {
            return CreateHu(flowDetailList, user, null);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<Hu> CreateHu(IList<FlowDetail> flowDetailList, User user, string idMark)
        {
            return CreateHu(flowDetailList, user, idMark, null);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<Hu> CreateHu(IList<FlowDetail> flowDetailList, User user, string idMark, string packageType)
        {
            if (flowDetailList != null && flowDetailList.Count > 0)
            {
                IList<Hu> huList = new List<Hu>();
                int? huLotSize = null;
                foreach (FlowDetail flowDetail in flowDetailList)
                {
                    Flow flow = flowDetail.Flow;
                    if (packageType == BusinessConstants.CODE_MASTER_PACKAGETYPE_INNER)
                    {
                        huLotSize = Convert.ToInt32(flowDetail.UnitCount);
                    }
                    else 
                    {
                        huLotSize = flowDetail.HuLotSize;
                    }

                    if (flow.Type != BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
                    {
                        IListHelper.AddRange<Hu>(huList,
                        CreateHu(flowDetail.Item, flowDetail.OrderedQty, flowDetail.HuLotNo, flowDetail.Uom, flowDetail.UnitCount, huLotSize,
                            null, null, null, flowDetail.Flow.PartyFrom, BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1, user, flowDetail, null, flowDetail.ItemVersion, idMark,flowDetail.CustomerItemCode));
                    }
                    else
                    {
                        IListHelper.AddRange<Hu>(huList,
                        CreateHu(flowDetail.Item, flowDetail.OrderedQty, flowDetail.HuLotNo, flowDetail.Uom, flowDetail.UnitCount, huLotSize,
                            null, null, null, flowDetail.Flow.PartyFrom, BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1, user, flowDetail, flowDetail.HuShiftCode, flowDetail.ItemVersion, idMark,flowDetail.CustomerItemCode));
                    }
                }

                return huList;
            }

            return null;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<Hu> CreateHu(IList<OrderDetail> orderDetailList, User user)
        {
            return CreateHu(orderDetailList, user, null);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<Hu> CreateHu(IList<OrderDetail> orderDetailList, User user, string idMark)
        {
            if (orderDetailList != null && orderDetailList.Count > 0)
            {
                IList<Hu> huList = new List<Hu>();
                foreach (OrderDetail orderDetail in orderDetailList)
                {
                  
                    string lotNo = orderDetail.HuLotNo != null && orderDetail.HuLotNo.Trim().Length != 0 ? orderDetail.HuLotNo.Trim() : LotNoHelper.GenerateLotNo(orderDetail.OrderHead.WindowTime);
                    IListHelper.AddRange<Hu>(huList,
                        CreateHu(orderDetail.Item, orderDetail.OrderedQty, lotNo, orderDetail.Uom, orderDetail.UnitCount, orderDetail.HuLotSize,
                        null, null, null, orderDetail.OrderHead.PartyFrom, BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1, user, orderDetail, null, orderDetail.ItemVersion, idMark,orderDetail.CustomerItemCode));
                }

                return huList;
            }

            return null;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<Hu> CreateHu(IList<OrderDetail> orderDetailList, User user, string idMark,string packageType)
        {
            if (orderDetailList != null && orderDetailList.Count > 0)
            {
                IList<Hu> huList = new List<Hu>();
                int? huLotSize = null;
                foreach (OrderDetail orderDetail in orderDetailList)
                {
                    if (packageType == BusinessConstants.CODE_MASTER_PACKAGETYPE_INNER)
                    {
                        huLotSize = Convert.ToInt32(orderDetail.UnitCount);
                    }
                    else
                    {
                        huLotSize = orderDetail.HuLotSize;
                    }
                    string lotNo = orderDetail.HuLotNo != null && orderDetail.HuLotNo.Trim().Length != 0 ? orderDetail.HuLotNo.Trim() : LotNoHelper.GenerateLotNo(orderDetail.OrderHead.WindowTime);
                    IListHelper.AddRange<Hu>(huList,
                        CreateHu(orderDetail.Item, orderDetail.OrderedQty, lotNo, orderDetail.Uom, orderDetail.UnitCount, huLotSize,
                        null, null, null, orderDetail.OrderHead.PartyFrom, BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1, user, orderDetail, null, orderDetail.ItemVersion, idMark,orderDetail.CustomerItemCode));
                }

                return huList;
            }

            return null;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<Hu> CreateHu(OrderHead orderHead, User user)
        {
            if (orderHead.OrderDetails != null && orderHead.OrderDetails.Count > 0)
            {
                IList<Hu> huList = new List<Hu>();
                foreach (OrderDetail orderDetail in orderHead.OrderDetails)
                {
                    IListHelper.AddRange<Hu>(huList,
                        CreateHu(orderDetail.Item, orderDetail.OrderedQty, orderDetail.HuLotNo, orderDetail.Uom, orderDetail.UnitCount, orderDetail.HuLotSize,
                        null, null, null, orderDetail.OrderHead.PartyFrom, BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1, user, orderDetail, null, orderDetail.ItemVersion, null,orderDetail.CustomerItemCode));
                }

                return huList;
            }

            return null;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<Hu> CreateHu(InProcessLocationDetail inProcessLocationDetail, User user)
        {
            if (inProcessLocationDetail.HuId != null)
            {
                throw new TechnicalException("HuId already exist.");
            }

            OrderLocationTransaction orderLocationTransaction = inProcessLocationDetail.OrderLocationTransaction;
            OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
            OrderHead orderHead = orderDetail.OrderHead;

            return CreateHu(orderLocationTransaction.Item, inProcessLocationDetail.Qty, inProcessLocationDetail.LotNo, orderDetail.Uom, orderDetail.UnitCount, orderDetail.HuLotSize,
                    orderHead.OrderNo, null, null, orderHead.PartyFrom, BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1, user, inProcessLocationDetail, null, orderLocationTransaction.ItemVersion, null,orderDetail.CustomerItemCode);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<Hu> CreateHu(ReceiptDetail receiptDetail, User user)
        {
            if (receiptDetail.HuId != null)
            {
                throw new TechnicalException("HuId already exist.");
            }

            OrderLocationTransaction orderLocationTransaction = receiptDetail.OrderLocationTransaction;
            OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
            OrderHead orderHead = orderDetail.OrderHead;
            IList<Hu> huList = new List<Hu>();
            string lotNo = null;

            if (orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                lotNo = receiptDetail.LotNo;
            }

            #region 为正品创建Hu
            if (receiptDetail.ReceivedQty.HasValue && receiptDetail.ReceivedQty != 0)
            {
                huList = CreateHu(orderLocationTransaction.Item, receiptDetail.ReceivedQty.Value, lotNo, orderDetail.Uom, orderDetail.UnitCount, orderDetail.HuLotSize,
                    orderHead.OrderNo, receiptDetail.Receipt.ReceiptNo, null, orderHead.PartyFrom, BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1, user, receiptDetail, null, orderLocationTransaction.ItemVersion, null,orderDetail.CustomerItemCode);
            }
            #endregion

            #region 为次品创建Hu
            //if (receiptDetail.RejectedQty.HasValue && receiptDetail.RejectedQty != 0)
            //{
            //    IList<Hu> rejHuList = CreateHu(orderLocationTransaction.Item, receiptDetail.RejectedQty.Value, lotNo, orderDetail.Uom, orderDetail.UnitCount, orderDetail.HuLotSize,
            //        orderHead.OrderNo, receiptDetail.Receipt.ReceiptNo, null, orderHead.PartyFrom, BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_2, user, receiptDetail, null);
            //    IListHelper.AddRange<Hu>(huList, rejHuList);
            //}
            #endregion

            return huList;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<Hu> CloneHu(Hu hu, decimal uintCount, int count, User user)
        {
            return CloneHu(hu.HuId, uintCount, count, user);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<Hu> CloneHu(string huId, decimal uintCount, int count, User user)
        {
            IList<Hu> huList = new List<Hu>();
            Hu oldHu = this.LoadHu(huId);
            DateTime dateTimeNow = DateTime.Now;

            int i = 0;
            while (i < count)
            {
                Hu huTemplate = CloneHelper.DeepClone<Hu>(oldHu);
                huTemplate.LotSize = uintCount;
                huTemplate.UnitCount = uintCount;
                huTemplate.Qty = uintCount;
                #region 百利得定制条码生成规则，如果是以零件号开头的为原材料条码，不是以零件号开头的为成品条码
                if (oldHu.HuId.StartsWith(oldHu.Item.Code))
                {
                    huTemplate.HuId = this.numberControlMgr.CloneRMHuId(oldHu.HuId, uintCount);
                }
                else
                {
                    huTemplate.HuId = this.numberControlMgr.CloneFGHuId(oldHu.HuId);
                }
                #endregion
                huTemplate.CreateDate = dateTimeNow;
                huTemplate.CreateUser = user;
                huTemplate.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CREATE;

                this.CreateHu(huTemplate);
                huList.Add(huTemplate);
                i++;
            }

            return huList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public Hu CheckAndLoadHu(string huId)
        {
            Hu hu = this.LoadHu(huId);
            if (hu == null)
            {
                throw new BusinessErrorException("Hu.Error.HuIdNotExist", huId);
            }

            return hu;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Hu> GetHuList(string userCode, int firstRow, int maxRows, params string[] orderTypes)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Hu));

            //criteria.CreateAlias("OrderNo","order");
            criteria.Add(Expression.Eq("CreateUser.Code", userCode));
            //criteria.Add(Expression.In("order.OrderType", orderTypes));
            criteria.Add(Expression.Ge("CreateDate", DateTime.Today));//直显示当天的条码
            criteria.AddOrder(Order.Desc("CreateDate"));
            IList<Hu> huList = criteriaMgr.FindAll<Hu>(criteria, firstRow, maxRows);
            if (huList.Count > 0)
            {
                return huList;
            }
            return null;
        }

        #endregion Customized Methods

        #region Private Methods
        private IList<Hu> CreateHu(Item item, decimal qty, string lotNo, Uom uom, decimal unitCount, int? huLotSize,
            string orderNo, string recNo, DateTime? manufactureDate, Party manufactureParty, string qualityLevel, User user, Object obj, string shiftCode, string itemVersion, string idMark,string customerItemCode)
        {
            IList<Hu> huList = new List<Hu>();

            #region 根据Hu批量创建Hu
            decimal remainHuQty = qty;                                        //剩余量
            decimal currentHuQty = GetNextHuQty(ref remainHuQty, huLotSize);  //本次量
            DateTime dateTimeNow = DateTime.Now;

            while (currentHuQty > 0)
            {
                #region 创建Hu
                Hu hu = new Hu();
                #region HuId生成
                if (obj.GetType() == typeof(FlowDetail))
                {
                    FlowDetail flowDetail = (FlowDetail)obj;
                    if (flowDetail.Flow.Type != BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
                    {
                        lotNo = lotNo != null ? lotNo : LotNoHelper.GenerateLotNo();
                        hu.HuId = this.numberControlMgr.GenerateRMHuId(flowDetail, lotNo, currentHuQty, idMark);
                    }
                    else
                    {
                        if (shiftCode != null && shiftCode.Trim().Length != 0)
                        {
                            lotNo = lotNo != null ? lotNo : LotNoHelper.GenerateLotNo();
                            hu.HuId = this.numberControlMgr.GenerateFGHuId(flowDetail, shiftCode, currentHuQty, idMark);
                        }
                        else
                        {
                            throw new TechnicalException("ShiftCode can't be null when create fg huId by flowdetail");
                        }
                    }
                }
                else if (obj.GetType() == typeof(OrderDetail))
                {
                    OrderDetail orderDetail = (OrderDetail)obj;
                    if (orderDetail.OrderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                    {
                        lotNo = lotNo != null ? lotNo : LotNoHelper.GenerateLotNo(orderDetail.OrderHead.WindowTime);
                        hu.HuId = this.numberControlMgr.GenerateRMHuId(orderDetail, lotNo, currentHuQty, idMark);
                    }
                    else
                    {
                        lotNo = lotNo != null ? lotNo : LotNoHelper.GenerateLotNo(orderDetail.OrderHead.WindowTime);
                        hu.HuId = this.numberControlMgr.GenerateFGHuId(orderDetail, currentHuQty, idMark);
                    }
                }
                else if (obj.GetType() == typeof(InProcessLocationDetail))
                {
                    InProcessLocationDetail inProcessLocationDetail = (InProcessLocationDetail)obj;
                    if (inProcessLocationDetail.OrderLocationTransaction.OrderDetail.OrderHead.Type
                        != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                    {
                        lotNo = lotNo != null ? lotNo : LotNoHelper.GenerateLotNo();
                        hu.HuId = this.numberControlMgr.GenerateRMHuId(inProcessLocationDetail, lotNo, currentHuQty);
                    }
                    else
                    {
                        throw new TechnicalException("Can't create fg huid by InProcessLocationDetail");
                    }
                }
                else if (obj.GetType() == typeof(ReceiptDetail))
                {
                    ReceiptDetail receiptDetail = (ReceiptDetail)obj;
                    if (receiptDetail.OrderLocationTransaction.OrderDetail.OrderHead.Type
                        != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                    {
                        lotNo = lotNo != null ? lotNo : LotNoHelper.GenerateLotNo();
                        hu.HuId = this.numberControlMgr.GenerateRMHuId(receiptDetail, lotNo, currentHuQty);
                    }
                    else
                    {
                        lotNo = lotNo != null ? lotNo : LotNoHelper.GenerateLotNo();
                        hu.HuId = this.numberControlMgr.GenerateFGHuId(receiptDetail, currentHuQty);
                    }
                }
                else
                {
                    throw new TechnicalException("Parameter obj only accept type: FlowDetail, OrderDetail, InProcessLocationDetail, ReceiptDetail");
                }
                #endregion
                hu.Item = item;
                hu.OrderNo = orderNo;
                hu.ReceiptNo = recNo;
                hu.Uom = uom;   //用订单单位
                hu.UnitCount = unitCount;
                #region 单位用量
                //如果是OrderDetail，应该等于inOrderLocationTransaction.UnitQty，现在暂时直接用单位换算
                if (item.Uom.Code != uom.Code)
                {
                    hu.UnitQty = this.uomConversionMgr.ConvertUomQty(item, uom, 1, item.Uom);   //单位用量
                }
                else
                {
                    hu.UnitQty = 1;
                }
                #endregion
                hu.QualityLevel = qualityLevel;
                hu.Qty = currentHuQty;
                hu.LotNo = lotNo;
                hu.ManufactureDate = manufactureDate.HasValue ? manufactureDate.Value : LotNoHelper.ResolveLotNo(hu.LotNo);
                hu.ManufactureParty = manufactureParty;
                hu.CreateUser = user;
                hu.CreateDate = dateTimeNow;
                hu.LotSize = huLotSize.HasValue ? huLotSize.Value : currentHuQty;
                hu.Version = itemVersion;
                hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CREATE;
                hu.CustomerItemCode = customerItemCode;

                this.CreateHu(hu);
                #endregion

                huList.Add(hu);
                currentHuQty = GetNextHuQty(ref remainHuQty, huLotSize);
            }
            #endregion

            return huList;
        }

        private decimal GetNextHuQty(ref decimal remainHuQty, int? huLotSize)
        {
            #region 设置下次Hu批量
            decimal currentHuQty = 0;
            if (huLotSize.HasValue)
            {
                if (remainHuQty - huLotSize.Value > 0)
                {
                    remainHuQty -= huLotSize.Value;
                    currentHuQty = huLotSize.Value;
                }
                else
                {
                    currentHuQty = remainHuQty;
                    remainHuQty = 0;
                }
            }
            else
            {
                currentHuQty = remainHuQty;
                remainHuQty = 0;
            }

            return currentHuQty;
            #endregion
        }

        
        #endregion
    }
}