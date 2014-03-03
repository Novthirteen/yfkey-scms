using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using NUnit.Framework;
using com.Sconit.Service.Criteria;
using SconitTesting.Utility;

namespace SconitTesting.TestCase.IntegratedTesting.Procurement
{
    [TestFixture]
    class ReceiveOrder : IntegratedTestingBase
    {
        private string formatURL;
        private string url { get { return Utilities.GetUrl(formatURL); } }

        [Test]
        public void OrderReceive()
        {
            #region 初级阶段的查询方法
            this.formatURL = "Main.aspx?mid=Order.OrderHead.Procurement__mp--ModuleType-Procurement_ModuleSubType-Nml_StatusGroupId-4";
            ie.GoTo(url);

            ie.SelectList(Find.ById("ctl01_ucSearch_ddlStatus")).SelectByValue("In-Process");

            ie.Button(Find.ById("ctl01_ucSearch_btnSearch")).Click();

            string orderNo = ie.Table(Find.ById("ctl01_ucList_GV_List")).TableRows[1].TableCells[1].Text;
            #endregion

            this.formatURL = "Main.aspx?mid=Order.GoodsReceipt__mp--ModuleType-Procurement";

            ie.GoTo(url);

            ie.TextField(Find.ById("ctl01_ucOrderReceipt_ucSearch_tbOrderNo")).TypeText(orderNo);

            ie.Button(Find.ById("ctl01_ucOrderReceipt_ucSearch_btnSearch")).Click();

            ie.Link(Find.ById("ctl01_ucOrderReceipt_ucList_GV_List_ctl02_lbtnEdit")).Click();

            ie.Button(Find.ById("ctl01_ucOrderReceipt_ucReceiveMain_btnReceive")).Click();

            Assert.AreEqual(true, ie.ContainsText("成功"));
            ie.Close();
        }
    }
}
