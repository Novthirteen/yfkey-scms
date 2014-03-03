using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Customize;
namespace com.Sconit.Service.Customize.Impl
{
    [Transactional]
  public  class ProjectMgr:SessionBase,IProjectMgr
    {
      public IProjectDao entityDao{get;set;}

      public IList<ProjectMstr> GetAll()
      {
          return base.FindAll<ProjectMstr>();
      }
         
    }
}
