using System;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Business;
using com.Sconit.Service.Distribution;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Procurement;
using com.Sconit.Service.Production;
using com.Sconit.Utility;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.View;
using com.Sconit.Service.EDI;

/// <summary>
/// Summary description for BaseWS
/// </summary>
public class BaseWS : System.Web.Services.WebService
{
    public BaseWS()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    protected T GetService<T>(string serviceName)
    {
        return ServiceLocator.GetService<T>(serviceName);
    }

    #region Services
    protected IUserMgr TheUserMgr { get { return GetService<IUserMgr>("UserMgr.service"); } }
    protected IPermissionMgr ThePermissionMgr { get { return GetService<IPermissionMgr>("PermissionMgr.service"); } }
    protected IFavoritesMgr TheFavoritesMgr { get { return GetService<IFavoritesMgr>("FavoritesMgr.service"); } }
    protected IEntityPreferenceMgr TheEntityPreferenceMgr { get { return GetService<IEntityPreferenceMgr>("EntityPreferenceMgr.service"); } }
    protected IUserPreferenceMgr TheUserPreferenceMgr { get { return GetService<IUserPreferenceMgr>("UserPreferenceMgr.service"); } }
    protected ILanguageMgr TheLanguageMgr { get { return GetService<ILanguageMgr>("LanguageMgr.service"); } }
    protected ILocationMgr TheLocationMgr { get { return GetService<ILocationMgr>("LocationMgr.service"); } }
    protected IItemMgr TheItemMgr { get { return GetService<IItemMgr>("ItemMgr.service"); } }
    protected IItemReferenceMgr TheItemReferenceMgr { get { return GetService<IItemReferenceMgr>("ItemReferenceMgr.service"); } }
    protected IFlowMgr TheFlowMgr { get { return GetService<IFlowMgr>("FlowMgr.service"); } }
    protected IFlowDetailMgr TheFlowDetailMgr { get { return GetService<IFlowDetailMgr>("FlowDetailMgr.service"); } }
    protected IPriceListDetailMgr ThePriceListDetailMgr { get { return GetService<IPriceListDetailMgr>("PriceListDetailMgr.service"); } }
    protected ILocationDetailMgr TheLocationDetailMgr { get { return GetService<ILocationDetailMgr>("LocationDetailMgr.service"); } }
    protected IEmployeeMgr TheEmployeeMgr { get { return GetService<IEmployeeMgr>("EmployeeMgr.service"); } }
    protected IResolverMgr TheResolverMgr { get { return GetService<IResolverMgr>("ResolverMgr.service"); } }
    protected ICriteriaMgr TheCriteriaMgr { get { return GetService<ICriteriaMgr>("CriteriaMgr.service"); } }
    protected ISupllierLocationViewMgr TheSupllierLocationViewMgr { get { return GetService<ISupllierLocationViewMgr>("SupllierLocationViewMgr.service"); } }
    //protected ICodeMasterMgr TheCodeMasterMgr { get { return GetService<ICodeMasterMgr>("CodeMasterMgr.service"); } }
    //protected IUserRoleMgr TheUserRoleMgr { get { return GetService<IUserRoleMgr>("UserRoleMgr.service"); } }
    //protected IFileUploadMgr TheFileUploadMgr { get { return GetService<IFileUploadMgr>("FileUploadMgr.service"); } }
    //protected IRegionMgr TheRegionMgr { get { return GetService<IRegionMgr>("RegionMgr.service"); } }
    //protected ISupplierMgr TheSupplierMgr { get { return GetService<ISupplierMgr>("SupplierMgr.service"); } }
    //protected ICustomerMgr TheCustomerMgr { get { return GetService<ICustomerMgr>("CustomerMgr.service"); } }
    //protected IWorkCenterMgr TheWorkCenterMgr { get { return GetService<IWorkCenterMgr>("WorkCenterMgr.service"); } }
    //protected IRoleMgr TheRoleMgr { get { return GetService<IRoleMgr>("RoleMgr.service"); } }
    //protected IWorkdayMgr TheWorkdayMgr { get { return GetService<IWorkdayMgr>("WorkdayMgr.service"); } }
    //protected IShiftMgr TheShiftMgr { get { return GetService<IShiftMgr>("ShiftMgr.service"); } }
    //protected ISpecialTimeMgr TheSpecialTimeMgr { get { return GetService<ISpecialTimeMgr>("SpecialTimeMgr.service"); } }
    //protected IWorkCalendarMgr TheWorkCalendarMgr { get { return GetService<IWorkCalendarMgr>("WorkCalendarMgr.service"); } }
    //protected IPartyMgr ThePartyMgr { get { return GetService<IPartyMgr>("PartyMgr.service"); } }
    //protected IBomMgr TheBomMgr { get { return GetService<IBomMgr>("BomMgr.service"); } }
    //protected IBomDetailMgr TheBomDetailMgr { get { return GetService<IBomDetailMgr>("BomDetailMgr.service"); } }
    //protected ILocationTransactionMgr TheLocationTransactionMgr { get { return GetService<ILocationTransactionMgr>("LocationTransactionMgr.service"); } }
    //protected IUserPermissionMgr TheUserPermissionMgr { get { return GetService<IUserPermissionMgr>("UserPermissionMgr.service"); } }
    //protected IItemKitMgr TheItemKitMgr { get { return GetService<IItemKitMgr>("ItemKitMgr.service"); } }
    //protected IUomMgr TheUomMgr { get { return GetService<IUomMgr>("UomMgr.service"); } }
    //protected IUomConversionMgr TheUomConversionMgr { get { return GetService<IUomConversionMgr>("UomConversionMgr.service"); } }
    //protected IRoutingMgr TheRoutingMgr { get { return GetService<IRoutingMgr>("RoutingMgr.service"); } }
    //protected IRoutingDetailMgr TheRoutingDetailMgr { get { return GetService<IRoutingDetailMgr>("RoutingDetailMgr.service"); } }
    //protected IRolePermissionMgr TheRolePermissionMgr { get { return GetService<IRolePermissionMgr>("RolePermissionMgr.service"); } }
    //protected IPermissionCategoryMgr ThePermissionCategoryMgr { get { return GetService<IPermissionCategoryMgr>("PermissionCategoryMgr.service"); } }
    //protected IAddressMgr TheAddressMgr { get { return GetService<IAddressMgr>("AddressMgr.service"); } }
    //protected IBillAddressMgr TheBillAddressMgr { get { return GetService<IBillAddressMgr>("BillAddressMgr.service"); } }
    //protected IShipAddressMgr TheShipAddressMgr { get { return GetService<IShipAddressMgr>("ShipAddressMgr.service"); } }
    //protected IFlowBindingMgr TheFlowBindingMgr { get { return GetService<IFlowBindingMgr>("FlowBindingMgr.service"); } }
    protected IOrderMgr TheOrderMgr { get { return GetService<IOrderMgr>("OrderMgr.service"); } }
    protected IOrderHeadMgr TheOrderHeadMgr { get { return GetService<IOrderHeadMgr>("OrderHeadMgr.service"); } }
    protected IOrderDetailMgr TheOrderDetailMgr { get { return GetService<IOrderDetailMgr>("OrderDetailMgr.service"); } }
    protected IOrderLocationTransactionMgr TheOrderLocationTransactionMgr { get { return GetService<IOrderLocationTransactionMgr>("OrderLocationTransactionMgr.service"); } }
    //protected IOrderOperationMgr TheOrderOperationMgr { get { return GetService<IOrderOperationMgr>("OrderOperationMgr.service"); } }
    //protected IOrderBindingMgr TheOrderBindingMgr { get { return GetService<IOrderBindingMgr>("OrderBindingMgr.service"); } }
    //protected INumberControlMgr TheNumberControlMgr { get { return GetService<INumberControlMgr>("NumberControlMgr.service"); } }
    //protected IWorkdayShiftMgr TheWorkdayShiftMgr { get { return GetService<IWorkdayShiftMgr>("WorkdayShiftMgr.service"); } }
    //protected ICurrencyMgr TheCurrencyMgr { get { return GetService<ICurrencyMgr>("CurrencyMgr.service"); } }
    //protected IPriceListMgr ThePriceListMgr { get { return GetService<IPriceListMgr>("PriceListMgr.service"); } }
    //protected ILocationLotDetailMgr TheLocationLotDetailMgr { get { return GetService<ILocationLotDetailMgr>("LocationLotDetailMgr.service"); } }
    //protected IReceiptMgr TheReceiptMgr { get { return GetService<IReceiptMgr>("ReceiptMgr.service"); } }
    //protected IReceiptDetailMgr TheReceiptDetailMgr { get { return GetService<IReceiptDetailMgr>("ReceiptDetailMgr.service"); } }
    //protected IReceiptInProcessLocationMgr TheReceiptInProcessLocationMgr { get { return GetService<IReceiptInProcessLocationMgr>("ReceiptInProcessLocationMgr.service"); } }
    //protected INamedQueryMgr TheNamedQueryMgr { get { return GetService<INamedQueryMgr>("NamedQueryMgr.service"); } }
    //protected IHuMgr TheHuMgr { get { return GetService<IHuMgr>("HuMgr.service"); } }
    //protected IHuOddMgr TheHuOddMgr { get { return GetService<IHuOddMgr>("HuOddMgr.service"); } }
    //protected IMiscOrderMgr TheMiscOrderMgr { get { return GetService<IMiscOrderMgr>("MiscOrderMgr.service"); } }
    //protected IMiscOrderDetailMgr TheMiscOrderDetailMgr { get { return GetService<IMiscOrderDetailMgr>("MiscOrderDetailMgr.service"); } }
    //protected ICycleCountMgr TheCycleCountMgr { get { return GetService<ICycleCountMgr>("CycleCountMgr.service"); } }
    //protected ICycleCountDetailMgr TheCycleCountDetailMgr { get { return GetService<ICycleCountDetailMgr>("CycleCountDetailMgr.service"); } }
    //protected IPlannedBillMgr ThePlannedBillMgr { get { return GetService<IPlannedBillMgr>("PlannedBillMgr.service"); } }
    //protected IBillTransactionMgr TheBillTransactionMgr { get { return GetService<IBillTransactionMgr>("BillTransactionMgr.service"); } }
    //protected IPurchasePriceListMgr ThePurchasePriceListMgr { get { return GetService<IPurchasePriceListMgr>("PurchasePriceListMgr.service"); } }
    //protected ILeanEngineMgr TheLeanEngineMgr { get { return GetService<ILeanEngineMgr>("LeanEngineMgr.service"); } }
    //protected ISupplyChainMgr TheSupplyChainMgr { get { return GetService<ISupplyChainMgr>("SupplyChainMgr.service"); } }
    //protected IAutoOrderTrackMgr TheAutoOrderTrackMgr { get { return GetService<IAutoOrderTrackMgr>("AutoOrderTrackMgr.service"); } }
    //protected IItemFlowPlanMgr TheItemFlowPlanMgr { get { return GetService<IItemFlowPlanMgr>("ItemFlowPlanMgr.service"); } }
    //protected IItemFlowPlanDetailMgr TheItemFlowPlanDetailMgr { get { return GetService<IItemFlowPlanDetailMgr>("ItemFlowPlanDetailMgr.service"); } }
    //protected IItemFlowPlanTrackMgr TheItemFlowPlanTrackMgr { get { return GetService<IItemFlowPlanTrackMgr>("ItemFlowPlanTrackMgr.service"); } }
    //protected IActingBillMgr TheActingBillMgr { get { return GetService<IActingBillMgr>("ActingBillMgr.service"); } }
    //protected IBillMgr TheBillMgr { get { return GetService<IBillMgr>("BillMgr.service"); } }
    //protected IBillDetailMgr TheBillDetailMgr { get { return GetService<IBillDetailMgr>("BillDetailMgr.service"); } }
    //protected IShiftPlanScheduleMgr TheShiftPlanScheduleMgr { get { return GetService<IShiftPlanScheduleMgr>("ShiftPlanScheduleMgr.service"); } }
    //protected ISalesPriceListMgr TheSalesPriceListMgr { get { return GetService<ISalesPriceListMgr>("SalesPriceListMgr.service"); } }
    protected IInProcessLocationMgr TheInProcessLocationMgr { get { return GetService<IInProcessLocationMgr>("InProcessLocationMgr.service"); } }
    //protected IInProcessLocationDetailMgr TheInProcessLocationDetailMgr { get { return GetService<IInProcessLocationDetailMgr>("InProcessLocationDetailMgr.service"); } }
    //protected IInProcessLocationTrackMgr TheInProcessLocationTrackMgr { get { return GetService<IInProcessLocationTrackMgr>("InProcessLocationTrackMgr.service"); } }
    //protected IMaterialFlushBackMgr TheMaterialFlushBackMgr { get { return GetService<IMaterialFlushBackMgr>("MaterialFlushBackMgr.service"); } }
    //protected IWorkingHoursMgr TheWorkingHoursMgr { get { return GetService<IWorkingHoursMgr>("WorkingHoursMgr.service"); } }
    //protected IClientMonitorMgr TheClientMonitorMgr { get { return GetService<IClientMonitorMgr>("ClientMonitorMgr.service"); } }
    //protected IClientLogMgr TheClientLogMgr { get { return GetService<IClientLogMgr>("ClientLogMgr.service"); } }
    //protected IClientOrderHeadMgr TheClientOrderHeadMgr { get { return GetService<IClientOrderHeadMgr>("ClientOrderHeadMgr.service"); } }
    //protected IClientOrderDetailMgr TheClientOrderDetailMgr { get { return GetService<IClientOrderDetailMgr>("ClientOrderDetailMgr.service"); } }
    //protected IClientWorkingHoursMgr TheClientWorkingHoursMgr { get { return GetService<IClientWorkingHoursMgr>("ClientWorkingHoursMgr.service"); } }
    //protected IClientMgr TheClientMgr { get { return GetService<IClientMgr>("ClientMgr.service"); } }
    //protected IPickListMgr ThePickListMgr { get { return GetService<IPickListMgr>("PickListMgr.service"); } }
    //protected IPickListDetailMgr ThePickListDetailMgr { get { return GetService<IPickListDetailMgr>("PickListDetailMgr.service"); } }
    //protected IPickListResultMgr ThePickListResultMgr { get { return GetService<IPickListResultMgr>("PickListResultMgr.service"); } }
    //protected IStorageBinMgr TheStorageBinMgr { get { return GetService<IStorageBinMgr>("StorageBinMgr.service"); } }
    //protected IStorageAreaMgr TheStorageAreaMgr { get { return GetService<IStorageAreaMgr>("StorageAreaMgr.service"); } }
    //protected IReportMgr TheReportMgr { get { return GetService<IReportMgr>("ReportMgr.service"); } }
    //protected IScanBarcodeMgr TheScanBarcodeMgr { get { return GetService<IScanBarcodeMgr>("ScanBarcodeMgr.service"); } }
    protected IEDIMgr TheEDIMgr { get { return GetService<IEDIMgr>("IEDIMgr.service"); } }
    #endregion

