using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using WireMock.Logging;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace APV.Console.Tests.Unit
{
    public class SensorHistoryManager
    {

        ILogger<Console.SensorHistoryManager> _logger;
        [SetUp]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _logger = loggerFactory.CreateLogger<Console.SensorHistoryManager>();
        }

        [Test]
        public void GetSensorHistoryWorks()
        {
            string fakeApi = "http://localhost:37010/";
            APV.Console.SensorHistoryManager historyManager =
                new Console.SensorHistoryManager(_logger, fakeApi);

            List<object> list = new List<object>();
            var reading = new
            {
                Temperature = 45,
                RegisteredOn = DateTime.Now
            };
            list.Add(reading);

            reading = new
            {
                Temperature = -1,
                RegisteredOn = DateTime.Now.AddDays(-3)
            };
            list.Add(reading);

            string data = JsonSerializer.Serialize(list);

            var server = WireMockServer.Start(new WireMockServerSettings
            {
                Logger = new WireMockConsoleLogger(),
                Urls = new string[] { fakeApi }
            });

            server.Given(Request.Create().UsingGet().WithPath("/Readings/GetSensorHistory"))
                .RespondWith(Response.Create().WithBody(data));

            List<SensorHistoryEntryModel>? entries = 
                historyManager.GetSensorHistory("Fake", DateTime.MinValue, null);

            Assert.That(entries, Is.Not.Null);

            Assert.That(entries.Count, Is.EqualTo(2));

        }
    }
}
