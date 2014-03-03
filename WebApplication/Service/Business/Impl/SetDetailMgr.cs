using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using Castle.Services.Transaction;
using com.Sconit.Entity.View;
using com.Sconit.Entity.Production;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.Distribution;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using System.Collections;

namespace com.Sconit.Service.Business.Impl
{
    public class SetDetailMgr : ISetDetailMgr
    {
        private IHuMgr huMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        private IUserMgr userMgr;
        private INumberControlMgr numberControlMgr;
        private IUomConversionMgr uomConversionMgr;
        private IItemMgr itemMgr;
        private IOrderDetailMgr orderDetailMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;
        private IOrderMgr orderMgr;
        private IFlowMgr flowMgr;
        private ILocationMgr locationMgr;
        private IProductLineInProcessLocationDetailMgr productLineInProcessLocationDetailMgr;
        private IRoutingDetailMgr routingDetailMgr;
        private IOrderHeadMgr orderHeadMgr;
        private IInProcessLocationMgr inProcessLocationMgr;
        private ICriteriaMgr criteriaMgr;

        public SetDetailMgr(
            IHuMgr huMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            IUserMgr userMgr,
            INumberControlMgr numberControlMgr,
            IUomConversionMgr uomConversionMgr,
            IItemMgr itemMgr,
            IOrderDetailMgr orderDetailMgr,
            IEntityPreferenceMgr entityPreferenceMgr,
            IOrderMgr orderMgr,
            IFlowMgr flowMgr,
            ILocationMgr locationMgr,
            IProductLineInProcessLocationDetailMgr productLineInProcessLocationDetailMgr,
            IRoutingDetailMgr routingDetailMgr,
            IOrderHeadMgr orderHeadMgr,
            IInProcessLocationMgr inProcessLocationMgr,
            ICriteriaMgr criteriaMgr
            )
        {
            this.huMgr = huMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.userMgr = userMgr;
            this.numberControlMgr = numberControlMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.itemMgr = itemMgr;
            this.orderDetailMgr = orderDetailMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
            this.orderMgr = orderMgr;
            this.flowMgr = flowMgr;
            this.locationMgr = locationMgr;
            this.productLineInProcessLocationDetailMgr = productLineInProcessLocationDetailMgr;
            this.routingDetailMgr = routingDetailMgr;
            this.orderHeadMgr = orderHeadMgr;
            this.inProcessLocationMgr = inProcessLocationMgr;
            this.criteriaMgr = criteriaMgr;
        }

