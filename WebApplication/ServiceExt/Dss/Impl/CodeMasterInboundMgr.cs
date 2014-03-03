using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.Dss;

namespace com.Sconit.Service.Dss.Impl
{
    public class CodeMasterInboundMgr : AbstractInboundMgr
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssInbound");

        private IUomMgr uomMgr;

        private string[] uomFields = new string[] 
            {
                "Name",
                "Description"
            };

        public CodeMasterInboundMgr(IUomMgr uomMgr,
            IDssImportHistoryMgr dssImportHistoryMgr)
            : base(dssImportHistoryMgr)
        {
            this.uomMgr = uomMgr;
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
            string key = dssImportHistory[1]; //代码

            //计量单位
            if (key.Trim().ToUpper() == "PT_UM")
            {
                Uom uom = new Uom();
                uom.Code = dssImportHistory[2];//计量单位
                if (isUpdate)
                {
                    uom.Name = dssImportHistory[3];//名称
                    uom.Description = dssImportHistory[3];//描述
                }
                return uom;
            }
            else
            {
                throw new BusinessErrorException("Common.Business.Error.EntityNotExist", key);
            }
        }

        protected override void CreateOrUpdateObject(object obj)
        {
            Uom uom = (Uom)obj;

            Uom newUom = this.uomMgr.LoadUom(uom.Code);
            if (newUom == null)
            {
                this.uomMgr.CreateUom(uom);
            }
            else
            {
                CloneHelper.CopyProperty(uom, newUom, this.uomFields);
                this.uomMgr.UpdateUom(newUom);
            }
        }

        protected override void DeleteObject(object obj)
        {
            Uom uom = (Uom)obj;

            Uom newUom = this.uomMgr.LoadUom(uom.Code);
            if (newUom != null)
            {
                this.uomMgr.DeleteUom(newUom.Code);
            }
        }
    }
}
