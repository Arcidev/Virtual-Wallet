using BL.Models;
using BL.Models.BankModels;
using BL.Service;
using DAL.Config;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Shared.Enums;
using Shared.Filters;
using Shared.Modifiers;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.Service
{
    [TestClass]
    public class ServiceTests
    {
        private static readonly IDatabase _database = new DAL.Config.Database();
        private static readonly IBankService _banks = new BankService();
        private static readonly ICategoryService _categories = new CategoryService();
        private static readonly IIconService _icons = new IconService();

        [TestInitialize]
        public async Task InitTest()
        {
            await _database.InitAsync();
        }

        [TestMethod]
        public async Task BankServiceTests()
        {
            var fio = (await _banks.GetAll()).Single();
            Assert.IsTrue(fio is Fio);
            Assert.AreEqual((int)BankId.Fio, fio.Id);
            Assert.IsFalse(string.IsNullOrEmpty(fio.Name));
        }

        [TestMethod]
        public async Task CategoryServiceTests()
        {
            await _categories.DeleteAll();

            Category cat1 = new Category() { Name = "Category 1" };
            Category cat2 = new Category() { Name = "Category 2" };

            await _categories.Create(cat1, cat2);
            var cats = await _categories.GetAll();
            Assert.AreEqual(2, cats.Count);

            var category1Id = cats.Where(x => x.Name == "Category 1").First().Id;
            var category2Id = cats.Where(x => x.Name == "Category 2").First().Id;

            var filter = new CategoryFilter() { Name = "Category 1" };
            Assert.AreEqual("Category 1", (await _categories.Get(filter)).First().Name);
            Assert.AreEqual("Category 1", (await _categories.Get(category1Id)).Name);

            filter.Name = "Category 2";
            Assert.AreEqual("Category 2", (await _categories.Get(filter)).First().Name);
            Assert.AreEqual("Category 2", (await _categories.Get(category2Id)).Name);

            filter.Name = "Category";
            Assert.AreEqual(2, (await _categories.Get(filter)).Count);

            await _categories.Update(new Category() { Id = category1Id, Name = "Updated Category" });
            Assert.AreEqual(2, (await _categories.GetAll()).Count);

            filter.Name = "Category 1";
            Assert.AreEqual(0, (await _categories.Get(filter)).Count);
            Assert.AreEqual("Updated Category", (await _categories.Get(category1Id)).Name);

            await _categories.Delete(category2Id);
            Assert.AreEqual(1, (await _categories.GetAll()).Count);

            await _categories.DeleteAll();
            Assert.AreEqual(0, (await _categories.GetAll()).Count);
        }

        [TestMethod]
        public async Task CategoryModifierTest()
        {
            await _categories.DeleteAll();
            await _icons.DeleteAll();

            Icon icon = new Icon() { Id = 1, Name = "TestIcon", Path = "TestPath" };
            await _icons.Create(icon);

            Category cat1 = new Category() { Name = "Category 1", IconId = 1 };
            await _categories.Create(cat1);

            var modifier = new CategoryModifier() { IncludeIcon = true };
            var category = (await _categories.GetAll(modifier)).First();

            Assert.IsNotNull(category.Icon);
            Assert.AreEqual("TestIcon", category.Icon.Name);

            await _categories.DeleteAll();
            await _icons.DeleteAll();

            Assert.AreEqual(0, (await _categories.GetAll()).Count);
            Assert.AreEqual(0, (await _icons.GetAll()).Count);
        }
    }
}
