using APV.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("APV.Service.Tests.Unit")]
namespace APV.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadingController : ControllerBase
    {
        private readonly ILogger<ReadingController> _logger;
        private readonly IMeasurementService _measurementService;
        private readonly ISensorService _sensorService;
        public ReadingController(ILogger<ReadingController> logger, 
            ISensorService sensorService, 
            IMeasurementService measurementService)
        {
            _logger = logger;
            _measurementService = measurementService;
            _sensorService = sensorService;
            _logger.LogInformation("ReadingController created.");
        }

        [HttpPost(Name = "SubmitReading")]
        public string Submit(string? id, int? temperature)
        {
            _logger.LogInformation($"Submitted sensorid {id} and temperature {temperature} from query parameters");
            try
            {
                id = id ?? HttpContext.Request.Form["id"];
                _logger.LogInformation($"Request body sensorid: {id}");
                if (string.IsNullOrEmpty(id))
                {
                    return "no id";
                }

                if (!_sensorService.IsSensorRegistered(id))
                {
                    _logger.LogInformation($"Sensor {id} is not registered");
                    return "invalid id";
                }

                temperature = temperature.HasValue ? temperature.Value : Convert.ToInt32(HttpContext.Request.Form["temperature"]);

                _logger.LogInformation($"Temperature for sensor {id} will be set as {temperature}");

                if (!temperature.HasValue)
                {
                    return "no temperature";
                }

                return _measurementService.AddMeasurement(id, temperature.Value).ToString();
            }
            catch (Exception e)
            {
                _logger.LogError($"Add measurement failed with error: {e.Message}");
                return e.Message;
            }
        }

        [HttpGet(Name = "GetReading")]
        public string Get(string id)
        {
            _logger.LogInformation($"Getting temperature from sensorid {id}");
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogInformation($"No sensor id");
                return "no sensor id";
            }

            if (!_sensorService.IsSensorRegistered(id))
            {
                _logger.LogInformation($"Invalid sensor id {id}");
                return $"invalid sensor id";
            }

            Measurement? m = _measurementService.GetMeasurement(id);

            if (m == null)
            {
                return $"no temperature registered yet for {id}";
            }

            _logger.LogInformation($"Temperature from sensorid {id} is {m?.Value.ToString()}");
            return m.Value.ToString();
        }
    }
}