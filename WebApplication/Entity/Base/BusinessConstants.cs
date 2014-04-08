using System;

/// <summary>
/// Summary description for BusinessConstants
/// </summary>
namespace com.Sconit.Entity
{
    public static class BusinessConstants
    {
        public static readonly string ROLE_EVERYONE = "everyone";

        public static readonly string PAGE_LIST_ACTION = "ListAction";
        public static readonly string PAGE_NEW_ACTION = "NewAction";
        public static readonly string PAGE_EDIT_ACTION = "EditAction";
        public static readonly string PAGE_SEARCH_ACTION = "SearchAction";

        public static readonly string PARTY_TYPE_REGION = "Region";
        public static readonly string PARTY_TYPE_SUPPLIER = "Supplier";
        public static readonly string PARTY_TYPE_CARRIER = "Carrier";
        public static readonly string PARTY_TYPE_CUSTOMER = "Customer";

        public static readonly string BILL_TRANS_TYPE_PO = "PO";
        public static readonly string BILL_TRANS_TYPE_SO = "SO";
        public static readonly string BILL_TRANS_TYPE_PO_BILL = "PO-BIll";
        public static readonly string BILL_TRANS_TYPE_SO_BILL = "SO-BIll";

        public static readonly string IO_TYPE_IN = "In";
        public static readonly string IO_TYPE_OUT = "Out";

        public static readonly string RESULT_SUCCESS = "Success";
        public static readonly string RESULT_FAIL = "Fail";

        public static readonly string PARTY_ADDRESS_TYPE_BILL_ADDRESS = "BillAddress";
        public static readonly string PARTY_ADDRESS_TYPE_SHIP_ADDRESS = "ShipAddress";

        //public static readonly string CODE_PREFIX_HU = "HU";
        public static readonly string CODE_PREFIX_ASN = "ASN";
        public static readonly string CODE_PREFIX_RECEIPT = "REC";
        public static readonly string CODE_PREFIX_BILL = "BIL";
        public static readonly string CODE_PREFIX_BILL_RED = "RED";
        public static readonly string CODE_PREFIX_MISCO = "MIS";
        public static readonly string CODE_PREFIX_CYCCNT = "CYC";
        public static readonly string CODE_PREFIX_PICKLIST = "PIK";
        public static readonly string CODE_PREFIX_REPACK = "REP";
        public static readonly string CODE_PREFIX_INSPECTION = "INS";
        public static readonly string CODE_PREFIX_ORDER = "ORD";
        public static readonly string CODE_PREFIX_INSPECTION_RESULT = "INR";
        public static readonly string CODE_PREFIX_INSPECTION_REJECT = "REJ";
        public static readonly string CODE_PREFIX_TRANSPORTATION_ORDER = "SHP";
        public static readonly string CODE_PREFIX_TRANSPORTATIONBILL = "TBL";
        public static readonly string CODE_PREFIX_TRANSPORTATIONBILL_RED = "TRED";
        public static readonly string CODE_PREFIX_IMPORT_TEMPEDI830 = "TEMPEDI830";
        public static readonly string CODE_PREFIX_IMPORT_TEMPEDI856 = "TEMPEDI856";
        public static readonly string CODE_PREFIX_IMPORT_TEMPEDI862 = "TEMPEDI862";

        public static readonly string ENTITY_PREFERENCE_CODE_DEFAULT_LANGUAGE = "Language";
        public static readonly string ENTITY_PREFERENCE_CODE_ORDER_LENGTH = "OrderLength";
        public static readonly string ENTITY_PREFERENCE_CODE_ALLOW_EXCEED_GI_GR = "AllowExceedGiGR";
        public static readonly string ENTITY_PREFERENCE_CODE_IS_SHOW_PRICE = "IsShowPrice";
        public static readonly string ENTITY_PREFERENCE_CODE_SEQ_INTERVAL = "SeqInterval";
        public static readonly string ENTITY_PREFERENCE_CODE_AMOUNT_DECIMAL_LENGTH = "AmountDecimalLength";
        public static readonly string ENTITY_PREFERENCE_CODE_BASE_CURRENCY = "BaseCurrency";
        public static readonly string ENTITY_PREFERENCE_CODE_FCLTOFCL = "FclToFcl";
        public static readonly string ENTITY_PREFERENCE_CODE_DEFAULT_RECEIPT_OPTION = "ReceiptQtyOpt";
        public static readonly string ENTITY_PREFERENCE_CODE_DEFAULT_RECEIPT_OPTION_ZERO = "0";
        public static readonly string ENTITY_PREFERENCE_CODE_DEFAULT_RECEIPT_OPTION_GOODS_RECEIPT_LOTSIZE = "GoodsReceiptLotSize";
        public static readonly string ENTITY_PREFERENCE_CODE_ALLOW_EXCEED_UC = "AllowExceedUC";
        public static readonly string ENTITY_PREFERENCE_CODE_IS_RECEIPT_ONE_ITEM = "IsReceiptOneItem";
        public static readonly string ENTITY_PREFERENCE_CODE_PICK_BY = "PickBy";
        public static readonly string ENTITY_PREFERENCE_CODE_AUTO_RETRIEVE_SUPPLIER_HU = "AutoRetrieveSupHu";
        public static readonly string ENTITY_PREFERENCE_CODE_RECALCULATE_WHEN_BILL = "RecalculateWhenBill";
        public static readonly string ENTITY_PREFERENCE_CODE_COMPANY_ID_MARK = "CompanyIdMark";
        public static readonly string ENTITY_PREFERENCE_CODE_COMPANY_CODE = "CompanyCode";
        public static readonly string ENTITY_PREFERENCE_CODE_PORTAL_PARAM = "PortalParam";
        public static readonly string ENTITY_PREFERENCE_CODE_NO_PRICE_LIST_RECEIPT = "NoPriceListReceipt";
        public static readonly string ENTITY_PREFERENCE_CODE_AUTO_CREATE_WHEN_DEAVING = "AutoCreateWhenDeaving";
        public static readonly string ENTITY_PREFERENCE_CODE_ALLOW_PART_QUALIFIED = "AllowPartQualified";
        public static readonly string ENTITY_PREFERENCE_CODE_DEFAULT_HU_TEMPLATE = "DefaultHuTemplate";
        public static readonly string ENTITY_PREFERENCE_CODE_MERGE_PICKLIST = "MergePickList";
        public static readonly string ENTITY_PREFERENCE_CODE_PUT_WHEN_CYCLE_COUNT = "PutWhenCycleCount";
        public static readonly string ENTITY_PREFERENCE_CODE_INSIDE_SALES_FLOW = "InsideSalesFlow";
        public static readonly string ENTITY_PREFERENCE_CODE_REPACK_SHIFT = "RepackShift";
        public static readonly string ENTITY_PREFERENCE_CODE_RECALCULATE_WHEN_TRANSPORTATIONBILL = "RecalculateWhenTransportationBill";
        public static readonly string ENTITY_PREFERENCE_CODE_PALLENTVOLUME = "PallentVolume";
        public static readonly string ENTITY_PREFERENCE_CODE_VALUATE_WHEN_COMPLETE = "ValuateWhenComplete";

