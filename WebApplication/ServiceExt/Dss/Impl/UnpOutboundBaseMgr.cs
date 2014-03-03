using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.Criteria;
using Castle.Services.Transaction;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using System.Collections;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.Dss;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;

namespace com.Sconit.Service.Dss.Impl
{
    public abstract class UnpOutboundBaseMgr : AbstractOutboundMgr
    {
        private ICriteriaMgr criteriaMgr;
        private ICommonOutboundMgr commonOutboundMgr;
        private IMiscOrderMgr miscOrderMgr;
        private ILocationMgr locMgr;
        public UnpOutboundBaseMgr(INumberControlMgr numberControlMgr,
           IDssExportHistoryMgr dssExportHistoryMgr,
           ICriteriaMgr criteriaMgr,
           IDssOutboundControlMgr dssOutboundControlMgr,
           IDssObjectMappingMgr dssObjectMappingMgr,
            ICommonOutboundMgr commonOutboundMgr,
            IMiscOrderMgr miscOrderMgr,
            ILocationMgr locationMgr)
            : base(numberControlMgr, dssExportHistoryMgr, criteriaMgr, dssOutboundControlMgr, dssObjectMappingMgr,locationMgr)
        {
            this.criteriaMgr = criteriaMgr;
            this.commonOutboundMgr = commonOutboundMgr;
            this.miscOrderMgr = miscOrderMgr; 
            locMgr = locationMgr;
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override object GetOutboundData(DssExportHistory dssExportHistory)
        {
            return (object)dssExportHistory;
        }

        protected override object Serialize(object obj)
        {
            DssExportHistory dssExportHistory = (DssExportHistory)obj;
            DateTime effDate = dssExportHistory.EffectiveDate.HasValue ? dssExportHistory.EffectiveDate.Value : DateTime.Today;

            string[] line1 = new string[] 
            { 
                DssHelper.GetEventValue(dssExportHistory.EventCode),
                dssExportHistory.Item,//零件号
                dssExportHistory.Qty.ToString("0.########"),//数量
                dssExportHistory.PartyTo,//QAD:Site
                dssExportHistory.Location,//库位
                dssExportHistory.KeyCode,//单号
                DssHelper.FormatDate(effDate,dssExportHistory.DssOutboundControl.ExternalSystem.Code),//生效日期
                dssExportHistory.DefinedString1,//账户
                dssExportHistory.DefinedString2,//分账户
                dssExportHistory.DefinedString3,//成本中心
                dssExportHistory.DefinedString4//项目
            };

            string[][] data = new string[][] { line1 };

            return new object[] { effDate, data };
        }

        #region Protected Methods
        protected IList<DssExportHistory> ConvertList(IList list, DssOutboundControl dssOutboundControl)
        {
            IList<DssExportHistory> result = new List<DssExportHistory>();
            if (list != null && list.Count > 0)
            {
                foreach (object obj in list)
                {
                    DssExportHistory dssExportHistory = commonOutboundMgr.ConvertLocationTransactionToDssExportHistory(obj, dssOutboundControl);

                    dssExportHistory.KeyCode = dssExportHistory.OrderNo;//订单号

                    #region 账户、分账户、成本中心、项目
                    dssExportHistory.DefinedString1 = null;//账户
                    dssExportHistory.DefinedString2 = null;//分账户
                    dssExportHistory.DefinedString3 = null;//成本中心
                    dssExportHistory.DefinedString4 = null;//项目
                    this.GetAccount(dssExportHistory);
                    #endregion

                    result.Add(dssExportHistory);
                }
            }

            return result;
        }
        #endregion

        /// <summary>
        /// ISS-UNP 计划外出库
        /// </summary>
        /// <param name="list"></param>
        /// <param name="dssOutboundControl"></param>
        /// <param name="isFLag">是</param>
        /// <returns></returns>
        protected IList<DssExportHistory> ConvertList(IList list, DssOutboundControl dssOutboundControl,bool isFLag)
        {
            IList<DssExportHistory> result = new List<DssExportHistory>();
            if (list != null && list.Count > 0)
            {
                foreach (object obj in list)
                {
                    DssExportHistory dssExportHistory = commonOutboundMgr.ConvertLocationTransactionToDssExportHistory(obj, dssOutboundControl);
                    //add djin 20120910
                    if ((string)((object[])obj)[4] == "Reject" && ((object[])obj).Length > 9)
                        dssExportHistory.PartyTo = locMgr.LoadLocation((string)((object[])obj)[9]).Region.Code;

                    dssExportHistory.KeyCode = dssExportHistory.OrderNo;//订单号

                    #region 账户、分账户、成本中心、项目
                    dssExportHistory.DefinedString1 = null;//账户
                    dssExportHistory.DefinedString2 = null;//分账户
                    dssExportHistory.DefinedString3 = null;//成本中心
                    dssExportHistory.DefinedString4 = null;//项目
                    this.GetAccount(dssExportHistory);
                    #endregion

                    result.Add(dssExportHistory);
                }
            }

            return result;
        }



        /// <summary>
        /// QAD Dss
        /// </summary>
        /// <param name="dssOutboundControl"></param>
        [Transaction(TransactionMode.Unspecified)]
        private void GetAccount(DssExportHistory dssExportHistory)
        {
            MiscOrder miscOrder = miscOrderMgr.LoadMiscOrder(dssExportHistory.OrderNo);
            if (miscOrder != null)
            {
                dssExportHistory.DefinedString1 = miscOrder.SubjectList.SubjectCode;//账户
                dssExportHistory.DefinedString2 = miscOrder.SubjectList.AccountCode;//分账户
                dssExportHistory.DefinedString3 = miscOrder.SubjectList.CostCenterCode;//成本中心
                dssExportHistory.DefinedString4 = miscOrder.ProjectCode;//项目
            }
        }
    }
}
