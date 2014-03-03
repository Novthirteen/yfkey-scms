using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Mes;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Mes
{
    public interface IProductLineUserBaseDao
    {
        #region Method Created By CodeSmith

        void CreateProductLineUser(ProductLineUser entity);

        ProductLineUser LoadProductLineUser(Int32 id);
  
        IList<ProductLineUser> GetAllProductLineUser();
  
        void UpdateProductLineUser(ProductLineUser entity);
        
        void DeleteProductLineUser(Int32 id);
    
        void DeleteProductLineUser(ProductLineUser entity);
    
        void DeleteProductLineUser(IList<Int32> pkList);
    
        void DeleteProductLineUser(IList<ProductLineUser> entityList);    
        #endregion Method Created By CodeSmith
    }
}