        #region Public Method
        [Transaction(TransactionMode.Unspecified)]
        public void MatchReceive(Resolver resolver)
        {
            //参数解析
            string huId = resolver.Input;
            string pickBy = resolver.PickBy;

            //校验重复扫描
            if (this.CheckMatchHuScanExist(resolver))
            {
                throw new BusinessErrorException("Warehouse.Error.HuReScan", huId);
            }

            //校验并修正Hu
            Hu hu = ResolveHu(resolver.AntiResolveHu, huId, resolver.Transformers);

            //采购收货检查此Hu是否已经用过
            if (resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                && hu.Location != null)
            {
                throw new BusinessErrorException("Warehouse.Error.HuAlreadyExist", huId);
            }

            //检查发货时Hu是否在正确的库位上
            bool isMatch = false;
            if (pickBy == BusinessConstants.CODE_MASTER_PICKBY_HU)
            {
                isMatch = MatchByHu(resolver, hu);
                if (!isMatch && resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                && resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE)//采购收货 用lot再match一次
                {
                    isMatch = MatchByLot(resolver, hu);
                }
            }
            else if (pickBy == BusinessConstants.CODE_MASTER_PICKBY_LOTNO)
            {
                isMatch = MatchByLot(resolver, hu);
            }
            else
            {
                isMatch = MatchByItem(resolver, hu);
            }

            if (!isMatch)
            {
                throw new BusinessErrorException("Warehouse.HuMatch.NotMatch", huId);
            }

            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        /// <summary>
        /// ship 匹配 pickList匹配
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void MatchShip(Resolver resolver)
        {
            //参数解析
            string huId = resolver.Input;
            string pickBy = resolver.PickBy;

            //校验重复扫描
            if (this.CheckMatchHuScanExist(resolver))
            {
                throw new BusinessErrorException("Warehouse.Error.HuReScan", huId);
            }

            //校验并修正Hu
            Hu hu = ResolveHu(resolver.AntiResolveHu, huId, resolver.Transformers);

            //是否上架，没有上架不能发货
            if (resolver.IsPickFromBin && hu.StorageBin != null && hu.StorageBin.Trim() == string.Empty)
            {
                throw new BusinessErrorException("Common.Business.Error.NotInBin.CannotShip", huId);
            }

            LocationLotDetail locationLotDetail = null;
            //采购类型(供应商发货)不检查库存, 否则检查库存和权限
            if (resolver.OrderType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                //locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(huId, resolver.UserCode);
                //if (locationLotDetail != null && locationLotDetail.Hu != null)
                //{
                //    hu = locationLotDetail.Hu;
                //    hu.Qty = locationLotDetail.Qty / hu.UnitQty;
                //}
                if (hu.Status != BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_INVENTORY)
                {
                    throw new BusinessErrorException("Hu.Error.NoInventory", huId);
                }
                //todo库位匹配
            }

            bool isMatch = false;
            if (pickBy == BusinessConstants.CODE_MASTER_PICKBY_HU)
            {
                isMatch = MatchByHu(resolver, hu, locationLotDetail != null ? locationLotDetail.Id : 0);

                if (!isMatch && resolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                    && resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE)//采购收货 用lot再match一次
                {
                    isMatch = MatchByLot(resolver, hu, locationLotDetail != null ? locationLotDetail.Id : 0);
                }
            }
            else if (pickBy == BusinessConstants.CODE_MASTER_PICKBY_LOTNO)
            {
                isMatch = MatchByLot(resolver, hu, locationLotDetail != null ? locationLotDetail.Id : 0);
            }
            else
            {
                isMatch = MatchByItem(resolver, hu, locationLotDetail != null ? locationLotDetail.Id : 0);
            }

            if (!isMatch)
            {
                throw new BusinessErrorException("Warehouse.HuMatch.NotMatch", huId);
            }
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        /// <summary>
        /// 是否需要汇总
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void MatchInspet(Resolver resolver)
        {
            if (resolver.IsScanHu)
            {
                Hu hu = huMgr.CheckAndLoadHu(resolver.Input);
                this.CheckHuReScan(resolver);

                if (!this.MatchByHu(resolver, hu))
                {
                    throw new BusinessErrorException("Warehouse.HuMatch.NotMatch", resolver.Input);
                }
            }
            else
            {
                bool isMatch = false;
                if (resolver.Transformers != null)
                {
                    foreach (Transformer transformer in resolver.Transformers)
                    {
                        if (transformer.TransformerDetails != null && transformer.TransformerDetails.Count == 1)
                        {
                            foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                            {
                                if (transformerDetail.CurrentQty + transformerDetail.CurrentRejectQty > transformerDetail.Qty)
                                {
                                    transformerDetail.CurrentQty = 0;
                                    transformerDetail.CurrentRejectQty = 0;
                                    throw new BusinessErrorException("Warehouse.Inspect.ItemMatch.QtyExcceed", transformerDetail.ItemCode);
                                }
                                else
                                {
                                    transformerDetail.CurrentQty = transformerDetail.Qty;
                                    transformerDetail.CurrentRejectQty = 0;
                                }
                                if (transformerDetail.ItemCode.ToLower() == resolver.Input.ToLower().Trim())
                                {
                                    isMatch = true;
                                }
                            }
                        }
                    }
                }
                if (!isMatch)
                {
                    throw new BusinessErrorException("Warehouse.ItemMatch.NotMatch", resolver.Input);
                }
            }
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        /// <summary>
        /// 翻箱匹配
        /// </summary>
        /// <param name="resolver"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void MatchRepack(Resolver resolver)
        {
            string huId = resolver.Input;
            if (huId == string.Empty)
            {
                throw new BusinessErrorException("Common.Business.Hu.NotExist");
            }

            //为了转成统一对象，只能写死，第一个为投入，第二个为产出
            #region 翻箱前
            if (resolver.IOType == BusinessConstants.IO_TYPE_IN)
            {
                if (resolver.Transformers != null && resolver.Transformers.Count == 2)
                {
                    Transformer inTransformer = resolver.Transformers[0];
                    if (inTransformer == null)
                    {
                        inTransformer = new Transformer();
                    }
                    if (inTransformer.TransformerDetails == null)
                    {
                        inTransformer.TransformerDetails = new List<TransformerDetail>();
                    }

                    if (resolver.Transformers[1] == null)
                    {
                        resolver.Transformers[1] = new Transformer();
                    }
                    if (resolver.Transformers[1].TransformerDetails == null)
                    {
                        resolver.Transformers[1].TransformerDetails = new List<TransformerDetail>();
                    }

                    #region 拆箱输入只能一条
                    if (resolver.ModuleType == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_DEVANNING)
                    {
                        if (inTransformer.TransformerDetails.Count > 0)
                        {
                            throw new BusinessErrorException("MasterData.Inventory.Repack.Devanning.MoreThanOneInput");
                        }
                    }
                    #endregion


                    //校验重复扫描
                    if (inTransformer.TransformerDetails != null && inTransformer.TransformerDetails.Count > 0)
                    {
                        foreach (TransformerDetail inTransformDetail in inTransformer.TransformerDetails)
                        {
                            if (inTransformDetail.HuId == huId)
                            {
                                throw new BusinessErrorException("Repack.Error.HuReScan", huId);
                            }
                        }
                    }
                    LocationLotDetail locationLotDetail = locationLotDetailMgr.CheckLoadHuLocationLotDetail(huId, resolver.UserCode);
                    if (inTransformer.LocationFromCode == null || inTransformer.LocationFromCode == string.Empty)
                    {
                        inTransformer.LocationFromCode = locationLotDetail.Location.Code;
                    }
                    if (locationLotDetail.Location.Code != inTransformer.LocationFromCode)
                    {
                        throw new BusinessErrorException("Repack.Error.Location.NotEqual");
                    }
                    RepackDetail repackDetail = new RepackDetail();
                    repackDetail.LocationLotDetail = locationLotDetail;
                    repackDetail.Hu = locationLotDetail.Hu;
                    repackDetail.IOType = BusinessConstants.IO_TYPE_IN;
                    repackDetail.Qty = repackDetail.Hu.Qty * repackDetail.Hu.UnitQty;
                    inTransformer.AddTransformerDetail(TransformerHelper.ConvertRepackDetailToTransformerDetail(repackDetail));
                    resolver.Transformers[0] = inTransformer;
                    if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING)
                    {
                        resolver.AntiResolveHu = locationLotDetail.Hu.AntiResolveHu;
                        resolver.IOType = BusinessConstants.IO_TYPE_OUT;
                        resolver.BinCode = locationLotDetail.StorageBin == null ? string.Empty : locationLotDetail.StorageBin.Code;
                    }
                }
            }
            #endregion

            #region 翻箱后
            else if (resolver.IOType == BusinessConstants.IO_TYPE_OUT)
            {
                if (resolver.Transformers != null && resolver.Transformers.Count == 2)
                {
                    Transformer outTransformer = resolver.Transformers[1];
                    Transformer inTransformer = resolver.Transformers[0];
                    if (outTransformer == null)
                    {
                        outTransformer = new Transformer();
                    }
                    if (outTransformer.TransformerDetails == null)
                    {
                        outTransformer.TransformerDetails = new List<TransformerDetail>();
                    }

                    #region 拆箱必须先扫描输入
                    if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING)
                    {
                        if (inTransformer.TransformerDetails.Count == 0)
                        {
                            throw new BusinessErrorException("Devanning.Error.Input.Empty");
                        }
                    }
                    #endregion

                    #region 校验重复扫描
                    if (outTransformer.TransformerDetails != null && outTransformer.TransformerDetails.Count > 0)
                    {
                        foreach (TransformerDetail inTransformDetail in outTransformer.TransformerDetails)
                        {
                            if (inTransformDetail.HuId == huId)
                            {
                                throw new BusinessErrorException("Repack.Error.HuReScan", huId);
                            }
                        }
                    }
                    #endregion

                    #region 根据投入的反向解析hu选项来判断产出
                    Hu hu = null;
                    if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING)  //只有拆箱支持解析条码
                    {
                        TransformerDetail inTransformerDetail = inTransformer.TransformerDetails[0];
                        Hu inputHu = huMgr.LoadHu(inTransformerDetail.HuId);

                        hu = this.ResolveHu(resolver.AntiResolveHu, huId, inputHu.Uom, inputHu.ManufactureParty);
                    }
                    else if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_REPACK)
                    {
                        hu = huMgr.CheckAndLoadHu(huId);
                    }
                    #endregion
                    TransformerDetail transformerDetail = TransformerHelper.ConvertHuToTransformerDetail(hu);
                    transformerDetail.IOType = resolver.IOType;

                    outTransformer.AddTransformerDetail(transformerDetail);
                    resolver.Transformers[1] = outTransformer;
                }
            }
            #endregion

            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
            // return transformerList;
        }

