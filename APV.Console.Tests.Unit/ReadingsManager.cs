using Microsoft.Extensions.Logging;
using System.Text.Json;
using WireMock.Logging;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace APV.Console.Tests.Unit
{
    public class ReadingsManager 
    {
        ILogger<Console.ReadingsManager> _logger;
        [SetUp]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _logger = loggerFactory.CreateLogger<Console.ReadingsManager>();
        }

        [Test]
        public void GetAllLatestReadingsWorks()
        {
            string fakeApi = "http://localhost:37010/";
            APV.Console.ReadingsManager readingsManager = 
                new Console.ReadingsManager( _logger, fakeApi);
            
            List<object> list = new List<object>();
            var reading = new
            {
                SensorId = "Fake1",
                Value = 45,
                PositionX = 2,
                PositionY = 3,
                Time = DateTime.Now
            };
            list.Add(reading);

            reading = new
            {
                SensorId = "Fake2",
                Value = 22,
                PositionX = 2,
                PositionY = 3,
                Time = DateTime.Now.AddDays(-21)
            };
            list.Add(reading);

            string data = JsonSerializer.Serialize(list);

            var server = WireMockServer.Start(new WireMockServerSettings
            {
                Logger = new WireMockConsoleLogger(),
                Urls = new string[] { fakeApi }
            });

            server.Given(Request.Create().UsingGet().WithPath("/Readings/GetAllLatest"))
                .RespondWith(Response.Create().WithBody(data));            

            List<ReadingModel>? latest = readingsManager.GetAllLatestReadings();

            Assert.That(latest, Is.Not.Null);

            Assert.That(latest.Count, Is.EqualTo(2));

            Assert.That(latest.FindAll(s => s.SensorId == "Fake1").Count, Is.EqualTo(1));

            Assert.That(latest.DistinctBy(x => x.SensorId).Count, Is.EqualTo(latest.Count));

            Assert.That(latest.OrderByDescending(s => s.Time).ToList()[0].SensorId, Is.EqualTo(latest[0].SensorId));

            Assert.That(latest.OrderByDescending(s => s.Time).ToList().Last().SensorId, Is.EqualTo(latest.Last().SensorId));

            server.Stop();
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
