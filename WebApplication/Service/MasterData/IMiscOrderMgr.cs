using com.Sconit.Entity.MasterData;

namespace com.Sconit.Service.MasterData
{
    public interface IMiscOrderMgr : IMiscOrderBaseMgr
    {
        #region Customized Methods
        MiscOrder SaveMiscOrder(MiscOrder miscOrder, User user);
        void RemoveMiscOrder(MiscOrder miscOrder);
        MiscOrder ReLoadMiscOrder(string orderNo);
        #endregion Customized Methods
    }
}