        /// <summary>
        /// 用于移库,退货匹配
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="hu"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void MatchHuByFlowView(Resolver resolver, FlowView flowView, Hu hu)
        {
            TransformerDetail transformerDetail = Utility.TransformerHelper.ConvertHuToTransformerDetail(flowView, hu);
            transformerDetail.StorageBinCode = resolver.BinCode;//库格
            resolver.AddTransformerDetail(transformerDetail);
        }

        /// <summary>
        /// 用于移库,退货匹配//已经废弃此方法 用MatchHuByFlowView(Resolver resolver, FlowView flowView, Hu hu)
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="hu"></param>
        [Transaction(TransactionMode.Unspecified)]
        public void MatchHuByItem(Resolver resolver, Hu hu)
        {
            if (hu == null)
            {
                throw new BusinessErrorException("Hu.Error.HuIdNotExist", resolver.Input);
            }

            //校验重复扫描
            if (this.CheckMatchHuScanExist(resolver))
            {
                throw new BusinessErrorException("Warehouse.Error.HuReScan", resolver.Input);
            }
            if (!MatchByItem(resolver, hu))
            {
                if (!resolver.AllowCreateDetail)
                {
                    throw new BusinessErrorException("Warehouse.HuMatch.NotMatch", resolver.Input);
                }
                else //允许新增明细
                {
                    Flow flow = flowMgr.LoadFlow(resolver.Code, false);
                    string locationCode = string.Empty;
                    if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVERETURN)
                    {
                        locationCode = flow.LocationTo.Code;
                    }
                    else
                    {
                        locationCode = flow.LocationFrom == null ? string.Empty : flow.LocationFrom.Code;
                    }
                    if (hu.Location != locationCode)
                    {
                        throw new BusinessErrorException("Common.Business.Error.HuNoInventory", hu.Location, resolver.Input);
                    }

                    TransformerDetail transformerDetail = Utility.TransformerHelper.ConvertHuToTransformerDetail(hu);
                    Transformer transformer = new Transformer();
                    transformer.ItemCode = hu.Item.Code;
                    transformer.ItemDescription = hu.Item.Description;
                    transformer.UnitCount = hu.Item.UnitCount;
                    transformer.UomCode = hu.Uom.Code;
                    transformer.LocationFromCode = hu.Location;
                    transformer.LocationToCode = flow.LocationTo == null ? string.Empty : flow.LocationTo.Code;

                    transformer.AddTransformerDetail(transformerDetail);
                    resolver.AddTransformer(transformer);
                    TransformerHelper.ProcessTransformer(transformer);
                }
            }
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        public void CheckHuReScan(Resolver resolver)
        {
            if (CheckMatchHuScanExist(resolver))
            {
                throw new BusinessErrorException("Warehouse.Error.HuReScan", resolver.Input);
            }
        }