        public static readonly string QUARTZ_JOB_LOG_STATUS = "QuartzStatus";
        public static readonly string QUARTZ_JOB_LOG_STATUS_SUCCESS = "Success";
        public static readonly string QUARTZ_JOB_LOG_STATUS_FAIL = "Fail";
        public static readonly string QUARTZ_JOB_LOG_STATUS_INPROCESS = "InProcess";

        public static readonly string QUARTZ_JOB_LOG_PIORITY = "QuartzPriority";

        public static readonly string PLAN_VIEW_TYPE_GROUPLINE = "GroupLine";
        public static readonly string PLAN_VIEW_TYPE_DEMAND = "Demand";
        public static readonly string PLAN_VIEW_TYPE_PLAN = "Plan";
        public static readonly string PLAN_VIEW_TYPE_ORDER = "Order";
        public static readonly string PLAN_VIEW_TYPE_PAB = "PAB";

        public static readonly string ORDER_OPERATION_EDIT_ORDER = "EditOrder";
        public static readonly string ORDER_OPERATION_EDIT_ORDER_DETAIL = "EditOrderDetail";
        public static readonly string ORDER_OPERATION_DELETE_ORDER = "DeleteOrder";
        public static readonly string ORDER_OPERATION_DELETE_ORDER_DETAIL = "DeleteOrderDetail";
        public static readonly string ORDER_OPERATION_SUBMIT_ORDER = "SubmitOrder";
        public static readonly string ORDER_OPERATION_START_ORDER = "StartOrder";
        public static readonly string ORDER_OPERATION_CANCEL_ORDER = "CancelOrder";
        public static readonly string ORDER_OPERATION_SHIP_ORDER = "ShipOrder";
        public static readonly string ORDER_OPERATION_RECEIVE_ORDER = "ReceiveOrder";
        public static readonly string ORDER_OPERATION_COMPLETE_ORDER = "CompleteOrder";
        public static readonly string ORDER_OPERATION_EDIT_ORDER_PRICE = "EditOrderPrice";
        public static readonly string ORDER_OPERATION_CLOSE_ORDER = "CloseOrder";
        public static readonly string PERMISSION_CATEGORY_TERMINAL = "Terminal";
        public static readonly string PERMISSION_PAGE_VALUE_AUTOPRINT = "AutoPrint";
        public static readonly string PERMISSION_PAGE_AUTOPRINT_VALUE_PAGE_PRODUCTIONORDERPRINT = "Page_ProductionOrderPrint";
        public static readonly string PERMISSION_PAGE_AUTOPRINT_VALUE_PAGE_PROCUREMENTORDERPRINT = "Page_ProcurementOrderPrint";
        public static readonly string PERMISSION_PAGE_AUTOPRINT_VALUE_PAGE_PICKLISTPRINT = "Page_PicklistPrint";
        public static readonly string PERMISSION_PAGE_AUTOPRINT_VALUE_PAGE_INSPECTIONPRINT = "Page_InspectionPrint";
        public static readonly string PERMISSION_PAGE_AUTOPRINT_VALUE_PAGE_ASNPRINT = "Page_ASNPrint";

        public static readonly string INVENTORY_REPORTS_INVDET = "InvDet";
        public static readonly string INVENTORY_REPORTS_HISINV = "HisInv";

        public static readonly string CREATE_HU_OPTION_SHIP = "Ship";
        public static readonly string CREATE_HU_OPTION_RECEIVE = "Receive";

        public static readonly string MINUS_INVENTORY = "Minus";
        public static readonly string PLUS_INVENTORY = "Plus";

        public static readonly string PHYCNT_MODULE_CYCCNT = "CycCnt";
        public static readonly string PHYCNT_MODULE_INVADJ = "InvAdj";

        public static readonly string SEARCH_MODE_CRITERIA = "Criteria";
        public static readonly string SEARCH_MODE_CUSTOMIZE = "Customize";

        public static readonly string SYSTEM_REGION = "System";

        public static readonly string SYSTEM_LOCATION_REJECT = "Reject";
        public static readonly string SYSTEM_LOCATION_INSPECT = "Inspect";

        public static readonly string SYSTEM_PARTY_FG = "YFK-FG";

        public static readonly string SYSTEM_REGION_RW = "YFK-RW";

        public static readonly string SYSTEM_USER_MONITOR = "Monitor";

        public static readonly string DATETIME_TYPE_YEAR = "Years";
        public static readonly string DATETIME_TYPE_MONTH = "Months";
        public static readonly string DATETIME_TYPE_DAY = "Days";
        public static readonly string DATETIME_TYPE_HOUR = "Hours";
        public static readonly string DATETIME_TYPE_MINUTE = "Minutes";
        public static readonly string DATETIME_TYPE_SECOND = "Seconds";
        public static readonly string DATETIME_TYPE_MILLISECOND = "Milliseconds";

