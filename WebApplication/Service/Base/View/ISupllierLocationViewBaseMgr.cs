using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;

//TODO: Add other using statements here.

namespace com.Sconit.Service.View
{
    public interface ISupllierLocationViewBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateSupllierLocationView(SupllierLocationView entity);

        SupllierLocationView LoadSupllierLocationView(Int32 id);

        IList<SupllierLocationView> GetAllSupllierLocationView();
    
        void UpdateSupllierLocationView(SupllierLocationView entity);

        void DeleteSupllierLocationView(Int32 id);
    
        void DeleteSupllierLocationView(SupllierLocationView entity);
    
        void DeleteSupllierLocationView(IList<Int32> pkList);
    
        void DeleteSupllierLocationView(IList<SupllierLocationView> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
