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

        public Measurement? GetMeasurement(string sensorId)
        {
            _logger.LogInformation($"Get Measurement of sensor: {sensorId}");
            if (IsConnected())
            {
                try
                {
                    Dictionary<string, string> filters = new Dictionary<string, string>();
                    filters.Add("SensorId", sensorId);
                    return _dbManager.GetData(filters);
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
