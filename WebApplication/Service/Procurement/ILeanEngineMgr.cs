using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Service.Procurement
{
    public interface ILeanEngineMgr
    {
        #region 2G
        //void GenerateOrder();

        //IList<OrderHead> GenerateOrder(OrderHead orderTemplate);

        //IList<OrderHead> GenerateOrder(OrderHead orderTemplate, bool isUrgent);

        //void GetOrderDetailReqQty(OrderDetail orderDetail, bool isUrgent);

        //void UpdateAbnormalNextOrderTime();
        #endregion
        
        //3G
        void OrderGenerate();

        OrderHead PreviewGenOrder(string flowCode);

        void CreateOrder(OrderHead order, string userCode);
    }
}
