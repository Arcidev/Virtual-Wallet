using DAL.Data;
using Shared.Enums;
using SQLite.Net.Async;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    internal static class InitDataHelper
    {
        private const string imageStorage = "ms-appx:///Assets/Images/";

        /// <summary>
        /// Adds required data into database if not exists
        /// </summary>
        /// <param name="connection">SQLiteAsyncConnection that should be inicialized outside</param>
        public static async Task InitData(SQLiteAsyncConnection connection)
        {
            await connection.InsertOrReplaceAllAsync(new object[]
            {
                new Image() { Id = (int)ImageId.Fio, Path= $"{imageStorage}Fio.png" },

                new Image() { Id = 10, Path= $"{imageStorage}/Wallets/Wallet1.png" },
                new Image() { Id = 11, Path= $"{imageStorage}/Wallets/Wallet2.png" },
                new Image() { Id = 12, Path= $"{imageStorage}/Wallets/Wallet3.png" },
                new Image() { Id = 13, Path= $"{imageStorage}/Wallets/Wallet4.png" },
                new Image() { Id = 14, Path= $"{imageStorage}/Wallets/Wallet5.png" },
                new Image() { Id = 15, Path= $"{imageStorage}/Wallets/Wallet6.png" },
                new Image() { Id = 16, Path= $"{imageStorage}/Wallets/Wallet7.png" },
                new Image() { Id = 17, Path= $"{imageStorage}/Wallets/Wallet8.png" },
                new Image() { Id = 18, Path= $"{imageStorage}/Wallets/Wallet9.png" },
                new Image() { Id = 19, Path= $"{imageStorage}/Wallets/Wallet10.png" },
                new Image() { Id = 20, Path= $"{imageStorage}/Wallets/Wallet11.png" },
                new Image() { Id = 21, Path= $"{imageStorage}/Wallets/Wallet12.png" },
                new Image() { Id = 22, Path= $"{imageStorage}/Wallets/Wallet13.png" },
                new Image() { Id = 23, Path= $"{imageStorage}/Wallets/Wallet14.png" },
                new Image() { Id = 24, Path= $"{imageStorage}/Wallets/Wallet15.png" },

                new Image() { Id = 25, Path= $"{imageStorage}/Categories/Cloth/CategoryCloth1.png" },
                new Image() { Id = 26, Path= $"{imageStorage}/Categories/Cloth/CategoryCloth2.png" },
                new Image() { Id = 27, Path= $"{imageStorage}/Categories/Cloth/CategoryCloth3.png" },
                new Image() { Id = 28, Path= $"{imageStorage}/Categories/Cloth/CategoryCloth4.png" },
                new Image() { Id = 29, Path= $"{imageStorage}/Categories/Cloth/CategoryCloth5.png" },
                new Image() { Id = 30, Path= $"{imageStorage}/Categories/Cloth/CategoryCloth6.png" },
                new Image() { Id = 31, Path= $"{imageStorage}/Categories/Cloth/CategoryCloth7.png" },

                new Image() { Id = 32, Path= $"{imageStorage}/Categories/Electronics/CategoryElectronics1.png" },
                new Image() { Id = 33, Path= $"{imageStorage}/Categories/Electronics/CategoryElectronics2.png" },
                new Image() { Id = 34, Path= $"{imageStorage}/Categories/Electronics/CategoryElectronics3.png" },
                new Image() { Id = 35, Path= $"{imageStorage}/Categories/Electronics/CategoryElectronics4.png" },
                new Image() { Id = 36, Path= $"{imageStorage}/Categories/Electronics/CategoryElectronics5.png" },
                new Image() { Id = 37, Path= $"{imageStorage}/Categories/Electronics/CategoryElectronics6.png" },
                new Image() { Id = 38, Path= $"{imageStorage}/Categories/Electronics/CategoryElectronics7.png" },
                new Image() { Id = 39, Path= $"{imageStorage}/Categories/Electronics/CategoryElectronics8.png" },
                new Image() { Id = 40, Path= $"{imageStorage}/Categories/Electronics/CategoryElectronics9.png" },

                new Image() { Id = 41, Path= $"{imageStorage}/Categories/Family/CategoryFamily1.png" },
                new Image() { Id = 42, Path= $"{imageStorage}/Categories/Family/CategoryFamily2.png" },
                new Image() { Id = 43, Path= $"{imageStorage}/Categories/Family/CategoryFamily3.png" },
                new Image() { Id = 44, Path= $"{imageStorage}/Categories/Family/CategoryFamily4.png" },
                new Image() { Id = 45, Path= $"{imageStorage}/Categories/Family/CategoryFamily5.png" },
                new Image() { Id = 46, Path= $"{imageStorage}/Categories/Family/CategoryFamily6.png" },
                new Image() { Id = 47, Path= $"{imageStorage}/Categories/Family/CategoryFamily7.png" },
                new Image() { Id = 48, Path= $"{imageStorage}/Categories/Family/CategoryFamily8.png" },

                new Image() { Id = 49, Path= $"{imageStorage}/Categories/Food/CategoryFood1.png" },
                new Image() { Id = 50, Path= $"{imageStorage}/Categories/Food/CategoryFood2.png" },
                new Image() { Id = 51, Path= $"{imageStorage}/Categories/Food/CategoryFood3.png" },
                new Image() { Id = 52, Path= $"{imageStorage}/Categories/Food/CategoryFood4.png" },
                new Image() { Id = 53, Path= $"{imageStorage}/Categories/Food/CategoryFood5.png" },
                new Image() { Id = 54, Path= $"{imageStorage}/Categories/Food/CategoryFood6.png" },
                new Image() { Id = 55, Path= $"{imageStorage}/Categories/Food/CategoryFood7.png" },
                new Image() { Id = 56, Path= $"{imageStorage}/Categories/Food/CategoryFood8.png" },
                new Image() { Id = 57, Path= $"{imageStorage}/Categories/Food/CategoryFood9.png" },
                new Image() { Id = 58, Path= $"{imageStorage}/Categories/Food/CategoryFood10.png" },
                new Image() { Id = 59, Path= $"{imageStorage}/Categories/Food/CategoryFood11.png" },
                new Image() { Id = 60, Path= $"{imageStorage}/Categories/Food/CategoryFood12.png" },
                new Image() { Id = 61, Path= $"{imageStorage}/Categories/Food/CategoryFood13.png" },
                new Image() { Id = 62, Path= $"{imageStorage}/Categories/Food/CategoryFood14.png" },
                new Image() { Id = 63, Path= $"{imageStorage}/Categories/Food/CategoryFood15.png" },
                new Image() { Id = 64, Path= $"{imageStorage}/Categories/Food/CategoryFood16.png" },

                new Image() { Id = 65, Path= $"{imageStorage}/Categories/Health/CategoryHealth1.png" },
                new Image() { Id = 66, Path= $"{imageStorage}/Categories/Health/CategoryHealth2.png" },
                new Image() { Id = 67, Path= $"{imageStorage}/Categories/Health/CategoryHealth3.png" },
                new Image() { Id = 68, Path= $"{imageStorage}/Categories/Health/CategoryHealth4.png" },

                new Image() { Id = 69, Path= $"{imageStorage}/Categories/Holidays/CategoryHolidays1.png" },
                new Image() { Id = 70, Path= $"{imageStorage}/Categories/Holidays/CategoryHolidays2.png" },
                new Image() { Id = 71, Path= $"{imageStorage}/Categories/Holidays/CategoryHolidays3.png" },
                new Image() { Id = 72, Path= $"{imageStorage}/Categories/Holidays/CategoryHolidays4.png" },
                new Image() { Id = 73, Path= $"{imageStorage}/Categories/Holidays/CategoryHolidays5.png" },
                new Image() { Id = 74, Path= $"{imageStorage}/Categories/Holidays/CategoryHolidays6.png" },
                new Image() { Id = 75, Path= $"{imageStorage}/Categories/Holidays/CategoryHolidays7.png" },
                new Image() { Id = 76, Path= $"{imageStorage}/Categories/Holidays/CategoryHolidays8.png" },

                new Image() { Id = 77, Path= $"{imageStorage}/Categories/Home/CategoryHome1.png" },
                new Image() { Id = 78, Path= $"{imageStorage}/Categories/Home/CategoryHome2.png" },
                new Image() { Id = 79, Path= $"{imageStorage}/Categories/Home/CategoryHome3.png" },
                new Image() { Id = 80, Path= $"{imageStorage}/Categories/Home/CategoryHome4.png" },
                new Image() { Id = 81, Path= $"{imageStorage}/Categories/Home/CategoryHome5.png" },
                new Image() { Id = 82, Path= $"{imageStorage}/Categories/Home/CategoryHome6.png" },
                new Image() { Id = 83, Path= $"{imageStorage}/Categories/Home/CategoryHome7.png" },
                new Image() { Id = 84, Path= $"{imageStorage}/Categories/Home/CategoryHome8.png" },
                new Image() { Id = 85, Path= $"{imageStorage}/Categories/Home/CategoryHome9.png" },
                new Image() { Id = 86, Path= $"{imageStorage}/Categories/Home/CategoryHome10.png" },

                new Image() { Id = 87, Path= $"{imageStorage}/Categories/Pets/CategoryPets1.png" },
                new Image() { Id = 88, Path= $"{imageStorage}/Categories/Pets/CategoryPets2.png" },
                new Image() { Id = 89, Path= $"{imageStorage}/Categories/Pets/CategoryPets3.png" },
                new Image() { Id = 90, Path= $"{imageStorage}/Categories/Pets/CategoryPets4.png" },
                new Image() { Id = 91, Path= $"{imageStorage}/Categories/Pets/CategoryPets5.png" },
                new Image() { Id = 92, Path= $"{imageStorage}/Categories/Pets/CategoryPets6.png" },
                new Image() { Id = 93, Path= $"{imageStorage}/Categories/Pets/CategoryPets7.png" },
                new Image() { Id = 94, Path= $"{imageStorage}/Categories/Pets/CategoryPets8.png" },
                new Image() { Id = 95, Path= $"{imageStorage}/Categories/Pets/CategoryPets9.png" },
                new Image() { Id = 96, Path= $"{imageStorage}/Categories/Pets/CategoryPets10.png" },

                new Image() { Id = 97, Path= $"{imageStorage}/Categories/Relax/CategoryRelax1.png" },
                new Image() { Id = 98, Path= $"{imageStorage}/Categories/Relax/CategoryRelax2.png" },
                new Image() { Id = 99, Path= $"{imageStorage}/Categories/Relax/CategoryRelax3.png" },
                new Image() { Id = 100, Path= $"{imageStorage}/Categories/Relax/CategoryRelax4.png" },
                new Image() { Id = 101, Path= $"{imageStorage}/Categories/Relax/CategoryRelax5.png" },
                new Image() { Id = 102, Path= $"{imageStorage}/Categories/Relax/CategoryRelax6.png" },
                new Image() { Id = 103, Path= $"{imageStorage}/Categories/Relax/CategoryRelax7.png" },
                new Image() { Id = 104, Path= $"{imageStorage}/Categories/Relax/CategoryRelax8.png" },

                new Image() { Id = 105, Path= $"{imageStorage}/Categories/Sport/CategorySport1.png" },
                new Image() { Id = 106, Path= $"{imageStorage}/Categories/Sport/CategorySport2.png" },
                new Image() { Id = 107, Path= $"{imageStorage}/Categories/Sport/CategorySport3.png" },
                new Image() { Id = 108, Path= $"{imageStorage}/Categories/Sport/CategorySport4.png" },
                new Image() { Id = 109, Path= $"{imageStorage}/Categories/Sport/CategorySport5.png" },
                new Image() { Id = 110, Path= $"{imageStorage}/Categories/Sport/CategorySport6.png" },
                new Image() { Id = 111, Path= $"{imageStorage}/Categories/Sport/CategorySport7.png" },
                new Image() { Id = 112, Path= $"{imageStorage}/Categories/Sport/CategorySport8.png" },
                new Image() { Id = 113, Path= $"{imageStorage}/Categories/Sport/CategorySport9.png" },
                new Image() { Id = 114, Path= $"{imageStorage}/Categories/Sport/CategorySport10.png" },
                new Image() { Id = 115, Path= $"{imageStorage}/Categories/Sport/CategorySport11.png" },
                new Image() { Id = 116, Path= $"{imageStorage}/Categories/Sport/CategorySport12.png" },
                new Image() { Id = 117, Path= $"{imageStorage}/Categories/Sport/CategorySport13.png" },

                new Image() { Id = 118, Path= $"{imageStorage}/Categories/Transport/CategoryTransport1.png" },
                new Image() { Id = 119, Path= $"{imageStorage}/Categories/Transport/CategoryTransport2.png" },
                new Image() { Id = 120, Path= $"{imageStorage}/Categories/Transport/CategoryTransport3.png" },
                new Image() { Id = 121, Path= $"{imageStorage}/Categories/Transport/CategoryTransport4.png" },
                new Image() { Id = 122, Path= $"{imageStorage}/Categories/Transport/CategoryTransport5.png" },
                new Image() { Id = 123, Path= $"{imageStorage}/Categories/Transport/CategoryTransport6.png" },
                new Image() { Id = 124, Path= $"{imageStorage}/Categories/Transport/CategoryTransport7.png" },
                new Image() { Id = 125, Path= $"{imageStorage}/Categories/Transport/CategoryTransport8.png" },
                new Image() { Id = 126, Path= $"{imageStorage}/Categories/Transport/CategoryTransport9.png" },
                new Image() { Id = 127, Path= $"{imageStorage}/Categories/Transport/CategoryTransport10.png" },
                new Image() { Id = 128, Path= $"{imageStorage}/Categories/Transport/CategoryTransport11.png" },
                new Image() { Id = 129, Path= $"{imageStorage}/Categories/Transport/CategoryTransport12.png" },
                new Image() { Id = 130, Path= $"{imageStorage}/Categories/Transport/CategoryTransport13.png" },
                new Image() { Id = 131, Path= $"{imageStorage}/Categories/Transport/CategoryTransport14.png" },
                new Image() { Id = 132, Path= $"{imageStorage}/Categories/Transport/CategoryTransport15.png" },
                new Image() { Id = 133, Path= $"{imageStorage}/Categories/Transport/CategoryTransport16.png" },
                new Image() { Id = 134, Path= $"{imageStorage}/Categories/Transport/CategoryTransport17.png" },

                new Image() { Id = 135, Path= $"{imageStorage}/Categories/Vacation/CategoryVacation1.png" },
                new Image() { Id = 136, Path= $"{imageStorage}/Categories/Vacation/CategoryVacation2.png" },
                new Image() { Id = 137, Path= $"{imageStorage}/Categories/Vacation/CategoryVacation3.png" },
                new Image() { Id = 138, Path= $"{imageStorage}/Categories/Vacation/CategoryVacation4.png" },
                new Image() { Id = 139, Path= $"{imageStorage}/Categories/Vacation/CategoryVacation5.png" },

                new Bank() { Id = (int)BankId.Fio, Name = "Fio banka", ImageId = (int)ImageId.Fio }
            });

            if (await connection.Table<Currency>().FirstOrDefaultAsync() == null)
            {
                await connection.InsertAllAsync(new object[]
                {
                    new Currency() { Code = "EUR", ExchangeRate = 1, IsDefaultCurrency = false },
                    new Currency() { Code = "CZK", ExchangeRate = 28, IsDefaultCurrency = true },
                    new Currency() { Code = "USD", ExchangeRate = 1.11f, IsDefaultCurrency = false }
                });
            }
        }
    }
}
