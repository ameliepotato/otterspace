using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using APV.Service.Services;

namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class ReadingController
    {
        protected ILogger<Controllers.ReadingController> _loggerController;
        protected ILogger<Service.Services.MeasurementService> _loggerService;


        [TestInitialize]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _loggerController = loggerFactory.CreateLogger<Controllers.ReadingController>();
            _loggerService = loggerFactory.CreateLogger<MeasurementService>();
        }


        [TestMethod]
        public void SubmitReadingSuccess()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\ValidSensorService.json";
            Services.SensorService ss = new Service.Services.SensorService(filePath);
            Controllers.ReadingController controller = new Controllers.ReadingController(_loggerController, ss, 
                new Services.MeasurementService(_loggerService, new MockImplementations.DataManager<Measurement>()));
            // Act
            string result = controller.Submit("Two", 3);

            // Assert
            Assert.AreEqual("true", result.ToLower());
        }

        [TestMethod]
        public void SubmitReadingInvalidSensor()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\ValidSensorService.json";
            Services.SensorService ss = new Services.SensorService(filePath);
            Controllers.ReadingController controller = 
                new Controllers.ReadingController(_loggerController, ss, 
                    new Services.MeasurementService(_loggerService, new MockImplementations.DataManager<Measurement>()));
            // Act
            string result = controller.Submit("Three", 3);

            // Assert
            Assert.AreEqual("invalid id", result.ToLower());
        }

        [TestMethod]
        public void GetReadingSuccessNoReadings()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\ValidSensorService.json";
            Services.SensorService ss = new Service.Services.SensorService(filePath);
            Controllers.ReadingController controller = 
                new Controllers.ReadingController(_loggerController, ss, 
                    new Services.MeasurementService(_loggerService, new MockImplementations.DataManager<Measurement>()));
            // Act
            string result = controller.Get("Two");

            // Assert
            Assert.AreEqual("no temperature registered yet for Two", result);
        }

        [TestMethod]
        public void GetReadingInvalidSensor()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\ValidSensorService.json";
            Services.SensorService ss = new Service.Services.SensorService(filePath);
            Controllers.ReadingController controller = 
                new Controllers.ReadingController(_loggerController, ss, 
                    new Services.MeasurementService(_loggerService, new MockImplementations.DataManager<Measurement>()));
            
            // Act
            string result = controller.Get("Three");

            // Assert
            Assert.AreEqual("invalid sensor id", result.ToLower());
        }
    }
}