using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Dss;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Dss
{
    public interface IDssFtpControlBaseDao
    {
        #region Method Created By CodeSmith

        void CreateDssFtpControl(DssFtpControl entity);

        DssFtpControl LoadDssFtpControl(Int32 id);
  
        IList<DssFtpControl> GetAllDssFtpControl();
  
        void UpdateDssFtpControl(DssFtpControl entity);
        
        void DeleteDssFtpControl(Int32 id);
    
        void DeleteDssFtpControl(DssFtpControl entity);
    
        void DeleteDssFtpControl(IList<Int32> pkList);
    
        void DeleteDssFtpControl(IList<DssFtpControl> entityList);    
        #endregion Method Created By CodeSmith
    }
}
