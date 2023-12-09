using Microsoft.VisualStudio.TestTools.UnitTesting;
using APV.Service.Controllers;
using Microsoft.AspNetCore.Mvc;
using APV.Service.Services;
namespace APV.Service.Tests.Unit
{
    [TestClass]
    public class SensorService
    {
        [TestMethod]
        public void LoadFromFileSuccess()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\LoadFromFileSuccess.json";

            // Act
            Services.SensorService service = new Services.SensorService(filePath);

            // Assert
            Assert.AreEqual(2, service.SensorCount());
        }


        [TestMethod]
        public void SaveToFileSuccess()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\SaveToFileSuccess.json";

            // Act
            using (Services.SensorService service1 = new Services.SensorService(filePath))
            {
                service1.AddSensor("One", new KeyValuePair<int, int>(4, 5));
                service1.AddSensor("Two", new KeyValuePair<int, int>(3, 4));
            }
            Services.SensorService service2 = new Services.SensorService(filePath);

            // Assert
            Assert.AreEqual(2, service2.SensorCount());
        }
    }
}