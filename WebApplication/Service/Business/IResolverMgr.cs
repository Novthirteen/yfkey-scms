using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Service.Business
{
    public interface IResolverMgr
    {
        Resolver Resolve(Resolver inputResolver);
    }
}
