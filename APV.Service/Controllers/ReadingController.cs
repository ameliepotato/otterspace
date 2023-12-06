using Microsoft.AspNetCore.DataProtection.KeyManagement;
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
        public string Submit()
        {
            if(!HttpContext.Request.Form.Keys.Contains("id") && !HttpContext.Request.Form.Keys.Contains("temperature"))
            {
                return "both id and temperature are required";
            }
            string id = HttpContext.Request.Form["id"];
            if (string.IsNullOrEmpty(id))
            {
                return "no id";
            }
            string temp = HttpContext.Request.Form["temperature"];
            if (string.IsNullOrEmpty(temp))
            {
                return "no temperature";
            }
            return "true";
        }
    }
}