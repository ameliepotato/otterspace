using Microsoft.AspNetCore.DataProtection.KeyManagement;
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

        public ReadingController(ILogger<ReadingController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "SubmitReading")]
        public string Submit(string? id, int? temperature)
        {

            //if (!HttpContext.Request.Form.Keys.Contains("id") && !HttpContext.Request.Form.Keys.Contains("temperature"))
            //{
            //    return "both id and temperature are required";
            //}
            id = id??HttpContext.Request.Form["id"];
            if (string.IsNullOrEmpty(id))
            {
                return "no id";
            }
            temperature = temperature.HasValue? temperature.Value : Convert.ToInt32(HttpContext.Request.Form["temperature"]);

            if (!temperature.HasValue)
            {
                return "no temperature";
            }
            return "true";
        }
    }
}