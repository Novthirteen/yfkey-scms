using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using com.Sconit.Entity.Exception;

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class Resolver : EntityBase
    {
        public string ModuleType { get; set; }
        public string UserCode { get; set; }
        public string CodePrefix { get; set; }
        public string BarcodeHead { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string PickBy { get; set; }
        /// <summary>
        /// 传递输入值(HuId,ItemCode)
        /// </summary>
        public string Input { get; set; }
        public string BinCode { get; set; }
        //是否强制打箱
        public bool IsOddCreateHu { get; set; }
        public bool IsScanHu { get; set; }
        public bool NeedPrintAsn { get; set; }
        public bool NeedPrintReceipt { get; set; }
        public bool AutoPrintHu { get; set; }
        public bool AllowExceed { get; set; }
        public bool AllowCreateDetail { get; set; }
        public bool IsPickFromBin { get; set; }
        public string IOType { get; set; }
        public string Command { get; set; }
        public string Result { get; set; }
        public string OrderType { get; set; }
        public string AntiResolveHu { get; set; }
        public string PrintUrl { get; set; }
        public string ExternalOrderNo { get; set; }
        //用于移库和退库
        public string LocationCode { get; set; }
        public string LocationFormCode { get; set; }
        public string LocationToCode { get; set; }

        public List<Transformer> Transformers { get; set; }
        public List<ReceiptNote> ReceiptNotes { get; set; }
        //盘点时用于指定库格和物料 Item=string[0] ; Bin=string[1]
        public List<string[]> WorkingHours { get; set; }
        [XmlIgnore]
        public bool IsCSClient { get; set; }
        [XmlIgnore]//用于不整包收发(FulfillUnitCount)就不匹配单包装
        public bool FulfillUnitCount { get; set; }
        [XmlIgnore]//是否可以订单发货
        public bool IsShipByOrder { get; set; }

        public void AddTransformer(Transformer transformer)
        {
            if (transformer != null)
            {
                if (this.Transformers == null)
                {
                    this.Transformers = new List<Transformer>();
                }
                this.Transformers.Add(transformer);
            }
        }

        /// <summary>
        /// 用于创建订单类型的 增加明细.如:退货,移库,上下架,投料,盘点等
        /// 检查重复扫描
        /// 自动生成序号
        /// 匹配
        /// 自动新增明细未加控制
        /// </summary>
        /// <param name="transformerDetail"></param>
        public void AddTransformerDetail(TransformerDetail transformerDetail)
        {
            if (transformerDetail != null)
            {
                if (this.Transformers == null)
                {
                    this.Transformers = new List<Transformer>();
                }
                //检查重复扫描
                var oldtd =
                    from transformer in this.Transformers
                    from oldtransformerDetail in transformer.TransformerDetails
                    where oldtransformerDetail.HuId.ToLower() == transformerDetail.HuId.ToLower()
                    select oldtransformerDetail;
                if (oldtd.Count() == 1 && oldtd.Single().CurrentQty != 0M)
                {
                    throw new BusinessErrorException("Warehouse.Error.HuReScan", transformerDetail.HuId);
                }
                //自动生成序号
                var seq = from t in this.Transformers
                          from td in t.TransformerDetails
                          select td.Sequence;
                transformerDetail.Sequence = seq.Count() > 0 ? seq.Max() + 1 : 0;
                //匹配
                var query = this.Transformers.Where
                    (t => (t.ItemCode == transformerDetail.ItemCode
                           && t.UnitCount == transformerDetail.UnitCount
                           && t.UomCode == transformerDetail.UomCode
                           && t.StorageBinCode == transformerDetail.StorageBinCode
                           && t.LocationCode == transformerDetail.LocationCode
                           && t.LocationFromCode == transformerDetail.LocationFromCode
                           && t.LocationToCode == transformerDetail.LocationToCode));
                if (query.Count() == 1)
                {
                    Transformer transformer = query.Single();
                    if (oldtd.Count() == 1 && oldtd.Single().CurrentQty == 0M)
                    {
                        oldtd.Single().CurrentQty = transformerDetail.CurrentQty;
                        oldtd.Single().Sequence = transformerDetail.Sequence;
                    }
                    else
                    {
                        transformer.AddTransformerDetail(transformerDetail);
                    }
                    transformer.CurrentQty += transformerDetail.CurrentQty;
                    transformer.Qty += transformerDetail.Qty;
                    transformer.Cartons++;
                }
                else if (query.Count() == 0)
                {
                    Transformer transformer = new Transformer();
                    transformer.ItemCode = transformerDetail.ItemCode;
                    transformer.ItemDescription = transformerDetail.ItemDescription;
                    transformer.UomCode = transformerDetail.UomCode;
                    transformer.UnitCount = transformerDetail.UnitCount;
                    transformer.CurrentQty = transformerDetail.CurrentQty;
                    transformer.Qty = transformerDetail.Qty;
                    transformer.LocationCode = transformerDetail.LocationCode;
                    transformer.LocationFromCode = transformerDetail.LocationFromCode;
                    transformer.LocationToCode = transformerDetail.LocationToCode;
                    transformer.LotNo = transformerDetail.LotNo;
                    transformer.StorageBinCode = transformerDetail.StorageBinCode;
                    transformer.Cartons = 1;
                    transformer.Sequence = this.Transformers.Count > 0 ? this.Transformers.Max(t => t.Sequence) + 1 : 0;

                    transformer.AddTransformerDetail(transformerDetail);
                    this.Transformers.Add(transformer);
                }
                else
                {
                    throw new TechnicalException("com.Sconit.Entity.MasterData.Resolver:Line 147");
                }
            }

            //if (transformerDetail != null && transformerDetail.CurrentQty > 0)
            //{
            //    this.Transformers = this.Transformers == null ? new List<Transformer>() : this.Transformers;

            //    //检查重复扫描
            //    //CheckMatchHuExist(resolver, transformerDetail.HuId);

            //    //自动生成序号
            //    int seq = FindMaxSeq(this.Transformers);
            //    transformerDetail.Sequence = seq + 1;

            //    //匹配:严格匹配ItemCode/UomCode/UnitCount/StorageBinCode/LotNo
            //    var query = this.Transformers.Where
            //        (t => (string.Equals(t.ItemCode, transformerDetail.ItemCode, StringComparison.OrdinalIgnoreCase)
            //               && string.Equals(t.UomCode, transformerDetail.UomCode, StringComparison.OrdinalIgnoreCase)
            //               && (t.UnitCount == transformerDetail.UnitCount || t.UnitCount == transformerDetail.Qty)
            //               && string.Equals(t.StorageBinCode, transformerDetail.StorageBinCode, StringComparison.OrdinalIgnoreCase)
            //               && (t.LotNo == null || t.LotNo.Trim() == string.Empty || (string.Equals(t.LotNo, transformerDetail.LotNo, StringComparison.OrdinalIgnoreCase)))
            //              ));
            //    //匹配:如果没有匹配上,降低条件,匹配ItemCode/UomCode/UnitCount/StorageBinCode
            //    if (query.Count() == 0)
            //    {
            //        query = this.Transformers.Where
            //        (t => (string.Equals(t.ItemCode, transformerDetail.ItemCode, StringComparison.OrdinalIgnoreCase)
            //               && string.Equals(t.UomCode, transformerDetail.UomCode, StringComparison.OrdinalIgnoreCase)
            //               && (t.UnitCount == transformerDetail.UnitCount || t.UnitCount == transformerDetail.Qty)
            //               && string.Equals(t.StorageBinCode, transformerDetail.StorageBinCode, StringComparison.OrdinalIgnoreCase)
            //               ));
            //    }
            //    //匹配:如果没有匹配上,降低条件,匹配ItemCode/UomCode/UnitCount
            //    if (query.Count() == 0)
            //    {
            //        query = this.Transformers.Where
            //        (t => (string.Equals(t.ItemCode, transformerDetail.ItemCode, StringComparison.OrdinalIgnoreCase)
            //               && string.Equals(t.UomCode, transformerDetail.UomCode, StringComparison.OrdinalIgnoreCase)
            //               && (t.UnitCount == transformerDetail.UnitCount || t.UnitCount == transformerDetail.Qty)
            //               ));
            //    }
            //    //匹配:如果没有匹配上,降低条件,匹配ItemCode/UomCode/StorageBinCode
            //    if (query.Count() == 0)
            //    {
            //        query = this.Transformers.Where
            //        (t => (string.Equals(t.ItemCode, transformerDetail.ItemCode, StringComparison.OrdinalIgnoreCase)
            //               && string.Equals(t.UomCode, transformerDetail.UomCode, StringComparison.OrdinalIgnoreCase)
            //               && string.Equals(t.StorageBinCode, transformerDetail.StorageBinCode, StringComparison.OrdinalIgnoreCase)
            //               ));
            //    }
            //    //匹配:如果还是没有匹配上,再降低条件,匹配ItemCode/UomCode
            //    if (query.Count() == 0)
            //    {
            //        query = this.Transformers.Where
            //        (t => (string.Equals(t.ItemCode, transformerDetail.ItemCode, StringComparison.OrdinalIgnoreCase)
            //               && string.Equals(t.UomCode, transformerDetail.UomCode, StringComparison.OrdinalIgnoreCase)
            //               ));
            //    }
            //    //匹配:如果还是没有匹配上,再降低条件,匹配ItemCode
            //    if (query.Count() == 0)
            //    {
            //        query = this.Transformers.Where
            //        (t => (string.Equals(t.ItemCode, transformerDetail.ItemCode, StringComparison.OrdinalIgnoreCase)));
            //    }
            //    //如果没有匹配的Transformer,新增Transformer和TransformerDetail
            //    if (query.Count() == 0)
            //    {
            //        Transformer transformer = new Transformer();
            //        transformer.ItemCode = transformerDetail.ItemCode;
            //        transformer.ItemDescription = transformerDetail.ItemDescription;
            //        transformer.UomCode = transformerDetail.UomCode;
            //        transformer.UnitCount = transformerDetail.UnitCount;
            //        transformer.CurrentQty = transformerDetail.CurrentQty;
            //        transformer.Qty = transformerDetail.Qty;
            //        transformer.LocationCode = transformerDetail.LocationCode;
            //        transformer.LotNo = transformerDetail.LotNo;
            //        transformer.StorageBinCode = transformerDetail.StorageBinCode;
            //        transformer.Cartons = 1;
            //        transformer.Sequence = this.Transformers.Count > 0 ? this.Transformers.Max(t => t.Sequence) + 1 : 0;

            //        transformer.AddTransformerDetail(transformerDetail);
            //        this.Transformers.Add(transformer);
            //    }
            //    //如果有匹配的Transformer,新增与之相匹配的TransformerDetail
            //    else if (query.Count() == 1)
            //    {
            //        int tdSeq = query.Select(q => q.Sequence).Single();
            //        bool match = false;
            //        for (int i = 0; i < this.Transformers.Count; i++)
            //        {
            //            if (this.Transformers[i].Sequence == tdSeq)
            //            {
            //                //已有条码
            //                if (this.Transformers[i].TransformerDetails != null && this.Transformers[i].TransformerDetails.Count > 0)
            //                {
            //                    foreach (var td in this.Transformers[i].TransformerDetails)
            //                    {
            //                        if (td.HuId != null && td.HuId.ToLower() == transformerDetail.HuId.ToLower())
            //                        {
            //                            if (td.CurrentQty == 0M)
            //                            {
            //                                td.CurrentQty = transformerDetail.CurrentQty;
            //                                td.Sequence = transformerDetail.Sequence;
            //                                this.Transformers[i].CurrentQty += transformerDetail.CurrentQty;
            //                                this.Transformers[i].Cartons++;
            //                                match = true;
            //                            }
            //                            else
            //                            {
            //                                throw new BusinessErrorException("Warehouse.Error.HuReScan", transformerDetail.HuId);
            //                            }
            //                            break;
            //                        }
            //                    }
            //                }
            //                //没有条码
            //                if (!match)
            //                {
            //                    this.Transformers[i].AddTransformerDetail(transformerDetail);
            //                    this.Transformers[i].CurrentQty += transformerDetail.CurrentQty;
            //                    this.Transformers[i].Cartons++;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}
            this.Command = BusinessConstants.CS_BIND_VALUE_TRANSFORMERDETAIL;
        }

        private int FindMaxSeq(List<Transformer> transformers)
        {
            int maxSeq = 0;
            if (transformers != null)
            {
                foreach (Transformer transformer in transformers)
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

    }
}
