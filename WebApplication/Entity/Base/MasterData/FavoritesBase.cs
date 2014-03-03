using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class FavoritesBase : EntityBase
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
		private com.Sconit.Entity.MasterData.User _user;
		public com.Sconit.Entity.MasterData.User User
		{
			get
			{
				return _user;
			}
			set
			{
				_user = value;
			}
		}
		private string _type;
		public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}
		private string _pageName;
		public string PageName
		{
			get
			{
				return _pageName;
			}
			set
			{
				_pageName = value;
			}
		}
		private string _pageUrl;
		public string PageUrl
		{
			get
			{
				return _pageUrl;
			}
			set
			{
				_pageUrl = value;
			}
		}
        private string _pageImage;
        public string PageImage
        {
            get
            {
                return _pageImage;
            }
            set
            {
                _pageImage = value;
            }
        }
        
        #endregion

		public override int GetHashCode()
        {
			if (Id != 0)
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
            FavoritesBase another = obj as FavoritesBase;

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
