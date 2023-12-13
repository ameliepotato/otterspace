using APV.Service.Database;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver.Core.Configuration;
using System.Globalization;

namespace APV.Service.Services
{
    public class MeasurementService : IDbService, IMeasurementService
    {
        private readonly ILogger<MeasurementService> _logger;
        private IDataManager<Measurement> _dbManager { get; set; }
        public MeasurementService(ILogger<MeasurementService> logger, IDataManager<Measurement> dataManager)
        {
            _logger = logger;
            _logger.LogInformation($"Measurement service created and is connected: {dataManager.IsConnected()}");
            _dbManager = dataManager;
        }

        public bool IsConnected()
        {
            bool connected = _dbManager != null && _dbManager.IsConnected();
            if (!connected)
            {
                _logger.LogWarning($"Measurement service not connected");
            }
            return connected;
        }

        public List<Measurement>? GetMeasurements()
        {
            _logger.LogInformation($"Get Measurements");
            if (IsConnected())
            {
                try
                {
                    List<Measurement>? measurements = _dbManager.GetManyData();
                    _logger.LogInformation($"Found {measurements?.Count} measurements");
                    if (measurements != null && measurements.Count > 0)
                    {
                        measurements = measurements.OrderByDescending(x => x.Time).ToList();
                        List<Measurement> lastValues = new List<Measurement>();
                        foreach (Measurement measurement in measurements)
                        {
                            if(lastValues.Find( x => x.SensorId == measurement.SensorId ) == null)
                            {
                                lastValues.Add( measurement );
                            }
                        }
                        return lastValues;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error when retrieving measurements: {ex.Message}");
                }
            }
            _logger.LogWarning("No measurement found");
            return null;
        }

        public bool AddMeasurement(string sensorID, int temp, DateTime? time = null)
        {
            _logger.LogInformation($"Add Measurement of sensor: {sensorID}, temperature {temp}, time: {time?.ToLongTimeString()}");
            if (IsConnected())
            {
                if (time == null)
                {
                    time = DateTime.Now;
                }
                Measurement measurement = new Measurement(sensorID, temp, time);
                _logger.LogInformation($"Add Measurement of sensor: {sensorID}, temperature {temp}, time: {time?.ToLongTimeString()}");
                return _dbManager != null && _dbManager.AddData(measurement);
            }
            _logger.LogWarning("Not added");
            return false;
        }
    }
}
