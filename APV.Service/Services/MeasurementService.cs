using System.Security;
using System.Text.Json;

namespace APV.Service.Services
{
    public class MeasurementService
    {
        private List<Measurement> _measurements { get; set; }
        private string _file { get; set; }
        public MeasurementService(string? file = null) 
        {
            if (string.IsNullOrEmpty(file))
            {
                file = Directory.GetCurrentDirectory();
                file += Path.DirectorySeparatorChar + "measurementsService.json";
            }
            _file = file;
            _measurements = new List<Measurement>();
            LoadFromFile();
        }
        public bool AddMeasurement(Measurement measurement)
        {
            _measurements.Add(measurement);
            return true;
        }

        public bool AddMeasurement(string sensorID, int measurement)
        {
            return AddMeasurement(new Measurement(sensorID, measurement));
        }

        public Measurement? GetMeasurement(string sensorID)
        {
            try
            {
                List<Measurement> allFromSensor = _measurements.Where(x => x.SensorID == sensorID).ToList();
                allFromSensor = allFromSensor.OrderBy(x => x.Time).ToList();
                return allFromSensor.First();
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        private bool LoadFromFile() 
        {
            try
            {
                string jsonString = File.ReadAllText(_file);
                _measurements = JsonSerializer.Deserialize<List<Measurement>>(jsonString);
            }
            catch (Exception)
            {
                _measurements = new List<Measurement>();
                return false;
            }
            return true;
        }
    }
}