        public static readonly string DEFAULT_SUPPLIER_ID_MARK = "1";
        public static readonly string DEFAULT_FINISHI_GOODS_ID_MARK = "1";

        public static readonly string PARTY_AUTHRIZE_OPTION_FROM = "from";
        public static readonly string PARTY_AUTHRIZE_OPTION_TO = "to";
        public static readonly string PARTY_AUTHRIZE_OPTION_BOTH = "both";

        public static readonly string LOCATION_TYPE_WORKSHOP = "02";

        public static readonly string TRANSPORTATION_VEHICLE_TYPE = "VehicleType";
        public static readonly string TRANSPORTATION_VEHICLE_TYPE_SCATTERED = "Scattered";
        public static readonly string TRANSPORTATION_VEHICLE_TYPE_2T = "2T";
        public static readonly string TRANSPORTATION_VEHICLE_TYPE_5T = "5T";
        public static readonly string TRANSPORTATION_VEHICLE_TYPE_8T = "8T";
        public static readonly string TRANSPORTATION_VEHICLE_TYPE_10T = "10T";
        public static readonly string TRANSPORTATION_VEHICLE_TYPE_12T = "12T";
        public static readonly string TRANSPORTATION_VEHICLE_TYPE_20FOOT = "20FOOT";
        public static readonly string TRANSPORTATION_VEHICLE_TYPE_40FOOT = "40FOOT";

        public static readonly string TRANSPORTATION_PRICING_METHOD_M3 = "M3";
        public static readonly string TRANSPORTATION_PRICING_METHOD_KG = "KG";
        public static readonly string TRANSPORTATION_PRICING_METHOD_SHIPT = "SHIPT";

        public static readonly string TRANSPORTATION_PRICELIST_DETAIL_TYPE_TRANSPORTATION = "Transportation";
        public static readonly string TRANSPORTATION_PRICELIST_DETAIL_TYPE_OPERATION = "Operation";
        public static readonly string TRANSPORTATION_PRICELIST_DETAIL_TYPE_WAREHOUSELEASE = "WarehouseLease";

        public static readonly string TRANSPORTATION_BILLING_METHOD_OUT = "Out";
        public static readonly string TRANSPORTATION_BILLING_METHOD_IN = "In";

        public static readonly string CODE_TRANSPORTATION_TRANSPORTATIONBILL_TYPE = "TransportationBillType";
        public static readonly string CODE_TRANSPORTATION_TRANSPORTATIONBILL_TYPE_VALUE_NORMAL = "Normal";
        public static readonly string CODE_TRANSPORTATION_TRANSPORTATIONBILL_TYPE_VALUE_CANCEL = "Cancel";

        public static readonly string TRACK_EVENT_CREATE = "Create";
        public static readonly string TRACK_EVENT_UPDATE= "Update";
        public static readonly string TRACK_EVENT_DELETE = "Delete";


        public static readonly string ENTITY_PREFERENCE_CODE_BATPATH = "BatPath"; //运行Bat文件目录
        public static readonly string ENTITY_PREFERENCE_CODE_SOURCEFILEPATH = "SourceFilePath"; //导入文件目录
        public static readonly string ENTITY_PREFERENCE_CODE_BAKFILEPATH = "BakFilePath";       //成功读取文件目录
        public static readonly string ENTITY_PREFERENCE_CODE_ERRORFILEPATH = "ErrorFilePath";    //读取文件失败目录
        public static readonly string ENTITY_PREFERENCE_CODE_ARCHIVEFOLDER = "ArchiveFolder";         //存档目录
        public static readonly string ENTITY_PREFERENCE_CODE_TEMPFOLDER = "TempFolder";         //      临时目录
        public static readonly string ENTITY_PREFERENCE_CODE_OUTFOLDER = "OutFolder";     //输出文件目录

        #region Template File Relative Path

        public static readonly string TEMPLATE_REPORTS_FILENAME_INVDET = "InvDetail";
        public static readonly string TEMPLATE_REPORTS_FILENAME_HISINV = "HisInventory";
        public static readonly string TEMPLATE_REPORTS_FILENAME_INVIOB = "InvIOB";

        public static readonly string TEMPLATE_FILE_PATH = "Reports/Templates/XMLTemplates/";
        public static readonly string TEMPLATE_EXCEL_FILE_PATH = "Reports/Templates/ExcelTemplates/";
        public static readonly string TEMP_FILE_PATH = "Reports/Templates/TempFiles/";

        #endregion

        #region Code Master
        public static readonly string CODE_MASTER_LANGUAGE = "Language";
        public static readonly string CODE_MASTER_LANGUAGE_VALUE_ZH_CN = "zh-CN";
        public static readonly string CODE_MASTER_LANGUAGE_VALUE_EN = "en";

        public static readonly string CODE_MASTER_USER_PREFERENCE = "UserPreference";
        public static readonly string CODE_MASTER_USER_PREFERENCE_VALUE_LANGUAGE = "Language";
        public static readonly string CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE = "ThemePage";
        public static readonly string CODE_MASTER_USER_PREFERENCE_VALUE_THEMEFRAME = "ThemeFrame";
        public static readonly string CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE_RANDOM = "Random";
        public static readonly string CODE_MASTER_USER_PREFERENCE_VALUE_THEMEFRAME_PICTURE = "Picture";
        public static readonly string CODE_MASTER_USER_PREFERENCE_VALUE_THEMEFRAME_DEFAULT = "Default";

        public static readonly string CODE_MASTER_GENDER = "Gender";
        public static readonly string CODE_MASTER_GENDER_VALUE_M = "M";
        public static readonly string CODE_MASTER_GENDER_VALUE_F = "F";

