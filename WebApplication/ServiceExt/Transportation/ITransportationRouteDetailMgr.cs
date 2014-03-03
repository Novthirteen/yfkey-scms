using System.Collections.Generic;
using com.Sconit.Entity.Transportation;

namespace com.Sconit.Service.Transportation
{
    public interface ITransportationRouteDetailMgr : ITransportationRouteDetailBaseMgr
    {
        #region Customized Methods

        IList<TransportationRouteDetail> GetAllTransportationRouteDetail(string transportationRouteCode);

        #endregion Customized Methods
    }
}