    #region 私有
    protected string RenderingLanguage(string content, string userCode, params string[] parameters)
    {
        try
        {
            content = ProcessMessage(content, parameters);
            if (userCode != null && userCode.Trim() != string.Empty)
            {
                User user = TheUserMgr.LoadUser(userCode, true, false);

                if (user != null && user.UserLanguage != null && user.UserLanguage != string.Empty)
                {
                    content = TheLanguageMgr.ProcessLanguage(content, user.UserLanguage);
                }
                else
                {
                    EntityPreference defaultLanguage = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_LANGUAGE);
                    content = TheLanguageMgr.ProcessLanguage(content, defaultLanguage.Value);
                }
            }
            else
            {
                EntityPreference defaultLanguage = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_LANGUAGE);
                content = TheLanguageMgr.ProcessLanguage(content, defaultLanguage.Value);
            }
        }
        catch (Exception ex)
        {
            return content;
        }
        return content;
    }

    private string ProcessMessage(string message, string[] paramters)
    {
        string messageParams = string.Empty;
        if (paramters != null && paramters.Length > 0)
        {
            //处理Message参数
            foreach (string para in paramters)
            {
                messageParams += "," + para;
            }
        }
        message = "${" + message + messageParams + "}";

        return message;
    }
    #endregion
}