        public static readonly string CODE_MASTER_PERMISSION_CATEGORY_TYPE = "PermissionCategoryType";
        public static readonly string CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_ORGANIZATION = "Organization";
        public static readonly string CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_MENU = "Menu";
        public static readonly string CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_PAGE = "Page";
        public static readonly string CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_TERMINAL = "Terminal";
        public static readonly string CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_REGION = "Region";
        public static readonly string CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_SUPPLIER = "Supplier";
        public static readonly string CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_CARRIER = "Carrier";
        public static readonly string CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_CUSTOMER = "Customer";

        public static readonly string CODE_MASTER_FLOW_TYPE = "FlowType";
        public static readonly string CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION = "Production";
        public static readonly string CODE_MASTER_FLOW_TYPE_VALUE_INSPECTION = "Inspection";
        public static readonly string CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT = "Procurement";
        public static readonly string CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION = "Distribution";
        public static readonly string CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER = "Transfer";
        public static readonly string CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS = "CustomerGoods";
        public static readonly string CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING = "Subconctracting";

        public static readonly string CODE_MASTER_ORDER_TYPE = "OrderType";
        public static readonly string CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION = "Production";
        public static readonly string CODE_MASTER_ORDER_TYPE_VALUE_INSPECTION = "Inspection";
        public static readonly string CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT = "Procurement";
        public static readonly string CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION = "Distribution";
        public static readonly string CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER = "Transfer";
        public static readonly string CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS = "CustomerGoods";
        public static readonly string CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING = "Subconctracting";

        //public static readonly string ORDER_MODULETYPE_VALUE_SUPPLIERDISTRIBUTION = "SupplierDistribution";
        //public static readonly string ORDER_MODULETYPE_VALUE_PROCUREMENTCONFIRM = "ProcurementConfirm";

        public static readonly string CODE_MASTER_ORDER_SUB_TYPE = "OrderSubType";
        public static readonly string CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML = "Nml";
        public static readonly string CODE_MASTER_ORDER_SUB_TYPE_VALUE_ADJ = "Adj";
        public static readonly string CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN = "Rtn";
        public static readonly string CODE_MASTER_ORDER_SUB_TYPE_VALUE_RWO = "Rwo";

        public static readonly string CODE_MASTER_MISC_ORDER_TYPE = "MiscOrderType";
        public static readonly string CODE_MASTER_MISC_ORDER_TYPE_VALUE_GI = "Gi";
        public static readonly string CODE_MASTER_MISC_ORDER_TYPE_VALUE_GR = "Gr";
        public static readonly string CODE_MASTER_MISC_ORDER_TYPE_VALUE_ADJ = "Adj";

        public static readonly string CODE_MASTER_ITEM_TYPE = "ItemType";
        public static readonly string CODE_MASTER_ITEM_TYPE_VALUE_A = "A";
        public static readonly string CODE_MASTER_ITEM_TYPE_VALUE_K = "K";
        public static readonly string CODE_MASTER_ITEM_TYPE_VALUE_M = "M";
        public static readonly string CODE_MASTER_ITEM_TYPE_VALUE_P = "P";
        public static readonly string CODE_MASTER_ITEM_TYPE_VALUE_X = "X";

        public static readonly string CODE_MASTER_BOM_DETAIL_TYPE = "BomDetType";
        public static readonly string CODE_MASTER_BOM_DETAIL_TYPE_VALUE_N = "N";
        public static readonly string CODE_MASTER_BOM_DETAIL_TYPE_VALUE_O = "O";
        public static readonly string CODE_MASTER_BOM_DETAIL_TYPE_VALUE_X = "X";

        public static readonly string CODE_MASTER_WORKCALENDAR_TYPE = "WorkCalendarType";
        public static readonly string CODE_MASTER_WORKCALENDAR_TYPE_VALUE_WORK = "Work";
        public static readonly string CODE_MASTER_WORKCALENDAR_TYPE_VALUE_REST = "Rest";

        public static readonly string CODE_MASTER_STATUS = "Status";
        public static readonly string CODE_MASTER_STATUS_VALUE_CREATE = "Create";
        public static readonly string CODE_MASTER_STATUS_VALUE_SUBMIT = "Submit";
        public static readonly string CODE_MASTER_STATUS_VALUE_CANCEL = "Cancel";
        public static readonly string CODE_MASTER_STATUS_VALUE_INPROCESS = "In-Process";
        public static readonly string CODE_MASTER_STATUS_VALUE_PAUSE = "Pause";
        public static readonly string CODE_MASTER_STATUS_VALUE_COMPLETE = "Complete";
        public static readonly string CODE_MASTER_STATUS_VALUE_CLOSE = "Close";
        public static readonly string CODE_MASTER_STATUS_VALUE_VOID = "Void";

        public static readonly string CODE_MASTER_INVOICE_STATUS_VALUE_INPROCESS = "In-Process";
        public static readonly string CODE_MASTER_INVOICE_STATUS_VALUE_REJECTED = "Rejected";
        public static readonly string CODE_MASTER_INVOICE_STATUS_VALUE_APPROVED = "Approved";

        public static readonly string CODE_MASTER_WORKCENTER_TYPE = "WorkCenterType";
        public static readonly string CODE_MASTER_WORKCENTER_TYPE_VALUE_SHIP = "Ship";
        public static readonly string CODE_MASTER_WORKCENTER_TYPE_VALUE_PRODUCTION = "Production";
        public static readonly string CODE_MASTER_WORKCENTER_TYPE_VALUE_INSPECTION = "Inspection";

        public static readonly string CODE_MASTER_PARTY_TYPE = "PartyType";
        public static readonly string CODE_MASTER_PARTY_TYPE_VALUE_REGION = "Region";
        public static readonly string CODE_MASTER_PARTY_TYPE_VALUE_SUPPLIER = "Supplier";
        public static readonly string CODE_MASTER_PARTY_TYPE_VALUE_CARRIER = "Carrier";
        public static readonly string CODE_MASTER_PARTY_TYPE_VALUE_CUSTOMER = "Customer";

        public static readonly string CODE_MASTER_YESNO = "YesNo";
        public static readonly string CODE_MASTER_YESNO_VALUE_YES = "Yes";
        public static readonly string CODE_MASTER_YESNO_VALUE_NO = "No";

