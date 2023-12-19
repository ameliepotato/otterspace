using Microsoft.Extensions.Logging;
using System.Drawing;

namespace APV.Console.Tests.Unit
{
    public class IndexModel
    {
        ILogger<Pages.IndexModel> _logger;
        [SetUp]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _logger = loggerFactory.CreateLogger<Pages.IndexModel>();
        }

        [Test]
        public void GetAverageIsSuccesful()
        {
            MockImplementations.ReadingsManager readingsManager = 
                new MockImplementations.ReadingsManager(MockImplementations.ReadingsManager.GetTwo());
            Pages.IndexModel page = new Pages.IndexModel(_logger, readingsManager);
            page.OnGet();
            Assert.That(page.Readings.Count, Is.EqualTo(2));
            Assert.That(page.AverageTemp()?.Value, Is.EqualTo(20));
        }

        [Test]
        public void GetReadingsIsSuccesful()
        {
            MockImplementations.ReadingsManager readingsManager =
                new MockImplementations.ReadingsManager(MockImplementations.ReadingsManager.GetOne());
            Pages.IndexModel page = new Pages.IndexModel(_logger, readingsManager);
            page.OnGet();
            Assert.That(page.Readings.Count, Is.EqualTo(1));
            Assert.That(page.Readings.Any(x => string.Compare(x.SensorId, "Test") == 0));
            Assert.That(page.Readings[0].ColorCode, Is.EqualTo("#709569"));
            Assert.That(page.Readings[0].Value, Is.EqualTo(10));
        }

        [Test]
        public void SetColorWorks()
        {
            ReadingModel readingModel = Pages.IndexModel.SetColor(new ReadingModel()
            {
                Value = Constants.IDEALTEMPERATURE - 1,
                SensorId = "Colder"
            });

            Assert.That(readingModel.ColorCode, Is.EqualTo("#97C935"));

            readingModel = Pages.IndexModel.SetColor(new ReadingModel()
            {
                Value = Constants.IDEALTEMPERATURE + 1,
                SensorId = "Warmer"
            });

            Assert.That(readingModel.ColorCode, Is.EqualTo("#9FC22F"));
        }
    }
}