using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity;
using Castle.Services.Transaction;
using com.Sconit.Entity.Dss;
using com.Sconit.Service.Mes;
using com.Sconit.Entity.Mes;

namespace com.Sconit.Service.Dss.Impl
{
    public class BomDetailInboundMgr : AbstractInboundMgr
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssInbound");

        private IItemMgr itemMgr;
        private IUomMgr uomMgr;
        private IBomMgr bomMgr;
        private IUserMgr userMgr;
        private IBomDetailMgr bomDetailMgr;
        private IMesBomMgr mesBomMgr;
        private IMesBomDetailMgr mesBomDetailMgr;

        private string[] fields = new string[] 
            { 
                "RateQty",
                "StructureType",
                "EndDate",
                "ScrapPercentage",
                "Operation",
                "BackFlushMethod"
            };

        public BomDetailInboundMgr(IItemMgr itemMgr,
            IUomMgr uomMgr,
            IBomMgr bomMgr,
            IUserMgr userMgr,
            IBomDetailMgr bomDetailMgr,
            IDssImportHistoryMgr dssImportHistoryMgr,
            IMesBomMgr mesBomMgr,
            IMesBomDetailMgr mesBomDetailMgr,
            IGenericMgr genericMgr)
            : base(dssImportHistoryMgr, genericMgr)
        {
            this.itemMgr = itemMgr;
            this.uomMgr = uomMgr;
            this.bomMgr = bomMgr;
            this.userMgr = userMgr;
            this.mesBomMgr = mesBomMgr;
            this.bomDetailMgr = bomDetailMgr;
            this.mesBomDetailMgr = mesBomDetailMgr;
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
            Item bomItem = this.itemMgr.CheckAndLoadItem(dssImportHistory[1]);//bom头
            Item item = this.itemMgr.CheckAndLoadItem(dssImportHistory[2]);//零件号
            if (item.IsMes && bomItem.IsMes)
            {
                MesBomDetail mesBomDetail = new MesBomDetail();
                mesBomDetail.Bom = this.LoadMesBom(dssImportHistory[1]);//Bom代码
                mesBomDetail.Item = item;

                mesBomDetail.StartDate = dssImportHistory[4] != null ?
                    DssHelper.GetDate(dssImportHistory[4], BusinessConstants.DSS_SYSTEM_CODE_QAD) : new DateTime(2000, 1, 1);

                if (isUpdate)
                {
                    mesBomDetail.RateQty = decimal.Parse(dssImportHistory[5]);//用量
                    mesBomDetail.StructureType = this.GetStructureType(dssImportHistory[6], BusinessConstants.DSS_SYSTEM_CODE_QAD);//类型

                    if (dssImportHistory[8] != null) mesBomDetail.EndDate = DssHelper.GetDate(dssImportHistory[8], BusinessConstants.DSS_SYSTEM_CODE_QAD);//结束日期
                }
                return mesBomDetail;
            }
            else
            {
                BomDetail bomDetail = new BomDetail();
                bomDetail.Bom = this.LoadBom(dssImportHistory[1]);//Bom代码
                bomDetail.Item = item;//零件号
                bomDetail.Reference = dssImportHistory[3];//参考
                bomDetail.StartDate = dssImportHistory[4] != null ?
                    DssHelper.GetDate(dssImportHistory[4], BusinessConstants.DSS_SYSTEM_CODE_QAD) : new DateTime(2000, 1, 1);

                if (isUpdate)
                {
                    bomDetail.RateQty = decimal.Parse(dssImportHistory[5]);//用量
                    bomDetail.StructureType = this.GetStructureType(dssImportHistory[6], BusinessConstants.DSS_SYSTEM_CODE_QAD);//类型

                    if (dssImportHistory[8] != null) bomDetail.EndDate = DssHelper.GetDate(dssImportHistory[8], BusinessConstants.DSS_SYSTEM_CODE_QAD);//结束日期

                    //回冲方式从零件上取
                    bomDetail.BackFlushMethod = bomDetail.Item.BackFlushMethod;
                    bomDetail.ScrapPercentage = decimal.Parse(dssImportHistory[10]) / 100;//废品百分比

                    bomDetail.Operation = int.Parse(dssImportHistory[12]);

                }

                #region 默认值
                bomDetail.Uom = bomDetail.Item.Uom;
                bomDetail.Priority = 0;
                bomDetail.NeedPrint = true;
                bomDetail.IsShipScanHu = false;

                #endregion

                return bomDetail;
            }

        }