        public bool CheckMatchHuScanExist(Resolver resolver)
        {

            if (resolver != null && resolver.Transformers != null && resolver.Transformers.Count > 0)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer.TransformerDetails != null && transformer.TransformerDetails.Count > 0)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            if (transformerDetail.HuId != null
                                && transformerDetail.HuId.Trim().ToUpper() == resolver.Input.Trim().ToUpper()
                                && transformerDetail.CurrentQty != 0)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        public void SetMateria(Resolver resolver)
        {
            //将已投料和新投料合并
            #region 已投料
            IList<ProductLineInProcessLocationDetail> productLineIpList =
                productLineInProcessLocationDetailMgr.GetProductLineInProcessLocationDetail(resolver.Code, BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE);
            #endregion

            #region 新投料
            //todo 根据flow得到Material的list
            IList<BomDetail> bomDetailList = flowMgr.GetBatchFeedBomDetail(resolver.Code);
            IList<MaterialIn> materialInList = new List<MaterialIn>();
            Flow flow = flowMgr.LoadFlow(resolver.Code, true);
            if (bomDetailList.Count > 0)
            {
                foreach (BomDetail bomDetail in bomDetailList)
                {
                    MaterialIn materialIn = new MaterialIn();
                    materialIn.Location = bomDetail.Location;
                    materialIn.Operation = bomDetail.Operation;
                    materialIn.RawMaterial = bomDetail.Item;

                    //来源库位查找逻辑BomDetail-->RoutingDetail-->FlowDetail-->Flow
                    Location bomLocFrom = bomDetail.Location;
                    if (flow.Routing != null)
                    {
                        //在Routing上查找，并检验Routing上的工序和BOM上的是否匹配
                        RoutingDetail routingDetail = routingDetailMgr.LoadRoutingDetail(flow.Routing, bomDetail.Operation, bomDetail.Reference);
                        if (routingDetail != null)
                        {
                            if (bomLocFrom == null)
                            {
                                bomLocFrom = routingDetail.Location;
                            }
                        }
                    }
                    if (bomLocFrom == null)
                    {
                        bomLocFrom = bomDetail.DefaultLocation;
                    }
                    materialIn.Location = bomLocFrom;
                    materialInList.Add(materialIn);
                }
            }
            #endregion
            List<Transformer> inTransformer = Utility.TransformerHelper.ConvertProductLineInProcessLocationDetailsToTransformers(productLineIpList);
            List<Transformer> newTransformer = Utility.TransformerHelper.ConvertMaterialInsToTransformers(materialInList);
            resolver.Transformers = MergeTransformers(inTransformer, newTransformer);
            resolver.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMER;
        }


