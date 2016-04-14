using BL.Models.BankModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Threading.Tasks;

namespace Tests.Banks
{
    [TestClass]
    public class FioTests
    {
        [TestMethod]
        public async Task GetTransactionsTest()
        {
            var token = "USE_YOUR_TOKEN";

            var fio = new Fio() { Token = token };
            await fio.SetLastDownloadDateAsync(DateTime.Now.AddMonths(-1));
            var t = await fio.GetNewTransactionsAsync();
        }
    }
}
