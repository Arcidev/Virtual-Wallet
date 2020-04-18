using SQLite;
using System.IO;
using Windows.Storage;

namespace DAL.Helpers
{
    internal static class ConnectionHelper
    {
        private static readonly object locker = new object();
        private static SQLiteAsyncConnection connection;

        public static readonly string DbPath = ApplicationData.Current.LocalFolder.Path;

        public const string DbFileName = "VirtualWalletDB.sqlite";

        public static string DbFilePath => Path.Combine(DbPath, DbFileName);

        /// <summary>
        /// Gets db connection or creates new db if not exists
        /// </summary>
        /// <returns>Db connection</returns>
        public static SQLiteAsyncConnection GetDbAsyncConnection()
        {
            if (connection != null)
                return connection;

            lock (locker)
            {
                if (connection == null)
                    connection = new SQLiteAsyncConnection(DbFilePath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);

                return connection;
            }
        }
    }
}
