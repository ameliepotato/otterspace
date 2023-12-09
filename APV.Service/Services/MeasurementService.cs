using System.Security;

namespace APV.Service.Services
{
    public class MeasurementService : IDisposable
    {
        private List<Measurement> _measurements { get; set; }
        private string _filePath { get; set; }
        public MeasurementService(string? file = null) 
        {
            if (string.IsNullOrEmpty(file))
            {
                file = Directory.GetCurrentDirectory();
                file += "\\measurementsService.json";
            }
            _filePath = file;
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

        private bool SaveToFile()
        {
            return false;
        }

        private bool LoadFromFile() 
        { 
            return false; 
        }

        public virtual void Dispose()
        {
            SaveToFile();
        }
    }
}
