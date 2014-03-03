using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IProductLineInProcessLocationDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateProductLineInProcessLocationDetail(ProductLineInProcessLocationDetail entity);

        ProductLineInProcessLocationDetail LoadProductLineInProcessLocationDetail(Int32 id);
  
        IList<ProductLineInProcessLocationDetail> GetAllProductLineInProcessLocationDetail();
  
        void UpdateProductLineInProcessLocationDetail(ProductLineInProcessLocationDetail entity);
        
        void DeleteProductLineInProcessLocationDetail(Int32 id);
    
        void DeleteProductLineInProcessLocationDetail(ProductLineInProcessLocationDetail entity);
    
        void DeleteProductLineInProcessLocationDetail(IList<Int32> pkList);
    
        void DeleteProductLineInProcessLocationDetail(IList<ProductLineInProcessLocationDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
