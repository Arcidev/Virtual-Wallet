using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;
using System;
using System.IO;
using Windows.Storage;

namespace DAL.Helpers
{
    internal static class ConnectionHelper
    {
        public static readonly string DbPath = ApplicationData.Current.LocalFolder.Path;

        public const string DbFileName = "VirtualWalletDB.sqlite";

        /// <summary>
        /// Gets db connection or creates new db if not exists
        /// </summary>
        /// <returns></returns>
        public static SQLiteAsyncConnection GetDbAsyncConnection()
        {
            var dbFilePath = Path.Combine(DbPath, DbFileName);

            var connectionFactory =
                new Func<SQLiteConnectionWithLock>(
                    () =>
                    new SQLiteConnectionWithLock(
                        new SQLitePlatformWinRT(),
                        new SQLiteConnectionString(dbFilePath, storeDateTimeAsTicks: false)));

            return new SQLiteAsyncConnection(connectionFactory);
        }
    }
}
