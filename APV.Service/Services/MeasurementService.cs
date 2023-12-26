using APV.Service.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics.Metrics;

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

        public List<Measurement>? GetAllLatestMeasurements()
        {
            _logger.LogInformation($"Get all latest measurements");

            try
            {
                List<Measurement>? measurements = new List<Measurement>();
                var readingsCollection = _dataManager.GetCollection<Measurement>();
                List<BsonDocument> pipeline = new List<BsonDocument>();
               
                var filtered = readingsCollection?.AsQueryable()?.OrderByDescending(m => m.Time)?
                    .GroupBy(m => m.SensorId)?.Select(x => x.First());

                measurements = filtered?.ToList();

                if (measurements != null)
                {
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

        public List<SensorHistoryEntry>? GetSensorHistory(string sensorId, DateTime from, DateTime? to)
        {
            to = to ?? DateTime.Now;
            _logger.LogInformation($"Getting sensor {sensorId} history from {from} to {to}");
            try
            {
                var readingsCollection = _dataManager.GetCollection<Measurement>();
                List<BsonDocument> pipeline = new List<BsonDocument>();

                IEnumerable<SensorHistoryEntry>? entries = readingsCollection?.AsQueryable()?
                    .Where( x => x.SensorId == sensorId &&
                                x.Time >= from &&
                                x.Time <= to)?
                    .OrderByDescending(m => m.Time)?
                    .Select(s => new SensorHistoryEntry()
                    {
                        Temperature = s.Value,
                        RegisteredOn = s.Time
                    });

                if (entries != null)
                {
                    _logger.LogInformation($"Found {entries.Count()} results");
                }
                else
                {
                    _logger.LogWarning("No data found.");
                }
                return entries?.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when retrieving measurements: {ex.Message}");
            }
            _logger.LogWarning("No history entry found");
            return null;
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
