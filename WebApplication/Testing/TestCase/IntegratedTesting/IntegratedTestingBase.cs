using System;
using System.Diagnostics;
using WatiN.Core;
using WatiN.Core.Native.Windows;
using NUnit.Framework;
using System.Configuration;
using SconitTesting.Utility;

namespace SconitTesting.TestCase.IntegratedTesting
{
    public class IntegratedTestingBase
    {
        #region Property to web browser instance
        private IE _ie = null;

        /// <summary>
        /// Hook to our Internet Explorer instance
        /// </summary>
        public IE ie
        {
            get
            {
                if (_ie == null)
                {
                    this.Login(ref _ie);
                }
                return _ie;
            }
            set { _ie = value; }
        }
        #endregion

        #region Fixture setup and teardown
        [TestFixtureSetUp]
        public void SetupTestFixture()
        {
            //we lazy load the browser, so it is not in setup
        }

        [TestFixtureTearDown]
        public void TearDownTestFixture()
        {
            ie.Close();
            ie = null;
        }
        #endregion

        private void Login(ref IE ie)
        {
            Utilities.NavigateToHomePage(ref ie);

            if (ie.Url.Contains("Login"))
            {
                ie.GoTo(Utilities.GetUrl("Index.aspx"));

                string UserName = ConfigurationReader.getWebUserName();
                string Password = ConfigurationReader.getWebPassword();

                ie.TextField(Find.ById("txtUsername")).TypeText(UserName);
                ie.TextField(Find.ById("txtPassword")).TypeText(Password);

                ie.Image(Find.ById("IbLogin")).ClickNoWait();
            }
        }
    }
}
