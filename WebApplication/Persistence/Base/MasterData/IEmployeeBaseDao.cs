using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IEmployeeBaseDao
    {
        #region Method Created By CodeSmith

        void CreateEmployee(Employee entity);

        Employee LoadEmployee(String code);
  
        IList<Employee> GetAllEmployee();
  
        void UpdateEmployee(Employee entity);
        
        void DeleteEmployee(String code);
    
        void DeleteEmployee(Employee entity);
    
        void DeleteEmployee(IList<String> pkList);
    
        void DeleteEmployee(IList<Employee> entityList);    
        #endregion Method Created By CodeSmith
    }
}
