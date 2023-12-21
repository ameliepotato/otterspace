using APV.Service.Database;
using APV.Service.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Search;

namespace APV.Service.Tests.Unit.MockImplementations
{
    internal class MongoCollection<T> : MongoCollectionBase<T>
    {
        private List<T> _collection = new List<T>();
        public override void InsertOne(T document, InsertOneOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            _collection.Add(document);
        }

        public override CollectionNamespace CollectionNamespace => throw new NotImplementedException();

        public override IMongoDatabase Database => throw new NotImplementedException();

        public override IBsonSerializer<T> DocumentSerializer => throw new NotImplementedException();

        public override IMongoIndexManager<T> Indexes => throw new NotImplementedException();

        public override IMongoSearchIndexManager SearchIndexes => throw new NotImplementedException();

        public override MongoCollectionSettings Settings => throw new NotImplementedException();

        public override Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(PipelineDefinition<T, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<BulkWriteResult<T>> BulkWriteAsync(IEnumerable<WriteModel<T>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<long> CountAsync(FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IAsyncCursor<TField>> DistinctAsync<TField>(FieldDefinition<T, TField> field, FilterDefinition<T> filter, DistinctOptions options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(FilterDefinition<T> filter, FindOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<TProjection> FindOneAndDeleteAsync<TProjection>(FilterDefinition<T> filter, FindOneAndDeleteOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<TProjection> FindOneAndReplaceAsync<TProjection>(FilterDefinition<T> filter, T replacement, FindOneAndReplaceOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<TProjection> FindOneAndUpdateAsync<TProjection>(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<T, TResult> options = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override IFilteredMongoCollection<TDerivedDocument> OfType<TDerivedDocument>()
        {
            throw new NotImplementedException();
        }

        public override IMongoCollection<T> WithReadPreference(ReadPreference readPreference)
        {
            throw new NotImplementedException();
        }

        public override IMongoCollection<T> WithWriteConcern(WriteConcern writeConcern)
        {
            throw new NotImplementedException();
        }
    }

    public class MongoDataManager : IMongoDataManager
    {
        private readonly ILogger<MongoDataManager> _logger;
        public MongoDataManager(ILogger<MongoDataManager> logger) 
        {
            _logger = logger;
        }
        public IMongoCollection<T>? GetCollection<T>()
        {
            MongoCollection<T> collection = new MongoCollection<T>();
            return collection;
        }

        public bool IsConnected()
        {
            return true;
        }
    }
}
