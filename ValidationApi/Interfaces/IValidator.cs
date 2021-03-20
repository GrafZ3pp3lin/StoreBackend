using Gamlo.StoreBackend.Model;

namespace Gamlo.StoreBackend.Service
{
    public interface IValidator
    {
        string Name { get; }

        bool IsValueValid(SchemeModel scheme, ValueModel value);
    }
}
