using DAL.Config;
using DAL.Data;
using DAL.DataAccess;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Shared.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.Database
{
    [TestClass]
    public class DatabaseTests
    {
        private static readonly IDatabase database = new DAL.Config.Database();
        private static readonly ICategories categories = new Categories();

        [TestInitialize]
        public async Task InitTest()
        {
            await database.InitDatabaseAsync();
        }

        [TestMethod]
        public async Task CRUDTest()
        {
            Category cat1 = new Category() { Name = "Category 1" };
            Category cat2 = new Category() { Name = "Category 2" };

            await categories.Create(cat1, cat2);
            var cats = await categories.GetAll();
            Assert.AreEqual(2, cats.Count);

            var category1Id = cats.Where(x => x.Name == "Category 1").First().Id;
            var category2Id = cats.Where(x => x.Name == "Category 2").First().Id;

            var filter = new CategoryFilter() { Name = "Category 1" };
            Assert.AreEqual("Category 1", (await categories.Get(filter)).First().Name);
            Assert.AreEqual("Category 1", (await categories.Get(category1Id)).Name);

            filter.Name = "Category 2";
            Assert.AreEqual("Category 2", (await categories.Get(filter)).First().Name);
            Assert.AreEqual("Category 2", (await categories.Get(category2Id)).Name);

            filter.Name = "Category";
            Assert.AreEqual(2, (await categories.Get(filter)).Count);

            await categories.Update(new Category() { Id = category1Id, Name = "Updated Category" });
            Assert.AreEqual(2, (await categories.GetAll()).Count);

            filter.Name = "Category 1";
            Assert.AreEqual(0, (await categories.Get(filter)).Count);
            Assert.AreEqual("Updated Category", (await categories.Get(category1Id)).Name);

            await categories.Delete(category2Id);
            Assert.AreEqual(1, (await categories.GetAll()).Count);

            await categories.DeleteAll();
            Assert.AreEqual(0, (await categories.GetAll()).Count);
        }
    }
}
