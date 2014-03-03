using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class FileUploadMgr : FileUploadBaseMgr, IFileUploadMgr
    {
        public FileUploadMgr(IFileUploadDao entityDao)
            : base(entityDao)
        {
        }

        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}