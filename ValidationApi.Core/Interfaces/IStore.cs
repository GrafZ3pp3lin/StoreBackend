using Gamlo.ValidationApi.Core.Model;
using System.Threading.Tasks;

namespace Gamlo.ValidationApi.Core.Interfaces
{
    public interface IStore
    {
        Task<SchemeModel> GetScheme(string name);

        Task<string> GetValue(string id);

        Task StoreScheme(SchemeModel scheme);

        Task StoreValue(string id, string value);
    }
}