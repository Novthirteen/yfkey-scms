using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Production;

namespace com.Sconit.Service.MasterData
{
    public interface IScanBarcodeMgr//Service已转移至Business,出门右转
    {
        //Resolver AnalyzeBarcode(string barcode, string userCode, string moduleType);

        //Resolver AnalyzeBarcode(string barcode, User user, string moduleType);
        
        //void PutAway(Resolver resolver);

        //void PickUp(Resolver resolver);

        //Resolver ShipOrder(Resolver resolver);

        //string TransferOrder(Resolver resolver);

        //string ReceiveWorkOrder(Resolver resolver);

        //Resolver ReceiveOrder(Resolver resolver);

        //Resolver ScanBarcode(Resolver resolver);

        //void RawMaterialIn(Resolver resolver);

        //void CreateRepack(Resolver resolver);

        //void CreateDevanning(Resolver resolver);

        //void Inspect(Resolver resolver);

        //void CreateInspectOrder(Resolver resolver);

    }
}
