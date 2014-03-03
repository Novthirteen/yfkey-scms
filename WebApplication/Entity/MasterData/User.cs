using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class User : UserBase
    {
        #region Non O/R Mapping Properties
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string UserThemePage
        {
            get
            {
                if (this.UserPreferences != null && this.UserPreferences.Count > 0)
                {
                    foreach (UserPreference up in this.UserPreferences)
                    {
                        if (up.Code == BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE)
                            return up.Value;
                    }
                }
                return null;
            }
            set
            {
                if (this.UserPreferences != null && this.UserPreferences.Count > 0)
                {
                    foreach (UserPreference up in this.UserPreferences)
                    {
                        if (up.Code == BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE)
                        {
                            up.Value = value;
                            return;
                        }
                    }
                }

                UserPreference userPreference = new UserPreference();
                userPreference.Code = BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEPAGE;
                userPreference.Value = value;
                this.UserPreferences.Add(userPreference);
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string UserThemeFrame
        {
            get
            {
                if (this.UserPreferences != null && this.UserPreferences.Count > 0)
                {
                    foreach (UserPreference up in this.UserPreferences)
                    {
                        if (up.Code == BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEFRAME)
                            return up.Value;
                    }
                }
                return null;
            }
            set
            {
                if (this.UserPreferences != null && this.UserPreferences.Count > 0)
                {
                    foreach (UserPreference up in this.UserPreferences)
                    {
                        if (up.Code == BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEFRAME)
                        {
                            up.Value = value;
                            return;
                        }
                    }
                }

                UserPreference userPreference = new UserPreference();
                userPreference.Code = BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_THEMEFRAME;
                userPreference.Value = value;
                this.UserPreferences.Add(userPreference);
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string UserLanguage
        {
            get
            {
                if (this.UserPreferences != null && this.UserPreferences.Count > 0)
                {
                    foreach (UserPreference up in this.UserPreferences)
                    {
                        if (up.Code == BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_LANGUAGE)
                            return up.Value;
                    }
                }

                return null;
            }
            set
            {
                if (this.UserPreferences != null && this.UserPreferences.Count > 0)
                {
                    foreach (UserPreference up in this.UserPreferences)
                    {
                        if (up.Code == BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_LANGUAGE)
                        {
                            up.Value = value;
                            return;
                        }
                    }
                }

                UserPreference userPreference = new UserPreference();
                userPreference.Code = BusinessConstants.CODE_MASTER_USER_PREFERENCE_VALUE_LANGUAGE;
                userPreference.Value = value;
                this.UserPreferences.Add(userPreference);
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string UserCSModule
        {
            get
            {
                if (this.UserPreferences != null && this.UserPreferences.Count > 0)
                {
                    foreach (UserPreference up in this.UserPreferences)
                    {
                        if (up.Code == BusinessConstants.PERMISSION_CATEGORY_TERMINAL)
                            return up.Value;
                    }
                }

                return null;
            }
            set
            {
                if (this.UserPreferences != null && this.UserPreferences.Count > 0)
                {
                    foreach (UserPreference up in this.UserPreferences)
                    {
                        if (up.Code == BusinessConstants.PERMISSION_CATEGORY_TERMINAL)
                        {
                            up.Value = value;
                            return;
                        }
                    }
                }

                UserPreference userPreference = new UserPreference();
                userPreference.Code = BusinessConstants.PERMISSION_CATEGORY_TERMINAL;
                userPreference.Value = value;
                this.UserPreferences.Add(userPreference);
            }
        }


        private IList<Permission> _permissions;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<Permission> Permissions
        {
            get
            {
                return this._permissions;
            }
            set
            {
                this._permissions = value;
            }
        }

        //public Permission[] PermissionArray
        //{
        //    get
        //    {
        //        if (this._permissions == null || this._permissions.Count == 0)
        //        {
        //            return null;
        //        }
        //        Permission[] _permissionArray = new Permission[this._permissions.Count];

        //        for (int i = 0; i < this._permissions.Count; i++)
        //        {
        //            _permissionArray[i] = this._permissions[i];
        //        }

        //         return _permissionArray;
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
        
        public IList<Permission> GetPermissions(string type)
        {
            IList<Permission> resultList = new List<Permission>();

            if (this.Permissions != null && this.Permissions.Count > 0)
            {
                foreach (Permission permission in this.Permissions)
                {
                    if (permission.Category.Type == type)
                        resultList.Add(permission);
                }
            }

            return resultList;
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<Permission> OrganizationPermission
        {
            get
            {
                return GetPermissions(BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_ORGANIZATION);
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<Permission> MenuPermission
        {
            get
            {
                return GetPermissions(BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_MENU);
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<Permission> PagePermission
        {
            get
            {
                return GetPermissions(BusinessConstants.CODE_MASTER_PERMISSION_CATEGORY_TYPE_VALUE_PAGE);
            }
        }

        public bool HasPermission(string permissionCode)
        {
            foreach (Permission p in this.Permissions)
            {
                if (permissionCode == p.Code)
                {
                    return true;
                }
            }
            return false;
        }

        public string Name
        {
            get
            {
                return ((FirstName != null ? FirstName : string.Empty) +" "+ (LastName != null ? LastName : string.Empty));
            }

        }
        public string CodeName
        {
            get
            {
                return ((Code != null ? Code : string.Empty) + (Name != null ? " [" + Name + "]" : string.Empty));
            }

        }
        #endregion
    }
}