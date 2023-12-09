using System.Text.Json;

namespace APV.Service.Services
{
    public class SensorService: IDisposable
    {
        private List<Sensor> _sensors;
        private string _file = Directory.GetCurrentDirectory() + "\\sensorService.json";
        public SensorService(string? file = null) 
        {
            if (!string.IsNullOrEmpty(file) || !File.Exists(file))
            {
                _file = file??_file;
            }
            _sensors = new List<Sensor>();
            LoadFromFile();
        }
        public bool AddSensor(string id, KeyValuePair<int, int> position, int? measurement = null, string? name = null)
        {
            Sensor sensor = new Sensor(id, position, measurement, name);
            return AddSensor(sensor);
        }
        public bool AddSensor(Sensor s)
        {
            if(_sensors.Select(x => x.Id).Contains(s.Id))
            {
                return false;
            }
            _sensors.Add(s);
            return true;
        }

        public bool RemoveSensor(string id)
        {
            return false;
        }

        private bool LoadFromFile()
        {
            try
            {
                string jsonString = File.ReadAllText(_file);
                _sensors = JsonSerializer.Deserialize<List<Sensor>>(jsonString);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool SaveToFile()
        {
            try
            {
                using FileStream createStream = File.Create(_file);
                JsonSerializer.SerializeAsync(createStream, _sensors);
                createStream.DisposeAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool IsSensorRegistered(string id)
        {
            return _sensors.FirstOrDefault(x => string.Compare(x.Id, id, true) == 0) != null;
        }

        public int SensorCount()
        {
            return _sensors.Count;
        }

        public virtual void Dispose()
        {
            SaveToFile();
        }
    }
}
