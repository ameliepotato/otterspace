using Microsoft.AspNetCore.Mvc;

namespace APV.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadingController : ControllerBase
    {
        private readonly ILogger<ReadingController> _logger;

        public ReadingController(ILogger<ReadingController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "SubmitReading")]
        public string Submit(string id, int temperature)
        {
            return "true";
        }
    }
}