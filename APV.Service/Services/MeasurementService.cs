using APV.Service.Database;
using System.Globalization;
using System.Security;
using System.Text.Json;

namespace APV.Service.Services
{
    public class MeasurementService
    { 
        private MongoDatabaseManager _dbManager { get; set; }

        public MeasurementService(string? connectionString = null)
        {
            connectionString = string.IsNullOrEmpty(connectionString) ?
                    Environment.GetEnvironmentVariable("MEASUREMENTSDB_CONNECTIONSTRING") :
                    connectionString;

            if (connectionString == null)
            {
                Console.WriteLine("No connection string.");
            }
            else
            {
                _dbManager = new MongoDatabaseManager(connectionString, "Measurements", "Readings");
            }
        }
        public bool IsConnectedToDB()
        {
            return _dbManager.IsConnected();
        }

        public Measurement? GetMeasurement(string sensorID)
        {
            try
            {
                Dictionary<string, string> filters = new Dictionary<string, string>();
                filters.Add("sensorid", sensorID);
                return _dbManager.GetData<Measurement>(filters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when retrieving measurements: {ex.Message}");
            }
            return null;
        }

        public bool AddMeasurement(string sensorID, int temp, DateTime? time = null)
        {
            if(time == null)
            {
                time = DateTime.Now;
            }
            Measurement measurement = new Measurement(sensorID, temp, time);
            measurement._id = time.Value.Ticks.ToString();
            return _dbManager.AddData<Measurement>(measurement);
        }
    }
}
