using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Utility;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.Distribution;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.Transportation;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ReceiptMgr : ReceiptBaseMgr, IReceiptMgr
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.Receipt");

        private string[] ReceiptDetail2ReceiptDetailCloneFields = new string[] 
            { 
                "Receipt",
                "OrderLocationTransaction",
                "HuId",
                "LotNo",
                "ReceivedQty",
                "ShippedQty",
                "RejectedQty",
                "ScrapQty",
                "IsConsignment",
                "PlannedBill"
            };

        private IReceiptDetailMgr receiptDetailMgr;
        private INumberControlMgr numberControlMgr;
        private ILocationMgr locationMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private IHuMgr huMgr;
        private IHuOddMgr huOddMgr;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        private IInProcessLocationMgr inProcessLocationMgr;
        private IInProcessLocationDetailMgr inProcessLocationDetailMgr;
        private IReceiptInProcessLocationMgr receiptInProcessLocationMgr;
        private IInspectOrderMgr inspectOrderMgr;
        private ICriteriaMgr criteriaMgr;
        private IUserMgr userMgr;
        //private ITransportationOrderMgr transportationOrderMgr;

        public ReceiptMgr(IReceiptDao entityDao,
            IReceiptDetailMgr receiptDetailMgr,
            INumberControlMgr numberControlMgr,
            ILocationMgr locationMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            IHuMgr huMgr,
            IHuOddMgr huOddMgr,
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            IInProcessLocationMgr inProcessLocationMgr,
            IInProcessLocationDetailMgr inProcessLocationDetailMgr,
            IReceiptInProcessLocationMgr receiptInProcessLocationMgr,
            IInspectOrderMgr inspectOrderMgr,
            ICriteriaMgr criteriaMgr,
            IUserMgr userMgr)
            : base(entityDao)
        {
            this.receiptDetailMgr = receiptDetailMgr;
            this.numberControlMgr = numberControlMgr;
            this.locationMgr = locationMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.huMgr = huMgr;
            this.huOddMgr = huOddMgr;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.inProcessLocationMgr = inProcessLocationMgr;
            this.inProcessLocationDetailMgr = inProcessLocationDetailMgr;
            this.receiptInProcessLocationMgr = receiptInProcessLocationMgr;
            this.inspectOrderMgr = inspectOrderMgr;
            this.criteriaMgr = criteriaMgr;
            this.userMgr = userMgr;;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Requires)]
        public override void CreateReceipt(Receipt receipt)
        {
            #region ��Receipt�ϼ�¼�ο�Asn��Ϊ�ִ�
            string ipNo = null;
            if (receipt.InProcessLocations != null && receipt.InProcessLocations.Count > 0)
            {
                foreach (InProcessLocation inProcessLocation in receipt.InProcessLocations)
                {
                    if (ipNo == null)
                    {
                        ipNo = inProcessLocation.IpNo;
                    }
                    else
                    {
                        ipNo += ", " + inProcessLocation.IpNo;
                    }
                }
            }
            receipt.ReferenceIpNo = ipNo;
            #endregion

            this.entityDao.CreateReceipt(receipt);

            #region �����ջ��ͷ�����ϵ
            if (receipt.InProcessLocations != null && receipt.InProcessLocations.Count > 0)
            {
                foreach (InProcessLocation inProcessLocation in receipt.InProcessLocations)
                {
                    ReceiptInProcessLocation receiptInProcessLocation = new ReceiptInProcessLocation();
                    receiptInProcessLocation.InProcessLocation = inProcessLocation;
                    receiptInProcessLocation.Receipt = receipt;

                    this.receiptInProcessLocationMgr.CreateReceiptInProcessLocation(receiptInProcessLocation);
                }
            }
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateReceipt(Receipt receipt, User user)
        {
            CreateReceipt(receipt, user, true);
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateReceipt(Receipt receipt, User user, bool isOddCreateHu)
        {
            log.Debug("Start create receipt");
            #region �������еķ�����ջ�����ӡģ�壬�ջ����촦��ѡ��
            string orderType = null;
            Party partyFrom = null;
            Party partyTo = null;
            ShipAddress shipFrom = null;
            ShipAddress shipTo = null;
            string dockDescription = null;
            string receiptTemplate = null;
            string huTemplate = null;
            string grGapTo = null;
            IList<InProcessLocationDetail> inProcessLocationDetailList = new List<InProcessLocationDetail>();
            if (receipt.InProcessLocations != null && receipt.InProcessLocations.Count > 0)
            {
                foreach (InProcessLocation inProcessLocation in receipt.InProcessLocations)
                {
                    InProcessLocation currentIp = inProcessLocationMgr.LoadInProcessLocation(inProcessLocation.IpNo);
                    if (currentIp.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)
                    {
                        throw new BusinessErrorException("InProcessLocation.Error.StatusErrorWhenReceive", currentIp.Status, currentIp.IpNo);
                    }

                    if (orderType == null)
                    {
                        orderType = inProcessLocation.OrderType;
                    }

                    //�ж�OrderHead��PartyFrom�Ƿ�һ��
                    if (partyFrom == null)
                    {
                        partyFrom = inProcessLocation.PartyFrom;
                    }
                    else if (inProcessLocation.PartyFrom.Code != partyFrom.Code)
                    {
                        throw new BusinessErrorException("Order.Error.ReceiveOrder.PartyFromNotEqual");
                    }

                    //�ж�OrderHead��PartyFrom�Ƿ�һ��
                    if (partyTo == null)
                    {
                        partyTo = inProcessLocation.PartyTo;
                    }
                    else if (inProcessLocation.PartyTo.Code != partyTo.Code)
                    {
                        throw new BusinessErrorException("Order.Error.ReceiveOrder.PartyToNotEqual");
                    }

                    //�ж�OrderHead��ShipFrom�Ƿ�һ��
                    if (shipFrom == null)
                    {
                        shipFrom = inProcessLocation.ShipFrom;
                    }
                    else if (!AddressHelper.IsAddressEqual(inProcessLocation.ShipFrom, shipFrom))
                    {
                        throw new BusinessErrorException("Order.Error.ReceiveOrder.ShipFromNotEqual");
                    }

                    //�ж�OrderHead��ShipTo�Ƿ�һ��
                    if (shipTo == null)
                    {
                        shipTo = inProcessLocation.ShipTo;
                    }
                    else if (!AddressHelper.IsAddressEqual(inProcessLocation.ShipTo, shipTo))
                    {
                        throw new BusinessErrorException("Order.Error.ReceiveOrder.ShipToNotEqual");
                    }

                    if (dockDescription == null)
                    {
                        dockDescription = inProcessLocation.DockDescription;
                    }
                    else if (inProcessLocation.DockDescription != dockDescription)
                    {
                        throw new BusinessErrorException("Order.Error.ReceiveOrder.DockDescriptionNotEqual");
                    }

                    //�ж��ջ�����ӡģ���Ƿ�һ��
                    if (receiptTemplate == null)
                    {
                        receiptTemplate = inProcessLocation.ReceiptTemplate;
                    }
                    else
                    {
                        if (inProcessLocation.ReceiptTemplate != null && inProcessLocation.ReceiptTemplate != receiptTemplate)
                        {
                            throw new BusinessErrorException("Order.Error.ReceiveOrder.ReceiptTemplateNotEqual");
                        }
                    }

                    //�ж������ӡģ���Ƿ�һ��
                    if (huTemplate == null)
                    {
                        huTemplate = inProcessLocation.HuTemplate;
                    }
                    else
                    {
                        if (inProcessLocation.HuTemplate != null && inProcessLocation.HuTemplate != huTemplate)
                        {
                            throw new BusinessErrorException("Order.Error.ReceiveOrder.HuTemplateNotEqual");
                        }
                    }

                    #region �����ջ����촦��ѡ��
                    if (grGapTo == null)
                    {
                        grGapTo = inProcessLocation.GoodsReceiptGapTo;
                    }
                    else
                    {
                        if (inProcessLocation.GoodsReceiptGapTo != null && inProcessLocation.GoodsReceiptGapTo != grGapTo)
                        {
                            throw new BusinessErrorException("Order.Error.ReceiveOrder.GoodsReceiptGapToNotEqual");
                        }
                    }
                    #endregion

                    IListHelper.AddRange<InProcessLocationDetail>(
                        inProcessLocationDetailList, this.inProcessLocationDetailMgr.GetInProcessLocationDetail(inProcessLocation));
                }
            }
            #endregion

            IList<ReceiptDetail> targetReceiptDetailList = receipt.ReceiptDetails;
            receipt.ReceiptDetails = null;   //���Asn��ϸ���Ժ����

            #region �����ջ���Head
            receipt.ReceiptNo = numberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_RECEIPT);
            receipt.OrderType = orderType;
            receipt.CreateDate = DateTime.Now;
            receipt.CreateUser = user;
            receipt.PartyFrom = partyFrom;
            receipt.PartyTo = partyTo;
            receipt.ShipFrom = shipFrom;
            receipt.ShipTo = shipTo;
            receipt.DockDescription = dockDescription;
            receipt.ReceiptTemplate = receiptTemplate;
            receipt.IsPrinted = false;
            receipt.NeedPrint = false;
            if (receipt.InProcessLocations != null && receipt.InProcessLocations.Count > 0)
            {
                foreach (InProcessLocation inProcessLocation in receipt.InProcessLocations)
                {
                    if (inProcessLocation.NeedPrintReceipt)
                    {
                        receipt.NeedPrint = true;
                        break;
                    }
                }
            }

            this.CreateReceipt(receipt);
            log.Debug("Create receipt " + receipt.ReceiptNo + " head successful");
            #endregion

            #region HU����/������/�����ջ���ϸ
            log.Debug("Start create receipt detail");
            IList<LocationLotDetail> inspectLocationLotDetailList = new List<LocationLotDetail>();
            foreach (ReceiptDetail receiptDetail in targetReceiptDetailList)
            {
                OrderLocationTransaction orderLocationTransaction = receiptDetail.OrderLocationTransaction;
                OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
                OrderHead orderHead = orderDetail.OrderHead;

                if (orderHead.CreateHuOption == BusinessConstants.CODE_MASTER_CREATE_HU_OPTION_VALUE_GR
                        && receiptDetail.HuId == null)  //�����������Ϊ�ջ�ʱ����Hu�������ջ�ʱ�Ѿ�ɨ���Hu�ˣ�����ɨ�账��
                {
                    #region �ջ�ʱ����Hu
                    log.Debug("Create receipt detail with generate barcode.");
                    #region ���������ջ�+ʣ����ͷ����Hu
                    decimal oddQty = 0;

                    if (!isOddCreateHu && orderDetail.HuLotSize.HasValue
                        && orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION) //ֻ������֧����ͷ
                    {
                        #region ����ʣ����ͷ + �����ջ����Ƿ��ܹ�����Hu
                        Hu oddHu = this.CreateHuFromOdd(receiptDetail, user);
                        if (oddHu != null)
                        {
                            log.Debug("Generate barcode using odd qty.");
                            //�����ͷ������Hu�������ջ�����ۼ�
                            #region ����Hu
                            IList<Hu> oddHuList = new List<Hu>();
                            oddHuList.Add(oddHu);
                            IList<ReceiptDetail> oddReceiptDetailList = this.receiptDetailMgr.CreateReceiptDetail(receipt, orderLocationTransaction, oddHuList);
                            log.Debug("Generate odd barcode successful.");
                            #endregion

                            #region ���
                            IList<InventoryTransaction> inventoryTransactionList = this.locationMgr.InventoryIn(oddReceiptDetailList[0], user, receiptDetail.PutAwayBinCode);
                            log.Debug("odd Inventory in successful.");
                            #endregion

                            #region �Ƿ����
                            if (orderDetail.NeedInspection && orderHead.NeedInspection && orderHead.SubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML)
                            {
                                foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                                {
                                    LocationLotDetail locationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransaction.LocationLotDetailId);
                                    locationLotDetail.CurrentInspectQty = locationLotDetail.Qty;
                                    inspectLocationLotDetailList.Add(locationLotDetail);
                                }
                            }
                            #endregion
                        }
                        #endregion

                        oddQty = receiptDetail.ReceivedQty.HasValue && orderDetail.HuLotSize.HasValue ?
                            receiptDetail.ReceivedQty.Value % orderDetail.HuLotSize.Value : 0;   //�ջ���ͷ��
                        log.Debug("Receive odd qty is " + oddQty);

                        receiptDetail.ReceivedQty = receiptDetail.ReceivedQty.Value - oddQty; //�ջ���������                        
                    }
                    #endregion

                    #region ����װ/��ͷ����Hu����
                    if (receiptDetail.ReceivedQty.HasValue
                        || receiptDetail.RejectedQty.HasValue
                        || receiptDetail.ScrapQty.HasValue)
                    {
                        //����Hu
                        IList<Hu> huList = this.huMgr.CreateHu(receiptDetail, user);
                        log.Debug("Create barcode successful.");

                        //�����ջ���
                        IList<ReceiptDetail> receiptDetailList = this.receiptDetailMgr.CreateReceiptDetail(receipt, orderLocationTransaction, huList);
                        log.Debug("Create receipt detail successful.");

                        #region ������д�Ʒ���߷�Ʒ�ջ�����ӵ��ջ��б�
                        if ((receiptDetail.RejectedQty.HasValue && receiptDetail.RejectedQty > 0)
                            || (receiptDetail.ScrapQty.HasValue && receiptDetail.ScrapQty > 0))
                        {
                            ReceiptDetail rejAndScrapReceiptDetail = new ReceiptDetail();
                            CloneHelper.CopyProperty(receiptDetail, rejAndScrapReceiptDetail);
                            rejAndScrapReceiptDetail.ReceivedQty = null;
                            rejAndScrapReceiptDetail.PutAwayBinCode = null;
                            rejAndScrapReceiptDetail.Receipt = receipt;

                            this.receiptDetailMgr.CreateReceiptDetail(rejAndScrapReceiptDetail);

                            receiptDetailList.Add(rejAndScrapReceiptDetail);
                            receipt.AddReceiptDetail(rejAndScrapReceiptDetail);
                        }
                        #endregion

                        foreach (ReceiptDetail huReceiptDetail in receiptDetailList)
                        {
                            #region ƥ��ReceiptDetail��InProcessLocationDetail��Copy�����Ϣ
                            if (orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                            {
                                IList<InProcessLocationDetail> matchInProcessLocationDetailList = OrderHelper.FindMatchInProcessLocationDetail(receiptDetail, inProcessLocationDetailList);
                                if (matchInProcessLocationDetailList != null && matchInProcessLocationDetailList.Count > 0)
                                {
                                    if (matchInProcessLocationDetailList.Count > 1)
                                    {
                                        //ֻ�е�ASN�а������룬�������ջ������ջ��󴴽�������п��ܷ������������
                                        //��̬����ô�ɡ�
                                        log.Error("ֻ�е�ASN�а������룬�������ջ������ջ��󴴽�������п��ܷ������������");
                                        throw new BusinessErrorException("���Ǳ�̬����ô���á�");
                                    }

                                    //����ҵ�ƥ���ֻ������һ��
                                    huReceiptDetail.PlannedBill = matchInProcessLocationDetailList[0].PlannedBill;
                                    huReceiptDetail.IsConsignment = matchInProcessLocationDetailList[0].IsConsignment;
                                    huReceiptDetail.ShippedQty = matchInProcessLocationDetailList[0].Qty;

                                    this.receiptDetailMgr.UpdateReceiptDetail(huReceiptDetail);
                                }

                                //�ջ�����HU������PlannedAmount��todo����������
                                huReceiptDetail.PlannedAmount = receiptDetail.PlannedAmount / receiptDetail.ReceivedQty.Value * huReceiptDetail.ReceivedQty.Value;
                            }
                            #endregion

                            #region ���
                            IList<InventoryTransaction> inventoryTransactionList = this.locationMgr.InventoryIn(huReceiptDetail, user, receiptDetail.PutAwayBinCode);
                            log.Debug("Inventory in successful.");
                            #endregion

                            #region �Ƿ����
                            if (orderDetail.NeedInspection
                                && orderHead.NeedInspection
                                && orderHead.SubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML
                                && huReceiptDetail.ReceivedQty.HasValue
                                && huReceiptDetail.ReceivedQty > 0)
                            {
                                foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                                {
                                    if (inventoryTransaction.Location.Code != BusinessConstants.SYSTEM_LOCATION_REJECT)
                                    {
                                        LocationLotDetail locationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransaction.LocationLotDetailId);
                                        locationLotDetail.CurrentInspectQty = inventoryTransaction.Qty;
                                        inspectLocationLotDetailList.Add(locationLotDetail);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion

                    #region ����ʣ����ͷ����
                    if (oddQty > 0)
                    {
                        log.Debug("Start handle odd qty.");
                        ReceiptDetail oddReceiptDetail = new ReceiptDetail();
                        CloneHelper.CopyProperty(receiptDetail, oddReceiptDetail);

                        oddReceiptDetail.ReceivedQty = oddQty;
                        oddReceiptDetail.RejectedQty = 0;
                        oddReceiptDetail.ScrapQty = 0;

                        #region ��ͷ���
                        oddReceiptDetail.Receipt = receipt;
                        IList<InventoryTransaction> inventoryTransactionList = this.locationMgr.InventoryIn(oddReceiptDetail, user, receiptDetail.PutAwayBinCode);
                        #endregion

                        #region ��ͷ�����ջ���ϸ
                        this.receiptDetailMgr.CreateReceiptDetail(oddReceiptDetail);
                        receipt.AddReceiptDetail(oddReceiptDetail);
                        #endregion

                        #region ����HuOdd
                        LocationLotDetail locationLotDetail = locationLotDetailMgr.LoadLocationLotDetail(inventoryTransactionList[0].LocationLotDetailId);
                        this.huOddMgr.CreateHuOdd(oddReceiptDetail, locationLotDetail, user);
                        #endregion
                        log.Debug("End handle odd qty.");
                    }
                    #endregion

                    #endregion
                }
                else
                {
                    #region �ջ�ʱ������Hu
                    log.Debug("Create receipt detail with no generate barcode.");

                    #region ����Hu�ϵ�OrderNo��ReceiptNo��AntiResloveHu
                    if (receiptDetail.HuId != null && receiptDetail.HuId.Trim() != string.Empty)
                    {
                        Hu hu = this.huMgr.LoadHu(receiptDetail.HuId);
                        bool isUpdated = false;

                        if (hu.OrderNo == null || hu.ReceiptNo == null)
                        {
                            if (hu.OrderNo == null)
                            {
                                log.Debug("Update hu OrderNo " + orderHead.OrderNo + ".");
                                hu.OrderNo = orderHead.OrderNo;
                            }

                            if (hu.ReceiptNo == null)
                            {
                                log.Debug("Update hu ReceiptNo " + receipt.ReceiptNo + ".");
                                hu.ReceiptNo = receipt.ReceiptNo;
                            }

                            isUpdated = true;
                        }

                        if (hu.AntiResolveHu == null
                            && orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                        {
                            hu.AntiResolveHu = orderHead.AntiResolveHu;
                            isUpdated = true;
                        }

                        if (isUpdated)
                        {
                            this.huMgr.UpdateHu(hu);
                        }
                    }
                    #endregion

                    IList<ReceiptDetail> noCreateHuReceiptDetailList = new List<ReceiptDetail>();

                    #region ƥ��ReceiptDetail��InProcessLocationDetail��Copy�����Ϣ
                    log.Debug("Start match ReceiptDetail and InProcessLocationDetail.");
                    if (orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION
                        && orderHead.SubType != BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_ADJ)  //�ջ������Ѿ�ƥ���InProcessLocationDetail������Ҫ��ƥ��
                    {
                        IList<InProcessLocationDetail> matchInProcessLocationDetailList = OrderHelper.FindMatchInProcessLocationDetail(receiptDetail, inProcessLocationDetailList);
                        log.Debug("Find matched InProcessLocationDetailList, count = " + matchInProcessLocationDetailList != null ? matchInProcessLocationDetailList.Count : 0);

                        if (matchInProcessLocationDetailList != null && matchInProcessLocationDetailList.Count == 1)
                        {
                            //һ���ջ���Ӧһ�η�����
                            log.Debug("one ipdet vs one receiptdet.");
                            receiptDetail.PlannedBill = matchInProcessLocationDetailList[0].PlannedBill;
                            receiptDetail.IsConsignment = matchInProcessLocationDetailList[0].IsConsignment;
                            if (matchInProcessLocationDetailList[0].InProcessLocation.Type == BusinessConstants.CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_GAP)
                            {
                                receiptDetail.ShippedQty = 0 - matchInProcessLocationDetailList[0].Qty;
                            }
                            else
                            {
                                receiptDetail.ShippedQty = matchInProcessLocationDetailList[0].Qty;
                            }
                            receiptDetail.ReceivedInProcessLocationDetail = matchInProcessLocationDetailList[0];
                            noCreateHuReceiptDetailList.Add(receiptDetail);
                        }
                        else if (matchInProcessLocationDetailList != null && matchInProcessLocationDetailList.Count > 1)
                        {
                            //һ���ջ���Ӧ��η�����
                            //�磺���������룬�ջ���������
                            log.Debug("multi ipdet vs one receiptdet.");
                            decimal totalRecQty = receiptDetail.ReceivedQty.Value;
                            InProcessLocationDetail lastInProcessLocationDetail = null;
                            log.Debug("Start Fetch matched InProcessLocationDetailList.");
                            foreach (InProcessLocationDetail inProcessLocationDetail in matchInProcessLocationDetailList)
                            {
                                lastInProcessLocationDetail = inProcessLocationDetail; //��¼���һ�η������û�ж�Ӧ�������ջ���ʹ��

                                if (inProcessLocationDetail.ReceivedQty.HasValue && Math.Abs(inProcessLocationDetail.ReceivedQty.Value) >= Math.Abs(inProcessLocationDetail.Qty))
                                {
                                    continue;
                                }

                                if (Math.Abs(totalRecQty) > 0)
                                {
                                    log.Debug("Start cloned ReceiptDetail.");
                                    ReceiptDetail clonedReceiptDetail = new ReceiptDetail();
                                    CloneHelper.CopyProperty(receiptDetail, clonedReceiptDetail);
                                    log.Debug("End cloned ReceiptDetail.");

                                    clonedReceiptDetail.PlannedBill = inProcessLocationDetail.PlannedBill;
                                    clonedReceiptDetail.IsConsignment = inProcessLocationDetail.IsConsignment;

                                    #region
                                    if (matchInProcessLocationDetailList[0].InProcessLocation.Type == BusinessConstants.CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_GAP)
                                    {
                                        inProcessLocationDetail.Qty = 0 - inProcessLocationDetail.Qty;
                                    }
                                    #endregion

                                    if (Math.Abs(totalRecQty) > Math.Abs(inProcessLocationDetail.Qty - (inProcessLocationDetail.ReceivedQty.HasValue ? inProcessLocationDetail.ReceivedQty.Value : decimal.Zero)))
                                    {
                                        clonedReceiptDetail.ReceivedQty = inProcessLocationDetail.Qty - (inProcessLocationDetail.ReceivedQty.HasValue ? inProcessLocationDetail.ReceivedQty.Value : decimal.Zero);
                                        clonedReceiptDetail.ShippedQty = inProcessLocationDetail.Qty - (inProcessLocationDetail.ReceivedQty.HasValue ? inProcessLocationDetail.ReceivedQty.Value : decimal.Zero);
                                        totalRecQty -= inProcessLocationDetail.Qty - (inProcessLocationDetail.ReceivedQty.HasValue ? inProcessLocationDetail.ReceivedQty.Value : decimal.Zero);
                                    }
                                    else
                                    {
                                        clonedReceiptDetail.ReceivedQty = totalRecQty;
                                        clonedReceiptDetail.ShippedQty = totalRecQty;
                                        totalRecQty = 0;
                                    }

                                    //��Ϊȥ������������¼�Ѿ�ƥ��ķ����������촦���ʱ��ƥ��������������졣
                                    clonedReceiptDetail.ReceivedInProcessLocationDetail = inProcessLocationDetail;

                                    noCreateHuReceiptDetailList.Add(clonedReceiptDetail);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            log.Debug("End Fetch matched InProcessLocationDetailList.");

                            //���գ�û���ҵ���Ӧ�ķ����ֻ��¼�ջ�������������0
                            if (Math.Abs(totalRecQty) > 0)
                            {
                                ReceiptDetail clonedReceiptDetail = new ReceiptDetail();
                                CloneHelper.CopyProperty(receiptDetail, clonedReceiptDetail);

                                clonedReceiptDetail.ShippedQty = 0;
                                clonedReceiptDetail.ReceivedQty = totalRecQty;
                                clonedReceiptDetail.ReceivedInProcessLocationDetail = lastInProcessLocationDetail;

                                noCreateHuReceiptDetailList.Add(clonedReceiptDetail);
                            }
                        }
                        else
                        {
                            noCreateHuReceiptDetailList.Add(receiptDetail);
                        }
                    }
                    else
                    {
                        noCreateHuReceiptDetailList.Add(receiptDetail);
                    }
                    log.Debug("End match ReceiptDetail and InProcessLocationDetail.");
                    #endregion

                    foreach (ReceiptDetail noCreateHuReceiptDetail in noCreateHuReceiptDetailList)
                    {
                        noCreateHuReceiptDetail.Receipt = receipt;

                        if (noCreateHuReceiptDetail.ReceivedQty != 0)
                        {
                            #region ���
                            log.Debug("Start Inventory In.");
                            IList<InventoryTransaction> inventoryTransactionList = this.locationMgr.InventoryIn(noCreateHuReceiptDetail, user, noCreateHuReceiptDetail.PutAwayBinCode);
                            log.Debug("End Inventory In.");
                            #endregion

                            #region �Ƿ����
                            if (orderDetail.NeedInspection && orderHead.NeedInspection && inventoryTransactionList != null && inventoryTransactionList.Count > 0 &&
                                orderHead.SubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML)
                            {
                                foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                                {
                                    if (inventoryTransaction.Location.Code != BusinessConstants.SYSTEM_LOCATION_REJECT)
                                    {
                                        LocationLotDetail locationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransaction.LocationLotDetailId);
                                        locationLotDetail.CurrentInspectQty = inventoryTransaction.Qty;
                                        inspectLocationLotDetailList.Add(locationLotDetail);
                                    }
                                }
                            }
                            #endregion
                        }

                        #region �����ջ���ϸ
                        log.Debug("Start Create Receipt Detail.");
                        this.receiptDetailMgr.CreateReceiptDetail(noCreateHuReceiptDetail);
                        receipt.AddReceiptDetail(noCreateHuReceiptDetail);
                        log.Debug("End Create Receipt Detail.");
                        #endregion
                    }

                    #endregion
                }
            }
            #endregion

            #region ����
            if (inspectLocationLotDetailList.Count > 0)
            {
                //����û��Hu�ģ�����ջ�ʱ�Ѿ��س��˸�����棬Ҳ���ǿ�������ʹ�����������һ�¿��ܻ�������
                //����ipno��receiptno��isseperated�ֶ�
                this.inspectOrderMgr.CreateInspectOrder(inspectLocationLotDetailList, user, receipt.InProcessLocations[0].IpNo, receipt.ReceiptNo, false);
            }
            #endregion

            //#region ƥ���ջ���������Ҳ���
            //IList<InProcessLocationDetail> gapInProcessLocationDetailList = new List<InProcessLocationDetail>();

            //#region �����ƥ��
            //foreach (InProcessLocationDetail inProcessLocationDetail in inProcessLocationDetailList)
            //{
            //    if (inProcessLocationDetail.OrderLocationTransaction.OrderDetail.OrderHead.Type
            //        != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)   //������ʱ��֧�ֲ���
            //    {
            //        decimal receivedQty = 0;  //��������ۼ��ջ���

            //        //һ����������ܶ�Ӧ�����ջ���
            //        foreach (ReceiptDetail receiptDetail in receipt.ReceiptDetails)
            //        {
            //            //ƥ���ջ���ͷ�����
            //            if (receiptDetail.ReceivedInProcessLocationDetail != null)
            //            {
            //                //�����Ѿ�ƥ��ģ�ֱ�Ӱ�������ƥ��
            //                if (receiptDetail.ReceivedInProcessLocationDetail.Id == inProcessLocationDetail.Id)
            //                {
            //                    if (receiptDetail.ReceivedQty.HasValue)
            //                    {
            //                        receivedQty += receiptDetail.ReceivedQty.Value;
            //                    }
            //                }
            //            }
            //            else if (OrderHelper.IsInProcessLocationDetailMatchReceiptDetail(
            //                inProcessLocationDetail, receiptDetail))
            //            {
            //                if (receiptDetail.ReceivedQty.HasValue)
            //                {
            //                    receivedQty += receiptDetail.ReceivedQty.Value;
            //                }
            //            }
            //        }

            //        if (receivedQty != inProcessLocationDetail.Qty)
            //        {
            //            #region �ջ������ͷ���������ƥ�䣬��¼����
            //            InProcessLocationDetail gapInProcessLocationDetail = new InProcessLocationDetail();
            //            gapInProcessLocationDetail.Qty = receivedQty - inProcessLocationDetail.Qty;
            //            gapInProcessLocationDetail.OrderLocationTransaction = inProcessLocationDetail.OrderLocationTransaction;
            //            //gapInProcessLocationDetail.HuId = inProcessLocationDetail.HuId;
            //            gapInProcessLocationDetail.LotNo = inProcessLocationDetail.LotNo;
            //            gapInProcessLocationDetail.IsConsignment = inProcessLocationDetail.IsConsignment;
            //            gapInProcessLocationDetail.PlannedBill = inProcessLocationDetail.PlannedBill;

            //            gapInProcessLocationDetailList.Add(gapInProcessLocationDetail);
            //            #endregion
            //        }
            //    }
            //}
            //#endregion

            //#region �ջ��ƥ��
            //foreach (ReceiptDetail receiptDetail in receipt.ReceiptDetails)
            //{
            //    if (receiptDetail.OrderLocationTransaction.OrderDetail.OrderHead.Type
            //        != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)   //������ʱ��֧�ֲ���
            //    {
            //        IList<InProcessLocationDetail> matchInProcessLocationDetailList = OrderHelper.FindMatchInProcessLocationDetail(receiptDetail, inProcessLocationDetailList);

            //        if (matchInProcessLocationDetailList == null || matchInProcessLocationDetailList.Count == 0)
            //        {
            //            OrderLocationTransaction outOrderLocationTransaction =
            //                this.orderLocationTransactionMgr.GetOrderLocationTransaction(receiptDetail.OrderLocationTransaction.OrderDetail, BusinessConstants.IO_TYPE_OUT)[0];
            //            #region û���ҵ����ջ����Ӧ�ķ�����
            //            InProcessLocationDetail gapInProcessLocationDetail = new InProcessLocationDetail();
            //            gapInProcessLocationDetail.Qty = receiptDetail.ReceivedQty.Value;
            //            gapInProcessLocationDetail.OrderLocationTransaction = outOrderLocationTransaction;
            //            //gapInProcessLocationDetail.HuId = receiptDetail.HuId;
            //            gapInProcessLocationDetail.LotNo = receiptDetail.LotNo;
            //            gapInProcessLocationDetail.IsConsignment = receiptDetail.IsConsignment;
            //            gapInProcessLocationDetail.PlannedBill = receiptDetail.PlannedBill;

            //            gapInProcessLocationDetailList.Add(gapInProcessLocationDetail);
            //            #endregion
            //        }
            //    }
            //}
            //#endregion
            //#endregion

            #region �ر�InProcessLocation
            if (receipt.InProcessLocations != null && receipt.InProcessLocations.Count > 0)
            {
                foreach (InProcessLocation inProcessLocation in receipt.InProcessLocations)
                {
                    if (inProcessLocation.IsAsnUniqueReceipt)
                    {
                        //��֧�ֶ���ջ�ֱ�ӹر�                      
                        this.inProcessLocationMgr.CloseInProcessLocation(inProcessLocation, user);
                    }
                    else
                    {
                        this.inProcessLocationMgr.TryCloseInProcessLocation(inProcessLocation, user);
                    }

                    //transportationOrderMgr.TryCompleteTransportationOrder(inProcessLocation, user);
                }
            }
            #endregion
        }

        [Transaction(TransactionMode.Unspecified)]
        public Receipt LoadReceipt(string receiptNo, User user)
        {
            return LoadReceipt(receiptNo, user, false, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Receipt LoadReceipt(string receiptNo, bool includeDetail)
        {
            return LoadReceipt(receiptNo, null, includeDetail, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Receipt LoadReceipt(string receiptNo, User user, bool includeDetail)
        {
            return LoadReceipt(receiptNo, user, includeDetail, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Receipt LoadReceipt(string receiptNo, bool includeDetail, bool includeInProcessLocations)
        {
            return LoadReceipt(receiptNo, null, includeDetail, includeInProcessLocations);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Receipt LoadReceipt(string receiptNo, User user, bool includeDetail, bool includeInProcessLocations)
        {
            Receipt receipt = this.LoadReceipt(receiptNo);

            if (user != null)
            {
                CheckReceiptOperationAuthrize(receipt, user);
            }

            if (includeDetail && receipt.ReceiptDetails != null && receipt.ReceiptDetails.Count > 0)
            {
            }
            if (includeInProcessLocations && receipt.InProcessLocations != null && receipt.InProcessLocations.Count > 0)
            {
            }
            return receipt;
        }

        [Transaction(TransactionMode.Unspecified)]
        public Receipt CheckAndLoadReceipt(string receiptNo)
        {
            Receipt receipt = this.LoadReceipt(receiptNo);
            if (receipt == null)
            {
                throw new BusinessErrorException("Common.Business.Error.EntityNotExist", receiptNo);
            }
            return receipt;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Receipt> GetReceipts(string userCode, int firstRow, int maxRows, params string[] orderTypes)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Receipt));
            //todo Ȩ��У��
            //SecurityHelper.SetPartySearchCriteria(criteria, "PartyFrom.Code", userCode);
            //SecurityHelper.SetPartySearchCriteria(criteria, "PartyTo.Code", userCode);
            criteria.Add(Expression.Eq("CreateUser.Code", userCode));
            if (orderTypes.Length == 1)
            {
                criteria.Add(Expression.Eq("OrderType", orderTypes[0]));
            }
            else
            {
                criteria.Add(Expression.In("OrderType", orderTypes));
            }
            criteria.Add(Expression.Ge("CreateDate", DateTime.Today));
            criteria.AddOrder(Order.Desc("ReceiptNo"));
            IList<Receipt> receiptList = criteriaMgr.FindAll<Receipt>(criteria, firstRow, maxRows);
            if (receiptList.Count > 0)
            {
                return receiptList;
            }
            return null;
        }

        #endregion Customized Methods

        #region Private Methods
        private Hu CreateHuFromOdd(ReceiptDetail receiptDetail, User user)
        {
            OrderLocationTransaction orderLocationTransaction = receiptDetail.OrderLocationTransaction;
            OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
            OrderHead orderHead = orderDetail.OrderHead;
            Receipt receipt = receiptDetail.Receipt;

            int huLotSize = orderDetail.HuLotSize.Value;
            decimal qty = 0; //�ۼƿ��Odd����

            #region ѭ����ȡ���Odd����
            IList<HuOdd> huOddList = this.huOddMgr.GetHuOdd(
                orderDetail.Item, orderDetail.UnitCount, orderDetail.Uom,
                orderDetail.DefaultLocationFrom, orderDetail.DefaultLocationTo,
                BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE);

            if (huOddList != null && huOddList.Count > 0)
            {
                foreach (HuOdd huOdd in huOddList)
                {
                    qty += huOdd.OddQty - huOdd.CreatedQty;
                }

                if (qty + receiptDetail.ReceivedQty.Value >= huLotSize)
                {
                    #region ��������������Hu
                    DateTime dateTimeNow = DateTime.Now;

                    #region ���¿��Odd������
                    foreach (HuOdd huOdd in huOddList)
                    {
                        //ȫ���ر�
                        huOdd.CurrentCreateQty = huOdd.OddQty - huOdd.CreatedQty;
                        huOdd.CreatedQty = huOdd.OddQty;
                        huOdd.LastModifyDate = dateTimeNow;
                        huOdd.LastModifyUser = user;
                        huOdd.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;

                        this.huOddMgr.UpdateHuOdd(huOdd);

                        //����
                        this.locationMgr.InventoryOut(huOdd, receiptDetail, user);
                    }
                    #endregion

                    #region ���ܻ�����ͷ����Ҫ����receiptDetail�ջ������������ջ��� + ���Odd�� - Hu����
                    receiptDetail.ReceivedQty = qty + receiptDetail.ReceivedQty.Value - huLotSize;
                    #endregion

                    #region ����Hu
                    ReceiptDetail clonedReceiptDetail = new ReceiptDetail();
                    CloneHelper.CopyProperty(receiptDetail, clonedReceiptDetail);
                    clonedReceiptDetail.ReceivedQty = huLotSize;
                    clonedReceiptDetail.RejectedQty = 0;
                    IList<Hu> huList = this.huMgr.CreateHu(clonedReceiptDetail, user);

                    return huList[0];
                    #endregion

                    #endregion
                }
            }

            return null;

            #endregion
        }

        private void CheckReceiptOperationAuthrize(Receipt receipt, User user)
        {
            if (receipt == null)
            {
                throw new BusinessErrorException("MasterData.Receipt.NotExit");
            }
            //bool partyFromAuthrized = false;
            bool partyToAuthrized = false;
            foreach (Permission permission in user.Permissions)
            {
                //if (permission.Code == receipt.PartyFrom.Code)
                //{
                //    partyFromAuthrized = true;
                //}

                if (permission.Code == receipt.PartyTo.Code)
                {
                    partyToAuthrized = true;
                    break;
                }

                //if (partyFromAuthrized && partyToAuthrized)
                //{
                //    break;
                //}
            }

            //if (!(partyFromAuthrized && partyToAuthrized))
            if (!partyToAuthrized)
            {
                //û�и�asn�Ĳ���Ȩ��
                throw new BusinessErrorException("Receipt.Error.NoAuthrization", receipt.ReceiptNo);

            }
        }

        #endregion
    }
}