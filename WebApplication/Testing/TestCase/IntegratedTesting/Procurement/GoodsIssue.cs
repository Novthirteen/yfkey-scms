using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using NUnit.Framework;
using SconitTesting.Utility;

namespace SconitTesting.TestCase.IntegratedTesting.Procurement
{
    [TestFixture]
    class GoodsIssue : IntegratedTestingBase
    {
        private string url { get { return Utilities.GetUrl("~/Main.aspx?mid=Order.OrderIssue__mp--ModuleType-Distribution_Supplier-true"); } }

        [Test]
        public void OrderDeliver()
        {
            ie.GoTo(url);

            ie.TextField(Find.ById("ctl01_ucOrderIssue_ucSearch_tbFlow_suggest")).Focus();
            ie.WaitForComplete();

            ie.TextField(Find.ById("ctl01_ucOrderIssue_ucSearch_tbFlow_suggest")).TypeText("CSSY");

            ie.CheckBox(Find.ById("ctl01_ucOrderIssue_ucList_GV_List_ctl02_CheckBoxGroup")).Checked = true;

            ie.Button(Find.ById("ctl01_ucOrderIssue_ucList_btnEditShipQty")).Click();

            ie.CheckBox(Find.ById("ctl01_ucOrderIssue_ucDetailMain_cbPrintAsn")).Checked = true;

            ie.Button(Find.ById("ctl01_ucOrderIssue_ucDetailMain_btnShip")).Click();

            ie.Close();
        }
    }
}
