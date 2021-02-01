using SQLite;
using System;
using System.IO;
using Gamlo.StoreBackend.Service;

namespace Gamlo.StoreBackend.DataAccess
{
    public class SqLiteStore : IStore
    {
        private readonly SQLiteConnection connection;
        public SqLiteStore()
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "store-backend");
            _ = Directory.CreateDirectory(directory);
            var file = Path.Combine(directory, "data.db");
            connection = new SQLiteConnection(file);
            connection.CreateTable<Entry>();
        }

        public string GetValue(string id)
        {
            var entry = connection.Table<Entry>().First(e => e.Id.Equals(id));
            return entry.Value;
        }

        public void StoreValue(string id, string value)
        {
            var entry = new Entry
            {
                Id = id,
                Value = value
            };
            connection.InsertOrReplace(entry);
        }
    }
}
