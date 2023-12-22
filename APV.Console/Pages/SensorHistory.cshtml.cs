using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace APV.Console.Pages
{
    public class SensorHistoryModel : PageModel
    {
        public List<SensorHistoryEntryModel> _entries = new List<SensorHistoryEntryModel>();

        private readonly ILogger<SensorHistoryModel> _logger;

        public SensorHistoryModel(ILogger<SensorHistoryModel> logger)
        {
            _logger = logger;
        }
        public void OnGet(string sensorId)
        {
            SensorHistoryManager historyManager = new SensorHistoryManager(_logger);
            _entries = historyManager.GetSensorHistory(sensorId, DateTime.UtcNow.AddDays(-3), DateTime.UtcNow) ??
                new List<SensorHistoryEntryModel>();
        }
    }
}
