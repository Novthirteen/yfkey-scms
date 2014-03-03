using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using com.Sconit.Utility;
using System.Linq;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class RepackMgr : RepackBaseMgr, IRepackMgr
    {
        private ILocationLotDetailMgr locationLotDetailMgr;
        private IHuMgr huMgr;
        private IStorageBinMgr storageBinMgr;
        private ILocationMgr locationMgr;
        private INumberControlMgr numberControlMgr;
        private IRepackDetailMgr repackDetailMgr;
        private IUserMgr userMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;
        public RepackMgr(IRepackDao entityDao,
            ILocationLotDetailMgr locationLotDetailMgr,
            IHuMgr huMgr,
            IStorageBinMgr storageBinMgr,
            ILocationMgr locationMgr,
            INumberControlMgr numberControlMgr,
            IRepackDetailMgr repackDetailMgr,
            IUserMgr userMgr,
            IEntityPreferenceMgr entityPreferenceMgr)
            : base(entityDao)
        {
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.huMgr = huMgr;
            this.storageBinMgr = storageBinMgr;
            this.locationMgr = locationMgr;
            this.numberControlMgr = numberControlMgr;
            this.repackDetailMgr = repackDetailMgr;
            this.userMgr = userMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public Repack LoadRepack(String repackNo, bool includeDetail)
        {
            Repack repack = this.LoadRepack(repackNo);
            if (includeDetail && repack.RepackDetails != null && repack.RepackDetails.Count > 0)
            {

            }
            return repack;
        }

        [Transaction(TransactionMode.Requires)]
        public Repack CreateRepack(IList<RepackDetail> repackDetailList, User user)
        {
            return CreateRepack(repackDetailList, BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_REPACK, user);
        }

        [Transaction(TransactionMode.Requires)]
        public Repack CreateDevanning(IList<RepackDetail> repackDetailList, User user)
        {
            return CreateRepack(repackDetailList, BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_DEVANNING, user);
        }
        #endregion Customized Methods

        #region Private Methods
        private Repack CreateRepack(IList<RepackDetail> repackDetailList, string type, User user)
        {
            IList<RepackDetail> inRepackDetailList = new List<RepackDetail>();
            IList<RepackDetail> outRepackDetailList = new List<RepackDetail>();
            bool hasHu = false;
            #region 判断RepackDetailList是否为空
            if (repackDetailList != null && repackDetailList.Count > 0)
            {
                foreach (RepackDetail repackDetail in repackDetailList)
                {
                    if (repackDetail.Qty != 0)
                    {
                        if (repackDetail.IOType == BusinessConstants.IO_TYPE_IN)
                        {
                            inRepackDetailList.Add(repackDetail);
                        }
                        else if (repackDetail.IOType == BusinessConstants.IO_TYPE_OUT)
                        {
                            outRepackDetailList.Add(repackDetail);
                            if (!hasHu && repackDetail.Hu != null)
                            {
                                hasHu = true;
                            }
                        }
                        else
                        {
                            throw new TechnicalException("Invalid IO Type:" + repackDetail.IOType);
                        }
                    }
                }

                #region 翻箱的如果没有输出，将输入代码合并,生成一张新条码
                if (outRepackDetailList.Count == 0 && type == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_REPACK)
                {
                    Hu inHu = inRepackDetailList[0].Hu;
                    Hu outHu = new Hu();
                    CloneHelper.CopyProperty(inHu, outHu);
                    outHu.OrderNo = null;
                    outHu.ReceiptNo = null;
                    outHu.Location = null;
                    outHu.StorageBin = null;
                    outHu.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;

                    string repackShift = entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_REPACK_SHIFT).Value;

                    string prefix = inHu.HuId.Substring(0, inHu.HuId.Length - 4) + repackShift;
                    outHu.HuId =numberControlMgr.GenerateNumber(prefix, 3);
                    outHu.Qty = (from l in inRepackDetailList select l.Qty).Sum();
                    outHu.UnitCount = outHu.Qty;
                    outHu.LotSize = outHu.UnitCount;
                    outHu.PrintCount = 0;
                    huMgr.CreateHu(outHu);

                    RepackDetail outRepackDetail = new RepackDetail();
                    outRepackDetail.Hu = outHu;
                    outRepackDetail.IOType = BusinessConstants.IO_TYPE_OUT;
                    outRepackDetail.Qty = outHu.Qty;
                    outRepackDetail.itemCode = outHu.Item.Code;
                    outRepackDetailList.Add(outRepackDetail);
                }
                #endregion

                if (inRepackDetailList.Count == 0 || outRepackDetailList.Count == 0)
                {
                    throw new BusinessErrorException("MasterData.Inventory.Repack.Error.RepackDetailEmpty");
                }
                if (hasHu && type == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_DEVANNING && outRepackDetailList.Count < 2)
                {
                    throw new BusinessErrorException("MasterData.Inventory.Devanning.Error.DevanningDetailLessThanTwo");
                }
            }
            else
            {
                throw new BusinessErrorException("MasterData.Inventory.Repack.Error.RepackDetailEmpty");
            }
            #endregion

            #region 判断是否被拣货
            foreach (RepackDetail inRepackDetail in inRepackDetailList)
            {
                if (inRepackDetail.LocationLotDetail.Hu != null && this.locationMgr.IsHuOcuppyByPickList(inRepackDetail.LocationLotDetail.Hu.HuId))
                {
                    throw new BusinessErrorException("MasterData.Inventory.Repack.Error.HuOccupied", inRepackDetail.Hu.HuId);
                }
            }
            #endregion

            #region 判断翻箱后条码是否为新条码
            foreach (RepackDetail outRepackDetail in outRepackDetailList)
            {
                if (outRepackDetail.Hu != null && outRepackDetail.Hu.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                {
                    throw new BusinessErrorException("MasterData.Inventory.Repack.Error.HuStatusNotCreate", outRepackDetail.Hu.HuId);
                }
            }
            #endregion

            #region 检查In和Out明细数量是否匹配
            IDictionary<string, decimal> inItemQtyDic = new Dictionary<string, decimal>();
            Location location = null;

            #region 收集In数量
            foreach (RepackDetail inRepackDetail in inRepackDetailList)
            {
                LocationLotDetail inLocationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inRepackDetail.LocationLotDetail.Id);

                if (location == null)
                {
                    location = inLocationLotDetail.Location;

                    if (!user.HasPermission(inLocationLotDetail.Location.Region.Code))
                    {
                        throw new BusinessErrorException("MasterData.Inventory.Repack.Error.NoPermission", location.Code);
                    }
                }
                else if (location.Code != inLocationLotDetail.Location.Code)
                {
                    throw new BusinessErrorException("MasterData.Inventory.Repack.Error.InRepackDetailLocationNotEqual");
                }

                if (inItemQtyDic.ContainsKey(inLocationLotDetail.Item.Code))
                {
                    inItemQtyDic[inLocationLotDetail.Item.Code] += inRepackDetail.Qty;
                }
                else
                {
                    inItemQtyDic.Add(inLocationLotDetail.Item.Code, inRepackDetail.Qty);
                }
            }
            #endregion

            #region 收集Out数量
            IDictionary<string, decimal> outItemQtyDic = new Dictionary<string, decimal>();

            foreach (RepackDetail outRepackDetail in outRepackDetailList)
            {
                if (type == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_REPACK)
                {
                    if (outRepackDetail.Hu == null)
                    {
                        throw new BusinessErrorException("MasterData.Inventory.Repack.Error.HuIdIsEmpty");
                    }
                    else
                    {
                        if (outItemQtyDic.ContainsKey(outRepackDetail.Hu.Item.Code))
                        {
                            outItemQtyDic[outRepackDetail.Hu.Item.Code] += outRepackDetail.Qty;
                        }
                        else
                        {
                            outItemQtyDic.Add(outRepackDetail.Hu.Item.Code, outRepackDetail.Qty);
                        }
                    }
                }
                else if (type == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_DEVANNING)
                {
                    string itemCode = outRepackDetail.Hu != null ? outRepackDetail.Hu.Item.Code : outRepackDetail.itemCode;

                    if (itemCode == null)
                    {
                        throw new TechnicalException("ItemCode not specified.");
                    }

                    if (outItemQtyDic.ContainsKey(itemCode))
                    {
                        outItemQtyDic[itemCode] += outRepackDetail.Qty;
                    }
                    else
                    {
                        outItemQtyDic.Add(itemCode, outRepackDetail.Qty);
                    }
                }
                else
                {
                    throw new TechnicalException("Repack type: " + type + " is not valided.");
                }
            }
            #endregion

            #region 比较
            if (inItemQtyDic.Count != outItemQtyDic.Count)
            {
                throw new BusinessErrorException("MasterData.Inventory.Repack.Error.InOutQtyNotMatch");
            }

            foreach (string itemCode in inItemQtyDic.Keys)
            {
                if (outItemQtyDic.ContainsKey(itemCode))
                {
                    decimal inQty = inItemQtyDic[itemCode];
                    decimal outQty = outItemQtyDic[itemCode];

                    //是否自动创建剩余数量的记录
                    bool autoCreate = bool.Parse(entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_AUTO_CREATE_WHEN_DEAVING).Value);

                    #region 拆箱根据剩余数量得到剩余数量的条码
                    if (autoCreate && type == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_DEVANNING && inQty > outQty)
                    {
                        RepackDetail remainRepackDetail = CloneHelper.DeepClone(inRepackDetailList[0]);
                        remainRepackDetail.Qty = inQty - outQty;
                        remainRepackDetail.IOType = BusinessConstants.IO_TYPE_OUT;
                        outRepackDetailList.Add(remainRepackDetail);
                    }
                    #endregion

                    else if (inQty != outQty)
                    {
                        throw new BusinessErrorException("MasterData.Inventory.Repack.Error.InOutQtyNotMatch");
                    }
                }
                else
                {
                    throw new BusinessErrorException("MasterData.Inventory.Repack.Error.InOutItemNotMatch", itemCode);
                }
            }
            #endregion
            #endregion

            #region 创建翻箱单头
            Repack repack = new Repack();
            repack.RepackNo = this.numberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_REPACK);
            repack.CreateDate = DateTime.Now;
            repack.CreateUser = user;
            repack.Type = type;

            this.CreateRepack(repack);
            #endregion

            #region 创建翻箱单明细
            Int32? plannedBillId = null;  //拆箱传递PlannedBill
            foreach (RepackDetail inRepackDetail in inRepackDetailList)
            {
                //出库
                inRepackDetail.Repack = repack;
                this.locationMgr.InventoryRepackIn(inRepackDetail, user);

                this.repackDetailMgr.CreateRepackDetail(inRepackDetail);

                if (repack.Type == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_DEVANNING)
                {
                    plannedBillId = inRepackDetail.LocationLotDetail.IsConsignment ? inRepackDetail.LocationLotDetail.PlannedBill : null;
                }
            }

            foreach (RepackDetail outRepackDetail in outRepackDetailList)
            {
                //入库
                outRepackDetail.Repack = repack;
                InventoryTransaction inventoryTransaction = this.locationMgr.InventoryRepackOut(outRepackDetail, location, plannedBillId, user);
                outRepackDetail.LocationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransaction.LocationLotDetailId);

                this.repackDetailMgr.CreateRepackDetail(outRepackDetail);
            }
            #endregion

            return repack;
        }
        #endregion
    }
}