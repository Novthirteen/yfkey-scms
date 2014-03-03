using System;
using System.Collections.Generic;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.Production;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface ILocationMgr : ILocationBaseMgr
    {
        #region Customized Methods

        Location CheckAndLoadLocation(string locationCode);

        IList<Location> GetLocation(Region region);

        IList<Location> GetLocation(Region region, bool includeInactive);

        IList<Location> GetLocation(string regionCode);

        IList<Location> GetLocation(string regionCode, bool includeInactive);

        IList<Location> GetLocation(User user, bool includeInactive);

        IList<Location> GetLocation(User user);

        IList<Location> GetLocation(string regionCode, bool includeInactive, bool includeReject);

        IList<Location> GetLocationByUserCode(string userCode);

        IList<Location> GetLocation(string userCode, string type, bool includeInactive);

        IList<Location> GetLocation(string userCode, string type);

        IList<Location> GetLocation(IList<string> locationCodeList);

        Location GetRejectLocation();

        Location GetInspectLocation();

        IList<InventoryTransaction> InventoryOut(InProcessLocationDetail inProcessLocationDetail, User user);

        IList<InventoryTransaction> InventoryOut(MiscOrderDetail miscOrderDetail, User user);

        InventoryTransaction InventoryOut(HuOdd huOdd, ReceiptDetail receiptDetail, User user);

        IList<InventoryTransaction> InventoryOut(MaterialIn materialIn, User user, Flow ProductLine);

        IList<InventoryTransaction> InventoryIn(ReceiptDetail receiptDetail, User user);

        IList<InventoryTransaction> InventoryIn(ReceiptDetail receiptDetail, User user, string binCode);

        IList<InventoryTransaction> InventoryIn(Receipt receipt, User user);

        IList<InventoryTransaction> InventoryIn(MiscOrderDetail miscOrderDetail, User user);

        IList<InventoryTransaction> InventoryIn(MiscOrderDetail miscOrderDetail, User user, string binCode);

        IList<InventoryTransaction> InventoryIn(ProductLineInProcessLocationDetail productLineInProcessLocationDetail, User user);

        void InventoryPick(LocationLotDetail locationLotDetail, User user);

        void InventoryPick(IList<LocationLotDetail> locationLotDetailList, User user);

        void InventoryPick(IList<LocationLotDetail> locationLotDetailList, string userCode);

        void InventoryPut(LocationLotDetail locationLotDetail, User user);

        void InventoryPut(IList<LocationLotDetail> locationLotDetailList, User user);

        void InventoryPut(IList<LocationLotDetail> locationLotDetailList, string userCode);

        void InventoryRepackIn(RepackDetail repackDetail, User user);

        InventoryTransaction InventoryRepackOut(RepackDetail repackDetail, Location location, int? plannedBillId, User user);

        InventoryTransaction InspectOut(LocationLotDetail locationLotDetail, User user, bool needSettle, string inspectNo, Location locationTo);

        IList<InventoryTransaction> InspectOut(Location location, Item item, decimal qty, User user, string inspectNo, Location locationTo);

        IList<InventoryTransaction> InspectIn(LocationLotDetail locationLotDetail, Location locIn, User user, bool needSettle, string inspectNo, string inrNo,int? inspectDetailId);

        IList<InventoryTransaction> InspectIn(LocationLotDetail locationLotDetail, Location locIn, StorageBin bin, User user, bool needSettle, string inspectNo, string inrNo, int? inspectDetailId);

        IList<InventoryTransaction> InspectIn(Item item, decimal qty, User user, string inspectNo, int? plannedBillId, Location locationFrom);

        IList<InventoryTransaction> InventoryAdjust(CycleCountResult cycleCountResult, User user);

        /**
       * ��������
       * ��λ
       * ������Ч����
       * ������Ч������
       * ���񴴽�ʱ��
       * ���񴴽�ʱ����
       * ����ʱ��  --ȥ��
       * ����ʱ���� --ȥ��
       * ��Դ��Ӧ��/����
       * Ŀ�Ŀͻ�/����
       * �����
       * ������
       * �ջ���
       * ������
       * asn��
       */
        IList<LocationTransaction> GetLocationTransaction(string[] transType,
            string[] loc, DateTime effdateStart, DateTime effDateEnd, DateTime createDateStart, DateTime createDateEnd,
            string partyFrom, string partyTo, string[] itemCode,
            string[] orderNo, string[] recNo, string createUser, string[] ipNo, string userCode);

        bool IsHuOcuppyByPickList(string huId);

        #endregion Customized Methods
    }
}
