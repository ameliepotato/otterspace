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


        [TestMethod]
        public void SaveAndGetSensorsWorks()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\SaveAndGetSensorsWorks.json";

            // Act
            Services.SensorService service = new Services.SensorService(_loggerSensorService, filePath);

            List<Sensor> sensors = new List<Sensor>();
            Sensor sensor = new Sensor("One", new Tuple<int, int>(0, 0), null);
            sensors.Add(sensor);
            sensor = new Sensor("Two", new Tuple<int, int>(150, 20), null);
            sensors.Add(sensor);

            bool success = service.SaveSensors(sensors);

            Assert.IsTrue(success);

            var s = service.FindSensor("One");
            Assert.IsNotNull(s);
            s = service.FindSensor("Two");
            Assert.IsNotNull(s);

            Assert.IsTrue(s.Position.Item1 == 150);
            Assert.IsTrue(s.Position.Item2 == 20);
        }



        [TestMethod]
        public void SetAndGetPlanWorks()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\plan.jpg";
            Assert.IsTrue(File.Exists(filePath));

            // Act
            Services.SensorService service = new Services.SensorService(_loggerSensorService);
            bool success = service.SetPlan(File.ReadAllBytes(filePath));

            // Assert
            Assert.IsTrue(success);
            byte[] plan = service.GetPlan();
            Assert.IsNotNull(plan);
            Assert.IsTrue(plan.Length > 0);
            Assert.AreEqual(plan.Length, File.ReadAllBytes(filePath).Length);
        }
    }
}