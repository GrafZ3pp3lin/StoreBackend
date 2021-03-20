using Gamlo.StoreBackend.Service;

namespace Gamlo.StoreBackend.Interfaces
{
    public interface IValidatorResolver
    {
        IValidator ResolveValidator(string name);

        bool LoadValidator(string name, string path);

        int LoadAllValidators(string path);
    }
}
