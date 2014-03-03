using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MRP;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP
{
    public interface ICustomerScheduleBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateCustomerSchedule(CustomerSchedule entity);

        CustomerSchedule LoadCustomerSchedule(Int32 id);

        IList<CustomerSchedule> GetAllCustomerSchedule();
    
        void UpdateCustomerSchedule(CustomerSchedule entity);

        void DeleteCustomerSchedule(Int32 id);
    
        void DeleteCustomerSchedule(CustomerSchedule entity);
    
        void DeleteCustomerSchedule(IList<Int32> pkList);
    
        void DeleteCustomerSchedule(IList<CustomerSchedule> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
