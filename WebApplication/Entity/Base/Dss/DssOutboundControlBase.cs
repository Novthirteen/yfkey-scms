using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Dss
{
    [Serializable]
    public abstract class DssOutboundControlBase : EntityBase
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
        private com.Sconit.Entity.Dss.DssSystem _externalSystem;
        public com.Sconit.Entity.Dss.DssSystem ExternalSystem
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
        private string _externalObjectCode;
        public string ExternalObjectCode
        {
            get
            {
                return _externalObjectCode;
            }
            set
            {
                _externalObjectCode = value;
            }
        }
        private string _outFolder;
        public string OutFolder
        {
            get
            {
                return _outFolder;
            }
            set
            {
                _outFolder = value;
            }
        }
        private string _serviceName;
        public string ServiceName
        {
            get
            {
                return _serviceName;
            }
            set
            {
                _serviceName = value;
            }
        }
        private string _archiveFolder;
        public string ArchiveFolder
        {
            get
            {
                return _archiveFolder;
            }
            set
            {
                _archiveFolder = value;
            }
        }
        private Int32 _sequence;
        public Int32 Sequence
        {
            get
            {
                return _sequence;
            }
            set
            {
                _sequence = value;
            }
        }
        private string _tempFolder;
        public string TempFolder
        {
            get
            {
                return _tempFolder;
            }
            set
            {
                _tempFolder = value;
            }
        }
        private string _fileEncoding;
        public string FileEncoding
        {
            get
            {
                return _fileEncoding;
            }
            set
            {
                _fileEncoding = value;
            }
        }
        private string _sysAlias;
        public string SysAlias
        {
            get
            {
                return _sysAlias;
            }
            set
            {
                _sysAlias = value;
            }
        }
        private string _filePrefix;
        public string FilePrefix
        {
            get
            {
                return _filePrefix;
            }
            set
            {
                _filePrefix = value;
            }
        }
        private string _fileSuffix;
        public string FileSuffix
        {
            get
            {
                return _fileSuffix;
            }
            set
            {
                _fileSuffix = value;
            }
        }
        private Boolean _isActive;
        public Boolean IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }
        public Int32 Mark { get; set; }
        private string _undefinedString1;
        public string UndefinedString1
        {
            get
            {
                return _undefinedString1;
            }
            set
            {
                _undefinedString1 = value;
            }
        }
        private string _undefinedString2;
        public string UndefinedString2
        {
            get
            {
                return _undefinedString2;
            }
            set
            {
                _undefinedString2 = value;
            }
        }
        private string _undefinedString3;
        public string UndefinedString3
        {
            get
            {
                return _undefinedString3;
            }
            set
            {
                _undefinedString3 = value;
            }
        }
        private string _undefinedString4;
        public string UndefinedString4
        {
            get
            {
                return _undefinedString4;
            }
            set
            {
                _undefinedString4 = value;
            }
        }
        private string _undefinedString5;
        public string UndefinedString5
        {
            get
            {
                return _undefinedString5;
            }
            set
            {
                _undefinedString5 = value;
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
            DssOutboundControlBase another = obj as DssOutboundControlBase;

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
