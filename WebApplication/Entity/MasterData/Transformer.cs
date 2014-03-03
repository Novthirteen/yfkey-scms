using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class Transformer : EntityBase
    {
        public int Id { get; set; }
        public int OrderLocTransId { get; set; }
        public string OrderNo { get; set; }
        public int Sequence { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string ReferenceItemCode { get; set; }
        public string UomCode { get; set; }
        public decimal UnitCount { get; set; }
        public string LocationFromCode { get; set; }
        public string LocationToCode { get; set; }
        public string StorageBinCode { get; set; }
        public string LotNo { get; set; }
        public decimal OrderedQty { get; set; }
        public decimal ShippedQty { get; set; }
        public decimal ReceivedQty { get; set; }
        public int LocationLotDetId { get; set; }
        public string OddShipOption { get; set; }
        //用于上下架,投料
        public string LocationCode { get; set; }
        /// <summary>
        /// 待收、待发数量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 本次输入数量
        /// </summary>
        public decimal CurrentQty { get; set; }
        public decimal CurrentRejectQty { get; set; }
        public decimal ScrapQty { get; set; }
        public decimal RejectedQty { get; set; }
        public int Cartons { get; set; }
        public int Operation { get; set; }

        /// <summary>
        /// 调整数，收货调整使用 //Picklist待收数
        /// </summary>
        public decimal AdjustQty { get; set; }

        public List<TransformerDetail> TransformerDetails { get; set; }
        public void AddTransformerDetail(TransformerDetail transformerDetail)
        {
            if (transformerDetail != null)
            {
                if (this.TransformerDetails == null)
                {
                    this.TransformerDetails = new List<TransformerDetail>();
                }
                this.TransformerDetails.Add(transformerDetail);
            }
        }
    }

    /// <summary>
    /// 用于CS
    /// </summary>
    [Serializable]
    public class ReceiptNote : EntityBase
    {
        public int Sequence { get; set; }
        public string ReceiptNo { get; set; }
        public string IpNo { get; set; }
        public string OrderNo { get; set; }
        public string PartyFrom { get; set; }
        public string PartyTo { get; set; }
        public string Dock { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        //Hu
        public string HuId { get; set; }
        public string ItemDescription { get; set; }
        public decimal UnitCount { get; set; }
        public decimal Qty { get; set; }
        public string Status { get; set; }
        public string PrintUrl { get; set; }

    }
}
