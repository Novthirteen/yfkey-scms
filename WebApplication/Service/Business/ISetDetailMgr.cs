using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.View;

namespace com.Sconit.Service.Business
{
    public interface ISetDetailMgr
    {
        void MatchShip(Resolver resolver);

        void MatchReceive(Resolver resolver);

        void MatchInspet(Resolver resolver);

        void MatchRepack(Resolver resolver);

        void MatchHuByItem(Resolver resolver, Hu hu);

        void CheckHuReScan(Resolver resolver);

        bool CheckMatchHuScanExist(Resolver resolver);

        int FindMaxSeq(List<Transformer> transformerList);

        int FindMaxSeq(Transformer transformer);

        void MatchHuByFlowView(Resolver resolver, FlowView flowView, Hu hu);

        void SetMateria(Resolver resolver);

        string CheckFIFO(Hu targetHu, IList<string> pickedHuList);
    }
}
