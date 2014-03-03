using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using com.Sconit.Entity.MasterData;

namespace SconitTesting.TestCase.UnitTesting
{
    [TestFixture]
    public class Test : UnitTestingBase
    {
        private decimal a;
        private decimal b;

        [SetUp]
        public void SetUp()
        {
            a = 16;
            b = -16;
        }

        [Test]
        public void Test1()
        {
            IList<CycleCountResult> list = TheCycleCountResultMgr.GetCycleCountResult("CYC000000006");
            Assert.AreEqual(a, list[0].InvQty);
            Assert.AreEqual(b, list[1].DiffQty);
        }
    }
}
