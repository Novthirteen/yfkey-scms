using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Sconit.Service.Batch
{
    public interface IJob
    {
        void Execute(JobRunContext context);
    }
}
