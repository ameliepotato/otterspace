using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace APV.Service.Services
{
    public class SensorService : ISensorService
    {
        private readonly ILogger<SensorService> _logger;
        private List<Sensor> _sensors;
        private string _file = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "sensorService.json";
        public SensorService(ILogger<SensorService> logger, string? file = null)
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
    }
}
