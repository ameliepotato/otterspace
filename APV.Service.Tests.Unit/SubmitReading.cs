using Microsoft.VisualStudio.TestTools.UnitTesting;
using APV.Service.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class SubmitReading
    {
        [TestMethod]
        public void SubmitReadingSuccess()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\ValidSensorService.json";
            Services.SensorService ss = new Service.Services.SensorService(filePath);
            ReadingController controller = new ReadingController(null, ss, new Services.MeasurementService());
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
            ReadingController controller = new ReadingController(null, ss, new Services.MeasurementService());
            // Act
            string result = controller.Submit("Three", 3);

            // Assert
            Assert.AreEqual("invalid id", result.ToLower());
        }
    }
}