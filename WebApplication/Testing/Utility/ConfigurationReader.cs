using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SconitTesting.Utility
{
    /// <summary>
    /// Reads the app.config for the application DB name and username etc
    /// </summary>
    public static class ConfigurationReader
    {
        #region Public Methods

        /// <summary>
        /// Reads the HomePage URL from app.config
        /// </summary>
        /// <returns>Home Page URL</returns>
        public static string getHomePageUrl()
        {
            return getConfigurationValue("WebSiteUrl");
        }

        /// <summary>
        /// Reads the User Name from app.config
        /// </summary>
        /// <returns>User Name</returns>
        public static string getWebUserName()
        {
            return getConfigurationValue("WebUserName");
        }

        /// <summary>
        /// Reads the Password from the app.config
        /// </summary>
        /// <returns>Password</returns>
        public static string getWebPassword()
        {
            return getConfigurationValue("WebPassword");
        }

        #endregion

        #region Private Methods

        private static string getConfigurationValue(string KeyToFetch)
        {
            return ConfigurationManager.AppSettings[KeyToFetch];
        }

        #endregion
    }
}
