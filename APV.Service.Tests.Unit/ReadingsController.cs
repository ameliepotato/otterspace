using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;

namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class ReadingsController
    {
        protected ILogger<Controllers.ReadingsController> _loggerController;
        protected ILogger<Services.MeasurementService> _loggerMeasurementsService;
        protected ILogger<Services.SensorService> _loggerSensorService;
        protected ILogger<MockImplementations.MongoDataManager> _loggerDatabaseManager;


        [TestInitialize]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _loggerController = loggerFactory.CreateLogger<Controllers.ReadingsController>();
            _loggerMeasurementsService = loggerFactory.CreateLogger<Services.MeasurementService>();
            _loggerSensorService = loggerFactory.CreateLogger<Services.SensorService>();
            _loggerDatabaseManager = loggerFactory.CreateLogger<MockImplementations.MongoDataManager>();
        }


        [TestMethod]
        public void SubmitReadingSuccess()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\ValidSensorService.json";
            Services.SensorService ss = new Services.SensorService(_loggerSensorService, filePath);
            Controllers.ReadingsController controller = new Controllers.ReadingsController(_loggerController, ss, 
                new Services.MeasurementService(_loggerMeasurementsService, 
                    new MockImplementations.MongoDataManager(
                        _loggerDatabaseManager)));
            // Act
            string result = controller.SubmitReading("Two", 3);

            // Assert
            Assert.AreEqual("true", result.ToLower());
        }

        [TestMethod]
        public void SubmitReadingInvalidSensor()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\ValidSensorService.json";
            Services.SensorService ss = new Services.SensorService(_loggerSensorService, filePath);
            Controllers.ReadingsController controller = 
                new Controllers.ReadingsController(_loggerController, ss, 
                    new Services.MeasurementService(_loggerMeasurementsService,
                         new MockImplementations.MongoDataManager(
                             _loggerDatabaseManager)));
            // Act
            string result = controller.SubmitReading("Three", 3);

            // Assert
            Assert.AreEqual("invalid sensor id", result.ToLower());
        }

        [TestMethod]
        public void GetReadingSuccessNoReadings()
        {
            // Arrange
            Services.SensorService ss = new Services.SensorService(_loggerSensorService);
            Controllers.ReadingsController controller = 
                new Controllers.ReadingsController(_loggerController, ss, 
                    new Services.MeasurementService(_loggerMeasurementsService,
                         new MockImplementations.MongoDataManager(_loggerDatabaseManager)));
            // Act
            string? result = controller.GetReadings();

            // Assert
            Assert.AreEqual("no measurements registered yet", result);
        }
    }
}