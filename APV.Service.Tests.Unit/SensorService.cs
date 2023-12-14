using Microsoft.VisualStudio.TestTools.UnitTesting;
using APV.Service.Controllers;
using Microsoft.AspNetCore.Mvc;
using APV.Service.Services;
using Microsoft.Extensions.Logging;

namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class SensorService
    {
        protected ILogger<Services.SensorService> _loggerSensorService;


        [TestInitialize]
        public void Setup()
        {
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            _loggerSensorService = loggerFactory.CreateLogger<Services.SensorService>();
        }

        [TestMethod]
        public void LoadFromInexistentFile()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Inexistent.json";

            // Act
            Services.SensorService service = new Services.SensorService(_loggerSensorService, filePath);

            // Assert
            Assert.AreEqual(0, service.SensorCount());
            Assert.IsNull(service.FindSensor("id"));

        }


        [TestMethod]
        public void LoadFromValidFile()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\ValidSensorService.json";

            // Act
            Services.SensorService service = new Services.SensorService(_loggerSensorService, filePath);

            // Assert
            Assert.AreEqual(2, service.SensorCount());
            Assert.IsNotNull(service.FindSensor("One"));
            Assert.IsNotNull(service.FindSensor("Two"));
            Assert.IsNull(service.FindSensor("Three"));
        }
    }
}