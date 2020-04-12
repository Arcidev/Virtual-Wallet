using SQLite;
using System.IO;
using Windows.Storage;

namespace DAL.Helpers
{
    internal static class ConnectionHelper
    {
        public static readonly string DbPath = ApplicationData.Current.LocalFolder.Path;

        public const string DbFileName = "VirtualWalletDB.sqlite";

        public static string DbFilePath => Path.Combine(DbPath, DbFileName);

        /// <summary>
        /// Gets db connection or creates new db if not exists
        /// </summary>
        /// <returns>Db connection</returns>
        public static SQLiteAsyncConnection GetDbAsyncConnection()
        {
            return new SQLiteAsyncConnection(DbFilePath, false);
        }
    }
}
