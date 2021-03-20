using Gamlo.ValidationApi.Core.Interfaces;

namespace Gamlo.ValidationApi.Interfaces
{
    public interface IValidatorResolver
    {
        bool HasValidator(string name);

        int LoadAllValidators(string path);

        bool LoadValidator(string name, string path);

        IValidator ResolveValidator(string name);
    }
}