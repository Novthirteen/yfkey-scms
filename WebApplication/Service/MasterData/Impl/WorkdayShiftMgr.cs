using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Utility;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class WorkdayShiftMgr : WorkdayShiftBaseMgr, IWorkdayShiftMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IShiftMgr ShiftMgr;
        public WorkdayShiftMgr(IWorkdayShiftDao entityDao, ICriteriaMgr criteriaMgr, IShiftMgr ShiftMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.ShiftMgr = ShiftMgr;
        }

        #region Customized Methods

        public IList<Shift> GetShiftsNotInWorkday(int Id)
        {
            IList<Shift> allShifts = ShiftMgr.GetAllShift();
            IList<Shift> workdayShifts = GetShiftsByWorkdayId(Id);
            List<Shift> otherShifts = new List<Shift>();
            if (allShifts != null && allShifts.Count > 0)
            {
                foreach (Shift s in allShifts)
                {
                    if (!workdayShifts.Contains(s))
                    {
                        otherShifts.Add(s);
                    }
                }
            }
            return otherShifts;
        }

        public IList<Shift> GetShiftsByWorkdayId(int Id)
        {
            List<Shift> sList = new List<Shift>();

            DetachedCriteria criteria = DetachedCriteria.For(typeof(WorkdayShift));
            criteria.Add(Expression.Eq("Workday.Id", Id));
            IList<WorkdayShift> wsList = criteriaMgr.FindAll<WorkdayShift>(criteria);
            foreach (WorkdayShift ws in wsList)
            {
                sList.Add(ws.Shift);
            }
            return sList;
        }

        public WorkdayShift LoadWorkdayShift(int workdayId, string shiftCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(WorkdayShift));
            criteria.Add(Expression.Eq("Workday.Id", workdayId));
            criteria.Add(Expression.Eq("Shift.Code", shiftCode));
            IList<WorkdayShift> wsList = criteriaMgr.FindAll<WorkdayShift>(criteria);
            if (wsList.Count == 0) return null;
            return wsList[0];
        }

        public void CreateWorkdayShifts(Workday workday, IList<Shift> sList)
        {
            foreach (Shift shift in sList)
            {
                WorkdayShift workdayShift = new WorkdayShift();
                workdayShift.Workday = workday;
                workdayShift.Shift = shift;
                entityDao.CreateWorkdayShift(workdayShift);
            }
        }

        public IList<WorkdayShift> GetWorkdayShiftsByWorkdayId(int workdayId)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(WorkdayShift));
            criteria.Add(Expression.Eq("Workday.Id", workdayId));
            IList workdayShifts = criteriaMgr.FindAll(criteria);

            return IListHelper.ConvertToList<WorkdayShift>(workdayShifts);
        }

        #endregion Customized Methods
    }
}