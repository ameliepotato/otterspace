using Microsoft.AspNetCore.Mvc;

namespace APV.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly ILogger<TemperatureController> _logger;

        public TemperatureController(ILogger<TemperatureController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTemperature")]
        public IEnumerable<TemperatureRecord> Get(int id)
        {
            return Enumerable.Range(1, 5).Select(index => new TemperatureRecord
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                ID = id++
            })
            .ToArray();
        }
    }
}