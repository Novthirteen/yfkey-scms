using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class SubjectListBaseMgr : SessionBase, ISubjectListBaseMgr
    {
        protected ISubjectListDao entityDao;
        
        public SubjectListBaseMgr(ISubjectListDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateSubjectList(SubjectList entity)
        {
            entityDao.CreateSubjectList(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual SubjectList LoadSubjectList(Int32 id)
        {
            return entityDao.LoadSubjectList(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<SubjectList> GetAllSubjectList()
        {
            return entityDao.GetAllSubjectList();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateSubjectList(SubjectList entity)
        {
            entityDao.UpdateSubjectList(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSubjectList(Int32 id)
        {
            entityDao.DeleteSubjectList(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSubjectList(SubjectList entity)
        {
            entityDao.DeleteSubjectList(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSubjectList(IList<Int32> pkList)
        {
            entityDao.DeleteSubjectList(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteSubjectList(IList<SubjectList> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteSubjectList(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
