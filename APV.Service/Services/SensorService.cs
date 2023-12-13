using System.Text.Json;

namespace APV.Service.Services
{
    public class SensorService : ISensorService
    {
        private List<Sensor> _sensors;
        private string _file = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "sensorService.json";
        public SensorService(string? file = null)
        {
            if (!string.IsNullOrEmpty(file))
            {
                if (File.Exists(file))
                {
                    _file = file;
                }
            }
            _sensors = new List<Sensor>();
            LoadFromFile();
        }

        public bool LoadFromFile()
        {
            try
            {
                if (File.Exists(_file))
                {
                    string jsonString = File.ReadAllText(_file);
                    _sensors = JsonSerializer.Deserialize<List<Sensor>>(jsonString) ?? new List<Sensor>();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
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
    }
}