        public static readonly string CODE_MASTER_FLOW_BINDING_MODE = "FlowBindingMode";
        public static readonly string CODE_MASTER_FLOW_BINDING_MODE_VALUE_PUSH = "Push";
        public static readonly string CODE_MASTER_FLOW_BINDING_MODE_VALUE_PULL = "Pull";

        public static readonly string CODE_MASTER_BINDING_TYPE = "BindingType";
        public static readonly string CODE_MASTER_BINDING_TYPE_VALUE_CREATE = "Create";
        public static readonly string CODE_MASTER_BINDING_TYPE_VALUE_SUBMIT = "Submit";
        public static readonly string CODE_MASTER_BINDING_TYPE_VALUE_RECEIVE_SYN = "RecSyn";
        public static readonly string CODE_MASTER_BINDING_TYPE_VALUE_RECEIVE_ASYN = "RecAsyn";

        public static readonly string CODE_MASTER_GR_GAP_TO = "GrGapTo";
        public static readonly string CODE_MASTER_GR_GAP_TO_IPGAP = "IpGap";
        public static readonly string CODE_MASTER_GR_GAP_TO_GI = "GI";
        public static readonly string CODE_MASTER_GR_GAP_TO_GR = "GR";    

        public static readonly string CODE_MASTER_CREATE_HU_OPTION = "CreateHuOption";
        public static readonly string CODE_MASTER_CREATE_HU_OPTION_VALUE_GI = "GI";
        public static readonly string CODE_MASTER_CREATE_HU_OPTION_VALUE_GR = "GR";
        public static readonly string CODE_MASTER_CREATE_HU_OPTION_VALUE_NONE = "None";
        public static readonly string CODE_MASTER_CREATE_HU_OPTION_VALUE_RELEASE = "Release";
        public static readonly string CODE_MASTER_CREATE_HU_OPTION_VALUE_START = "Start";

        public static readonly string CODE_MASTER_ORDER_PRIORITY = "OrderPriority";
        public static readonly string CODE_MASTER_ORDER_PRIORITY_VALUE_NORMAL = "Normal";
        public static readonly string CODE_MASTER_ORDER_PRIORITY_VALUE_URGENT = "Urgent";

        public static readonly string CODE_MASTER_PRICE_LIST_TYPE = "PriceListType";
        public static readonly string CODE_MASTER_PRICE_LIST_TYPE_VALUE_PURCHASE = "Purchase";
        public static readonly string CODE_MASTER_PRICE_LIST_TYPE_VALUE_SALES = "Sales";

        public static readonly string CODE_MASTER_FLOW_STRATEGY = "FlowStrategy";
        public static readonly string CODE_MASTER_FLOW_STRATEGY_VALUE_MRP = "MRP";
        public static readonly string CODE_MASTER_FLOW_STRATEGY_VALUE_JIT = "JIT";
        public static readonly string CODE_MASTER_FLOW_STRATEGY_VALUE_KB = "KB";
        public static readonly string CODE_MASTER_FLOW_STRATEGY_VALUE_ODP = "ODP";
        public static readonly string CODE_MASTER_FLOW_STRATEGY_VALUE_MANUAL = "Manual";
        public static readonly string CODE_MASTER_FLOW_STRATEGY_VALUE_MES = "Mes";

        public static readonly string CODE_MASTER_PLAN_TYPE = "PlanType";
        public static readonly string CODE_MASTER_PLAN_TYPE_VALUE_DMDSCHEDULE = "DmdSchedule";
        public static readonly string CODE_MASTER_PLAN_TYPE_VALUE_MPS = "MPS";
        public static readonly string CODE_MASTER_PLAN_TYPE_VALUE_MRP = "MRP";

        public static readonly string CODE_MASTER_INPROCESS_LOCATION_TYPE = "IpType";
        public static readonly string CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_NORMAL = "Nml";
        public static readonly string CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_GAP = "Gap";

        public static readonly string CODE_MASTER_BILL_TYPE = "BillType";
        public static readonly string CODE_MASTER_BILL_TYPE_VALUE_NORMAL = "Normal";
        public static readonly string CODE_MASTER_BILL_TYPE_VALUE_CANCEL = "Cancel";

        public static readonly string CODE_MASTER_CHECK_ORDER_DETAIL_OPTION = "CheckOrderDetOption";
        public static readonly string CODE_MASTER_CHECK_ORDER_DETAIL_OPTION_VALUE_NOT_CHECK = "NotCheck";
        public static readonly string CODE_MASTER_CHECK_ORDER_DETAIL_OPTION_VALUE_CHECK_SOURCE = "CheckSource";
        public static readonly string CODE_MASTER_CHECK_ORDER_DETAIL_OPTION_VALUE_CHECK_INVENTORY = "CheckInv";

        public static readonly string CODE_MASTER_ROUTING_TYPE = "RoutingType";
        public static readonly string CODE_MASTER_ROUTING_TYPE_VALUE_REWORK = "Rework";
        public static readonly string CODE_MASTER_ROUTING_TYPE_VALUE_BINARY = "Binary";

        public static readonly string CODE_MASTER_ROUTING_TYPE_VALUE_PRODUCTION = "Production";
        public static readonly string CODE_MASTER_ROUTING_TYPE_VALUE_STREAMLINE = "StreamLine";
        public static readonly string CODE_MASTER_ROUTING_TYPE_VALUE_SINGLELABOUR = "SingleLabour";
        public static readonly string CODE_MASTER_ROUTING_TYPE_VALUE_SELFSCHEDULE = "Self-Schedule";

