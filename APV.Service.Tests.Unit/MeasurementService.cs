using APV.Service.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class MeasurementService
    {
        private ILogger<Services.MeasurementService> _logger;

        [TestInitialize]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _logger = loggerFactory.CreateLogger<Services.MeasurementService>();
        }

        [TestMethod]
        public void AddThenGetMeasurementSuccesful()
        {
            Services.MeasurementService measurementService = 
                new Services.MeasurementService(_logger, new MockImplementations.DataManager<Measurement>());

            Assert.IsTrue(measurementService.IsConnected());

            string id = "Test";
            bool ret = measurementService.AddMeasurement(id, 33);
            
            Assert.IsTrue(ret);

            List<Measurement>? measurement = measurementService.GetMeasurements();
            
            Assert.IsNotNull(measurement);
            Assert.AreEqual(1, measurement.Count);
            Assert.AreEqual(33, measurement.First().Value);
            Assert.AreEqual("Test", measurement.First().SensorId);
        }

        [TestMethod]
        public void AddThenGetMeasurementsSuccesful()
        {
            Services.MeasurementService measurementService =
                new Services.MeasurementService(_logger, new MockImplementations.DataManager<Measurement>());

            Assert.IsTrue(measurementService.IsConnected());

            string id = "Test";
            bool ret = measurementService.AddMeasurement(id, 33);

            Assert.IsTrue(ret);

            ret = measurementService.AddMeasurement(id, 35);

            Assert.IsTrue(ret);

            ret = measurementService.AddMeasurement("Test2", 30);

            Assert.IsTrue(ret);

            List<Measurement>? measurement = measurementService.GetMeasurements();

            Assert.IsNotNull(measurement);
            Assert.AreEqual(2, measurement.Count);
            Assert.AreEqual(30, measurement.Where(x => x.SensorId == "Test2").First().Value);
            Assert.AreEqual(35, measurement.Where(x => x.SensorId == "Test").First().Value);
        }
    }
}
