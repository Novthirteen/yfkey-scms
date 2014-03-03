using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class SubjectListMgr : SubjectListBaseMgr, ISubjectListMgr
    {
        private ICriteriaMgr criteriaMgr;
        public SubjectListMgr(ISubjectListDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        public IList<SubjectList> GetSubjectList(string subjectCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<SubjectList>();
            if (subjectCode != string.Empty && subjectCode != null)
            {
                criteria.Add(Expression.Eq("SubjectCode", subjectCode));
            }
            else
            {
                return null;
            }
          
            return criteriaMgr.FindAll<SubjectList>(criteria);
        }

        public SubjectList LoadSubjectList(string subjectCode,string costCenterCode,string accountCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<SubjectList>();
            if (subjectCode != string.Empty && subjectCode != null)
            {
                criteria.Add(Expression.Eq("SubjectCode", subjectCode));
            }
            if (costCenterCode != string.Empty && costCenterCode != null)
            {
                criteria.Add(Expression.Eq("CostCenterCode", costCenterCode));
            }
            if (accountCode != string.Empty && accountCode != null)
            {
                criteria.Add(Expression.Eq("AccountCode", accountCode));
            }
            
            IList<SubjectList> subjectList = criteriaMgr.FindAll<SubjectList>(criteria);
            if (subjectList != null && subjectList.Count > 0)
            {
                return subjectList[0];
            }
            else
            {
                return null;
            }
        }

        public IList<SubjectList> GetAllSubject()
        {
            DetachedCriteria criteria = DetachedCriteria.For<SubjectList>();
            ProjectionList list = Projections.ProjectionList().Create();
            list.Add(Projections.Distinct(Projections.Property("SubjectCode")));
            list.Add(Projections.GroupProperty("SubjectCode"), "SubjectCode");
            list.Add(Projections.GroupProperty("SubjectName"), "SubjectName");
            criteria.SetProjection(list);
            criteria.SetResultTransformer(new NHibernate.Transform.AliasToBeanResultTransformer(typeof(SubjectList)));

            return criteriaMgr.FindAll<SubjectList>(criteria);
        }

        public IList<SubjectList> GetCostCenter(string subjectCode)
        {
            if (subjectCode == null || subjectCode == string.Empty)
            {
                return null;
            }
            else
            {
                DetachedCriteria criteria = DetachedCriteria.For<SubjectList>();
                criteria.Add(Expression.Eq("SubjectCode", subjectCode));
                ProjectionList list = Projections.ProjectionList().Create();
                list.Add(Projections.Distinct(Projections.Property("CostCenterCode")));
                list.Add(Projections.GroupProperty("CostCenterCode"), "CostCenterCode");
                list.Add(Projections.GroupProperty("CostCenterName"), "CostCenterName");
                criteria.SetProjection(list);
                criteria.SetResultTransformer(new NHibernate.Transform.AliasToBeanResultTransformer(typeof(SubjectList)));

                return criteriaMgr.FindAll<SubjectList>(criteria);
            }
        }

        public IList<SubjectList> GetAccount(string subjectCode,string costCenterCode)
        {

            if (subjectCode == null || subjectCode == string.Empty || costCenterCode == null || costCenterCode == string.Empty)
            {
                return null;
            }
            else
            {
                DetachedCriteria criteria = DetachedCriteria.For<SubjectList>();
                criteria.Add(Expression.Eq("SubjectCode", subjectCode));
                criteria.Add(Expression.Eq("CostCenterCode", costCenterCode));
                ProjectionList list = Projections.ProjectionList().Create();
                list.Add(Projections.Distinct(Projections.Property("AccountCode")));
                list.Add(Projections.GroupProperty("AccountCode"), "AccountCode");
                criteria.SetProjection(list);
                criteria.SetResultTransformer(new NHibernate.Transform.AliasToBeanResultTransformer(typeof(SubjectList)));

                return criteriaMgr.FindAll<SubjectList>(criteria);
            }
        }

        #endregion Customized Methods
    }
}