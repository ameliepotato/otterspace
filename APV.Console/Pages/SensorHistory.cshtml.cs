using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace APV.Console.Pages
{
    public class SensorHistoryModel : PageModel
    {
        public List<SensorHistoryEntryModel> Entries ;
        public string ChartData { get; private set; }


        private ISensorHistoryManager _historyManager;
        private readonly ILogger<SensorHistoryModel> _logger;

        public SensorHistoryModel(ILogger<SensorHistoryModel> logger, ISensorHistoryManager historyManager)
        {
            _historyManager = historyManager;
            _logger = logger;
            Entries = new List<SensorHistoryEntryModel>();
            ChartData = "";
        }
        public void OnGet(string sensorId)
        {
            _logger.LogInformation($"On get entries for sensorId {sensorId}");
            Entries = _historyManager.GetSensorHistory(sensorId, DateTime.UtcNow.AddDays(-3), DateTime.UtcNow) ??
                new List<SensorHistoryEntryModel>();
            _logger.LogInformation($"Found {Entries.Count} entries");
            Entries = Entries.OrderBy(x => x.RegisteredOn).ToList();
            ChartData = GetChartInfo(Entries).ToUrl();
            _logger.LogInformation($"Chart data is {ChartData}");
        }

        [NonHandler]
        public static ChartInfo GetChartInfo(List<SensorHistoryEntryModel> entries)
        {
            ChartInfo chartInfo = new ChartInfo();
            chartInfo.type = "line";
            chartInfo.data = new dataChart()
            {
                labels = entries.Select(x => x.RegisteredOn.ToShortDateString()).ToList()
            };
            chartInfo.datasets = new dataSetChart()
            {
                data = entries.Select(x => x.Temperature).ToList(),
                label = "Temperatures",
            };
            return chartInfo;
        }
    }
}
