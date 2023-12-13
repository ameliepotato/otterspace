using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using APV.Service.Services;

namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class ReadingsController
    {
        protected ILogger<Controllers.ReadingsController> _loggerController;
        protected ILogger<Services.MeasurementService> _loggerService;


        [TestInitialize]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _loggerController = loggerFactory.CreateLogger<Controllers.ReadingsController>();
            _loggerService = loggerFactory.CreateLogger<Services.MeasurementService>();
        }


        [TestMethod]
        public void SubmitReadingSuccess()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\ValidSensorService.json";
            Services.SensorService ss = new Service.Services.SensorService(filePath);
            Controllers.ReadingsController controller = new Controllers.ReadingsController(_loggerController, ss, 
                new Services.MeasurementService(_loggerService, new MockImplementations.DataManager<Measurement>()));
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
            Services.SensorService ss = new Services.SensorService(filePath);
            Controllers.ReadingsController controller = 
                new Controllers.ReadingsController(_loggerController, ss, 
                    new Services.MeasurementService(_loggerService, new MockImplementations.DataManager<Measurement>()));
            // Act
            string result = controller.SubmitReading("Three", 3);

            // Assert
            Assert.AreEqual("invalid sensor id", result.ToLower());
        }

        [TestMethod]
        public void GetReadingSuccessNoReadings()
        {
            // Arrange
            Services.SensorService ss = new Service.Services.SensorService(null);
            Controllers.ReadingsController controller = 
                new Controllers.ReadingsController(_loggerController, ss, 
                    new Services.MeasurementService(_loggerService, new MockImplementations.DataManager<Measurement>()));
            // Act
            string? result = controller.GetReadings();

            // Assert
            Assert.AreEqual("no measurements registered yet", result);
        }
    }
}