using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MRP;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP
{
    public interface IMrpReceivePlanBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateMrpReceivePlan(MrpReceivePlan entity);

        MrpReceivePlan LoadMrpReceivePlan(Int32 id);

        IList<MrpReceivePlan> GetAllMrpReceivePlan();
    
        void UpdateMrpReceivePlan(MrpReceivePlan entity);

        void DeleteMrpReceivePlan(Int32 id);
    
        void DeleteMrpReceivePlan(MrpReceivePlan entity);
    
        void DeleteMrpReceivePlan(IList<Int32> pkList);
    
        void DeleteMrpReceivePlan(IList<MrpReceivePlan> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
