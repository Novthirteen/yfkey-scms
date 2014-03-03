using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IFileUploadBaseDao
    {
        #region Method Created By CodeSmith

        void CreateFileUpload(FileUpload entity);

        FileUpload LoadFileUpload(Int32 id);

        void UpdateFileUpload(FileUpload entity);

        void DeleteFileUpload(Int32 id);
		
        void DeleteFileUpload(FileUpload entity);

        void DeleteFileUpload(IList<Int32> pkList);
		
        void DeleteFileUpload(IList<FileUpload> entityList);

        #endregion Method Created By CodeSmith
    }
}
