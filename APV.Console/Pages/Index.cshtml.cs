using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("APV.Console.Tests.Unit")]
namespace APV.Console.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<ReadingModel> Readings { get; private set; }

        private IReadingsManager _readingsManager;

        public ReadingModel? AverageTemp()
        {
            if (Readings.Count > 0)
            {
                return SetColor(new ReadingModel()
                {
                    Value = (int)Readings.Average(x => x.Value)
                });
            }
            return null;
        }

        public IndexModel(ILogger<IndexModel> logger, IReadingsManager readingsManager)
        {
            _logger = logger;
            _readingsManager = readingsManager;
            Readings = new List<ReadingModel>();
        }

        public void OnGet()
        {
            List<ReadingModel> unprocessed = _readingsManager.GetReadings() ?? new List<ReadingModel>();
            foreach (ReadingModel reading in unprocessed)
            {
                Readings.Add(SetColor(reading));
            }
            _logger.LogInformation($"OnGet found {Readings.Count} readings");
        }

        [NonHandler]
        public static ReadingModel SetColor(ReadingModel readingModel)
        {
            TemperatureGradient temperatureGradient = new TemperatureGradient(
                Constants.MINTEMPERATURE,
                Constants.IDEALTEMPERATURE,
                Constants.COLOR_MINTEMPERATURE,
                Constants.COLOR_IDEALTEMPERATURE);
            if (readingModel.Value > Constants.IDEALTEMPERATURE)
            {
                temperatureGradient = new TemperatureGradient(
                Constants.IDEALTEMPERATURE,
                Constants.MAXTEMPERATURE,
                Constants.COLOR_IDEALTEMPERATURE,
                Constants.COLOR_MAXTEMPERATURE);
            }
            readingModel.ColorCode = temperatureGradient.ColorCodeByTemperature(readingModel.Value);
            return readingModel;
        }

    }
}