        public static readonly string CODE_MASTER_TIME_PERIOD_TYPE = "TimePeriodType";
        public static readonly string CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY = "Daily";
        public static readonly string CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK = "Weekly";
        public static readonly string CODE_MASTER_TIME_PERIOD_TYPE_VALUE_MONTH = "Monthly";
        public static readonly string CODE_MASTER_TIME_PERIOD_TYPE_VALUE_QUARTER = "Quarterly";
        public static readonly string CODE_MASTER_TIME_PERIOD_TYPE_VALUE_YEAR = "Yearly";
        public static readonly string CODE_MASTER_TIME_PERIOD_TYPE_VALUE_HOUR = "Hourly";

        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON = "StockAdjReason";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON1 = "StockAdjReason1";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON2 = "StockAdjReason2";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON3 = "StockAdjReason3";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON4 = "StockAdjReason4";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON5 = "StockAdjReason5";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON6 = "StockAdjReason6";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON7 = "StockAdjReason7";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON8 = "StockAdjReason8";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON9 = "StockAdjReason9";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON10 = "StockAdjReason10";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON11 = "StockAdjReason11";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON12 = "StockAdjReason12";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON13 = "StockAdjReason13";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON14 = "StockAdjReason14";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON15 = "StockAdjReason15";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON16 = "StockAdjReason16";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON17 = "StockAdjReason17";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON18 = "StockAdjReason18";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON19 = "StockAdjReason19";
        public static readonly string CODE_MASTER_STOCK_ADJUST_REASON_VALUE_REASON20 = "StockAdjReason20";

        public static readonly string CODE_MASTER_STOCK_IN_REASON = "StockInReason";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON1 = "StockInReason1";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON2 = "StockInReason2";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON3 = "StockInReason3";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON4 = "StockInReason4";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON5 = "StockInReason5";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON6 = "StockInReason6";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON7 = "StockInReason7";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON8 = "StockInReason8";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON9 = "StockInReason9";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON10 = "StockInReason10";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON11 = "StockInReason11";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON12 = "StockInReason12";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON13 = "StockInReason13";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON14 = "StockInReason14";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON15 = "StockInReason15";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON16 = "StockInReason16";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON17 = "StockInReason17";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON18 = "StockInReason18";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON19 = "StockInReason19";
        public static readonly string CODE_MASTER_STOCK_IN_REASON_VALUE_REASON20 = "StockInReason20";

        public static readonly string CODE_MASTER_STOCK_OUT_REASON = "StockOutReason";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON1 = "StockOutReason1";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON2 = "StockOutReason2";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON3 = "StockOutReason3";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON4 = "StockOutReason4";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON5 = "StockOutReason5";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON6 = "StockOutReason6";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON7 = "StockOutReason7";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON8 = "StockOutReason8";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON9 = "StockOutReason9";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON10 = "StockOutReason10";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON11 = "StockOutReason11";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON12 = "StockOutReason12";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON13 = "StockOutReason13";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON14 = "StockOutReason14";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON15 = "StockOutReason15";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON16 = "StockOutReason16";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON17 = "StockOutReason17";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON18 = "StockOutReason18";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON19 = "StockOutReason19";
        public static readonly string CODE_MASTER_STOCK_OUT_REASON_VALUE_REASON20 = "StockOutReason20";

        public static readonly string CODE_MASTER_ITEM_QUALITY_LEVEL = "ItemQualityLevel";
        public static readonly string CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_1 = "Level1";
        public static readonly string CODE_MASTER_ITEM_QUALITY_LEVEL_VALUE_2 = "Level2";

        public static readonly string CODE_MASTER_BILL_SETTLE_TERM = "BillSettleTerm";
        public static readonly string CODE_MASTER_BILL_SETTLE_TERM_VALUE_RECEIVING_SETTLEMENT = "BAR";
        public static readonly string CODE_MASTER_BILL_SETTLE_TERM_VALUE_ONLINE_BILLING = "BBB";
        public static readonly string CODE_MASTER_BILL_SETTLE_TERM_VALUE_LINEAR_CLEARING = "BAB";
        public static readonly string CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION = "BAI";
        public static readonly string CODE_MASTER_BILL_SETTLE_TERM_VALUE_CONSIGNMENT = "BAC";

        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE = "LocTransType";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS = "ISS";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT = "RCT";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO = "ISS-WO";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_WO = "RCT-WO";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO_BF = "ISS-WO-BF";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_CYC_CNT = "CYC-CNT";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_PO = "RCT-PO";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_TR = "RCT-TR";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_INP = "RCT-INP";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_PUT = "RCT-PUT";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_PICK = "RCT-PIK";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_UNP = "RCT-UNP";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_REPACK = "RCT-REP";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_MATERIAL_IN = "RCT-MIN";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_SO = "ISS-SO";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR = "ISS-TR";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_INP = "ISS-INP";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_UNP = "ISS-UNP";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_PUT = "ISS-PUT";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_PICK = "ISS-PIK";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_REPACK = "ISS-REP";
        public static readonly string CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_MATERIAL_IN = "ISS-MIN";

        public static readonly string CODE_MASTER_PHYCNT_TYPE = "PhysicalCountType";
        public static readonly string CODE_MASTER_PHYCNT_TYPE_WHOLECHECK = "WholeCheck";
        public static readonly string CODE_MASTER_PHYCNT_TYPE_CLASSIFYINGCHECK = "ClassifyingCheck";
        //public static readonly string CODE_MASTER_PHYCNT_TYPE_SPOTCHECK = "SpotCheck";

        public static readonly string CODE_MASTER_PICKBY = "PickBy";
        public static readonly string CODE_MASTER_PICKBY_HU = "Hu";
        public static readonly string CODE_MASTER_PICKBY_LOTNO = "LotNo";
        public static readonly string CODE_MASTER_PICKBY_ITEM = "Item";

        public static readonly string CODE_MASTER_RAW_MATERIAL_BAR_CODE_TYPE = "RMBarCodeType";
        public static readonly string CODE_MASTER_RAW_MATERIAL_BAR_CODE_TYPE_VALUE_DEFAULT = "Default";

