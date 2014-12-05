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
    public class BomInboundMgr : AbstractInboundMgr
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssInbound");

        private IItemMgr itemMgr;
        private IUomMgr uomMgr;
        private IBomMgr bomMgr;

        private string[] fields = new string[] 
            {
                "Description",
                "Uom"
            };

        public BomInboundMgr(IItemMgr itemMgr,
            IUomMgr uomMgr,
            IBomMgr bomMgr,
            IDssImportHistoryMgr dssImportHistoryMgr,
            IGenericMgr genericMgr)
            : base(dssImportHistoryMgr, genericMgr)
        {
            this.itemMgr = itemMgr;
            this.uomMgr = uomMgr;
            this.bomMgr = bomMgr;
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
            Bom bom = new Bom();
            bom.Code = dssImportHistory[1]; //Bom代码
            if (isUpdate)
            {
                bom.Description = dssImportHistory[2]; //说明
                bom.Uom = this.uomMgr.CheckAndLoadUom(dssImportHistory[3].ToUpper());//单位
            }

            return bom;
        }

        protected override void CreateOrUpdateObject(object obj)
        {
            Bom bom = (Bom)obj;

            Bom newBom = this.bomMgr.LoadBom(bom.Code);
            if (newBom == null)
            {
                bom.IsActive = true;
                this.bomMgr.CreateBom(bom);
            }
            else
            {
                CloneHelper.CopyProperty(bom, newBom, this.fields);
                this.bomMgr.UpdateBom(newBom);
            }
        }

        protected override void DeleteObject(object obj)
        {
            Bom bom = (Bom)obj;

            Bom newBom = this.bomMgr.LoadBom(bom.Code);
            if (newBom != null)
            {
                newBom.IsActive = false;
                this.bomMgr.UpdateBom(newBom);
            }
        }
    }
}
