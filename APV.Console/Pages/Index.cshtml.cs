using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APV.Console.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<ReadingModel> Readings { get; private set; }

        private IReadingsManager _readingsManager;

        public IndexModel(ILogger<IndexModel> logger, IReadingsManager readingsManager)
        {
            _logger = logger;
            _readingsManager = readingsManager;
            Readings = new List<ReadingModel>();
        }

        public void OnGet()
        {
            Readings = _readingsManager.GetReadings() ?? new List<ReadingModel>();
            _logger.LogInformation($"OnGet found {Readings.Count} readings");
        }
    }
}