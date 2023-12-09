using System.Text.Json;

namespace APV.Service.Services
{
    public class SensorService
    {
        private List<Sensor> _sensors;
        private string _file = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "sensorService.json";
        public SensorService(string? file = null)
        {
            if (!string.IsNullOrEmpty(file))
            {
                if (!File.Exists(file))
                {
                    try
                    {
                        File.Create(file);
                    }
                    catch (Exception)
                    {
                        file = null;
                    }
                }
                _file = file ?? _file;
            }
            _sensors = new List<Sensor>();
            LoadFromFile();
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
