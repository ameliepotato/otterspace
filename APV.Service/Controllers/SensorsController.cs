using APV.Service.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

[assembly: InternalsVisibleTo("APV.Service.Tests.Unit")]
namespace APV.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorsController : ControllerBase
    {
        private readonly ILogger<SensorsController> _logger;
        private readonly ISensorService _sensorService;
       

        public SensorsController(ILogger<SensorsController> logger, 
            ISensorService sensorService, 
            IMeasurementService measurementService)
        {
            _logger = logger;
            _sensorService = sensorService;
            _logger.LogInformation("Sensor controller created.");
        }

        [EnableCors("CorsPolicy")]
        [HttpPost]
        [Route("SaveSensors")]
        public string Save()
        {
            _logger.LogInformation($"Save sensors");
            try
            {
                List<Sensor>? sensors = JsonSerializer.Deserialize<List<Sensor>?>(HttpContext.Request.Form["sensors"]);
                _logger.LogInformation($"Sensors found: {sensors?.Count}");
                if (sensors == null)
                {
                    return "no sensors";
                }
                sensors.ForEach(x => x.Id = Guid.NewGuid().ToString());
                return _sensorService.SaveSensors(sensors).ToString();
            }
            catch (Exception e)
            {
                _logger.LogError($"Save sensors failed with error: {e.Message}");
                return e.Message;
            }
        }

        [EnableCors("CorsPolicy")]
        [HttpGet]
        [Route("GetSensors")]
        public string Get()
        {
            _logger.LogInformation($"Getting all sensors");
            List<Sensor> sensors = _sensorService.GetSensors();

            if (sensors == null || sensors.Count < 1)
            {
                return $"no sensors";
            }

            _logger.LogInformation($"Sensors retrieved: {sensors.Count}");

            return JsonSerializer.Serialize(sensors);
        }


        [EnableCors("CorsPolicy")]
        [HttpGet]
        [Route("GetPlan")]
        public string GetPlan()
        {
            _logger.LogInformation($"Getting plan");
            byte[] plan = _sensorService.GetPlan();
            _logger.LogInformation($"Plan length retrieved: {plan.Length}");
            return Convert.ToBase64String(plan);
        }

        [EnableCors("CorsPolicy")]
        [HttpPost]
        [Route("SavePlan")]
        public string SavePlan()
        {
            _logger.LogInformation($"Save plan");
            try
            { 
                var file = HttpContext.Request.Form.Files[0];
                System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(file.OpenReadStream());
                byte[] plan = binaryReader.ReadBytes((int)file.Length);
                _logger.LogInformation($"Plan size found: {plan?.Length}");
                if(plan == null || plan.Length<1)
                {
                    return "no plan";
                }
                return _sensorService.SetPlan(plan).ToString();
            }
            catch (Exception e)
            {
                _logger.LogError($"Save plan failed with error: {e.Message}");
                return e.Message;
            }
        }
    }
}