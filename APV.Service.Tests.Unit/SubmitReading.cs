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
            Services.SensorService ss = new Service.Services.SensorService();
            ss.AddSensor("doi", new KeyValuePair<int, int>(45, 5));
            ReadingController controller = new ReadingController(null, ss, new Services.MeasurementService());
            // Act
            string result = controller.Submit("doi", 3);

            // Assert
            Assert.AreEqual("true", result.ToLower());
        }
    }
}