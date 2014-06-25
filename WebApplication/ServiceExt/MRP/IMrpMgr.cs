using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.MRP;
using System.IO;

namespace com.Sconit.Service.MRP
{
    public interface IMrpMgr
    {
        void RunMrp(User user);

        void RunMrp(DateTime effectiveDate, User user);

    }
}
