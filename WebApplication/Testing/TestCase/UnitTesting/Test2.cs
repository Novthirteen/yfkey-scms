using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using com.Sconit.Entity.MasterData;

namespace SconitTesting.TestCase.UnitTesting
{
    [TestFixture]
    public class Test2 : UnitTestingBase
    {
        [Test]
        public void Test1()
        {
            IList<CycleCountResult> list = TheCycleCountResultMgr.GetCycleCountResult("CYC000000006");
        }
    }
}
