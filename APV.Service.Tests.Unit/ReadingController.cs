using Microsoft.VisualStudio.TestTools.UnitTesting;
using APV.Service.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class ReadingController
    {
        [TestMethod]
        public void SubmitReadingSuccess()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\ValidSensorService.json";
            Services.SensorService ss = new Service.Services.SensorService(filePath);
            Controllers.ReadingController controller = new Controllers.ReadingController(null, ss, 
                new Services.MeasurementService("mongodb://admin:Example@localhost:27017"));
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
            Services.SensorService ss = new Service.Services.SensorService(filePath);
            Controllers.ReadingController controller = new Controllers.ReadingController(null, ss, new Services.MeasurementService());
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
            Controllers.ReadingController controller = new Controllers.ReadingController(null, ss, new Services.MeasurementService());
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
            Controllers.ReadingController controller = new Controllers.ReadingController(null, ss, new Services.MeasurementService());
            // Act
            string result = controller.Get("Three");

            // Assert
            Assert.AreEqual("invalid sensor id", result.ToLower());
        }
    }
}