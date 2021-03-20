using Gamlo.ValidationApi.Core.Model;

namespace Gamlo.ValidationApi.Core.Interfaces
{
    public interface IValidator
    {
        string Name { get; }

        bool IsValueValid(SchemeModel scheme, ValueModel value);
    }
}