        public static readonly string CODE_MASTER_FINISH_GOODS_BAR_CODE_TYPE = "FGBarCodeType";
        public static readonly string CODE_MASTER_FINISH_GOODS_BAR_CODE_TYPE_VALUE_DEFAULT = "Default";

        public static readonly string CODE_MASTER_BACKFLUSH_METHOD = "BackFlushMethod";
        public static readonly string CODE_MASTER_BACKFLUSH_METHOD_VALUE_GOODS_RECEIVE = "GoodsReceive";
        public static readonly string CODE_MASTER_BACKFLUSH_METHOD_VALUE_BATCH_FEED = "BatchFeed";


        public static readonly string CODE_MASTER_ANTI_RESOLVE_HU = "AntiResolveHu";
        public static readonly string CODE_MASTER_ANTI_RESOLVE_HU_VALUE_NOT_RESOLVE = "NotResolve";
        public static readonly string CODE_MASTER_ANTI_RESOLVE_HU_VALUE_AUTO_RESOLVE = "AutoResolve";


        public static readonly string CODE_MASTER_REPACK_TYPE = "RepackType";
        public static readonly string CODE_MASTER_REPACK_TYPE_VALUE_REPACK = "Repack";
        public static readonly string CODE_MASTER_REPACK_TYPE_VALUE_DEVANNING = "Devanning";

        public static readonly string CODE_MASTER_ODD_SHIP_OPTION = "OddShipOption";
        public static readonly string CODE_MASTER_ODD_SHIP_OPTION_VALUE_SHIP_FIRST = "ShipFirst";
        public static readonly string CODE_MASTER_ODD_SHIP_OPTION_VALUE_NOT_SHIP = "NotShip";

        public static readonly string CODE_MASTER_HU_STATUS = "HuStatus";
        public static readonly string CODE_MASTER_HU_STATUS_VALUE_CREATE = "Create";
        public static readonly string CODE_MASTER_HU_STATUS_VALUE_INVENTORY = "Inventory";
        public static readonly string CODE_MASTER_HU_STATUS_VALUE_INPROCESS = "In-Process";
        public static readonly string CODE_MASTER_HU_STATUS_VALUE_CLOSE = "Close";


        public static readonly string CODE_MASTER_INSPECT_DISPOSITION = "Disposition";
        public static readonly string CODE_MASTER_INSPECT_DISPOSITION_DEVIATIONAUTHORIZATION = "DeviationAuthorization";
        public static readonly string CODE_MASTER_INSPECT_DISPOSITION_REPAIR = "Repair";
        public static readonly string CODE_MASTER_INSPECT_DISPOSITION_RETURNTOSUPPLIER = "ReturnToSupplier";
        public static readonly string CODE_MASTER_INSPECT_DISPOSITION_REWORK = "Rework";
        public static readonly string CODE_MASTER_INSPECT_DISPOSITION_SCRAP = "Scrap";
        public static readonly string CODE_MASTER_INSPECT_DISPOSITION_SORT = "Sort";
        public static readonly string CODE_MASTER_INSPECT_DEFECTCLASSIFICATION = "DefectClassification";
        public static readonly string CODE_MASTER_INSPECT_DEFECTFACTOR = "DefectFactor";

        public static readonly string CODE_MASTER_DAYS_ADD_TYPE = "DaysAddType";
        public static readonly string CODE_MASTER_DAYS_ADD_TYPE_NOT_ADD = "0";
        public static readonly string CODE_MASTER_DAYS_ADD_TYPE_START_ADD = "1";
        public static readonly string CODE_MASTER_DAYS_ADD_TYPE_END_ADD = "2";
        public static readonly string CODE_MASTER_REGION_TYPE = "RegionType";
        public static readonly string CODE_MASTER_REGION_TYPE_VALUE_NORMAL = "Normal";
        public static readonly string CODE_MASTER_REGION_TYPE_VALUE_REMOTE = "Remote";

        public static readonly string CODE_MASTER_MRP_OPTION = "MRPOpt";
        public static readonly string CODE_MASTER_MRP_OPTION_VALUE_ORDER_BEFORE_PLAN = "OrderBeforePlan";
        public static readonly string CODE_MASTER_MRP_OPTION_VALUE_PLAN_ONLY = "PlanOnly";
        public static readonly string CODE_MASTER_MRP_OPTION_VALUE_ORDER_ONLY = "OrderOnly";

        public static readonly string CODE_MASTER_MRP_SOURCE_TYPE = "MRPSourceType";
        public static readonly string CODE_MASTER_MRP_SOURCE_TYPE_VALUE_ORDER = "Order";
        public static readonly string CODE_MASTER_MRP_SOURCE_TYPE_VALUE_CUSTOMER_PLAN = "Plan";
        public static readonly string CODE_MASTER_MRP_SOURCE_TYPE_VALUE_SAFE_STOCK = "SafeStock";
        public static readonly string CODE_MASTER_MRP_SOURCE_TYPE_VALUE_MARKET_FORECAST = "Foecast";
        #endregion

        #region TRANSFORMER Special Barcode
        public static readonly string BARCODE_SPECIAL_MARK = "$";
        public static readonly string BARCODE_HEAD_DEFAULT = "Default";
        public static readonly string BARCODE_HEAD_FLOW = "F";
        public static readonly string BARCODE_HEAD_BIN = "B";
        public static readonly string BARCODE_HEAD_OK = "O";
        public static readonly string BARCODE_HEAD_PRINT = "P";
        public static readonly string BARCODE_HEAD_CANCEL = "C";
        public static readonly string BARCODE_HEAD_INSPECT = "I";
        public static readonly string BARCODE_HEAD_NOTE = "N";
        public static readonly string BARCODE_HEAD_LOCATION = "L";

        public static readonly string CS_BIND_VALUE_TRANSFORMER = "BindTransformer";
        public static readonly string CS_BIND_VALUE_TRANSFORMERDETAIL = "BindTransformerDetail";


        public static readonly string BARCODE_BODY_INSPECT_QUALIFIED = "Qualified";
        public static readonly string BARCODE_BODY_INSPECT_UNQUALIFIED = "Unqualified";

