using DAL.Config;
using DAL.Data;
using DAL.DataAccess;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
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
        private static readonly IIcons icons = new Icons();

        [TestInitialize]
        public async Task InitTest()
        {
            await database.InitAsync();
        }

        [TestMethod]
        public async Task CategoryCRUDTest()
        {
            await categories.DeleteAll();

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

        [TestMethod]
        public async Task CategoryModifierTest()
        {
            await categories.DeleteAll();
            await icons.DeleteAll();

            Icon icon = new Icon() { Id = 1, Name = "TestIcon", Path = "TestPath" };
            await icons.Create(icon);

            Category cat1 = new Category() { Name = "Category 1", IconId = 1 };
            await categories.Create(cat1);

            var modifier = new CategoryModifier() { IncludeIcon = true };
            var category = (await categories.GetAll(modifier)).First();

            Assert.IsNotNull(category.Icon);
            Assert.AreEqual("TestIcon", category.Icon.Name);

            await categories.DeleteAll();
            await icons.DeleteAll();

            Assert.AreEqual(0, (await categories.GetAll()).Count);
            Assert.AreEqual(0, (await icons.GetAll()).Count);
        }
    }
}
