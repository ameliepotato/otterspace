using APV.Service.Database;

namespace APV.Service.Services
{
    public class MeasurementService
    {
        private MongoDatabaseManager? _dbManager { get; set; }

        public MeasurementService(string? connectionString = null)
        {
            _dbManager = null;
            connectionString = string.IsNullOrEmpty(connectionString) ?
                    Environment.GetEnvironmentVariable("MEASUREMENTSDB_CONNECTIONSTRING") :
                    connectionString;

            if (connectionString == null)
            {
                Console.WriteLine("No connection string.");
            }
            else
            {
                try
                {
                    _dbManager = new MongoDatabaseManager(connectionString, "Measurements", "Readings");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Measurement service failed to initialize DB connection: {e.Message}");
                }
            }
        }
        public bool IsConnectedToDB()
        {
            return _dbManager != null && _dbManager.IsConnected();
        }

        public Measurement? GetMeasurement(string sensorId)
        {
            if (IsConnectedToDB())
            {
                try
                {
                    Dictionary<string, string> filters = new Dictionary<string, string>();
                    filters.Add("SensorId", sensorId);
                    return _dbManager?.GetData<Measurement>(filters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error when retrieving measurements: {ex.Message}");
                }
            }
            return null;
        }

        public bool AddMeasurement(string sensorID, int temp, DateTime? time = null)
        {
            if (IsConnectedToDB())
            {
                if (time == null)
                {
                    time = DateTime.Now;
                }
                Measurement measurement = new Measurement(sensorID, temp, time);
                return _dbManager != null && _dbManager.AddData(measurement);
            }
            return false;
        }
    }
}
