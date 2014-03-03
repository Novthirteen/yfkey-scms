using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Dss
{
    [Serializable]
    public abstract class DssObjectMappingBase : EntityBase
    {
        #region O/R Mapping Properties

        private Int32 _id;
        public Int32 Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private string _entity;
        public string Entity
        {
            get
            {
                return _entity;
            }
            set
            {
                _entity = value;
            }
        }
        private string _code;
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }
        private DssSystem _externalSystem;
        public DssSystem ExternalSystem
        {
            get
            {
                return _externalSystem;
            }
            set
            {
                _externalSystem = value;
            }
        }
        private string _externalEntity;
        public string ExternalEntity
        {
            get
            {
                return _externalEntity;
            }
            set
            {
                _externalEntity = value;
            }
        }
        private string _externalCode;
        public string ExternalCode
        {
            get
            {
                return _externalCode;
            }
            set
            {
                _externalCode = value;
            }
        }

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
            DssObjectMappingBase another = obj as DssObjectMappingBase;

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
