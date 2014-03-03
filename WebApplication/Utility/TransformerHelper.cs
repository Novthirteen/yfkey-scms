using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity;
using com.Sconit.Entity.View;
using com.Sconit.Entity.Production;

namespace com.Sconit.Utility
{
    /// <summary>
    /// 各种对象转换Transformer
    /// </summary>
    public static class TransformerHelper
    {
        #region 基础转换
        public static Transformer ConvertTransformerDetailToTransformer(TransformerDetail transformerDetail)
        {
            if (transformerDetail == null)
                return null;

            Transformer transformer = new Transformer();
            transformer.ItemCode = transformerDetail.ItemCode;
            transformer.ItemDescription = transformerDetail.ItemDescription;
            transformer.UomCode = transformerDetail.UomCode;
            transformer.UnitCount = transformerDetail.UnitCount;
            transformer.Qty = transformerDetail.Qty;
            transformer.CurrentQty = transformerDetail.CurrentQty;
            transformer.LocationCode = transformerDetail.LocationCode;
            transformer.LocationFromCode = transformerDetail.LocationFromCode;
            transformer.LocationToCode = transformerDetail.LocationToCode;
            transformer.LotNo = transformerDetail.LotNo;
            transformer.LocationLotDetId = transformerDetail.LocationLotDetId;
            transformer.OrderLocTransId = transformerDetail.OrderLocTransId;
            transformer.StorageBinCode = transformerDetail.StorageBinCode;

            return transformer;
        }

        public static Transformer ConvertOrderLocationTransactionToTransformer(OrderLocationTransaction orderLocationTransaction)
        {
            if (orderLocationTransaction == null)
                return null;

            Transformer transformer = new Transformer();
            transformer.OrderLocTransId = orderLocationTransaction.Id;
            transformer.OrderNo = orderLocationTransaction.OrderDetail.OrderHead.OrderNo;
            transformer.Sequence = orderLocationTransaction.OrderDetail.Sequence;
            transformer.ItemCode = orderLocationTransaction.OrderDetail.Item.Code;
            transformer.ItemDescription = orderLocationTransaction.Item.Description;
            transformer.ReferenceItemCode = orderLocationTransaction.OrderDetail.ReferenceItemCode;
            transformer.UomCode = orderLocationTransaction.OrderDetail.Uom.Code;
            transformer.UnitCount = orderLocationTransaction.OrderDetail.UnitCount;
            transformer.LocationFromCode = orderLocationTransaction.OrderDetail.DefaultLocationFrom != null ?
                orderLocationTransaction.OrderDetail.DefaultLocationFrom.Code : null;
            transformer.LocationToCode = orderLocationTransaction.OrderDetail.DefaultLocationTo != null ?
                orderLocationTransaction.OrderDetail.DefaultLocationTo.Code : null;
            transformer.OrderedQty = orderLocationTransaction.OrderDetail.OrderedQty;
            transformer.ShippedQty = orderLocationTransaction.OrderDetail.ShippedQty.HasValue ? orderLocationTransaction.OrderDetail.ShippedQty.Value : 0;
            transformer.ReceivedQty = orderLocationTransaction.OrderDetail.ReceivedQty.HasValue ? orderLocationTransaction.OrderDetail.ReceivedQty.Value : 0;
            transformer.OddShipOption = orderLocationTransaction.OrderDetail.OddShipOption;
            return transformer;
        }

        public static TransformerDetail ConvertOrderLocationTransactionToTransformerDetail(OrderLocationTransaction orderLocationTransaction)
        {
            if (orderLocationTransaction == null)
                return null;

            TransformerDetail transformerDetail = new TransformerDetail();
            transformerDetail.ItemCode = orderLocationTransaction.OrderDetail.Item.Code;
            transformerDetail.ItemDescription = orderLocationTransaction.Item.Description;
            transformerDetail.UomCode = orderLocationTransaction.OrderDetail.Uom.Code;
            transformerDetail.UnitCount = orderLocationTransaction.OrderDetail.UnitCount;
            transformerDetail.OrderNo = orderLocationTransaction.OrderDetail.OrderHead.OrderNo;
            transformerDetail.ReferenceItemCode = orderLocationTransaction.OrderDetail.ReferenceItemCode;
            transformerDetail.Sequence = orderLocationTransaction.OrderDetail.Sequence;
            transformerDetail.LocationFromCode = orderLocationTransaction.OrderDetail.DefaultLocationFrom != null ?
             orderLocationTransaction.OrderDetail.DefaultLocationFrom.Code : null;
            transformerDetail.LocationToCode = orderLocationTransaction.OrderDetail.DefaultLocationTo != null ?
                orderLocationTransaction.OrderDetail.DefaultLocationTo.Code : null;
            transformerDetail.OrderLocTransId = orderLocationTransaction.Id;
            return transformerDetail;
        }

