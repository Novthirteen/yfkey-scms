using System;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Utility
{
    public static class FlowHelper
    {
        public static string GetFlowServiceMethod(string type)
        {
            string serviceMethod = string.Empty;
            if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT)
            {
                serviceMethod = "GetProcurementFlow";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
            {
                serviceMethod = "GetDistributionFlow";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_INSPECTION)
            {
                serviceMethod = "GetInspectionFlow";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
            {
                serviceMethod = "GetProductionFlow";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
            {
                serviceMethod = "GetTransferFlow";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS)
            {
                serviceMethod = "GetCustomerGoodsFlow";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
            {
                serviceMethod = "GetSubconctractingFlow";
            }
            return serviceMethod;
        }

        public static string GetFlowPartyFromLabel(string type)
        {
            string partyFromLabel = string.Empty;
            if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT)
            {
                partyFromLabel = "${MasterData.Flow.Party.From.Supplier}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS)
            {
                partyFromLabel = "${MasterData.Flow.Party.From.CustomerGoods}";
            }
            else
            {
                partyFromLabel = "${MasterData.Flow.Party.From.Region}";
            }
            return partyFromLabel;
        }

        public static string GetFlowPartyToLabel(string type)
        {
            string partyToLabel = string.Empty;
            if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
            {
                partyToLabel = "${MasterData.Flow.Party.To.Customer}";
            }
            else
            {
                partyToLabel = "${MasterData.Flow.Party.To.Region}";
            }
            return partyToLabel;
        }
        public static string GetFlowLabel(string type)
        {
            string flowLabel = string.Empty;
            if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
            {
                flowLabel = "${MasterData.Flow.Flow.Distribution}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT)
            {
                flowLabel = "${MasterData.Flow.Flow.Procurement}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
            {
                flowLabel = "${MasterData.Flow.Flow.Production}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_INSPECTION)
            {
                flowLabel = "${MasterData.Flow.Flow.Inspection}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
            {
                flowLabel = "${MasterData.Flow.Flow.Transfer}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS)
            {
                flowLabel = "${MasterData.Flow.Flow.CustomerGoods}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
            {
                flowLabel = "${MasterData.Flow.Flow.Subconctracting}";
            }
            //else if (type == BusinessConstants.ORDER_MODULETYPE_VALUE_SUPPLIERDISTRIBUTION)
            //{
            //    flowLabel = "${MasterData.Flow.Flow.SupplierDistribution}";
            //}
            //else if (type == BusinessConstants.ORDER_MODULETYPE_VALUE_PROCUREMENTCONFIRM)
            //{
            //    flowLabel = "${Common.Business.Flow}";
            //}
            return flowLabel;
        }

        public static string GetFlowStrategyLabel(string type)
        {
            string strategyLabel = string.Empty;
            if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
            {
                strategyLabel = "${MasterData.Flow.Strategy.Distribution}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT)
            {
                strategyLabel = "${MasterData.Flow.Strategy.Procurement}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
            {
                strategyLabel = "${MasterData.Flow.Strategy.Production}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_INSPECTION)
            {
                strategyLabel = "${MasterData.Flow.Strategy.Inspection}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
            {
                strategyLabel = "${MasterData.Flow.Strategy.Transfer}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS)
            {
                strategyLabel = "${MasterData.Flow.Strategy.CustomerGoods}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
            {
                strategyLabel = "${MasterData.Flow.Strategy.Subconctracting}";
            }
            return strategyLabel;
        }

        public static string GetFlowDetailLabel(string type)
        {
            string flowLabel = string.Empty;
            if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
            {
                flowLabel = "${MasterData.Flow.Detail.Distribution}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT)
            {
                flowLabel = "${MasterData.Flow.Detail.Procurement}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
            {
                flowLabel = "${MasterData.Flow.Detail.Production}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_INSPECTION)
            {
                flowLabel = "${MasterData.Flow.Detail.Inspection}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
            {
                flowLabel = "${MasterData.Flow.Detail.Transfer}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS)
            {
                flowLabel = "${MasterData.Flow.Detail.CustomerGoods}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
            {
                flowLabel = "${MasterData.Flow.Detail.Subconctracting}";
            }
            return flowLabel;
        }

        public static string GetFlowRoutingLabel(string type)
        {
            string flowLabel = string.Empty;
            if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
            {
                flowLabel = "${MasterData.Flow.Routing.Distribution}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT)
            {
                flowLabel = "${MasterData.Flow.Routing.Procurement}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
            {
                flowLabel = "${MasterData.Flow.Routing.Production}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_INSPECTION)
            {
                flowLabel = "${MasterData.Flow.Routing.Inspection}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
            {
                flowLabel = "${MasterData.Flow.Routing.Transfer}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS)
            {
                flowLabel = "${MasterData.Flow.Routing.CustomerGoods}";
            }
            else if (type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
            {
                flowLabel = "${MasterData.Flow.Routing.Subconctracting}";
            }
            return flowLabel;
        }

        public static bool CheckDetailSeqExists(List<FlowDetail> flowDetailList, List<FlowDetail> refFlowDetailList)
        {
            bool isValid = true;
            foreach (FlowDetail refFlowDetail in refFlowDetailList)
            {
                foreach (FlowDetail flowDetail in flowDetailList)
                {
                    if (refFlowDetail.Sequence == flowDetail.Sequence)
                    {
                        isValid = false;
                        return isValid;
                    }
                }
            }
            return isValid;
        }

        public static bool CheckDetailItemExists(List<string[]> itemList, string[] item)
        {
            bool isValid = true;
            foreach (string[] listItem in itemList)
            {
                for (int i = 0; i < listItem.Length; i++)
                {
                    if (listItem[i] == item[i])
                    {
                        if (i == listItem.Length - 1)
                        {
                            isValid = false;
                            return isValid;
                        }
                    }
                    else break;
                }
            }

            return isValid;
        }

        public static bool IsFlowEqual(Flow flow1, Flow flow2)
        {
            if (flow1 == null && flow2 == null)
            {
                return true;
            }

            if (flow1 == null && flow2 != null)
            {
                return false;
            }

            if (flow1 != null && flow2 == null)
            {
                return false;
            }

            if (flow1.Code == flow2.Code)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        public static int GetMaxFlowSeq(Flow flow)
        {
            int maxSeq = 0;
            if (flow.FlowDetails != null && flow.FlowDetails.Count > 0)
            {
                foreach (FlowDetail flowDetail in flow.FlowDetails)
                {
                    if (flowDetail.Sequence > maxSeq)
                    {
                        maxSeq = flowDetail.Sequence;
                    }
                }
            }

            return maxSeq;
        }

        public static DateTime GetWinTime(Flow flow, DateTime dateTime)
        {
            List<string> winTimes = new List<string>();

            DayOfWeek day = dateTime.DayOfWeek;
            if (day == DayOfWeek.Monday && flow.WinTime1 != null && flow.WinTime1.Trim() != string.Empty)
            {
                winTimes.AddRange(flow.WinTime1.Split('|'));
            }
            else if (day == DayOfWeek.Tuesday && flow.WinTime2 != null && flow.WinTime2.Trim() != string.Empty)
            {
                winTimes.AddRange(flow.WinTime2.Split('|'));
            }
            else if (day == DayOfWeek.Wednesday && flow.WinTime3 != null && flow.WinTime3.Trim() != string.Empty)
            {
                winTimes.AddRange(flow.WinTime3.Split('|'));
            }
            else if (day == DayOfWeek.Thursday && flow.WinTime4 != null && flow.WinTime4.Trim() != string.Empty)
            {
                winTimes.AddRange(flow.WinTime4.Split('|'));
            }
            else if (day == DayOfWeek.Friday && flow.WinTime5 != null && flow.WinTime5.Trim() != string.Empty)
            {
                winTimes.AddRange(flow.WinTime5.Split('|'));
            }
            else if (day == DayOfWeek.Saturday && flow.WinTime6 != null && flow.WinTime6.Trim() != string.Empty)
            {
                winTimes.AddRange(flow.WinTime6.Split('|'));
            }
            else if (day == DayOfWeek.Sunday && flow.WinTime7 != null && flow.WinTime7.Trim() != string.Empty)
            {
                winTimes.AddRange(flow.WinTime7.Split('|'));
            }

            if (winTimes.Count > 0)
            {
                string dayNow = dateTime.ToShortDateString();

                foreach (string winTime in winTimes)
                {
                    DateTime nearestDateTime = DateTime.Parse(dayNow + " " + winTime);
                    if (DateTime.Compare(DateTime.Now, nearestDateTime) < 0)
                    {
                        return nearestDateTime;
                    }
                }
            }

            if ((dateTime - DateTime.Now).Days < 8)
            {
                return GetWinTime(flow, dateTime.AddDays(1));
            }

            return dateTime;
        }
    }
}
