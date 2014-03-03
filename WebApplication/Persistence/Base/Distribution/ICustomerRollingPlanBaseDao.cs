using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Distribution;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Distribution
{
    public interface ICustomerRollingPlanBaseDao
    {
        #region Method Created By CodeSmith

        void CreateCustomerRollingPlan(CustomerRollingPlan entity);

        CustomerRollingPlan LoadCustomerRollingPlan(Int32 id);
  
        IList<CustomerRollingPlan> GetAllCustomerRollingPlan();
  
        void UpdateCustomerRollingPlan(CustomerRollingPlan entity);
        
        void DeleteCustomerRollingPlan(Int32 id);
    
        void DeleteCustomerRollingPlan(CustomerRollingPlan entity);
    
        void DeleteCustomerRollingPlan(IList<Int32> pkList);
    
        void DeleteCustomerRollingPlan(IList<CustomerRollingPlan> entityList);    
        #endregion Method Created By CodeSmith
    }
}
