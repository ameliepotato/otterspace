using APV.Service.Services;
using APV.Service.Tests.Unit.MockImplementations;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class MeasurementService
    {
        private ILogger<Services.MeasurementService> _logger;
        private ILogger<Database.MongoDataManager> _loggerDatabaseManager;

        [TestInitialize]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _logger = loggerFactory.CreateLogger<Services.MeasurementService>();
            _loggerDatabaseManager = loggerFactory.CreateLogger<Database.MongoDataManager>();
        }

        [TestMethod]
        public void AddThenGetMeasurementSuccesful()
        {
            Services.MeasurementService measurementService = 
                new Services.MeasurementService(_logger, 
                    new Database.MongoDataManager(
                        _loggerDatabaseManager, 
                        "mongodb://admin:Example@localhost:27017", 
                        "APVServiceTests",
                        "AddThenGetMeasurementSuccesful"+DateTime.Now.Ticks));

            Measurement measurement = new Measurement("Test", 33);
            bool ret = measurementService.AddMeasurement(measurement);            
            Assert.IsTrue(ret);

            List<Measurement>? measurements = measurementService.GetMeasurements();
            
            Assert.IsNotNull(measurements);
            Assert.AreEqual(1, measurements.Count);
            Assert.AreEqual(33, measurements.First().Value);
            Assert.AreEqual("Test", measurements.First().SensorId);
        }

        [TestMethod]
        public void AddThenGetMeasurementsSuccesful()
        {
            Services.MeasurementService measurementService =
                new Services.MeasurementService(_logger, 
                    new Database.MongoDataManager(
                        _loggerDatabaseManager,
                        "mongodb://admin:Example@localhost:27017",
                        "APVServiceTests",
                        "AddThenGetMeasurementsSuccesful"+DateTime.Now.Ticks));

            Measurement measurement = new Measurement("Test", 33);

            bool ret = measurementService.AddMeasurement(measurement);

            Assert.IsTrue(ret);

            measurement.Value = 35;
            measurement._id = Guid.NewGuid().ToString();
            measurement.Time = measurement.Time?.AddSeconds(1);//to make sure it's more recent

            ret = measurementService.AddMeasurement(measurement);

            Assert.IsTrue(ret);

            measurement.SensorId = "Test2";
            measurement.Value = 30;
            measurement._id = Guid.NewGuid().ToString();

            ret = measurementService.AddMeasurement(measurement);

            Assert.IsTrue(ret);

            List<Measurement>? measurements = measurementService.GetMeasurements();

            Assert.IsNotNull(measurements);
            Assert.AreEqual(2, measurements.Count);
            Assert.AreEqual(30, measurements.Where(x => x.SensorId == "Test2").First().Value);
            Assert.AreEqual(35, measurements.Where(x => x.SensorId == "Test").First().Value);
        }

        [TestMethod]
        public void CreateIndexByIdAndDateSuccess()
        {
            Services.MeasurementService measurementService =
               new Services.MeasurementService(_logger, 
                new Database.MongoDataManager(
                    _loggerDatabaseManager, 
                    "mongodb://admin:Example@localhost:27017", 
                    "APVServiceTests", 
                    "CreateIndexByIdAndDateSuccess"));

            bool ret = measurementService.CreateIndex();

            Assert.IsTrue(ret);
        }
    }
}
