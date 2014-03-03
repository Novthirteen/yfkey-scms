using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MRP;
using com.Sconit.Entity.MRP;
using System.Linq;
using com.Sconit.Utility;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.MRP;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP.Impl
{
    [Transactional]
    public class CustomerScheduleMgr : CustomerScheduleBaseMgr, ICustomerScheduleMgr
    {
        private ICriteriaMgr criteriaMgr;
        private ICustomerScheduleDetailMgr customerScheduleDetailMgr;

        public CustomerScheduleMgr(ICustomerScheduleDao entityDao, ICriteriaMgr criteriaMgr, ICustomerScheduleDetailMgr customerScheduleDetailMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.customerScheduleDetailMgr = customerScheduleDetailMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public IList<CustomerSchedule> GetCustomerSchedules(string flowCode, string referenceScheduleNo, List<string> statusList, DateTime? startDate, DateTime? endDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(CustomerSchedule));
            if (referenceScheduleNo != null && referenceScheduleNo.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("ReferenceScheduleNo", referenceScheduleNo));
            }
            if (flowCode != null && flowCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("Flow", flowCode));
            }
            if (statusList != null && statusList.Count > 0)
            {
                criteria.Add(Expression.In("Status", statusList));
            }
            if (startDate != null)
            {
                criteria.Add(Expression.Ge("CreateDate", startDate));
            }
            if (endDate != null)
            {
                criteria.Add(Expression.Le("CreateDate", endDate.Value.AddDays(1)));
            }
            criteria.AddOrder(Order.Desc("Id"));
            return criteriaMgr.FindAll<CustomerSchedule>(criteria);
        }

        [Transaction(TransactionMode.Requires)]
        public override void CreateCustomerSchedule(CustomerSchedule customerSchedule)
        {
            base.CreateCustomerSchedule(customerSchedule);
            if (customerSchedule.CustomerScheduleDetails != null)
            {
                foreach (CustomerScheduleDetail customerScheduleDetail in customerSchedule.CustomerScheduleDetails)
                {
                    customerScheduleDetail.CustomerSchedule = customerSchedule;
                    customerScheduleDetailMgr.CreateCustomerScheduleDetail(customerScheduleDetail);
                }
            }
        }


        [Transaction(TransactionMode.Requires)]
        public override void DeleteCustomerSchedule(CustomerSchedule customerSchedule)
        {
            if (customerSchedule.CustomerScheduleDetails != null)
            {
                foreach (CustomerScheduleDetail customerScheduleDetail in customerSchedule.CustomerScheduleDetails)
                {
                    customerScheduleDetailMgr.DeleteCustomerScheduleDetail(customerScheduleDetail);
                }
            }
            base.DeleteCustomerSchedule(customerSchedule);
        }

        [Transaction(TransactionMode.Requires)]
        public void CancelCustomerSchedule(int customerScheduleId, string userCode)
        {
            CustomerSchedule customerSchedule = this.LoadCustomerSchedule(customerScheduleId);
            if (customerSchedule.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
            {
                throw new BusinessErrorException("MRP.Schedule.Close.Fail", customerSchedule.Status);
            }
            customerSchedule.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
            customerSchedule.LastModifyDate = DateTime.Now;
            customerSchedule.LastModifyUser = userCode;
            this.UpdateCustomerSchedule(customerSchedule);
        }

        [Transaction(TransactionMode.Requires)]
        public void ReleaseCustomerSchedule(int customerScheduleId, string userCode)
        {
            CustomerSchedule customerSchedule = this.LoadCustomerSchedule(customerScheduleId);
            if (customerSchedule.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                throw new BusinessErrorException("MRP.Schedule.Release.Fail.Detail", customerSchedule.Status);
            }
            customerSchedule.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT;
            customerSchedule.ReleaseDate = DateTime.Now;
            customerSchedule.ReleaseUser = userCode;
            customerSchedule.LastModifyDate = DateTime.Now;
            customerSchedule.LastModifyUser = userCode;
            this.UpdateCustomerSchedule(customerSchedule);
        }

        [Transaction(TransactionMode.Requires)]
        public CustomerSchedule LoadCustomerSchedule(int customerScheduleId, bool includeDetails)
        {
            CustomerSchedule customerSchedule = this.LoadCustomerSchedule(customerScheduleId);

            if (includeDetails && customerSchedule != null && customerSchedule.CustomerScheduleDetails != null
                && customerSchedule.CustomerScheduleDetails.Count > 0)
            {
            }
            return customerSchedule;
        }

        [Transaction(TransactionMode.Requires)]
        public override void UpdateCustomerSchedule(CustomerSchedule customerSchedule)
        {
            customerSchedule.LastModifyDate = DateTime.Now;
            base.UpdateCustomerSchedule(customerSchedule);
            if (customerSchedule.CustomerScheduleDetails != null)
            {
                foreach (CustomerScheduleDetail customerScheduleDetail in customerSchedule.CustomerScheduleDetails)
                {
                    customerScheduleDetailMgr.UpdateCustomerScheduleDetail(customerScheduleDetail);
                }
            }
        }

        #endregion Customized Methods
    }
}
