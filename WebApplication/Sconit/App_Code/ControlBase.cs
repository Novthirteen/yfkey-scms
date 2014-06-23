using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Service.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using System.IO;
using System.Text;
using System.ServiceModel.Channels;
using com.Sconit.Entity;
using com.Sconit.Service.Procurement;
using com.Sconit.Service.Distribution;
using com.Sconit.Service.Production;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.Batch;
using com.Sconit.Service.View;
using com.Sconit.Service.Business;
using com.Sconit.Service.Report;
using com.Sconit.Service.Transportation;
using com.Sconit.Service.MRP;
using com.Sconit.Service.Mes;
using com.Sconit.Service;
using com.Sconit.Service.EDI;

/// <summary>
/// Summary description for ControlBase
/// </summary>
namespace com.Sconit.Web
{
    public abstract class ControlBase : System.Web.UI.UserControl
    {

        #region 变量
        protected User CurrentUser
        {
            get
            {
                return (new SessionHelper(Page)).CurrentUser;
            }
        }

        private IEntityPreferenceMgr EntityPreferenceMgr
        {
            get
            {
                return GetService<IEntityPreferenceMgr>("EntityPreferenceMgr.service");
            }
        }

        private ILanguageMgr LanguageMgr
        {
            get
            {
                return GetService<ILanguageMgr>("LanguageMgr.service");
            }
        }

        private IDictionary<string, System.Web.UI.Control> _findControlHelperCache = new Dictionary<string, System.Web.UI.Control>();
        #endregion

