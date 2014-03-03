using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Dss
{
    [Serializable]
    public abstract class DssFtpControlBase : EntityBase
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
		private string _ftpServer;
		public string FtpServer
		{
			get
			{
				return _ftpServer;
			}
			set
			{
				_ftpServer = value;
			}
		}
		private Int32? _ftpPort;
		public Int32? FtpPort
		{
			get
			{
				return _ftpPort;
			}
			set
			{
				_ftpPort = value;
			}
		}
		private string _ftpUser;
		public string FtpUser
		{
			get
			{
				return _ftpUser;
			}
			set
			{
				_ftpUser = value;
			}
		}
		private string _ftpPassword;
		public string FtpPassword
		{
			get
			{
				return _ftpPassword;
			}
			set
			{
				_ftpPassword = value;
			}
		}
		private string _ftpTempFolder;
		public string FtpTempFolder
		{
			get
			{
				return _ftpTempFolder;
			}
			set
			{
				_ftpTempFolder = value;
			}
		}
		private string _ftpFolder;
		public string FtpFolder
		{
			get
			{
				return _ftpFolder;
			}
			set
			{
				_ftpFolder = value;
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
		private string _localTempFolder;
		public string LocalTempFolder
		{
			get
			{
				return _localTempFolder;
			}
			set
			{
				_localTempFolder = value;
			}
		}
		private string _localFolder;
		public string LocalFolder
		{
			get
			{
				return _localFolder;
			}
			set
			{
				_localFolder = value;
			}
		}
		private string _iOType;
		public string IOType
		{
			get
			{
				return _iOType;
			}
			set
			{
				_iOType = value;
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
            DssFtpControlBase another = obj as DssFtpControlBase;

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