        protected override void CreateOrUpdateObject(object obj)
        {
            if (obj.GetType() == typeof(BomDetail))
            {
                BomDetail bomDetail = (BomDetail)obj;

                BomDetail newBomDetail = this.bomDetailMgr.GetBomDetail(bomDetail.Bom.Code, bomDetail.Item.Code);
                if (newBomDetail == null)
                {
                    this.bomDetailMgr.CreateBomDetail(bomDetail);
                    log.Debug("Create BomDetail:" + bomDetail.Bom.Code + "," + bomDetail.Item.Code + "," + bomDetail.RateQty.ToString("0.########"));
                }
                else
                {
                    CloneHelper.CopyProperty(bomDetail, newBomDetail, this.fields);
                    this.bomDetailMgr.UpdateBomDetail(newBomDetail);
                    log.Debug("Update BomDetail:" + bomDetail.Bom.Code + "," + bomDetail.Item.Code + "," + bomDetail.RateQty.ToString("0.########"));
                }
            }
            else if (obj.GetType() == typeof(MesBomDetail))
            {
                MesBomDetail bomDetail = (MesBomDetail)obj;

                MesBomDetail newBomDetail = this.mesBomDetailMgr.GetBomDetail(bomDetail.Bom.Code, bomDetail.Item.Code);
                if (newBomDetail == null)
                {
                    this.mesBomDetailMgr.CreateBomDetail(bomDetail);
                    log.Debug("Create MesBomDetail:" + bomDetail.Bom.Code + "," + bomDetail.Item.Code + "," + bomDetail.RateQty.ToString("0.########"));
                }
                else
                {
                    CloneHelper.CopyProperty(bomDetail, newBomDetail, this.fields);
                    this.mesBomDetailMgr.UpdateBomDetail(newBomDetail);
                    log.Debug("Update MesBomDetail:" + bomDetail.Bom.Code + "," + bomDetail.Item.Code + "," + bomDetail.RateQty.ToString("0.########"));
                }
                IList<MesBom> mesBomList = mesBomDetailMgr.GetRelatedBomDetail(bomDetail);
                if (mesBomList != null && mesBomList.Count > 0)
                {
                    foreach (MesBom mesBom in mesBomList)
                    {
                        mesBom.TransferFlag = true;
                        mesBomMgr.UpdateBom(mesBom);
                    }
                }
            }
        }

        protected override void DeleteObject(object obj)
        {

            if (obj.GetType() == typeof(BomDetail))
            {
                BomDetail bomDetail = (BomDetail)obj;
                BomDetail newBomDetail = this.bomDetailMgr.GetBomDetail(bomDetail.Bom.Code, bomDetail.Item.Code);
                if (newBomDetail != null)
                {
                    newBomDetail.EndDate = DateTime.Today.AddDays(-1);
                    this.bomDetailMgr.UpdateBomDetail(newBomDetail);
                    log.Debug("Update BomDetail to inactive");
                    // this.bomDetailMgr.DeleteBomDetail(newBomDetail.Id);
                }
            }
            else if (obj.GetType() == typeof(MesBomDetail))
            {
                MesBomDetail bomDetail = (MesBomDetail)obj;
                MesBomDetail newBomDetail = this.mesBomDetailMgr.GetBomDetail(bomDetail.Bom.Code, bomDetail.Item.Code);
                if (newBomDetail != null)
                {
                    newBomDetail.EndDate = DateTime.Today.AddDays(-1);
                    this.mesBomDetailMgr.UpdateBomDetail(newBomDetail);
                    log.Debug("Update MesBomDetail to inactive");
                }
                IList<MesBom> mesBomList = mesBomDetailMgr.GetRelatedBomDetail(bomDetail);
                if (mesBomList != null && mesBomList.Count > 0)
                {
                    foreach (MesBom mesBom in mesBomList)
                    {
                        mesBom.TransferFlag = true;
                        mesBomMgr.UpdateBom(mesBom);
                    }
                }
            }
        }

        #region Private Method
        private string GetStructureType(string type, string externalSystemCode)
        {
            string result = BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_N;
            if (externalSystemCode == BusinessConstants.DSS_SYSTEM_CODE_QAD)
            {
                if (type.Trim().ToUpper() == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_X)
                {
                    result = BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_X;
                }
                else if (type.Trim().ToUpper() == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_O)
                {
                    result = BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_O;
                }
            }

            return result;
        }

        [Transaction(TransactionMode.Requires)]
        private Bom LoadBom(string code)
        {
            Bom bom = this.bomMgr.LoadBom(code);
            if (bom == null)
            {
                bom = new Bom();
                bom.Code = code;
                Item item = this.itemMgr.LoadItem(code);
                if (item != null)
                {
                    bom.Description = item.Description;
                    bom.Uom = item.Uom;
                    bom.IsActive = true;
                }
                this.bomMgr.CreateBom(bom);
            }
            return bom;
        }

        [Transaction(TransactionMode.Requires)]
        private MesBom LoadMesBom(string code)
        {
            MesBom mesBom = this.mesBomMgr.LoadBom(code);
            if (mesBom == null)
            {
                mesBom = new MesBom();
                mesBom.Code = code;
                Item item = this.itemMgr.LoadItem(code);
                if (item != null)
                {
                    mesBom.Description = item.Description;
                    mesBom.Uom = item.Uom;
                    mesBom.IsActive = true;
                }
                this.mesBomMgr.CreateBom(mesBom);
            }
            return mesBom;
        }

        #endregion
    }
}
