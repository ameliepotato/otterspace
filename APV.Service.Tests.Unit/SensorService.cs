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
        public void LoadFromInexistentFile()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Inexistent.json";

            // Act
            Services.SensorService service = new Services.SensorService(filePath);

            // Assert
            Assert.AreEqual(0, service.SensorCount());
            Assert.AreEqual(false, service.IsSensorRegistered("id"));

        }


        [TestMethod]
        public void LoadFromValidFile()
        {
            // Arrange
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestData\\ValidSensorService.json";

            // Act
            Services.SensorService service = new Services.SensorService(filePath);

            // Assert
            Assert.AreEqual(2, service.SensorCount());
            Assert.AreEqual(true, service.IsSensorRegistered("One"));
            Assert.AreEqual(true, service.IsSensorRegistered("Two"));
            Assert.AreEqual(false, service.IsSensorRegistered("Three"));
        }
    }
}