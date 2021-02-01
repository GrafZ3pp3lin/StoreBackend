using System.Collections.Generic;

namespace Gamlo.StoreBackend.Service
{
    public class MemoryStore : IStore
    {
        private readonly Dictionary<string, string> values;

        public MemoryStore()
        {
            values = new Dictionary<string, string>();
        }

        public string GetValue(string id)
        {
            return values[id];
        }

        public void StoreValue(string id, string value)
        {
            values[id] = value;
        }
    }
}
