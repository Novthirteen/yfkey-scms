using System;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Procurement;
using System.IO;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IHuMgr : IHuBaseMgr
    {
        #region Customized Methods

        IList<Hu> CreateHu(IList<FlowDetail> flowDetailList, User user);

        IList<Hu> CreateHu(IList<FlowDetail> flowDetailList, User user, string idMark);

        IList<Hu> CreateHu(IList<FlowDetail> flowDetailList, User user, string idMark ,string packageType);

        IList<Hu> CreateHu(IList<OrderDetail> orderDetailList, User user);

        IList<Hu> CreateHu(IList<OrderDetail> orderDetailList, User user, string idMark);

        IList<Hu> CreateHu(IList<OrderDetail> orderDetailList, User user, string idMark, string packageType);

        IList<Hu> CreateHu(OrderHead orderHead, User user);

        IList<Hu> CreateHu(InProcessLocationDetail inProcessLocationDetail, User user);

        IList<Hu> CreateHu(ReceiptDetail receiptDetail, User user);

        //IList<Hu> CreateHu(Item item, decimal qty, string lotNo, Uom uom, decimal unitCount, int? huLotSize,
        //    string orderNo, string recNo, DateTime? manufactureDate, Party manufactureParty, string qualityLevel, User user);

        Hu CheckAndLoadHu(string huId);

        //Hu CreateHu(string barCode, string flowCode);
        IList<Hu> CloneHu(Hu hu, decimal uintCount, int count, User user);

        IList<Hu> CloneHu(string huId, decimal uintCount, int count, User user);

        IList<Hu> GetHuList(string userCode, int firstRow, int maxRows, params string[] orderTypes);

        #endregion Customized Methods
    }
}
