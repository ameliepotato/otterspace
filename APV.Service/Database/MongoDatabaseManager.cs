using MongoDB.Driver;
using MongoDB.Bson;
using APV.Service.Services;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata;

[assembly: InternalsVisibleTo("APV.Service.Tests.Unit")]
namespace APV.Service.Database
{
    public class MongoDatabaseManager<T> : IDataManager<T>

    {
        private ILogger<MongoDatabaseManager<T>> _logger;
        private IMongoClient? _client { get; }
        private string _database { get; }
        private string _collection { get; }

        public MongoDatabaseManager(ILogger<MongoDatabaseManager<T>> logger, string connection, string database, string collection)
        {
            _collection = collection;
            _database = database;
            _logger = logger;
             try
            {
                _client = new MongoClient(connection);
            }
            catch (Exception e)
            {
                _logger.LogError($"Connection to mongo failed: {e.Message}");
            }

            if (_client == null)
            {
                _logger.LogError($"Connection was not possible with {connection}");
            }
            _logger.LogInformation($"Mongo database manager created");
        }

        public bool IsConnected()
        {
            bool isConnected = _client != null && !string.IsNullOrEmpty(_database) && !string.IsNullOrEmpty(_collection);
            if(!isConnected)
            {
                _logger.LogWarning($"Mongo database manager client is not connected for database {_database} and collection {_collection}");
            }
            return isConnected;
        }

        public List<T>? GetManyData(Dictionary<string, string>? filters = null)
        {
            if (!IsConnected())
            {
                return null;
            }
            var filter = Builders<T>.Filter.Empty;
            if (filters != null)
            {
                filter = Builders<T>.Filter.Eq(filters.First().Key, filters.First().Value);
            }
            var readingsCollection = _client?.GetDatabase(_database)?.GetCollection<T>(_collection);
            if (readingsCollection != null)
            {
                return readingsCollection.Find(filter)?.ToList<T>();
            }
            _logger.LogWarning($"Did not find data.");
            return null;
        }

        public T? GetData(Dictionary<string, string>? filters = null)
        {
            if (!IsConnected())
            {
                return default(T);
            }
            var filter = Builders<T>.Filter.Empty;
            if (filters != null && filters.Count > 0)
            {
                filter = Builders<T>.Filter.Eq(filters.First().Key, filters.First().Value);
            }
            var readingsCollection = _client?.GetDatabase(_database).GetCollection<T>(_collection);
            if (readingsCollection != null)
            {
                List<T> results = readingsCollection.Find(filter).ToList();
                if (results!= null && results.Count > 0)
                {
                    _logger.LogInformation($"Found {results.Count} results");
                    return results.First();
                }
            }
            _logger.LogWarning("No data found.");
            return default(T);
        }

        public bool AddData(T data)
        {
            _logger.LogInformation($"Adding data {data?.ToString()}");
            try
            {
                if (data != null && IsConnected())
                {
                    var readingsCollection = _client?.GetDatabase(_database)?.GetCollection<T>(_collection);
                    if (readingsCollection != null)
                    {
                        readingsCollection.InsertOne(data);
                        return true;
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogError($"Add data failed with error: {e.Message}");
            }
            return false;
        }

        public bool AddManyData(List<T> data)
        {
            _logger.LogInformation($"Adding data {data?.Count} sets");
            if(!IsConnected())
            {
                return false;
            }
            try
            {
                if (data != null && IsConnected() && data.Count > 0)
                {
                    var readingsCollection = _client?.GetDatabase(_database).GetCollection<T>(_collection);
                    readingsCollection?.InsertMany(data);
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Add data failed with error: {e.Message}");
            }
            return false;
        }
    }  
}

