﻿using MongoDB.Driver;
using MongoDB.Bson;
using APV.Service.Services;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata;

[assembly: InternalsVisibleTo("APV.Service.Tests.Unit")]
namespace APV.Service.Database
{
    public class MongoDatabaseManager<T> : IDataManager<T>

    {
        private IMongoClient? _client { get; }
        private string _database { get; }
        private string _collection { get; }

        public MongoDatabaseManager(string connection, string database, string collection)
        {
            _collection = collection;
            _database = database;
            try
            {
                _client = new MongoClient(connection);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Connection to mongo failed: {e.Message}");
            }

            if (_client == null)
            {
                Console.WriteLine($"Connection was not possible with {connection}");
            }
        }

        public bool IsConnected()
        {
            return _client != null && !string.IsNullOrEmpty(_database) && !string.IsNullOrEmpty(_collection);
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
                    return results.First();
                }
            }
            return default(T);
        }

        public bool AddData(T data)
        {
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
                Console.WriteLine($"Add data failed with error: {e.Message}");
            }
            return false;
        }

        public bool AddManyData(List<T> data)
        {
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
                Console.WriteLine($"Add data failed with error: {e.Message}");
            }
            return false;
        }
    }  
}

