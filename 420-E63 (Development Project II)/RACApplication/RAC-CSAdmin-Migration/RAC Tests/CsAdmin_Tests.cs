using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RAC_Tests
{
    [TestClass]
    public class CsAdmin_Tests
    {
        [TestMethod]
        public void TestCsAdminDbCalls()
        {
            CsAdmin cs = new CsAdmin();
            cs.GetCompetancyByProgramCode("");
            cs.GetProgramsOffered();
        }
    }
}
