using Gamlo.StoreBackend.Model;
using System.Threading.Tasks;

namespace Gamlo.StoreBackend.Service
{
    public interface IStore
    {
        Task<SchemeModel> GetScheme(string name);

        Task<string> GetValue(string id);

        Task StoreScheme(SchemeModel scheme);

        Task StoreValue(string id, string value);
    }
}