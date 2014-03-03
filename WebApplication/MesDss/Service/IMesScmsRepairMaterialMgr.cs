using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Mes.Dss.Entity;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service
{
    public interface IMesScmsRepairMaterialMgr
    {

        MesScmsRepairMaterial LoadMesScmsRepairMaterial(string code);

        void UpdateMesScmsRepairMaterial(MesScmsRepairMaterial entity);


        IList<MesScmsRepairMaterial> GetUpdateMesScmsRepairMaterial();

        void Complete(MesScmsRepairMaterial mesScmsRepairMaterial);
    }
}
