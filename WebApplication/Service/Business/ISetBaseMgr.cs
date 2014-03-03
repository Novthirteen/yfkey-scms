using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Service.Business
{
    public interface ISetBaseMgr
    {
        void FillResolverByOrder(Resolver resolver);

        void FillResolverByASN(Resolver resolver);

        void FillResolverByPickList(Resolver resolver);

        void FillResolverByBin(Resolver resolver);

        void FillResolverByLocation(Resolver resolver);

        void FillResolverByFlow(Resolver resolver);

        void FillResolverByFlow(Resolver resolver, Flow flow);

        void FillDetailByFlow(Resolver resolver);
    }
}
