namespace Gamlo.StoreBackend.Service
{
    public interface IStore
    {
        void StoreValue(string id, string value);

        string GetValue(string id);
    }
}
