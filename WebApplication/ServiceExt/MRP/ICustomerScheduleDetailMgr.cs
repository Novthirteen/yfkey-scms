using System;
using com.Sconit.Entity.MRP;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP
{
    public interface ICustomerScheduleDetailMgr : ICustomerScheduleDetailBaseMgr
    {
        #region Customized Methods
        IList<CustomerScheduleDetail> GetCustomerScheduleDetails(string flowCode, DateTime currentDate);

        IList<CustomerScheduleDetail> GetCustomerScheduleDetails(int customerScheduleId);

        IList<CustomerScheduleDetail> GetEffectiveCustomerScheduleDetail(IList<CustomerScheduleDetail> customerScheduleDetailList);

        IList<CustomerScheduleDetail> GetEffectiveCustomerScheduleDetail(IList<CustomerScheduleDetail> customerScheduleDetailList, DateTime effectiveDate);

        ScheduleView TransferCustomerScheduleDetails2ScheduleView(IList<CustomerScheduleDetail> customerScheduleDetaills, DateTime effDate);

        #endregion Customized Methods
    }
}