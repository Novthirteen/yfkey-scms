using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class TransformerDetail : EntityBase
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string HuId { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string UomCode { get; set; }
        public decimal UnitCount { get; set; }
        public string LotNo { get; set; }
        public string StorageBinCode { get; set; }
        public string Status { get; set; }
        public string LocationCode { get; set; }
        public int LocationLotDetId { get; set; }
        public int OrderLocTransId { get; set; }
        public string IOType { get; set; }
        /// <summary>
        /// 待收、待发数量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 本次输入数量
        /// </summary>
        public decimal CurrentQty { get; set; }
        public decimal CurrentRejectQty { get; set; }
        //public bool? IsQualified { get; set; }
        [XmlIgnore]
        public string StorageAreaCode { get; set; }
        [XmlIgnore]
        public int? Operation { get; set; }
        /// <summary>
        /// 合格数量
        /// </summary>
        [XmlIgnore]
        public decimal QualifiedQty { get; set; }
        /// <summary>
        /// 不合格数量
        /// </summary>
        [XmlIgnore]
        public decimal RejectedQty { get; set; }

        /// <summary>
        /// 来源库位
        /// </summary>
        [XmlIgnore]
        public string LocationFromCode { get; set; }
        /// <summary>
        /// 目的库位
        /// </summary>
        [XmlIgnore]
        public string LocationToCode { get; set; }

        /// <summary>
        /// 订单号，参考物料号，调整数，收货调整使用
        /// </summary>
        [XmlIgnore]
        public string OrderNo { get; set; }
        [XmlIgnore]
        public string ReferenceItemCode { get; set; }
        [XmlIgnore]
        public decimal AdjustQty { get; set; }

        //[XmlIgnore]
        //public Transformer Transformer{ get; set; }
    }
}
