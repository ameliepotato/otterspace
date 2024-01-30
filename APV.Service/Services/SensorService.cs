using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace APV.Service.Services
{
    public class SensorService : ISensorService
    {
        private readonly ILogger<SensorService> _logger;
        private List<Sensor> _sensors;
        private string _file = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "sensorService.json";
        private string _plan = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "plan.jpg";

        public SensorService(ILogger<SensorService> logger, string? file = null, string? plan = null)
        {
            _logger = logger;
            if (!string.IsNullOrEmpty(file))
            {
                _logger.LogInformation($"File {file} will replace the default service config file {_file}");
                if (File.Exists(file))
                {
                    _file = file;
                }
                else
                {
                    _logger.LogInformation($"File {file} does not exist.");
                }
            }
            _plan = plan ?? _plan;
            _sensors = new List<Sensor>();
            LoadFromFile();
        }

        public bool LoadFromFile()
        {
            _logger.LogInformation($"Loading sensors from file {_file}");
            try
            {
                if (File.Exists(_file))
                {
                    string jsonString = File.ReadAllText(_file);
                    _sensors = JsonSerializer.Deserialize<List<Sensor>>(jsonString) ?? new List<Sensor>();
                }
                else
                {
                    _logger.LogError($"File {_file} not exist");
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Configuration error: {e.Message}");
                return false;
            }
            _logger.LogInformation($"Loaded {_sensors.Count} sensors from file {_file}");
            return true;
        }

        public bool SaveToFile()
        {
            _logger.LogInformation($"Saving sensors to file {_file}");
            try
            {
                string jsonString = JsonSerializer.Serialize(_sensors) ?? "[]";

                File.WriteAllText(_file, jsonString); 
            }
            catch (Exception e)
            {
                _logger.LogError($"Write error: {e.Message}");
                return false;
            }
            _logger.LogInformation($"Saved {_sensors.Count} sensors to file {_file}");
            return true;
        }

        public Sensor? FindSensor(string id)
        {
            Sensor? result = _sensors.FirstOrDefault(x => string.Compare(x.Id, id, true) == 0);
            if(result == null)
            {
                _logger.LogInformation($"Sensor {id} not found");
            }
            return result;
        }

        public int SensorCount()
        {
            return _sensors.Count;
        }

        public bool SaveSensors(List<Sensor> sensors)
        {
            _sensors=sensors;
            SaveToFile();
            return true;
        }

        public List<Sensor> GetSensors()
        {
            return _sensors;
        }

        public byte[] GetPlan()
        {
            return File.ReadAllBytes(_plan);
        }

        public bool SetPlan(byte[] plan)
        {
            File.WriteAllBytes(_plan, plan);
            return true;
        }
    }
}
