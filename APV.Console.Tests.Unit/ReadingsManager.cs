using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            Tools.Server server = new Tools.Server();
            
            server.AddToResponseData("GetAllLatest", data);

            Task task = Task.Run(() =>
            {
                server.StartListening(fakeApi);
            });
            

            List<ReadingModel>? latest = readingsManager.GetAllLatestReadings();

            Assert.That(latest, Is.Not.Null);

            Assert.That(latest.Count, Is.EqualTo(2));

            Assert.That(latest.FindAll(s => s.SensorId == "Fake1").Count, Is.EqualTo(1));

            Assert.That(latest.DistinctBy(x => x.SensorId).Count, Is.EqualTo(latest.Count));

            Assert.That(latest.OrderByDescending(s => s.Time).ToList()[0].SensorId, Is.EqualTo(latest[0].SensorId));

            Assert.That(latest.OrderByDescending(s => s.Time).ToList().Last().SensorId, Is.EqualTo(latest.Last().SensorId));
        }
    }
}
