using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Mes.Dss.Entity;

namespace com.Mes.Dss.Service
{
    public interface IMesDssInMgr
    {
        void ProcessIn(MesScmsTableIndex mesScmsTableIndex);
        /*
        void ProcessOrderIn(MesScmsTableIndex mesScmsTableIndex);

        void ProcessInspectOrderIn(MesScmsTableIndex mesScmsTableIndex);

        void ProcessShelfIn(MesScmsTableIndex mesScmsTableIndex);

        void ProcessShelfItemIn(MesScmsTableIndex mesScmsTableIndex);

        void ProcessBomDetailIn(MesScmsTableIndex mesScmsTableIndex);

        void ProcessOrderLocationTransactionIn(MesScmsTableIndex mesScmsTableIndex);
        */
    }
}
