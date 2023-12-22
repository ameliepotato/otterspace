using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace APV.Console.Pages
{
    public class SensorHistoryModel : PageModel
    {
        public List<SensorHistoryEntryModel> _entries = new List<SensorHistoryEntryModel>();

        private ISensorHistoryManager _historyManager;
        private readonly ILogger<SensorHistoryModel> _logger;

        public SensorHistoryModel(ILogger<SensorHistoryModel> logger, ISensorHistoryManager historyManager)
        {
            _historyManager = historyManager;
            _logger = logger;
        }
        public void OnGet(string sensorId)
        {
            _entries = _historyManager.GetSensorHistory(sensorId, DateTime.UtcNow.AddDays(-3), DateTime.UtcNow) ??
                new List<SensorHistoryEntryModel>();
        }
    }
}
