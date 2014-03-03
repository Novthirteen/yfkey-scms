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
    class ReceiveByASN : IntegratedTestingBase
    {
        private string url { get { return Utilities.GetUrl("Main.aspx?mid=Order.GoodsReceipt__mp--ModuleType-Procurement"); } }

        [Test]
        public void OrderReceiveByASN()
        {    
            ie.GoTo(url);

            ie.Link(Find.ById("ctl01_ucNavigator_lbAsn")).Click();

            #region todo 查询条件
            #endregion

            ie.Button(Find.ById("ctl01_ucAsnReceipt_ucSearch_btnSearch")).Click();

            ie.Link(Find.ById("ctl01_ucAsnReceipt_ucList_GV_List_ctl02_lbtnView")).Click();

            string huid = ie.Span(Find.ById("ctl01_ucAsnReceipt_ucViewMain_ucDetailList_ucTransformer_GV_List_ctl02_ucTransformerDetail_GV_List_ctl02_lblHuId")).Text;

            ie.Button(Find.ById("ctl01_ucAsnReceipt_ucViewMain_ucDetailList_btnBack")).Click();

            ie.Link(Find.ById("ctl01_ucAsnReceipt_ucList_GV_List_ctl02_lbtnEdit")).Click();

            decimal toIssueQty = Convert.ToDecimal(ie.Span(Find.ById("ctl01_ucAsnReceipt_ucEditMain_ucDetailList_ucTransformer_GV_List_ctl02_lblQty")).Text);

            ie.TextField(Find.ById("ctl01_ucAsnReceipt_ucEditMain_ucDetailList_ucTransformer_GV_List_ctl02_tbCurrentQty")).TypeText((toIssueQty / 2).ToString());

            ie.TextField(Find.ById("ctl01_ucAsnReceipt_ucEditMain_ucDetailList_tbRefNo")).TypeText(huid);

            ie.Button(Find.ById("ctl01_ucAsnReceipt_ucEditMain_ucDetailList_btnReceive")).Click();

            ie.Close();
        }
    }
}
