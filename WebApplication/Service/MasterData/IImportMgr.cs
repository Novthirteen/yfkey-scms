using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.MRP;
using NPOI.SS.UserModel;

namespace com.Sconit.Service.MasterData
{
    public interface IImportMgr
    {
        IList<ShiftPlanSchedule> ReadPSModelFromXls(Stream inputStream, User user, string regionCode, string flowCode, DateTime date, string shiftCode);

        IList<FlowPlan> ReadShipScheduleYFKFromXls(Stream inputStream, User user, string planType, string partyCode, string timePeriodType, DateTime date);

        IList<CycleCountDetail> ReadCycleCountFromXls(Stream inputStream, User user, CycleCount cycleCount);

        IList<OrderLocationTransaction> ReadOrderLocationTransactionFromXls(Stream inputStream, string orderNo);

        IList<RollingForecast> ReadRollingForecastFromXls(Stream inputStream, User user);

        OrderHead ReadBatchTransferFromXls(Stream inputStream, User user, string flowCode);

        IList<CustomerSchedule> ReadCustomerScheduleFromXls(Stream inputStream, User user, DateTime? startDate, DateTime? endDate, string flowCode, string refScheduleNo, bool isItemRef);

        bool CheckValidDataRow(Row row, int startColIndex, int endColIndex);
    }
}
