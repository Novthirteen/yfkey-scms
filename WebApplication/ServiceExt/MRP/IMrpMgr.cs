using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.MRP;
using System.IO;

namespace com.Sconit.Service.MRP
{
    public interface IMrpMgr
    {
        void RunShipPlan(User user);
        void RunShipPlan(DateTime effectiveDate, User user);
        void RunMrp(User user);
        void RunMrp(DateTime effectiveDate, User user);

        void RunProductionPlan(User user);

        List<CustomerScheduleDetail> ReadCustomerPlanFromXls(Stream inputStream, string dateType, User user);

        List<ShiftPlanDet> ReadShiftPlanFromXls(Stream inputStream, User user);

        void UpdateShipPlanQty(IList<string> flowList, IList<string> itemList, IList<string> idList, IList<decimal> qtyList, IList<string> releaseNoList, IList<string> dateFrom, User user, string type);

        void UpdatePurchasePlanQty(IList<string> flowList, IList<string> itemList, IList<string> idList, IList<decimal> qtyList, IList<string> releaseNoList, IList<string> dateFrom, User user, string type);

        void UpdateWeeklyPurchasePlanQty(IList<string> flowList, IList<string> itemList, IList<string> idList, IList<decimal> qtyList, IList<string> releaseNoList, IList<string> dateFrom, User user);
        void ReadShipPlanParametersFromXls(Stream inputStream, User user);

        void ReadProductionPlanParametersFromXls(Stream inputStream, User user);

        void ReadPurchasePlanParametersFromXls(Stream inputStream, User user);
             
    }
}
