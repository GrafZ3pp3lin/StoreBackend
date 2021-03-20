using Gamlo.ValidationApi.Core.Interfaces;
using Gamlo.ValidationApi.Core.Model;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Gamlo.ValidationApi.DataAccess
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
            connection.CreateTable<ValueEntry>();
        }

        public Task<SchemeModel> GetScheme(string name)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetValue(string id)
        {
            var entry = connection.Table<ValueEntry>().First(e => e.Id!.Equals(id));
            return Task.FromResult(entry.Value!);
        }

        public Task StoreScheme(SchemeModel scheme)
        {
            throw new NotImplementedException();
        }

        public Task StoreValue(string id, string value)
        {
            var entry = new ValueEntry
            {
                Id = id,
                Value = value
            };
            connection.InsertOrReplace(entry);
            return Task.CompletedTask;
        }
    }
}