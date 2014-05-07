using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Sconit.Entity.EDI
{
    [Serializable]
    public class EDIFordPlan : EDIFordPlanBase
    {
        #region Non O/R Mapping Properties

        //public Dictionary<DateTime, decimal[]> PlanDateDic { get; set; }

        public string PlanDateString { get; set; }

        public List<decimal[]> PlanQtyArr { get; set; }

        /// <summary>
        /// 总毛重
        /// </summary>
        public decimal? GrossWeight { get; set; }

        /// <summary>
        /// 总净重
        /// </summary>
        public decimal? NetWeight { get; set; }

        /// <summary>
        /// 毛重净重单位
        /// </summary>
        public string WeightUom { get; set; }


        /// <summary>
        /// 外包装类型
        /// </summary>
        public string OutPackType { get; set; }

        /// <summary>
        /// 外包装类型数量
        /// </summary>
        public decimal? OutPackQty { get; set; }


        /// <summary>
        /// 承运商
        /// </summary>
        public string CarrierCode { get; set; }

        /// <summary>
        /// 运输方式
        /// </summary>
        public string TransportationMethod { get; set; }

        /// <summary>
        /// 运载媒介
        /// </summary>
        public string EquipmentDesc { get; set; }

        /// <summary>
        /// 运载媒介的序列号
        /// </summary>
        public string EquipmentNum { get; set; }

        /// <summary>
        /// 提单号
        /// </summary>
        public string LadingNum { get; set; }

        /// <summary>
        /// 发货量
        /// </summary>
        public decimal? ShipQty { get; set; }

        /// <summary>
        /// 发货累计量
        /// </summary>
        public decimal? ShipQtyCum { get; set; }


        /// <summary>
        /// 内包装类型
        /// </summary>
        public string InPackType { get; set; }

        /// <summary>
        /// 内包装类型个数
        /// </summary>
        public decimal? InPackQty { get; set; }
        /// <summary>
        /// 每个包装数量
        /// </summary>
        public decimal? PerLoadQty { get; set; }

        /// <summary>
        /// 机场代码
        /// </summary>
        public string AirportCode { get; set; }


        public string Purpose { get; set; }

        /// <summary>
        /// 装箱单号
        /// </summary>
        public string ShipmentID { get; set; }




        #endregion
    }
}
