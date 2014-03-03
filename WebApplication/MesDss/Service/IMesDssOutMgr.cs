using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Mes.Dss.Entity;

namespace com.Mes.Dss.Service
{
    public interface IMesDssOutMgr
    {
        void ProcessOut(ScmsTableIndex scmsTableIndex);
        /*
        void ProcessHuOut(ScmsTableIndex scmsTableIndex);

        void ProcessOrderDetailOut(ScmsTableIndex scmsTableIndex);

        void ProcessItemOut(ScmsTableIndex scmsTableIndex);

        void ProcessMesBomDetailOut(ScmsTableIndex scmsTableIndex);
         * */
    }
}
