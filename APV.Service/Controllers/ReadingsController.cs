using APV.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text.Json;

[assembly: InternalsVisibleTo("APV.Service.Tests.Unit")]
namespace APV.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadingsController : ControllerBase
    {
        private readonly ILogger<ReadingsController> _logger;
        private readonly IMeasurementService _measurementService;
        private readonly ISensorService _sensorService;
        public ReadingsController(ILogger<ReadingsController> logger, 
            ISensorService sensorService, 
            IMeasurementService measurementService)
        {
            _logger = logger;
            _measurementService = measurementService;
            _sensorService = sensorService;
            _logger.LogInformation("Readings controller created.");
        }

        [HttpPost]
        [Route("SubmitReading")]
        public string SubmitReading(string? sensorid, int? temperature)
        {
            _logger.LogInformation($"Submitted sensorid {sensorid}, temperature {temperature}  from query parameters");
            try
            {
                sensorid = sensorid ?? HttpContext.Request.Form["sensorid"];
                _logger.LogInformation($"Request body sensorid: {sensorid}");
                if (string.IsNullOrEmpty(sensorid))
                {
                    return "no sensor id";
                }

                if (_sensorService.FindSensor(sensorid) == null)
                {
                    _logger.LogInformation($"Sensor {sensorid} is not registered");
                    return "invalid sensor id";
                }

                temperature = temperature.HasValue ? temperature.Value : Convert.ToInt32(HttpContext.Request.Form["temperature"]);

                _logger.LogInformation($"Temperature for sensor {sensorid} will be set as {temperature}");

                if (!temperature.HasValue)
                {
                    return "no temperature";
                }

                return _measurementService.AddMeasurement(new Measurement(sensorid, temperature.Value, DateTime.UtcNow)).ToString();
            }
            catch (Exception e)
            {
                _logger.LogError($"Add measurement failed with error: {e.Message}");
                return e.Message;
            }
        }

        [HttpGet]
        [Route("GetAllLatest")]
        public string GetAllLatest()
        {
            _logger.LogInformation($"Getting all latest readings");
            List<Reading> readings = new List<Reading>();
            List<Measurement>? m = _measurementService.GetAllLatestMeasurements();

            if (m == null || m.Count < 1)
            {
                return $"no measurements registered yet";
            }

            _logger.LogInformation($"Measurements retrieved: {m.Count}");
            _logger.LogInformation($"Getting sensors");
            
            foreach(Measurement measurement in m)
            {
                Sensor? sensor = _sensorService.FindSensor(measurement.SensorId);
                if(sensor == null)
                {
                    _logger.LogWarning($"Sensor {measurement.SensorId} not registered");
                }
                else
                {
                    Reading reading = new Reading() {
                        SensorId = measurement.SensorId,
                        PositionX = sensor.Position.Item1,
                        PositionY = sensor.Position.Item2,
                        Value = measurement.Value,
                        Time = measurement.Time??DateTime.MinValue
                    };
                    _logger.LogInformation($"Adding reading: {reading.SensorId} - {reading.Value} - {reading.Time}");
                    readings.Add(reading);
                }
            }
            _logger.LogInformation($"Found {readings.Count} readings");
            return JsonSerializer.Serialize(readings);
        }
    }
}