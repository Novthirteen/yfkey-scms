using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity;

//TODO: Add other using statements here

namespace com.Mes.Dss.Entity
{
    [Serializable]
    public class ScmsPart : EntityBase
    {
        public string Code { get; set; }
        public Int32 Flag { get; set; }
        public string Des { get; set; }
        public string Uom { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string LastModifyUser { get; set; }

        public override int GetHashCode()
        {
            if (Code != null)
            {
                return Code.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            ScmsPart another = obj as ScmsPart;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.Code == another.Code);
            }
        }
    }

}
