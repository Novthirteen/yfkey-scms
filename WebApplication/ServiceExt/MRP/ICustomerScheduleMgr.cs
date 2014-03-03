using System;
using com.Sconit.Entity.MRP;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP
{
    public interface ICustomerScheduleMgr : ICustomerScheduleBaseMgr
    {
        #region Customized Methods

        IList<CustomerSchedule> GetCustomerSchedules(string flowCode, string referenceScheduleNo, List<string> statusList, DateTime? startDate, DateTime? endDate);

        void CancelCustomerSchedule(int customerScheduleId, string userCode);

        CustomerSchedule LoadCustomerSchedule(int customerScheduleId, bool includeDetails);

        void ReleaseCustomerSchedule(int customerScheduleId, string userCode);

        #endregion Customized Methods
    }
}