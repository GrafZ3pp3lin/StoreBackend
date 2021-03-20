using SQLite;

namespace Gamlo.StoreBackend.DataAccess
{
    internal class ValueEntry
    {
        [PrimaryKey]
        public string? Id { get; set; }
        public string? Value { get; set; }
    }
}
