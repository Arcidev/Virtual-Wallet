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

        public static string DbFilePath { get { return Path.Combine(DbPath, DbFileName); } }

        /// <summary>
        /// Gets db connection or creates new db if not exists
        /// </summary>
        /// <returns></returns>
        public static SQLiteAsyncConnection GetDbAsyncConnection()
        {
            var connectionFactory =
                new Func<SQLiteConnectionWithLock>(
                    () =>
                    new SQLiteConnectionWithLock(
                        new SQLitePlatformWinRT(),
                        new SQLiteConnectionString(DbFilePath, storeDateTimeAsTicks: false)));

            return new SQLiteAsyncConnection(connectionFactory);
        }
    }
}
