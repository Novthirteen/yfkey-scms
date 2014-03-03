using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity;

//TODO: Add other using statements here

namespace com.Mes.Dss.Entity
{
    [Serializable]
    public class ScmsTableIndex : EntityBase
    {

        public string TableName { get; set; }
        public Int32 Flag { get; set; }
        public DateTime LastModifyDate { get; set; }
       

        public override int GetHashCode()
        {
            if (TableName != null)
            {
                return TableName.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            ScmsTableIndex another = obj as ScmsTableIndex;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.TableName == another.TableName);
            }
        }
    }

}
