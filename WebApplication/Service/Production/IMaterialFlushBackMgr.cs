using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Production;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.Dss;

namespace com.Sconit.Service.Production
{
    public interface IMaterialFlushBackMgr
    {
        #region Customized Methods

        //IList<MaterialFlushBack> FindMatchMaterialFlushBack(OrderLocationTransaction orderLocationTransaction, string ipNo);

        //IList<MaterialFlushBack> FindMatchMaterialFlushBack(OrderLocationTransaction orderLocationTransaction, IList<string> ipNoList);

        //IList<MaterialFlushBack> FindMatchMaterialFlushBack(OrderLocationTransaction orderLocationTransaction, IList<MaterialFlushBack> materialFlushBackList);

        IList<MaterialFlushBack> AssignMaterialFlushBack(MaterialFlushBack sourceMaterialFlushBack, IList<OrderDetail> orderDetailList);

        IList<MaterialFlushBack> AssignMaterialFlushBack(MaterialFlushBack sourceMaterialFlushBack, IList<OrderLocationTransaction> inOrderLocationTransaction);

        void ImportProdItemHuId(IList<DssImportHistory> dssImportHistoryList);

        #endregion Customized Methods
    }
}
