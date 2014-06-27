
//TODO: Add other using statements here.

using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Distribution;
namespace com.Sconit.Service.MasterData
{
    public interface INumberControlMgr : INumberControlBaseMgr
    {
        #region Customized Methods
        int GenerateNumberNextSequence(string code);

        string GenerateNumber(string code);

        string GenerateNumber(string code, int numberSuffixLength);

        string GenerateRMHuId(FlowDetail flowDetail, string lotNo, decimal qty);

        string GenerateRMHuId(FlowDetail flowDetail, string lotNo, decimal qty, string idMark);

        string GenerateFGHuId(FlowDetail flowDetail, string shiftCode, decimal qty);

        string GenerateFGHuId(FlowDetail flowDetail, string shiftCode, decimal qty, string idMark);

        string GenerateRMHuId(OrderDetail orderDetail, string lotNo, decimal qty);

        string GenerateRMHuId(OrderDetail orderDetail, string lotNo, decimal qty, string idMark);

        string GenerateFGHuId(OrderDetail orderDetail, decimal qty);

        string GenerateFGHuId(OrderDetail orderDetail, decimal qty, string idMark);

        string GenerateRMHuId(InProcessLocationDetail inProcessLocationDetail, string lotNo, decimal qty);

        string GenerateRMHuId(ReceiptDetail receiptDetail, string lotNo, decimal qty);

        string GenerateFGHuId(ReceiptDetail receiptDetail, decimal qty);

        string CloneRMHuId(string huId, decimal qty);

        string CloneFGHuId(string huId);

        void ReverseUpdateHuId(string huId);

        string GetNextSequence(string code);
        #endregion Customized Methods
    }
}