        public static readonly string TRANSFORMER_MODULE_TYPE_SELECTION = "Module_Selection";
        public static readonly string TRANSFORMER_MODULE_TYPE_LOGOUT = "Module_Logout";
        public static readonly string TRANSFORMER_MODULE_TYPE_SHIP = "Module_Ship";
        public static readonly string TRANSFORMER_MODULE_TYPE_RECEIVE = "Module_Receive";
        public static readonly string TRANSFORMER_MODULE_TYPE_PICKUP = "Module_Pickup";
        public static readonly string TRANSFORMER_MODULE_TYPE_PUTAWAY = "Module_PutAway";
        public static readonly string TRANSFORMER_MODULE_TYPE_ONLINE = "Module_Online";
        public static readonly string TRANSFORMER_MODULE_TYPE_OFFLINE = "Module_Offline";
        public static readonly string TRANSFORMER_MODULE_TYPE_SHIPVIEW = "Module_ShipView";
        public static readonly string TRANSFORMER_MODULE_TYPE_INSPECT = "Module_Inspect";
        public static readonly string TRANSFORMER_MODULE_TYPE_TRANSFER = "Module_Transfer";
        public static readonly string TRANSFORMER_MODULE_TYPE_SHIPCONFIRM = "Module_ShipConfirm";
        public static readonly string TRANSFORMER_MODULE_TYPE_PICKLIST = "Module_PickList";
        public static readonly string TRANSFORMER_MODULE_TYPE_REPACK = "Module_Repack";
        public static readonly string TRANSFORMER_MODULE_TYPE_SHIPRETURN = "Module_ShipReturn";
        public static readonly string TRANSFORMER_MODULE_TYPE_RECEIVERETURN = "Module_ReceiveReturn";
        public static readonly string TRANSFORMER_MODULE_TYPE_MATERIALIN = "Module_MaterialIn";
        public static readonly string TRANSFORMER_MODULE_TYPE_FLUSHBACK = "Module_FlushBack";
        public static readonly string TRANSFORMER_MODULE_TYPE_INSPECTION = "Module_Inspection";
        public static readonly string TRANSFORMER_MODULE_TYPE_DEVANNING = "Module_Devanning";
        public static readonly string TRANSFORMER_MODULE_TYPE_STOCKTAKING = "Module_StockTaking";
        public static readonly string TRANSFORMER_MODULE_TYPE_PRODUCTIONRECEIVE = "Module_ProductionReceive";//新品收货
        public static readonly string TRANSFORMER_MODULE_TYPE_PICKLISTONLINE = "Module_PickListOnline";
        public static readonly string TRANSFORMER_MODULE_TYPE_HUSTATUS = "Module_HuStatus";
        public static readonly string TRANSFORMER_MODULE_TYPE_REUSE = "Module_Reuse";
        //百利得客户化,把拣货发货和订单发货分开
        public static readonly string TRANSFORMER_MODULE_TYPE_SHIPORDER = "Module_ShipOder";
        public static readonly string TRANSFORMER_MODULE_TYPE_INSPECTCONFIM = "Module_InspectConfirm";
        public static readonly string TRANSFORMER_MODULE_TYPE_PRINTING = "Module_Printing";
        public static readonly string TRANSFORMER_MODULE_TYPE_BATCHDEVANNING = "Module_BatchDevanning";

        public static readonly string CODE_MASTER_PACKAGETYPE = "PackageType";
        public static readonly string CODE_MASTER_PACKAGETYPE_INNER = "Inner";
        public static readonly string CODE_MASTER_PACKAGETYPE_OUTER = "Outer";

        public static readonly string CODE_MASTER_PHYCNT_GROUPBY = "PhyCntGroupBy";
        public static readonly string CODE_MASTER_PHYCNT_GROUPBY_LOCATION = "Location";
        public static readonly string CODE_MASTER_PHYCNT_GROUPBY_BIN = "Bin";
        public static readonly string CODE_MASTER_PHYCNT_GROUPBY_NOGROUP = "NoGroup";
        #endregion

        #region Dss
        public static readonly string DSS_EVENT_CODE_CREATE = "CREATE";
        public static readonly string DSS_EVENT_CODE_UPDATE = "UPDATE";
        public static readonly string DSS_EVENT_CODE_DELETE = "DELETE";

        public static readonly string DSS_SYSTEM_CODE_SCONIT = "SCONIT";
        public static readonly string DSS_SYSTEM_CODE_QAD = "QAD";
        public static readonly string DSS_SYSTEM_CODE_UFIDA = "UFIDA";
        public static readonly string DSS_SYSTEM_CODE_MES_YFK = "MES";

        public static readonly string DSS_ENTITY_LOCATION = "Location";
        public static readonly string DSS_ENTITY_PARTY = "Party";
        public static readonly string DSS_ENTITY_SITE = "Site";

        public static readonly string DSS_OUT_ITEM = "SCMS_PART";
        public static readonly string DSS_OUT_HU = "SCMS_MATERIAL_BARCODE";
        public static readonly string DSS_OUT_ORDERDETAIL = "SCMS_WO";
        public static readonly string DSS_OUT_MESBOMDETAIL = "SCMS_BOM";

        public static readonly string DSS_IN_SHELF = "MES_SCMS_STATION_SHELF";
        public static readonly string DSS_IN_ORDER = "MES_SCMS_COMPLETED_ORDER";
        public static readonly string DSS_IN_ORDERLOCATIONTRANSACTION = "MES_SCMS_STATION_BOX";
        public static readonly string DSS_IN_SHELFITEM = "MES_SCMS_SHELF_PART";
        public static readonly string DSS_IN_ORDER_BOX = "MES_SCMS_COMPLETED_BOX";
        public static readonly string DSS_IN_BOMDETAIL = "MES_SCMS_BOM";
        public static readonly string DSS_IN_INSPECTORDER = "MES_SCMS_REPAIR_MATERIAL";
        #endregion
    }
}

