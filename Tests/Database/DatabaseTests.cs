using DAL.Config;
using DAL.Data;
using DAL.DataAccess;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Shared.Enums;
using Shared.Filters;
using Shared.Modifiers;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.Database
{
    [TestClass]
    public class DatabaseTests
    {
        private static readonly IDatabase database = new DAL.Config.Database();
        private static readonly ICategories categories = new Categories();
        private static readonly IImages images = new Images();

        [ClassInitialize]
        public static async Task InitTest(TestContext context)
        {
            await database.InitAsync();
        }

        [TestMethod]
        public async Task CategoryCRUDTest()
        {
            await categories.DeleteAllAsync();

            Category cat1 = new Category() { Name = "Category 1" };
            Category cat2 = new Category() { Name = "Category 2" };

            await categories.CreateAsync(cat1, cat2);
            var cats = await categories.GetAllAsync();
            Assert.AreEqual(2, cats.Count);

            var category1Id = cats.Where(x => x.Name == "Category 1").Single().Id;
            var category2Id = cats.Where(x => x.Name == "Category 2").Single().Id;

            var filter = new CategoryFilter() { Name = "Category 1" };
            Assert.AreEqual("Category 1", (await categories.GetAsync(filter)).Single().Name);
            Assert.AreEqual("Category 1", (await categories.GetAsync(category1Id)).Name);

            filter.Name = "Category 2";
            Assert.AreEqual("Category 2", (await categories.GetAsync(filter)).Single().Name);
            Assert.AreEqual("Category 2", (await categories.GetAsync(category2Id)).Name);

            filter.Name = "Category";
            Assert.AreEqual(2, (await categories.GetAsync(filter)).Count);

            await categories.UpdateAsync(new Category() { Id = category1Id, Name = "Updated Category" });
            Assert.AreEqual(2, (await categories.GetAllAsync()).Count);

            filter.Name = "Category 1";
            Assert.AreEqual(0, (await categories.GetAsync(filter)).Count);
            Assert.AreEqual("Updated Category", (await categories.GetAsync(category1Id)).Name);

            await categories.DeleteAsync(category2Id);
            Assert.AreEqual(1, (await categories.GetAllAsync()).Count);

            await categories.DeleteAllAsync();
            Assert.AreEqual(0, (await categories.GetAllAsync()).Count);
        }

        [TestMethod]
        public async Task CategoryModifierTest()
        {
            await categories.DeleteAllAsync();

            Category cat1 = new Category() { Name = "Category 1", ImageId = (int)ImageId.Fio };
            await categories.CreateAsync(cat1);

            var modifier = new CategoryModifier() { IncludeImage = true };
            var category = (await categories.GetAllAsync(modifier)).Single();

            Assert.IsNotNull(category.Image);
            Assert.AreEqual((int)ImageId.Fio, category.Image.Id);

            await categories.DeleteAllAsync();
            Assert.AreEqual(0, (await categories.GetAllAsync()).Count);
        }
    }
}
