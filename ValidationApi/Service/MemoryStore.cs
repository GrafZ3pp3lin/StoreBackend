using Gamlo.ValidationApi.Core.Interfaces;
using Gamlo.ValidationApi.Core.Model;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Gamlo.ValidationApi.Service
{
    public class MemoryStore : IStore
    {
        private readonly ConcurrentDictionary<string, SchemeModel> schemes;
        private readonly ConcurrentDictionary<string, string> values;

        public MemoryStore()
        {
            values = new ConcurrentDictionary<string, string>();
            schemes = new ConcurrentDictionary<string, SchemeModel>();
        }

        public virtual Task<SchemeModel> GetScheme(string name)
        {
            if (!schemes.TryGetValue(name, out SchemeModel? model) || model == null)
            {
                throw new InvalidOperationException($"No Scheme with name {name} found.");
            }
            return Task.FromResult(model);
        }

        public virtual Task<string> GetValue(string id)
        {
            if (!values.TryGetValue(id, out string? value) || value == null)
            {
                throw new InvalidOperationException($"No Value with id {id} found.");
            }
            return Task.FromResult(value);
        }

        public virtual Task StoreScheme(SchemeModel scheme)
        {
            if (!schemes.TryAdd(scheme.Name, scheme))
            {
                throw new InvalidOperationException($"Scheme with name {scheme.Name} already exists.");
            }
            return Task.CompletedTask;
        }

        public virtual Task StoreValue(string id, string value)
        {
            values.AddOrUpdate(id, value, (key, oldValue) => value);
            return Task.CompletedTask;
        }
    }
}