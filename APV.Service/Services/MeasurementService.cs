namespace APV.Service.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly ILogger<MeasurementService> _logger;
        private IDataManager<Measurement> _dataManager { get; set; }

        public MeasurementService(ILogger<MeasurementService> logger, IDataManager<Measurement> dataManager)
        {
            _logger = logger;
            _dataManager = dataManager;
            _logger.LogInformation($"Measurement service created");
        }

        public bool IsConnected()
        {
            bool connected = _dataManager != null && _dataManager.IsConnected();
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
                    List<Measurement>? measurements = _dataManager.GetManyData();
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
            _logger.LogInformation($"Add Measurement of sensor: {sensorID}, temperature {temp}, time: {time?.ToString()}");
            if (IsConnected())
            {
                if (time == null)
                {
                    time = DateTime.Now;
                }
                Measurement measurement = new Measurement(sensorID, temp, time);
                _logger.LogInformation($"Add Measurement of sensor: {sensorID}, temperature {temp}, time: {time?.ToString()}");
                return _dataManager.AddData(measurement);
            }
            _logger.LogWarning("Not added");
            return false;
        }
    }
}
