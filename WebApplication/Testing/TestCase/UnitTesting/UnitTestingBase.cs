using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core.Resource;
using com.Sconit.Service.MasterData;
using SconitTesting.Utility;
using com.Sconit.Service.Procurement;

namespace SconitTesting.TestCase.UnitTesting
{
    public class UnitTestingBase
    {
        #region 请按字母排序书写
        protected ICycleCountResultMgr TheCycleCountResultMgr { get { return ServiceHelper.GetService<ICycleCountResultMgr>("CycleCountResultMgr.service"); } }
        protected IFlowMgr TheFlowMgr { get { return ServiceHelper.GetService<IFlowMgr>("FlowMgr.service"); } }
        protected ILeanEngineMgr TheLeanEngineMgr { get { return ServiceHelper.GetService<ILeanEngineMgr>("LeanEngineMgr.service"); } }
        protected IOrderLocationTransactionMgr TheOrderLocationTransactionMgr { get { return ServiceHelper.GetService<IOrderLocationTransactionMgr>("OrderLocationTransactionMgr.service"); } }
        protected IOrderMgr TheOrderMgr { get { return ServiceHelper.GetService<IOrderMgr>("OrderMgr.service"); } }
        #endregion

        #region Fixture setup and teardown
        [TestFixtureSetUp]
        public void SetupTestFixture()
        {
        }

        [TestFixtureTearDown]
        public void TearDownTestFixture()
        {
        }
        #endregion

    }
}
