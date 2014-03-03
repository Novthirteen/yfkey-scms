using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MRP;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP
{
    public interface ICustomerScheduleDetailBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateCustomerScheduleDetail(CustomerScheduleDetail entity);

        CustomerScheduleDetail LoadCustomerScheduleDetail(Int32 id);

        IList<CustomerScheduleDetail> GetAllCustomerScheduleDetail();
    
        void UpdateCustomerScheduleDetail(CustomerScheduleDetail entity);

        void DeleteCustomerScheduleDetail(Int32 id);
    
        void DeleteCustomerScheduleDetail(CustomerScheduleDetail entity);
    
        void DeleteCustomerScheduleDetail(IList<Int32> pkList);
    
        void DeleteCustomerScheduleDetail(IList<CustomerScheduleDetail> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
