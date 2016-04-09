using BL.Models.BankModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Tests.Banks
{
    [TestClass]
    public class FioTests
    {
        [TestMethod]
        public void GetTransactionsTest()
        {
            var token = "USE_YOUR_TOKEN";

            var fio = new Fio() { Token = token };
            var t = fio.StoredTransactions;
        }
    }
}