        #region 构造函数
        public ControlBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region 页面事件
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter mywriter = new HtmlTextWriter(sw);
            base.Render(mywriter);
            string content = sb.ToString();
            if (CurrentUser != null && CurrentUser.UserLanguage != null && CurrentUser.UserLanguage != string.Empty)
            {
                content = LanguageMgr.ProcessLanguage(content, CurrentUser.UserLanguage);
            }
            else
            {
                EntityPreference defaultLanguage = EntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_LANGUAGE);
                content = LanguageMgr.ProcessLanguage(content, defaultLanguage.Value);
            }
            writer.Write(content);
        }

        /*
         * 用于反射调用,参见GridView
         * 
         */ 
        public string Render(String content)
        {

            if (CurrentUser != null && CurrentUser.UserLanguage != null && CurrentUser.UserLanguage != string.Empty)
            {
                content = TheLanguageMgr.ProcessLanguage(content, CurrentUser.UserLanguage);
            }
            else
            {
                EntityPreference defaultLanguage = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_LANGUAGE);
                content = TheLanguageMgr.ProcessLanguage(content, defaultLanguage.Value);
            }
            return content;
            
        }


        public HttpResponse GetResponse()
        {
            return Response;
        }
        #endregion

        #region 方法
        protected T GetService<T>(string serviceName)
        {
            return ServiceLocator.GetService<T>(serviceName);
        }

        /// <summary>
        /// This helper automates locating a control by ID.
        /// 
        /// It calls FindControl on the NamingContainer, then the Page.  If that fails,
        /// it fires the resolve event.
        /// </summary>
        /// <param name="id">The ID of the control to find</param>
        /// <param name="props">The TargetProperties class associated with that control</param>
        /// <returns></returns>
        protected System.Web.UI.Control FindControlHelper(string id)
        {
            System.Web.UI.Control c = null;
            if (_findControlHelperCache.ContainsKey(id))
            {
                c = _findControlHelperCache[id];
            }
            else
            {
                c = base.FindControl(id);  // Use "base." to avoid calling self in an infinite loop
                System.Web.UI.Control nc = NamingContainer;
                while ((null == c) && (null != nc))
                {
                    c = nc.FindControl(id);
                    nc = nc.NamingContainer;
                }
                //if (null == c)
                //{
                //    // Note: props MAY be null, but we're firing the event anyway to let the user
                //    // do the best they can
                //    ResolveControlEventArgs args = new ResolveControlEventArgs(id);

                //    OnResolveControlID(args);
                //    c = args.Control;

                //}
                if (null != c)
                {
                    _findControlHelperCache[id] = c;
                }
            }
            return c;
        }

        public override System.Web.UI.Control FindControl(string id)
        {
            // Use FindControlHelper so that more complete searching and OnResolveControlID will be used
            return FindControlHelper(id);
        }
        #endregion

        #region Services
        protected ICriteriaMgr TheCriteriaMgr { get { return GetService<ICriteriaMgr>("CriteriaMgr.service"); } }
        protected IUserMgr TheUserMgr { get { return GetService<IUserMgr>("UserMgr.service"); } }
        protected IUserRoleMgr TheUserRoleMgr { get { return GetService<IUserRoleMgr>("UserRoleMgr.service"); } }
        protected IFileUploadMgr TheFileUploadMgr { get { return GetService<IFileUploadMgr>("FileUploadMgr.service"); } }
        protected IPermissionMgr ThePermissionMgr { get { return GetService<IPermissionMgr>("PermissionMgr.service"); } }
        protected ICodeMasterMgr TheCodeMasterMgr { get { return GetService<ICodeMasterMgr>("CodeMasterMgr.service"); } }
        protected IFavoritesMgr TheFavoritesMgr { get { return GetService<IFavoritesMgr>("FavoritesMgr.service"); } }
        protected IEntityPreferenceMgr TheEntityPreferenceMgr { get { return GetService<IEntityPreferenceMgr>("EntityPreferenceMgr.service"); } }
        protected IUserPreferenceMgr TheUserPreferenceMgr { get { return GetService<IUserPreferenceMgr>("UserPreferenceMgr.service"); } }
        protected ILanguageMgr TheLanguageMgr { get { return GetService<ILanguageMgr>("LanguageMgr.service"); } }
        protected IRegionMgr TheRegionMgr { get { return GetService<IRegionMgr>("RegionMgr.service"); } }
        protected ISupplierMgr TheSupplierMgr { get { return GetService<ISupplierMgr>("SupplierMgr.service"); } }
        protected ICarrierMgr TheCarrierMgr { get { return GetService<ICarrierMgr>("CarrierMgr.service"); } }
        protected ICustomerMgr TheCustomerMgr { get { return GetService<ICustomerMgr>("CustomerMgr.service"); } }
        protected IWorkCenterMgr TheWorkCenterMgr { get { return GetService<IWorkCenterMgr>("WorkCenterMgr.service"); } }
        protected IRoleMgr TheRoleMgr { get { return GetService<IRoleMgr>("RoleMgr.service"); } }
        protected IWorkdayMgr TheWorkdayMgr { get { return GetService<IWorkdayMgr>("WorkdayMgr.service"); } }
        protected IShiftMgr TheShiftMgr { get { return GetService<IShiftMgr>("ShiftMgr.service"); } }
        protected IShiftDetailMgr TheShiftDetailMgr { get { return GetService<IShiftDetailMgr>("ShiftDetailMgr.service"); } }
        protected ISpecialTimeMgr TheSpecialTimeMgr { get { return GetService<ISpecialTimeMgr>("SpecialTimeMgr.service"); } }
        protected IWorkCalendarMgr TheWorkCalendarMgr { get { return GetService<IWorkCalendarMgr>("WorkCalendarMgr.service"); } }
        protected IPartyMgr ThePartyMgr { get { return GetService<IPartyMgr>("PartyMgr.service"); } }
        protected IBomMgr TheBomMgr { get { return GetService<IBomMgr>("BomMgr.service"); } }
        protected IBomDetailMgr TheBomDetailMgr { get { return GetService<IBomDetailMgr>("BomDetailMgr.service"); } }
        protected ILocationMgr TheLocationMgr { get { return GetService<ILocationMgr>("LocationMgr.service"); } }
        protected ILocationTransactionMgr TheLocationTransactionMgr { get { return GetService<ILocationTransactionMgr>("LocationTransactionMgr.service"); } }
        protected IUserPermissionMgr TheUserPermissionMgr { get { return GetService<IUserPermissionMgr>("UserPermissionMgr.service"); } }
        protected IItemMgr TheItemMgr { get { return GetService<IItemMgr>("ItemMgr.service"); } }
        protected IItemReferenceMgr TheItemReferenceMgr { get { return GetService<IItemReferenceMgr>("ItemReferenceMgr.service"); } }
        protected IItemKitMgr TheItemKitMgr { get { return GetService<IItemKitMgr>("ItemKitMgr.service"); } }
        protected IUomMgr TheUomMgr { get { return GetService<IUomMgr>("UomMgr.service"); } }
        protected IUomConversionMgr TheUomConversionMgr { get { return GetService<IUomConversionMgr>("UomConversionMgr.service"); } }
        protected IRoutingMgr TheRoutingMgr { get { return GetService<IRoutingMgr>("RoutingMgr.service"); } }
        protected IRoutingDetailMgr TheRoutingDetailMgr { get { return GetService<IRoutingDetailMgr>("RoutingDetailMgr.service"); } }
        protected IRolePermissionMgr TheRolePermissionMgr { get { return GetService<IRolePermissionMgr>("RolePermissionMgr.service"); } }
        protected IPermissionCategoryMgr ThePermissionCategoryMgr { get { return GetService<IPermissionCategoryMgr>("PermissionCategoryMgr.service"); } }
        protected IAddressMgr TheAddressMgr { get { return GetService<IAddressMgr>("AddressMgr.service"); } }
        protected IBillAddressMgr TheBillAddressMgr { get { return GetService<IBillAddressMgr>("BillAddressMgr.service"); } }
        protected IShipAddressMgr TheShipAddressMgr { get { return GetService<IShipAddressMgr>("ShipAddressMgr.service"); } }
        protected IFlowMgr TheFlowMgr { get { return GetService<IFlowMgr>("FlowMgr.service"); } }
        protected IFlowDetailMgr TheFlowDetailMgr { get { return GetService<IFlowDetailMgr>("FlowDetailMgr.service"); } }
        protected IFlowBindingMgr TheFlowBindingMgr { get { return GetService<IFlowBindingMgr>("FlowBindingMgr.service"); } }
        protected IOrderMgr TheOrderMgr { get { return GetService<IOrderMgr>("OrderMgr.service"); } }
        protected IOrderHeadMgr TheOrderHeadMgr { get { return GetService<IOrderHeadMgr>("OrderHeadMgr.service"); } }
        protected IOrderDetailMgr TheOrderDetailMgr { get { return GetService<IOrderDetailMgr>("OrderDetailMgr.service"); } }
        protected IOrderLocationTransactionMgr TheOrderLocationTransactionMgr { get { return GetService<IOrderLocationTransactionMgr>("OrderLocationTransactionMgr.service"); } }
        protected IOrderOperationMgr TheOrderOperationMgr { get { return GetService<IOrderOperationMgr>("OrderOperationMgr.service"); } }
        protected IOrderBindingMgr TheOrderBindingMgr { get { return GetService<IOrderBindingMgr>("OrderBindingMgr.service"); } }
        protected IPriceListDetailMgr ThePriceListDetailMgr { get { return GetService<IPriceListDetailMgr>("PriceListDetailMgr.service"); } }
        protected INumberControlMgr TheNumberControlMgr { get { return GetService<INumberControlMgr>("NumberControlMgr.service"); } }
        protected IWorkdayShiftMgr TheWorkdayShiftMgr { get { return GetService<IWorkdayShiftMgr>("WorkdayShiftMgr.service"); } }
        protected ICurrencyMgr TheCurrencyMgr { get { return GetService<ICurrencyMgr>("CurrencyMgr.service"); } }
        protected IPriceListMgr ThePriceListMgr { get { return GetService<IPriceListMgr>("PriceListMgr.service"); } }
        protected ILocationDetailMgr TheLocationDetailMgr { get { return GetService<ILocationDetailMgr>("LocationDetailMgr.service"); } }
        protected ILocationLotDetailMgr TheLocationLotDetailMgr { get { return GetService<ILocationLotDetailMgr>("LocationLotDetailMgr.service"); } }
        protected IReceiptMgr TheReceiptMgr { get { return GetService<IReceiptMgr>("ReceiptMgr.service"); } }
        protected IReceiptDetailMgr TheReceiptDetailMgr { get { return GetService<IReceiptDetailMgr>("ReceiptDetailMgr.service"); } }
        protected IReceiptInProcessLocationMgr TheReceiptInProcessLocationMgr { get { return GetService<IReceiptInProcessLocationMgr>("ReceiptInProcessLocationMgr.service"); } }
        protected INamedQueryMgr TheNamedQueryMgr { get { return GetService<INamedQueryMgr>("NamedQueryMgr.service"); } }
        protected IHuMgr TheHuMgr { get { return GetService<IHuMgr>("HuMgr.service"); } }
        protected IHuOddMgr TheHuOddMgr { get { return GetService<IHuOddMgr>("HuOddMgr.service"); } }
        protected IMiscOrderMgr TheMiscOrderMgr { get { return GetService<IMiscOrderMgr>("MiscOrderMgr.service"); } }
        protected IMiscOrderDetailMgr TheMiscOrderDetailMgr { get { return GetService<IMiscOrderDetailMgr>("MiscOrderDetailMgr.service"); } }
        protected ICycleCountMgr TheCycleCountMgr { get { return GetService<ICycleCountMgr>("CycleCountMgr.service"); } }
        protected ICycleCountDetailMgr TheCycleCountDetailMgr { get { return GetService<ICycleCountDetailMgr>("CycleCountDetailMgr.service"); } }
        protected ICycleCountResultMgr TheCycleCountResultMgr { get { return GetService<ICycleCountResultMgr>("CycleCountResultMgr.service"); } }
        protected IPlannedBillMgr ThePlannedBillMgr { get { return GetService<IPlannedBillMgr>("PlannedBillMgr.service"); } }
        protected IBillTransactionMgr TheBillTransactionMgr { get { return GetService<IBillTransactionMgr>("BillTransactionMgr.service"); } }
        protected IPurchasePriceListMgr ThePurchasePriceListMgr { get { return GetService<IPurchasePriceListMgr>("PurchasePriceListMgr.service"); } }
        protected ILeanEngineMgr TheLeanEngineMgr { get { return GetService<ILeanEngineMgr>("LeanEngineMgr.service"); } }
        protected ISupplyChainMgr TheSupplyChainMgr { get { return GetService<ISupplyChainMgr>("SupplyChainMgr.service"); } }
        protected IAutoOrderTrackMgr TheAutoOrderTrackMgr { get { return GetService<IAutoOrderTrackMgr>("AutoOrderTrackMgr.service"); } }
        protected IItemFlowPlanMgr TheItemFlowPlanMgr { get { return GetService<IItemFlowPlanMgr>("ItemFlowPlanMgr.service"); } }
        protected IItemFlowPlanDetailMgr TheItemFlowPlanDetailMgr { get { return GetService<IItemFlowPlanDetailMgr>("ItemFlowPlanDetailMgr.service"); } }
        protected IItemFlowPlanTrackMgr TheItemFlowPlanTrackMgr { get { return GetService<IItemFlowPlanTrackMgr>("ItemFlowPlanTrackMgr.service"); } }
        protected IActingBillMgr TheActingBillMgr { get { return GetService<IActingBillMgr>("ActingBillMgr.service"); } }
        protected IBillMgr TheBillMgr { get { return GetService<IBillMgr>("BillMgr.service"); } }
        protected IBillDetailMgr TheBillDetailMgr { get { return GetService<IBillDetailMgr>("BillDetailMgr.service"); } }
        protected IShiftPlanScheduleMgr TheShiftPlanScheduleMgr { get { return GetService<IShiftPlanScheduleMgr>("ShiftPlanScheduleMgr.service"); } }
        protected ISalesPriceListMgr TheSalesPriceListMgr { get { return GetService<ISalesPriceListMgr>("SalesPriceListMgr.service"); } }
        protected IInProcessLocationMgr TheInProcessLocationMgr { get { return GetService<IInProcessLocationMgr>("InProcessLocationMgr.service"); } }
        protected IInProcessLocationDetailMgr TheInProcessLocationDetailMgr { get { return GetService<IInProcessLocationDetailMgr>("InProcessLocationDetailMgr.service"); } }
        protected IInProcessLocationTrackMgr TheInProcessLocationTrackMgr { get { return GetService<IInProcessLocationTrackMgr>("InProcessLocationTrackMgr.service"); } }
        protected IMaterialFlushBackMgr TheMaterialFlushBackMgr { get { return GetService<IMaterialFlushBackMgr>("MaterialFlushBackMgr.service"); } }
        protected IEmployeeMgr TheEmployeeMgr { get { return GetService<IEmployeeMgr>("EmployeeMgr.service"); } }
        protected IWorkingHoursMgr TheWorkingHoursMgr { get { return GetService<IWorkingHoursMgr>("WorkingHoursMgr.service"); } }
        protected IClientMonitorMgr TheClientMonitorMgr { get { return GetService<IClientMonitorMgr>("ClientMonitorMgr.service"); } }
        protected IClientLogMgr TheClientLogMgr { get { return GetService<IClientLogMgr>("ClientLogMgr.service"); } }
        protected IClientOrderHeadMgr TheClientOrderHeadMgr { get { return GetService<IClientOrderHeadMgr>("ClientOrderHeadMgr.service"); } }
        protected IClientOrderDetailMgr TheClientOrderDetailMgr { get { return GetService<IClientOrderDetailMgr>("ClientOrderDetailMgr.service"); } }
        protected IClientWorkingHoursMgr TheClientWorkingHoursMgr { get { return GetService<IClientWorkingHoursMgr>("ClientWorkingHoursMgr.service"); } }
        protected IClientMgr TheClientMgr { get { return GetService<IClientMgr>("ClientMgr.service"); } }
        protected IPickListMgr ThePickListMgr { get { return GetService<IPickListMgr>("PickListMgr.service"); } }
        protected IPickListDetailMgr ThePickListDetailMgr { get { return GetService<IPickListDetailMgr>("PickListDetailMgr.service"); } }
        protected IPickListResultMgr ThePickListResultMgr { get { return GetService<IPickListResultMgr>("PickListResultMgr.service"); } }
        protected IStorageBinMgr TheStorageBinMgr { get { return GetService<IStorageBinMgr>("StorageBinMgr.service"); } }
        protected IStorageAreaMgr TheStorageAreaMgr { get { return GetService<IStorageAreaMgr>("StorageAreaMgr.service"); } }
        protected IReportMgr TheReportMgr { get { return GetService<IReportMgr>("ReportMgr.service"); } }
        protected IImportMgr TheImportMgr { get { return GetService<IImportMgr>("ImportMgr.service"); } }
        protected IRepackMgr TheRepackMgr { get { return GetService<IRepackMgr>("RepackMgr.service"); } }
        protected IRepackDetailMgr TheRepackDetailMgr { get { return GetService<IRepackDetailMgr>("RepackDetailMgr.service"); } }
        protected IScanBarcodeMgr TheScanBarcodeMgr { get { return GetService<IScanBarcodeMgr>("ScanBarcodeMgr.service"); } }
        protected IBatchTriggerMgr TheBatchTriggerMgr { get { return GetService<IBatchTriggerMgr>("BatchTriggerMgr.service"); } }
        protected IInspectOrderMgr TheInspectOrderMgr { get { return GetService<IInspectOrderMgr>("InspectOrderMgr.service"); } }
        protected IInspectOrderDetailMgr TheInspectOrderDetailMgr { get { return GetService<IInspectOrderDetailMgr>("InspectOrderDetailMgr.service"); } }
        protected IProductLineInProcessLocationDetailMgr TheProductLineInProcessLocationDetailMgr { get { return GetService<IProductLineInProcessLocationDetailMgr>("ProductLineInProcessLocationDetailMgr.service"); } }
        protected IOrderDetailViewMgr TheOrderDetailViewMgr { get { return GetService<IOrderDetailViewMgr>("OrderDetailViewMgr.service"); } }
        protected IOrderLocTransViewMgr TheOrderLocTransViewMgr { get { return GetService<IOrderLocTransViewMgr>("OrderLocTransViewMgr.service"); } }
        protected IPlannedBillViewMgr ThePlannedBillViewMgr { get { return GetService<IPlannedBillViewMgr>("PlannedBillViewMgr.service"); } }
        protected IBillAgingViewMgr TheBillAgingViewMgr { get { return GetService<IBillAgingViewMgr>("BillAgingViewMgr.service"); } }
        protected IResolverMgr TheResolverMgr { get { return GetService<IResolverMgr>("ResolverMgr.service"); } }
        protected ISubjectListMgr TheSubjectListMgr { get { return GetService<ISubjectListMgr>("SubjectListMgr.service"); } }
        protected IKPOrderMgr TheKPOrderMgr { get { return GetService<IKPOrderMgr>("KPOrderMgr.service"); } }
        protected IKPItemMgr TheKPItemMgr { get { return GetService<IKPItemMgr>("KPItemMgr.service"); } }
        protected IRollingForecastMgr TheRollingForecastMgr { get { return GetService<IRollingForecastMgr>("RollingForecastMgr.service"); } }
        protected IInspectResultMgr TheInspectResultMgr { get { return GetService<IInspectResultMgr>("InspectResultMgr.service"); } }
        protected IVehicleMgr TheVehicleMgr { get { return GetService<IVehicleMgr>("VehicleMgr.service"); } }
        protected ITransportationAddressMgr TheTransportationAddressMgr { get { return GetService<ITransportationAddressMgr>("TransportationAddressMgr.service"); } }
        protected ITransportationRouteMgr TheTransportationRouteMgr { get { return GetService<ITransportationRouteMgr>("TransportationRouteMgr.service"); } }
        protected ITransportationRouteDetailMgr TheTransportationRouteDetailMgr { get { return GetService<ITransportationRouteDetailMgr>("TransportationRouteDetailMgr.service"); } }
        protected ITransportPriceListMgr TheTransportPriceListMgr { get { return GetService<ITransportPriceListMgr>("TransportPriceListMgr.service"); } }
        protected ITransportPriceListDetailMgr TheTransportPriceListDetailMgr { get { return GetService<ITransportPriceListDetailMgr>("TransportPriceListDetailMgr.service"); } }
        protected ITransportationOrderMgr TheTransportationOrderMgr { get { return GetService<ITransportationOrderMgr>("TransportationOrderMgr.service"); } }
        protected IExpenseMgr TheExpenseMgr { get { return GetService<IExpenseMgr>("ExpenseMgr.service"); } }
        protected ITransportationActBillMgr TheTransportationActBillMgr { get { return GetService<ITransportationActBillMgr>("TransportationActBillMgr.service"); } }
        protected ITransportationBillMgr TheTransportationBillMgr { get { return GetService<ITransportationBillMgr>("TransportationBillMgr.service"); } }
        protected ITransportationBillDetailMgr TheTransportationBillDetailMgr { get { return GetService<ITransportationBillDetailMgr>("TransportationBillDetailMgr.service"); } }
        protected IFlowTrackMgr TheFlowTrackMgr { get { return GetService<IFlowTrackMgr>("FlowTrackMgr.service"); } }
        protected IFlowDetailTrackMgr TheFlowDetailTrackMgr { get { return GetService<IFlowDetailTrackMgr>("FlowDetailTrackMgr.service"); } }
     
        protected IShelfMgr TheShelfMgr { get { return GetService<IShelfMgr>("ShelfMgr.service"); } }
        protected IShelfItemMgr TheShelfItemMgr { get { return GetService<IShelfItemMgr>("ShelfItemMgr.service"); } }
        protected IProductLineUserMgr TheProductLineUserMgr { get { return GetService<IProductLineUserMgr>("ProductLineUserMgr.service"); } }
        protected IByMaterialMgr TheByMaterialMgr { get { return GetService<IByMaterialMgr>("ByMaterialMgr.service"); } }

        protected IGenericMgr TheGenericMgr { get { return GetService<IGenericMgr>("IGenericMgr.service"); } }
        protected IEDIMgr TheEDIMgr { get { return GetService<IEDIMgr>("IEDIMgr.service"); } }

        #region MRP
        protected ICustomerScheduleDetailMgr TheCustomerScheduleDetailMgr { get { return ServiceLocator.GetService<ICustomerScheduleDetailMgr>("CustomerScheduleDetailMgr.service"); } }
        protected ICustomerScheduleMgr TheCustomerScheduleMgr { get { return ServiceLocator.GetService<ICustomerScheduleMgr>("CustomerScheduleMgr.service"); } }
        protected IMrpMgr TheMrpMgr { get { return GetService<IMrpMgr>("MrpMgr.service"); } }
        protected IMrpShipPlanMgr TheMrpShipPlanMgr { get { return ServiceLocator.GetService<IMrpShipPlanMgr>("MrpShipPlanMgr.service"); } }
        protected IMrpShipPlanViewMgr TheMrpShipPlanViewMgr { get { return ServiceLocator.GetService<IMrpShipPlanViewMgr>("MrpShipPlanViewMgr.service"); } }
        protected IFlatBomMgr TheFlatBomMgr { get { return ServiceLocator.GetService<IFlatBomMgr>("FlatBomMgr.service"); } }
        
        #endregion

        #endregion
    }
}
