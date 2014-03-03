using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IMesScmsCompletedIssueMgr
    {

        MesScmsCompletedIssue LoadMesScmsCompletedIssue(string code);
        
        void UpdateMesScmsCompletedIssue(MesScmsCompletedIssue entity);

        IList<MesScmsCompletedIssue> GetUpdateMesScmsCompletedIssue(string wo, string huid);

        void Complete(MesScmsCompletedIssue GetUpdateMesScmsCompletedIssue);

       
    }
}