        public static Transformer ConvertFlowDetailToTransformer(FlowDetail flowDetail)
        {
            if (flowDetail == null)
                return null;

            Transformer transformer = new Transformer();
            transformer.ItemCode = flowDetail.Item.Code;
            transformer.ItemDescription = flowDetail.Item.Description;
            transformer.ReferenceItemCode = flowDetail.ReferenceItemCode;
            transformer.UomCode = flowDetail.Uom.Code;
            transformer.UnitCount = flowDetail.UnitCount;
            transformer.LocationFromCode = flowDetail.DefaultLocationFrom != null ? flowDetail.DefaultLocationFrom.Code : null;
            transformer.LocationToCode = flowDetail.DefaultLocationTo != null ? flowDetail.DefaultLocationTo.Code : null;

            return transformer;
        }

        public static List<Transformer> ConvertFlowDetailsToTransformers(IList<FlowDetail> flowDetails)
        {
            List<Transformer> transformers = new List<Transformer>();
            if (flowDetails == null)
            {
                return null;
            }
            foreach (FlowDetail flowDetail in flowDetails)
            {
                transformers.Add(ConvertFlowDetailToTransformer(flowDetail));
            }
            return transformers;
        }


        public static Transformer ConvertItemToTransformer(Item item)
        {
            if (item == null)
                return null;

            Transformer transformer = new Transformer();
            transformer.ItemCode = item.Code;
            transformer.ItemDescription = item.Description;
            transformer.UomCode = item.Uom.Code;
            transformer.UnitCount = item.UnitCount;

            return transformer;
        }

        public static List<TransformerDetail> ConvertProductLineInProcessLocationDetailsToTransformerDetails(IList<ProductLineInProcessLocationDetail> productLineIpList)
        {

            if (productLineIpList == null)
            {
                return null;
            }
            List<TransformerDetail> transformerDetails = new List<TransformerDetail>();
            foreach (ProductLineInProcessLocationDetail productLineIp in productLineIpList)
            {
                TransformerDetail transformerDetail = new TransformerDetail();
                transformerDetail.ItemCode = productLineIp.Item.Code;
                transformerDetail.ItemDescription = productLineIp.Item.Description;
                transformerDetail.UomCode = productLineIp.Item.Uom.Code;
                transformerDetail.UnitCount = productLineIp.Item.UnitCount;
                transformerDetail.Qty = productLineIp.Qty - productLineIp.BackflushQty;
                transformerDetail.CurrentQty = transformerDetail.Qty;
                transformerDetails.Add(transformerDetail);
            }
            return transformerDetails;
        }

        public static List<Transformer> ConvertProductLineInProcessLocationDetailsToTransformers(IList<ProductLineInProcessLocationDetail> productLineIpList)
        {
            if (productLineIpList == null)
            {
                return null;
            }
            List<Transformer> transformers = new List<Transformer>();
            foreach (ProductLineInProcessLocationDetail productLineIp in productLineIpList)
            {
                Transformer transformer = new Transformer();
                transformer.Operation = productLineIp.Operation.HasValue ? productLineIp.Operation.Value : 0;
                transformer.ItemCode = productLineIp.Item.Code;
                transformer.ItemDescription = productLineIp.Item.Description;
                transformer.UomCode = productLineIp.Item.Uom.Code;
                transformer.UnitCount = productLineIp.Item.UnitCount;
                transformer.Qty = productLineIp.Qty;//累计回冲量
                transformer.OrderedQty = productLineIp.BackflushQty;//总投量
                transformer.LocationCode = productLineIp.LocationFrom == null ? string.Empty : productLineIp.LocationFrom.Code;
                transformers.Add(transformer);
            }
            return transformers;
        }
        #endregion

