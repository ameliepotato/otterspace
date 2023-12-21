using MongoDB.Driver;
using MongoDB.Bson;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata;

[assembly: InternalsVisibleTo("APV.Service.Tests.Unit")]
namespace APV.Service.Database
{
    public class MongoDataManager : IMongoDataManager

    {
        private readonly ILogger<MongoDataManager> _logger;
        private string _database { get; }
        private string _collection { get; }

        private IMongoClient? _client { get; }

        public MongoDataManager(ILogger<MongoDataManager> logger, string connection, string database, string collection)
        {
            _logger = logger;
            _logger.LogInformation($"Mongo database manager connecting to {connection}, database {_database}, collection {_collection}");
            _collection = collection;
            _database = database;
            try
            {
                _client = new MongoClient(connection);
                _logger.LogInformation($"Mongo database manager connected to {connection}");
            }
            catch (Exception e) 
            {
                _logger.LogError($"Error connecting to mongo db: {e.Message}");
            }
        }

        public IMongoCollection<T>? GetCollection<T>()
        {
            _logger.LogInformation($"Getting collection {_collection} from database {_database}");
            return _client?.GetDatabase(_database).GetCollection<T>(_collection);
        }

        public bool IsConnected()
        {
            if(_client != null)
            {
                return true;
            }
            _logger.LogError("Data manager is not connected to mongo db");
            return false;
        }
    }  
}

