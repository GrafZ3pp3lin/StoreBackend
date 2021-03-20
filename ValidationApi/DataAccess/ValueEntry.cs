using SQLite;

namespace Gamlo.ValidationApi.DataAccess
{
    internal class ValueEntry
    {
        [PrimaryKey]
        public string? Id { get; set; }
        public string? Value { get; set; }
    }
}
