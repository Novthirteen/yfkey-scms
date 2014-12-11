using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using com.Sconit.Service.Distribution.Impl;
using com.Sconit.Service.Distribution;
using com.Sconit.Service.Procurement;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.Distribution;
using Castle.Services.Transaction;
using com.Sconit.Entity.Dss;

namespace com.Sconit.Service.Dss.Impl
{
    public class PriceListInboundMgr : AbstractInboundMgr
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssInbound");

        private IPriceListMgr priceListMgr;
        private IPartyMgr partyMgr;
        private IUserMgr userMgr;
        private IItemMgr itemMgr;
        private IUomMgr uomMgr;
        private ICurrencyMgr currencyMgr;
        private IPriceListDetailMgr priceListDetailMgr;
        private ISalesPriceListMgr salesPriceListMgr;
        private IPurchasePriceListMgr purchasePriceListMgr;
        private ISupplierMgr supplierMgr;
        private ICustomerMgr customerMgr;

        private string[] fields = new string[] 
            { 
                "UnitPrice",
                "EndDate",
                "IsProvisionalEstimate"
            };

        public PriceListInboundMgr(IPriceListMgr priceListMgr,
            IUserMgr userMgr, IPartyMgr partyMgr,
            IItemMgr itemMgr, IUomMgr uomMgr,
            ICurrencyMgr currencyMgr, IPriceListDetailMgr priceListDetailMgr,
            ISalesPriceListMgr salesPriceListMgr, IPurchasePriceListMgr purchasePriceListMgr,
            IDssImportHistoryMgr dssImportHistoryMgr,
            ISupplierMgr supplierMgr,
            ICustomerMgr customerMgr,
            IGenericMgr genericMgr)
            : base(dssImportHistoryMgr, genericMgr)
        {
            this.priceListMgr = priceListMgr;
            this.userMgr = userMgr;
            this.partyMgr = partyMgr;
            this.itemMgr = itemMgr;
            this.uomMgr = uomMgr;
            this.currencyMgr = currencyMgr;
            this.priceListDetailMgr = priceListDetailMgr;
            this.salesPriceListMgr = salesPriceListMgr;
            this.purchasePriceListMgr = purchasePriceListMgr;
            this.supplierMgr = supplierMgr;
            this.customerMgr = customerMgr;
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
            PriceListDetail priceListDetail = new PriceListDetail();

            PriceList priceList = priceListMgr.LoadPriceList(dssImportHistory[1].Trim());
            if (priceList != null)
            {
                priceListDetail.PriceList = priceList;
            }
            else
            {
                Party party = partyMgr.CheckAndLoadParty(dssImportHistory[1].Trim());
                Supplier supplier = supplierMgr.LoadSupplier(dssImportHistory[1].Trim());
                if (supplier != null)
                {
                    priceListDetail.PriceList = this.LoadPurchasePriceList(dssImportHistory[1], party);//采购价格单
                }
                else
                {
                    priceListDetail.PriceList = this.LoadSalesPriceList(dssImportHistory[1], party);//销售价格单
                }
              
            }

            priceListDetail.Currency = this.currencyMgr.CheckAndLoadCurrency(dssImportHistory[2]);//货币
            priceListDetail.Item = this.itemMgr.CheckAndLoadItem(dssImportHistory[3]);//零件号
            priceListDetail.Uom = this.uomMgr.CheckAndLoadUom(dssImportHistory[4]);//单位
            priceListDetail.StartDate = dssImportHistory[6] != null ? DssHelper.GetDate(dssImportHistory[6], BusinessConstants.DSS_SYSTEM_CODE_QAD) : DateTime.Now;//开始日期
            if (isUpdate)
            {
                priceListDetail.UnitPrice = decimal.Parse(dssImportHistory[5]);//单价
                if (dssImportHistory[7] != null) priceListDetail.EndDate = DssHelper.GetDate(dssImportHistory[7], BusinessConstants.DSS_SYSTEM_CODE_QAD);//结束日期
            }

            #region 默认值
            priceListDetail.TaxCode = string.Empty;//todo
            priceListDetail.IsIncludeTax = false;
            priceListDetail.IsProvisionalEstimate = this.CheckProvisionalEstimate(priceListDetail.UnitPrice);
            #endregion

            return priceListDetail;
        }

        protected override void CreateOrUpdateObject(object obj)
        {
            PriceListDetail priceListDetail = (PriceListDetail)obj;

            priceListDetail.IsProvisionalEstimate = CheckProvisionalEstimate(priceListDetail.UnitPrice);


            PriceListDetail newPriceListDetail =
                this.priceListDetailMgr.LoadPriceListDetail(priceListDetail.PriceList.Code, priceListDetail.Currency.Code,
                priceListDetail.Item.Code, priceListDetail.Uom.Code, priceListDetail.StartDate);
            if (newPriceListDetail == null)
            {
                this.priceListDetailMgr.CreatePriceListDetail(priceListDetail);
            }
            else
            {
                CloneHelper.CopyProperty(priceListDetail, newPriceListDetail, this.fields);
                this.priceListDetailMgr.UpdatePriceListDetail(newPriceListDetail);
            }
        }

        protected override void DeleteObject(object obj)
        {
            PriceListDetail priceListDetail = (PriceListDetail)obj;

            PriceListDetail newPriceListDetail =
                 this.priceListDetailMgr.GetLastestPriceListDetail(priceListDetail.PriceList.Code, priceListDetail.Item.Code, priceListDetail.StartDate,
                 priceListDetail.Currency.Code);
            if (newPriceListDetail != null)
            {
                newPriceListDetail.EndDate = DateTime.Today.AddDays(-1);
                this.priceListDetailMgr.UpdatePriceListDetail(newPriceListDetail);
            }
        }

        #region Private Method
        /// <summary>
        /// YFK客户化,小数点后三四位为11的为暂沽价
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        private bool CheckProvisionalEstimate(decimal price)
        {
            int i = Convert.ToInt32((price * 10000) % 100);
            if (i == 11)
            {
                return true;
            }

            return false;
        }

        [Transaction(TransactionMode.Requires)]
        private SalesPriceList LoadSalesPriceList(string code, Party party)
        {
            SalesPriceList salesPriceList = salesPriceListMgr.LoadSalesPriceList(code);
            if (salesPriceList == null)
            {
                salesPriceList = new SalesPriceList();
                salesPriceList.Code = code;
                salesPriceList.Party = party;
                salesPriceList.IsActive = true;
                this.salesPriceListMgr.CreateSalesPriceList(salesPriceList);
            }
            return salesPriceList;
        }

        [Transaction(TransactionMode.Requires)]
        private PurchasePriceList LoadPurchasePriceList(string code, Party party)
        {
            PurchasePriceList purchasePriceList = purchasePriceListMgr.LoadPurchasePriceList(code);
            if (purchasePriceList == null)
            {
                purchasePriceList = new PurchasePriceList();
                purchasePriceList.Code = code;
                purchasePriceList.Party = party;
                purchasePriceList.IsActive = true;
                this.purchasePriceListMgr.CreatePurchasePriceList(purchasePriceList);
            }
            return purchasePriceList;
        }
        #endregion
    }
}
