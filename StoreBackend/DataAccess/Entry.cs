using SQLite;

namespace Gamlo.StoreBackend.DataAccess
{
    class Entry
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Value { get; set; }
    }
}
