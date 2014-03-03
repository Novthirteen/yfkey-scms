using System;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface ISubjectListMgr : ISubjectListBaseMgr
    {
        #region Customized Methods

        IList<SubjectList> GetSubjectList(string subjectCode);

        IList<SubjectList> GetAllSubject();

        SubjectList LoadSubjectList(string subjectCode, string costCenterCode, string accountCode);

        IList<SubjectList> GetCostCenter(string subjectCode);

        IList<SubjectList> GetAccount(string subjectCode, string costCenterCode);

        #endregion Customized Methods
    }
}