        public string CheckFIFO(Hu targetHu, IList<string> pickedHuList)
        {
            string pickBy = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_PICK_BY).Value;
            if (pickBy == BusinessConstants.CODE_MASTER_PICKBY_HU)
            {
                throw new NotImplementedException();
            }
            else
            {
                DetachedCriteria criteria = DetachedCriteria.For<LocationLotDetail>().SetProjection(Projections.Min("LotNo"));
                criteria.CreateAlias("Location", "loc");
                criteria.CreateAlias("Hu", "hu");

                criteria.Add(Expression.Eq("Item", targetHu.Item));
                criteria.Add(Expression.Eq("loc.Code", targetHu.Location));
                criteria.Add(Expression.IsNotNull("Hu"));
                criteria.Add(Expression.Not(Expression.Eq("Qty", decimal.Zero)));
                criteria.Add(Expression.Lt("hu.ManufactureDate", targetHu.ManufactureDate));

                if (pickedHuList != null && pickedHuList.Count > 0)
                {
                    criteria.Add(Expression.Not(Expression.In("hu.HuId", pickedHuList.ToArray())));
                }

                IList<string> result = this.criteriaMgr.FindAll<string>(criteria);

                return result[0];
            }
        }
        #endregion


        #region Private Method

        private bool MatchByHu(Resolver resolver, Hu hu)
        {
            return MatchByHu(resolver, hu, 0);
        }
        /// <summary>
        /// 按Hu匹配
        /// </summary>
        /// <param name="resolver"></param>
        /// <returns>Resolver</returns>
        private bool MatchByHu(Resolver resolver, Hu hu, int locationLotDetId)
        {
            bool isMatch = false;

            if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ASN)
            {
                #region   ASN
                if (resolver.Transformers != null && hu != null)
                {
                    foreach (Transformer transformer in resolver.Transformers)
                    {
                        if (transformer.ItemCode.ToUpper() == hu.Item.Code.ToUpper())
                        {
                            InProcessLocation ip = inProcessLocationMgr.LoadInProcessLocation(resolver.Code, true);
                            var count = ip.InProcessLocationDetails.Where(t => t.HuId.ToUpper() == hu.HuId.ToUpper()).Count();
                            isMatch = count == 1;
                            if (isMatch)
                            {
                                TransformerDetail NewTransformerDetail = TransformerHelper.ConvertHuToTransformerDetail(hu);
                                if (resolver.BinCode != null && resolver.BinCode.Trim() != string.Empty)
                                {
                                    NewTransformerDetail.StorageBinCode = resolver.BinCode;
                                }
                                NewTransformerDetail.LocationLotDetId = locationLotDetId;
                                NewTransformerDetail.Sequence = this.FindMaxSeq(resolver.Transformers) + 1;
                                transformer.AddTransformerDetail(NewTransformerDetail);
                                TransformerHelper.ProcessTransformer(transformer);
                                break;
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region 老的逻辑
                if (resolver.Transformers != null && hu != null)
                {
                    foreach (Transformer transformer in resolver.Transformers)
                    {
                        if (transformer.TransformerDetails != null && transformer.TransformerDetails.Count > 0)
                        {
                            foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                            {
                                if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST)
                                {
                                    if (this.locationMgr.IsHuOcuppyByPickList(hu.HuId))
                                    {
                                        //判断条码是否被拣货单占用
                                        throw new BusinessErrorException("Order.Error.PickUp.HuOcuppied", hu.HuId);
                                    }
                                }

                                if (transformerDetail.HuId != null
                                    && transformerDetail.HuId.ToUpper() == hu.HuId.ToUpper())
                                {
                                    transformerDetail.StorageBinCode = resolver.BinCode;
                                    transformerDetail.CurrentQty = hu.Qty;
                                    transformerDetail.LocationLotDetId = locationLotDetId;
                                    transformerDetail.Sequence = this.FindMaxSeq(resolver.Transformers) + 1;
                                    isMatch = true;
                                    break;
                                }
                            }

                            if (isMatch)
                            {
                                TransformerHelper.ProcessTransformer(transformer);
                                break;
                            }
                        }
                    }
                }
                #endregion
            }
            return isMatch;
        }


        private bool MatchByLot(Resolver resolver, Hu hu)
        {
            return MatchByLot(resolver, hu, 0);
        }
        /// <summary>
        /// 按LotNo匹配
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="hu"></param>
        /// <returns></returns>
        private bool MatchByLot(Resolver resolver, Hu hu, int locationLotDetId)
        {
            if (resolver.Transformers != null && hu != null)
            {
                bool findMatch = false;
                foreach (Transformer transformer in resolver.Transformers)
                {
                    //先用零件号,单位和单包装匹配                
                    if (transformer.ItemCode.ToUpper() == hu.Item.Code.ToUpper()
                        && transformer.UomCode.ToUpper() == hu.Uom.Code.ToUpper()
                        && (transformer.UnitCount == hu.UnitCount || transformer.UnitCount == hu.LotSize || transformer.UnitCount == hu.Qty))
                    {
                        findMatch = ProcessTransformerByLotNo(resolver, transformer, hu, locationLotDetId);

                        if (findMatch)
                        {
                            return findMatch;
                        }
                    }
                }

                foreach (Transformer transformer in resolver.Transformers)
                {
                    //没有找到再用零件号,单位匹配一次 零头先发
                    if (transformer.ItemCode.ToUpper() == hu.Item.Code.ToUpper()
                        && transformer.UomCode.ToUpper() == hu.Uom.Code.ToUpper()
                        && transformer.OddShipOption == BusinessConstants.CODE_MASTER_ODD_SHIP_OPTION_VALUE_SHIP_FIRST)
                    {
                        findMatch = ProcessTransformerByLotNo(resolver, transformer, hu, locationLotDetId);

                        if (findMatch)
                        {
                            return findMatch;
                        }
                    }
                }
            }
            return false;
        }

        private bool ProcessTransformerByLotNo(Resolver resolver, Transformer transformer, Hu hu, int locationLotDetId)
        {
            if (transformer.Qty - transformer.CurrentQty >= hu.Qty
                && transformer.LotNo != null && transformer.LotNo.ToUpper() == hu.LotNo.ToUpper())
            {
                #region 如果是拣货/订单发货，要匹配库位和库格是否相同
                if (resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_PICKLIST ||
                    resolver.ModuleType == BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIPORDER)
                {
                    if (this.locationMgr.IsHuOcuppyByPickList(hu.HuId))
                    {
                        //判断条码是否被拣货单占用
                        throw new BusinessErrorException("Order.Error.PickUp.HuOcuppied", hu.HuId);
                    }

                    if (hu.StorageBin != null && hu.StorageBin.Trim() != string.Empty &&
                        transformer.StorageBinCode != null &&
                        hu.StorageBin == transformer.StorageBinCode.Trim())
                    {
                        //有库格按照库格匹配
                    }
                    else if (hu.StorageBin != null && hu.StorageBin.Trim() != string.Empty
                            && hu.Location != null && hu.Location.Trim() != string.Empty &&
                         (transformer.StorageBinCode == null || transformer.StorageBinCode.Trim() == string.Empty) &&
                          transformer.LocationFromCode == hu.Location)
                    {
                        //都没有库格按照库位匹配
                    }
                    else
                    {
                        return false;
                    }
                }
                #endregion

                TransformerDetail NewTransformerDetail = TransformerHelper.ConvertHuToTransformerDetail(hu);
                if (resolver.BinCode != null && resolver.BinCode.Trim() != string.Empty)
                {
                    NewTransformerDetail.StorageBinCode = resolver.BinCode;
                }
                NewTransformerDetail.LocationLotDetId = locationLotDetId;
                NewTransformerDetail.Sequence = this.FindMaxSeq(resolver.Transformers) + 1;
                transformer.AddTransformerDetail(NewTransformerDetail);
                TransformerHelper.ProcessTransformer(transformer);
                return true;
            }
            return false;
        }

        private bool MatchByItem(Resolver resolver, Hu hu)
        {
            return MatchByItem(resolver, hu, 0);
        }
        /// <summary>
        /// 缺省匹配规则,按Item匹配//暂不考虑超收超发,只有扫描条码的才有Item匹配逻辑
        /// FulfillUnitCount 需要重构
        /// 场景1:订单发运:不整包收发(FulfillUnitCount)就不匹配单包装
        /// 场景2:订单收货:不整包收发(FulfillUnitCount)就不匹配单包装
        /// 场景3:ASN收货:一定要匹配单包装,对供应商要求严格
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="hu"></param>
        /// <returns></returns>
        private bool MatchByItem(Resolver resolver, Hu hu, int locationLotDetId)
        {
            if (resolver.Transformers != null)
            {
                foreach (Transformer transformer in resolver.Transformers)
                {
                    if (transformer.ItemCode.ToUpper() == hu.Item.Code.ToUpper()
                       && transformer.UomCode.ToUpper() == hu.Uom.Code.ToUpper()
                       && (transformer.UnitCount == hu.UnitCount || transformer.UnitCount == hu.LotSize || transformer.UnitCount == hu.Qty)
                       && (transformer.Qty - transformer.CurrentQty >= hu.Qty || resolver.AllowExceed))
                    {
                        TransformerDetail transformerDetail = TransformerHelper.ConvertHuToTransformerDetail(hu);
                        transformerDetail.StorageBinCode = resolver.BinCode;
                        transformerDetail.LocationLotDetId = locationLotDetId;
                        transformerDetail.Sequence = this.FindMaxSeq(resolver.Transformers) + 1;
                        transformer.AddTransformerDetail(transformerDetail);
                        TransformerHelper.ProcessTransformer(transformer);
                        return true;
                    }
                }
                //没有匹配上,再次匹配
                if (resolver.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER)
                {
                    OrderHead orderHead = orderHeadMgr.LoadOrderHead(resolver.Code);
                    if (!orderHead.FulfillUnitCount)
                    {
                        foreach (Transformer transformer in resolver.Transformers)
                        {
                            if (transformer.ItemCode.ToUpper() == hu.Item.Code.ToUpper()
                               && transformer.UomCode.ToUpper() == hu.Uom.Code.ToUpper()
                               && (transformer.Qty - transformer.CurrentQty >= hu.Qty || resolver.AllowExceed))
                            {
                                TransformerDetail transformerDetail = TransformerHelper.ConvertHuToTransformerDetail(hu);
                                transformerDetail.StorageBinCode = resolver.BinCode;
                                transformerDetail.LocationLotDetId = locationLotDetId;
                                transformerDetail.Sequence = this.FindMaxSeq(resolver.Transformers) + 1;
                                transformer.AddTransformerDetail(transformerDetail);
                                TransformerHelper.ProcessTransformer(transformer);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public int FindMaxSeq(List<Transformer> transformerList)
        {
            int maxSeq = 0;
            if (transformerList != null)
            {
                foreach (Transformer transformer in transformerList)
                {
                    if (transformer.TransformerDetails != null)
                    {
                        foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                        {
                            if (transformerDetail.Sequence > maxSeq)
                            {
                                maxSeq = transformerDetail.Sequence;
                            }
                        }
                    }
                }
            }
            return maxSeq;
        }

        public int FindMaxSeq(Transformer transformer)
        {
            int maxSeq = 0;
            if (transformer.TransformerDetails != null)
            {
                foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                {
                    if (transformerDetail.Sequence > maxSeq)
                    {
                        maxSeq = transformerDetail.Sequence;
                    }
                }
            }
            return maxSeq;
        }

        [Transaction(TransactionMode.Unspecified)]
        private Hu ResolveAndCreateHu(string barCode, IList<int> orderLocationTransactionIdList)
        {
            object[] result = ResolveBarCode(barCode);
            Item item = (Item)result[0];
            string supMark = (string)result[1];
            string lotNo = (string)result[2];
            decimal qty = (decimal)result[3];
            int seq = (int)result[4];
            DateTime manufactureDate = (DateTime)result[5];

            EntityPreference entityPreference = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_COMPANY_ID_MARK); //企业标识位

            foreach (int orderLocationTransactionId in orderLocationTransactionIdList)
            {
                OrderLocationTransaction orderLocationTransaction = this.orderLocationTransactionMgr.LoadOrderLocationTransaction(orderLocationTransactionId);
                OrderDetail orderDetail = orderLocationTransaction.OrderDetail;

                //对于一条flow上同一个Item出现有多次，使用第一个。
                if (item.Code.ToUpper() == orderDetail.Item.Code.ToUpper()
                    //&& (supMark == orderDetail.IdMark      //等于订单上供应商标识位
                    //    || supMark == entityPreference.Value  //等于企业标识位
                    //    || (supMark == BusinessConstants.DEFAULT_SUPPLIER_ID_MARK    //等于默认供应商标示位
                    //        && (orderDetail.IdMark == null
                    //            || orderDetail.IdMark.Trim() == string.Empty)
                    //        )
                    //    )
                    )
                {
                    Hu hu = new Hu();
                    hu.HuId = barCode;
                    hu.Item = item;
                    hu.Uom = orderDetail.Uom;   //用Flow单位
                    #region 单位用量
                    if (item.Uom.Code != orderDetail.Uom.Code)
                    {
                        hu.UnitQty = this.uomConversionMgr.ConvertUomQty(item, orderDetail.Uom, 1, item.Uom);   //单位用量
                    }
                    else
                    {
                        hu.UnitQty = 1;
                    }
                    #endregion
                    hu.QualityLevel = BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1;
                    hu.Qty = qty / hu.UnitQty;
                    hu.UnitCount = orderDetail.UnitCount;
                    hu.LotNo = lotNo;
                    hu.ManufactureDate = manufactureDate;
                    hu.ManufactureParty = orderDetail.OrderHead.PartyFrom;
                    hu.CreateUser = this.userMgr.GetMonitorUser();
                    hu.CreateDate = DateTime.Now;
                    hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CREATE;
                    hu.LotSize = orderDetail.HuLotSize.HasValue ? orderDetail.HuLotSize.Value : hu.Qty;

                    this.huMgr.CreateHu(hu);
                    this.numberControlMgr.ReverseUpdateHuId(barCode);

                    return hu;
                }
            }
            return null;
        }

        [Transaction(TransactionMode.Unspecified)]
        private Hu ResolveAndCreateHu(string barCode, Uom uom, Party manufactureParty)
        {
            object[] result = ResolveBarCode(barCode);
            Item item = (Item)result[0];
            string supMark = (string)result[1];
            string lotNo = (string)result[2];
            decimal qty = (decimal)result[3];
            int seq = (int)result[4];
            DateTime manufactureDate = (DateTime)result[5];

            Hu hu = new Hu();
            hu.HuId = barCode;
            hu.Item = item;
            hu.Uom = uom;
            #region 单位用量
            if (item.Uom.Code != uom.Code)
            {
                hu.UnitQty = this.uomConversionMgr.ConvertUomQty(item, uom, 1, item.Uom);   //单位用量
            }
            else
            {
                hu.UnitQty = 1;
            }
            #endregion
            hu.QualityLevel = BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1;
            hu.Qty = qty / hu.UnitQty;
            hu.UnitCount = qty;
            hu.LotNo = lotNo;
            hu.ManufactureDate = manufactureDate;
            hu.ManufactureParty = manufactureParty;
            hu.CreateUser = this.userMgr.GetMonitorUser();
            hu.CreateDate = DateTime.Now;
            hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CREATE;
            hu.LotSize = hu.Qty;

            this.huMgr.CreateHu(hu);
            this.numberControlMgr.ReverseUpdateHuId(barCode);

            return hu;
        }


        [Transaction(TransactionMode.Unspecified)]
        private Hu ResolveAndCreateHu(string barCode, string orderNo)
        {
            IList<OrderDetail> orderDetailList = this.orderDetailMgr.GetOrderDetail(orderNo);
            return ResolveAndCreateHu(barCode, orderDetailList);
        }

        [Transaction(TransactionMode.Unspecified)]
        private Hu ResolveAndCreateHu(string barCode, Flow flow)
        {
            IList<OrderDetail> orderDetailList = orderMgr.TransferFlow2Order(flow).OrderDetails;
            return ResolveAndCreateHu(barCode, orderDetailList);
        }

        [Transaction(TransactionMode.Unspecified)]
        private Hu ResolveAndCreateHu(string barCode, IList<OrderDetail> orderDetailList)
        {
            object[] result = ResolveBarCode(barCode);
            Item item = (Item)result[0];
            string supMark = (string)result[1];
            string lotNo = (string)result[2];
            decimal qty = (decimal)result[3];
            int seq = (int)result[4];
            DateTime manufactureDate = (DateTime)result[5];

            EntityPreference entityPreference = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_COMPANY_ID_MARK); //企业标识位

            //IList<OrderDetail> orderDetailList = this.orderDetailMgr.GetOrderDetail(orderNo);
            foreach (OrderDetail orderDetail in orderDetailList)
            {
                //对于一条flow上同一个Item出现有多次，使用第一个。
                if (item.Code.ToUpper() == orderDetail.Item.Code.ToUpper()
                    //&& (supMark == orderDetail.IdMark      //等于订单上供应商标识位
                    //    || supMark == entityPreference.Value  //等于企业标识位
                    //    || (supMark == BusinessConstants.DEFAULT_SUPPLIER_ID_MARK    //等于默认供应商标示位
                    //        && (orderDetail.IdMark == null
                    //            || orderDetail.IdMark.Trim() == string.Empty)
                    //        )
                    //    )
                    )
                {
                    Hu hu = new Hu();
                    hu.HuId = barCode;
                    hu.Item = item;
                    hu.Uom = orderDetail.Uom;   //用Flow单位
                    #region 单位用量
                    if (item.Uom.Code != orderDetail.Uom.Code)
                    {
                        hu.UnitQty = this.uomConversionMgr.ConvertUomQty(item, orderDetail.Uom, 1, item.Uom);   //单位用量
                    }
                    else
                    {
                        hu.UnitQty = 1;
                    }
                    #endregion
                    hu.QualityLevel = BusinessConstants.CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1;
                    hu.Qty = qty / hu.UnitQty;
                    hu.UnitCount = orderDetail.UnitCount;
                    hu.LotNo = lotNo;
                    hu.ManufactureDate = manufactureDate;
                    hu.ManufactureParty = orderDetail.OrderHead.PartyFrom;
                    hu.CreateUser = this.userMgr.GetMonitorUser();
                    hu.CreateDate = DateTime.Now;
                    hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CREATE;
                    hu.LotSize = orderDetail.HuLotSize.HasValue ? orderDetail.HuLotSize.Value : hu.Qty;

                    this.huMgr.CreateHu(hu);
                    this.numberControlMgr.ReverseUpdateHuId(barCode);

                    return hu;
                }
            }
            return null;
        }

        [Transaction(TransactionMode.Unspecified)]
        private object[] ResolveBarCode(string barCode)
        {
            try
            {
                string[] splitedBarcode = BarcodeHelper.SplitBarcode(barCode);

                Item item = null;
                string supMark = null;
                string lotNo = null;
                decimal qty = 0;
                int seq = 0;
                DateTime manufactureDate = DateTime.Now;

                item = this.itemMgr.CheckAndLoadItem(splitedBarcode[0]);
                supMark = splitedBarcode[1];
                lotNo = splitedBarcode[2];
                qty = decimal.Parse(splitedBarcode[3]);
                seq = int.Parse(splitedBarcode[4]);
                manufactureDate = LotNoHelper.ResolveLotNo(lotNo);

                object[] result = new object[6];
                result[0] = item;
                result[1] = supMark;
                result[2] = lotNo;
                result[3] = qty;
                result[4] = seq;
                result[5] = manufactureDate;

                return result;
            }
            catch (BusinessErrorException ex)
            {
                throw new BusinessErrorException("Hu.Error.HuIdNotExist", ex, barCode);
            }
        }

        private Hu ResolveHu(string antiResolveHuOption, string huId, List<Transformer> transformerList)
        {
            Hu hu = null;
            if (antiResolveHuOption == BusinessConstants.CODE_MASTER_ANTI_RESOLVE_HU_VALUE_NOT_RESOLVE
                || antiResolveHuOption == null || antiResolveHuOption.Trim() == string.Empty)
            {
                hu = huMgr.CheckAndLoadHu(huId);
            }
            else
            {
                #region 自动解析HuId，生成Hu
                hu = huMgr.LoadHu(huId);

                if (hu == null)
                {
                    IList<int> orderLocationTransactionIdList = new List<int>();
                    if (transformerList != null && transformerList.Count > 0)
                    {
                        foreach (Transformer transformer in transformerList)
                        {
                            if (transformer.OrderLocTransId > 0)
                            {
                                orderLocationTransactionIdList.Add(transformer.OrderLocTransId);
                            }
                        }
                    }
                    hu = this.ResolveAndCreateHu(huId, orderLocationTransactionIdList);
                    if (hu == null)
                    {
                        throw new BusinessErrorException("MasterData.Hu.AntiResolveHu.CannotResolve", huId);
                    }
                }
                #endregion
            }

            return hu;
        }

        private Hu ResolveHu(string antiResolveHuOption, string huId, Uom uom, Party manufactureParty)
        {
            Hu hu = null;
            if (antiResolveHuOption == BusinessConstants.CODE_MASTER_ANTI_RESOLVE_HU_VALUE_NOT_RESOLVE
                || antiResolveHuOption == null || antiResolveHuOption.Trim() == string.Empty)
            {
                hu = huMgr.CheckAndLoadHu(huId);
            }
            else
            {
                #region 自动解析HuId，生成Hu
                hu = huMgr.LoadHu(huId);

                if (hu == null)
                {
                    hu = this.ResolveAndCreateHu(huId, uom, manufactureParty);
                }
                #endregion
            }

            return hu;
        }


        /// <summary>
        /// 用于投料/回冲
        /// </summary>
        /// <param name="originalTransformer"></param>
        /// <param name="targetTransformers"></param>
        /// <returns></returns>
        private List<Transformer> MergeTransformers(List<Transformer> originalTransformer, List<Transformer> targetTransformers)
        {
            if (targetTransformers == null)
            {
                return null;
            }

            originalTransformer = Utility.TransformerHelper.MergeTransformers(originalTransformer);
            targetTransformers = Utility.TransformerHelper.MergeTransformers(targetTransformers);

            foreach (Transformer transformer in targetTransformers)
            {
                var q = originalTransformer.Where(t => t.ItemCode == transformer.ItemCode
                                                    && t.UnitCount == transformer.UnitCount
                                                    && t.UomCode == transformer.UomCode
                                                    && t.LocationCode == transformer.LocationCode);
                if (q.Count() > 0)
                {
                    transformer.Qty = q.Sum(r => r.Qty);
                    transformer.OrderedQty = q.Sum(r => r.OrderedQty);
                }
            }

            return targetTransformers;
        }


        #endregion

    }
}
