using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.MRP;
using com.Sconit.Service.Ext;
using com.Sconit.Service.Ext.MasterData;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using com.Sconit.Persistence.MRP;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using System.Data.SqlClient;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP.Impl
{
    [Transactional]
    public class FlatBomMgr : FlatBomBaseMgr, IFlatBomMgr
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.FlatBom");
        private static List<object[]>  itemCodeTyes = new List<object[]>();
        public IGenericMgr genericMgr;
        public IBomDetailMgr bomDetailMgr;
        public IUomConversionMgr uomConversionMgr;
        public ICriteriaMgr criterialMgr;
        //public ISqlHelperMgrE sqlHelperMgr { get; set; }

        public FlatBomMgr(IGenericMgr genericMgr,
            IBomDetailMgr bomDetailMgr,
            IUomConversionMgr uomConversionMgr,
            ICriteriaMgr criterialMgr,
            IFlatBomDao entityDao)
            : base(genericMgr, bomDetailMgr, uomConversionMgr, entityDao)
        {
            this.genericMgr = genericMgr;
            this.bomDetailMgr = bomDetailMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.criterialMgr = criterialMgr;
        }
        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public void GenFlatBom(string userCode)
        {
            #region �ֽ�BOM
            log.Info(string.Format("---- ��ʼ�ֽ�Bom ---"));

//            //����
//            var itemTable = this.genericMgr.GetDatasetBySql
//                (@" select distinct(Item) as Item from FlowDet f
//                    join FlowMstr m on f.Flow = m.Code join Location l on m.LocTo = l.Code
//                    where l.TextField4 = '1' and m.Type ='Transfer' and m.IsMRP=1 and f.MrpWeight>0 ").Tables[0];

            var itemTable = this.genericMgr.GetDatasetBySql
             (@" select distinct(Item) as Item from FlowDet f join FlowMstr m on f.Flow = m.Code
                                where m.Type ='Distribution' and m.IsMRP=1 and f.MrpWeight>0 ").Tables[0];
            var itemTypes = this.genericMgr.GetDatasetBySql(" select Code,Type,Uom from Item ").Tables[0];
            itemCodeTyes = new List<object[]>();
            foreach (DataRow dr in itemTypes.Rows)
            {
                itemCodeTyes.Add(new object[] { dr.ItemArray[0], dr.ItemArray[1], dr.ItemArray[2] });
            }

            DateTime dateTimeNow=DateTime.Now;
            var allBomDetails = this.genericMgr.GetDatasetBySql(string.Format(" select Bom,Item,Uom,StartDate,EndDate,ScrapPct,RateQty,Op,Ref,StruType from BomDet as b where StartDate<='{0}' and (EndDate>='{1}' or EndDate is null ) ",dateTimeNow,dateTimeNow)).Tables[0];

            IList<TempBomDetail> allBomDetailList = new List<TempBomDetail>();
            foreach (DataRow dr in allBomDetails.Rows)
            {
                allBomDetailList.Add(new TempBomDetail() {
                    Bom = dr.ItemArray[0].ToString(),
                    Item = dr.ItemArray[1].ToString(),
                    Uom = dr.ItemArray[2].ToString(),
                    StartDate = Convert.ToDateTime(dr.ItemArray[3]),
                    EndDate = string.IsNullOrEmpty(((object)dr.ItemArray[4]).ToString())?null:(DateTime?)Convert.ToDateTime(dr.ItemArray[4]),
                    ScrapPercentage = Convert.ToDecimal(dr.ItemArray[5]),
                    RateQty = Convert.ToDecimal(dr.ItemArray[6]),
                    Operation = dr.ItemArray[7].ToString(),
                    Reference = dr.ItemArray[8].ToString(),
                    StructureType = dr.ItemArray[9].ToString(),
                });
            }
           
            int i = itemTable.Rows.Count;
            int j = 0;
            IList<FlatBom> flatBoms = new List<FlatBom>();
            foreach (DataRow dr in itemTable.Rows)
            {
                j++;
                //if (j < 1636)
                //{
                //    continue;
                //}
                var itemCode = (string)dr.ItemArray[0];
                var item = itemCodeTyes.Where(d => d[0].ToString().ToUpper() == itemCode.ToUpper());
                if (item == null || item.Count() == 0 || item.First()[1].ToString()=="P")
                {
                    continue;
                }
                //var item = genericMgr.FindById<Item>(itemCode);
                //if (item.Type == "P")
                //{
                //    continue;
                //}
                var bomDetails = this.GetFlatBomDetail(itemCode, dateTimeNow, allBomDetailList);
                if (bomDetails != null && bomDetails.Count > 0)
                {
                    foreach (var bomDetail in bomDetails)
                    {
                        var checkdoubleFlatBom = flatBoms.Where(f => f.Bom == bomDetail.Bom && f.Item == bomDetail.Item);
                        if (checkdoubleFlatBom != null && checkdoubleFlatBom.Count() > 0)
                        {
                            continue;
                        }
                        else
                        {
                            FlatBom flatBom = new FlatBom();
                            flatBom.Bom = itemCode;
                            flatBom.Item = bomDetail.Item;
                            flatBom.Fg = itemCode;
                            flatBom.BomLevel = 1;
                            flatBom.Qty = 1;
                            //flatBom.RateQty = (double)((bomDetail.ScrapPercentage.HasValue ? bomDetail.ScrapPercentage.Value : 0M) + 1M) *
                            //    (double)this.ConvertUomQty(bomDetail.Item, bomDetail.Uom, bomDetail.RateQty, bomDetail.Item.Uom);
                            flatBom.RateQty = flatBom.Qty * (double)(bomDetail.ScrapPercentage + 1) *
                            (double)this.ConvertUomQty(bomDetail.Item, bomDetail.Uom, bomDetail.RateQty, itemCodeTyes.Where(d => d[0].ToString().ToUpper() == bomDetail.Item.ToUpper()).First()[2].ToString());
                            flatBom.CreateDate = DateTime.Now;
                            flatBom.CreateUser = userCode;
                            //this.genericMgr.Create(flatBom);
                            flatBoms.Add(flatBom);
                            this.GetNextBom(flatBom, 0, flatBoms, allBomDetailList);
                        }
                    }
                }
                else
                {
                    log.Warn(string.Format("û���ҵ�����{0}��bom", itemCode));
                }
                //this.genericMgr.FlushSession();
            }
            j = 0;
            if (flatBoms != null && flatBoms.Count() > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (var flatBom in flatBoms)
                {
                    j++;
                    sb.Append(string.Format("'{0}','{1}','{2}',{3},{4},{5},'{6}','{7}',{8};", flatBom.Bom, flatBom.Fg, flatBom.Item, flatBom.BomLevel, flatBom.Qty, flatBom.RateQty, flatBom.CreateDate, flatBom.CreateUser, flatBom.IsLastLevel?1:0));
                }
                //this.genericMgr.ExecuteSql(" truncate table MRP_FlatBom ", null);
                //DataTable insertFlatBomTable = new DataTable("LocationArrayTable");
                //insertFlatBomTable.Columns.Add("Bom", typeof(string));
                //insertFlatBomTable.Columns.Add("Fg", typeof(string));
                //insertFlatBomTable.Columns.Add("Item", typeof(string));
                //insertFlatBomTable.Columns.Add("BomLevel", typeof(Int32));
                //insertFlatBomTable.Columns.Add("Qty", typeof(double));
                //insertFlatBomTable.Columns.Add("RateQty", typeof(double));
                //insertFlatBomTable.Columns.Add("CreateDate", typeof(DateTime));
                //insertFlatBomTable.Columns.Add("CreateUser", typeof(string));
                //insertFlatBomTable.Columns.Add("IsLastLevel", typeof(bool));
               
                //foreach (var flatBom in flatBoms)
                //{
                //    insertFlatBomTable.Rows.Add(new object[]{flatBom.Bom,flatBom.Fg,flatBom.Item,flatBom.BomLevel,flatBom.Qty,flatBom.RateQty,flatBom.CreateDate,flatBom.CreateUser,flatBom.IsLastLevel});
                //    //this.genericMgr.Create(flatBom);
                //}
                this.genericMgr.ExecuteSql(" truncate table MRP_FlatBom ", null);
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@AllInsertParam", System.Data.SqlDbType.VarChar);
                sb.Remove(sb.Length - 1, 1);
                parameters[0].Value = sb.ToString();
                this.genericMgr.GetDatasetByStoredProcedure("USP_Busi_MRP_CreateFlatBom", parameters);
            }
            log.Info(string.Format("---- �����ֽ�Bom ---"));
            #endregion
        }

        private void GetNextBom(FlatBom parentFlatBom, int iterateCount, IList<FlatBom> flatBoms, IList<TempBomDetail> allBomDetailList)
        {
            var item = itemCodeTyes.Where(d => d[0].ToString().ToUpper() == parentFlatBom.Item.ToUpper());
            if (item == null || item.Count() == 0 || item.First()[1].ToString() == "P")
            {
                return;
            }
            //var item = genericMgr.FindById<Item>(parentFlatBom.Item);
            //if (item.Type == "P")
            //{
            //    return;
            //}
            iterateCount++;
            if (iterateCount > 10)
            {
                log.Info(string.Format("����{0}�Ѿ�������10����δ����,���ܴ���ѭ����BOM", parentFlatBom.Item));
                return;
            }
            var bomDetails = this.GetFlatBomDetail(parentFlatBom.Item, DateTime.Now, allBomDetailList);
            if (bomDetails != null && bomDetails.Count > 0)
            {
                foreach (var bomDetail in bomDetails)
                {
                    var checkdoubleFlatBom = flatBoms.Where(f => f.Bom == bomDetail.Bom && f.Item == bomDetail.Item);
                    if (checkdoubleFlatBom != null && checkdoubleFlatBom.Count() > 0)
                    {
                        continue;
                    }
                    else
                    {
                        FlatBom flatBom = new FlatBom();
                        flatBom.Bom = parentFlatBom.Item;
                        flatBom.Item = bomDetail.Item;
                        flatBom.Fg = parentFlatBom.Fg;
                        flatBom.BomLevel = parentFlatBom.BomLevel + 1;
                        flatBom.Qty = parentFlatBom.RateQty;
                        flatBom.RateQty = flatBom.Qty * (double)(bomDetail.ScrapPercentage + 1) *
                            (double)this.ConvertUomQty(bomDetail.Item, bomDetail.Uom, bomDetail.RateQty, itemCodeTyes.Where(d => d[0].ToString().ToUpper() == bomDetail.Item.ToUpper()).First()[2].ToString());
                        flatBom.CreateDate = DateTime.Now;
                        flatBom.CreateUser = parentFlatBom.CreateUser;
                        //this.genericMgr.Create(flatBom);

                        flatBoms.Add(flatBom);
                        this.GetNextBom(flatBom, iterateCount, flatBoms, allBomDetailList);
                    }
                }
            }
            else
            {
                parentFlatBom.IsLastLevel = true;

                //this.genericMgr.Update(parentFlatBom);
            }
        }

        private double ConvertUomQty(string item, string sourceUom, decimal sourceQty, string targetUom)
        {
            try
            {
                return (double)uomConversionMgr.ConvertUomQty(item, sourceUom, sourceQty, targetUom);
            }
            catch (Exception)
            {
                log.Error(string.Format("UomConversion.Error.NotFound", item, sourceUom, targetUom));
                return 1;
            }
        }

        #region   ��Bom
        public IList<TempBomDetail> GetFlatBomDetail(string bomCode, DateTime efftiveDate, IList<TempBomDetail> allBomDetailList)
        {
            IList<TempBomDetail> flatBomDetailList = new List<TempBomDetail>();
            IList<TempBomDetail> nextBomDetailList = this.GetNextLevelBomDetail(bomCode, efftiveDate, allBomDetailList);

            if (nextBomDetailList != null && nextBomDetailList.Count > 0)
            {
                foreach (TempBomDetail nextBomDetail in nextBomDetailList)
                {
                    nextBomDetail.CalculatedQty = nextBomDetail.RateQty * (1 + nextBomDetail.ScrapPercentage);
                    ProcessCurrentBomDetail(flatBomDetailList, nextBomDetail, efftiveDate, allBomDetailList);
                }
            }
            return flatBomDetailList;
        }

        public IList<TempBomDetail> GetNextLevelBomDetail(string bomCode, DateTime efftiveDate, IList<TempBomDetail> allBomDetailList)
        {
            IList<TempBomDetail> bomDetailList = allBomDetailList.Where(a => a.Bom == bomCode && a.StartDate <= efftiveDate && (a.EndDate >= efftiveDate || a.EndDate == null)).ToList();
            return this.GetNoOverloadBomDetail(bomDetailList);
        }

        private IList<TempBomDetail> GetNoOverloadBomDetail(IList<TempBomDetail> bomDetailList)
        {
            //����BomCode��ItemCode��Operation��Reference��ͬ��BomDetail��ֻȡStartDate���ġ�
            if (bomDetailList != null && bomDetailList.Count > 0)
            {
                IList<TempBomDetail> noOverloadBomDetailList = new List<TempBomDetail>();
                foreach (TempBomDetail bomDetail in bomDetailList)
                {
                    int overloadIndex = -1;
                    for (int i = 0; i < noOverloadBomDetailList.Count; i++)
                    {
                        //�ж�BomCode��ItemCode��Operation��Reference�Ƿ���ͬ
                        if (noOverloadBomDetailList[i].Bom== bomDetail.Bom
                            && noOverloadBomDetailList[i].Item == bomDetail.Item
                            && noOverloadBomDetailList[i].Operation == bomDetail.Operation
                            && noOverloadBomDetailList[i].Reference == bomDetail.Reference)
                        {
                            //������ͬ�ģ���¼λ�á�
                            overloadIndex = i;
                            break;
                        }
                    }

                    if (overloadIndex == -1)
                    {
                        //û����ͬ�ļ�¼��ֱ�Ӱ�BomDetail���뷵���б�
                        noOverloadBomDetailList.Add(bomDetail);
                    }
                    else
                    {
                        //����ͬ�ļ�¼���ж�bomDetail.StartDate�ͽ�����еĴ�
                        if (noOverloadBomDetailList[overloadIndex].StartDate < bomDetail.StartDate)
                        {
                            //bomDetail.StartDate���ڽ�����еģ��滻�����
                            noOverloadBomDetailList[overloadIndex] = bomDetail;
                        }
                    }
                }
                return noOverloadBomDetailList;
            }
            else
            {
                return null;
            }
        }

        private void ProcessCurrentBomDetail(IList<TempBomDetail> flatBomDetailList, TempBomDetail currentBomDetail, DateTime efftiveDate, IList<TempBomDetail> allBomDetailList)
        {
            if (currentBomDetail.StructureType == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_N) //��ͨ�ṹ(N)
            {
                ProcessCurrentBomDetailByItemType(flatBomDetailList, currentBomDetail, efftiveDate, allBomDetailList);
            }
            else if (currentBomDetail.StructureType == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_O) //ѡ���(O)
            {
                currentBomDetail.OptionalItemGroup = currentBomDetail.Bom;   //Ĭ����BomCode��Ϊѡ��������
                ProcessCurrentBomDetailByItemType(flatBomDetailList, currentBomDetail, efftiveDate, allBomDetailList);
            }
            else if (currentBomDetail.StructureType == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_X) //��ṹ(X)
            {
                //�������ṹ(X)�������Լ��ӵ����ر���������·ֽ�
                NestingGetNextLevelBomDetail(flatBomDetailList, currentBomDetail, efftiveDate, allBomDetailList);
            }
            else
            {
                throw new TechnicalException("no such kind fo bomdetail structure type " + currentBomDetail.StructureType);
            }
        }

        private void ProcessCurrentBomDetailByItemType(IList<TempBomDetail> flatBomDetailList, TempBomDetail currentBomDetail, DateTime efftiveDate, IList<TempBomDetail> allBomDetailList)
        {
            var itemType = itemCodeTyes.Where(d => d[0].ToString().ToUpper() == currentBomDetail.Item.ToUpper()).First()[2].ToString();
            if (itemType.ToUpper() == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_X)
            {
                //����������(X)���������·ֽ�
                NestingGetNextLevelBomDetail(flatBomDetailList, currentBomDetail, efftiveDate,allBomDetailList);
            }
            else if (itemType.ToUpper() == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_A)
            {
                //todo ������費��Ҫ�ֽ⣿�Ƿ���������ϻس�ʱ��ָ����
                flatBomDetailList.Add(currentBomDetail);
            }
            else if (itemType.ToUpper() == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
            {
                //K���͵�Item���ܳ�����Bom�ṹ��
                throw new BusinessErrorException("Bom.Error.ItemTypeKInBom", currentBomDetail.Bom);

                //������������·ֽ�
                //NestingGetNextLevelBomDetail(flatBomDetailList, currentBomDetail, efftiveDate);
            }
            else
            {
                //ֱ�Ӽ��뵽flatBomDetailList
                flatBomDetailList.Add(currentBomDetail);
            }
        }

        private void NestingGetNextLevelBomDetail(IList<TempBomDetail> flatBomDetailList, TempBomDetail currentBomDetail, DateTime efftiveDate, IList<TempBomDetail> allBomDetailList)
        {
            IList<TempBomDetail> nextBomDetailList = this.GetNextLevelBomDetail(currentBomDetail.Item, efftiveDate, allBomDetailList);

            if (nextBomDetailList != null && nextBomDetailList.Count > 0)
            {
                foreach (TempBomDetail nextBomDetail in nextBomDetailList)
                {
                    var itemType = itemCodeTyes.Where(d => d[0].ToString().ToUpper() == nextBomDetail.Item.ToUpper()).First()[2].ToString();
                    if (itemType == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
                    {
                        //K���͵�Item���ܳ�����Bom�ṹ��
                        throw new BusinessErrorException("Bom.Error.ItemTypeKInBom", nextBomDetail.Bom);
                    }

                    ////��ǰ�Ӽ���Uom���²�Bom��Uom��ƥ�䣬��Ҫ����λת��
                    //if (currentBomDetail.Uom.Code.ToUpper() != nextBomDetail.Bom.Uom.Code.ToUpper())
                    //{
                    //    //��λ����
                    //    nextBomDetail.CalculatedQty = uomConversionMgr.ConvertUomQty(currentBomDetail.Item, currentBomDetail.Uom, 1, nextBomDetail.Bom.Uom)
                    //        * currentBomDetail.CalculatedQty * nextBomDetail.RateQty * (1 + nextBomDetail.ScrapPercentage);
                    //}
                    //else
                    //{
                    nextBomDetail.CalculatedQty = nextBomDetail.RateQty * currentBomDetail.CalculatedQty * (1 + nextBomDetail.ScrapPercentage);
                    //}

                    nextBomDetail.OptionalItemGroup = currentBomDetail.OptionalItemGroup;

                    ProcessCurrentBomDetail(flatBomDetailList, nextBomDetail, efftiveDate, allBomDetailList);
                }
            }
            else
            {
                throw new BusinessErrorException("Bom.Error.NotFoundForItem", currentBomDetail.Item);
            }
        }
        #endregion

        #endregion Customized Methods
    }
}



