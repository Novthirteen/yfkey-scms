using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class FlatBomBase : EntityBase
    {
        #region O/R Mapping Properties
		
		public Int32 Id { get; set; }
		public string Bom { get; set; }
		public string Fg { get; set; }
		public string Item { get; set; }
		public Int32 BomLevel { get; set; }
        //上一级的数量
		public Double Qty { get; set; }
        //上一级的数量乘以耗用
		public Double RateQty { get; set; }
		public DateTime CreateDate { get; set; }
		public string CreateUser { get; set; }

        public bool IsLastLevel { get; set; }
        #endregion

		public override int GetHashCode()
        {
			if (Id != null)
            {
                return Id.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            FlatBomBase another = obj as FlatBomBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id);
            }
        } 
    }
	
}
