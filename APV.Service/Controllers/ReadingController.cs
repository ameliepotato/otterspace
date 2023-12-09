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
        private readonly ILogger<ReadingController>? _logger;
        private readonly MeasurementService _measurementService;
        private readonly SensorService _sensorService;
        public ReadingController(ILogger<ReadingController>? logger = null, SensorService? sensorService = null, MeasurementService? measurementService = null)
        {
            _logger = logger;
            _measurementService = measurementService ?? new MeasurementService();
            _sensorService = sensorService ?? new SensorService();
        }

        [HttpPost(Name = "SubmitReading")]
        public string Submit(string? id, int? temperature)
        { 
            id = id??HttpContext.Request.Form["id"];
            if (string.IsNullOrEmpty(id))
            {
                return "no id";
            }
            
            if(!_sensorService.IsSensorRegistered(id))
            {
                return "invalid id";
            }
            
            temperature = temperature.HasValue? temperature.Value : Convert.ToInt32(HttpContext.Request.Form["temperature"]);

            if (!temperature.HasValue)
            {
                return "no temperature";
            }


            return _measurementService.AddMeasurement(id, temperature.Value).ToString();
        }
    }
}