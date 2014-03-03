using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using NUnit.Framework;
using SconitTesting.TestCase.IntegratedTesting.Page;
using com.Sconit.Entity;
using SconitTesting.Utility;

namespace SconitTesting.TestCase.IntegratedTesting.Procurement
{
    [TestFixture]
    public class CreateOrder : IntegratedTestingBase
    {
        [SetUp]
        public void SetUp()
        {
            ie.GoTo(Utilities.GetUrl("Main.aspx?mid=Order.OrderHead.Procurement__mp--ModuleType-Procurement_ModuleSubType-Nml_StatusGroupId-4"));
        }

        [Test]
        public void CreateOrder1()
        {
            var page = ie.Page<OrderCreate>();

            //FlowMstr flowMstr = this.GetFlow(false);
            //if (flowMstr == null) return;

            string flowCode = string.Empty;
            double leadTime = .2;
            string winTime = DateTime.Now.AddDays(leadTime).AddHours(1).ToString("yyyy-MM-dd HH:mm");
            string qty1 = "200";

            page.btnNew.Click();
            page.tbFlow.Focus();
            page.tbFlow.TypeText(flowCode);
            ie.WaitForComplete();
            page.tbWinTime.TypeText(winTime);

            page.tbOrderQty1.TypeText(qty1);

            page.btnConfirm.Click();

            Assert.AreEqual(true, ie.ContainsText("成功"));
            ie.Close();
        }

        //private FlowMstr GetFlow(bool isScanHu)
        //{
        //    var query =
        //        from f in db.FlowMstrs
        //        where f.IsRecScan == isScanHu && f.Type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT
        //        select f;
        //    List<FlowMstr> flowMstrs = new List<FlowMstr>(query);

        //    FlowMstr flowMstr = null;
        //    if (flowMstrs != null && flowMstrs.Count > 0)
        //    {
        //        flowMstr = flowMstrs[0];
        //    }

        //    return flowMstr;
        //}
    }
}
