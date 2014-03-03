using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sconit_CS
{
    public static class BusinessContants
    {
        public static readonly string ORDER_TYPE_WORKORDER = "WorkOrder";

        public static readonly string WORK_ORDER_OPERATION_DOWNLOAD = "Download";
        public static readonly string WORK_ORDER_OPERATION_SEQUENCE = "Sequence";
        public static readonly string WORK_ORDER_OPERATION_ONLINE = "Online";
        public static readonly string WORK_ORDER_OPERATION_TRANSFER = "Transfer";
        public static readonly string WORK_ORDER_OPERATION_RECEIVE = "Receive";

        public static readonly string SYNRESULT_SUCCESS = "Success";
        public static readonly string SYNRESULT_FAILURE = "Failure";

        public static readonly string SYNSTATUS_WAITING = "Waiting";
        public static readonly string SYNSTATUS_SUCCESS = "Success";
        public static readonly string SYNSTATUS_FAILURE = "Failure";
        public static readonly string SYNSTATUS_CANCELED = "Canceled";

        public static readonly string MODULESELECTION_WOSCANONLINE = "CS_WOOnline";
        public static readonly string MODULESELECTION_WOSCANOFFLINE = "CS_WOOffline";
        public static readonly string MODULESELECTION_SHIP = "CS_Ship";
        public static readonly string MODULESELECTION_RECEIVE = "CS_Receive";
        public static readonly string MODULESELECTION_INVTRANSFER = "CS_Invtransfer";
        public static readonly string MODULESELECTION_MODULESELECTION = "CS_ModuleSelection";
        public static readonly string MODULESELECTION_PICKUP = "CS_PickUp";
        public static readonly string MODULESELECTION_PUTAWAY = "CS_PutAway";
        public static readonly string MODULESELECTION_COUNT = "CS_Count";

        public static readonly string CODE_MASTER_ORDER_SUB_TYPE = "OrderSubType";
        public static readonly string CODE_MASTER_ORDER_SUB_TYPE_CODE_GP_ORDER = "GPOrder";
        public static readonly string CODE_MASTER_ORDER_SUB_TYPE_CODE_GP_RTN_ORDER = "GPRTNOrder";
        public static readonly string CODE_MASTER_ORDER_SUB_TYPE_CODE_GP_ADJ_ORDER = "GPADJOrder";
        public static readonly string CODE_MASTER_ORDER_SUB_TYPE_CODE_DELIVERY_ORDER = "DeliveryOrder";
        public static readonly string CODE_MASTER_ORDER_SUB_TYPE_CODE_DELIVERY_RTN_ORDER = "DeliveryRTNOrder";
        public static readonly string CODE_MASTER_ORDER_SUB_TYPE_CODE_DELIVERY_ADJ_ORDER = "DeliveryADJOrder";
        public static readonly string CODE_MASTER_ORDER_SUB_TYPE_CODE_SHIP_ORDER = "ShipOrder";
        public static readonly string CODE_MASTER_ORDER_SUB_TYPE_CODE_SHIP_RTN_ORDER = "ShipRTNOrder";
        public static readonly string CODE_MASTER_ORDER_SUB_TYPE_CODE_SHIP_ADJ_ORDER = "ShipADJOrder";

        public static readonly string CODE_MASTER_STATUS = "Status";
        public static readonly string CODE_MASTER_STATUS_VALUE_CREATE = "Create";
        public static readonly string CODE_MASTER_STATUS_VALUE_SUBMIT = "Submit";
        public static readonly string CODE_MASTER_STATUS_VALUE_CANCEL = "Cancel";
        public static readonly string CODE_MASTER_STATUS_VALUE_INPROCESS = "In-Process";
        public static readonly string CODE_MASTER_STATUS_VALUE_PAUSE = "Pause";
        public static readonly string CODE_MASTER_STATUS_VALUE_COMPLETE = "Complete";
        public static readonly string CODE_MASTER_STATUS_VALUE_CLOSE = "Close";
        public static readonly string CODE_MASTER_STATUS_VALUE_VOID = "Void";        

        public static readonly string CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_ORGANIZATION = "Organization";
        public static readonly string CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_MENU = "Menu";
        public static readonly string CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_PAGE = "Page";
        public static readonly string CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_TERMINAL = "Terminal";

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
    }
}
