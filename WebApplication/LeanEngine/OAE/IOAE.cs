using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeanEngine.Entity;

namespace LeanEngine.OAE
{
    public interface IOAE
    {
        void ProcessTime(Flow flow);

        void ProcessReqQty(ItemFlow itemFlow);
    }
}
