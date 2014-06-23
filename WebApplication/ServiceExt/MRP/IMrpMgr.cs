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
        void RunMrp(User user);

        void RunMrp(DateTime effectiveDate, User user);

        List<CustomerPlan> ReadCustomerPlanFromXls(Stream inputStream, CodeMaster.TimeUnit dateType, User user);

        List<ShiftPlan> ReadShiftPlanFromXls(Stream inputStream, User user);

        IList<OrderHead> CreateTransferOrder(IList<FirmPlan> firmPlanList, User user);

        string ReadProcurementPlanFromXls(Stream inputStream, CodeMaster.TimeUnit dateType, User user);

        void MrpCalculate(IList<OrderHead> orderHeadList, string userCode, IList<ProcurementPlan> procurementPlanList);
        
    }
}
