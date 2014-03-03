using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Dss;

namespace com.Sconit.Service.Dss
{
    public interface IOutboundMgr
    {
        void ProcessOutbound(DssOutboundControl dssOutboundControl);
    }
}
