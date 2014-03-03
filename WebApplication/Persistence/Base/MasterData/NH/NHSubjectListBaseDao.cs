using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statmens here.

namespace com.Sconit.Persistence.MasterData.NH
{
    public class NHSubjectListBaseDao : NHDaoBase, ISubjectListBaseDao
    {
        public NHSubjectListBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateSubjectList(SubjectList entity)
        {
            Create(entity);
        }

        public virtual IList<SubjectList> GetAllSubjectList()
        {
            return FindAll<SubjectList>();
        }

        public virtual SubjectList LoadSubjectList(Int32 id)
        {
            return FindById<SubjectList>(id);
        }

        public virtual void UpdateSubjectList(SubjectList entity)
        {
            Update(entity);
        }

        public virtual void DeleteSubjectList(Int32 id)
        {
            string hql = @"from SubjectList entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteSubjectList(SubjectList entity)
        {
            Delete(entity);
        }

        public virtual void DeleteSubjectList(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from SubjectList entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteSubjectList(IList<SubjectList> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (SubjectList entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteSubjectList(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
