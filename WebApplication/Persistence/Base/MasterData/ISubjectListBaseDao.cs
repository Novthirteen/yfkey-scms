using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface ISubjectListBaseDao
    {
        #region Method Created By CodeSmith

        void CreateSubjectList(SubjectList entity);

        SubjectList LoadSubjectList(Int32 id);
  
        IList<SubjectList> GetAllSubjectList();
  
        void UpdateSubjectList(SubjectList entity);
        
        void DeleteSubjectList(Int32 id);
    
        void DeleteSubjectList(SubjectList entity);
    
        void DeleteSubjectList(IList<Int32> pkList);
    
        void DeleteSubjectList(IList<SubjectList> entityList);    
        #endregion Method Created By CodeSmith
    }
}
