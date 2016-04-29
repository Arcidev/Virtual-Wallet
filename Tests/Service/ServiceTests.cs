using BL.Models;
using BL.Service;
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
        private static readonly IDatabaseService _database = new DatabaseService();
        private static readonly IBankService _banks = new BankService();
        private static readonly ICategoryService _categories = new CategoryService();
        private static readonly IImageService _images = new ImageService();

        [ClassInitialize]
        public static async Task InitTest(TestContext context)
        {
            await _database.InitAsync();
        }

        [TestMethod]
        public async Task BankServiceTests()
        {
            var fio = (await _banks.GetAllAsync()).Single();
            Assert.IsTrue(fio is Fio);
            Assert.AreEqual((int)BankId.Fio, fio.Id);
            Assert.IsFalse(string.IsNullOrEmpty(fio.Name));
        }

        [TestMethod]
        public async Task CategoryServiceTests()
        {
            await _categories.DeleteAllAsync();

            Category cat1 = new Category() { Name = "Category 1" };
            Category cat2 = new Category() { Name = "Category 2" };

            await _categories.InsertAsync(cat1, cat2);
            var cats = await _categories.GetAllAsync();
            Assert.AreEqual(2, cats.Count);

            var category1Id = cats.Where(x => x.Name == "Category 1").Single().Id;
            var category2Id = cats.Where(x => x.Name == "Category 2").Single().Id;

            var filter = new CategoryFilter() { Name = "Category 1" };
            Assert.AreEqual("Category 1", (await _categories.GetAsync(filter)).Single().Name);
            Assert.AreEqual("Category 1", (await _categories.GetAsync(category1Id)).Name);

            filter.Name = "Category 2";
            Assert.AreEqual("Category 2", (await _categories.GetAsync(filter)).Single().Name);
            Assert.AreEqual("Category 2", (await _categories.GetAsync(category2Id)).Name);

            filter.Name = "Category";
            Assert.AreEqual(2, (await _categories.GetAsync(filter)).Count);

            await _categories.UpdateAsync(new Category() { Id = category1Id, Name = "Updated Category" });
            Assert.AreEqual(2, (await _categories.GetAllAsync()).Count);

            filter.Name = "Category 1";
            Assert.AreEqual(0, (await _categories.GetAsync(filter)).Count);
            Assert.AreEqual("Updated Category", (await _categories.GetAsync(category1Id)).Name);

            await _categories.DeleteAsync(category2Id);
            Assert.AreEqual(1, (await _categories.GetAllAsync()).Count);

            await _categories.DeleteAllAsync();
            Assert.AreEqual(0, (await _categories.GetAllAsync()).Count);
        }

        [TestMethod]
        public async Task CategoryServiceModifierTest()
        {
            await _categories.DeleteAllAsync();

            Category cat = new Category() { Name = "Category 1", ImageId = (int)ImageId.Fio };
            await _categories.InsertAsync(cat);

            var modifier = new CategoryModifier() { IncludeImage = true };
            var category = (await _categories.GetAllAsync(modifier)).Single();

            Assert.IsNotNull(category.Image);
            Assert.AreEqual((int)ImageId.Fio, category.Image.Id);

            cat = new Category() { Name = "Category 2", Image = (await _images.GetAsync((int)ImageId.Fio)) };
            await _categories.InsertAsync(cat);

            var filter = new CategoryFilter() { Name = "Category 2" };
            category = (await _categories.GetAsync(filter, modifier)).Single();

            Assert.IsNotNull(category.Image);
            Assert.AreEqual((int)ImageId.Fio, category.Image.Id);

            await _categories.DeleteAllAsync();
            Assert.AreEqual(0, (await _categories.GetAllAsync()).Count);
        }
    }
}