        #region 转换为Transformer
        public static List<Transformer> ConvertInProcessLocationDetailsToTransformers(IList<InProcessLocationDetail> inProcessLocationDetailList)
        {
            List<Transformer> transformerList = new List<Transformer>();
            if (inProcessLocationDetailList != null && inProcessLocationDetailList.Count > 0)
            {
                foreach (InProcessLocationDetail inProcessLocationDetail in inProcessLocationDetailList)
                {
                    bool isExist = false;
                    foreach (Transformer transformer in transformerList)
                    {
                        //按OrderLocTransId还是按Item汇总?
                        if (inProcessLocationDetail.OrderLocationTransaction.Id == transformer.OrderLocTransId)
                        {
                            TransformerDetail transformerDetail = ConvertInProcessLocationDetailToTransformerDetail(inProcessLocationDetail);

                            transformer.AddTransformerDetail(transformerDetail);
                            transformer.Qty += transformerDetail.Qty;
                            isExist = true;
                            break;
                        }
                    }

                    if (!isExist)
                    {
                        Transformer transformer = ConvertOrderLocationTransactionToTransformer(inProcessLocationDetail.OrderLocationTransaction);
                        if (inProcessLocationDetail.InProcessLocation.Status == null)
                        {
                            transformer.Qty = inProcessLocationDetail.QtyToShip;
                            transformer.CurrentQty = inProcessLocationDetail.Qty;
                        }
                        else if (inProcessLocationDetail.InProcessLocation.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                        {
                            transformer.Qty = inProcessLocationDetail.Qty;
                            transformer.CurrentQty = inProcessLocationDetail.Qty;
                        }
                        else if (inProcessLocationDetail.InProcessLocation.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
                        {
                            transformer.Qty = inProcessLocationDetail.Qty;
                            if (inProcessLocationDetail.ReceivedQty.HasValue)
                            {
                                if (inProcessLocationDetail.Qty > inProcessLocationDetail.ReceivedQty.Value)
                                {
                                    transformer.CurrentQty = inProcessLocationDetail.Qty - inProcessLocationDetail.ReceivedQty.Value;
                                }
                                else
                                {
                                    transformer.CurrentQty = 0;
                                }
                            }
                            else
                            {
                                transformer.CurrentQty = inProcessLocationDetail.Qty;
                            }
                        }
                        else if (inProcessLocationDetail.InProcessLocation.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)
                        {
                            transformer.Qty = inProcessLocationDetail.Qty;
                            transformer.CurrentQty = inProcessLocationDetail.Qty;
                        }

                        if (inProcessLocationDetail.HuId != null || inProcessLocationDetail.LotNo != null)
                            transformer.AddTransformerDetail(ConvertInProcessLocationDetailToTransformerDetail(inProcessLocationDetail));
                        transformerList.Add(transformer);
                    }
                }
            }
            ProcessTransformer(transformerList);
            return transformerList;
        }

        public static List<Transformer> ConvertReceiptToTransformer(IList<ReceiptDetail> receiptDetailList)
        {
            List<Transformer> transformerList = new List<Transformer>();
            if (receiptDetailList != null && receiptDetailList.Count > 0)
            {
                foreach (ReceiptDetail receiptDetail in receiptDetailList)
                {
                    bool isExist = false;
                    foreach (Transformer transformer in transformerList)
                    {
                        if (receiptDetail.OrderLocationTransaction.Id == transformer.OrderLocTransId)
                        {
                            TransformerDetail transformerDetail = ConvertReceiptToTransformerDetail(receiptDetail);

                            //transformer.AddTransformerDetail(transformerDetail);
                            if (receiptDetail.HuId != null || receiptDetail.LotNo != null)
                            {
                                transformer.AddTransformerDetail(transformerDetail);
                                transformer.Qty += transformerDetail.Qty;
                                transformer.CurrentQty += transformerDetail.CurrentQty;
                            }
                            transformer.AdjustQty = transformer.Qty;
                            isExist = true;
                            break;
                        }
                    }

                    if (!isExist)
                    {
                        Transformer transformer = ConvertOrderLocationTransactionToTransformer(receiptDetail.OrderLocationTransaction);
                        transformer.Qty = receiptDetail.ShippedQty.HasValue ? receiptDetail.ShippedQty.Value : 0;
                        transformer.CurrentQty = receiptDetail.ReceivedQty.HasValue ? receiptDetail.ReceivedQty.Value : 0;

                        transformer.AdjustQty = transformer.CurrentQty;

                        if (receiptDetail.HuId != null || receiptDetail.LotNo != null)
                        {
                            transformer.AddTransformerDetail(ConvertReceiptToTransformerDetail(receiptDetail));
                        }
                        transformerList.Add(transformer);
                    }
                }
            }
            ProcessTransformer(transformerList);
            return transformerList;
        }

        public static TransformerDetail ConvertReceiptToTransformerDetail(ReceiptDetail receiptDetail)
        {
            TransformerDetail transformerDetail = ConvertOrderLocationTransactionToTransformerDetail(receiptDetail.OrderLocationTransaction);
            transformerDetail.HuId = receiptDetail.HuId;
            transformerDetail.LotNo = receiptDetail.LotNo;
            transformerDetail.Qty = receiptDetail.ShippedQty.HasValue ? receiptDetail.ShippedQty.Value : 0;
            transformerDetail.CurrentQty = receiptDetail.ReceivedQty.HasValue ? receiptDetail.ReceivedQty.Value : 0;
            transformerDetail.AdjustQty = receiptDetail.ReceivedQty.HasValue ? receiptDetail.ReceivedQty.Value : 0;
            return transformerDetail;
        }

        public static List<TransformerDetail> ConvertReceiptsToTransformerDetails(IList<ReceiptDetail> receiptDetailList)
        {
            List<TransformerDetail> transformerDetailList = new List<TransformerDetail>();
            if (receiptDetailList != null && receiptDetailList.Count > 0)
            {
                foreach (ReceiptDetail receiptDetail in receiptDetailList)
                {
                    TransformerDetail transformerDetail = ConvertReceiptToTransformerDetail(receiptDetail);
                    transformerDetailList.Add(transformerDetail);
                }
            }
            return transformerDetailList;
        }

        public static List<Transformer> ConvertPickListDetailsToTransformers(IList<PickListDetail> pickListDetailList)
        {
            List<Transformer> transformerList = new List<Transformer>();
            if (pickListDetailList != null && pickListDetailList.Count > 0)
            {
                foreach (PickListDetail pickListDetail in pickListDetailList)
                {
                    if (pickListDetail.LotNo != null && pickListDetail.LotNo != string.Empty)//备料不足的不显示
                    {
                        bool isExist = false;
                        string pickListDetailStorageBinCode = pickListDetail.StorageBin == null ? string.Empty : pickListDetail.StorageBin.Code;
                        foreach (Transformer transformer in transformerList)
                        {
                            string transformerStorageBinCode = transformer.StorageBinCode == null ? string.Empty : transformer.StorageBinCode;
                            if (pickListDetailStorageBinCode == transformerStorageBinCode //(pickListDetail.OrderLocationTransaction.Id == transformer.OrderLocTransId)
                                && pickListDetail.Item.Code == transformer.ItemCode
                                && pickListDetail.LotNo == transformer.LotNo//按LotNo汇总,以后可能需要考虑按Hu汇总
                                && pickListDetail.OrderLocationTransaction.Id == transformer.OrderLocTransId)//按订单汇总
                            {
                                TransformerDetail transformerDetail = ConvertPickListDetailToTransformerDetail(pickListDetail);
                                transformer.AddTransformerDetail(transformerDetail);
                                transformer.Qty += transformerDetail.Qty;
                                isExist = true;
                                break;
                            }
                        }

                        if (!isExist)
                        {
                            Transformer transformer = ConvertOrderLocationTransactionToTransformer(pickListDetail.OrderLocationTransaction);
                            transformer.Id = pickListDetail.Id;
                            //transformer.Qty = pickListDetail.Qty;
                            transformer.StorageBinCode = pickListDetail.StorageBin == null ? string.Empty : pickListDetail.StorageBin.Code;
                            transformer.LotNo = pickListDetail.LotNo;
                            //transformer.CurrentQty = 
                            if (pickListDetail.HuId != null || pickListDetail.LotNo != null)
                            {
                                TransformerDetail transformerDetail = ConvertPickListDetailToTransformerDetail(pickListDetail);
                                transformer.AddTransformerDetail(transformerDetail);
                                transformer.Qty = transformerDetail.Qty;
                            }
                            transformerList.Add(transformer);
                        }
                    }
                }

            }
            ProcessTransformer(transformerList);
            return transformerList;
        }

        public static List<Transformer> ConvertPickListResultToTransformer(IList<PickListResult> pickListResultList)
        {
            List<Transformer> transformerList = new List<Transformer>();
            if (pickListResultList != null && pickListResultList.Count > 0)
            {
                foreach (PickListResult pickListResult in pickListResultList)
                {
                    bool isExist = false;
                    foreach (Transformer transformer in transformerList)
                    {
                        if (pickListResult.PickListDetail.OrderLocationTransaction.Id == transformer.OrderLocTransId)
                        {
                            TransformerDetail transformerDetail = ConvertPickListResultToTransformerDetail(pickListResult);

                            transformer.AddTransformerDetail(transformerDetail);
                            transformer.Qty += transformerDetail.Qty;
                            isExist = true;
                            break;
                        }
                    }

                    if (!isExist)
                    {
                        Transformer transformer = ConvertOrderLocationTransactionToTransformer(pickListResult.PickListDetail.OrderLocationTransaction);
                        transformer.Qty = pickListResult.Qty / pickListResult.PickListDetail.OrderLocationTransaction.UnitQty;
                        transformer.CurrentQty = transformer.Qty;//单位换算

                        if (pickListResult.PickListDetail.HuId != null || pickListResult.PickListDetail.LotNo != null)
                        {
                            transformer.AddTransformerDetail(ConvertPickListResultToTransformerDetail(pickListResult));
                        }
                        transformerList.Add(transformer);
                    }
                }
            }
            ProcessTransformer(transformerList);
            return transformerList;
        }

        public static List<Transformer> ConvertInspectDetailToTransformer(IList<InspectOrderDetail> inspectOrderDetailList)
        {
            List<Transformer> transformerList = new List<Transformer>();
            if (inspectOrderDetailList != null && inspectOrderDetailList.Count > 0)
            {
                foreach (InspectOrderDetail inspectDetail in inspectOrderDetailList)
                {
                    bool isExist = false;
                    foreach (Transformer transformer in transformerList)
                    {
                        if (inspectDetail.LocationLotDetail.Item.Code == transformer.ItemCode
                            && inspectDetail.LocationFrom.Code == transformer.LocationFromCode
                            && inspectDetail.LocationTo.Code == transformer.LocationToCode)
                        {
                            TransformerDetail transformerDetail = ConvertInspectDetailToTransformerDetail(inspectDetail);

                            transformer.AddTransformerDetail(transformerDetail);
                            transformer.Qty += transformerDetail.Qty;
                            isExist = true;
                            break;
                        }
                    }

                    if (!isExist)
                    {
                        Transformer transformer = new Transformer();

                        transformer.Id = inspectDetail.Id;
                        transformer.ItemCode = inspectDetail.LocationLotDetail.Item.Code;
                        transformer.ItemDescription = inspectDetail.LocationLotDetail.Item.Description;

                        if (inspectDetail.LocationLotDetail.Hu != null)
                        {
                            transformer.UomCode = inspectDetail.LocationLotDetail.Hu.Uom.Code;
                            transformer.UnitCount = inspectDetail.LocationLotDetail.Hu.UnitCount;
                        }
                        else
                        {
                            transformer.UomCode = inspectDetail.LocationLotDetail.Item.Uom.Code;
                            transformer.UnitCount = inspectDetail.LocationLotDetail.Item.UnitCount;
                        }
                        transformer.LocationFromCode = inspectDetail.LocationFrom.Code;
                        transformer.LocationToCode = inspectDetail.LocationTo.Code;
                        transformer.Qty = inspectDetail.InspectedQty;
                        transformer.CurrentQty = 0;

                        if (inspectDetail.LocationLotDetail.Hu != null || inspectDetail.LocationLotDetail.LotNo != null)
                        {
                            transformer.AddTransformerDetail(ConvertInspectDetailToTransformerDetail(inspectDetail));
                        }
                        transformerList.Add(transformer);
                    }
                }
            }
            ProcessTransformer(transformerList);
            return transformerList;
        }
        #endregion

        #region 转换为TransformerDetail
        public static TransformerDetail ConvertHuToTransformerDetail(Hu hu)
        {
            TransformerDetail transformerDetail = new TransformerDetail();
            transformerDetail.ItemCode = hu.Item.Code;
            transformerDetail.ItemDescription = hu.Item.Description;
            transformerDetail.UomCode = hu.Uom.Code;
            transformerDetail.UnitCount = hu.UnitCount;
            transformerDetail.HuId = hu.HuId;
            transformerDetail.LotNo = hu.LotNo;
            transformerDetail.Qty = hu.Qty;
            transformerDetail.CurrentQty = hu.Qty;
            transformerDetail.Status = hu.Status;
            transformerDetail.LocationCode = hu.Location == null ? string.Empty : hu.Location;
            transformerDetail.StorageBinCode = hu.StorageBin == null ? string.Empty : hu.StorageBin;

            return transformerDetail;
        }

        public static TransformerDetail ConvertHuToTransformerDetail(FlowView flowView, Hu hu)
        {
            TransformerDetail transformerDetail = ConvertHuToTransformerDetail(hu);
            transformerDetail.LocationFromCode = flowView.LocationFrom != null ? flowView.LocationFrom.Code : null;
            transformerDetail.LocationToCode = flowView.LocationTo != null ? flowView.LocationTo.Code : null;
            transformerDetail.StorageBinCode = hu.StorageBin == null ? string.Empty : hu.StorageBin;

            return transformerDetail;
        }

        public static List<TransformerDetail> ConvertHusToTransformerDetails(IList<Hu> huList)
        {
            List<TransformerDetail> transformerDetails = new List<TransformerDetail>();
            if (huList == null || huList.Count == 0)
            {
                return null;
            }
            foreach (Hu hu in huList)
            {
                transformerDetails.Add(ConvertHuToTransformerDetail(hu));
            }
            return transformerDetails;
        }

        public static TransformerDetail ConvertInProcessLocationDetailToTransformerDetail(InProcessLocationDetail inProcessLocationDetail)
        {
            TransformerDetail transformerDetail = ConvertOrderLocationTransactionToTransformerDetail(inProcessLocationDetail.OrderLocationTransaction);
            transformerDetail.HuId = inProcessLocationDetail.HuId;
            transformerDetail.LotNo = inProcessLocationDetail.LotNo;
            //transformerDetail.Qty = inProcessLocationDetail.QtyToShip;
            transformerDetail.Qty = inProcessLocationDetail.Qty;

            //多次收货加的
            decimal receivedQty = inProcessLocationDetail.ReceivedQty.HasValue ? (decimal)inProcessLocationDetail.ReceivedQty : 0;
            transformerDetail.CurrentQty = inProcessLocationDetail.Qty - receivedQty;

            return transformerDetail;
        }



        /// <summary>
        /// 拣货
        /// </summary>
        /// <param name="pickListDetail"></param>
        /// <returns></returns>
        public static TransformerDetail ConvertPickListDetailToTransformerDetail(PickListDetail pickListDetail)
        {
            TransformerDetail transformerDetail = ConvertOrderLocationTransactionToTransformerDetail(pickListDetail.OrderLocationTransaction);

            transformerDetail.HuId = pickListDetail.HuId;
            transformerDetail.LotNo = pickListDetail.LotNo;
            transformerDetail.Qty = pickListDetail.Qty;
            transformerDetail.StorageBinCode = pickListDetail.StorageBin == null ? string.Empty : pickListDetail.StorageBin.Code;

            if (pickListDetail.PickListResults != null && pickListDetail.PickListResults.Count > 0)
            {
                foreach (PickListResult pickListResult in pickListDetail.PickListResults)
                {
                    transformerDetail.Qty -= pickListResult.Qty;
                }
            }
            return transformerDetail;
        }

        public static TransformerDetail ConvertPickListResultToTransformerDetail(PickListResult pickListResult)
        {
            TransformerDetail transformerDetail = ConvertOrderLocationTransactionToTransformerDetail(pickListResult.PickListDetail.OrderLocationTransaction);
            transformerDetail.HuId = pickListResult.LocationLotDetail.Hu.HuId;
            transformerDetail.LotNo = pickListResult.LocationLotDetail.LotNo;
            transformerDetail.Qty = pickListResult.Qty / pickListResult.PickListDetail.OrderLocationTransaction.UnitQty;
            transformerDetail.CurrentQty = transformerDetail.Qty;//单位换算

            return transformerDetail;
        }

        public static List<TransformerDetail> ConvertRepackDetailListToTransformerDetailList(IList<RepackDetail> repackDetailList)
        {
            if (repackDetailList == null || repackDetailList.Count == 0)
            {
                return null;
            }
            List<TransformerDetail> transformerDetailList = new List<TransformerDetail>();
            foreach (RepackDetail repackDetail in repackDetailList)
            {
                TransformerDetail transformerDetail = ConvertRepackDetailToTransformerDetail(repackDetail);
                transformerDetailList.Add(transformerDetail);
            }
            return transformerDetailList;
        }

        public static TransformerDetail ConvertRepackDetailToTransformerDetail(RepackDetail repackDetail)
        {
            TransformerDetail transformerDetail = new TransformerDetail();
            if (repackDetail.LocationLotDetail.Hu != null)
            {
                transformerDetail = ConvertHuToTransformerDetail(repackDetail.LocationLotDetail.Hu);
                transformerDetail.Qty = repackDetail.Qty / repackDetail.LocationLotDetail.Hu.UnitQty;
                transformerDetail.CurrentQty = transformerDetail.Qty;
            }
            else
            {
                transformerDetail.ItemCode = repackDetail.LocationLotDetail.Item.Code;
                transformerDetail.ItemDescription = repackDetail.LocationLotDetail.Item.Description;

                transformerDetail.UomCode = repackDetail.LocationLotDetail.Item.Uom.Code;
                transformerDetail.UnitCount = repackDetail.LocationLotDetail.Item.UnitCount;

                transformerDetail.Qty = repackDetail.Qty;
                transformerDetail.CurrentQty = repackDetail.Qty;
            }

            transformerDetail.LocationLotDetId = repackDetail.LocationLotDetail.Id;
            transformerDetail.LocationCode = repackDetail.LocationLotDetail.Location.Code;
            transformerDetail.StorageBinCode = repackDetail.StorageBinCode;
            transformerDetail.IOType = repackDetail.IOType;

            return transformerDetail;
        }


        public static List<TransformerDetail> ConvertLocationLotDetailToTransformerDetail(IList<LocationLotDetail> locationLotDetailList, bool isPutAway)
        {
            if (locationLotDetailList == null || locationLotDetailList.Count == 0)
                return null;

            List<TransformerDetail> transformerDetailList = new List<TransformerDetail>();
            foreach (LocationLotDetail locationLotDetail in locationLotDetailList)
            {
                transformerDetailList.Add(ConvertLocationLotDetailToTransformerDetail(locationLotDetail, isPutAway));
            }
            return transformerDetailList;
        }

        public static TransformerDetail ConvertLocationLotDetailToTransformerDetail(LocationLotDetail locationLotDetail, bool isPutAway)
        {
            TransformerDetail transformerDetail = new TransformerDetail();
            transformerDetail.Id = locationLotDetail.Id;
            transformerDetail.LocationLotDetId = locationLotDetail.Id;
            transformerDetail.ItemCode = locationLotDetail.Item.Code;
            transformerDetail.ItemDescription = locationLotDetail.Item.Description;
            if (locationLotDetail.Hu != null)
            {
                transformerDetail.UomCode = locationLotDetail.Hu.Uom.Code;
                transformerDetail.UnitCount = locationLotDetail.Hu.UnitCount;
                transformerDetail.Qty = locationLotDetail.Qty / locationLotDetail.Hu.UnitQty;
                transformerDetail.CurrentQty = locationLotDetail.Qty / locationLotDetail.Hu.UnitQty;
            }
            else
            {
                transformerDetail.UomCode = locationLotDetail.Item.Uom.Code;
                transformerDetail.UnitCount = locationLotDetail.Item.UnitCount;
                transformerDetail.Qty = locationLotDetail.Qty;
                transformerDetail.CurrentQty = locationLotDetail.Qty;
            }
            transformerDetail.HuId = locationLotDetail.Hu.HuId;
            transformerDetail.LotNo = locationLotDetail.Hu.LotNo;
            transformerDetail.LocationCode = locationLotDetail.Location.Code;
            if (isPutAway)
                transformerDetail.StorageBinCode = locationLotDetail.NewStorageBin != null ? locationLotDetail.NewStorageBin.Code : null;
            else
                transformerDetail.StorageBinCode = locationLotDetail.StorageBin != null ? locationLotDetail.StorageBin.Code : null;

            return transformerDetail;
        }

        public static TransformerDetail ConvertInspectDetailToTransformerDetail(InspectOrderDetail inspectDetail)
        {
            TransformerDetail transformerDetail = new TransformerDetail();
            transformerDetail.Id = inspectDetail.Id;
            transformerDetail.ItemCode = inspectDetail.LocationLotDetail.Item.Code;
            transformerDetail.ItemDescription = inspectDetail.LocationLotDetail.Item.Description;
            if (inspectDetail.LocationLotDetail.Hu != null)
            {
                transformerDetail.UomCode = inspectDetail.LocationLotDetail.Hu.Uom.Code;
                transformerDetail.UnitCount = inspectDetail.LocationLotDetail.Hu.UnitCount;
                transformerDetail.Qty = inspectDetail.InspectedQty / inspectDetail.LocationLotDetail.Hu.UnitQty;
            }
            else
            {
                transformerDetail.UomCode = inspectDetail.LocationLotDetail.Item.Uom.Code;
                transformerDetail.UnitCount = inspectDetail.LocationLotDetail.Item.UnitCount;
                transformerDetail.Qty = inspectDetail.InspectedQty;
            }
            transformerDetail.HuId = inspectDetail.LocationLotDetail.Hu == null ? null : inspectDetail.LocationLotDetail.Hu.HuId;
            transformerDetail.LotNo = inspectDetail.LocationLotDetail.LotNo;
            transformerDetail.CurrentQty = 0;
            return transformerDetail;
        }

        public static Transformer ConvertInspectOrderToTransformer(InspectOrder inspectOrder)
        {
            Transformer transformer = new Transformer();
            foreach (InspectOrderDetail inspectDetail in inspectOrder.InspectOrderDetails)
            {
                TransformerDetail transformerDetail = new TransformerDetail();

                transformerDetail.QualifiedQty = inspectDetail.QualifiedQty.HasValue ? inspectDetail.QualifiedQty.Value : 0;
                transformerDetail.RejectedQty = inspectDetail.RejectedQty.HasValue ? inspectDetail.RejectedQty.Value : 0;
                transformerDetail.CurrentQty = 0;
                transformerDetail.CurrentRejectQty = 0;
                transformerDetail.HuId = inspectOrder.IsDetailHasHu ? inspectDetail.LocationLotDetail.Hu.HuId : null;
                transformerDetail.Id = inspectDetail.Id;
                transformerDetail.ItemCode = inspectDetail.LocationLotDetail.Item.Code;
                transformerDetail.ItemDescription = inspectDetail.LocationLotDetail.Item.Description;
                transformerDetail.LocationCode = inspectDetail.LocationFrom.Code;
                transformerDetail.LocationLotDetId = inspectDetail.LocationLotDetail.Id;
                transformerDetail.LotNo = inspectDetail.LocationLotDetail.LotNo;
                if (inspectDetail.LocationLotDetail.Hu != null)
                {
                    transformerDetail.Qty = inspectDetail.InspectQty / inspectDetail.LocationLotDetail.Hu.UnitQty;
                    transformerDetail.UomCode = inspectDetail.LocationLotDetail.Hu.Uom.Code;
                    transformerDetail.UnitCount = inspectDetail.LocationLotDetail.Hu.UnitCount;
                }
                else
                {
                    transformerDetail.Qty = inspectDetail.InspectQty;
                    transformerDetail.UomCode = inspectDetail.LocationLotDetail.Item.Uom.Code;
                    transformerDetail.UnitCount = inspectDetail.LocationLotDetail.Item.UnitCount;
                }
                transformerDetail.LocationFromCode = inspectDetail.LocationFrom.Code;
                transformerDetail.LocationToCode = inspectDetail.LocationTo.Code;

                transformer.AddTransformerDetail(transformerDetail);
            }
            return transformer;
        }
        #endregion

        public static void ProcessTransformer(List<Transformer> transformerList)
        {
            if (transformerList != null && transformerList.Count > 0)
            {
                foreach (Transformer transformer in transformerList)
                {
                    ProcessTransformer(transformer);
                }
            }
        }

        public static void ProcessTransformer(Transformer transformer)
        {
            if (transformer != null && transformer.TransformerDetails != null)
            {
                transformer.CurrentQty = GetSumCurrentQty(transformer);
                transformer.Cartons = GetSumCartons(transformer);
            }
        }

        public static int GetSumCartons(Transformer transformer)
        {
            int count = 0;
            if (transformer != null && transformer.TransformerDetails != null && transformer.TransformerDetails.Count > 0)
            {
                foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                {
                    if (transformerDetail.HuId != null && transformerDetail.HuId.Trim() != string.Empty && transformerDetail.CurrentQty != 0)
                        count++;
                }
            }
            return count;
        }

        public static decimal GetSumCurrentQty(Transformer transformer)
        {
            decimal currentQty = 0;
            if (transformer != null && transformer.TransformerDetails != null && transformer.TransformerDetails.Count > 0)
            {
                foreach (TransformerDetail transformerDetail in transformer.TransformerDetails)
                {
                    currentQty += transformerDetail.CurrentQty;
                }
            }
            return currentQty;
        }


        /// <summary>
        /// 生产收货转换
        /// </summary>
        /// <param name="orderHead"></param>
        /// <returns></returns>
        public static List<Transformer> ConvertOrderHeadToTransformers(OrderHead orderHead)
        {
            List<Transformer> transformers = new List<Transformer>();
            if (orderHead == null || orderHead.OrderDetails == null || orderHead.OrderDetails.Count == 0)
            {
                return null;
            }
            foreach (OrderDetail orderDetail in orderHead.OrderDetails)
            {
                transformers.Add(ConvertOrderDetailToTransformer(orderDetail));
            }
            return transformers;
        }

        public static Transformer ConvertOrderDetailToTransformer(OrderDetail orderDetail)
        {
            if (orderDetail == null)
            {
                return null;
            }
            Transformer transformer = new Transformer();
            transformer.ItemCode = orderDetail.Item.Code;
            transformer.ItemDescription = orderDetail.Item.Description;
            transformer.ReferenceItemCode = orderDetail.ReferenceItemCode;
            transformer.UomCode = orderDetail.Uom.Code;
            transformer.UnitCount = orderDetail.UnitCount;
            transformer.LocationFromCode = orderDetail.DefaultLocationFrom != null ? orderDetail.DefaultLocationFrom.Code : null;
            transformer.LocationToCode = orderDetail.DefaultLocationTo != null ? orderDetail.DefaultLocationTo.Code : null;
            transformer.OrderedQty = orderDetail.OrderedQty;
            transformer.Qty = orderDetail.OrderedQty;
            transformer.CurrentQty = orderDetail.GoodsReceiptLotSize.HasValue ? orderDetail.GoodsReceiptLotSize.Value : 0;
            transformer.ReceivedQty = orderDetail.ReceivedQty.HasValue ? orderDetail.ReceivedQty.Value : 0;
            transformer.RejectedQty = orderDetail.RejectedQty.HasValue ? orderDetail.RejectedQty.Value : 0;
            transformer.ScrapQty = orderDetail.ScrapQty.HasValue ? orderDetail.ScrapQty.Value : 0;
            transformer.Sequence = orderDetail.Sequence;
            transformer.ShippedQty = orderDetail.ShippedQty.HasValue ? orderDetail.ShippedQty.Value : 0;
            transformer.Id = orderDetail.Id;

            return transformer;
        }

        public static List<Transformer> ConvertMaterialInsToTransformers(IList<MaterialIn> materialIns)
        {
            if (materialIns == null)
            {
                return null;
            }
            List<Transformer> transformerList = new List<Transformer>();
            foreach (MaterialIn materialIn in materialIns)
            {
                Transformer transformer = ConvertMaterialInToTransformer(materialIn);
                transformerList.Add(transformer);
            }
            return transformerList;
        }

        public static Transformer ConvertMaterialInToTransformer(MaterialIn materialIn)
        {
            if (materialIn == null)
            {
                return null;
            }

            Transformer transformer = new Transformer();
            transformer.Operation = materialIn.Operation.HasValue ? materialIn.Operation.Value : 0;
            transformer.ItemCode = materialIn.RawMaterial.Code;
            transformer.ItemDescription = materialIn.RawMaterial.Description;
            transformer.UomCode = materialIn.RawMaterial.Uom.Code;
            transformer.UnitCount = materialIn.RawMaterial.UnitCount;
            transformer.Qty = 0M;
            transformer.CurrentQty = 0M;
            transformer.LocationCode = materialIn.Location == null ? string.Empty : materialIn.Location.Code;

            return transformer;
        }

        /// <summary>
        /// 合并相同Transformer
        /// </summary>
        /// <param name="transformers"></param>
        /// <returns></returns>
        public static List<Transformer> MergeTransformers(List<Transformer> transformers)
        {
            if (transformers == null)
            {
                return null;
            }

            List<Transformer> newTransformers = new List<Transformer>();
            foreach (Transformer transformer in transformers)
            {
                bool isMatch = false;
                foreach (Transformer newTransformer in newTransformers)
                {
                    if (transformer.ItemCode == newTransformer.ItemCode
                       && transformer.UnitCount == newTransformer.UnitCount
                       && transformer.UomCode == newTransformer.UomCode
                       && transformer.LocationCode == newTransformer.LocationCode)
                    {
                        newTransformer.Qty += transformer.Qty;
                        transformer.OrderedQty += transformer.OrderedQty;
                        isMatch = true;
                    }
                }
                if (!isMatch)
                {
                    newTransformers.Add(transformer);
                }
            }
            return newTransformers;
        }
    }
}
