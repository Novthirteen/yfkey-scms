using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Persistence.MasterData
{
   public interface IProjectBaseDao
    {
        IList<ProjectMstr> GetAllItem();

        IList<ProjectMstr> GetAllItem(bool distinct);
    }
}
