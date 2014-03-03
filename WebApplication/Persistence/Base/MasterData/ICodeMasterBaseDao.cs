using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface ICodeMasterBaseDao
    {
        #region Method Created By CodeSmith

        void CreateCodeMaster(CodeMaster entity);

        CodeMaster LoadCodeMaster(String code, String value);

        void UpdateCodeMaster(CodeMaster entity);
        
        void DeleteCodeMaster(String code, String value);
		
        void DeleteCodeMaster(CodeMaster entity);
		
        void DeleteCodeMaster(IList<CodeMaster> entityList);

        #endregion Method Created By CodeSmith
    }
}
