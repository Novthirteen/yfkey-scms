using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Dss
{
    [Serializable]
    public abstract class DssInboundControlBase : EntityBase
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
		private string _inFloder;
        public string InFloder
		{
			get
			{
                return _inFloder;
			}
			set
			{
                _inFloder = value;
			}
		}
		private string _filePattern;
		public string FilePattern
		{
			get
			{
				return _filePattern;
			}
			set
			{
				_filePattern = value;
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
		private string _archiveFloder;
		public string ArchiveFloder
		{
			get
			{
				return _archiveFloder;
			}
			set
			{
				_archiveFloder = value;
			}
		}
		private string _errorFloder;
		public string ErrorFloder
		{
			get
			{
				return _errorFloder;
			}
			set
			{
				_errorFloder = value;
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
            DssInboundControlBase another = obj as DssInboundControlBase;

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
