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
        private readonly ILogger _logger;
        private readonly IMeasurementService _measurementService;
        private readonly ISensorService _sensorService;
        public ReadingController(ILogger logger, ISensorService sensorService, IMeasurementService measurementService)
        {
            _logger = logger;
            _measurementService = measurementService;
            _sensorService = sensorService;
        }

        [HttpPost(Name = "SubmitReading")]
        public string Submit(string? id, int? temperature)
        {
            try
            {
                id = id ?? HttpContext.Request.Form["id"];
                if (string.IsNullOrEmpty(id))
                {
                    return "no id";
                }

                if (!_sensorService.IsSensorRegistered(id))
                {
                    return "invalid id";
                }

                temperature = temperature.HasValue ? temperature.Value : Convert.ToInt32(HttpContext.Request.Form["temperature"]);

                if (!temperature.HasValue)
                {
                    return "no temperature";
                }


                return _measurementService.AddMeasurement(id, temperature.Value).ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet(Name = "GetReading")]
        public string Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "no id";
            }

            if (!_sensorService.IsSensorRegistered(id))
            {
                return $"invalid sensor id";
            }

            Measurement? m = _measurementService.GetMeasurement(id);

            if (m == null)
            {
                return $"no temperature registered yet for {id}";
            }

            return m.Value.ToString();
        }
    }
}