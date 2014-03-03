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
    public class FileUploadBaseMgr : SessionBase, IFileUploadBaseMgr
    {
        protected IFileUploadDao entityDao;
        
        public FileUploadBaseMgr(IFileUploadDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateFileUpload(FileUpload entity)
        {
            entityDao.CreateFileUpload(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual FileUpload LoadFileUpload(Int32 id)
        {
            return entityDao.LoadFileUpload(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateFileUpload(FileUpload entity)
        {
            entityDao.UpdateFileUpload(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFileUpload(Int32 id)
        {
            entityDao.DeleteFileUpload(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFileUpload(FileUpload entity)
        {
            entityDao.DeleteFileUpload(entity);
        }
		
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFileUpload(IList<Int32> pkList)
        {
            entityDao.DeleteFileUpload(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFileUpload(IList<FileUpload> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteFileUpload(entityList);
        }

        #endregion Method Created By CodeSmith
    }
}
