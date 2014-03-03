using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using System;
using com.Sconit.Entity.Exception;
using NHibernate.Transform;
using System.Linq;
using com.Sconit.Entity;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class ShiftMgr : ShiftBaseMgr, IShiftMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IShiftDetailMgr shiftDetailMgr;
        public ShiftMgr(
            IShiftDao entityDao,
            ICriteriaMgr criteriaMgr,
            IShiftDetailMgr shiftDetailMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.shiftDetailMgr = shiftDetailMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public IList GetShiftWizard(string region, string workcenter)
        {
            IList shifts = new List<Shift>();
            shifts = this.GetShift(region, workcenter);
            if (shifts.Count > 0)
            {
                return shifts;
            }

            workcenter = null;
            shifts = this.GetShift(region, workcenter);
            if (shifts.Count > 0)
            {
                return shifts;
            }

            region = null;
            shifts = this.GetShift(region, workcenter);

            return shifts;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList GetShift(string region, string workcenter)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Shift));
            if (workcenter == null || workcenter.Trim() == "")
            {
                criteria.Add(Expression.IsNull("WorkCenter.Code"));
            }
            else
            {
                criteria.Add(Expression.Eq("WorkCenter.Code", workcenter));
            }
            if (region == null || region.Trim() == "")
            {
                criteria.Add(Expression.IsNull("Region.Code"));
            }
            else
            {
                criteria.Add(Expression.Eq("Region.Code", region));
            }
            criteria.AddOrder(Order.Asc("StartTime"));

            return criteriaMgr.FindAll(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DateTime GetShiftStartTime(DateTime date, string code)
        {
            Shift shift = this.LoadShift(code, date);
            if (shift == null)
                return date.Date;
            else
                return this.GetShiftStartTime(date, shift);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DateTime GetShiftStartTime(DateTime date, Shift shift)
        {
            string[] shiftTime = shift.ShiftTime.Split('-');
            if (shift.DaysAdd == BusinessConstants.CODE_MASTER_DAYS_ADD_TYPE_START_ADD)
            {
                date = date.AddDays(1);
            }
            if (shiftTime.Length == 0)
                return date.Date;
            else
                return this.AssembleActualTime(date, shiftTime[0]);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DateTime GetShiftEndTime(DateTime date, string code)
        {
            Shift shift = this.LoadShift(code, date);
            if (shift == null)
                return date.Date;
            else
                return this.GetShiftEndTime(date, shift);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DateTime GetShiftEndTime(DateTime date, Shift shift)
        {
            string[] shiftTime = shift.ShiftTime.Split('-');

            if (shiftTime.Length == 0)
            {
                return date.Date.AddDays(1);
            }
            else
            {
                if (shift.DaysAdd == BusinessConstants.CODE_MASTER_DAYS_ADD_TYPE_START_ADD || shift.DaysAdd == BusinessConstants.CODE_MASTER_DAYS_ADD_TYPE_END_ADD)
                {
                    date = date.AddDays(1);
                }

                return  this.AssembleActualTime(date, shiftTime[shiftTime.Length - 1]);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public Shift LoadShift(string code, DateTime date)
        {
            Shift shift = this.LoadShift(code);
            if (shift == null)
            {
                throw new BusinessErrorException("MasterData.WorkCalendar.ErrorMessage.ShiftNotExist");
            }
            IList<ShiftDetail> shiftDetailList = shiftDetailMgr.GetShiftDetail(code, date);
            if (shiftDetailList != null && shiftDetailList.Count > 0)
            {
                shift.ShiftTime = shiftDetailList[0].ShiftTime;
                shift.DaysAdd = shiftDetailList[0].DaysAdd;
                return shift;
            }
            else
            {
                return null;
            }
        }

        [Transaction(TransactionMode.Requires)]
        public override void CreateShift(Shift entity)
        {
            base.CreateShift(entity);
            ShiftDetail shiftDetail = new ShiftDetail();
            shiftDetail.Shift = entity;
            shiftDetail.ShiftTime = entity.ShiftTime;
            if (entity.StartDate != null && entity.StartDate.Trim() != string.Empty)
            {
                shiftDetail.StartDate = DateTime.Parse(entity.StartDate);
            }
            if (entity.EndDate != null && entity.EndDate.Trim() != string.Empty)
            {
                shiftDetail.EndDate = DateTime.Parse(entity.EndDate);
            }
            shiftDetailMgr.CreateShiftDetail(shiftDetail);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Shift> GetRegionShift(string region)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(WorkdayShift));
            criteria.CreateAlias("Workday", "w");
            if (region != null && region.Trim() != string.Empty)
            {
                criteria.Add(Expression.Or(Expression.IsNull("w.Region"), Expression.Eq("w.Region.Code", region)));
            }

            IList<WorkdayShift> workdayShifts = criteriaMgr.FindAll<WorkdayShift>(criteria);
            var query =
                from workdayShift in workdayShifts
                select workdayShift.Shift;

            IList<Shift> shifts = new List<Shift>(query.Distinct());
            return shifts;
        }
        #endregion Customized Methods

        #region Private Method

        private DateTime AssembleActualTime(DateTime date, string time)
        {
            DateTime actualTime = date;
            try
            {
                actualTime = Convert.ToDateTime(date.ToString("yyyy-MM-dd") + " " + time);
            }
            catch (Exception)
            { }

            return actualTime;
        }

        #endregion
    }
}