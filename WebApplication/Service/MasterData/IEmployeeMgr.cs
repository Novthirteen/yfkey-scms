using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IEmployeeMgr : IEmployeeBaseMgr
    {
        #region Customized Methods

        Employee LoadEmployee(string code, bool IncludeInactive);

        IList<Employee> GetAllEmployee(DateTime lastModifyDate, int firstRow, int maxRows);

        #endregion Customized Methods
    }
}
