using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Sconit.Entity.View
{
    /// <summary>
    /// 常用查询条件缓存对象
    /// 用途:定义使用频次较高的条件,逻辑较复杂的条件等
    /// </summary>
    [Serializable]
    public class CriteriaParam : EntityBase
    {
        public string UserCode { get; set; }

        public string Item { get; set; }

        public string TransactionType { get; set; }

        public string Shift { get; set; }

        public string[] Party { get; set; }

        public string[] Flow { get; set; }

        public string[] Location { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string OrderNo { get; set; }

        public string HuId { get; set; }

        public string BinCode { get; set; }

        public string LotNo { get; set; }

        public string LocationType { get; set; }
        //目标库位 djin 2012-08-02
        public string[] RefLoc { get; set; }

        #region Classified Properties
        public bool ClassifiedItem { get; set; }

        public bool ClassifiedParty { get; set; }

        public bool ClassifiedFlow { get; set; }

        public bool ClassifiedLocation { get; set; }

        public bool ClassifiedDate { get; set; }

        public bool ClassifiedShift { get; set; }

        public bool ClassifiedTransType { get; set; }

        public bool ClassifiedUser { get; set; }

        public bool ClassifiedOrderNo { get; set; }

        public bool ClassifiedHuId { get; set; }

        public bool ClassifiedBin { get; set; }

        public bool ClassifiedLotNo { get; set; }
        #endregion
    }
}
