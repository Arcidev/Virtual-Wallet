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
        private static readonly IWallets wallets = new Wallets();
        private static readonly IWalletsCategories walletsCategories = new WalletsCategories();

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

            await categories.InsertAsync(cat1, cat2);
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
            await categories.InsertAsync(cat1);

            var modifier = new CategoryModifier() { IncludeImage = true };
            var category = (await categories.GetAllAsync(modifier)).Single();

            Assert.IsNotNull(category.Image);
            Assert.AreEqual((int)ImageId.Fio, category.Image.Id);

            await categories.DeleteAllAsync();
            Assert.AreEqual(0, (await categories.GetAllAsync()).Count);
        }

        [TestMethod]
        public async Task AssociationDeleteTest()
        {
            await categories.DeleteAllAsync();
            await wallets.DeleteAllAsync();
            await walletsCategories.DeleteAllAsync();

            Category cat1 = new Category() { Name = "category test 1" };
            Category cat2 = new Category() { Name = "category test 2" };

            Wallet wallet1 = new Wallet() { Name = "wallet test 1" };
            Wallet wallet2 = new Wallet() { Name = "wallet test 2" };

            await categories.InsertAsync(cat1, cat2);
            await wallets.InsertAsync(wallet1, wallet2);

            var categoryFilter = new CategoryFilter() { Name = "category test 1" };
            cat1 = (await categories.GetAsync(categoryFilter)).First();
            categoryFilter.Name = "category test 2";
            cat2 = (await categories.GetAsync(categoryFilter)).First();

            var walletFilter = new WalletFilter() { Name = "wallet test 1" };
            wallet1 = (await wallets.GetAsync(walletFilter)).First();
            walletFilter.Name = "wallet test 2";
            wallet2 = (await wallets.GetAsync(walletFilter)).First();

            var walletCategory1 = new WalletCategory() { CategoryId = cat1.Id, WalletId = wallet1.Id };
            var walletCategory2 = new WalletCategory() { CategoryId = cat2.Id, WalletId = wallet2.Id };
            await walletsCategories.InsertAsync(walletCategory1, walletCategory2);
            Assert.AreEqual(2, (await walletsCategories.GetAllAsync()).Count);

            await categories.DeleteAsync(cat1.Id);
            Assert.AreEqual(1, (await walletsCategories.GetAllAsync()).Count);

            await wallets.DeleteAsync(wallet2.Id);
            Assert.AreEqual(0, (await walletsCategories.GetAllAsync()).Count);
        }
    }
}
