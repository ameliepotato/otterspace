using MongoDB.Driver;

namespace APV.Service.Database
{
    public interface IMongoDataManager
    {
        public abstract IMongoCollection<T>? GetCollection<T>();

        public abstract bool IsConnected();
    }
}
