using APV.Service.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class MeasurementsService
    {
        private ILogger<MeasurementService> _logger;

        [TestInitialize]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _logger = loggerFactory.CreateLogger<MeasurementService>();
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

            Services.Measurement? measurement = measurementService.GetMeasurement(id);
            Assert.IsNotNull(measurement);
            Assert.AreEqual(33, measurement.Value);
        }        
    }
}
