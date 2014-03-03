using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Distribution;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Distribution
{
    public interface ICustomerRollingPlanDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateCustomerRollingPlanDetail(CustomerRollingPlanDetail entity);

        CustomerRollingPlanDetail LoadCustomerRollingPlanDetail(Int32 id);
  
        IList<CustomerRollingPlanDetail> GetAllCustomerRollingPlanDetail();
  
        void UpdateCustomerRollingPlanDetail(CustomerRollingPlanDetail entity);
        
        void DeleteCustomerRollingPlanDetail(Int32 id);
    
        void DeleteCustomerRollingPlanDetail(CustomerRollingPlanDetail entity);
    
        void DeleteCustomerRollingPlanDetail(IList<Int32> pkList);
    
        void DeleteCustomerRollingPlanDetail(IList<CustomerRollingPlanDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
