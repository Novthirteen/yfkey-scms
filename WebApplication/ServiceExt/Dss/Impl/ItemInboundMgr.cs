using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity;
using com.Sconit.Entity.Dss;

namespace com.Sconit.Service.Dss.Impl
{
    public class ItemInboundMgr : AbstractInboundMgr
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssInbound");

        private IItemMgr itemMgr;
        private IUomMgr uomMgr;
        private IBomMgr bomMgr;
        private IRoutingMgr routingMgr;
        private IUserMgr userMgr;

        private string[] Item2ItemFields = new string[] 
            { 
                "Uom",
                "Desc1",
                "Desc2",
                "Type",
                "Bom",
                "Routing",
                "Category",
                "IsActive",
                "BackFlushMethod"
            };

        public ItemInboundMgr(IItemMgr itemMgr,
            IUomMgr uomMgr,
            IBomMgr bomMgr,
            IRoutingMgr routingMgr,
            IUserMgr userMgr,
            IDssImportHistoryMgr dssImportHistoryMgr,
            IGenericMgr genericMgr)
            : base(dssImportHistoryMgr, genericMgr)
        {
            this.itemMgr = itemMgr;
            this.uomMgr = uomMgr;
            this.bomMgr = bomMgr;
            this.routingMgr = routingMgr;
            this.userMgr = userMgr;
        }

        protected override void FillDssImportHistory(string[] lineData, DssImportHistory dssImportHistory)
        {
            if (lineData != null && lineData.Length > 0 && dssImportHistory != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (lineData[i] == "?")
                        lineData[i] = null;
                    else
                        dssImportHistory[i] = lineData[i];
                }

                dssImportHistory[5] = lineData[55];//P/M类型

                dssImportHistory[6] = lineData[74];//BOM
                dssImportHistory[7] = lineData[75];//Routing
                dssImportHistory[8] = lineData[5];//Category
                dssImportHistory[9] = lineData[9];//IsActive
                dssImportHistory[12] = lineData[12];//backflushmethod

                if (dssImportHistory[2] != null)
                    dssImportHistory[2] = dssImportHistory[2].ToUpper();//单位
            }
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
            Item item = new Item();
            item.Code = dssImportHistory[1]; //零件号
            if (isUpdate)
            {
                item.Uom = this.uomMgr.CheckAndLoadUom(dssImportHistory[2].ToUpper());//单位
                item.Desc1 = dssImportHistory[3];//描述1
                item.Desc2 = dssImportHistory[4];//描述2
               
                 item.Type = this.GetItemType(dssImportHistory[5], BusinessConstants.DSS_SYSTEM_CODE_QAD);//P/M类型
                if (dssImportHistory[6] != null && dssImportHistory[6].Trim() != string.Empty)
                {
                    item.Bom = this.bomMgr.CheckAndLoadBom(dssImportHistory[6]); //BOM
                }
                if (dssImportHistory[7] != null && dssImportHistory[7].Trim() != string.Empty)
                {
                    item.Routing = this.routingMgr.CheckAndLoadRouting(dssImportHistory[7]);//Routing
                }
                if (dssImportHistory[8] != null && dssImportHistory[8].Trim() != string.Empty)
                {
                    item.Category = dssImportHistory[8].Substring(1, 1);//Category
                    item.Plant = dssImportHistory[8].Substring(0, 1);//plant
                }
                if (dssImportHistory[9] != null && dssImportHistory[9].Trim() != string.Empty)
                {
                    item.IsActive = dssImportHistory[9].Trim().ToUpper() == "A" ? true : false;
                }
                if (dssImportHistory[12] != null && dssImportHistory[12].Trim() != string.Empty
                    && dssImportHistory[12].Trim().ToUpper() == "TL")
                {
                    item.BackFlushMethod = BusinessConstants.CODE_MASTER_BACKFLUSH_METHOD_VALUE_BATCH_FEED;
                }
                else
                {
                    item.BackFlushMethod = BusinessConstants.CODE_MASTER_BACKFLUSH_METHOD_VALUE_GOODS_RECEIVE;
                }
            }

            #region 默认值
            item.UnitCount = 1;
            item.LastModifyUser = this.userMgr.GetMonitorUser();
            item.LastModifyDate = DateTime.Now;
            #endregion

            return item;
        }

        protected override void CreateOrUpdateObject(object obj)
        {
            Item item = (Item)obj;

            Item newItem = this.itemMgr.LoadItem(item.Code);
            if (newItem == null)
            {
               // item.IsActive = true;
                this.itemMgr.CreateItem(item);
            }
            else
            {
                CloneHelper.CopyProperty(item, newItem, this.Item2ItemFields);
                this.itemMgr.UpdateItem(newItem);
            }
        }

        protected override void DeleteObject(object obj)
        {
            Item item = (Item)obj;

            Item newItem = this.itemMgr.LoadItem(item.Code);
            if (newItem != null)
            {
                newItem.IsActive = false;
                newItem.LastModifyUser = this.userMgr.GetMonitorUser();
                newItem.LastModifyDate = DateTime.Now;
                this.itemMgr.UpdateItem(newItem);
            }
        }

        #region Private Method
        private string GetItemType(string type, string externalSystemCode)
        {
            string result = BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_M;
            if (StringHelper.Eq(externalSystemCode, BusinessConstants.DSS_SYSTEM_CODE_QAD))
            {
                if (StringHelper.Eq(type, BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_P))
                {
                    result = BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_P;
                }
            }

            return result;
        }
        #endregion
    }
}
