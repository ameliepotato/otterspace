using APV.Service.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace APV.Service.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly ILogger<MeasurementService> _logger;
        private IMongoDataManager _dataManager;

        public MeasurementService(ILogger<MeasurementService> logger, IMongoDataManager dataManager)
        {
            _logger = logger;
            _dataManager = dataManager;
            _logger.LogInformation($"Measurement service created and is connected to db: {dataManager.IsConnected()}");
        }

        public List<Measurement>? GetMeasurements(string? sensorId = null,
            DateTime? from = null, DateTime? to = null, bool descending = true)
        {
            _logger.LogInformation($"Get Measurements");

            try
            {
                List<Measurement>? measurements = new List<Measurement>();
                var readingsCollection = _dataManager.GetCollection<Measurement>();
                _logger.LogInformation($"Filter: matching SensorId {sensorId}");
                List<BsonDocument> pipeline = new List<BsonDocument>();

                if (string.IsNullOrEmpty(sensorId))
                {
                    //sensorId = "*";
                }


                if (!to.HasValue)
                {
                    to = DateTime.MaxValue;
                }

                _logger.LogInformation($"Filter: time <= {to}");

                if (!from.HasValue)
                {
                    from = DateTime.MinValue;
                }

                to = to?.ToUniversalTime();
                from = from?.ToUniversalTime();
                
                _logger.LogInformation($"Filter: time >= {from}");

                var filtered = readingsCollection?.AsQueryable()?
                                    .Where(m =>
                                        m.Time <= to &&
                                        m.Time >= from &&
                                        (string.IsNullOrEmpty(sensorId) ?
                                            m.SensorId.Length > 0 :
                                            m.SensorId == sensorId));

                if(descending)
                {
                    filtered = filtered?.OrderByDescending(m => m.Time);
                }

                if(string.IsNullOrEmpty(sensorId))
                {
                    filtered = filtered?.GroupBy(m => m.SensorId)?.Select(x => x.First());
                }

                measurements = filtered?.ToList();

                if (measurements != null)
                {
                    if (descending)
                    {
                        _logger.LogInformation($"Filter: Descending");
                        measurements = measurements.OrderByDescending(x => x.Time).ToList();
                    }
                    _logger.LogInformation($"Found {measurements.Count} results");
                }
                else
                {
                    _logger.LogWarning("No data found.");
                }
                return measurements;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when retrieving measurements: {ex.Message}");
            }
            _logger.LogWarning("No measurement found");
            return null;
        }

        public bool AddMeasurement(Measurement measurement)
        {
            _logger.LogInformation($"Add Measurement of sensor: " +
                $"{measurement.SensorId}, temperature {measurement.Value}, time: {measurement.Time}");
            try
            {
                var readingsCollection = _dataManager.GetCollection<Measurement>();
                if (readingsCollection != null)
                {
                    readingsCollection.InsertOne(measurement);
                    return true;
                }
            }
            catch (MongoWriteException e)
            {
                _logger.LogError($"Add measurement failed with mongo error: {e.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Add measurement failed with error: {e.Message}");
            }
            return false;
        }

        public bool CreateIndex()
        {
            string index = "";
            _logger.LogInformation($"Creating index...");
            try
            {
                var collection = _dataManager.GetCollection<Measurement>();
                var indexKeysDefinition = Builders<Measurement>.IndexKeys.Ascending(m => m.SensorId).Descending(m => m.Time);
                index = collection.Indexes.CreateOne(new CreateIndexModel<Measurement>(indexKeysDefinition));
                _logger.LogInformation($"Index {index} was created");
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Index {index} creation failed with error: {e.Message}");
            }
            return false;
        }
    }
}
