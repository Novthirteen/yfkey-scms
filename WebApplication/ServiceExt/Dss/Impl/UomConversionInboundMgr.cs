using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity.Dss;

namespace com.Sconit.Service.Dss.Impl
{
    public class UomConversionInboundMgr : AbstractInboundMgr
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssInbound");

        private IItemMgr itemMgr;
        private IUomMgr uomMgr;
        private IUomConversionMgr uomConversionMgr;

        private string[] fields = new string[] 
            {
                "BaseQty",
                "AlterQty"
            };

        public UomConversionInboundMgr(IItemMgr itemMgr,
            IUomMgr uomMgr,
            IUomConversionMgr uomConversionMgr,
            IDssImportHistoryMgr dssImportHistoryMgr,
            IGenericMgr genericMgr)
            : base(dssImportHistoryMgr, genericMgr)
        {
            this.itemMgr = itemMgr;
            this.uomMgr = uomMgr;
            this.uomConversionMgr = uomConversionMgr;
        }

        protected override object DeserializeForDelete(DssImportHistory dssImportHistory)
        {
            return this.Deserialize(dssImportHistory, false);
        }

        protected override object DeserializeForCreate(DssImportHistory dssImportHistory)
        {
            return this.Deserialize(dssImportHistory, true);
        }

        private object Deserialize(DssImportHistory dssImportHistory, bool isUpdate)
        {
            UomConversion uomConversion = new UomConversion();
            uomConversion.BaseUom = this.uomMgr.CheckAndLoadUom(dssImportHistory[1]);//基本单位
            uomConversion.AlterUom = this.uomMgr.CheckAndLoadUom(dssImportHistory[2]);//替代单位
            uomConversion.Item = this.itemMgr.CheckAndLoadItem(dssImportHistory[3]);//零件号
            if (isUpdate)
            {
                //基本单位:G,替代单位:KG,换算因子:1000 => 1 KG = 1000 G
                uomConversion.BaseQty = decimal.Parse(dssImportHistory[4]);//换算因子
                uomConversion.AlterQty = 1;
            }

            return uomConversion;
        }

        protected override void CreateOrUpdateObject(object obj)
        {
            UomConversion uomConversion = (UomConversion)obj;

            UomConversion newUomConversion = this.uomConversionMgr.LoadUomConversion(uomConversion.Item.Code, uomConversion.AlterUom.Code, uomConversion.BaseUom.Code);
            if (newUomConversion == null)
            {
                this.uomConversionMgr.CreateUomConversion(uomConversion);
            }
            else
            {
                CloneHelper.CopyProperty(uomConversion, newUomConversion, this.fields);
                this.uomConversionMgr.UpdateUomConversion(newUomConversion);
            }
        }

        protected override void DeleteObject(object obj)
        {
            UomConversion uomConversion = (UomConversion)obj;

            UomConversion newUomConversion = this.uomConversionMgr.LoadUomConversion(uomConversion.Item.Code, uomConversion.AlterUom.Code, uomConversion.BaseUom.Code);
            if (newUomConversion != null)
            {
                this.uomConversionMgr.DeleteUomConversion(newUomConversion.Id);
            }
        }
    }
}
